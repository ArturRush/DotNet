using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Runtime.Remoting.Messaging;
using Aviation.Aviation;
using Aviation.Engines;
using Aviation.Exeptions;
using Aviation.Factories;
using NUnit.Framework;

namespace AviaUnitTest
{
	/// <summary>
	/// Тестирование коллекции аэропорт
	/// </summary>
	[TestFixture]
	public class AeroportTests
	{
		private List<IPassengerAviation<IEngine>> aviation;
		[SetUp]
		public void Init()
		{
			aviation = new List<IPassengerAviation<IEngine>>()
			{
				new Plane<ReactiveEngine>(10,20,"Alfa",new ReactiveEngine("Eng")),
				new Plane<ReactiveEngine>(10,20,"Alfa",new ReactiveEngine("Eng")),
				new Plane<ReactiveEngine>(10,20,"Alfa",new ReactiveEngine("Eng")),
				new Plane<ReactiveEngine>(10,20,"Alfa",new ReactiveEngine("Eng")),
				new Plane<ReactiveEngine>(10,20,"Alfa",new ReactiveEngine("Eng"))
			};
		}


		[Test]
		public void CreateTest1()
		{
			string name = "TestName";
			var aerTest = new Aeroport<IPassengerAviation<IEngine>>(name);

			Assert.AreEqual(name, aerTest.Name);
			Assert.AreEqual(null, aerTest.Sorter);
			Assert.AreEqual(null, aerTest.Comparer);
			Assert.AreEqual(null, aerTest.Progressor);
		}

		[Test]
		public void CreateTest2()
		{
			string name = "TestName";
			var aerTest = new Aeroport<IPassengerAviation<IEngine>>(name,aviation);

			Assert.AreEqual(name, aerTest.Name);
			Assert.AreEqual(null, aerTest.Sorter);
			Assert.AreEqual(null, aerTest.Comparer);
			Assert.AreEqual(null, aerTest.Progressor);
			Assert.AreEqual(aviation,aerTest.Aviation);
		}


		[Test]
		public void IndexTest()
		{
			string name = "TestName";
			var aer = new Aeroport<IPassengerAviation<IEngine>>(name, aviation);
			Assert.AreEqual(aer[0], aviation[0]);
		}

		[Test]
		public void CountItemsTest()
		{
			string name = "TestName";
			var aer = new Aeroport<IPassengerAviation<IEngine>>(name, aviation);
			Assert.AreEqual(aviation.Count, aer.Count);
		}

		[Test]
		public void AddItemTest()
		{
			string name = "TestName";
			var aer = new Aeroport<IPassengerAviation<IEngine>>(name, aviation);
			var tmp = new Plane<ReactiveEngine>(40, 3, "NotInAer", new ReactiveEngine("Eng"));
			aer.Add(tmp);
			Assert.AreEqual(tmp, aer[aer.Count-1]);
		}

		[Test]
		public void ClearAeroportTest()
		{
			string name = "TestName";
			var aer = new Aeroport<IPassengerAviation<IEngine>>(name, aviation);
			aer.Clear();
			Assert.AreEqual(0,aer.Count);
		}

		[Test]
		public void ContainsItemTest()
		{
			string name = "TestName";
			var aer = new Aeroport<IPassengerAviation<IEngine>>(name, aviation);
			var notInAer = new Plane<ReactiveEngine>(40, 3, "NotInAer", new ReactiveEngine("Eng"));
			var inAer = aer[0];

			Assert.AreEqual(false, aer.Contains(notInAer));
			Assert.AreEqual(true, aer.Contains(inAer));
		}

		[Test]
		public void RemoveItemTest()
		{
			string name = "TestName";
			var aer = new Aeroport<IPassengerAviation<IEngine>>(name, aviation);
			var tmp = new Plane<ReactiveEngine>(40, 3, "NotInAer", new ReactiveEngine("Eng"));
			aer.Add(tmp);
			aer.Remove(tmp);
			Assert.AreEqual(false, aer.Contains(tmp));
		}

		[Test]
		public void GetAviaTest()
		{
			string name = "TestName";
			var aer = new Aeroport<IPassengerAviation<IEngine>>(name, aviation);
			var tmp1 = aer.GetAvia(0, 0);
			var tmp2 = aer.GetAvia(200, 200);

			Assert.AreEqual(aer[0], tmp1);
		}

		[Test]
		public void PrintTest()
		{
			string name = "TestName";
			var aer = new Aeroport<IPassengerAviation<IEngine>>(name, aviation);
			Assert.DoesNotThrow(()=>aer.PrintAviation());
		}

		[Test]
		public void SortTest()
		{
			string name = "TestName";
			var aer = new Aeroport<IPassengerAviation<IEngine>>(name, aviation);
			
			Assert.DoesNotThrow(() => aer.Sort());
		}
		[Test]
		public void SortAsyncTest()
		{
			string name = "TestName";
			var aer = new Aeroport<IPassengerAviation<IEngine>>(name, aviation);
			Random r = new Random();
			foreach (var avia in aer)
			{
				avia.PlacePassenger(r.Next(10));
			}
			aer.Comparer = (a, b) => a.Engaged.CompareTo(b.Engaged);
			bool sorted = true;

			for (int i = 1; i < aer.Count; ++i)
				if (aer[i].Engaged < aer[i - 1].Engaged)
					sorted = false;
			Assert.AreEqual(true, sorted);
		}

		[Test]
		public void DoSmthTest()
		{
			string name = "TestName";
			var aer = new Aeroport<IPassengerAviation<IEngine>>(name, aviation);

			Assert.DoesNotThrow(()=>aer.DoSmth((a)=>a.PlacePassenger(1)));
		}

		[Test]
		public void PrintSomeInfoTest()
		{
			string name = "TestName";
			var aer = new Aeroport<IPassengerAviation<IEngine>>(name, aviation);

			Assert.DoesNotThrow(() => aer.PrintSomeInfo((a)=>a.Engaged));
		}
	}
}
