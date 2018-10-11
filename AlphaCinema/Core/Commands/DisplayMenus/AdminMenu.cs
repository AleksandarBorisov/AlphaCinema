using AlphaCinema.Core.Contracts;
using AlphaCinema.Core.Commands.DisplayMenus.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AlphaCinema.Core.Commands.DisplayMenus
{
    public class AdminMenu : DisplayBaseCommand
    {
        public AdminMenu(ICommandProcessor commandProcessor, IItemSelector selector)
            : base(commandProcessor, selector)
        {

        }

        public override void Execute(List<string> parameters)
        {
            string offSetFromTop = parameters[parameters.Count - 2];
            string startingRow = parameters[parameters.Count - 1];
            List<string> displayItems = new List<string>() { parameters[0] };
            displayItems.Add("AddMovie");
            displayItems.Add("RemoveMovie");
            displayItems.Add("AddGenre");
            displayItems.Add("RemoveGenre");
            displayItems.Add("AddTown");
            displayItems.Add("RemoveTown");
            //displayItems.Add("AddProjection");
            //displayItems.Add("RemoveProjection");
            //displayItems.Add("FindUser");
            displayItems.Add("Back");
            displayItems.Add("Home");
            displayItems.Add(offSetFromTop);
            displayItems.Add(startingRow);

            string commandName = selector.DisplayItems(displayItems);

            if (commandName == "Back" || commandName == "Home")
            {
                commandProcessor.ExecuteCommand(parameters.Skip(1).ToList());
            }
            else
            {
                parameters.Insert(0, commandName);
                commandProcessor.ExecuteCommand(parameters);
            }
        }
    }
}
