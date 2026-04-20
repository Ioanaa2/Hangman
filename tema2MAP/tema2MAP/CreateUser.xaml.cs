using System.Windows;

namespace tema2MAP
{
    public partial class CreateUser : Window
    {
        public string? username { get; set; }
        public int index { get; set; }

        public CreateUser()
        {
            InitializeComponent();
        }

        private void ButtonOK_Click(object sender, RoutedEventArgs e)
        {
            username = Username.Text;

            if (int.TryParse(IndexImage.Text, out int result))
            {
                index = result;
                this.DialogResult = true;
            }
            else
            {
                MessageBox.Show("Introdu un număr valid pentru index!");
            }
        }
    }
}