using System;
using System.Collections.Generic;
using System.Text;

namespace AlphaCinemaServices.Exceptions
{
	public class EntityDoesntExistException : Exception
	{
		public EntityDoesntExistException(string message) : base(message)
		{

		}
	}
}
