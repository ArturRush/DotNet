using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aviation.Engines
{
	/// <summary>
	/// Интерфейс "Самолетный двигатель"
	/// </summary>
	public interface IPlaneEngine :IEngine
	{
		/// <summary>
		/// Лететь вперед
		/// </summary>
		void Fly();
	}
}
