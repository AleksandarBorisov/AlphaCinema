using AlphaCinema.Core.Commands.DisplayMenus.Abstract;
using AlphaCinema.Core.Contracts;
using AlphaCinemaServices.Contracts;
using AlphaCinemaServices.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AlphaCinema.Core.Commands.BasicCommands
{
    public class UserInfo : DisplayBaseCommand
    {
        private readonly IUserServices userServices;
        private readonly IProjectionsServices projectionsServices;
        private readonly IAlphaCinemaConsole cinemaConsole;

        public UserInfo(ICommandProcessor commandProcessor, IItemSelector selector,
            IUserServices userServices, IProjectionsServices projectionsServices,
            IAlphaCinemaConsole cinemaConsole)
            : base(commandProcessor, selector)
        {
            this.userServices = userServices;
            this.projectionsServices = projectionsServices;
            this.cinemaConsole = cinemaConsole;
        }
        public override void Execute(List<string> parameters)
        {
            int offSetFromTop = int.Parse(parameters[parameters.Count - 2]);
            int startingRow = int.Parse(parameters[parameters.Count - 1]);

            List<string> displayItems = new List<string>() { "UserInfo" };
            
            displayItems = new List<string>
            {
                parameters[0],
                "Retry",
                "Back",
                "Home"
            };

            string enterUserName = "Type user name:";
            int currentRow = startingRow * 0;

            selector.PrintAtPosition(displayItems[0].ToUpper(), currentRow++ + offSetFromTop, false);
            selector.PrintAtPosition(enterUserName, currentRow++ + offSetFromTop, false);

            string userName = selector.ReadAtPosition(currentRow++ + offSetFromTop, enterUserName, false, 250);

            displayItems.Add(offSetFromTop.ToString());
            displayItems.Add(startingRow.ToString());

            try
            {
                //Just clear enterUserName string from the console
                selector.PrintAtPosition(new string(' ', enterUserName.Length), startingRow * 1 + offSetFromTop, false);

                //Find user by name
                int userID = this.userServices.GetID(userName);

                //Get projections that user has been on them
                var projections = this.userServices.GetProjectionsByUserID(userID);

                currentRow = offSetFromTop + 1;
                //Display all projections
                foreach (var projection in projections)
                {
                    selector.PrintAtPosition(projection.ToString(), currentRow++, false);
                }

                string endOfResluts = "Press any key to return";

                selector.PrintAtPosition(endOfResluts, currentRow, false);

                Console.ReadKey(true);

                currentRow = offSetFromTop + 1;
                foreach (var projection in projections)
                {
                    selector.PrintAtPosition(new string(' ', projection.ToString().Length), currentRow++, false);
                }// Тук просто затриваме това което е било принтирано
                selector.PrintAtPosition(new string(' ', endOfResluts.Length), currentRow, false);

                commandProcessor.ExecuteCommand(parameters.Skip(1).ToList());//Извикваме предишната команда
            }
            catch (Exception ex)
            {
                if (ex is ArgumentException || ex is EntityDoesntExistException)
                {
                    string wrongParametersDetails = ex.Message;

                    selector.PrintAtPosition(new string(' ', enterUserName.Length), 1 + offSetFromTop, false);//Затрива stringa enterAgeRange
                    selector.PrintAtPosition(wrongParametersDetails, startingRow * 4 + offSetFromTop, false);//Изписва error message-a

                    string selected = selector.DisplayItems(displayItems);//Изписва избора на бутон

                    selector.PrintAtPosition(new string(' ', wrongParametersDetails.Length), startingRow * 4 + offSetFromTop, false);//Затрива error message-a

                    if (selected == "Retry")
                    {
                        commandProcessor.ExecuteCommand(parameters);
                    }
                    else if (selected == "Back")
                    {
                        commandProcessor.ExecuteCommand(parameters.Skip(1).ToList());
                    }
                    else if (selected == "Home")
                    {
                        commandProcessor.ExecuteCommand(parameters.Skip(2).ToList());
                    }
                }
                else
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }
    }
}