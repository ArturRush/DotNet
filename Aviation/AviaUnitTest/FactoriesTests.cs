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
		}
		/// <summary>
		/// Проверка заполнения аэропорта случайной авиацией
		/// </summary>
		[Test]
		public void AeroGeneratorTest()
		{
			var aer = new Aeroport<IPassengerAviation<IEngine>>("Кольцово");
			RandomAviationGenerator.FillAeroport(10,20,aer);
		}
	}
}
