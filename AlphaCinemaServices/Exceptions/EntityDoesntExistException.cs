using System;

namespace AlphaCinemaServices.Exceptions
{
	public class EntityDoesntExistException : Exception
	{
		public EntityDoesntExistException(string message) : base(message)
		{

		}
	}
}
