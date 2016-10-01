using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aviation;
using Aviation.Aviation;
using Aviation.Engines;
using Aviation.Exeptions;
using NUnit.Framework;

namespace AviaUnitTest
{
	/// <summary>
	/// Тестирование классов авиации
	/// </summary>
	[TestFixture]
	class PassengerAviationTests
	{
		private int cap;
		private int capTank;
		private int engaged;
		private string model;
		private ReactiveEngine reEngine;
		private TurbopropEngine turEngine;
		private GasTurbineEngine hEngine;
		/// <summary>
		/// Инициирующая функция
		/// </summary>
		[SetUp]
		public void Init()
		{
			cap = 10;
			capTank = 100;
			engaged = 0;
			model = "Test";
			reEngine = new ReactiveEngine("react");
			turEngine = new TurbopropEngine("react");
			hEngine = new GasTurbineEngine("gastour");
		}
		/// <summary>
		/// Тестирование создания экземпляров авиации
		/// </summary>
		[Test]
		public void CreateTest()
		{
			var a1 = new Plane<ReactiveEngine>(cap, capTank, model, reEngine);
			var a2 = new Plane<TurbopropEngine>(cap, capTank, model, turEngine);
			var a3 = new Helicopter<GasTurbineEngine>(cap, capTank, model, hEngine);
			
			Assert.AreEqual(cap, a1.Capacity);
			Assert.AreEqual(cap, a2.Capacity);
			Assert.AreEqual(cap, a3.Capacity);
			Assert.AreEqual(capTank, a1.TankCapacity);
			Assert.AreEqual(model, a1.Model);
			Assert.AreEqual(reEngine, a1.Engine);
			Assert.AreEqual(capTank, a2.TankCapacity);
			Assert.AreEqual(model, a2.Model);
			Assert.AreEqual(turEngine, a2.Engine);
			Assert.AreEqual(capTank, a3.TankCapacity);
			Assert.AreEqual(model, a3.Model);
			Assert.AreEqual(hEngine, a3.Engine);
		}
		/// <summary>
		/// Тестирование посадки пассажиров
		/// </summary>
		[Test]
		public void PlacePassengerTest()
		{
			var a1 = new Plane<ReactiveEngine>(cap, capTank, model, reEngine);
			var a2 = new Plane<TurbopropEngine>(cap, capTank, model, turEngine);
			var a3 = new Helicopter<GasTurbineEngine>(cap, capTank, model, hEngine);
			a1.PlacePassenger(5);
			a2.PlacePassenger(20);
			
			Assert.AreEqual(a1.Engaged, 5);
			Assert.AreEqual(a2.Engaged, cap);
			Assert.Throws<PassPlaceException>(()=>a3.PlacePassenger(-3));
		}
		/// <summary>
		/// Тестирование высадки пассажиров
		/// </summary>
		[Test]
		public void DropOffPassTest()
		{
			var a1 = new Plane<ReactiveEngine>(cap, capTank, model, reEngine);
			a1.PlacePassenger(5);
			a1.DropOffPassenger();
			Assert.AreEqual(0, a1.Engaged);
		}
		/// <summary>
		/// Тестирование посылки сообщения
		/// </summary>
		[Test]
		public void SendMessTest()
		{
			var a1 = new Plane<ReactiveEngine>(cap, capTank, model, reEngine);
			var a2 = new Plane<TurbopropEngine>(cap, capTank, model, turEngine);

			Assert.DoesNotThrow(()=>a1.SendMessage(a2, "test"));
			Assert.DoesNotThrow(()=>a2.ReceiveMessage("fdsfsdf"));
			Assert.Throws<NoTargetExeption>(() => a1.SendMessage(null, "test"));
		}
		/// <summary>
		/// Тестирование совершения полета
		/// </summary>
		/// <param name="from">Откуда</param>
		/// <param name="to">Куда</param>
		[TestCase(Routs.Cities.Denver, Routs.Cities.Moscow)]
		[TestCase(Routs.Cities.Cherlak, Routs.Cities.Omsk)]
		[TestCase(Routs.Cities.Kazan, Routs.Cities.Kazan)]
		public void MakeFlightTest(Routs.Cities from, Routs.Cities to)
		{
			var a1 = new Plane<ReactiveEngine>(cap, capTank, model, reEngine);
			var a3 = new Helicopter<GasTurbineEngine>(cap, capTank, model, hEngine);
			Assert.DoesNotThrow(()=>a1.MakeFlight(from,to));
			Assert.DoesNotThrow(()=>a3.MakeFlight(from,to));
		}
		/// <summary>
		/// Тестирование клонирования элемента
		/// </summary>
		[Test]
		public void CloneTest()
		{
			var a1 = new Plane<ReactiveEngine>(cap, capTank, model, reEngine);
			var a3 = new Helicopter<GasTurbineEngine>(cap, capTank, model, hEngine);
			Plane<ReactiveEngine> a2 = (Plane<ReactiveEngine>)a1.Clone();
			Helicopter<GasTurbineEngine> a4 = (Helicopter<GasTurbineEngine>)a3.Clone();
			Assert.AreEqual(a1.Capacity, a2.Capacity);
			Assert.AreEqual(a1.TankCapacity, a2.TankCapacity);
			Assert.AreEqual(a1.Engine, a2.Engine);
			Assert.AreEqual(a3.Capacity, a4.Capacity);
			Assert.AreEqual(a3.TankCapacity, a4.TankCapacity);
			Assert.AreEqual(a3.Engine, a4.Engine);
			
		}
	}
}
