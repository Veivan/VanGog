using System;
using System.Drawing;
using System.Windows.Forms;

namespace VanGogDll
{
	/// TODO возможно больше не нужен
	/// <summary>
	/// Класс описывает заголовок графика
	/// </summary>
	internal class GraphShablon
	{
		/// <summary>
		/// Ширина надписи Утверждаю
		/// </summary>
		const Int32 StampWidth = 250;

		const String headPr = "по этапу ";
		const String headTh = "Тема: ";

		private bool IsSGR;

		/// <summary>
		/// Наименование Проекта (Этапа)
		/// </summary>
		private string Name;

		/// <summary>
		/// Наименование темы
		/// </summary>
		private string Theme;

		/// <summary>
		/// Наименование заказа
		/// </summary>
		private string Zakaz;

		/// <summary>
		/// Высота шаблона
		/// </summary>
		public int Height { get; private set; }

		/// <summary>
		/// Ширина шаблона
		/// </summary>
		public int Width { get { return Constants.ShablonWidth; }  }

		/// <summary>
		/// Доступный размер для надписи
		/// </summary>
		private Int32 availSpace { get { return Width - StampWidth - 10; } }

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="_isSGR"> true - СГР, false - СГПЭР</param>
		/// <param name="_name">Наименование Проекта (Этапа)</param>
		/// <param name="_Theme">Наименование Темы</param>
		/// <param name="_Zakaz">Номер заказа</param>
		public GraphShablon(bool _isSGR, string _name, string _Theme, string _Zakaz)
		{
			IsSGR = _isSGR;
			Name = _name;
			Theme = _Theme;
			Zakaz = _Zakaz;
		}

		internal void SetHeight()
		{
		/*	using (Font font = new Font(Constants.font, Constants.font4shablon))
			{
				var text = "А";
				var textSize = TextRenderer.MeasureText(text, font);
				Height = textSize.Height * 2;

				textSize = TextRenderer.MeasureText(headPr + Name, font, new Size(availSpace, textSize.Height), TextFormatFlags.WordBreak);
				Height += textSize.Height;

				textSize = TextRenderer.MeasureText(headTh + Theme, font, new Size(availSpace, textSize.Height), TextFormatFlags.WordBreak);
				Height += textSize.Height;
			}

			Height = Math.Max(Height, Constants.captionH * 4); */
		}

		/// <summary>
		/// Отрисовка заголовка графика
		/// </summary>
		/// <param name="gfx"></param>
		/// <param name="startX">Координата Х начала отображения</param>
		/// <param name="startY">Координата Y начала отображения</param>
		internal void Draw(Graphics gfx, int startX, int startY)
		{
			string text;
			Size textSize;
			int x = startX + Width - StampWidth;
			int y = startY;

			gfx.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;

			using (Font font = new Font(Constants.font, Constants.font4shablon)) 
			{
				if (IsSGR)
				{
					text = "У Т В Е Р Ж Д А Ю";
					textSize = TextRenderer.MeasureText(text, font);
					TextRenderer.DrawText(gfx, text, font, new Rectangle(new Point(x, y), textSize), Color.Black);

					y += Constants.captionH;
					text = "Генеральный директор";
					textSize = TextRenderer.MeasureText(text, font);
					TextRenderer.DrawText(gfx, text, font, new Rectangle(new Point(x, y), textSize), Color.Black);

					y += Constants.captionH;
					text = "____________В.Н.Трусов";
					textSize = TextRenderer.MeasureText(text, font);
					TextRenderer.DrawText(gfx, text, font, new Rectangle(new Point(x, y), textSize), Color.Black);

				}

				y += Constants.captionH;
				text = "\"____\"_______________20__ г.";
				textSize = TextRenderer.MeasureText(text, font);
				TextRenderer.DrawText(gfx, text, font, new Rectangle(new Point(x, y), textSize), Color.Black);
				var minHeight = textSize.Height;

				x = startX;
				y = startY;
				text = IsSGR ? "Сетевой график руководителя работ" : "Сетевой график подэтапных работ";
				textSize = TextRenderer.MeasureText(text, font);
				TextRenderer.DrawText(gfx, text, font, new Rectangle(new Point(x, y), textSize), Color.Black);
				y += textSize.Height;

				text = headPr + Name;
				textSize = TextRenderer.MeasureText(text, font, new Size(availSpace, minHeight), TextFormatFlags.WordBreak);
				TextRenderer.DrawText(gfx, text, font, new Rectangle(new Point(x, y), textSize), Color.Black, TextFormatFlags.WordBreak);
				y += textSize.Height;

				if (IsSGR)
				{
					text = headTh + Theme;
					textSize = TextRenderer.MeasureText(headTh + Theme, font, new Size(availSpace, minHeight), TextFormatFlags.WordBreak);
					TextRenderer.DrawText(gfx, text, font, new Rectangle(new Point(x, y), textSize), Color.Black, TextFormatFlags.WordBreak);
					y += textSize.Height;
				}

				text = "Заказ: " + Zakaz;
				textSize = TextRenderer.MeasureText(text, font);
				TextRenderer.DrawText(gfx, text, font, new Rectangle(new Point(x, y), textSize), Color.Black);
			}

		}
	}
}
