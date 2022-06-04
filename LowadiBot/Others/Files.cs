using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using LowadiBot.ViewModels.Pages;

namespace LowadiBot.Others
{
    public class Files
    {
        private static readonly string MyDirectory = AppDomain.CurrentDomain.BaseDirectory;
        private static readonly string DirectorySave = "data";
        private static readonly string AccountPath = "accounts.json";
        private static readonly string SettingPath = "setting.json";

        private static readonly string AccFullPath =
            Path.Combine(new string[] { MyDirectory, DirectorySave, AccountPath });

        private static readonly string SettFullPath =
            Path.Combine(new string[] { MyDirectory, DirectorySave, SettingPath });

        public static void Save(string content)
        {
            File.WriteAllText(AccFullPath, content);
        }

        public static ObservableCollection<T> OpenModel<T>()
        {
            if (!Directory.Exists(DirectorySave))
                Directory.CreateDirectory(DirectorySave);

            ObservableCollection<T> accounts = new ObservableCollection<T>();
            if (File.Exists(AccFullPath))
            {
                string data = File.ReadAllText(AccFullPath);
                accounts = JsonConvert.Deserialize<ObservableCollection<T>>(data);
            }

            return accounts;
        }
    }
}