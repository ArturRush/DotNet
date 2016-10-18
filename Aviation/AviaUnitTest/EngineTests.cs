using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aviation.Engines;
using NUnit.Framework;
using NUnit.Framework.Internal;

namespace AviaUnitTest
{
	/// <summary>
	/// Тестирование класса двигатель
	/// </summary>
	[TestFixture]
	public class EngineTests
	{
		/// <summary>
		/// Проверка создания двигателей разных типов и изменения скорости
		/// </summary>
		/// <param name="newSpeed">Новая скорость</param>
		[TestCase(10)]
		[TestCase(0)]
		[TestCase(-100)]
		public void BaseTests(int newSpeed)
		{
			var gas = new GasTurbineEngine("gas");
			var re = new ReactiveEngine("gas");
			var tur = new TurbopropEngine("gas");
			Assert.DoesNotThrow(()=>gas.ChangeSpeed(newSpeed));
			Assert.DoesNotThrow(()=>re.ChangeSpeed(newSpeed));
			Assert.DoesNotThrow(()=>tur.ChangeSpeed(newSpeed));
			Assert.DoesNotThrow(()=>gas.Fly());
			Assert.DoesNotThrow(()=>re.Fly());
			Assert.DoesNotThrow(()=>tur.Fly());
		}
		/// <summary>
		/// Проверка для направлений полета вертолетного двигателя
		/// </summary>
		/// <param name="dir">Направление полета</param>
		[TestCase(EngineBase.Direction.Backward)]
		[TestCase(EngineBase.Direction.Forward)]
		[TestCase(EngineBase.Direction.Right)]
		[TestCase(EngineBase.Direction.Left)]
		[TestCase(EngineBase.Direction.Up)]
		[TestCase(EngineBase.Direction.Down)]
		public void FlyToTests(EngineBase.Direction dir)
		{
			var gas = new GasTurbineEngine("gas");
			Assert.DoesNotThrow(()=>gas.FlyTo(dir));
		}
		/// <summary>
		/// Проверка проверятеля двигателей
		/// </summary>
		[Test]
		public void EngineCheckerTest()
		{
			var gas = new GasTurbineEngine("gas");
			var engChecker = new EngineChecker();
			Assert.DoesNotThrow(()=>engChecker.Check(gas));
		}
	}
}
