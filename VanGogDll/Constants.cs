using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VanGogDll
{
	internal static class Constants
	{
		/// <summary>
		/// Отступ графика по горизонтали
		/// </summary>
		internal const int marginX = 10;

		/// <summary>
		/// Отступ графика по вертикали
		/// </summary>
		internal const int marginY = 10;

		/// <summary>
		/// Высота заголовка грида
		/// </summary>
		internal const int captionH = 0;

		/// <summary>
		/// Размер стороны квадратика события
		/// </summary>
		internal const int boxL = 40;

		/// <summary>
		/// Размер шрифта квадратика события
		/// </summary>
		internal const int font4event = 12;

		/// <summary>
		/// Длина квадратика даты события
		/// </summary>
		internal const int DataBoxW = 50;

		/// <summary>
		/// Высота квадратика даты события
		/// </summary>
		internal const int DataBoxH = 16;

		/// <summary>
		/// Размер шрифта даты события
		/// </summary>
		internal const int font4date = 10;

		/// <summary>
		/// Размер шрифта наименования работы
		/// </summary>
		internal const int font4name = 10;

		/// <summary>
		/// Размер шрифта детализации работы
		/// </summary>
		internal const int font4details = 10;

		/// <summary>
		/// Размер шрифта шаблона
		/// </summary>
		internal const int font4shablon = 12;

		/// <summary>
		/// Размер шрифта заголовка грида
		/// </summary>
		internal const int font4grid = 12;

		/// <summary>
		/// Сдвиг по вертикали
		/// </summary>
		internal const int shiftY = 2;

		/// <summary>
		/// Имя шрифта
		/// </summary>
		//internal const string font = "Microsoft Sans Serif";
		internal const string font = "Times New Roman";

		/// <summary>
		/// Минимальная ширина сегмента в пикселях.
		/// </summary>
		internal const int minSegWidth = 300;

		/// <summary>
		/// Минимальная ширина сегмента в колонках.
		/// </summary>
		internal static int minSegWidthInColumns { get; private set; }

		/// <summary>
		/// Минимальная ширина колонки графика.
		/// </summary>
		private static readonly int minColWidth = 31;

		/// <summary>
		/// Количество дней в среднем месяце для расчёта колонок шкалы
		/// </summary>
		internal const int grad = 31;

		/// <summary>
		/// Ширина колонки графика в пикселях
		/// </summary>
		internal static int ColWidth { get; private set; }

		/// <summary>
		/// Ширина квадратика события в колонках
		/// </summary>
		internal static int BoxLenInColumns { get; private set; }

		/// <summary>
		/// Ширина шаблона в зависимости от ориентации страницы
		/// </summary>
		internal static int ShablonWidth { get { return (Orientation == orient.oPortrait ? A4width : A4length) - marginX * 2; } }

		/// <summary>
		/// Установка ширины колонки графика по длине минимального сегмента, измеренной в колонках
		/// </summary>
		internal static void SetColWidth(int minSegLength)
		{
			if (minSegLength * minColWidth < minSegWidth)
				ColWidth = (int)Math.Round(minSegWidth * 1.0 / minSegLength);
			else
				ColWidth = minColWidth;

			minSegWidthInColumns = minSegWidth / ColWidth;
			BoxLenInColumns = boxL / ColWidth;
		}

		/// <summary>
		/// Установка ширины колонки графика по увеличивающему коэфффициенту
		/// </summary>
		internal static void SetColWidth(Single koefIncrease)
		{
			ColWidth = (int)(ColWidth * koefIncrease);
			minSegWidthInColumns = minSegWidth / ColWidth;
			BoxLenInColumns = boxL / ColWidth;
		}

		internal enum numSide { sLeft, sRight, sBoth };

		/// <summary>
		/// Размеры страницы А4 в мм
		/// </summary>
		internal const int A4width = 210;
		internal const int A4length = 297;

		internal enum orient { oPortrait, oLandshaft };
		internal enum Serial { sBin, sXML };

		internal static orient Orientation { get; private set; }

		internal static Single koefIncrease { get; private set; }

		/// <summary>
		/// Максимальная ширина надписи имени этапа в пикселях
		/// </summary>
		internal static Int32 MaxNameLength { get; private set; }

		/// <summary>
		/// Определение необходимого числа страниц А4 и их формата
		/// </summary>
		/// <param name="maxWidth">Ширина первой строки в пикселях</param>
		/// <param name="dpi">Разрешение</param>
		internal static void CheckOrientation(int maxWidth, Int32 dpi)
		{
			koefIncrease = 1.0F;
			MaxNameLength = (Int32)(120 * dpi / 25.4);
			var A4pxW = (Int32)(A4width * dpi / 25.4);
			var A4pxH = (Int32)(A4length * dpi / 25.4);
			Single testW = A4pxW * koefIncrease - marginX * 2;
			if (maxWidth < testW)
			{
				Orientation = orient.oPortrait;
				var pers = maxWidth * 100.0 / testW;
				if (pers < 98)
					koefIncrease = testW / maxWidth;
				return;
			}
			testW = A4length * koefIncrease - marginX * 2;
			if (maxWidth < testW)
			{
				Orientation = orient.oLandshaft;
				var pers = maxWidth * 100.0 / testW;
				if (pers < 98)
					koefIncrease = testW / maxWidth;
				return;
			}
		}
	}
}
