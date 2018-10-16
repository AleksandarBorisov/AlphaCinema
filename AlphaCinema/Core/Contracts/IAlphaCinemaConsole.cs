using System;

namespace AlphaCinema.Core.Contracts
{
	public interface IAlphaCinemaConsole
	{
		string ReadLine();
        ConsoleKeyInfo ReadKey(bool value);

        void Write(char sign);
        void Write(string message);
        void WriteLine(string message = null);

        void Clear();
		void ReadKey();
        bool CursorVisible { get; set; }
        void HandleException(string message, int left = 40, int top = 4);
		void HandleOperation(string message, int left = 40, int top = 4);
		void WriteLineMiddle(string message, int left = 40, int top = 4);
		string ReadLineMiddle(int left = 40, int top = 4);
        ConsoleColor BackgroundColor { get; set; }
        ConsoleColor ForegroundColor { get; set; }
        int WindowWidth { get; set; }
        void SetCursorPosition(int left, int top);
        void GoBack();
	}
}
