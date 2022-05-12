using System;
using TaskbarAndTasks;
using UnityEngine;

namespace DesktopShortcuts
{
    public class Shortcut : MonoBehaviour
    {
        private protected Task Task;
        
        public void OnClick()
        {
            if (FindObjectOfType(typeof(TaskBar)) is not TaskBar taskBar)
                throw new NullReferenceException("TaskBarNotFound");

            taskBar.AddOrExpandTask(Task);
        }
    }
}
