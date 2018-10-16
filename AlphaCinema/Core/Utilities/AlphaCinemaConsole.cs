using AlphaCinema.Core.Contracts;
using System;
using System.Threading;

namespace AlphaCinema.Core.Utilities
{
    public class AlphaCinemaConsole : IAlphaCinemaConsole
    {
        private const int left = 40;
        private const int top = 4;
        public string ReadLine()
        {
            return Console.ReadLine();
        }

        public ConsoleKeyInfo ReadKey(bool value)
        {
            return Console.ReadKey(value);
        }

        public void Write(string message)
        {
            Console.Write(message);
        }

        public void Write(char sign)
        {
            Console.Write(sign);
        }

        public void WriteLine(string message = null)
        {
            if (message == null)
            {
                Console.WriteLine();
                return;
            }
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

        public void HandleException(string message, int left = 40, int top = 4)
        {
            ChangeColor(ConsoleColor.Red);
            SetMiddleWrite(0, top + 3);
            Console.WriteLine(message);
            GoBack();
        }

        public void HandleOperation(string message, int left = 40, int top = 4)
        {
            ChangeColor(ConsoleColor.Green);
            SetMiddleWrite(0, top + 3);
            Console.WriteLine(message);
            GoBack();
        }

        public void WriteLineMiddle(string message, int left = 40, int top = 4)
        {
            SetMiddleWrite(left, top);
            Console.WriteLine(message);
        }

        public string ReadLineMiddle(int left = 40, int top = 4)
        {
            SetMiddleRead(left, top + 2);
            return Console.ReadLine();
        }

        public void GoBack()
        {
            Thread.Sleep(1800);
            ResetColor();
            Console.Clear();
        }

        private void SetMiddleWrite(int left, int top)
        {
            Console.SetCursorPosition(left, top);
        }

        private void SetMiddleRead(int left, int top)
        {
            Console.SetCursorPosition(left, top);
        }

        private void ResetColor()
        {
            ChangeColor(ConsoleColor.White);
        }

        private void ChangeColor(ConsoleColor console)
        {
            Console.ForegroundColor = console;
        }

        public bool CursorVisible
        {
            get => Console.CursorVisible;
            set => Console.CursorVisible = value;
        }

        public ConsoleColor BackgroundColor
        {
            get => Console.BackgroundColor;
            set => Console.BackgroundColor = value;
        }

        public ConsoleColor ForegroundColor
        {
            get => Console.ForegroundColor;
            set => Console.ForegroundColor = value;
        }

        public void SetCursorPosition(int left, int top)
        {
            Console.SetCursorPosition(left, top);
        }

        public int WindowWidth
        {
            get => Console.WindowWidth;
            set => Console.WindowWidth = value;
        }
    }
}
