using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aviation.Engines
{
	/// <summary>
	/// Базовый класс двигателей
	/// </summary>
	public abstract class EngineBase : IEngine
	{
		public enum Direction { Forward, Backward, Right, Left, Up, Down}
		public int Consumption { get; private set; }

		public string Model { get; private set; }

		public int Speed { get; private set; }

		public virtual void Move()
		{
			
		}

		public void ChangeSpeed(int newSpeed)
		{
			Console.WriteLine("Новая скорость {0} км/ч", newSpeed);
		}

		protected EngineBase(int consumption, string model, int speed)
		{
			Consumption = consumption;
			Model = model;
			Speed = speed;
		}
	}
}
