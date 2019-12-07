using System;
using System.Drawing;
using System.Windows.Forms;

namespace VanGogDll
{

	/// <summary>
	/// VanGogDLL.Program.main( DataPack dPack );
	/// </summary>
	public partial class MainForm : Form
	{
		double curZoom;
		Bitmap bmp;

		public MainForm(DataPack dPack)
		{
			InitializeComponent();
			panel1.AutoScroll = true;
			pictureBox1.SizeMode = PictureBoxSizeMode.AutoSize;
			// reduce flickering
			this.DoubleBuffered = true;

			this.MouseWheel += new MouseEventHandler(FMouseWheel);

			curZoom = 100.0;

			var creatorBMP = new CreatorBMP(dPack);
			var PICTURE_DPI = 96;
			bmp = creatorBMP.PrepareBMP(PICTURE_DPI);

			RenderDoc();
		}

		private void FMouseWheel(object sender, MouseEventArgs e)
		{
			if (e.Delta == 0)
				return;

			if (e.Delta > 0)
				tbtnZoomIn_Click(null, null);
			else
				tbynZoomOut_Click(null, null);
		}

		private void RenderDoc()
		{
			if (pictureBox1.Image != null)
				pictureBox1.Image.Dispose();
			if (curZoom == 100)
				pictureBox1.Image = (Image) bmp.Clone();
			else
				pictureBox1.Image = ScaleImage2Scale(bmp, curZoom);
		}

		private void tbtnOpen_Click_1(object sender, EventArgs e)
		{
			var path = @"c:\temp\shot1.bmp";
			bmp = new Bitmap(path);
			RenderDoc();
		}

		private void tbtnClose_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		private void tbtnZoomIn_Click(object sender, EventArgs e)
		{
			curZoom = GetNewZoom(curZoom, true);
			RenderDoc();
		}

		private void tbynZoomOut_Click(object sender, EventArgs e)
		{
			curZoom = GetNewZoom(curZoom, false);
			RenderDoc();
		}

		private void tbtnZoomActualSize_Click(object sender, EventArgs e)
		{
			curZoom = 100;
			RenderDoc();
		}

		private void tbtnZoomFitVisible_Click(object sender, EventArgs e)
		{
			double scaleW = 95.0 * panel1.Width / bmp.Width;
			double scaleH = 95.0 * panel1.Height / bmp.Height;
			curZoom = Math.Min(scaleW, scaleH);
			RenderDoc();
		}

		private void tbtnZoomFitWidth_Click(object sender, EventArgs e)
		{
			// Использую ширину панели, потому что не знаю, как определить ширину видимой части picturebox.
			// Надо взять 95 % от коэффициента, потому что ширина picturebox меньше ширины panel1
			curZoom = 95.0 * panel1.Width / bmp.Width;
			RenderDoc();
		}

		private void tbtnZoomFitHeight_Click(object sender, EventArgs e)
		{

		}

		static int GetNewZoom(double currentZoom, bool larger)
		{
			int[] values = new int[]
			{
				10, 20, 30, 40, 50, 60, 70, 80, 90, 100, 120, 140, 160, 180, 200
				//,	250, 300, 350, 400, 450, 500, 600, 700, 800
			};
			var ZoomMin = values[0];
			var ZoomMax = values[14];

			if (currentZoom <= ZoomMin && !larger)
				return ZoomMin;
			if (currentZoom >= ZoomMax && larger)
				return ZoomMax;

			if (larger)
			{
				for (int i = 0; i < values.Length; i++)
				{
					if (currentZoom < values[i])
						return values[i];
				}
			}
			else
			{
				for (int i = values.Length - 1; i >= 0; i--)
				{
					if (currentZoom > values[i])
						return values[i];
				}
			}
			return 100;
		}

		/// <summary>
		/// Масштабирование изображения.
		/// Ширина и высота изображения приводятся к заданному масштабу.
		/// </summary>
		/// <param name="source">Исходное изображение</param>
		/// <param name="scale">Новый масштаб</param>
		private static Image ScaleImage2Scale(Image source, double scale)
		{
			if (source == null)
				return null;
			if (scale == 100.0)
				return source;

			int newwidth = (int)Math.Round(source.Width * scale / 100.0);
			int newheight = (int)Math.Round(source.Height * scale / 100.0);

			Image dest = new Bitmap(newwidth, newheight);
			using (Graphics gr = Graphics.FromImage(dest))
			{
				gr.Clear(Color.White); // Очищаем экран
				gr.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
				gr.DrawImage(source, 0, 0, newwidth, newheight);
				return dest;
			}
		}

		/// <summary>
		/// Масштабирование изображения.
		/// Ширина изображения становится равной заданной ширине,
		/// высота приводится к заданной высоте пропорционально.
		/// </summary>
		public static Image ScaleImageProportional(Image source, int width)
		{
			if (source == null)
				return null;

			var koef = width * 1.0 / source.Width;
			int dstwidth = width;
			int dstheight = (int)Math.Round(source.Height * koef);

			Bitmap bm = new Bitmap(source, dstwidth, dstheight);
			Graphics grap = Graphics.FromImage(bm);
			grap.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
			return bm;

			/*			Image dest = new Bitmap(dstwidth, dstheight);
						using (Graphics gr = Graphics.FromImage(dest))
						{
							gr.Clear(Color.White); // Очищаем экран
							gr.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
							gr.DrawImage(source, 0, 0, dstwidth, dstheight);
							return dest;
						} */
		}

		private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
		{
			var mem = new Memento(new Point(this.Left, this.Top), new Size(this.Width, this.Height));
			mem.Save();
		}

		private void LoadSettings()
		{
			var mem = Memento.Restore();
			if (mem == null) return;
			if (mem.Location != null)
				this.Location = mem.Location;
			if (mem.Size != null)
				this.Size = mem.Size;
		}

		private void MainForm_Load(object sender, EventArgs e)
		{
			this.LoadSettings();
		}

	}
}
