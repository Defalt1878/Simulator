namespace UserData.DataSavers
{
	public interface IDataSaver
	{
		void SaveData();
		StaticData LoadData();
		void ResetData();
	}
}