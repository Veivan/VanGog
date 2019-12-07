using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace VanGogDll
{
	/// <summary>
	/// Класс содержит данные для отображения одной строки на графике
	/// </summary>
	class DataRow
	{
		internal int Order;
		internal int Height;
		internal int NS { get; private set; }
		internal int KS { get; private set; }

		/// <summary>
		/// Расстояние по вертикали от 0 до средней линии строки.
		/// По этой линии будет проводиться линия соединения квадратов.
		/// </summary>
		internal int Mediana{ get; private set;	}

		/// <summary>
		/// Порядковый номер клеточки месяца начала строки среди NumCols
		/// </summary>
		internal int StartColumn { get; private set; }

		/// <summary>
		/// Порядковый номер клеточки месяца конца строки среди NumCols
		/// </summary>
		internal int FinishColumn { get; private set; }

		/// <summary>
		/// Список сегментов, вошедших в DataRow
		/// </summary>
		internal List<Segment> Segments = new List<Segment>();

		/// <summary>
		/// Список заюзанных сегментами строки колонок грида
		/// </summary>
		internal List<int> GetUsedCols()
		{
			var UsedColumns = new List<int>();
			foreach (var seg in Segments)
			{
				for (int i = 0; i < Constants.minSegWidthInColumns; i++)
					UsedColumns.Add(seg.StartColumn + i);
				for (int i = 0; i < Constants.BoxLenInColumns; i++)
					UsedColumns.Add(seg.FinishColumn + i);
			}
			return UsedColumns;
		}

		internal bool IsEmpty
		{
			get { return Segments.Count == 0; }
		}

		/// <summary>
		/// Абсолютная левая координата по горизонтали.
		/// Расстояние от начала графика до начала отображения строки.
		/// </summary>
		internal int BeginX { get; private set; }

		/// <summary>
		/// Абсолютная правая координата по горизонтали.
		/// Расстояние от начала графика до конца отображения строки.
		/// </summary>
		internal int EndX { get; private set; }

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="_NS">Начальное события для строки</param>
		/// <param name="_KS">Конечное события для строки</param>
		/// <param name="_ID">ID работы первого сегмента строки</param>
		internal DataRow(int _NS, int _KS)
		{
			NS = _NS;
			KS = _KS;
		}

		/// <summary>
		/// Добавление работы в пакет
		/// </summary>
		/// <param name="work">Работа</param>
		/// <param name="IsPrj">Признак - проект или этап</param>
		internal void AddWork(WorkSGR work, bool IsPrj)
		{
			var seg = new Segment(work);
			var maxNumber = 0;
			if (Segments.Any())
				maxNumber = Segments.Max(e => e.Order);
			seg.Order = maxNumber + 1;
			if (IsPrj && this.Order == 1 && seg.Order == 1)
				foreach (var desc in work.Description.OrderBy(e => e.Order))
				{
					seg.AddDetail(desc.FOT.ToString());
				}
			else
				foreach (var desc in work.Description.OrderBy(e => e.Order))
				{
					seg.AddDetail(string.Format("{0} - {1} - {2}", desc.VisualNumber, desc.NBR, desc.FOT));
				}
			var prevSeg = Segments.FirstOrDefault(e => e.KS == seg.NS);
			if (prevSeg != null && seg.StartDate != prevSeg.FinishDate)
			{
				seg.StartDate = prevSeg.FinishDate;
			}
			Segments.Add(seg);
			KS = work.KS;
		}

		/// <summary>
		/// Функция ищет конечный сегмент в списке по номеру события NS.
		/// Если найден - то проверяется непротиворечивость дат одного и того же события.
		/// </summary>
		/// <param name="NS"></param>
		/// <param name="startDate"></param>
		/// <returns></returns>
		internal bool IsSameDate(int NS, DateTime startDate)
		{
			var result = false;
			var seg = Segments.FirstOrDefault(e => e.KS == NS);
			if (seg != null)
				result = startDate >= seg.StartDate;
			return result;
		}

		/// <summary>
		/// Вычисление начальной и конечной колонки строки.
		/// </summary>
		/// <param name="startPack">Дата начала графика</param>
		/// <param name="finishPack">Дата окончания графика</param>
		internal void CalcColumns(DateTime startPack, DateTime finishPack)
		{
			foreach (var seg in Segments)
				seg.CalcColumns(startPack, finishPack);
			SetRowColumns();
		}

		internal void SetRowColumns()
		{
			StartColumn = Segments.Min(e => e.StartColumn);
			FinishColumn = Segments.Max(e => e.FinishColumn);
		}

		/// <summary>
		/// Подготовка данных для отображения строки:
		/// Вычисление высоты и длины.
		/// </summary>
		internal void PrepareCoords()
		{
			foreach (var seg in Segments)
				seg.PrepareCoords();
			
			BeginX = Segments.Min(e => e.BeginX);
			EndX = Segments.Max(e => e.EndX);

			var maxUpHeight = Segments.Max(e => e.UpHeight);
			var maxLowHeight = Segments.Max(e => e.LowHeight);
			Height = maxUpHeight + maxLowHeight;
			Mediana = maxUpHeight;
		}

		/// <summary>
		/// Корректировка координат по горизонтали.
		/// </summary>
		internal void CorrectCoords()
		{
			foreach (var seg in Segments)
				seg.CorrectCoords();

			BeginX = Segments.Min(e => e.BeginX);
			EndX = Segments.Max(e => e.EndX);

			var maxUpHeight = Segments.Max(e => e.UpHeight);
			var maxLowHeight = Segments.Max(e => e.LowHeight);
			Height = maxUpHeight + maxLowHeight;
		}

		/// <summary>
		/// Корректировка координат точек сопряжения сегментов
		/// </summary>
		/// <param name="prevRow">Строка графика, относительно которой нужно скорректировать координаты</param>
		/// <param name="vHeight">Высота графика до текущей строки</param>
		internal void UpdateVertPoints(DataRow prevRow, int vHeight)
		{
			foreach (var seg in Segments)
				seg.UpdateVertPoints(prevRow, vHeight);
		}

		/// <summary>
		/// Установка стиля отрисовки линий сопряжения сегментов
		/// </summary>
		internal void UpdateVertPointsStyle()
		{
			foreach (var seg in Segments)
				seg.UpdateVertPointsStyle();
		}

		/// <summary>
		/// Отрисовка строки
		/// </summary>
		/// <param name="gfx"></param>
		/// <param name="startY">Координата Y начала отображения</param>
		internal void Draw(Graphics gfx, int startY)
		{
			foreach (var seg in Segments.OrderBy(e => e.Order))
				seg.Draw(gfx, startY + Mediana);
		}

	}

}

