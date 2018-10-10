using AlphaCinema.Core.Commands.Factory;
using AlphaCinema.Core.Contracts;
using System.Collections.Generic;

namespace AlphaCinema.Core.Utilities
{
    class CommandProcessor : ICommandProcessor
    {
        private ICommandFactory commandFactory;

        public CommandProcessor(ICommandFactory commandFactory)
        {
            this.commandFactory = commandFactory;
        }

        public void ExecuteCommand(List<string> parameters)
        {
            var commandName = parameters[0];

            //Тук Resolve-аме командата и разбираме коя е
            var resolvedCommand = commandFactory.GetCommand(commandName);

            //Тук вкарваме параметрите и изпълняваме командата
            resolvedCommand.Execute(parameters); 
        }
    }
}
