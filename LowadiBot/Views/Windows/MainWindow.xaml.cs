using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using LowadiBot.Models;
using LowadiBot.Others;
using LowadiBot.ViewModels.Pages;
using LowadiBot.Views.Pages;
using LowadiBot.Views.Windows;
using JsonConvert = Lowadi.Others.JsonConvert;

namespace LowadiBot
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void MainWindow_OnClosing(object sender, CancelEventArgs e)
        {
            var accounts = LoginPageViewModel.Accounts.Where(x => x.IsSave == true);
            // var accounts = LoginPageViewModel.Accounts;
            Files.Save(JsonConvert.Serialize(accounts));
        }

        private void MainWindow_OnLoaded(object sender, RoutedEventArgs e)
        {
            LoginPageViewModel.Accounts = Files.OpenModel<Account>();
            this.MainFrame.Content = new LoginPage();
        }
    }
}