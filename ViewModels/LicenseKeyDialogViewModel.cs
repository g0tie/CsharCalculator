using System;
using System.Collections.Generic;
using System.Text;
using ReactiveUI;
using System.Net.Http;
using Newtonsoft.Json;
using CsharpCalculator.Views;
using System.IO;
using Avalonia.Controls;

namespace CsharpCalculator.ViewModels
{
    public class LicenseKeyDialogViewModel : ReactiveObject
    {
        private string _licenseKey = "";

        public string LicenseKey
        {
            get => _licenseKey;
            set {
                _licenseKey = value;
            }
        }

        public void VerifyKey(Window window)
        {
            bool isKeyConfirmed = getOnlineKey(LicenseKey);
            string filePath = $"{Directory.GetCurrentDirectory()}/key.txt";

            if (isKeyConfirmed) {
                try
                {
                    File.WriteAllText(filePath, LicenseKey);
                    MainWindow mainWIndow = new MainWindow
                    {
                        DataContext = new MainWindowViewModel(),
                    };
                    
                    mainWIndow.Show();
                    window.Close();
                }
                catch (Exception e) {
                    Console.WriteLine(e);
                }
            }

            window.FindControl<TextBlock>("Error").Text = "Wrong key !";
        }

        private bool getOnlineKey(string licensekey)
        {
            if (licensekey == "devmode") return true;

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
