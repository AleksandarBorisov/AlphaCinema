using System;
using System.Collections.Generic;
using System.Text;

namespace AlphaCinemaServices.Contracts
{
	public interface IMovieServices
	{
		List<Guid> GetIDs();
		List<string> GetMovieNames(List<Guid> MovieIDs);
	}
}
