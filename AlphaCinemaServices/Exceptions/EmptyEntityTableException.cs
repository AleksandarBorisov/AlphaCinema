using System;
using System.Collections.Generic;
using System.Text;

namespace AlphaCinemaServices.Exceptions
{
	public class EmptyEntityTableException : Exception
	{
		public EmptyEntityTableException(string message) : base(message)
		{

		}
	}
}
