using System;
using System.Collections.Generic;
using System.Text;

namespace AlphaCinemaServices.Exceptions
{
	public class InvalidClientInputException : Exception
	{
		public InvalidClientInputException(string message) : base(message)
		{

		}
	}
}
