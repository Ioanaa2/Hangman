using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace tema2MAP
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private int _currentIndex;
        private string? _selectedUser;
        private string _currentImage = string.Empty;

        public ObservableCollection<string> Users { get; set; }

        private string _selectedCategory = "All Categories";
        public List<string> Categories { get; set; } = new()
        {
            "All Categories",
            "Animals",
            "Fruits",
            "Countries"
        };
        public string SelectedCategory
        {
            get => _selectedCategory;
            set { _selectedCategory = value; OnPropertyChanged(); }
        }

        public string? SelectedUser
        {
            get => _selectedUser;
            set
            {
                _selectedUser = value;
                OnPropertyChanged();
                SyncImageWithSelection();
            }
        }

        public string CurrentImage
        {
            get => _currentImage;
            set
            {
                _currentImage = value;
                OnPropertyChanged();
            }
        }

        public ICommand NextCommand { get; }
        public ICommand PrevCommand { get; }
        public ICommand NewUserCommand { get; }
        public ICommand DeleteUserCommand { get; }
        public ICommand PlayCommand { get; }

        private readonly string[] _images =
        {
            "/Images/bunny.png",
            "/Images/panda.png",
            "/Images/ladybug.png",
            "/Images/reindeer.png",
            "/Images/owl.png"
        };

        public MainViewModel()
        {
            Users = new ObservableCollection<string>
            {
                "Cute Bunny",
                "Cute Panda",
                "Lucky Ladybug",
                "Strong Reindeer",
                "Smart Owl"
            };

            _currentImage = _images[0];

            NextCommand = new RelayCommand(_ => NextImage());
            PrevCommand = new RelayCommand(_ => PrevImage());
            NewUserCommand = new RelayCommand(_ => AddUser());
            DeleteUserCommand = new RelayCommand(_ => DeleteUser());
            PlayCommand = new RelayCommand(_ => StartGame());
        }

        private void NextImage()
        {
            _currentIndex = (_currentIndex + 1) % _images.Length;
            CurrentImage = _images[_currentIndex];
        }

        private void PrevImage()
        {
            _currentIndex = (_currentIndex - 1 + _images.Length) % _images.Length;
            CurrentImage = _images[_currentIndex];
        }

        private void SyncImageWithSelection()
        {
            if (SelectedUser == null) return;

            _currentIndex = Users.IndexOf(SelectedUser);

            if (_currentIndex >= 0)
                CurrentImage = _images[_currentIndex];
        }

        private void AddUser()
        {
            CreateUser window = new CreateUser();

            if (window.ShowDialog() == true)
            {
                string numeIntrodus = window.username;
                int indexIntrodus = window.index;
                if (!string.IsNullOrEmpty(numeIntrodus))
                {
                    Users.Add(numeIntrodus);
                }
            }
        }

        private void DeleteUser()
        {
            if (SelectedUser == null)
                return;

            Users.Remove(SelectedUser);
            SelectedUser = null;
        }

        private void StartGame()
        {
            if (SelectedUser == null)
                return;

            GameWindow window = new GameWindow(SelectedUser, "All categories");
            window.ShowDialog();
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string? name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}