using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aviation.Engines
{
	/// <summary>
	/// Вертолетный двигатель
	/// </summary>
	[Serializable]
	internal abstract class HelicopterEngine : EngineBase, IHelicopterEngine
	{
		protected HelicopterEngine(int consumption, string model, int speed) : base(consumption, model, speed)
		{
		}

		protected HelicopterEngine()
		{
			
		}

		override public void Move()
		{
			Console.WriteLine("Двигатель {0} разгоняет винт", Model);
			Console.WriteLine("Судно взлетает");
			Console.WriteLine("Судно прилетает в пункт назначения");
		}

		public void FlyTo(Direction to)
		{
			switch (to)
			{
				case Direction.Forward: Console.WriteLine("Вертолет летит вперед");
					break;
				case Direction.Backward: Console.WriteLine("Вертолет летит назад");
					break;
				case Direction.Left: Console.WriteLine("Вертолет летит влево");
					break;
				case Direction.Right: Console.WriteLine("Вертолет летит вправо");
					break;
				case Direction.Up: Console.WriteLine("Вертолет летит вверх");
					break;
				case Direction.Down: Console.WriteLine("Вертолет летит вниз");
					break;
			}
		}
	}
}
