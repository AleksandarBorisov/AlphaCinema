using System.Collections.Generic;

namespace AlphaCinema.Core.Contracts
{
    public interface ICommandProcessor
    {
        ICommand ParseCommand(string commandName);
    }
}
