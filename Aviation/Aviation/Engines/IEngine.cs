using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Aviation.Engines
{
	/// <summary>
	/// Интерфейс "двигатель"
	/// </summary>
	public interface IEngine
	{
		/// <summary>
		/// Расход топлива на 100 км;
		/// </summary>
		int Consumption { get; }

		/// <summary>
		/// Модель двигателя
		/// </summary>
		string Model { get; }

		/// <summary>
		/// Скорость движения
		/// </summary>
		int Speed { get; }

		/// <summary>
		/// Перемещать судно
		/// </summary>
		void Move();
	}
}
