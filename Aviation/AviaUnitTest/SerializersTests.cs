using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aviation.Aviation;
using Aviation.Engines;
using Aviation.Serializer;
using NUnit.Framework;

namespace AviaUnitTest
{
	/// <summary>
	/// Тестирование сериализаторов
	/// </summary>
	[TestFixture]
	public class SerializersTests
	{
		/// <summary>
		/// Создание и проверка работы сериализаторов
		/// </summary>
		[Test]
		public void WorkTests()
		{
			var binSer = new BinarySerializer<Plane<ReactiveEngine>>();
			var xmlSer = new XmlCustomSerializer<Plane<ReactiveEngine>>();
			var jsonSer = new JsonSerializer<Plane<ReactiveEngine>>();

			var aer = new Aeroport<Plane<ReactiveEngine>>("Кольцово");
			for (int i = 0; i < 10; ++i)
			{
				aer.Add(new Plane<ReactiveEngine>(100, 20000, "SuperJet", new ReactiveEngine("DoublePower")));
			}

			binSer.Serialize(aer, "aerBinary.dat");
			aer.PrintAviation();
			aer = binSer.Deserialize("aerBinary.dat");
			aer.PrintAviation();

			xmlSer.Serialize(aer, "aerXML.xml");
			aer.PrintAviation();
			aer = xmlSer.Deserialize("aerXML.xml");
			aer.PrintAviation();


			jsonSer.Serialize(aer, "aerjson.json");
			aer.PrintAviation();
			aer = jsonSer.Deserialize("aerjson.json");
			aer.PrintAviation();

		}
	}
}
