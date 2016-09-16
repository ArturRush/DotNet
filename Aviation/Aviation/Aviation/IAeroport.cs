using System.Collections.Generic;
using Aviation.Engines;

namespace Aviation.Aviation
{
	/// <summary>
	/// Интерфейс аэропортов
	/// </summary>
	interface IAeroport<T> : ICollection<T> where T : IPassengerAviation<IEngine>
	{
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
	}
}
