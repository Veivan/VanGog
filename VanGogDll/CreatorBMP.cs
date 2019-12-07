using System;
using System.Drawing;

namespace VanGogDll
{
	/// <summary>
	/// Класс готовит BMP из набора данных СГР
	/// </summary>
	class CreatorBMP
	{
		/// <summary>
		/// Набор данных СГР
		/// </summary>
		DataPack dPack;

		public CreatorBMP(DataPack _dPack)
		{
			dPack = _dPack;
		}

		/// <summary>
		/// Создание картинки СГР
		/// </summary>
		/// <param name="ppi">Разрешение</param>
		/// <returns>Bitmap</returns>
		internal Bitmap PrepareBMP(Int32 ppi)
		{
			var vPack = new RowsPack(dPack);
			vPack.Prepare(dPack, ppi);
			var bmp = new Bitmap(vPack.Width, vPack.Heght);
			bmp.SetResolution(ppi, ppi);
			using (Graphics gfx = Graphics.FromImage(bmp))
			{
				gfx.Clear(Color.White); // Очищаем экран
				gfx.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
				gfx.PageUnit = GraphicsUnit.Pixel;

				//DrawGrid(gfx);

				//vPack.DrawShablon(gfx);
				vPack.DrawRows(gfx);
			}
			return bmp;
		}

		/// <summary>
		/// Создание массива BMP из СГР : на каждую строку - отдельная BMP.
		/// </summary>
		/// <param name="ppi">Разрешение</param>
		/// <returns>Bitmap</returns>
		internal Bitmap[] PrepareSlicedBMP(Int32 ppi)
		{
			var vPack = new RowsPack(dPack);
			vPack.Prepare(dPack, ppi);
			var bmp = new Bitmap(vPack.Width, vPack.Heght);
			bmp.SetResolution(ppi, ppi);
			using (Graphics gfx = Graphics.FromImage(bmp))
			{
				gfx.Clear(Color.White); // Очищаем экран
				gfx.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
				gfx.PageUnit = GraphicsUnit.Pixel;
				vPack.DrawRows(gfx);
			}

			var arrBmp = new Bitmap[vPack.RowCount];
			var arrYs = vPack.GetRowsHeights();
			var startY = Constants.marginY + Constants.captionH;
			for (Int32 i = 0; i < arrYs.Length; i++)
			{
				arrBmp[i] = bmp.Clone(new Rectangle(0, startY, bmp.Width, arrYs[i]), bmp.PixelFormat);
				startY += arrYs[i];
			}

			return arrBmp;
		}

		/// <summary>
		/// Рисование сетки по количеству листов А4
		/// </summary>
		private void DrawGrid(Graphics gfx, Bitmap bmp)
		{
			Int32 horzCnt, vertCnt, cWidth, cHeight, x, y;
			using (Pen p = new Pen(Color.LightGray))
			{
				p.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
				if (Constants.Orientation == Constants.orient.oLandshaft)
				{
					cWidth = Constants.A4length;
					cHeight = Constants.A4width;
				}
				else
				{
					cWidth = Constants.A4width;
					cHeight = Constants.A4length;
				}
				horzCnt = (Int32)Math.Ceiling((Single)bmp.Width / cWidth);
				vertCnt = (Int32)Math.Ceiling((Single)bmp.Height / cHeight);

				x = 1;
				for (Int32 i = 0; i < horzCnt; i++)
				{
					y = 1;
					for (Int32 j = 1; j <= vertCnt; j++)
					{
						var points = new Point[] {new Point(x, y), new Point(x, y + cHeight),
							new Point(x + cWidth, y + cHeight), new Point(x + cWidth, y), new Point(x, y)};
						gfx.DrawLines(p, points);
						y += cHeight;
					}
					x += cWidth;
				}
			}
		}
	}
}
