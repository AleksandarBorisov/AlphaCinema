using AlphaCinema.Core.Contracts;
using System.Collections.Generic;

namespace AlphaCinema.Core.Commands.DisplayMenus.Abstract
{
	public abstract class DisplayBaseCommand : ICommand
	{
        protected readonly IItemSelector selector;

		public DisplayBaseCommand(IItemSelector selector)
		{
			this.selector = selector;
		}

		public abstract IEnumerable<string> Execute(IEnumerable<string> parameters);
	}
}
