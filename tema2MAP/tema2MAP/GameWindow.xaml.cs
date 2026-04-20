using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace tema2MAP
{
    /// <summary>
    /// Interaction logic for GameWindow.xaml
    /// </summary>
    public partial class GameWindow : Window
    {
        public GameWindow()
        {
            InitializeComponent();
        }
        public GameWindow(string playerName, string category)
        {
            InitializeComponent();
            DataContext = new GameViewModel(playerName, category);
        }
        private void Category_Click(object sender, RoutedEventArgs e)
        {

            MenuItem clickedItem = sender as MenuItem;

            MenuItem parent = clickedItem.Parent as MenuItem;

            foreach (MenuItem item in parent.Items)
            {
                item.IsChecked = (item == clickedItem);
            }

            var viewModel = this.DataContext as MainViewModel;
            if (viewModel != null)
            {
                string categorie = clickedItem.Header.ToString();

            }
        }
    }
}
