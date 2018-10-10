using System;
using System.Collections.Generic;
using System.Text;

namespace AlphaCinemaServices.Contracts
{
    public interface IMovieGenreServices
    {
        List<string> GetMovieIDsByGenre(string genreName);

        List<string> GetGenreIDsByMovie(string movieName);

    }
}
