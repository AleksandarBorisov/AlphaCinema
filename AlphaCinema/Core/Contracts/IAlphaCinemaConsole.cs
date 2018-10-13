namespace AlphaCinema.Core.Contracts
{
	public interface IAlphaCinemaConsole
	{
		string ReadLine();

        void Write(string message);
		void WriteLine(string message = null);

        void Clear();
		void ReadKey();
		void HandleException(string message, int left = 40, int top = 4);
		void HandleOperation(string message, int left = 40, int top = 4);
		void WriteLineMiddle(string message, int left = 40, int top = 4);
		string ReadLineMiddle(int left = 40, int top = 4);
		void GoBack();
	}
}
