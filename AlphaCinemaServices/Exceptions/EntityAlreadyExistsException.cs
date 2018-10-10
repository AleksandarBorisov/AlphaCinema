using System;
using System.Collections.Generic;
using System.Text;

namespace AlphaCinemaServices.Exceptions
{
	public class EntityAlreadyExistsException : Exception
	{
		public EntityAlreadyExistsException(string message) : base(message)
		{

		}
	}
}
