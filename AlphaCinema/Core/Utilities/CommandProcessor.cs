using AlphaCinema.Core.Commands.Factory;
using AlphaCinema.Core.Contracts;
using System.Collections.Generic;

namespace AlphaCinema.Core.Utilities
{
    class CommandProcessor : ICommandProcessor
    {
        private ICommandFactory commnadFactory;

        public CommandProcessor(ICommandFactory commnadFactory)
        {
            this.commnadFactory = commnadFactory;
        }

        public void ExecuteCommand(List<string> command)
        {
            var commandName = command[0];

            //Тук Resolve-аме командата и разбираме коя е
            var resolvedCommand = commnadFactory.GetCommand(commandName);

            //Тук вкарваме параметрите и изпълняваме командата
            resolvedCommand.Execute(command); 
        }
    }
}
