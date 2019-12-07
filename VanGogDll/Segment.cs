using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using System.Linq;

namespace VanGogDll
{
	/// <summary>
	/// Класс описывает один сегмент строки
	/// </summary>
	class Segment
	{
		int boxL = Constants.boxL;

		/// <summary>
		/// Абсолютная левая координата по горизонтали.
		/// Расстояние от начала графика до начала графического отображения сегмента.
		/// Начало отображения - левый край квадрата начального события,
		/// либо начало линии медианы (если не отображается левый квадрат)
		/// </summary>
		internal Int32 BeginX { get; private set; }

		/// <summary>
		/// Абсолютная правая координата по горизонтали.
		/// Расстояние от начала графика до конца отображения сегмента.
		/// Конец отображения - правый край квадрата конечного события,
		/// либо конец линии медианы (если не отображается правый квадрат)
		/// </summary>
		internal Int32 EndX { get; private set; }

		/// <summary>
		/// Ширина сегмента.
		/// Расстояние от начала до конца отображения сегмента.
		/// </summary>
		Int32 Width
		{ get { return EndX - BeginX; } }

		/// <summary>
		/// Левая координата Х начала линии медианы сегмента.
		/// </summary>
		Int32 MedianaStart
		{ get { return DrawStartBox ? BeginX + boxL : BeginX; } }

		/// <summary>
		/// Длина линии медианы сегмента.
		/// </summary>
		Int32 MedianaLength
		{ get
			{
				return Width - (DrawStartBox ? boxL : 0) - (DrawFinishBox ? boxL : 0);
			}
		}

		/// <summary>
		/// Левая координата Х вертикальной линии для потомков.
		/// Это середина квадрата начального события,
		/// либо начало линии медианы (если не отображается левый квадрат)
		/// </summary>
		internal Int32 VerticalLeftX
		{ get {	return DrawStartBox ? BeginX + boxL / 2 : MedianaStart;	} }

		/// <summary>
		/// Левая координата Y вертикальной линии для потомков.
		/// Это середина квадрата начального события,
		/// либо начало линии медианы (если не отображается левый квадрат)
		/// </summary>
		internal Int32 VerticalLeftY { get; private set; }

		/// <summary>
		/// Правая координата Х вертикальной линии для потомков.
		/// Это середина квадрата конечного события,
		/// либо конец линии медианы (если не отображается правый квадрат)
		/// </summary>
		internal Int32 VerticalRightX { get { return DrawFinishBox ? EndX - boxL / 2 : MedianaStart + MedianaLength; } }

		/// <summary>
		/// Правая координата Y вертикальной линии для потомков.
		/// Это середина квадрата конечного события,
		/// либо конец линии медианы (если не отображается правый квадрат)
		/// </summary>
		internal Int32 VerticalRightY { get; private set; }

		/// <summary>
		/// Левая координата Х начала отображения Наименования и Детализации.
		/// </summary>
		private Int32 NameStart
		{ get { return BeginX + (DrawStartBox ? boxL + boxL / 4 : boxL / 4); } }

		internal Int32 Order;
		string Name;
		internal Int32 NS { get; private set; }
		internal Int32 KS { get; private set; }
		internal DateTime StartDate;
		internal DateTime FinishDate;
		internal Int32 ID { get; private set; } // ID работы, из которой был создан сегмент

		/// <summary>
		/// Признак - рисовать начальное событие или нет.
		/// Оно рисуется, если не совпадает с событием родителя.
		/// </summary>
		private bool DrawStartBox = true;

		/// <summary>
		/// Признак - сдвигать начальное событие или нет.
		/// Оно сдвигается вправо, если это первый сегмент и его начальное событие не совпадает с событием родителя.
		/// </summary>
		private bool ShiftStartBox = false;

		/// <summary>
		/// Признак - рисовать конечное событие или нет.
		/// Она рисуется, если не совпадает с событием родителя.
		/// </summary>
		private bool DrawFinishBox = true;

		/// <summary>
		/// Признак - сдвигать конечное событие или нет.
		/// Оно сдвигается влево, если это последний сегмент и его конечное событие не совпадает с событием родителя.
		/// </summary>
		private bool ShiftFinishBox = false;

		/// <summary>
		/// Списох хранит данные о Номере этапа, Исполнителе и Трудоёмкости.
		/// Первая запись - о самом этапе, остальные - о вложенных работах 1-го уровня.
		/// </summary>
		private List<string> Details = new List<string>();

		internal void AddDetail(string detail)
		{
			Details.Add(detail);
		}

		internal int UpHeight { get; private set; }

		internal int LowHeight { get; private set; }

		internal Size NameSize { get; private set; }

