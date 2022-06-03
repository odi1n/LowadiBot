using System;
using System.Collections.Generic;
using System.IO;
using LowadiBot.ViewModels.Pages;

namespace LowadiBot.Others
{
    public class Files
    {
        private static readonly string MyDirectory = AppDomain.CurrentDomain.BaseDirectory;
        private static readonly string DirectorySave = "data";
        private static readonly string AccountPath = "accounts.txt";
        private static readonly string SettingPath = "setting.txt";

        private static readonly string AccFullPath =
            Path.Combine(new string[] { MyDirectory, DirectorySave, AccountPath });

        private static readonly string SettFullPath =
            Path.Combine(new string[] { MyDirectory, DirectorySave, SettingPath });

        public Files()
        {
            if (!Directory.Exists(DirectorySave))
                Directory.CreateDirectory(DirectorySave);
        }

        public static void Save(string content)
        {
            File.WriteAllText(AccFullPath, content);
        }

        public static List<T> OpenModel<T>()
        {
            List<T> accounts = new List<T>();
            if (File.Exists(AccFullPath))
            {
                string data = File.ReadAllText(AccFullPath);
                accounts = JsonConvert.Deserialize<List<T>>(data);
            }

            return accounts;
        }
    }
}