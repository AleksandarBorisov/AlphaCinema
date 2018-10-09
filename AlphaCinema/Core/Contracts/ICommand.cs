using System.Collections.Generic;

namespace AlphaCinema.Core.Contracts
{
    public interface ICommand
    {
        void Execute(List<string> parameters);
    }
}
