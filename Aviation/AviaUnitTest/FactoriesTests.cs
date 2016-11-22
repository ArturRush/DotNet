using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aviation.Aviation;
using Aviation.Engines;
using Aviation.Factories;
using NUnit.Framework;

namespace AviaUnitTest
{
	/// <summary>
	/// Тесты для абстрактных фабрик
	/// </summary>
	[TestFixture]
	public class FactoriesTests
	{
		/// <summary>
		/// Проверка создания фабрики
		/// </summary>
		[Test]
		public void CreateFactoryTest()
		{
			var af = new AmericanAviationFactory();
			var rf = new RussianAviationFactory();
		}
		/// <summary>
		/// Проверка создания фабриками авиации
		/// </summary>
		[Test]
		public void FactoryCreatorTest()
		{
			var af = new AmericanAviationFactory();
			var rf = new RussianAviationFactory();
			var ah = af.CreateHelicopter();
			var at = af.CreateTurbopropPlane();
			var ar = af.CreateReactivePlane();
			var rh = rf.CreateHelicopter();
			var rt = rf.CreateTurbopropPlane();
			var rr = rf.CreateReactivePlane();
			Assert.AreEqual(ah.GetType(), typeof(Helicopter<GasTurbineEngine>));
			Assert.AreEqual(at.GetType(), typeof(Plane<TurbopropEngine>));
			Assert.AreEqual(ar.GetType(), typeof(Plane<ReactiveEngine>));
			Assert.AreEqual(rh.GetType(), typeof(Helicopter<GasTurbineEngine>));
			Assert.AreEqual(rt.GetType(), typeof(Plane<TurbopropEngine>));
			Assert.AreEqual(rr.GetType(), typeof(Plane<ReactiveEngine>));
		}
		/// <summary>
		/// Проверка заполнения аэропорта случайной авиацией
		/// </summary>
		[TestCase(10,20)]
		[TestCase(10,0)]
		[TestCase(0,20)]
		public void AeroGeneratorTest(int planeCount, int heliCount)
		{
			var aer = new Aeroport<IPassengerAviation<IEngine>>("Кольцово");
			RandomAviationGenerator.FillAeroport(planeCount,heliCount,aer);
			Assert.AreEqual(aer.Count, planeCount+heliCount);
		}
	}
}
