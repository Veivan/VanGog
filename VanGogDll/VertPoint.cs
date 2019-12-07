using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VanGogDll
{
	class VertPoint
	{
		/// <summary>
		/// Order строки для соединения
		/// </summary>
		internal int row2connect { get; private set; }
		/// <summary>
		/// ID сегмента для соединения
		/// </summary>
		internal int segID { get; private set; }
		/// <summary>
		/// Позиция точки
		/// </summary>
		internal Constants.numSide side { get; private set; }

		/// <summary>
		/// координата X в этой строке
		/// </summary>
		internal int X { get; private set; }
		/// <summary>
		/// координата У в родительской строке
		/// </summary>
		internal int Y { get; private set; }
		internal bool IsDotted { get; private set; }

		internal VertPoint(int _row2connect, int _segID, Constants.numSide _side)
		{
			row2connect = _row2connect;
			segID = _segID;
			side = _side;
		}

		/// <summary>
		/// Установка координат
		/// </summary>
		/// <param name="_x">координата X в этой строке</param>
		/// <param name="_y">координата У в родительской строке</param>
		internal void SetCoords(int _x, int _y)
		{
			X = _x;
			Y = _y;
		}

		/// <summary>
		/// Установка стиля точки сопряжения
		/// </summary>
		/// <param name="IsDotted">стиль</param>
		internal void SetSyle(bool _IsDotted)
		{
			IsDotted = _IsDotted;
		}

	}
}