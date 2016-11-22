using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aviation.Aviation;
using Aviation.Engines;

namespace Aviation.Serializators
{
	/// <summary>
	/// Интерфейс для сериализаторов
	/// </summary>
	/// <typeparam name="T">Тип сериализуемой коллекции</typeparam>
	interface ISerializer<T> where T: IPassengerAviation<IEngine>
	{
		/// <summary>
		/// Сериализация объекта
		/// </summary>
		/// <param name="avia">Объект</param>
		/// <param name="filePath">Путь к файлу</param>
		void Serialize(Aeroport<T> avia, string filePath);
		/// <summary>
		/// Десериализация объекта
		/// </summary>
		/// <returns>Объект</returns>
		/// <param name="filePath">Путь к файлу</param>
		Aeroport<T> Deserialize(string filePath);
	}
}
