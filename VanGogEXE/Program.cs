using System;
using System.Windows.Forms;

namespace VanGogEXE
{
	static class Program
	{
		/// <summary>
		/// Главная точка входа для приложения.
		/// </summary>
		[STAThread]
		static void Main()
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);

			var stage = new VanGogDll.DataPack(false, true, "Очень очень очень длинное название Stage1");
			stage.StartDate = new DateTime(2019, 3, 1);
			stage.FinishDate = new DateTime(2020, 12, 25);

			stage.InitValues(8); 

			Application.Run(new VanGogDll.MainForm(stage));
		}
	}
}
