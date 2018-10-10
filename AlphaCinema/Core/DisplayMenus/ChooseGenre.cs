﻿using AlphaCinema.Core.Contracts;
using AlphaCinema.Core.DisplayMenus.Abstract;
using AlphaCinemaServices.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AlphaCinema.Core.DisplayMenus
{
	public class ChooseGenre : DisplayBaseCommand
	{
		private readonly IGenreServices genreServices;

		public ChooseGenre(ICommandProcessor commandProcessor, IItemSelector selector, IGenreServices genreServices)
			: base(commandProcessor, selector)
		{
			this.genreServices = genreServices;
		}

		public override void Execute(List<string> parameters)
		{
			string offSetFromTop = parameters[parameters.Count - 2];
			string startingRow = parameters[parameters.Count - 1];

			//Тук ще направим заявка до базата от таблицата MovieProjections да ни даде GUID-овете на градовете за текущия ден
			//Тук ще направим заявка до базата от таблицата Towns за да ни мапне филмите на GUID-овете

			var genreNames = this.genreServices.GetGenreNames();
			List<string> displayItems = new List<string>() { "Choose Genre" };

			displayItems.AddRange(genreNames);
			displayItems.Add("Back");
			displayItems.Add("Home");
			displayItems.Add(offSetFromTop);
			displayItems.Add(startingRow);

			string genreName = selector.DisplayItems(displayItems);
			if (genreName == "Back")
			{
				commandProcessor.ExecuteCommand(parameters.Skip(1).ToList());
			}
			//Изтриваме командата ChoooseMovie и извикваме отново предното menu
			else if (genreName == "Home")
			{
				commandProcessor.ExecuteCommand(parameters.Skip(1).ToList());
			}
			else
			{
				var genreID = this.genreServices.GetID(genreName);
				parameters.Insert(0, genreID);
				parameters.Insert(0, "ChooseTown");
				commandProcessor.ExecuteCommand(parameters);
			}
		}
	}
}