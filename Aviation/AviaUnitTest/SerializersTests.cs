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
			var aerCopy = aer;
			binSer.Serialize(aer, "aerBinary.dat");
			aer = binSer.Deserialize("aerBinary.dat");

			xmlSer.Serialize(aer, "aerXML.xml");
			aer = xmlSer.Deserialize("aerXML.xml");


			jsonSer.Serialize(aer, "aerjson.json");
			aer = jsonSer.Deserialize("aerjson.json");
			for(int i = 0; i<aer.Count; ++i)
			{
				Assert.AreEqual(aer.Aviation[i].Model, aerCopy.Aviation[i].Model);
				Assert.AreEqual(aer.Aviation[i].Capacity, aerCopy.Aviation[i].Capacity);
			}
		}
	}
}
