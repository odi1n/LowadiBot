using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using DevExpress.Mvvm;
using LowadiBot.Infrastructure.Commands;
using LowadiBot.ViewModels.Windows.Base;

namespace LowadiBot.ViewModels.Windows
{
    internal class MainWIndowViewModel : ViewModelBase
    {
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Заголовок окна
        /// </summary>
        public string Title { get; set; } = "Howrse-Lowadi Bot";

        public bool IsMenu { get; private set; } = true;

        public IEnumerable<LanguagePrograms> GameLanguages { get; set; } =
            Enum.GetValues(typeof(LanguagePrograms)).Cast<LanguagePrograms>();

        public enum LanguagePrograms
        {
            RU, EN
        }

        public LanguagePrograms LanguageProgramss { get; set; }

        public ICommand SelectLanguage {
            get {
                return new DelegateCommand(() => {
                    if (MessageBox.Show("Для изменения языка, потребуется перезагрузка", "Внимание",
                        MessageBoxButton.OKCancel, MessageBoxImage.Information) == MessageBoxResult.Cancel)
                        return;

                    if (Properties.Settings.Default.LanguageCode == "ru-RU")
                        Properties.Settings.Default.LanguageCode = "en-US";
                    else
                        Properties.Settings.Default.LanguageCode = "ru-RU";
                    Properties.Settings.Default.Save();

                    Thread.CurrentThread.CurrentCulture = new CultureInfo(Properties.Settings.Default.LanguageCode);
                    Thread.CurrentThread.CurrentUICulture = new CultureInfo(Properties.Settings.Default.LanguageCode);

                    System.Diagnostics.Process.Start(Application.ResourceAssembly.Location);
                    Application.Current.Shutdown();
                });
            }
        }

        public ICommand CollapsedMenu {
            get {
                return new DelegateCommand(() => {
                    IsMenu = IsMenu == true ? false : true;
                });
            }
        }

        public ICommand FormMinimized =>
            new DelegateCommand(() => {
                Application.Current.MainWindow.WindowState = WindowState.Minimized;
            });

        public ICommand FormMaximized =>
            new DelegateCommand(() => {
                Application.Current.MainWindow.WindowState ^= WindowState.Maximized;
            });

        public ICommand FormClose =>
            new DelegateCommand(() => {
                Application.Current.MainWindow.Close();
            });
    }
}