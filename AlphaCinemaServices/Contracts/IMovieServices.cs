using System;
using System.Collections.Generic;

namespace AlphaCinemaServices.Contracts
{
	public interface IMovieServices
	{
		List<Guid> GetIDs();
		List<string> GetMovieNames(List<Guid> MovieIDs);
	}
}