		/// <summary>
		/// Высота надписи детализации
		/// </summary>
		internal int DetailHeight { get; private set; }

		/// <summary>
		/// Порядковый номер клеточки месяца начала сегмента среди NumCols
		/// </summary>
		internal int StartColumn { get; private set; }

		/// <summary>
		/// Порядковый номер клеточки месяца конца сегмента среди NumCols
		/// </summary>
		internal int FinishColumn { get; private set; }

		List<VertPoint> segVertList = new List<VertPoint>();

		internal bool HaveVertPoints { get { return segVertList.Count > 0; } }

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="work"></param>
		internal Segment(WorkSGR work)
		{
			Name = work.Name;
			NS = work.NS;
			KS = work.KS;
			StartDate = work.StartDate;
			FinishDate = work.FinishDate;
			ID = work.ID;
		}

		/// <summary>
		/// Вычисление начальной и конечной колонки сегмента.
		/// </summary>
		/// <param name="startPack">Дата начала графика</param>
		/// <param name="FinishPack">Дата окончания графика</param>
		internal void CalcColumns(DateTime startPack, DateTime finishPack)
		{		
			StartColumn = (StartDate - startPack).Days;
			FinishColumn = (FinishDate - startPack).Days;
			if (StartColumn == FinishColumn)
				FinishColumn++;
		}

		/// <summary>
		/// Установка значения начальной или конечной колонки сегмента.
		/// </summary>
		/// <param name="IsStart">true - начальной, false - конечной</param>
		/// <param name="value">новое значение</param>
		internal void SetColumn(bool IsStart, int value)
		{
			if (IsStart)
				StartColumn = value;
			else
				FinishColumn = value;
		}

		/// <summary>
		/// Функция определяет необходимость рисования и сдвига начального и конечного квадратика сегмента.
		/// </summary>
		internal void DesideAboutBounds(Segment parSeg, Constants.numSide side)
		{
			if (side == Constants.numSide.sLeft || side == Constants.numSide.sBoth)
			{
				var leftPt = segVertList.FirstOrDefault(e => e.side == Constants.numSide.sLeft);
				if (leftPt != null)
				{
					DrawStartBox = this.NS != parSeg.NS;
					ShiftStartBox = DrawStartBox && this.StartColumn == parSeg.StartColumn;
				}
			}
			if (side == Constants.numSide.sRight || side == Constants.numSide.sBoth)
			{
				var rightPt = segVertList.FirstOrDefault(e => e.side == Constants.numSide.sRight);
				if (rightPt != null)
				{
					DrawFinishBox = this.KS != parSeg.KS;
					ShiftFinishBox = DrawFinishBox && this.FinishColumn == parSeg.FinishColumn;
				}
			}
		}

		/// <summary>
		/// Добавление точки сопряжения с родительским сегментом
		/// </summary>
		/// <param name="row2connect">строка, в которой находится родительский сегмент</param>
		/// <param name="segID">ID родительскоого сегмента</param>
		/// <param name="side">положение точки</param>
		internal void AddVertPoint(int row2connect, int segID, Constants.numSide side)
		{
			segVertList.Add(new VertPoint(row2connect, segID, side));
		}

		internal VertPoint GetPoint(Constants.numSide side)
		{
			return segVertList.FirstOrDefault(e => e.side == side);
		}

		/// <summary>
		/// Подготовка данных для отображения сегмента:
		/// Вычисление высоты и длины.
		/// </summary>
		internal void PrepareCoords()
		{
			BeginX = Constants.marginX + StartColumn * Constants.ColWidth + (ShiftStartBox ? boxL : 0);
			EndX = Constants.marginX + FinishColumn * Constants.ColWidth + boxL - (ShiftFinishBox ? boxL : 0);

			// Расчёт высоты детализации
			UpHeight = boxL / 2 + boxL / 4 + Constants.DataBoxH;
			if (Details.Count > 0)
				using (Font font = new Font(Constants.font, Constants.font4details))
				{
					Size textSize = TextRenderer.MeasureText(Details[0], font);
					DetailHeight = Details.Count * textSize.Height;
					UpHeight = Math.Max(UpHeight, DetailHeight);
				}

			// Расчёт высоты названия
			NameSize = CalcNameSize();
			LowHeight = boxL / 4 + NameSize.Height;

			VerticalLeftY = (DrawStartBox ? UpHeight + boxL / 2 : UpHeight);
			VerticalRightY = (DrawFinishBox ? UpHeight + boxL / 2 : UpHeight);
		}

