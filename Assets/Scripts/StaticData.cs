using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

[Serializable]
public class StaticData
{
	[NonSerialized] private static StaticData _instance;

	public static StaticData GetInstance()
	{
		if (_instance is not null)
			return _instance;
		_instance = new StaticData();
		DataSaver.LoadData();
		return _instance;
	}

	private StaticData()
	{
		Shortcuts = new List<string> {"Browser"};
		AvailableToDownloadApps = new HashSet<string>();
		ReceivedEmailsCount = 1;
	}

	public readonly List<string> Shortcuts;
	public readonly HashSet<string> AvailableToDownloadApps;
	public readonly int? ReceivedEmailsCount;
}

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
		foreach (var field in typeof(StaticData).GetFields())
		{
			var savedValue = field.GetValue(savedData);
			if (savedValue is not null)
				field.SetValue(currentData, savedValue);
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