﻿using AlphaCinema.Core.Commands.DisplayMenus.Abstract;
using AlphaCinema.Core.Contracts;
using AlphaCinemaServices.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AlphaCinema.Core.Commands.BasicCommands
{
    public class MoviesByAge : DisplayBaseCommand
    {
        private IUserServices userServices;
        private IAlphaCinemaConsole cinemaConsole;

        public MoviesByAge(IItemSelector selector, IAlphaCinemaConsole cinemaConsole, 
            IUserServices userServices) : base(selector)
        {
            this.userServices = userServices;
            this.cinemaConsole = cinemaConsole;
        }

        public override IEnumerable<string> Execute(IEnumerable<string> input)
        {
            var parameters = input.ToList();
            int offSetFromTop = int.Parse(parameters[parameters.Count - 2]);
            int startingRow = int.Parse(parameters[parameters.Count - 1]);

            List<string> displayItems = new List<string>
            {
                parameters[0],
                "Retry",
                "Back",
                "Home"
            };

            string enterAgeRange = "Format: MinAge | MaxAge";
            int currentRow = startingRow * 0;

            selector.PrintAtPosition(displayItems[0].ToUpper(), currentRow++ + offSetFromTop, false);
            selector.PrintAtPosition(enterAgeRange, currentRow++ + offSetFromTop, false);

            string ages = selector.ReadAtPosition(currentRow++ + offSetFromTop, enterAgeRange, false, 250);

            displayItems.Add(offSetFromTop.ToString());
            displayItems.Add(startingRow.ToString());

            string[] agesDetails = ages.Split('|');

            try
            {
                if (agesDetails.Length != 2)
                {
                    throw new ArgumentException("Please enter two arguments");
                }

                if (!int.TryParse(agesDetails[0].Trim(), out int minAge))
                {
                    throw new ArgumentException("Min Age must be integer number");
                }
                if (!int.TryParse(agesDetails[1].Trim(), out int maxAge))
                {
                    throw new ArgumentException("Max Age must be integer number");
                }

                selector.PrintAtPosition(new string(' ', enterAgeRange.Length), startingRow * 1 + offSetFromTop, false);//Затрива stringa enterAgeRange

                var results = userServices.GetMoviesByUserAge(minAge, maxAge);
                currentRow = offSetFromTop + 1;
                foreach (var result in results)
                {
                    selector.PrintAtPosition(result.ToString(), currentRow++, false);
                }

                string endOfResluts = "Press any key to return to Information";
                selector.PrintAtPosition(endOfResluts, currentRow, false);

                cinemaConsole.ReadKey(true);

                currentRow = offSetFromTop + 1;
                foreach (var result in results)
                {
                    selector.PrintAtPosition(new string(' ', result.ToString().Length), currentRow++, false);
                }// Тук просто затриваме това което е било принтирано
                selector.PrintAtPosition(new string(' ', endOfResluts.Length), currentRow, false);

                return parameters.Skip(1);//Извикваме предишната команда
            }
            catch (Exception ex)
            {
                if (ex is ArgumentException)
                {
                    string wrongParametersDetails = ex.Message;

                    selector.PrintAtPosition(new string(' ', enterAgeRange.Length), 1 + offSetFromTop, false);//Затрива stringa enterAgeRange
                    selector.PrintAtPosition(wrongParametersDetails, startingRow * 4 + offSetFromTop, false);

                    string selected = selector.DisplayItems(displayItems);

                    selector.PrintAtPosition(new string(' ', wrongParametersDetails.Length), startingRow * 4 + offSetFromTop, false);

                    if (selected == "Retry")
                    {
                        return parameters;
                    }
                    else if (selected == "Back")
                    {
                        return parameters.Skip(1);
                    }
                    else if (selected == "Home")
                    {
                        return parameters.Skip(2);
                    }
                }
                return ex.Message.ToString().Split();
            }
        }
    }
}
