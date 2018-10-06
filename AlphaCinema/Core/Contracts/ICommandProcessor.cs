using System.Collections.Generic;

namespace AlphaCinema.Core.Contracts
{
    public interface ICommandProcessor
    {
        void ExecuteCommand(List<string> command);
    }
}
