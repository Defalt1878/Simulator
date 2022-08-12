using System.IO;
using System.Reflection;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

namespace UserData.DataSavers
{
	public class BinaryDataSaver : IDataSaver
	{
		private static readonly string DataSavePath = Path.Combine(Application.persistentDataPath, "SaveData.dat");

		public void SaveData()
		{
			var bf = new BinaryFormatter();
			var file = File.Create(DataSavePath);
			bf.Serialize(file, StaticData.GetInstance());
			file.Close();
		}

		public StaticData LoadData()
		{
			if (!File.Exists(DataSavePath))
				return null;

			var bf = new BinaryFormatter();
			var file = File.Open(DataSavePath, FileMode.Open);
			return (StaticData) bf.Deserialize(file);
		}

		public void ResetData()
		{
			if (File.Exists(DataSavePath))
				File.Delete(DataSavePath);
			var instance = typeof(StaticData).GetField("_instance", BindingFlags.NonPublic | BindingFlags.Static);
			instance?.SetValue(null, null);

			StaticData.GetInstance();
		}
	}
}