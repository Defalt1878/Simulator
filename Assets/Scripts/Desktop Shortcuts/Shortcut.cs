using System;
using Taskbar_And_Tasks;
using UnityEngine;

namespace Desktop_Shortcuts
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