		/// <summary>
		/// Расчёт высоты названия
		/// </summary>
		/// <returns>Size of Name</returns>
		private Size CalcNameSize()
		{
			Size tNameSize;
			using (Font font = new Font(Constants.font, Constants.font4name))
			{
				var text = "А";
				var textSize = TextRenderer.MeasureText(text, font);
				// Отнимаем 10px, чтобы исключить наезд надписи на квадратик
				var neededW = Math.Min(Width - boxL * 2 - 10, Constants.MaxNameLength);
				tNameSize = TextRenderer.MeasureText(Name, font, new Size(neededW, textSize.Height), TextFormatFlags.WordBreak);
			}
			return tNameSize;
		}

		/// <summary>
		/// Приведение координат сегмента к точкам соединения с родителем.
		/// Пересчёт высоты сегмента с новыми размерами.
		/// </summary>
		internal void CorrectCoords()
		{
			var leftPt = segVertList.FirstOrDefault(e => e.side == Constants.numSide.sLeft);
			if (leftPt != null && BeginX < leftPt.X)
				BeginX = leftPt.X;
			var rightPt = segVertList.FirstOrDefault(e => e.side == Constants.numSide.sRight);
			if (rightPt != null && EndX > rightPt.X)
				EndX = rightPt.X;

			// Расчёт высоты названия
			NameSize = CalcNameSize();
			LowHeight = boxL / 4 + NameSize.Height;
		}

		/// <summary>
		/// Корректировка координат точек сопряжения
		/// </summary>
		/// <param name="prevRow">Строка графика, относительно которой нужно скорректировать координаты</param>
		/// <param name="vHeight">Высота графика до текущей строки</param>
		internal void UpdateVertPoints(DataRow prevRow, int VHeight)
		{
			var leftPt = segVertList.FirstOrDefault(e => e.side == Constants.numSide.sLeft && e.row2connect == prevRow.Order);
			if (leftPt != null)
			{
				var parSeg = prevRow.Segments.First(e => e.ID == leftPt.segID);
				leftPt.SetCoords(parSeg.VerticalLeftX, VHeight + parSeg.VerticalLeftY);
			}
			var rightPt = segVertList.FirstOrDefault(e => e.side == Constants.numSide.sRight && e.row2connect == prevRow.Order);
			if (rightPt != null)
			{
				var parSeg = prevRow.Segments.First(e => e.ID == rightPt.segID);
				rightPt.SetCoords(parSeg.VerticalRightX, VHeight + parSeg.VerticalRightY);
			}
		}

		/// <summary>
		/// Установка стиля отрисовки линий сопряжения
		/// </summary>
		internal void UpdateVertPointsStyle()
		{
			var leftPt = segVertList.FirstOrDefault(e => e.side == Constants.numSide.sLeft);
			if (leftPt != null)
			{
				var IsDotted = this.BeginX != leftPt.X;
				leftPt.SetSyle(IsDotted);
			}
			var rightPt = segVertList.FirstOrDefault(e => e.side == Constants.numSide.sRight);
			if (rightPt != null)
			{
				var IsDotted = this.EndX != rightPt.X;
				rightPt.SetSyle(IsDotted);
			}
		}

		/// <summary>
		/// Отрисовка строки
		/// </summary>
		/// <param name="gfx"></param>
		/// <param name="mediana">Координата Y средней линии строки</param>
		internal void Draw(Graphics gfx, int mediana)
		{
			var y = mediana;
			if (DrawStartBox)
			{
				DrawBox(gfx, BeginX, y, NS);
				if (Order == 1)
				{
					DrawEventDate(gfx, BeginX, y, StartDate);
				}
			}
			if (DrawFinishBox)
			{
				DrawBox(gfx, EndX - boxL, y, KS);
				DrawEventDate(gfx, EndX - boxL, y, FinishDate);
			}

			// Draw median line
			using (Pen p = new Pen(Color.Black, 2))
			{
				gfx.DrawLine(p, MedianaStart, y, MedianaStart + MedianaLength, y);
			}

			DrawWorkDetails(gfx, y);
			DrawWorkName(gfx, y);

			// Рисование вертикальной линии до родителя
			using (Pen p = new Pen(Color.Black, 2))
			{
				foreach (var vp in segVertList)
				{
					p.DashStyle = vp.IsDotted ? DashStyle.Dash : DashStyle.Solid;
					gfx.DrawLine(p, vp.X, vp.Y, vp.X, y);
				}
			}

			// Рисование горизонтальной линии до стыковки с вертикалью
			using (Pen p = new Pen(Color.Black, 2))
			{
				var leftPt = segVertList.FirstOrDefault(e => e.side == Constants.numSide.sLeft);
				if (leftPt != null && BeginX > leftPt.X)
				{
					p.DashStyle = leftPt.IsDotted ? DashStyle.Dash : DashStyle.Solid;
					gfx.DrawLine(p, leftPt.X, mediana, BeginX, mediana);
				}
				var rightPt = segVertList.FirstOrDefault(e => e.side == Constants.numSide.sRight);
				if (rightPt != null && EndX < rightPt.X)
				{
					p.DashStyle = rightPt.IsDotted ? DashStyle.Dash : DashStyle.Solid;
					gfx.DrawLine(p, EndX, mediana, rightPt.X, mediana);
				}
			} 

		}

