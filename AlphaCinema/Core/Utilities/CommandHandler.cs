using AlphaCinema.Core.Contracts;
using System.Collections.Generic;
using System.Linq;

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
