using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace tema2MAP
{
    public partial class GameWindow : Window
    {
        public GameWindow(string player, string category)
        {
            InitializeComponent();
            DataContext = new GameViewModel(player, category);
        }

        public GameWindow()
        {
            InitializeComponent();
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (DataContext is GameViewModel vm)
            {
                if (e.Key >= Key.A && e.Key <= Key.Z)
                {
                    char c = (char)('A' + (e.Key - Key.A));

                    var letter = vm.Letters.FirstOrDefault(l => l.Letter == c.ToString());

                    if (letter != null && letter.IsEnabled)
                        vm.GuessCommand.Execute(letter);
                }
            }
        }
        private void OpenStatistics_Click(object sender, RoutedEventArgs e)
        {
            StatisticsWindow statsWin = new StatisticsWindow();
            statsWin.ShowDialog();
        }
    }
}