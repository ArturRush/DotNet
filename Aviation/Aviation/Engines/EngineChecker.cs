using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aviation.Engines
{
	class EngineChecker : ICheckEngine<IEngine>
	{
		public void Check(IEngine obj)
		{
			Random r = new Random();
			int maxSpeed = r.Next(90, 100);
			int curCons = r.Next(95, 120);
			Console.WriteLine("Максимальная корость {0}% от номинальной ({1} км/ч)", maxSpeed, obj.Speed*maxSpeed);
			Console.WriteLine("Расход топлива {0}% от номинального ({1} л/100км)", curCons, obj.Consumption*curCons);
			Console.WriteLine("Все системы в норме");
		}
	}
}
