using System;

namespace AlphaCinemaServices.Exceptions
{
	public class EntityAlreadyExistsException : Exception
	{
		public EntityAlreadyExistsException(string message) : base(message)
		{

		}
	}
}
