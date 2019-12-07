using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace VanGogDll
{

	/// <summary>
	/// Класс предназначен для реализации хранения положения формы.
	/// Паттерн Memento.
	/// </summary>
	class Memento
	{
		/// <summary>
		///  Left Top point of Form
		/// </summary>
		public Point Location
		{
			get; private set;
		}
		/// <summary>
		/// Size of Form
		/// </summary>
		public Size Size
		{
			get; private set;
		}

		public Memento(Point Location, Size Size)
		{
			this.Location = Location;
			this.Size = Size;
		}

		public void Save()
		{
/*			if (Location != null)
				Properties.Settings.Default.Location = Location;
			if (Size != null)
				Properties.Settings.Default.Size = Size;
			Properties.Settings.Default.Save(); */
		}
		public static Memento Restore()
		{
			//	return new Memento(Properties.Settings.Default.Location, Properties.Settings.Default.Size);

			return null;
		}
	}
}