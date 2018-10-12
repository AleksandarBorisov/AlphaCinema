using AlphaCinema.Core.Contracts;
using System;

namespace AlphaCinema.Core.Utilities
{
	public class AlphaCinemaConsole : IAlphaCinemaConsole
	{
        private readonly IItemSelector selector;

        public AlphaCinemaConsole(IItemSelector selector)
        {
            this.selector = selector;
        }

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

		public void HandleException(string message)
		{
            Console.WriteLine(message);
            Console.Write("\nPress any key to go back...");
            Console.ReadKey();
            Console.Clear();
		}

		public void HandleOperation(string message)
		{
            Console.WriteLine(message);
            Console.Write("\nPress any key to go back...");
            Console.ReadKey();
			Console.Clear();
		}

		public void SetScreenSize()
		{
			Console.WindowHeight = 30;
			Console.BufferWidth = Console.WindowWidth = 120;
		}


	}
}
