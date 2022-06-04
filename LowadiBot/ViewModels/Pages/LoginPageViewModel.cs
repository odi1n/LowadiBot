using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using DevExpress.Mvvm;
using LowadiBot.Models;
using LowadiBot.Others;
using LowadiBot.ViewModels.Windows;
using LowadiBot.Views.Windows;

namespace LowadiBot.ViewModels.Pages
{
    internal class LoginPageViewModel : ViewModelBase
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public static ObservableCollection<Account> Accounts { get; set; }

        public ICommand AdditionAccountCommand { get; set; }
        public ICommand DeleteAccountCommand { get; set; }
        public ICommand AuthAccountsCommand { get; set; }

        public LoginPageViewModel()
        {
            AdditionAccountCommand = new DelegateCommand(AdditionAccount);
            DeleteAccountCommand = new DelegateCommand(DeleteAccount);
            AuthAccountsCommand = new AsyncCommand(() => AuthAccounts());
        }

        private void AdditionAccount()
        {
            AuthWindow authWindow = new AuthWindow();
            authWindow.ShowDialog();

            var data = (AuthWindowViewModel)authWindow.DataContext;
            if (data.Account.LowadiApi == null)
                return;
            if (Accounts.FirstOrDefault(x => x.Login == data.Account.Login) != null)
            {
                MessageBox.Show("Такой аккаунт уже есть", "Внимание", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            Accounts.Add(data.Account);
        }

        private void DeleteAccount()
        {
            Accounts.Remove(x => x.IsSelected);
        }

        private async Task AuthAccounts()
        {
            foreach (Account account in Accounts.Where(x => x.IsSelected))
            {
                await account.Auth();
            }
        }
    }
}