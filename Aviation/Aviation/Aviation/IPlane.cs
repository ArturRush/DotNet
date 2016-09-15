using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aviation.Engines;

namespace Aviation.Aviation
{
	interface IPlane<out T> where T: IPlaneEngine
	{
	}
}
