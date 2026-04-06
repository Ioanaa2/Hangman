using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using static System.Net.Mime.MediaTypeNames;

namespace tema2MAP
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string[] _images = {
        "Images/bunny.png",
        "Images/panda.png",
        "Images/ladybug.png",
        "Images/reindeer.png",
        "Images/owl.png"
        };
        private int _currentIndex = 0;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Play_Click(object sender, RoutedEventArgs e)
        {
            PlayWindow playWindow = new PlayWindow();
            playWindow.Show();
        }

        private void Prev_Click(object sender, RoutedEventArgs e)
        {
            if (_currentIndex > 0)
                _currentIndex--;
            else
                _currentIndex = _images.Length-1;
            UpdateImage();
        }

        private void Next_Click(object sender, RoutedEventArgs e)
        {
            if (_currentIndex < _images.Length - 1)
                _currentIndex++;
            else
                _currentIndex = 0; 
            UpdateImage();
        }
        private void UpdateImage()
        {
            MainImage.Source = new BitmapImage(new Uri(_images[_currentIndex], UriKind.Relative));
        }
    }
}