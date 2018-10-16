using AlphaCinema.Core.Contracts;
using AlphaCinema.Core.Commands.DisplayMenus.Abstract;
using System.Collections.Generic;
using System.Linq;

namespace AlphaCinema.Core.Commands.DisplayMenus
{
    public class AdminMenu : DisplayBaseCommand
    {
        public AdminMenu(IItemSelector selector)
            : base(selector)
        {

        }

        public override IEnumerable<string> Execute(IEnumerable<string> input)
        {
            var parameters = input.ToList();
            string offSetFromTop = parameters[parameters.Count - 2];
            string startingRow = parameters[parameters.Count - 1];
            List<string> displayItems = new List<string>() { parameters[0] };

            AddOptions(displayItems);

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
        private void AddOptions(List<string> displayItems)
        {
            displayItems.Add("AddMovie");
            displayItems.Add("UpdateMovie");
            displayItems.Add("RemoveMovie");
            //displayItems.Add("AddGenre");
            //displayItems.Add("RemoveGenre");
            //displayItems.Add("AddCity");
            //displayItems.Add("RemoveCity");
            //displayItems.Add("AddMovieGenre");
            //displayItems.Add("RemoveMovieGenre");
            //displayItems.Add("AddProjection");
            //displayItems.Add("RemoveProjection");
            //displayItems.Add("UserInfo");
            displayItems.Add("Back");
            displayItems.Add("Home");
        }
    }
}