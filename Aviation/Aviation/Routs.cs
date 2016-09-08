using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Aviation
{
	/// <summary>
	/// Маршруты полетов
	/// </summary>
	public class Routs
	{
		/// <summary>
		/// Список городов
		/// </summary>
		public enum Cities
		{
			Moscow,
			Omsk,
			Kazan,
			Denver,
			Cherlak
		}

		private static List<List<int>> distances = new List<List<int>>()
		{
			new List<int>(){0,3000,1100,8000,2000},
			new List<int>(){3000,0,2000,11000,100},
			new List<int>(){1100,2000,0,9100,900},
			new List<int>(){8000,11000,9100,0,10000},
			new List<int>(){2000,100,900,10000,0}
		};

		/// <summary>
		/// Расстояние между городами
		/// </summary>
		/// <param name="from">Откуда</param>
		/// <param name="to">Куда</param>
		/// <returns>Расстояние</returns>
		public static int GetDistance(Cities from, Cities to)
		{
			return distances[(int)from][(int)to];
		}
	}
}
