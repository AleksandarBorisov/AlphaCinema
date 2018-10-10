using AlphaCinema.Core.Contracts;
using System;

namespace AlphaCinema.Core.Utilities
{
	public class AlphaCinemaConsole : IAlphaCinemaConsole
	{
		public string ReadLine()
		{
			return Console.ReadLine();
		}

		public void Write(string message)
		{
			Console.Write(message);
		}

		public void WriteLine(string message)
		{
			Console.WriteLine(message);
		}

		public void Clear()
		{
			Console.Clear();
		}

		public void ReadKey()
		{
			Console.ReadKey();
		}
	}
}
