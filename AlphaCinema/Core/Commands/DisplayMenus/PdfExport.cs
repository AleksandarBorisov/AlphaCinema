﻿using AlphaCinema.Core.Commands.DisplayMenus.Abstract;
using AlphaCinema.Core.Contracts;
using System.Collections.Generic;
using System.Linq;

namespace AlphaCinema.Core.Commands.DisplayMenus
{
	public class PdfExport : DisplayBaseCommand
	{
		public PdfExport(IItemSelector selector) : base(selector)
		{
		}

		public override IEnumerable<string> Execute(IEnumerable<string> input)
		{
            var parameters = input.ToList();
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
                return parameters.Skip(1);
			}
			else
			{
				parameters.Insert(0, commandName);
                return parameters;
			}
		}
	}
}
