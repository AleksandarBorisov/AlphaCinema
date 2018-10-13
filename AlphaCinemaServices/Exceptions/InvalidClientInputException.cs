using System;

namespace AlphaCinemaServices.Exceptions
{
	public class InvalidClientInputException : Exception
	{
		public InvalidClientInputException(string message) : base(message)
		{

		}
	}
}
