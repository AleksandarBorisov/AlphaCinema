using AlphaCinema.Core.Contracts;
using AlphaCinema.Core.Commands.DisplayMenus.Abstract;
using System;
using System.Collections.Generic;

namespace AlphaCinema.Core.Commands.DisplayMenus
{
    public class Menu : DisplayBaseCommand
    {
        public Menu(ICommandProcessor commandProcessor,IItemSelector selector)
			: base(commandProcessor, selector)
		{

        }

		public override void Execute(List<string> parameters)
		{
            //В самите параметри които подаваме се съдържат координатите на принтиране
            string offSetFromTop = parameters[parameters.Count - 2];
            string startingRow = parameters[parameters.Count - 1];

            string result = selector.DisplayItems(parameters);

            if (result == "BuyTickets") // sorry for this
			{
				result = "ChooseCity"; // sorry for this
			}
			if (result == "Exit")
            {
                //Ако сме избрали Exit, просто с индекси е по-безопасно ако сменим името на стринга
                Environment.Exit(0);
            }
            if (result == "LogAsAdmin")
            {
                result = "Login";
            }
            parameters.Insert(0, result);
			
            commandProcessor.ExecuteCommand(parameters);
        }
    }
}
