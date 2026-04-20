using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;

namespace tema2MAP
{
    public class GameViewModel : INotifyPropertyChanged
    {
        private string _word = "";
        private string _displayWord = "";
        private int _wrongGuesses;
        public ICommand ChangeCategoryCommand { get; }
       

        private HashSet<char> _usedLetters = new();

        public string PlayerName { get; set; }
        public string SelectedCategory { get; set; }

        public ObservableCollection<LetterButton> Letters { get; set; }

        public string DisplayWord
        {
            get => _displayWord;
            set { _displayWord = value; OnPropertyChanged(); }
        }

        public int WrongGuesses
        {
            get => _wrongGuesses;
            set
            {
                _wrongGuesses = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(LivesDisplay));
                UpdateImage();
            }
        }

        public string LivesDisplay => new string('X', WrongGuesses);

        private string _hangmanImage = "";
        public string HangmanImage
        {
            get => _hangmanImage;
            set { _hangmanImage = value; OnPropertyChanged(); }
        }

        public ICommand GuessCommand { get; }
        public ICommand NewGameCommand { get; }

        private readonly string[] _images =
        {
            "/Images/0.png",
            "/Images/1.png",
            "/Images/2.png",
            "/Images/3.png",
            "/Images/4.png",
            "/Images/5.png",
            "/Images/6.png"
        };

        public GameViewModel(string playerName, string category)
        {
            PlayerName = playerName;
            SelectedCategory = category;

            GuessCommand = new RelayCommand(param => Guess(param));
            NewGameCommand = new RelayCommand(_ => StartNewGame());

            ChangeCategoryCommand = new RelayCommand(param =>
            {
                if (param == null) return;

                SelectedCategory = param.ToString()!;
                OnPropertyChanged(nameof(SelectedCategory));
                StartNewGame();
            });

            InitLetters();
            StartNewGame();
        }

        private void InitLetters()
        {
            Letters = new ObservableCollection<LetterButton>(
                "ABCDEFGHIJKLMNOPQRSTUVWXYZ"
                .Select(c => new LetterButton { Letter = c.ToString() })
            );
        }

        private void StartNewGame()
        {
            var data = WordProvider.LoadWords();

            List<string> words = SelectedCategory == "All categories"
                ? data.Values.SelectMany(x => x).ToList()
                : data[SelectedCategory];

            _word = WordProvider.GetRandomWord(words).ToLower();

            DisplayWord = string.Join(" ", _word.Select(_ => "_"));

            _usedLetters.Clear();
            WrongGuesses = 0;

            foreach (var l in Letters)
                l.IsEnabled = true;
        }

        private void Guess(object? param)
        {
            if (param is not LetterButton letterBtn) return;

            char letter = letterBtn.Letter.ToLower()[0];

            letterBtn.IsEnabled = false;

            if (_usedLetters.Contains(letter))
                return;

            _usedLetters.Add(letter);

            if (_word.Contains(letter))
            {
                UpdateDisplayWord(letter);

                if (!DisplayWord.Contains("_"))
                    MessageBox.Show("Ai castigat!");
            }
            else
            {
                WrongGuesses++;

                if (WrongGuesses >= _images.Length - 1)
                    MessageBox.Show($"Ai pierdut! Cuvantul era: {_word}");
            }
        }

        private void UpdateDisplayWord(char letter)
        {
            var display = DisplayWord.Split(' ').ToArray();

            for (int i = 0; i < _word.Length; i++)
            {
                if (_word[i] == letter)
                    display[i] = letter.ToString();
            }

            DisplayWord = string.Join(" ", display);
        }

        private void UpdateImage()
        {
            HangmanImage = _images[WrongGuesses];
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string? name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}