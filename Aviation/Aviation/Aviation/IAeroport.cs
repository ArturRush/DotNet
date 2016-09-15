using System.Collections.Generic;
using Aviation.Engines;

namespace Aviation.Aviation
{
	/// <summary>
	/// Интерфейс аэропортов
	/// </summary>
	interface IAeroport : ICollection<IPassengerAviation<IEngine>>
	{
		/// <summary>
		/// Возвращает судно из аэропорта по индексу
		/// </summary>
		/// <param name="ind">Индекс</param>
		/// <returns>Воздушное судно</returns>
		IPassengerAviation<IEngine> this[int ind] { get; set; }

		/// <summary>
		/// Запросить подходящее судно у аэропорта. Возвращает null, если такого нет
		/// </summary>
		/// <param name="peoples">Количество людей для посадки</param>
		/// <param name="distance">Дистанция перелета</param>
		/// <returns></returns>
		IPassengerAviation<IEngine> GetAvia(int peoples, int distance);

		/// <summary>
		/// Получить список воздушных судов аэропорта
		/// </summary>
		void PrintAviation();

		/// <summary>
		/// Заполняет аэропорт воздушной техникой
		/// </summary>
		/// <param name="planes">Количество самолетов</param>
		/// <param name="helicopters">Количество вертолетов</param>
		void FillAeroport(int planes, int helicopters);
	}
}
