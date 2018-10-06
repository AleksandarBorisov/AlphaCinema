using AlphaCinema.Core.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AlphaCinema.Core.DisplayMenus
{
    public class MainMenu : ICommand
    {
        private ICommandProcessor commandProcessor;
        private IItemSelector selector;

        public MainMenu(ICommandProcessor commandProcessor,IItemSelector selector)
        {
            this.commandProcessor = commandProcessor;
            this.selector = selector;
        }

        public void Execute(List<string> menus)
        {//В самите параметри които подаваме се съдържат координатите на принтиране
            string offSetFromTop = menus[menus.Count - 2];
            string startingRow = menus[menus.Count - 1];
            string result = selector.DisplayItems(menus);
            if (menus.IndexOf(result) == menus.Count - 3)
            {//Ако сме избрали Exit, просто с индекси е по-безопасно ако сменим името на стринга
                Environment.Exit(0);
            }
            menus.Insert(0, result);
            commandProcessor.ExecuteCommand(menus);
        }
    }
}