		/// <summary>
		/// Отрисовка квадратика события с номером внутри
		/// </summary>
		/// <param name="gfx"></param>
		/// <param name="startX">Координата Х начала отображения</param>
		/// <param name="mediana">Координата Y средней линии строки</param>
		/// <param name="eventNum">Номер события</param>
		private void DrawBox(Graphics gfx, int startX, int mediana, int eventNum)
		{
			var x = startX;
			var y = mediana;
			var dif = boxL / 2;
			var points = new Point[] {new Point(x, y - dif), new Point(x, y + dif),
				new Point(x + boxL, y + dif), new Point(x + boxL, y - dif), new Point(x, y - dif)};
			using (Pen p = new Pen(Color.Black, 2))
			{
				gfx.DrawLines(p, points);
			}

			using (Font font = new Font(Constants.font, Constants.font4event))
			//using (SolidBrush brush = new SolidBrush(Color.Black))
			{
				/*StringFormat sf = new StringFormat();
				sf.Alignment = StringAlignment.Center;
				sf.LineAlignment = StringAlignment.Center;

				var colBounds = new Rectangle(x, y - dif, boxL, boxL);
				gfx.DrawString(eventNum.ToString(), font, brush, colBounds, sf); */
				TextRenderer.DrawText(gfx, eventNum.ToString(), font, new Rectangle(x, y - dif, boxL, boxL),
					Color.Black, Color.White, TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);
			}
		}

		/// <summary>
		/// Отрисовка даты события
		/// </summary>
		/// <param name="gfx"></param>
		/// <param name="startX">Координата Х начала отображения</param>
		/// <param name="mediana">Координата Y средней линии строки</param>
		/// <param name="eventDate">Дата события</param>
		private void DrawEventDate(Graphics gfx, int startX, int mediana, DateTime eventDate)
		{
			using (Font font = new Font(Constants.font, Constants.font4date))
			{
				Size textSize = TextRenderer.MeasureText(eventDate.ToString("dd.MM.yy"), font);
				var x = startX + boxL / 2 - textSize.Width / 2;
				var y = mediana - boxL / 2 - textSize.Height - Constants.shiftY;
				TextRenderer.DrawText(gfx, eventDate.ToString("dd.MM.yy"), font, new Rectangle(x, y, textSize.Width, textSize.Height),
					Color.Black, Color.White, TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);
			}
		}

		/// <summary>
		/// Отрисовка наименования 
		/// </summary>
		/// <param name="gfx"></param>
		/// <param name="mediana">Координата Y средней линии строки</param>
		private void DrawWorkName(Graphics gfx, int mediana)
		{
			using (Font font = new Font(Constants.font, Constants.font4name, GraphicsUnit.Point))
			{
				TextRenderer.DrawText(gfx, Name, font, 
					new Rectangle(NameStart, mediana + Constants.shiftY, NameSize.Width, NameSize.Height),
					Color.Black, Color.White, TextFormatFlags.WordBreak);
			}
		/*	using (SolidBrush brush = new SolidBrush(Color.Black))
			{
				var colBounds = new Rectangle(new Point(NameStart, mediana), NameSize);
				gfx.DrawString(Name, font, brush, colBounds);
			} */
		}

		/// <summary>
		/// Отрисовка детализации 
		/// </summary>
		/// <param name="gfx"></param>
		/// <param name="mediana">Координата Y средней линии строки</param>
		private void DrawWorkDetails(Graphics gfx, int mediana)
		{
			if (Details.Count == 0) return;
			var y = mediana - DetailHeight - Constants.shiftY;
			using (Font font = new Font(Constants.font, Constants.font4details))
//			using (SolidBrush brush = new SolidBrush(Color.Black))
			{
				var text = string.Join(Environment.NewLine, Details);
				var colBounds = new Rectangle(new Point(NameStart, y), new Size(Width - boxL, UpHeight));
//				gfx.DrawString(text, font, brush, colBounds);

				TextRenderer.DrawText(gfx, text, font, new Rectangle(NameStart, y, Width - boxL, UpHeight),
					Color.Black, Color.White, TextFormatFlags.WordBreak);
			}
		}

	}
}
