using AlphaCinema.Core.Contracts;
using AlphaCinema.Core.Commands.DisplayMenus.Abstract;
using AlphaCinemaData.Context;
using AlphaCinemaServices.Contracts;
using System.Collections.Generic;
using System.Linq;

namespace AlphaCinema.Core.Commands.DisplayMenus
{
    public class ChooseHour : DisplayBaseCommand
    {
        private readonly IOpenHourServices openHourServices;
        private readonly IProjectionsServices projectionsServices;

        public ChooseHour(ICommandProcessor commandProcessor, IItemSelector selector, IOpenHourServices openHourServices, IProjectionsServices projectionsServices)
            : base(commandProcessor, selector)
        {
            this.openHourServices = openHourServices;
            this.projectionsServices = projectionsServices;
        }

        public override void Execute(List<string> parameters)
        {
            string offSetFromTop = parameters[parameters.Count - 2];
            string startingRow = parameters[parameters.Count - 1];
            string cityID = parameters[5];
            string movieID = parameters[1];

            //Тук ще направим заявка до базата от таблицата Movies за да ни мапне Прожекциите на GUID-овете
            //var hours = this.openHourServices.GetOpenHours();
            //Избираме час на база на филма и града
            var hours = this.projectionsServices.GetOpenHoursByMovieIDCityID(movieID, cityID);
            List<string> displayItems = new List<string>() { "Choose Hour" };

            displayItems.AddRange(hours);
            displayItems.Add("Back");
            displayItems.Add("Home");
            displayItems.Add(offSetFromTop);
            displayItems.Add(startingRow);
            var startHour = selector.DisplayItems(displayItems);
            if (startHour == "Back")
            {
                commandProcessor.ExecuteCommand(parameters.Skip(2).ToList());
            }
            else if (startHour == "Home")
            {
                commandProcessor.ExecuteCommand(parameters.Skip(7).ToList());
            }
            else
            {
                var openHourID = openHourServices.GetID(startHour);
                parameters.Insert(0, openHourID.ToString());
                parameters.Insert(0, "EnterUser");
                commandProcessor.ExecuteCommand(parameters.ToList());
            }
        }
    }
}
