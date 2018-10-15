using System.Collections.Generic;

namespace AlphaCinemaServices.Contracts
{
    public interface IMovieServices
    {
        int GetID(string movieName);
		ICollection<string> GetMovieNames();
        void AddNewMovie(string name, string description, int releaseYear, int duration);
        void UpdateMovie(string name, string description, int releaseYear, int duration);
        void DeleteMovie(string movieName);
		ICollection<string> GetMovieNamesByCityIDGenreID(int genreID, int cityID);
    }
}
