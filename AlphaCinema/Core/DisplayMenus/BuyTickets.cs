using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AlphaCinema.Core.Contracts;

namespace AlphaCinema.Core.DisplayMenus
{
    public class BuyTickets : ICommand
    {
        private ICommandProcessor commandProcessor;
        private IItemSelector selector;

        public BuyTickets(ICommandProcessor commandProcessor, IItemSelector selector)
        {
            this.commandProcessor = commandProcessor;
            this.selector = selector;
        }

        public void Execute(List<string> parameters)
        {
            string offSetFromTop = parameters[parameters.Count - 2];
            string startingRow = parameters[parameters.Count - 1];
            var displayItems = new List<string>() { parameters[0], "ChooseTown", "Back", offSetFromTop, startingRow };
            string result = selector.DisplayItems(displayItems);
            if (displayItems.IndexOf(result) == displayItems.Count - 3)
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
