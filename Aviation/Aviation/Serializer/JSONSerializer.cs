using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aviation.Aviation;
using Aviation.Engines;
using Aviation.Serializators;
using Newtonsoft.Json;

namespace Aviation.Serializer
{
	public class JsonSerializer<T> : ISerializer<T> where T : IPassengerAviation<IEngine>
	{
		public void Serialize(Aeroport<T> avia, string filePath)
		{
			AeroportWrapper<T> wrap = new AeroportWrapper<T>(avia);
			var json = JsonConvert.SerializeObject(wrap, Formatting.Indented);
			File.WriteAllText(filePath, json);
		}

		public Aeroport<T> Deserialize(string filePath)
		{
			var json = File.ReadAllText(filePath);

			var wrap = JsonConvert.DeserializeObject<AeroportWrapper<T>>(json);
			return new Aeroport<T>(wrap.Name, wrap.Aviation);
		}
	}
}
