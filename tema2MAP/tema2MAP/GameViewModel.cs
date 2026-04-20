using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace tema2MAP
{
    public class GameViewModel : INotifyPropertyChanged
    {
        private string _word = "";
        private string _displayWord = "";
        private int _lives = 6;

        private HashSet<char> _usedLetters = new();

        private string _playerName = "";

        public string PlayerName
        {
            get => _playerName;
            set
            {
                _playerName = value;
                OnPropertyChanged();
            }
        }
        public GameViewModel(string playerName, string category = "All categories")
        {
            PlayerName = playerName;
            SelectedCategory = category;

            GuessCommand = new RelayCommand(param => Guess(param));
            NewGameCommand = new RelayCommand(_ => StartNewGame());

            StartNewGame();
        }
        public string DisplayWord
        {
            get => _displayWord;
            set { _displayWord = value; OnPropertyChanged(); }
        }

        public int Lives
        {
            get => _lives;
            set { _lives = value; OnPropertyChanged(); }
        }

        public string SelectedCategory { get; set; }

        public ICommand GuessCommand { get; }
        public ICommand NewGameCommand { get; }

        public GameViewModel(string category = "All categories")
        {
            SelectedCategory = category;

            GuessCommand = new RelayCommand(param => Guess(param));
            NewGameCommand = new RelayCommand(_ => StartNewGame());

            StartNewGame();
        }

        private void StartNewGame()
        {
            var data = WordProvider.LoadWords();

            List<string> words;

            if (SelectedCategory == "All categories")
                words = data.Values.SelectMany(x => x).ToList();
            else
                words = data[SelectedCategory];

            _word = WordProvider.GetRandomWord(words).ToLower();

            _usedLetters.Clear();
            Lives = 6;

            DisplayWord = string.Join(" ", _word.Select(_ => "_"));
        }

        private void Guess(object? param)
        {
            if (param == null) return;

            char letter = param.ToString()!.ToLower()[0];

            if (_usedLetters.Contains(letter))
                return;

            _usedLetters.Add(letter);

            if (_word.Contains(letter))
            {
                UpdateDisplayWord(letter);

                if (!DisplayWord.Contains("_"))
                {
                    System.Windows.MessageBox.Show("Ai castigat!");
                }
            }
            else
            {
                Lives--;

                if (Lives == 0)
                {
                    System.Windows.MessageBox.Show($"Ai pierdut! Cuvantul era: {_word}");
                }
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

        public event PropertyChangedEventHandler? PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string? name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}