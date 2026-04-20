using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Windows;

namespace tema2MAP
{
    public partial class StatisticsWindow : Window
    {
        public StatisticsWindow()
        {
            InitializeComponent();
            LoadData();
        }

        private void LoadData()
        {
            if (File.Exists("statistics.json"))
            {
                string json = File.ReadAllText("statistics.json");
                var stats = JsonSerializer.Deserialize<List<Player>>(json);
                StatsGrid.ItemsSource = stats;
            }
            else
            {
                MessageBox.Show("Nu există statistici înregistrate încă.");
            }
        }
    }
}