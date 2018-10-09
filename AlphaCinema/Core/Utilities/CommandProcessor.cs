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
            var resolvedCommand = commnadFactory.GetCommand(commandName);//Тук Resolve-аме командата и разбираме коя е
            resolvedCommand.Execute(command);//Тук вкарваме параметрите и изпълняваме командата 
        }
    }
}
