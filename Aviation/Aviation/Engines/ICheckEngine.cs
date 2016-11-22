using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aviation.Engines
{
	interface ICheckEngine<in T> where T: IEngine
	{
		void Check(T obj);
	}
}
