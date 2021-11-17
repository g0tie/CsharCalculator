using System;
using System.Collections.Generic;
using System.Text;
using ReactiveUI;

namespace CsharpCalculator.ViewModels
{
    public class MainWindowViewModel : ReactiveObject
    {

        private string _currentInput = "0";

        public string CurrentInput 
        {
            get => _currentInput;
            set {
                _currentInput = this.RaiseAndSetIfChanged(ref _currentInput, value);
            }
        }

        public void AddCharacterToDisplay(string character)
        {
            // if ( CurrentInput[CurrentInput.Length - 1 ] ) {
            //     CurrentInput +=  character;
            // }
        }
    }
}
