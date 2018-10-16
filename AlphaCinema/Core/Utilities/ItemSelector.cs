using AlphaCinema.Core.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AlphaCinema.Core.Utilities
{
    public class ItemSelector : IItemSelector
    {
        private IAlphaCinemaConsole alphaConsole;

        public ItemSelector(IAlphaCinemaConsole alphaConsole)
        {
            this.alphaConsole = alphaConsole;
        }

        public string DisplayItems(IEnumerable<string> input)
        {
            var selection = input.ToList();

            int currentIndex = 1;

            int offSetFromTop = int.Parse(selection[selection.Count - 2]);
            int startingRow = int.Parse(selection[selection.Count - 1]);

            for (int i = 0; i < selection.Count - 2; i++)
            {
                //Последните два елемента са ни координатите
                //Ако i == 0, то принтим най-отгоре с големи букви секцията на менюто, в която сме
                PrintAtPosition(i == 0 ? selection[i].ToUpper() : selection[i],
                    i * startingRow + offSetFromTop, i == currentIndex ? true : false);
            }

            //Тук проверяваме дали индекса ни е равен на настоящия селектиран елемент
            ConsoleKeyInfo key = alphaConsole.ReadKey(true);

            while (key.Key != ConsoleKey.Enter)
            {
                if (key.Key == ConsoleKey.UpArrow && currentIndex > 1)
                {
                    PrintAtPosition(selection[currentIndex]
                        , currentIndex * startingRow + offSetFromTop, false);

                    PrintAtPosition(selection[--currentIndex],
                        currentIndex * startingRow + offSetFromTop, true);
                }
                else if (key.Key == ConsoleKey.DownArrow && currentIndex < selection.Count - 3)
                {
                    PrintAtPosition(selection[currentIndex],
                        currentIndex * startingRow + offSetFromTop, false);

                    PrintAtPosition(selection[++currentIndex],
                        currentIndex * startingRow + offSetFromTop, true);
                }

                key = alphaConsole.ReadKey(true);
            }

            //Принтим просто "празно място", за по-бързо триене от конзолата
            for (int i = 0; i < selection.Count - 2; i++)
            {
                PrintAtPosition(new string(' ', selection[i].Length), i * startingRow + offSetFromTop, false);
            }

            return selection[currentIndex];
        }

        public void PrintAtPosition(string item, int currentRow, bool selected)
        {
            alphaConsole.CursorVisible = false;

            if (selected)
            {
                alphaConsole.BackgroundColor = ConsoleColor.DarkCyan;
                alphaConsole.ForegroundColor = ConsoleColor.Cyan;
            }

            alphaConsole.SetCursorPosition(alphaConsole.WindowWidth / 2 - item.Length / 2, currentRow);
            alphaConsole.Write(item);

            if (selected)
            {
                alphaConsole.BackgroundColor = ConsoleColor.Black;
                alphaConsole.ForegroundColor = ConsoleColor.Gray;
            }
        }

        public string ReadAtPosition(int currentRow, string caption, bool hideCharacters, int maxLength)
        {
            alphaConsole.CursorVisible = true;
            alphaConsole.SetCursorPosition(alphaConsole.WindowWidth / 2 - caption.Length / 2, currentRow);

            string message = HideCharacters(hideCharacters, maxLength);

            alphaConsole.SetCursorPosition(alphaConsole.WindowWidth / 2 - caption.Length / 2, currentRow);
            alphaConsole.Write(new string(' ', message.Length));

            alphaConsole.CursorVisible = false;

            return message;
        }

        public string HideCharacters(bool hideCharacters, int stringMaxLength)
        {
            string password = "";
            ConsoleKeyInfo key;
            do
            {
                key = alphaConsole.ReadKey(true);
                if (!char.IsControl(key.KeyChar) && password.Length < stringMaxLength)
                {//(key.Key != ConsoleKey.Backspace && key.Key != ConsoleKey.Enter)
                    password += key.KeyChar;
                    if (hideCharacters)
                    {
                        alphaConsole.Write('*');
                    }
                    else
                    {
                        alphaConsole.Write(key.KeyChar);
                    }
                }
                else if (key.Key == ConsoleKey.Backspace && password.Length > 0)
                {
                    password = password.Substring(0, password.Length - 1);
                    alphaConsole.Write("\b \b");//'\b' е backspace символът който ни връща назад, след това спейса ни изтрива от конзолата
                    //предишната написана звездичка и ни мести напред, накрая второто '\b' пак ни мести едно назад върху спейса
                    //Цялото това действие се извършва изцяло върху конзолата и няма нищо общо със стринга pass
                }
            } while (key.Key != ConsoleKey.Enter);

            return password;
        }
    }
}
