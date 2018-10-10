﻿using AlphaCinema.Core.Contracts;
using AlphaCinema.Core.DisplayMenus.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AlphaCinema.Core.DisplayMenus
{
    public class Login : DisplayBaseCommand
    {
        public Login(ICommandProcessor commandProcessor, IItemSelector selector)
            : base(commandProcessor, selector)
        {

        }

        public override void Execute(List<string> parameters)
        {
            int offSetFromTop = int.Parse(parameters[parameters.Count - 2]);
            int startingRow = int.Parse(parameters[parameters.Count - 1]);
            List<string> displayItems = new List<string>();
            displayItems.Add(parameters[0]);
            displayItems.Add("Retry");
            displayItems.Add("Back");
            displayItems.Add("Home");
            string enterPassword = "Enter password:";
            string wrongPasswordMessage = "Incorrect passowrd, consider using 'TelerikAcademyFTW' instead";
            selector.PrintAtPosition(displayItems[0].ToUpper(), startingRow * 0 + offSetFromTop, false);
            selector.PrintAtPosition(enterPassword, startingRow * 1 + offSetFromTop, false);
            string password = selector.ReadAtPosition(startingRow * 2 + offSetFromTop, "Enter password:");
            displayItems.Add(offSetFromTop.ToString());
            displayItems.Add(startingRow.ToString());
            while (password != "TelerikAcademyFTW".ToLower())
            {
                selector.PrintAtPosition(new string(' ', enterPassword.Length), startingRow * 1 + offSetFromTop, false);
                selector.PrintAtPosition(wrongPasswordMessage, startingRow * 4 + offSetFromTop, false);
                string selected = selector.DisplayItems(displayItems);
                selector.PrintAtPosition(new string(' ', wrongPasswordMessage.Length), startingRow * 4 + offSetFromTop, false);
                if (selected == "Back" || selected == "Home")
                {
                    commandProcessor.ExecuteCommand(parameters.Skip(1).ToList());
                }
                selector.PrintAtPosition(displayItems[0].ToUpper(), startingRow * 0 + offSetFromTop, false);
                selector.PrintAtPosition(enterPassword, startingRow * 1 + offSetFromTop, false);
                password = selector.ReadAtPosition(startingRow * 2 + offSetFromTop, "Enter password:");
            }
            selector.PrintAtPosition(new string(' ', enterPassword.Length), startingRow * 1 + offSetFromTop, false);
            selector.PrintAtPosition(new string(' ', wrongPasswordMessage.Length), startingRow * 4 + offSetFromTop, false);
            parameters[0] = "AdminMenu";
            commandProcessor.ExecuteCommand(parameters);
        }
    }
}
