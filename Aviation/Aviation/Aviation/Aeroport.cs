using System;
using System.Collections;
using System.Collections.Generic;
using Aviation.Engines;
using Aviation.Factories;

namespace Aviation.Aviation
{
	/// <summary>
	/// Аэропорт
	/// </summary>
	class Aeroport : IAeroport
	{
		private readonly List<IPassengerAviation<IEngine>> _aviation;

		public IPassengerAviation<IEngine> this[int ind]
		{
			get { return _aviation[ind]; }
			set { _aviation[ind] = value; }
		}

		public Aeroport(string name)
		{
			Name = name;
			_aviation = new List<IPassengerAviation<IEngine>>();
		}

		public string Name { get; private set; }

		public IEnumerator<IPassengerAviation<IEngine>> GetEnumerator()
		{
			return new AeroportEnumerator(this);
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return new AeroportEnumerator(this);
		}

		public void Add(IPassengerAviation<IEngine> item)
		{
			_aviation.Add(item);
		}

		public void Clear()
		{
			_aviation.Clear();
		}

		public bool Contains(IPassengerAviation<IEngine> item)
		{
			return _aviation.Contains(item);
		}

		public void CopyTo(IPassengerAviation<IEngine>[] array, int arrayIndex)
		{
			_aviation.CopyTo(array, arrayIndex);
		}

		public bool Remove(IPassengerAviation<IEngine> item)
		{
			return _aviation.Remove(item);
		}

		public int Count
		{
			get { return _aviation.Count; }
		}

		public bool IsReadOnly
		{
			get { return false; }
		}

		public IPassengerAviation<IEngine> GetAvia(int peoples, int distance)
		{
			var temp = _aviation[0].Capacity >= peoples && distance <= _aviation[0].TankCapacity * 100 / _aviation[0].Engine.Consumption ? _aviation[0] : null;
			foreach (var avia in _aviation)
			{
				if (avia.Capacity >= peoples && distance <= avia.TankCapacity * 100 / avia.Engine.Consumption
					&& (temp == null || (avia.Capacity <= temp.Capacity && avia.TankCapacity <= temp.TankCapacity)))
					temp = avia;
			}
			return temp;
		}

		public void PrintAviation()
		{
			int planesCount = 0;
			int heliCount = 0;
			Console.WriteLine("Самолеты в аэропорту:");
			foreach (var avia in _aviation)
			{
				if (avia.GetType() == typeof(Plane<ITurbopropEngine>) || avia.GetType() == typeof(Plane<IReactiveEngine>))
				{
					Console.WriteLine("Самолет {0}, занято {1} мест из {2}", avia.Model, avia.Engaged, avia.Capacity);
					++planesCount;
				}
			}
			Console.WriteLine("Всего {0} шт.", planesCount);

			Console.WriteLine("Вертолеты в аэропорту:");
			foreach (var avia in _aviation)
			{
				if (avia.GetType() == typeof(Helicopter<IGasTurbineEngine>))
				{
					Console.WriteLine("Вертолет {0}, занято {1} мест из {2}", avia.Model, avia.Engaged, avia.Capacity);
					++heliCount;
				}
			}
			Console.WriteLine("Всего {0} шт.", heliCount);

		}

		public void FillAeroport(int planes, int helicopters)
		{
			AmericanAviationFactory aaf = new AmericanAviationFactory();
			RussianAviationFactory raf = new RussianAviationFactory();

			Random r = new Random();
			int americanPlanes = r.Next(planes);
			int russianHelicopters = r.Next(helicopters);

			for (int i = 0; i < planes - americanPlanes; ++i)
			{
				int reactive = r.Next(2);
				if (reactive == 1)
					_aviation.Add(raf.CreateReactivePlane());
				else
					_aviation.Add(raf.CreateTurbopropPlane());
			}

			for (int i = 0; i < americanPlanes; ++i)
			{
				int reactive = r.Next(2);
				if (reactive == 1)
					_aviation.Add(aaf.CreateReactivePlane());
				else
					_aviation.Add(aaf.CreateTurbopropPlane());
			}
			
			for (int i = 0; i < russianHelicopters; ++i)
			{
				_aviation.Add(raf.CreateHelicopter());
			}

			for (int i = 0; i < helicopters - russianHelicopters; ++i)
			{
				_aviation.Add(aaf.CreateHelicopter());
			}
		}
	}
}
