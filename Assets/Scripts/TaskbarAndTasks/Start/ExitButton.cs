using UnityEngine;

namespace TaskbarAndTasks.Start
{
    public class ExitButton : MonoBehaviour
    {
        public void OnClick()
        {
            Application.Quit();
            Debug.Log("Game exit");
        }
    }
}
