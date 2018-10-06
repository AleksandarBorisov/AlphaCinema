using System;
using Autofac;
using Autofac.Core;
using AlphaCinema.Core.Contracts;

namespace AlphaCinema.Core.Commands.Factory
{
    public class CommandFactory : ICommandFactory
    {
        private IComponentContext autofacContext;

        public CommandFactory(IComponentContext autofacContext)
        {
            this.autofacContext = autofacContext;
        }

        public ICommand GetCommand(string commandName)
        {
            try
            {
                return autofacContext.ResolveNamed<ICommand>(commandName);
            }
            catch (DependencyResolutionException ex)
            {
                throw new ArgumentException("No such command implemented! Consider implementing it before using!", ex);
            }
        }
    }
}
