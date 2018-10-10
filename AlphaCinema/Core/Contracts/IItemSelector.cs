using System.Collections.Generic;

namespace AlphaCinema.Core.Contracts
{
    public interface IItemSelector
    {
        string DisplayItems(List<string> selection);

        void PrintAtPosition(string item, int currentRow, bool selected);

        string ReadAtPosition(int currentRow, string caption);

        string HideCharacters();
    }
}
