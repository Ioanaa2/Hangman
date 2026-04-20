using System.Windows;

namespace tema2MAP
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Play_Click(object sender, RoutedEventArgs e)
        {
            GameWindow playWindow = new GameWindow();
            playWindow.Show();
        }
    }
}