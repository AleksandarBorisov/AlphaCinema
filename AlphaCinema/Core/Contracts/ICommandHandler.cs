using System.Collections.Generic;

namespace AlphaCinema.Core.Contracts
{
	public interface ICommandHandler
	{
		List<string> Input();
	}
}
