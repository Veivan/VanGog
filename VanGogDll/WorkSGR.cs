using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VanGogDll
{
	[Serializable]
	public struct ChildWork
	{
		public int Order;
		public string VisualNumber; // Номер этапа
		public string NBR; // Шифр исполнителя
		public int FOT; // Трудоёмкость
	}

	/// <summary>
	/// Класс описывает одну строку СГР
	/// </summary>
	[Serializable]
	public class WorkSGR
	{
		public string Name;
		public int GlobOrder;
		public int ID;
		public int ParentID;
		public int NS;
		public int KS;
		public DateTime StartDate;
		public DateTime FinishDate;

		/// <summary>
		/// Constructor 4 serialization
		/// </summary>
		public WorkSGR() { }

		public WorkSGR(string _Name, int _GlobOrder, int _ID, int _ParentID, int _NS, int _KS, 
			DateTime _StartDate, DateTime _FinishDate)
		{
			Name = _Name.Trim();
			GlobOrder = _GlobOrder;
			ID = _ID;
			ParentID = _ParentID;
			NS = _NS;
			KS = _KS;
			StartDate = _StartDate;
			FinishDate = _FinishDate;
		}

		/// <summary>
		/// Списох хранит данные о Номере этапа, Исполнителе и Трудоёмкости.
		/// Первая запись - о самом этапе, остальные - о вложенных работах 1-го уровня.
		/// Данные отсортированы в списке по GlobOrder в обратном порядке
		/// </summary>
		public List<ChildWork> Description = new List<ChildWork>();

		public void AddDescription(string VisualNumber, string NBR, int FOT)
		{
			var descr = new ChildWork();
			var maxNumber = 0;
			if (Description.Any())
				maxNumber = Description.Max(e => e.Order);
			descr.Order = maxNumber + 1;
			descr.VisualNumber = VisualNumber;
			descr.NBR = NBR;
			descr.FOT = FOT;
			Description.Add(descr);
		}
	}
}
