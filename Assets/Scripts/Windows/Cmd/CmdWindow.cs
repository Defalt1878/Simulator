namespace Windows.Cmd
{
    public class CmdWindow : Window
    {
        public override string Name => "CMD";
        public ConsoleOutput ConsoleOutput { get; private set; }
        
        private void Awake()
        {
            ConsoleOutput = GetComponentInChildren<ConsoleOutput>();
        }
    }
}
