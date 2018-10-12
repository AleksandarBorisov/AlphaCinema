namespace AlphaCinema.Core.Contracts
{
	public interface IAlphaCinemaConsole
	{
		string ReadLine();
        string ReadLineInMiddle(string str, int row);
        void Write(string message);
		void WriteLine(string message);
        void WriteInMiddle(string str, int row);
        void WriteLineInMiddle(string str, int row);
        void Clear();
		void ReadKey();
		void HandleException(string message);
		void HandleOperation(string message);
	}
}
