using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aviation.Aviation;
using Aviation.Engines;
using NUnit.Framework;

namespace AviaUnitTest
{
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

		[SetUp]
		private void Init()
		{
			cap = 10;
			capTank = 100;
			engaged = 0;
			model = "Test";
			reEngine = new ReactiveEngine("react");
			turEngine = new TurbopropEngine("react");
			hEngine = new GasTurbineEngine("gastour");
		}

		[Test]
		private void CreateTest()
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
			Assert.AreEqual(reEngine, a2.Engine);
			Assert.AreEqual(capTank, a3.TankCapacity);
			Assert.AreEqual(model, a3.Model);
			Assert.AreEqual(reEngine, a3.Engine);
		}

		[Test]
		private void PlacePassengerTest()
		{
			var a1 = new Plane<ReactiveEngine>(cap, capTank, model, reEngine);
			var a2 = new Plane<TurbopropEngine>(cap, capTank, model, turEngine);
			var a3 = new Helicopter<GasTurbineEngine>(cap, capTank, model, hEngine);
			a1.PlacePassenger(5);
			a2.PlacePassenger(20);
			
			Assert.AreEqual(a1.Engaged, 5);
			Assert.AreEqual(a1.Engaged, cap);
			Assert.Throws<Exception>(()=>a3.PlacePassenger(-3));
		}

		[Test]
		private void DropOffPassTest()
		{
			var a1 = new Plane<ReactiveEngine>(cap, capTank, model, reEngine);
			a1.PlacePassenger(5);
			a1.DropOffPassenger();
			Assert.AreEqual(0, a1.Engaged);
		}

		[Test]
		private void SendMessTest()
		{
			var a1 = new Plane<ReactiveEngine>(cap, capTank, model, reEngine);
			var a2 = new Plane<TurbopropEngine>(cap, capTank, model, turEngine);

			Assert.DoesNotThrow(()=>a1.SendMessage(a2, "test"));
			Assert.Throws<Exception>(() => a1.SendMessage(null, "test"));
		}
	}
}
