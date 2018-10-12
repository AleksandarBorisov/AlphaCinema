using AlphaCinema.Core.Commands.DisplayMenus.Abstract;
using AlphaCinema.Core.Contracts;
using AlphaCinemaServices.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace AlphaCinema.Core.Commands.BasicCommands
{
	public class UserInfo : DisplayBaseCommand
	{
		private readonly IUserServices userServices;
		private readonly IProjectionsServices projectionsServices;

		public UserInfo(ICommandProcessor commandProcessor, IItemSelector selector,
			IUserServices userServices, IProjectionsServices projectionsServices)
			: base(commandProcessor, selector)
		{
			this.userServices = userServices;
		}

		public override void Execute(List<string> parameters)
		{
			int offSetFromTop = int.Parse(parameters[parameters.Count - 2]);
			int startingRow = int.Parse(parameters[parameters.Count - 1]);

			List<string> displayItems = new List<string>
			{
				parameters[0],
				"Retry",
				"Back",
				"Home"
			};

			string enterUserName = "Type user name:";

			selector.PrintAtPosition(displayItems[0].ToUpper(), startingRow * 0 + offSetFromTop, false);
			selector.PrintAtPosition(enterUserName, startingRow * 1 + offSetFromTop, false);

			string userName = selector.ReadAtPosition(startingRow * 2 + offSetFromTop,
				enterUserName, false, 250);

			displayItems.Add(offSetFromTop.ToString());
			displayItems.Add(startingRow.ToString());

			try
			{
				//Just clear the input from the console
				selector.PrintAtPosition(new string(' ', enterUserName.Length),
					startingRow * 1 + offSetFromTop, false);

				//Find user by name
				int userID = this.userServices.GetID(userName);

				//Get only projections IDs that user has been on them
				var projectionsIDs = this.userServices.GetProjectionsIDsByUserID(userID);

				//Now get projections as objects by ID
				List<string> projections = new List<string>();
				foreach (int projID in projectionsIDs)
				{
					projections.Add(this.projectionsServices.GetProjectionByID(projID).ToString());
				}

				Console.WriteLine(string.Join(",", projections));
			}
			catch (Exception)
			{

				throw;
			}

		}
	}
}