using Windows.Cmd;

namespace Windows
{
    public class CmdWindow : Window
    {
        public ConsoleOutput ConsoleOutput { get; private set; }

        private void Awake()
        {
            ConsoleOutput = GetComponentInChildren<ConsoleOutput>();
        }
    }
}
