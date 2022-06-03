using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using DevExpress.Mvvm;
using LowadiBot.ViewModels.Windows.Base;
using Lowadi;
using LowadiBot.Infrastructure.Commands;
using LowadiBot.ViewModels.Windows;
using LowadiBot.Views.Windows;

namespace LowadiBot.ViewModels.Pages
{
    public class Account
    {
        public string Login { get; set; }
        public string Password { get; set; }
        public bool IsSave { get; set; }
        public bool IsProxy { get; set; }
        public string Proxy { get; set; } = string.Empty;
        public string ProxyLogin { get; set; } = string.Empty;
        public string ProxyPassword { get; set; } = string.Empty;
        public Server Server { get; set; }
        public bool IsSelected { get; set; }
    }

    internal class LoginPageViewModel : ViewModelBase
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public ICommand AdditioAccount { get; set; }
        public ICommand DeleteAccount { get; set; }

        public LoginPageViewModel()
        {
            AdditioAccount = new LambdaCommand(() => { AccountAuth(); });
        }

        public void AccountAuth()
        {
            AuthWindow authWindow = new AuthWindow();
            authWindow.ShowDialog();

            var test = (AuthWindowViewModel)authWindow.DataContext;
        }

        public static ICollection<Account> Accounts { get; private set; } = new ObservableCollection<Account>() {
            new Account() {
                Login = "Test 1",
                Password = "test 1",
                IsSave = true,
                IsProxy = true,
                Proxy = "123.123.12.12:8080",
                ProxyLogin = "ProxyLogin",
                ProxyPassword = "ProxyPass"
            },
            new Account() {
                Login = "Test 1", Password = "test 1", IsSave = false, IsProxy = false,
            },
        };
    }
}