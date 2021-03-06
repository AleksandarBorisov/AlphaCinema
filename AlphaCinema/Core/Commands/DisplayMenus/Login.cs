﻿using AlphaCinema.Core.Contracts;
using AlphaCinema.Core.Commands.DisplayMenus.Abstract;
using System.Collections.Generic;
using System.Linq;

namespace AlphaCinema.Core.Commands.DisplayMenus
{
    public class Login : DisplayBaseCommand
    {
        public Login(IItemSelector selector)
            : base(selector)
        {

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

            string enterPassword = "Enter password:";
            string wrongPasswordMessage = "Incorrect password, consider using 'TelerikAcademyFTW' instead";

            selector.PrintAtPosition(displayItems[0].ToUpper(), startingRow * 0 + offSetFromTop, false);
            selector.PrintAtPosition(enterPassword, startingRow * 1 + offSetFromTop, false);

            string password = selector.ReadAtPosition(startingRow * 2 + offSetFromTop, enterPassword, true, 20);

            displayItems.Add(offSetFromTop.ToString());
            displayItems.Add(startingRow.ToString());
			
            while (password != "123".ToLower())
            {
                selector.PrintAtPosition(new string(' ', enterPassword.Length), startingRow * 1 + offSetFromTop, false);
                selector.PrintAtPosition(wrongPasswordMessage, startingRow * 4 + offSetFromTop, false);

                string selected = selector.DisplayItems(displayItems);

                selector.PrintAtPosition(new string(' ', wrongPasswordMessage.Length), startingRow * 4 + offSetFromTop, false);

                if (selected == "Back" || selected == "Home")
                {
                    return parameters.Skip(1);
                }

                selector.PrintAtPosition(displayItems[0].ToUpper(), startingRow * 0 + offSetFromTop, false);
                selector.PrintAtPosition(enterPassword, startingRow * 1 + offSetFromTop, false);

                password = selector.ReadAtPosition(startingRow * 2 + offSetFromTop, "Enter password:", true, 20);
            }

            selector.PrintAtPosition(new string(' ', enterPassword.Length), startingRow * 1 + offSetFromTop, false);
            selector.PrintAtPosition(new string(' ', wrongPasswordMessage.Length), startingRow * 4 + offSetFromTop, false);

            parameters[0] = "AdminMenu";

            return parameters;
        }
    }
}
