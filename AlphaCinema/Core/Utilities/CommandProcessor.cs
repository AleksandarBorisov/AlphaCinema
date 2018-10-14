using AlphaCinema.Core.Commands.Factory;
using AlphaCinema.Core.Contracts;
using System.Collections.Generic;
using System;

namespace AlphaCinema.Core.Utilities
{
    public class CommandProcessor : ICommandProcessor
    {
        private ICommandFactory commandFactory;

        public CommandProcessor(ICommandFactory commandFactory)
        {
            if (commandFactory.Equals(null))
            {
                throw new NullReferenceException("CommandFactory can't be null!");
            }

            this.commandFactory = commandFactory;
        }

        public void ExecuteCommand(List<string> parameters)
        {
            var commandName = parameters[0];

            var command = ParseCommand(commandName);

            //Тук вкарваме параметрите и изпълняваме командата
            command.Execute(parameters); 
        }

        public ICommand ParseCommand(string commandName)
        {
            //Тук Resolve-аме командата и разбираме коя е
            return commandFactory.GetCommand(commandName);
        }
    }
}
