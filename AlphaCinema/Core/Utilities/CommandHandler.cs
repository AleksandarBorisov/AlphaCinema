using AlphaCinema.Core.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AlphaCinema.Core.Commands
{
	public class CommandHandler : ICommandHandler
	{
		private readonly IAlphaCinemaConsole IOconsole;

		public CommandHandler(IAlphaCinemaConsole IOconsole)
		{
			this.IOconsole = IOconsole;
		}

		public List<string> Input()
		{
			var parameters = IOconsole.ReadLine().Split('|').ToList();
			return parameters;
		}
	}
}
