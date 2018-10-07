﻿using AlphaCinema.Core.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace AlphaCinema.Core
{
    class Engine : IEngine
    {
        private ICommandProcessor commandProcessor;
        private List<string> menus = new List<string>() { "MainMenu", "BuyTickets", "Exit", "7", "2" };//, "AddMovie", "AddProjection"
        //Първото е името на командата, второто са стойностите, а това накрая са просто координати за принтиране
        public Engine(ICommandProcessor commandProcessor)//, IAlphaConsole alphaConsole)
        {
            this.commandProcessor = commandProcessor;
        }

        public void Run()
        {
            //var menus = new List<string>()
            //{
            //    "MainMenu",
            //    "ChooseTownMEnu",
            //    "ChooseMovieMenu",
            //    "ChooseProjectionMenu"
            //};

            commandProcessor.ExecuteCommand(menus);
            // Тук просто извикваме първата команда
        }
    }
}