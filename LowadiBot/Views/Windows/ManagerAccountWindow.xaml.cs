using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;

namespace LowadiBot.Views.Windows
{
    public partial class ManagerAccountWindow : Window
    {
        public ManagerAccountWindow()
        {
            InitializeComponent();
        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
    }
}