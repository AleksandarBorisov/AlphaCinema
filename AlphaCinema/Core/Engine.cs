using AlphaCinema.Core.Contracts;
using AlphaCinemaData.Context;
using AlphaCinemaData.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace AlphaCinema.Core
{
    class Engine : IEngine
    {
        private ICommandProcessor commandProcessor;
		private IAlphaCinemaContext context;
		private List<string> menus = new List<string>() { "MainMenu", "BuyTickets", "Exit", "7", "2" };//, "AddMovie", "AddProjection"
        //Първото е името на командата, второто са стойностите, а това накрая са просто координати за принтиране
        public Engine(ICommandProcessor commandProcessor, IAlphaCinemaContext context)//, IAlphaConsole alphaConsole)
        {
            this.commandProcessor = commandProcessor;
			this.context = context;
        }

        public void Run()
        {
            commandProcessor.ExecuteCommand(menus);
            // Тук просто извикваме първата команда
        }
    }
}
