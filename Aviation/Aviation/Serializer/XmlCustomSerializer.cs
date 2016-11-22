using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Aviation.Aviation;
using Aviation.Engines;
using Aviation.Serializators;

namespace Aviation.Serializer
{
	public class XmlCustomSerializer<T> : ISerializer<T> where T : IPassengerAviation<IEngine>
	{
		private readonly XmlSerializer _xmlFormat;

		public XmlCustomSerializer()
		{
			_xmlFormat = new XmlSerializer(typeof(AeroportWrapper<T>));
		}
		public void Serialize(Aeroport<T> avia, string filePath)
		{
			var wrap = new AeroportWrapper<T>(avia);
			using (Stream fStream = new FileStream(filePath,
				FileMode.Create, FileAccess.Write, FileShare.None))
			{
				_xmlFormat.Serialize(fStream, wrap);
			}
		}

		public Aeroport<T> Deserialize(string filePath)
		{
			AeroportWrapper<T> wrap;
			using (Stream fStream = new FileStream(filePath, FileMode.Open))
			{
				wrap = (AeroportWrapper<T>)_xmlFormat.Deserialize(fStream);
			}
			var temp = wrap.Aviation.Select(avia => (T)(IPassengerAviation<IEngine>)avia).ToList();
			return new Aeroport<T>(wrap.Name, temp);
		}
	}
}
