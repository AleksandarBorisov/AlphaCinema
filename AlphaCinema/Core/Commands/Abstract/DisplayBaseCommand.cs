using AlphaCinema.Core.Contracts;
using System.Collections.Generic;

namespace AlphaCinema.Core.Commands.DisplayMenus.Abstract
{
	public abstract class DisplayBaseCommand : ICommand
	{
		protected readonly ICommandProcessor commandProcessor;
        protected readonly IItemSelector selector;

		public DisplayBaseCommand(ICommandProcessor commandProcessor, IItemSelector selector)
		{
			this.commandProcessor = commandProcessor;
			this.selector = selector;
		}

		public abstract void Execute(List<string> parameters);
	}
}
