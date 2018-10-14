using System.Collections.Generic;

namespace AlphaCinema.Core.Contracts
{
    public interface ICommand
    {
        IEnumerable<string> Execute(IEnumerable<string> parameters);
    }
}
