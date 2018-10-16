using System.Collections.Generic;

namespace AlphaCinema.Core.Contracts
{
    public interface IItemSelector
    {
        string DisplayItems(IEnumerable<string> selection);

        void PrintAtPosition(string item, int currentRow, bool selected);

        string ReadAtPosition(int currentRow, string caption, bool hideCharacters, int maxLength);

        string HideCharacters(bool hideCharacters, int stringMaxLength);
    }
}
