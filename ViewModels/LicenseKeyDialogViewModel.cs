using System;
using System.Collections.Generic;
using System.Text;
using ReactiveUI;

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

        public void VerifyKey()
        {

        }
    }
}
