using AlphaCinema.Core.Commands.DisplayMenus.Abstract;
using AlphaCinema.Core.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AlphaCinema.Core.Commands.DisplayMenus
{
	public class PdfExport : DisplayBaseCommand
	{
		public PdfExport(ICommandProcessor commandProcessor, IItemSelector selector) : base(commandProcessor, selector)
		{
		}

		public override void Execute(List<string> parameters)
		{
			string offSetFromTop = parameters[parameters.Count - 2];
			string startingRow = parameters[parameters.Count - 1];

			List<string> displayItems = new List<string>() { "PDF Export" };

			displayItems.Add("WatchedMoviesByUser");
			displayItems.Add("AllWatchedMoviesByUsers");
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
