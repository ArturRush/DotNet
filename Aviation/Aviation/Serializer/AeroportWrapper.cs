using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Aviation.Aviation;
using Aviation.Engines;

namespace Aviation.Serializer
{
	/// <summary>
	/// Класс-обертка для сериализации аэропорта
	/// </summary>
	[Serializable]
	[XmlRoot]
	internal class AeroportWrapper<T> where T: IPassengerAviation<IEngine>
	{
		[XmlAttribute] 
		public string Name { get; set; }
		public List<T> Aviation { get; set; }

		public AeroportWrapper(Aeroport<T> aero)
		{
			Name = aero.Name;
			Aviation = aero.Aviation;
		}

		public AeroportWrapper()
		{
			
		}
	}
}
