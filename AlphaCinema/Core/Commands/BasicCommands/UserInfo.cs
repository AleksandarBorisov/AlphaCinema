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

        public UserInfo(IItemSelector selector, IAlphaCinemaConsole cinemaConsole,
            IUserServices userServices, IProjectionsServices projectionsServices)
            : base(selector)
        {
            this.userServices = userServices;
            this.projectionsServices = projectionsServices;
            this.cinemaConsole = cinemaConsole;
        }
        public override IEnumerable<string> Execute(IEnumerable<string> input)
        {
            var parameters = input.ToList();
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

            string enterUserName = "Type user name | age:";
            int currentRow = startingRow * 0;

            selector.PrintAtPosition(displayItems[0].ToUpper(), currentRow++ + offSetFromTop, false);
            selector.PrintAtPosition(enterUserName, currentRow++ + offSetFromTop, false);

            string inputDetails = selector.ReadAtPosition(currentRow++ + offSetFromTop, enterUserName, false, 250);
			string[] userDetails = inputDetails.Split('|');
			displayItems.Add(offSetFromTop.ToString());
            displayItems.Add(startingRow.ToString());

            try
            {
				var userName = userDetails[0];
				if (userDetails.Length != 2)
				{
					throw new ArgumentException("Please enter two arguments");
				}
				if (string.IsNullOrEmpty(userName))
				{
					throw new ArgumentException("User name cannot be empty");
				}
				if (!int.TryParse(userDetails[1].Trim(), out int age))
				{
					throw new ArgumentException("Age must be integer number");
				}
				//Just clear enterUserName string from the console
				selector.PrintAtPosition(new string(' ', enterUserName.Length), startingRow * 1 + offSetFromTop, false);

                //Find user by name
                int userID = this.userServices.GetID(userName, age);

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
                // TODO - HELP
                cinemaConsole.ReadKey(true);

                currentRow = offSetFromTop + 1;
                foreach (var projection in projections)
                {
                    selector.PrintAtPosition(new string(' ', projection.ToString().Length), currentRow++, false);
                }// Тук просто затриваме това което е било принтирано
                selector.PrintAtPosition(new string(' ', endOfResluts.Length), currentRow, false);

                return parameters.Skip(1);//Извикваме предишната команда
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