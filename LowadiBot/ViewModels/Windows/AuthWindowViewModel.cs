using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using DevExpress.Mvvm;
using LowadiBot.ViewModels.Pages;
using LowadiBot.ViewModels.Windows.Base;

namespace LowadiBot.ViewModels.Windows
{
    internal class AuthWindowViewModel : ViewModelBase
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public Account Account { get; set; }

        public IEnumerable<Server> Servers { get; set; } =
            Enum.GetValues(typeof(Server))
                .Cast<Server>();

        public AuthWindowViewModel()
        {
            this.Account = new Account();
        }
    }

    public enum Server
    {
        [Description("Русский")] RU,
        [Description("English")] EN
    }
}