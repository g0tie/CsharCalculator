using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using ReactiveUI;
using System.Text.RegularExpressions;
using MatchCollection = System.Text.RegularExpressions.MatchCollection ;  
using Match = System.Text.RegularExpressions.Match;


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
            
            int nb = 0;
            
            int lastIndex = CurrentInput.Length - 1;
            char lastCharacter = CurrentInput[lastIndex];
            bool isLastCharNumber = int.TryParse(lastCharacter.ToString(), out nb);
            bool isCharToAddNumber = int.TryParse(character.ToString(), out nb);

            if (!isLastCharNumber && !isCharToAddNumber) return;
            
            CurrentInput += character;
        }

        public void ExecuteCalculation()
        {		
            string[] calculationMembers = Regex.Split(CurrentInput,  @"[-+*\/()]");
            MatchCollection operationsMatches = Regex.Matches(CurrentInput, @"[-+*\/()]");
            var operationTypes = operationsMatches.Cast<Match>().Select(match => match.Value).ToList();

            float result = float.Parse(calculationMembers[0]);

            for (int i = 0; i < calculationMembers.Length; i++) {

                if (i < operationTypes.Count) {

                    switch (operationTypes[i]) {
                        case "+":
                            if (Regex.IsMatch(calculationMembers[i + 1], @"\," ))
                            {
                                result += float.Parse(calculationMembers[i + 1]);
                            } else {
                                result += int.Parse(calculationMembers[i + 1]);
                            }
                        break;

                        case "-":
                        if (Regex.IsMatch(calculationMembers[i + 1], @"\," ))
                            {
                                result -= float.Parse(calculationMembers[i + 1]);
                            } else {
                                result -= int.Parse(calculationMembers[i + 1]);
                            }
                        break;

                        case "*":
                            if (Regex.IsMatch(calculationMembers[i + 1], @"\," ))
                            {
                                result *= float.Parse(calculationMembers[i + 1]);
                            } else {
                                result *= int.Parse(calculationMembers[i + 1]);
                            }
                        break;

                        case "/":
                            if (Regex.IsMatch(calculationMembers[i + 1], @"\," ))
                            {
                                result /= float.Parse(calculationMembers[i + 1]);
                            } else {
                                result /= int.Parse(calculationMembers[i + 1]);
                            }
                        break;
                    }
                }

            }

            CurrentInput = result.ToString();
        }


        public void ResetCalculation()
        {
            CurrentInput = "0";
        }

        public void GoBack()
        {
            CurrentInput = CurrentInput.Remove(CurrentInput.Length - 1);
        }

    }
}
