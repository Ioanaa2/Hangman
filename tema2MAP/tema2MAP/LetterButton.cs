using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace tema2MAP
{
    public class LetterButton : INotifyPropertyChanged
    {
        public string Letter { get; set; } = "";

        private bool _isEnabled = true;
        public bool IsEnabled
        {
            get => _isEnabled;
            set { _isEnabled = value; OnPropertyChanged(); }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string? name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}