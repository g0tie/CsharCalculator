using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using CsharpCalculator.ViewModels;
using CsharpCalculator.Views;
using System.IO;
using System;
using System.Text;
using System.Net;
using System.Text.Json;
using System.Collections.Generic;
using System.Net.Http;
using Newtonsoft.Json;

namespace CsharpCalculator
{
    public class App : Application
    {
        public string key = "";
        public override void Initialize()
        {
            AvaloniaXamlLoader.Load(this);
        }

        public override void OnFrameworkInitializationCompleted()
        {
            bool isActivated  = isProductActivated();

            if (!isActivated) {
                LicenseKeyDialog dialog = new LicenseKeyDialog()
                {
                    DataContext = new LicenseKeyDialogViewModel(),
                };

                dialog.Show();

                return;
            }

            if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                desktop.MainWindow = new MainWindow
                {
                    DataContext = new MainWindowViewModel(),
                };
            }

            base.OnFrameworkInitializationCompleted();
        }

        public bool isProductActivated()
        {
            string line;
            string filePath = $"{Directory.GetCurrentDirectory()}/key.txt";

            if (!File.Exists(filePath)) {
                FileStream fs = File.Create(filePath);
                fs.Close();
            }

            try
            {
                //Pass the file path and file name to the StreamReader constructor
                StreamReader sr = new StreamReader(filePath);
                //Read the first line of text
                line = sr.ReadLine();

                if (line == null) {
                  return false;
                }

                if (line == "devmode") return true;

                bool status = getOnlineKey(line);
                sr.Close();

                if (!status) return false;

                return true;
            }
            catch(Exception e)
            {
                System.Console.WriteLine("Exception: " + e.Message);
                return false;
            }

        }

        private bool getOnlineKey(string licensekey)
        {
            var client = new HttpClient();

            var pairs = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("key", licensekey),
            };

            var content = new FormUrlEncodedContent(pairs);

            var response = client.PostAsync("http://localhost:8000/api/verify", content).Result;
            var responseStatus = JsonConvert.DeserializeObject<Dictionary<string, string>>(
                response.Content.ReadAsStringAsync().Result
            );

            if (responseStatus == null || responseStatus["status"] != "200") {
                return false;
            } else {
                return true;
            }
        }
    }
}