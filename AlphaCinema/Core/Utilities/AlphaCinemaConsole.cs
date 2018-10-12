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

        public string ReadLineInMiddle(string str, int row)
        {
            Console.SetCursorPosition(Console.WindowWidth / 2 - str.Length / 2, row);
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
        
        public void WriteInMiddle(string str, int row)
        {
            Console.SetCursorPosition(Console.WindowWidth / 2 - str.Length / 2, row);
        }

        public void WriteLineInMiddle(string str, int row)
        {
            Console.SetCursorPosition(Console.WindowWidth / 2 - str.Length / 2, row);
            Console.Write(str);
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
            //WriteLineInMiddle(message, 8);
            //WriteLineInMiddle("\nPress any key to go back...", 10);
            Console.ReadKey();
            Console.Clear();
		}

		public void HandleOperation(string message)
		{
            Console.WriteLine(message);
            Console.Write("\nPress any key to go back...");
            //WriteLineInMiddle(message, 8);
            //WriteLineInMiddle("\nPress any key to go back...", 10);
            Console.ReadKey();
			Console.Clear();
		}


	}
}
