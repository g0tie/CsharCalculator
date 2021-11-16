using System;
using System.Collections.Generic;
using System.Text;

namespace CsharpCalculator.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {

        private string _currentInput = "0";

        public string CurrentInput 
        {
            get => _currentInput;
            set {
                _currentInput = value;
            }
        }
    }
}
