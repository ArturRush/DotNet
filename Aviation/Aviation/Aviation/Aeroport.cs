using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Aviation.Engines;
using Aviation.Factories;

namespace Aviation.Aviation
{
	/// <summary>
	/// Аэропорт
	/// </summary>
	class Aeroport<T> : IAeroport<T> where T : IPassengerAviation<IEngine> 
	{
		private readonly List<T> _aviation;

		public T this[int ind]
		{
			get { return _aviation[ind]; }
			set { _aviation[ind] = value; }
		}

		public Aeroport(string name)
		{
			Name = name;
			_aviation = new List<T>();
		}

		public string Name { get; private set; }

		public IEnumerator<T> GetEnumerator()
		{
			return new AeroportEnumerator<T>(this);
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return new AeroportEnumerator<T>(this);
		}

		public void Add(T item)
		{
			_aviation.Add(item);
		}

		public void Clear()
		{
			_aviation.Clear();
		}

		public bool Contains(T item)
		{
			return _aviation.Contains(item);
		}

		public void CopyTo(T[] array, int arrayIndex)
		{
			_aviation.CopyTo(array, arrayIndex);
		}

		public bool Remove(T item)
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

		public T GetAvia(int peoples, int distance)
		{
			var temp = _aviation[0].Capacity >= peoples && distance <= _aviation[0].TankCapacity * 100 / _aviation[0].Engine.Consumption ? _aviation[0] : default(T);
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

		public void Sort(Comparison<T> sorter)
		{
			_aviation.Sort(sorter);
		}

		public void DoSmth(Action<T> smth)
		{
			foreach (var avia in _aviation)
			{
				smth(avia);
			}
		}

		public int PrintSomeInfo(Func<T, int> takeInfo)
		{
			return _aviation.Sum(takeInfo);
		}
	}
}
