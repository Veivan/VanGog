using System;
using System.Drawing;

namespace VanGogDll
{
    public class Program
    {
		/// <summary>
		/// Точка входа в длл из PLM.
		/// </summary>
		/// <param name="stage">Данные СГР</param>
		static public void main(DataPack stage)
		{
			var fMainForm = new MainForm(stage);
			fMainForm.Show();
		}

		/// <summary>
		/// Формирование BMP из СГР.
		/// </summary>
		/// <param name="stage">Данные СГР</param>
		/// <param name="ppi">Разрешение BMP</param>
		static public Bitmap GetBMP(DataPack stage, Int32 ppi)
		{
			//var formatter = new Formatter(Constants.Serial.sXML);
			//formatter.Save2file(stage);
			var creatorBMP = new CreatorBMP(stage);
			var bmp = creatorBMP.PrepareBMP(ppi);
			return bmp;
		}

		/// <summary>
		/// Формирование массива BMP из СГР.
		/// </summary>
		/// <param name="stage">Данные СГР</param>
		/// <param name="ppi">Разрешение BMP</param>
		static public Bitmap[] GetSlicedBMP(DataPack stage, Int32 ppi)
		{
			var creatorBMP = new CreatorBMP(stage);
			var arrBmp = creatorBMP.PrepareSlicedBMP(ppi);
			return arrBmp;
		}

		/// <summary>
		/// Формирование BMP из сохранённого файла.
		/// </summary>
		/// <param name="ppi">Разрешение BMP</param>
		static public Bitmap GetBMPfromSaved(Int32 ppi)
		{
			var formatter = new Formatter(Constants.Serial.sXML);
			var stage = formatter.RestoreFromfile();
			var creatorBMP = new CreatorBMP(stage);
			var bmp = creatorBMP.PrepareBMP(ppi);
			return bmp;
		}

	}
}
