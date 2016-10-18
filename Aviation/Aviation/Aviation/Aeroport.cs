  using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Aviation.Engines;
using Aviation.Exeptions;
using Aviation.Factories;
using Aviation.Loggers;

namespace Aviation.Aviation
{
	/// <summary>
	/// Аэропорт
	/// </summary>
	[Serializable]
	public class Aeroport<T> : IAeroport<T> where T : IPassengerAviation<IEngine>
	{
		public readonly List<T> Aviation;

		public delegate void MySorter(List<T> toSort, Func<T, T, int> comparer);
		public MySorter Sorter { get; set; }
		public Func<T, T, int> Comparer { get; set; }
		public Action<int> Progressor { get; set; }
		public T this[int ind]
		{
			get { return Aviation[ind]; }
			set { Aviation[ind] = value; }
		}

		public Aeroport(string name)
		{
			Name = name;
			SortProgress = 0;
			Aviation = new List<T>();
		}

		public Aeroport(string name, List<T> aviation)
		{
			Name = name;
			SortProgress = 0;
			Aviation = aviation;
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
			Aviation.Add(item);
		}

		public void Clear()
		{
			Aviation.Clear();
		}

		public bool Contains(T item)
		{
			return Aviation.Contains(item);
		}

		public void CopyTo(T[] array, int arrayIndex)
		{
			Aviation.CopyTo(array, arrayIndex);
		}

		public bool Remove(T item)
		{
			return Aviation.Remove(item);
		}

		public int Count
		{
			get { return Aviation.Count; }
		}

		public bool IsReadOnly
		{
			get { return false; }
		}

		public T GetAvia(int peoples, int distance)
		{
			var temp = Aviation[0].Capacity >= peoples && distance <= Aviation[0].TankCapacity * 100 / Aviation[0].Engine.Consumption ? Aviation[0] : default(T);
			foreach (var avia in Aviation)
			{
				if (avia.Capacity >= peoples && distance <= avia.TankCapacity * 100 / avia.Engine.Consumption
					&& (temp == null || (avia.Capacity <= temp.Capacity && avia.TankCapacity <= temp.TankCapacity)))
					temp = avia;
			}
			return temp;
		}

		public void PrintAviation()
		{
			foreach (var avia in Aviation)
			{
				Console.WriteLine("Судно {0}, мест свободно: {1}", avia.Model, avia.Capacity - avia.Engaged);
			}
		}

		public void Sort()
		{
			try
			{
				Sorter(Aviation, Comparer);
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
			foreach (var avia in Aviation)
			{
				smth(avia);
			}
		}

		public int PrintSomeInfo(Func<T, int> takeInfo)
		{
			return Aviation.Sum(takeInfo);
		}

		public int SortProgress { get; private set; }

		public async Task SortAsynk()
		{
			await Task.Run(() =>
			{
				for (int i = 0; i < Aviation.Count; ++i)
				{
					for (int j = 0; j < Aviation.Count; ++j)
					{
						if (Comparer(Aviation[i], Aviation[j]) > 0)
							Swap(i, j);
					}
					SortProgress = i * 100 / Aviation.Count;
					if (Progressor != null)
						Progressor(SortProgress);
				}
				if (Progressor != null)
					Progressor(100);
				Console.WriteLine("\nСортировка завершена");
				SortProgress = 0;
			});
		}
		/// <summary>
		/// Обмен пары элементов коллекции местами
		/// </summary>
		/// <param name="i">Первый элемент</param>
		/// <param name="j">Второй элемент</param>
		private void Swap(int i, int j)
		{
			T temp = Aviation[i];
			Aviation[i] = Aviation[j];
			Aviation[j] = temp;
		}
	}
}
