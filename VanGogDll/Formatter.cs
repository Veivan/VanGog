using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml.Serialization;

namespace VanGogDll
{
	class Formatter
	{
		Constants.Serial type;
		String pathDat = @"c:\temp\dpack.dat";
		String pathXml = @"c:\temp\dpack.xml";

		public Formatter(Constants.Serial _type)
		{
			type = _type;
		}

		internal void Save2file(DataPack dPack)
		{
			switch (type)
			{
				case Constants.Serial.sXML:
					{
						var formatter = new XmlSerializer(typeof(DataPack));
						using (var fs = new FileStream(pathXml, FileMode.OpenOrCreate)) 
						{
							formatter.Serialize(fs, dPack);
						}
						break;
					}
				default:
					{
						var formatter = new BinaryFormatter();
						using (FileStream fs = new FileStream(pathDat, FileMode.OpenOrCreate))
						{
							formatter.Serialize(fs, dPack);
						}
						break;
					}
			}
		}
		internal DataPack RestoreFromfile()
		{
			DataPack dPack;
			switch (type)
			{
				case Constants.Serial.sXML:
					{
						var formatter = new XmlSerializer(typeof(DataPack));
						using (var fs = new FileStream(pathXml, FileMode.OpenOrCreate))
						{
							dPack = (DataPack)formatter.Deserialize(fs);
						}
						break;
					}
				default:
					{
						var formatter = new BinaryFormatter();
						using (var fs = new FileStream(pathDat, FileMode.OpenOrCreate))
						{
							dPack = (DataPack)formatter.Deserialize(fs);
						}
						break;
					}
			}
			return dPack;
		}
	}
}
