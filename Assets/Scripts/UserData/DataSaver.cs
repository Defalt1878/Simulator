using System.IO;
using System.Reflection;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

namespace UserData
{
	public static class DataSaver
	{
		private static readonly string DataSavePath = Path.Combine(Application.persistentDataPath, "SaveData.dat");

		public static void SaveData()
		{
			var bf = new BinaryFormatter();
			var file = File.Create(DataSavePath);
			bf.Serialize(file, StaticData.GetInstance());
			file.Close();
			Debug.Log("Game data saved!");
		}

		public static void LoadData()
		{
			if (!File.Exists(DataSavePath))
				return;

			var bf = new BinaryFormatter();
			var file = File.Open(DataSavePath, FileMode.Open);
			var savedData = (StaticData) bf.Deserialize(file);
			var currentData = StaticData.GetInstance();
			foreach (var property in typeof(StaticData).GetProperties())
			{
				var savedValue = property.GetValue(savedData);
				if (savedValue is not null)
					property.SetValue(currentData, savedValue);
			}
		}

		public static void ResetData()
		{
			if (File.Exists(DataSavePath))
				File.Delete(DataSavePath);
			var instance = typeof(StaticData).GetField("_instance", BindingFlags.NonPublic | BindingFlags.Static);
			instance?.SetValue(null, null);
			
			StaticData.GetInstance();
		}
	}
}