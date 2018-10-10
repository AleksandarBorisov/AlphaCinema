using AlphaCinema.Core.Commands.DisplayMenus.Abstract;
using AlphaCinema.Core.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace AlphaCinema.Core.Commands.BasicCommands
{
    public class AddMovie : DisplayBaseCommand
    {
        public AddMovie(ICommandProcessor commandProcessor, IItemSelector selector)
            : base(commandProcessor, selector)
        {

        }

        public override void Execute(List<string> parameters)
        {
            string offSetFromTop = parameters[parameters.Count - 2];
            string startingRow = parameters[parameters.Count - 1];

        }
    }
}
