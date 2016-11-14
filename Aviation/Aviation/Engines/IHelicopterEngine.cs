using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aviation.Engines
{
	/// <summary>
	/// Интерфейс "Вертолетный двигатель"
	/// </summary>
	public interface IHelicopterEngine:IEngine
	{
		/// <summary>
		/// Указать направление полета
		/// </summary>
		/// <param name="to">Направление</param>
		void FlyTo(Direction to);
	}
}
