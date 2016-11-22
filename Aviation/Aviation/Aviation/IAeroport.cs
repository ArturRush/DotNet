using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Aviation.Engines;

namespace Aviation.Aviation
{
	/// <summary>
	/// Интерфейс аэропортов
	/// </summary>
	public interface IAeroport<T> : ICollection<T> where T : IPassengerAviation<IEngine>
	{
		/// <summary>
		/// Метод сортировки коллекции
		/// </summary>
		Aeroport<T>.MySorter Sorter { get; set; }
		/// <summary>
		/// Метод сравнения элементов для сортировки
		/// </summary>
		Func<T, T, int> Comparer { get; set; }
		/// <summary>
		/// Медот отображения прогресса сортировки
		/// </summary>
		Action<int> Progressor { get; set; }

		/// <summary>
		/// Возвращает судно из аэропорта по индексу
		/// </summary>
		/// <param name="ind">Индекс</param>
		/// <returns>Воздушное судно</returns>
		T this[int ind] { get; set; }

		/// <summary>
		/// Запросить подходящее судно у аэропорта. Возвращает null, если такого нет
		/// </summary>
		/// <param name="peoples">Количество людей для посадки</param>
		/// <param name="distance">Дистанция перелета</param>
		/// <returns></returns>
		T GetAvia(int peoples, int distance);

		/// <summary>
		/// Получить список воздушных судов аэропорта
		/// </summary>
		void PrintAviation();

		/// <summary>
		/// Сортировка коллекции по заданному условию. Требует указать для коллекции Sorter и Comparer
		/// </summary>
		void Sort();

		/// <summary>
		/// Выполняет указанное действие для всех элементов коллекции
		/// </summary>
		/// <param name="smth">Действие</param>
		void DoSmth(Action<T> smth);

		/// <summary>
		/// Для каждого элемента коллекции складывает значение, возвращаемое переданной функцией
		/// </summary>
		/// <param name="takeInfo">Функция подсчета</param>
		/// <returns>Сумма значений</returns>
		int PrintSomeInfo(Func<T, int> takeInfo);

		/// <summary>
		/// Прогресс сортировки
		/// </summary>
		int SortProgress { get;}

		/// <summary>
		/// Асинхронная блинная сортировка. Требует указать для коллекции Comparer
		/// </summary>
		Task SortAsynk();
	}
}
