using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aviation.Engines;

namespace Aviation.Aviation
{
	interface IHelicopter<out T> : IPassengerAviation<IEngine> where T: IHelicopterEngine
	{
	}
}
