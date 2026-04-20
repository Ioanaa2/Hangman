using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;

namespace tema2MAP
{
    public class GameViewModel : INotifyPropertyChanged
    {
        private string _word = "";
        private string _displayWord = "";
        private int _wrongGuesses;
        private string _hangmanImage = "";

        private int _timeLeft;
        private DispatcherTimer _timer;

        private HashSet<char> _usedLetters = new();

        public string PlayerName { get; set; }
        public string SelectedCategory { get; set; }

        public ObservableCollection<LetterButton> Letters { get; set; } = new();

        public ICommand GuessCommand { get; }
        public ICommand NewGameCommand { get; }
        public ICommand ChangeCategoryCommand { get; }

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

                Level = 1;
                Streak = 0;

                StartNewGame();
            });

            InitLetters();
            StartNewGame();
        }

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

                OnPropertyChanged(nameof(HeadVisibility));
                OnPropertyChanged(nameof(BodyVisibility));
                OnPropertyChanged(nameof(LeftArmVisibility));
                OnPropertyChanged(nameof(RightArmVisibility));
                OnPropertyChanged(nameof(LeftLegVisibility));
                OnPropertyChanged(nameof(RightLegVisibility));

                UpdateImage();
            }
        }

        public string LivesDisplay => new string('X', WrongGuesses);

        public string HangmanImage
        {
            get => _hangmanImage;
            set { _hangmanImage = value; OnPropertyChanged(); }
        }

        public int TimeLeft
        {
            get => _timeLeft;
            set
            {
                _timeLeft = value;
                OnPropertyChanged();

                if (_timeLeft <= 0)
                {
                    _timer.Stop();
                    MessageBox.Show($"Ai pierdut! Cuvantul era: {_word}");
                }
            }
        }

        public Visibility HeadVisibility => WrongGuesses >= 1 ? Visibility.Visible : Visibility.Hidden;
        public Visibility BodyVisibility => WrongGuesses >= 2 ? Visibility.Visible : Visibility.Hidden;
        public Visibility LeftArmVisibility => WrongGuesses >= 3 ? Visibility.Visible : Visibility.Hidden;
        public Visibility RightArmVisibility => WrongGuesses >= 4 ? Visibility.Visible : Visibility.Hidden;
        public Visibility LeftLegVisibility => WrongGuesses >= 5 ? Visibility.Visible : Visibility.Hidden;
        public Visibility RightLegVisibility => WrongGuesses >= 6 ? Visibility.Visible : Visibility.Hidden;

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

            TimeLeft = 30;
            StartTimer();

            Level = 1;
        }

        private void StartTimer()
        {
            _timer?.Stop();

            TimeLeft = 30;

            _timer = new DispatcherTimer();
            _timer.Interval = TimeSpan.FromSeconds(1);

            _timer.Tick += (s, e) =>
            {
                TimeLeft--;

                if (TimeLeft <= 0)
                {
                    _timer.Stop();
                    MessageBox.Show($"Ai pierdut! Cuvantul era: {_word}");
                }
            };

            _timer.Start();
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
                {
                    _timer.Stop();

                    Streak++;
                    Level++;

                    if (Streak >= 3)
                    {
                        MessageBox.Show("AI CASTIGAT JOCUL (3 niveluri consecutive)!");
                        Streak = 0;
                        Level = 1;
                    }
                    else
                    {
                        MessageBox.Show("Nivel castigat!");
                        StartNewGame();
                    }
                }
            }
            else
            {
                WrongGuesses++;

                if (WrongGuesses >= _images.Length - 1)
                {
                    _timer.Stop();

                    MessageBox.Show($"Ai pierdut! Cuvantul era: {_word}");

                    Streak = 0;
                    Level = 1;
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

        private void UpdateImage()
        {
            HangmanImage = _images[WrongGuesses];
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string? name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        private int _level;
        private int _streak;
        public int Level
        {
            get => _level;
            set { _level = value; OnPropertyChanged(); }
        }

        public int Streak
        {
            get => _streak;
            set { _streak = value; OnPropertyChanged(); }
        }
    }
}