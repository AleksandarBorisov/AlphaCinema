using AlphaCinema.Core.Contracts;
using AlphaCinemaServices.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace AlphaCinema.Core.DisplayMenus.Abstract
{
	public abstract class DisplayBaseCommand : ICommand
	{
		protected readonly ICommandProcessor commandProcessor;
        protected readonly ICityServices cityServices;
        protected readonly IItemSelector selector;

		public DisplayBaseCommand(ICommandProcessor commandProcessor, IItemSelector selector, ICityServices cityServices)
		{
			this.commandProcessor = commandProcessor;
            this.cityServices = cityServices;
			this.selector = selector;
		}

		public abstract void Execute(List<string> parameters);
	}
}
