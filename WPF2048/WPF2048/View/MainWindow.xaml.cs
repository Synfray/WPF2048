using System.Windows;
using System.Windows.Input;
using WPF2048.Assets;
using WPF2048.Module;

namespace WPF2048.View
{
    /// <summary>
    ///     Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();

            EventManager.RegisterClassHandler(typeof(Window),
                Keyboard.KeyUpEvent, new KeyEventHandler(KeyEventHandler), true);
        }

        private static void KeyEventHandler(object sender, KeyEventArgs keyEventArgs)
        {
            switch (keyEventArgs.Key)
            {
                case Key.Left:
                case Key.A:
                    Singleton.SpielfeldViewModel.KeyAction(Direction.Left);
                    break;
                case Key.Up:
                case Key.W:
                    Singleton.SpielfeldViewModel.KeyAction(Direction.Up);
                    break;
                case Key.Right:
                case Key.D:
                    Singleton.SpielfeldViewModel.KeyAction(Direction.Right);
                    break;
                case Key.Down:
                case Key.S:
                    Singleton.SpielfeldViewModel.KeyAction(Direction.Down);
                    break;
            }
        }
    }
}