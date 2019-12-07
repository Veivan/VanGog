using System;

namespace ConsoleApplication1
{
	class Program
	{
		static void Main(string[] args)
		{
			GetBMPfromSaved();
		}

		static void GetBMP()
		{
			var path = @"c:\temp\shot";
			var stage = new VanGogDll.DataPack(false, true, "Очень очень очень длинное название Stage1");
			stage.StartDate = new DateTime(2019, 3, 1);
			stage.FinishDate = new DateTime(2020, 12, 25);

			stage.InitValues(1);

			/*var bmp = VanGogDll.Program.GetBMP(stage, 96 * 2);
			bmp.Save(@"c:\temp\shot2.bmp");*/

			var bmpArr = VanGogDll.Program.GetSlicedBMP(stage, 96 * 2);
			for (Int32 i = 0; i < bmpArr.Length; i++)
			{
				bmpArr[i].Save(path + i.ToString() + ".bmp");
			}

		}

		static void GetBMPfromSaved()
		{
			var bmp = VanGogDll.Program.GetBMPfromSaved(96);
			bmp.Save(@"c:\temp\shot3.bmp");
		}
	}
}
