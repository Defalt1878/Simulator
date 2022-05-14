using UnityEngine;

namespace TaskbarAndTasks.Start
{
    public class SaveButton : MonoBehaviour
    {
        public void OnClick()
        {
            DataSaver.SaveData();
            Debug.Log("Game saved");
        }
    }
}
