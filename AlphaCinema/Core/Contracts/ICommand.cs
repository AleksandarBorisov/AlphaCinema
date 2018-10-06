using System;
using System.Collections.Generic;
using System.Text;

namespace AlphaCinema.Core.Contracts
{
    public interface ICommand
    {
        void Execute(List<string> parameters);
    }
}
