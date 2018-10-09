using System;
using System.Collections.Generic;

namespace AlphaCinemaServices.Contracts
{
	public interface IMovieServices
	{
		string GetID(string movieName);
		List<string> GetMovieNames();
	}
}
