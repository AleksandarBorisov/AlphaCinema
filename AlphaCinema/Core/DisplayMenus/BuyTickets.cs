using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AlphaCinema.Core.Contracts;
using AlphaCinema.Core.DisplayMenus.Abstract;
using AlphaCinemaServices.Contracts;

namespace AlphaCinema.Core.DisplayMenus
{
    public class BuyTickets : DisplayBaseCommand
	{
        public BuyTickets(ICommandProcessor commandProcessor, IItemSelector selector)
			: base (commandProcessor, selector)
        {
			// Add Buy tickets services in depedency (if needed)
        }

        public override void Execute(List<string> parameters)
        {
            string offSetFromTop = parameters[parameters.Count - 2];
            string startingRow = parameters[parameters.Count - 1];
            var displayItems = new List<string>() { parameters[0], "ChooseTown", "Back", "Home", offSetFromTop, startingRow };
            string result = selector.DisplayItems(displayItems);
            if (result == "Back" || result == "Home")
            {
                commandProcessor.ExecuteCommand(parameters.Skip(1).ToList());
            }
            else
            {
                parameters.Insert(0, result);
                commandProcessor.ExecuteCommand(parameters);
            }
        }
    }
}
