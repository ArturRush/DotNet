using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using Aviation.Aviation;
using Aviation.Engines;
using Aviation.Serializators;

namespace Aviation.Serializer
{
	public class BinarySerializer<T> : ISerializer<T> where T : IPassengerAviation<IEngine>
	{
		private readonly BinaryFormatter _binFormat;

		public BinarySerializer()
		{
			_binFormat = new BinaryFormatter();
		}
		public void Serialize(Aeroport<T> avia, string filePath)
		{
			using (Stream fStream = new FileStream(filePath,
			   FileMode.Create, FileAccess.Write, FileShare.None))
			{
				_binFormat.Serialize(fStream, avia);
			}
		}

		public Aeroport<T> Deserialize(string filePath)
		{
			using (var fs = new FileStream(filePath, FileMode.Open))
			{
				return (Aeroport<T>)_binFormat.Deserialize(fs);
			}
		}
	}
}
