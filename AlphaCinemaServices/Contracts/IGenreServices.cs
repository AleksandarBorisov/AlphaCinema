using AlphaCinemaData.Models;

namespace AlphaCinemaServices.Contracts
{
    public interface IGenreServices
    {
        int GetID(string genreName);
		void AddNewGenre(string genreName);
		void DeleteGenre(string genreName);
		Genre IfExist(string genreName);

	}
}
