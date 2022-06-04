using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Input;
using DevExpress.Mvvm;
using LowadiBot.Models;
using LowadiBot.Others;
using LowadiBot.ViewModels.Windows;
using LowadiBot.Views.Windows;
using MessageBox = System.Windows.MessageBox;
using OpenFileDialog = Microsoft.Win32.OpenFileDialog;

namespace LowadiBot.ViewModels.Pages
{
    internal class LoginPageViewModel : ViewModelBase
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public static ObservableCollection<Account> Accounts { get; set; } = new ObservableCollection<Account>() {
            new Account() { Login = "123", Password = "123" }
        };

        public ICommand AdditionAccountCommand { get; set; }
        public ICommand DeleteAccountCommand { get; set; }
        public ICommand AuthAccountsCommand { get; set; }
        public ICommand SaveAccountsCommand { get; set; }
        public ICommand LoadAccountsCommand { get; set; }
        public ICommand<Account> ManagerAccountCommand { get; set; }

        public LoginPageViewModel()
        {
            AdditionAccountCommand = new DelegateCommand(AdditionAccount);
            DeleteAccountCommand = new DelegateCommand(DeleteAccount);
            AuthAccountsCommand = new AsyncCommand(() => AuthAccounts());
            SaveAccountsCommand = new DelegateCommand(SaveAccounts);
            LoadAccountsCommand = new DelegateCommand(LoadAccounts);
            ManagerAccountCommand = new DelegateCommand<Account>(x => ManagerAccount(x));
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

        private void SaveAccounts()
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = ".json|*.json";
            sfd.FilterIndex = 2;
            sfd.RestoreDirectory = true;
            sfd.InitialDirectory = AppDomain.CurrentDomain.BaseDirectory;
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                var accounts = Accounts.Where(x => x.IsSave == true);
                Files.Save(JsonConvert.Serialize(accounts), sfd.FileName);
            }
        }

        private void LoadAccounts()
        {
            OpenFileDialog opf = new OpenFileDialog();
            opf.Filter = ".json|*.json";
            opf.FilterIndex = 2;
            opf.RestoreDirectory = true;
            opf.InitialDirectory = AppDomain.CurrentDomain.BaseDirectory;
            if (opf.ShowDialog() == true)
            {
                var accounts = Files.OpenModel<Account>(opf.FileName);

                Accounts.Clear();
                foreach (Account account in accounts)
                    Accounts.Add(account);
            }
        }

        private void ManagerAccount(Account account)
        {
            if (account.LowadiApi == null)
            {
                MessageBox.Show("Аккаунт не авторизован. Нужно выбрать аккаунт и авторизовать его",
                    "Информация", MessageBoxButton.OK, MessageBoxImage.Information, MessageBoxResult.OK);
                return;
            }

            ManagerAccountWindowViewModel managerAccountWindowViewModel = new ManagerAccountWindowViewModel(account);
            ManagerAccountWindow managerAccountWindow = new ManagerAccountWindow() {
                DataContext = managerAccountWindowViewModel,
            };
            managerAccountWindow.ShowDialog();
        }
    }
}