using System;
using System.Collections.Generic;

namespace VanGogDll
{
	/// <summary>
	/// Класс описывает структуру данных, которая должна отображаться на графике
	/// </summary>
	[Serializable]
	public class DataPack
	{
		bool isPrj, isSGR;
		string name;

		/// <summary>
		/// true - Проект, false - Этап
		/// Определяет тип шаблона и таблицы графика
		/// </summary>
		public bool IsPrj { get { return isPrj;} }

		/// <summary>
		/// true - СГР, false - СГПЭР
		/// Определяет тип шаблона  и таблицы графика
		/// </summary>
		public bool IsSGR { get { return isSGR; } }

		/// <summary>
		/// Наименование Проекта(Этапа)
		/// </summary>
		public string Name { get { return name; } }

		/// <summary>
		/// Constructor 4 serialization
		/// </summary>
		public DataPack() { }

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="_isPrj"> true - Проект, false - Этап</param>
		/// <param name="_isSGR"> true - СГР, false - СГПЭР</param>
		/// <param name="_name"></param>
		public DataPack(bool _isPrj, bool _isSGR, string _name)
		{
			isPrj = _isPrj;
			isSGR = _isSGR;
			name = _name;
		}

		public void SetSGR(bool val)
		{
			isSGR = val;
		}

		/// <summary>
		/// Левая граничная дата графика
		/// </summary>
		public DateTime StartDate { get; set; }
		/// <summary>
		/// Правая граничная дата графика
		/// </summary>
		public DateTime FinishDate { get; set; }

		/// <summary>
		/// Наименование темы
		/// </summary>
		public string Theme { get; set; }

		/// <summary>
		/// Наименование заказа
		/// </summary>
		public string Zakaz { get; set; }

		public List<WorkSGR> ListSGR = new List<WorkSGR>();

