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

        public static void Save(string content, string path = null)
        {
            File.WriteAllText(path ?? AccFullPath, content);
        }

        public static ObservableCollection<T> OpenModel<T>(string path = null)
        {
            if (!Directory.Exists(DirectorySave) && path == null)
                Directory.CreateDirectory(DirectorySave);

            ObservableCollection<T> accounts = new ObservableCollection<T>();
            if (File.Exists(path ?? AccFullPath))
            {
                string data = File.ReadAllText(path ?? AccFullPath);
                accounts = JsonConvert.Deserialize<ObservableCollection<T>>(data);
            }

            return accounts;
        }
    }
}