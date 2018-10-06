using AlphaCinema.Core.Contracts;

namespace AlphaCinema.Core.Commands.Factory
{
    public interface ICommandFactory
    {
        ICommand GetCommand(string commandName);
    }
}