		/// <summary>
		/// Функция для заполнения тестовыми данными
		/// </summary>
		/// <param name="shablon">Номер шаблона тестовых данных </param>
		public void InitValues(int shablon)
		{
			switch (shablon)
			{
				case 0: /// Отлажен
					#region Проект test.
					{
						Theme = "test";
						Zakaz = "test";
						StartDate = new DateTime(2019, 3, 1);
						FinishDate = new DateTime(2020, 6, 25);
						var wk = new WorkSGR("test", 0, 0, 0, 1, 6, StartDate, FinishDate);
						ListSGR.Add(wk);
						wk = new WorkSGR("qwt1", 1, 1, 0, 1, 2, new DateTime(2019, 3, 5), new DateTime(2020, 1, 15));
						ListSGR.Add(wk);
						wk = new WorkSGR("q2", 2, 2, 0, 5, 6, new DateTime(2020, 4, 1), new DateTime(2020, 4, 5));
						ListSGR.Add(wk);
						break;
					}
				#endregion
				case 1:
					#region Проект 22153.
					{
						Theme = "test";
						Zakaz = "003";
						StartDate = new DateTime(2019, 6, 25);
						FinishDate = new DateTime(2022, 12, 26);
						var wk = new WorkSGR("test3", 0, 0, 0, 2, 3, StartDate, FinishDate);
						ListSGR.Add(wk);
						wk = new WorkSGR("Э 1", 1, 1, 0, 2, 3, new DateTime(2019, 6, 26), new DateTime(2019, 12, 25));
						ListSGR.Add(wk); 
						wk = new WorkSGR("Э 1.1", 2, 2, 1, 2, 20, new DateTime(2019, 6, 26), new DateTime(2019, 10, 22));
						ListSGR.Add(wk);
						wk = new WorkSGR(" Э 1.2", 3, 3, 1, 20, 3, new DateTime(2019, 10, 22), new DateTime(2019, 12, 25));
						ListSGR.Add(wk);
						wk = new WorkSGR("Э 2", 4, 4, 0, 4, 6, new DateTime(2019, 7, 9), new DateTime(2020, 12, 25));
						ListSGR.Add(wk);
						wk = new WorkSGR("Э 2.1", 5, 5, 4, 4, 9, new DateTime(2019, 7, 9), new DateTime(2020, 3, 9));
						ListSGR.Add(wk);
						wk = new WorkSGR(" Э 3.2", 6, 6, 4, 4, 10, new DateTime(2019, 7, 9), new DateTime(2019, 12, 9));
						ListSGR.Add(wk);
						break;
					}
				#endregion
				case 2:
					#region Две работы. события совпадают. Даты событий совпадают.
					{
						var ev = 2;
						var stDt = new DateTime(2019, 5, 30);
						StartDate = new DateTime(2019, 3, 1);
						FinishDate = new DateTime(2020, 12, 25);
						var wk = new WorkSGR("w1", 1, 1, 0, 1, ev, new DateTime(2019, 3, 1), stDt);
						ListSGR.Add(wk);
						wk = new WorkSGR("w2", 2, 2, 0, ev, 4, stDt, new DateTime(2019, 9, 30));
						ListSGR.Add(wk);
						break;
					}
				#endregion
				case 3: /// Отлажен
					#region Проект СГР-2. Множественная детализация
					{
						StartDate = new DateTime(2016, 1, 11);
						FinishDate = new DateTime(2018, 6, 30);
						Theme = "ОСТРОТА";
						Zakaz = "57820";
						var evStart = 3;
						var evFin = 4;
						var wk = new WorkSGR("Работы с разработчиками СЧ (в том числе: офоромление Контрактов, Дополнений к ТЗ на СЧ ОКР, получение РКМпо СЧ ОКР с Заключением ВП разраб СЧ",
							1, 1, 0, evStart, evFin, StartDate, FinishDate);
						ListSGR.Add(wk);
						wk = new WorkSGR("Работы с разработчиками СЧ (в том числе: офоромление Контрактов, Дополнений к ТЗ на СЧ ОКР, получение РКМпо СЧ ОКР с Заключением ВП разраб СЧ",
							2, 2, 1, 31, 32, new DateTime(2017, 1, 1), new DateTime(2017, 6, 25));
						wk.AddDescription("1", "57820001", 100000);
						wk.AddDescription("3", "57820003", 100000);
						wk.AddDescription("4", "57820004", 100000);
						wk.AddDescription("5", "57820005", 100000);
						ListSGR.Add(wk);
						wk = new WorkSGR("Расчетно-экспериментальные работы. Программы, инструкции и методики испытаний. Отработка узлов и агрегатов. КПА",
							3, 3, 0, evStart, 34, StartDate, new DateTime(2018, 4, 25));
						wk.AddDescription("2", "57820002", 120000);
						ListSGR.Add(wk);
						break;
					}
				#endregion
				case 4: /// Отлажен
					#region Проект qqq.
					{
						Theme = "qqq";
						Zakaz = "0692";
						var ev = 2;
						StartDate = new DateTime(2019, 1, 9);
						FinishDate = new DateTime(2020, 2, 25);
						var plan = new DateTime(2020, 12, 4);
						var n1 = "Проектно-конструкторские работы по облику изд. 720 ( в том числе: уточнение облика; уточнение ККС, АС, проработка конструктивно-технологической схемы планера и построения БЭСО)";
						var wk = new WorkSGR(n1, 0, 0, 0, 3, ev, StartDate, FinishDate);
						ListSGR.Add(wk);
						var n2 = "Участие в проектно-конструкторских работах по облику изд.720";
						wk = new WorkSGR(n2, 1, 1, 0, 3, ev, StartDate, FinishDate);
						ListSGR.Add(wk);
						//wk = new WorkSGR("фф01", 2, 2, 0, 3, ev, StartDate, plan);
						//ListSGR.Add(wk);
						break;
					}
				#endregion
				case 5: /// Отлажен
					#region Проект Посланник.
					{
						Theme = "Посланник 1ц";
						Zakaz = "В690";
						StartDate = new DateTime(2018, 6, 1);
						FinishDate = new DateTime(2021, 12, 24);
						var wk = new WorkSGR("ПОСЛАННИК-1Ц", 0, 0, 0, 1, 4, StartDate, FinishDate);
						ListSGR.Add(wk);
						wk = new WorkSGR("Разработка рабочей конструкторской документации(РКД) изд 720)", 1, 1, 0, 1, 2, StartDate, new DateTime(2021, 1, 9));
						ListSGR.Add(wk);
						wk = new WorkSGR("Этап 2", 2, 2, 0, 2, 3, new DateTime(2021, 1, 9), new DateTime(2021, 3, 13));
						ListSGR.Add(wk);
						wk = new WorkSGR("Этап 3", 3, 3, 0, 3, 7, new DateTime(2021, 3, 13), new DateTime(2021, 8, 13));
						ListSGR.Add(wk);
						wk = new WorkSGR("Этап 4", 4, 4, 0, 7, 4, new DateTime(2021, 8, 13), FinishDate);
						ListSGR.Add(wk);
						break;
					}
				#endregion
				case 6: /// Отлажен
					#region Проект Посланник - Новые даты.
					{
						Theme = "Посланник 1ц";
						Zakaz = "В690";
						StartDate = new DateTime(2018, 6, 1);
						FinishDate = new DateTime(2021, 12, 24);
						var wk = new WorkSGR("ПОСЛАННИК-1Ц", 0, 0, 0, 1, 4, StartDate, FinishDate);
						ListSGR.Add(wk);
						wk = new WorkSGR("Разработка рабочей конструкторской документации(РКД) изд 720)", 1, 1, 0, 1, 2, StartDate, new DateTime(2020, 12, 24));
						ListSGR.Add(wk);
						wk = new WorkSGR("Этап 2", 2, 2, 0, 2, 3, new DateTime(2021, 1, 9), new DateTime(2021, 3, 13));
						ListSGR.Add(wk);
						wk = new WorkSGR("Этап 3", 3, 3, 0, 3, 4, new DateTime(2021, 3, 13), FinishDate);
						ListSGR.Add(wk);
						break;
					}
				#endregion
				case 7: /// Отлажен
					#region Проект Острота.
					{
						Theme = "Тема Острота";
						Zakaz = "В578";
						StartDate = new DateTime(2016, 4, 25);
						FinishDate = new DateTime(2022, 11, 25);
						var d3st = new DateTime(2017, 12, 1);
						var d3fn = new DateTime(2022, 4, 25);
						var wk = new WorkSGR("Не ломать !!! ОСТРОТА", 0, 0, 0, 1, 10, StartDate, FinishDate);
						ListSGR.Add(wk);
						wk = new WorkSGR("Этап 1  Разработка технического проекта.", 1, 1, 0, 1, 2, StartDate, new DateTime(2017, 6, 25));
						ListSGR.Add(wk);
						wk = new WorkSGR("Этап 2 Разработка рабочей конструкторской документации (РКД).", 2, 2, 0, 3, 4, new DateTime(2016, 11, 1), new DateTime(2018, 6, 25));
						ListSGR.Add(wk);
						wk = new WorkSGR("Этап 3 Изготовление опытной партии. Проведение предварительных наземных и летных испытаний. Корректировка РКД по результатам испытаний, присвоение РКД литеры 'О'.",
							3, 3, 0, 5, 6, d3st, d3fn);
						ListSGR.Add(wk);
						wk = new WorkSGR("    Этап 3.1 Изготовление опытных изделий для предварительных испытаний (ПИ) по согласованному перечню (первая очередь).",
							4, 4, 3, 5, 11, d3st, new DateTime(2020, 6, 25));
						ListSGR.Add(wk);
						wk = new WorkSGR("    Этап 3.2 Изготовление 2.", 5, 5, 3, 12, 13, new DateTime(2018, 6, 1), new DateTime(2021, 12, 15));
						ListSGR.Add(wk);
						wk = new WorkSGR("    Этап 3.3 Проведение.", 6, 6, 3, 14, 6, new DateTime(2019, 1, 1), d3fn);
						ListSGR.Add(wk);

						wk = new WorkSGR("Этап 4 Участие.", 7, 7, 0, 7, 8, new DateTime(2020, 12, 1), new DateTime(2022, 10, 25));
						ListSGR.Add(wk);
						wk = new WorkSGR("Этап 5 Корректировка.", 8, 8, 0, 9, 10, new DateTime(2022, 1, 1), FinishDate);
						ListSGR.Add(wk);
						break;
					}
				#endregion
				case 8: /// Отлажен
					#region Этап 21895. Прверка СГПЭ
					{
						Theme = "Посланник 1ц";
						Zakaz = "В69001017";
						StartDate = new DateTime(2019, 1, 9);
						FinishDate = new DateTime(2019, 12, 25);
						var wk = new WorkSGR("Разработка и выпуск РКД изд.720 (2 очередь)", 0, 0, 0, 20, 17, StartDate, FinishDate);
						ListSGR.Add(wk);
						wk = new WorkSGR("Разработка Доп. №3 к ТЗ на изд 720", 1, 1, 0, 171, 172, StartDate, new DateTime(2019, 4, 24));
						ListSGR.Add(wk);
						wk = new WorkSGR("Соглас. и утв. Доп. №3 к ТЗ на изд 720", 2, 2, 0, 172, 173, new DateTime(2019, 4, 24), new DateTime(2019, 5, 25));
						ListSGR.Add(wk);
						wk = new WorkSGR("Завершение оформления ККС  изд 720", 3, 3, 0, 171, 174, StartDate, new DateTime(2019, 3, 25));
						ListSGR.Add(wk);
						wk = new WorkSGR("Проработки по ККС изд 720", 7, 7, 0, 174, 175, new DateTime(2019, 3, 25), new DateTime(2019, 9, 25));
						ListSGR.Add(wk);
				/*		wk = new WorkSGR("Э 5 Проработка размещения изд 720 на ПУ С-Н", 8, 8, 0, 171, 176, StartDate, new DateTime(2019, 6, 25));
						ListSGR.Add(wk); */
						break;
					}
				#endregion
				case 9: /// Отлажен
					#region Этап 21891.
					{
						Theme = "Посланник 1ц";
						Zakaz = "В69001015";
						StartDate = new DateTime(2019, 1, 9);
						FinishDate = new DateTime(2019, 2, 15);
						var wk = new WorkSGR("Работа с разработчиками", 0, 0, 0, 20, 15, StartDate, FinishDate);
						ListSGR.Add(wk);
						wk = new WorkSGR("Этап 1", 1, 1, 0, 20, 210, StartDate, FinishDate);
						ListSGR.Add(wk);
						wk = new WorkSGR("Этап 2", 2, 2, 0, 20, 211, StartDate, FinishDate);
						ListSGR.Add(wk);
						wk = new WorkSGR("Этап 3", 3, 3, 0, 20, 212, StartDate, FinishDate);
						ListSGR.Add(wk);
						break;
					}
				#endregion
				case 10: /// Отлажен
					#region Этап 21942. [1-20] Разработка РКД изд 720 1-я очередь
					{
						SetSGR(false);
						Theme = "Посланник 1ц";
						Zakaz = "В69001";
						StartDate = new DateTime(2018, 6, 1);
						FinishDate = new DateTime(2018, 12, 25);
						var wk = new WorkSGR("Разработка РКД изд 720 1-я очередь", 0, 0, 0, 1, 20, StartDate, FinishDate);
						ListSGR.Add(wk);
						/*	wk = new WorkSGR("Этап 1", 1, 1, 0, 1, 11, StartDate, new DateTime(2018, 8, 25));
							ListSGR.Add(wk);
							wk = new WorkSGR("Этап 1.1", 2, 2, 1, 1, 11, StartDate, new DateTime(2018, 8, 25));
							ListSGR.Add(wk);
							wk = new WorkSGR("Этап 2", 3, 3, 0, 1, 12, StartDate, new DateTime(2018, 9, 25));
							ListSGR.Add(wk);
							wk = new WorkSGR("Этап 2.1", 4, 4, 3, 1, 12, StartDate, new DateTime(2018, 9, 25));
							ListSGR.Add(wk); */
						wk = new WorkSGR("Этап 3", 5, 5, 0, 13, 20, new DateTime(2018, 9, 1), FinishDate);
						ListSGR.Add(wk);
						wk = new WorkSGR("Этап 3.1", 6, 6, 5, 13, 20, new DateTime(2018, 9, 1), new DateTime(2018, 12, 1));
						ListSGR.Add(wk);
						break;
					}
				#endregion
				case 11: /// Отлажен
					#region Этап 21922. Разработка и выпуск РКД изд.720 (2 очередь)
					{
						SetSGR(false);
						Theme = "Посланник 1ц";
						Zakaz = "В69001017";
						StartDate = new DateTime(2019, 1, 9);
						FinishDate = new DateTime(2019, 12, 25);
						var wk = new WorkSGR("Разработка и выпуск РКД изд.720 (2 очередь)", 0, 0, 0, 20, 17, StartDate, FinishDate);
						ListSGR.Add(wk);
						wk = new WorkSGR("Этап 1", 1, 1, 0, 20, 700, StartDate, new DateTime(2019, 3, 25));
						ListSGR.Add(wk);
						wk = new WorkSGR("Этап 2", 2, 2, 0, 700, 701, new DateTime(2019, 3, 25), new DateTime(2019, 6, 25));
						ListSGR.Add(wk);
						wk = new WorkSGR("Этап 3", 3, 3, 0, 701, 702, new DateTime(2019, 6, 25), new DateTime(2019, 9, 25));
						ListSGR.Add(wk);
						wk = new WorkSGR("Этап 4", 4, 4, 0, 702, 703, new DateTime(2019, 9, 25), FinishDate);
						ListSGR.Add(wk);
						break;
					}
				#endregion
				case 12: /// Отлажен
					#region Этап 21950. длина этапа меньше месяца
					{
						SetSGR(false);
						Theme = "Посланник 1ц";
						Zakaz = "В69001017";
						StartDate = new DateTime(2019, 1, 9);
						FinishDate = new DateTime(2019, 12, 25);
						var wk = new WorkSGR("Разработка и выпуск РКД изд.720 (2 очередь)", 0, 0, 0, 20, 17, StartDate, FinishDate);
						ListSGR.Add(wk);
						//wk = new WorkSGR("Этап 1", 1, 1, 0, 20, 501, StartDate, new DateTime(2019, 3, 25));
						//ListSGR.Add(wk);
						wk = new WorkSGR("Этап 13", 5, 5, 0, 518, 519, new DateTime(2019, 10, 1), new DateTime(2019, 10, 25));
						ListSGR.Add(wk);
						wk = new WorkSGR("Этап 24", 6, 6, 0, 519, 17, new DateTime(2019, 10, 25), FinishDate);
						ListSGR.Add(wk);
						break;

					}
				#endregion
				case 13: /// Отлажен
					#region Этап 21894 1100 [20 - 17]. Проверка СГПЭ
					{
						SetSGR(false);
						//Theme = "Посланник 1ц";
						Theme = "Очень очень очень длинное название темы Посланник 1ц";
						Zakaz = "В69001017";
						StartDate = new DateTime(2019, 1, 9);
						FinishDate = new DateTime(2019, 12, 25);
						var wk = new WorkSGR("Разработка и выпуск РКД изд.720 (2 очередь)", 0, 0, 0, 20, 17, StartDate, FinishDate);
						ListSGR.Add(wk);
						wk = new WorkSGR("Утверждение перечня ТМ аппаратуры и расположение ее в изделии", 1, 1, 0, 20, 601, StartDate, new DateTime(2019, 3, 25));
						ListSGR.Add(wk);
						wk = new WorkSGR("Этап 1.1", 2, 2, 1, 20, 601, StartDate, new DateTime(2019, 3, 25));
						ListSGR.Add(wk);
						wk = new WorkSGR("Этап 1.2", 3, 3, 1, 20, 601, StartDate, new DateTime(2019, 3, 25));
						ListSGR.Add(wk);
						wk = new WorkSGR("Этап 2", 7, 7, 0, 601, 602, new DateTime(2019, 3, 25), new DateTime(2019, 6, 25));
						ListSGR.Add(wk);
						wk = new WorkSGR("Этап 2.1", 8, 8, 7, 601, 602, new DateTime(2019, 3, 25), new DateTime(2019, 6, 25));
						ListSGR.Add(wk); 
						break;
					}
				#endregion
				case 14: /// Отлажен
					#region Этап 21942 2010 [1 - 20]. Множественная детализация.
					{
						SetSGR(false);
						Theme = "Очень очень очень длинное название темы Посланник 1ц";
						Zakaz = "В69001015";
						StartDate = new DateTime(2018, 6, 1);
						FinishDate = new DateTime(2018, 12, 25);
						var wk = new WorkSGR("Работы с разработчиками СЧ (в том числе: офоромление Контрактов, Дополнений к ТЗ на СЧ ОКР, получение РКМпо СЧ ОКР с Заключением ВП разраб СЧ",
							0, 0, 0, 1, 20, StartDate, FinishDate);
						ListSGR.Add(wk);
						wk = new WorkSGR("Работы с разработчиками СЧ (в том числе: офоромление Контрактов, Дополнений к ТЗ на СЧ ОКР, получение РКМпо СЧ ОКР с Заключением ВП разраб СЧ",
							1, 1, 0, 1, 11, StartDate, new DateTime(2018, 8, 25));
						wk.AddDescription("1", "57820001", 100000);
						wk.AddDescription("3", "57820003", 100000);
						wk.AddDescription("4", "57820004", 100000);
						wk.AddDescription("5", "57820005", 100000);
						ListSGR.Add(wk);
						wk = new WorkSGR("Работы с разработчиками СЧ (в том числе: офоромление Контрактов, Дополнений к ТЗ на СЧ ОКР, получение РКМпо СЧ ОКР с Заключением ВП разраб СЧ",
							2, 2, 1, 1, 11, StartDate, new DateTime(2018, 8, 25));
						wk.AddDescription("1", "57820001", 100000);
						ListSGR.Add(wk);
						wk = new WorkSGR("Работы с разработчиками СЧ (в том числе: офоромление Контрактов, Дополнений к ТЗ на СЧ ОКР, получение РКМпо СЧ ОКР с Заключением ВП разраб СЧ",
							5, 5, 0, 1, 12, StartDate, new DateTime(2018, 9, 25));
						wk.AddDescription("1", "57820001", 100000);
						wk.AddDescription("3", "57820003", 100000);
						wk.AddDescription("4", "57820004", 100000);
						wk.AddDescription("5", "57820005", 100000);
						ListSGR.Add(wk);
						wk = new WorkSGR("   Проектно-конструкторские работы по облику изделия 720, в том числе: уточнение облика, разработка ККС, разработка АС,  проработка конструктивно-технологической схемы планера",
							6, 6, 5, 1, 12, StartDate, new DateTime(2018, 9, 25));
						wk.AddDescription("1", "57820001", 100000);
						ListSGR.Add(wk);
						wk = new WorkSGR("   Проектно-конструкторские работы по облику изделия 720, в том числе: уточнение облика, разработка ККС, разработка АС,  проработка конструктивно-технологической схемы планера ",
							7, 7, 5, 1, 12, StartDate, new DateTime(2018, 9, 25));
						wk.AddDescription("1", "57820001", 100000);
						ListSGR.Add(wk);
						wk = new WorkSGR("Разработка и выпуск РКД изделия 720, 1-я очередь",
							10, 10, 0, 13, 20, new DateTime(2018, 9, 1), FinishDate);
						wk.AddDescription("1", "57820001", 100000);
						ListSGR.Add(wk);
						wk = new WorkSGR("Разработка и выпуск РКД изделия 720, 1-я очередь",
							11, 11, 10, 13, 20, new DateTime(2018, 9, 1), new DateTime(2018, 12, 1));
						wk.AddDescription("1", "57820001", 100000);
						ListSGR.Add(wk);
						break;
					}
				#endregion
				case 15: /// Отлажен 
					#region Этап 21889 2010 [20 - 30].
					{
						SetSGR(false);
						Theme = "Очень очень очень длинное название темы Посланник 1ц";
						Zakaz = "В69001";
						StartDate = new DateTime(2019, 1, 9);
						FinishDate = new DateTime(2019, 12, 25);
						var wk = new WorkSGR("Разработка РКД изд 720 2-я очередь", 0, 0, 0, 20, 30, StartDate, FinishDate);
						ListSGR.Add(wk);
						wk = new WorkSGR("Этап 1", 1, 1, 0, 20, 30, StartDate, FinishDate);
						ListSGR.Add(wk);
						wk = new WorkSGR("Этап 2", 2, 2, 0, 20, 15, StartDate, new DateTime(2019, 2, 15));
						ListSGR.Add(wk);
						wk = new WorkSGR("Этап 2.1", 5, 5, 2, 20, 15, StartDate, new DateTime(2019, 2, 15));
						ListSGR.Add(wk);
						wk = new WorkSGR("Этап 3", 10, 10, 0, 20, 16, StartDate, new DateTime(2019, 2, 25));
						ListSGR.Add(wk);
						wk = new WorkSGR("Этап 3.1", 11, 11, 10, 20, 16, StartDate, new DateTime(2019, 2, 25));
						ListSGR.Add(wk);
						wk = new WorkSGR("Этап 4", 20, 20, 0, 20, 17, StartDate, FinishDate);
						ListSGR.Add(wk);
						wk = new WorkSGR("Этап 4.1", 21, 21, 20, 20, 17, StartDate, FinishDate);
						ListSGR.Add(wk);
						break;
					}
				#endregion
				case 16:
					#region Проект 21895. кусок. Проверка СГР
					{
						SetSGR(false);
						Theme = "Посланник 1ц";
						Zakaz = "В69001017";
						StartDate = new DateTime(2019, 1, 9);
						FinishDate = new DateTime(2019, 12, 25);
						var wk = new WorkSGR("Разработка и выпуск РКД изд.720 (2 очередь)", 0, 0, 0, 20, 17, StartDate, FinishDate);
						ListSGR.Add(wk);
						wk = new WorkSGR("Разработка Доп. №3 к ТЗ на изд 720", 1, 1, 0, 171, 172, StartDate, new DateTime(2019, 4, 24));
						ListSGR.Add(wk);
						wk = new WorkSGR("Соглас. и утв. Доп. №3 к ТЗ на изд 720", 2, 2, 0, 172, 173, new DateTime(2019, 4, 24), new DateTime(2019, 5, 25));
						ListSGR.Add(wk);
						wk = new WorkSGR("Завершение оформления ККС  изд 720", 3, 3, 0, 171, 174, StartDate, new DateTime(2019, 3, 25));
						ListSGR.Add(wk);
						wk = new WorkSGR("Проработки по ККС изд 720", 7, 7, 0, 174, 175, new DateTime(2019, 3, 25), new DateTime(2019, 9, 25));
						ListSGR.Add(wk);
						break;
					}
					#endregion
			}

		}
	}
}
