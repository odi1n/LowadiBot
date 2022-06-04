using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using DevExpress.Mvvm;
using Lowadi;
using Lowadi.Models;
using Lowadi.Models.Type;
using LowadiBot.Models;
using LowadiBot.Views.Windows;
using Application = System.Windows.Application;
using MessageBox = System.Windows.MessageBox;

namespace LowadiBot.ViewModels.Windows
{
    internal class AuthWindowViewModel : ViewModelBase
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public Account Account { get; set; }

        public IEnumerable<ServerType> Servers { get; set; } = Enum.GetValues(typeof(ServerType))
            .Cast<ServerType>();

        public ICommand AuthCommand { get; set; }

        public AuthWindowViewModel()
        {
            Account = new Account();


            AuthCommand = new DelegateCommand(() => Auth());
        }

        public async Task Auth()
        {
            if (!Validation())
                return;

            if (await Account.Auth())
                Close();
        }

        private bool Validation()
        {
            if (string.IsNullOrEmpty(Account.Login) || string.IsNullOrEmpty(Account.Password))
            {
                MessageBox.Show("Проверьте указаны ли данные", "Внимание", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            return true;
        }

        private void Close()
        {
            Application.Current.Windows.OfType<AuthWindow>().FirstOrDefault()?.Close();
        }
    }
}