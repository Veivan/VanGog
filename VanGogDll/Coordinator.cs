using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VanGogDll
{
	/// <summary>
	/// Класс занимается расчётом абсолютных координат строки и её сегментов
	/// </summary>
	internal class Coordinator
	{
		private DateTime startDate;
		private DateTime finishDate;
		private List<DataRow> DataRows;

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="_DataRows"> Список строк для отображения на графике</param>
		/// <param name="pack"> Данные об Этапе (Проекте)</param>
		internal Coordinator(List<DataRow> _DataRows, DataPack pack)
		{
			startDate = pack.StartDate;
			finishDate = pack.FinishDate;
			DataRows = _DataRows;
		}

		internal void CalcColumns()
		{
			foreach (var row in DataRows)
			{
				row.CalcColumns(startDate, finishDate);
			}

			// Находим длину нименьшего сегмента в колонках.
			var minSegLength = DataRows.SelectMany(e => e.Segments.ToArray())
				.Min(e => e.FinishColumn - e.StartColumn);

			// Определяем ширину колонки
			Constants.SetColWidth(minSegLength);
		}

		/// <summary>
		/// Оптимизация строк и сегментов
		/// </summary>
		/// <param name="NumCols"> Количество месяцев (колонок грида)</param>
		internal void Optimize(int NumCols)
		{
			// Список заюзанных всеми сегментами колонок грида
			HashSet<int> UsedColumns = new HashSet<int>();
			foreach (var row in DataRows)
			{
				List<int> rowUse = row.GetUsedCols();
				UsedColumns.UnionWith(rowUse);
			}
			var UsedColumnsList = UsedColumns.OrderBy(e => e).ToList();

			var b = 1;
			if (b == 1)
			{
				//Удаление пустых колонок
				foreach (var row in DataRows)
				{
					foreach (var seg in row.Segments)
					{
						// Ищем в списке количество занятых колонок до начала сегмента
						var countBeforeStart = UsedColumnsList.Where(e => e < seg.StartColumn).Count();

						// Ищем в списке количество занятых колонок внутри сегмента
						var lastcol = seg.FinishColumn + Constants.BoxLenInColumns;
						var countInSeg = UsedColumnsList
							.Where(e => e > seg.StartColumn && e <= lastcol - Constants.BoxLenInColumns).Count();
						var newFin = countBeforeStart + countInSeg;
						seg.SetColumn(true, countBeforeStart);
						seg.SetColumn(false, newFin);
					}
				}

				foreach (var row in DataRows)
					row.SetRowColumns();
			}
		}

		/// <summary>
		/// Определение ширины колонки в соответствии с выбранным форматом страницы
		/// </summary>
		/// <param name="ppi">Разрешение</param>
		internal void Zoom(Int32 ppi)
		{
			var fRow = DataRows.OrderBy(e => e.Order).First();
			var width = (fRow.FinishColumn - fRow.StartColumn) * Constants.ColWidth + Constants.boxL;
			Constants.CheckOrientation(width, ppi);

			if (Constants.koefIncrease > 1)
			{
				// Заново Определяем ширину колонки
				Constants.SetColWidth(Constants.koefIncrease);
			}


		}
	}
}
