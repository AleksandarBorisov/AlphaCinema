using System;
using System.Collections.Generic;
using System.Text;

namespace AlphaCinemaServices.Contracts
{
    public interface IGenreServices
    {
        string GetID(string genreName);

        List<string> GetGenreNames();
    }
}
