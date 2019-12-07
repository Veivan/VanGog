using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace VanGogDll
{
	/// <summary>
	/// Класс описывает структуру данных, которая будет отображаться на графике
	/// </summary>
	class RowsPack
	{
		/// <summary>
		/// Смещение по горизонтали
		/// </summary>
		internal int MarginX
		{
			get { return Constants.marginX; }
		}

		/// <summary>
		/// Смещение по вертикали
		/// </summary>
		internal int MarginY
		{
			get { return Constants.marginY; }
		}

		/// <summary>
		/// Высота заголовка таблицы месяцев
		/// </summary>
		internal int СaptionH
		{
			get { return Constants.captionH; }
		}

		internal int NumCols { get; private set; }

		/// <summary>
		/// Список строк для отображения на графике
		/// </summary>
		internal List<DataRow> DataRows = new List<DataRow>();

		/// <summary>
		/// Число строк для отображения на графике
		/// </summary>
		internal int RowCount
		{
			get
			{
				return DataRows.Count;
			}
		}

		/// <summary>
		/// Общая высота графика
		/// </summary>
		internal int Heght { get; private set; }

		/// <summary>
		/// Общая ширина графика
		/// </summary>
		internal int Width { get; private set; }

//		internal int StartMonth { get; private set; }

		/// <summary>
		/// true - СГР, false - СГПЭР
		/// </summary>
		internal bool isSGR { get; private set; }

		/// <summary>
		/// Наименование темы
		/// </summary>
		internal string Theme { get; set; }

		/// <summary>
		/// Наименование заказа
		/// </summary>
		internal string Zakaz { get; set; }

		private GraphShablon shablon;

		private Coordinator coordinator;

		/// <summary>
		/// Constructor
		/// Подготовка данных для отображения на основе данных Этапа (Проекта)
		/// </summary>
		/// <param name="pack"> Данные об Этапе (Проекте)</param>
		internal RowsPack(DataPack pack)
		{
			isSGR = pack.IsSGR;
			shablon = new GraphShablon(isSGR, pack.Name, pack.Theme, pack.Zakaz);

			foreach (var work in pack.ListSGR.OrderBy(e => e.GlobOrder))
			{
				var row = DataRows.FirstOrDefault(e => e.KS == work.NS);
				if (row == null || (!row.IsEmpty && !row.IsSameDate(work.NS, work.StartDate)))
				{
					row = new DataRow(work.NS, work.KS);
					var maxNumber = 0;
					if (DataRows.Any())
						maxNumber = DataRows.Max(e => e.Order);
					row.Order = maxNumber + 1;
					DataRows.Add(row);
				}
				row.AddWork(work, pack.IsPrj);
			}

			coordinator = new Coordinator(DataRows, pack);
		}

		/// <param name="ppi">Разрешение</param>
		internal void Prepare(DataPack pack, Int32 ppi)
		{
			coordinator.CalcColumns();

			NumCols = (13 - pack.StartDate.Month + 12 * (pack.FinishDate.Year - pack.StartDate.Year)) * Constants.grad;
			coordinator.Optimize(NumCols);
			coordinator.Zoom(ppi);
			shablon.SetHeight();

			#region Определение точек соединения сегментов
			foreach (var row in DataRows.OrderBy(e => e.Order).Where(e => e.Order > 1))
			{
				var segL = row.Segments.OrderBy(e => e.Order).First();
				var segR = row.Segments.OrderBy(e => e.Order).Last();
				foreach (var seg in row.Segments)
				{
					DataRow row2Connect = null;
					Segment parSeg = null;
					var work = pack.ListSGR.FirstOrDefault(e => e.ID == seg.ID);
					// Сначала ищем родительский сегмент
					if (work.ParentID > 0)
					{
						row2Connect = DataRows.FirstOrDefault(e => e.Segments.Exists(s => s.ID == work.ParentID) );
						if (row2Connect != null)
						{
							parSeg = row2Connect.Segments.FirstOrDefault(e => e.ID == work.ParentID);
							seg.AddVertPoint(row2Connect.Order, parSeg.ID, Constants.numSide.sLeft);
							seg.AddVertPoint(row2Connect.Order, parSeg.ID, Constants.numSide.sRight);
							seg.DesideAboutBounds(parSeg, Constants.numSide.sBoth);
						}
					}
					else
					{
						if (seg == segL)
						{
							// Если это первый сегмент, то ищем строку сопряжения.
							// Это одна из предыдущих строк с тем же номером начального события, что и текущая,
							// либо при отсутствии - первая строка.
							row2Connect = DataRows
								.FirstOrDefault(e => e.Segments.Exists(s => s.NS == seg.NS) && e.Order < row.Order);
							if (row2Connect != null)
							{
								parSeg = row2Connect.Segments.First(e => e.NS == seg.NS);
							}
							else
							{
								row2Connect = DataRows.First(e => e.Order == 1);
								parSeg = row2Connect.Segments.OrderBy(e => e.Order).First();
							}
							seg.AddVertPoint(row2Connect.Order, parSeg.ID, Constants.numSide.sLeft);
							seg.DesideAboutBounds(parSeg, Constants.numSide.sLeft);
						}
						if (seg == segR)
						{
							// Если это последний сегмент, то ищем строку сопряжения.
							// Это одна из предыдущих строк с тем же номером конечного события, что и текущая,
							// либо при отсутствии - первая строка.
							row2Connect = DataRows
								.FirstOrDefault(e => e.Segments.Exists(s => s.KS == seg.KS) && e.Order < row.Order);
							if (row2Connect != null)
							{
								parSeg = row2Connect.Segments.First(e => e.KS == seg.KS);
							}
							else
							{
								row2Connect = DataRows.First(e => e.Order == 1);
								parSeg = row2Connect.Segments.OrderBy(e => e.Order).First();
							}
							seg.AddVertPoint(row2Connect.Order, parSeg.ID, Constants.numSide.sRight);
							seg.DesideAboutBounds(parSeg, Constants.numSide.sRight);
						}
					}
				}
			}
			#endregion

			// Вычисление координат строк и сегментов
			foreach (var row in DataRows.OrderBy(e => e.Order))
				row.PrepareCoords();

			var VHeightConst = MarginY + shablon.Height + СaptionH;
			#region Определение координат точек соединения сегментов
			foreach (var row in DataRows.OrderBy(e => e.Order).Where(e => e.Order > 1))
			{
				foreach (var seg in row.Segments.Where(e => e.HaveVertPoints))
				{
					var pt = seg.GetPoint(Constants.numSide.sLeft);
					if (pt != null)
					{
						var row2Connect = DataRows.First(e => e.Order == pt.row2connect);
						var VHeight = VHeightConst +
							DataRows.Where(e => e.Order < row2Connect.Order).Sum(e => e.Height);
						var parSeg = row2Connect.Segments.First(e => e.ID == pt.segID);
						pt.SetCoords(parSeg.VerticalLeftX, VHeight + parSeg.VerticalLeftY);
					}
					pt = seg.GetPoint(Constants.numSide.sRight);
					if (pt != null)
					{
						var row2Connect = DataRows.First(e => e.Order == pt.row2connect);
						var VHeight = VHeightConst +
							DataRows.Where(e => e.Order < row2Connect.Order).Sum(e => e.Height);
						var parSeg = row2Connect.Segments.First(e => e.ID == pt.segID);
						pt.SetCoords(parSeg.VerticalRightX, VHeight + parSeg.VerticalRightY);
					}
				}
			}
			#endregion

			foreach (var row in DataRows.OrderBy(e => e.Order).Where(e => e.Order > 1))
			{
				row.CorrectCoords();
				var VHeight = VHeightConst +
					DataRows.Where(e => e.Order < row.Order).Sum(e => e.Height);

				foreach (var rowLow in DataRows.OrderBy(e => e.Order).Where(e => e.Order > row.Order))
				{
					rowLow.UpdateVertPoints(row, VHeight);
				}
			}

			foreach (var row in DataRows.OrderBy(e => e.Order).Where(e => e.Order > 1))
			{
				row.UpdateVertPointsStyle();
			}
			
			//Width = Math.Max(maxWidth + MarginX * 2, shablon.Width);
			NumCols = DataRows.Select(e => e.FinishColumn).Max();
			Width = Math.Max(NumCols * Constants.ColWidth + Constants.boxL + MarginX * 2, shablon.Width);
			Heght = MarginY + shablon.Height + СaptionH + DataRows.Sum(e => e.Height) + MarginY;        			
		}
		
		/// <summary>
		/// Рисование сетки по количеству месяцев
		/// </summary>
		internal void DrawGrid(Graphics gfx)
		{
/*			int lineLenghX = Width - MarginX * 2;
			int lineLenghY = СaptionH + DataRows.Sum(e => e.Height);

			var x = MarginX;
			var y = MarginY + shablon.Heght;
			using (Pen p = new Pen(Color.Blue))
			{
				// Grid caption
				p.DashStyle = System.Drawing.Drawing2D.DashStyle.Solid;
				gfx.DrawLine(p, x, y, lineLenghX, y);
				y += СaptionH;
				gfx.DrawLine(p, x, y, lineLenghX, y);

				p.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
				// Horizontal
				for (int i = 0; i < RowCount; i++)
				{
					y += DataRows[i].Height;
					gfx.DrawLine(p, x, y, lineLenghX, y);
				}

				// Vertical
				x = MarginX;
				y = MarginY + shablon.Heght;
				for (int i = 0; i <= NumCols; i++)
				{
					gfx.DrawLine(p, x, y, x, y + lineLenghY);
					x += ColWidth;
				}
			}
			// Номера месяцев
			using (Font font = new Font(Constants.font, Constants.font4grid))
			using (SolidBrush brush = new SolidBrush(Color.Black))
			{
				StringFormat sf = new StringFormat();
				sf.Alignment = StringAlignment.Center;
				sf.LineAlignment = StringAlignment.Center;

				x = MarginX;
				y = MarginY + shablon.Heght;
				var stMonth = StartMonth - 1;
				for (int i = 0; i < NumCols; i++)
				{
					stMonth++;
					var colBounds = new Rectangle(x, y, ColWidth, СaptionH);
					gfx.DrawString(stMonth.ToString(), font, brush, colBounds, sf);
					if (stMonth == 12) stMonth = 0;
					x += ColWidth;
				}
			} */
		}

		/// <summary>
		/// Рисование графика
		/// </summary>
		internal void DrawRows(Graphics gfx)
		{
			var startX = MarginX;
			//var startY = MarginY + shablon.Height + СaptionH;
			var startY = MarginY + СaptionH;
			foreach (var row in DataRows.OrderBy(e => e.Order))
			{
				row.Draw(gfx, startY);
				startY += row.Height;
			}
		}

		internal void DrawShablon(Graphics gfx)
		{
			shablon.Draw(gfx, MarginX, MarginY);
		}

		/// <summary>
		/// Получение массива высот строк
		/// </summary>
		internal Int32[] GetRowsHeights()
		{
			var arr = new Int32[RowCount];
			Int32 i = 0;
			foreach (var row in DataRows.OrderBy(e => e.Order))
			{
				arr[i] = row.Height;
				i++;
			}
			return arr;
		}

	}
}
