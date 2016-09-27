using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Aviation.Engines;
using Aviation.Exeptions;
using Aviation.Factories;
using Aviation.Loggers;

namespace Aviation.Aviation
{
	/// <summary>
	/// Аэропорт
	/// </summary>
	class Aeroport<T> : IAeroport<T> where T : IPassengerAviation<IEngine>
	{
		private readonly List<T> _aviation;

		public delegate void MySorter(List<T> toSort, Func<T, T, int> comparer);
		public MySorter Sorter { get; set; }
		public Func<T, T, int> Comparer { get; set; }

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
			foreach (var avia in _aviation)
			{
				Console.WriteLine("Судно {0}, мест свободно: {1}", avia.Model, avia.Capacity - avia.Engaged);
			}
		}

		public void Sort()
		{
			try
			{
				Sorter(_aviation, Comparer);
			}
			catch (UserException userEx)
			{
				ExceptionLogger.LogUserException(userEx);
			}
			catch (Exception ex)
			{
				ExceptionLogger.LogSystemException(ex);
			}
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
