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

            // interdire d'ajouter un signe après un autre 
            if (lastCharacter != ')' && !isLastCharNumber && !isCharToAddNumber) return;

            CurrentInput += character;
        }

        public void ExecuteCalculation()
        {		
            string[] calculationMembers = Regex.Split(CurrentInput,  @"[\+\*\/]|(?<!\()\-"); //Filtrer uniquement les nombres

            Console.WriteLine(calculationMembers[0]);

            for (int i = 0; i < calculationMembers.Length; i++) {

                if (Regex.IsMatch(calculationMembers[i], @"()")) {
                    calculationMembers[i] = parenthesisRemove(calculationMembers[i]);
                }
            }
            Console.WriteLine(calculationMembers[0]);

            MatchCollection operationsMatches = Regex.Matches(CurrentInput, @"[\+\*\/]|(?<!\()\-"); //Filtrer uniquement les signes
            var operationTypes = operationsMatches.Cast<Match>().Select(match => match.Value).ToList(); //Convertir les signes en string

            double result = double.Parse(calculationMembers[0]);

            for (int i = 0; i < calculationMembers.Length; i++) {

                if (i < operationTypes.Count) {

                    switch (operationTypes[i]) {
                        case "+":

                            // If number to add is a double, convert, else keep int
                            if (Regex.IsMatch(calculationMembers[i + 1], @"\," ))
                            {
                                result += double.Parse(calculationMembers[i + 1]);
                            } else {
                                result += int.Parse(calculationMembers[i + 1]);
                            }
                        break;

                        case "-":
                        if (Regex.IsMatch(calculationMembers[i + 1], @"\," ))
                            {
                                result -= double.Parse(calculationMembers[i + 1]);
                            } else {
                                result -= int.Parse(calculationMembers[i + 1]);
                            }
                        break;

                        case "*":
                            if (Regex.IsMatch(calculationMembers[i + 1], @"\," ))
                            {
                                result *= double.Parse(calculationMembers[i + 1]);
                            } else {
                                result *= int.Parse(calculationMembers[i + 1]);
                            }
                        break;

                        case "/":
                            if (Regex.IsMatch(calculationMembers[i + 1], @"\," ))
                            {
                                result /= double.Parse(calculationMembers[i + 1]);
                            } else {
                                result /= int.Parse(calculationMembers[i + 1]);
                            }
                        break;
                    }
                }

            }

            CurrentInput = Math.Round(result,3).ToString();

            if (Regex.IsMatch(CurrentInput, @"[\-]")) CurrentInput = $"({CurrentInput})"; 
        }


        public void ResetCalculation()
        {
            CurrentInput = "0";
        }

        public void GoBack()
        {
            CurrentInput = CurrentInput.Remove(CurrentInput.Length - 1);
        }

        public void InvertSign()
        {
            string[] calculationMembers = Regex.Split(CurrentInput, @"[\+\*\/]|(?<!\()\-"); //Get string as array without sign operators

            // Get last member
            var lastCalcMember = calculationMembers[calculationMembers.Length - 1];
            int lastMemberLen = lastCalcMember.Length;
            
            // check if last member in correct format
            if (Regex.IsMatch(CurrentInput[CurrentInput.Length - 1].ToString(), @"[\+\*\/]|(?<!\()\-")) return;
            if (Regex.IsMatch(lastCalcMember, @"[\-]")) lastCalcMember = parenthesisRemove(lastCalcMember);

            int invertedLastMember = int.Parse(lastCalcMember) * (-1);
            string invertedLastMemberFormatted = "";


            //Get string version of inverted last calculation member 
            if (invertedLastMember < 0) {
                invertedLastMemberFormatted = $"({invertedLastMember.ToString()})";
            } else {
                invertedLastMemberFormatted = invertedLastMember.ToString();
            }

            // replace old member by new inverted one in calculation string 
            if (calculationMembers.Length > 1) {

                CurrentInput = CurrentInput.Remove(CurrentInput.Length - lastMemberLen, lastMemberLen);
                CurrentInput = CurrentInput.Insert(CurrentInput.Length, invertedLastMemberFormatted);

            } else {
                // if contains just one number
                CurrentInput = invertedLastMemberFormatted;
            }
           
        }

        static string parenthesisRemove(string nb)
        {   
            return Regex.Replace(nb, @"[()]", "");
        }

    }
}
