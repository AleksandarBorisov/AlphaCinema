using AlphaCinema.Core.Contracts;
using AlphaCinemaData.Context;
using AlphaCinemaData.Models;
using AlphaCinemaData.Models.Associative;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AlphaCinema.Core.Utilities
{
    public class Data : IData
    {
        private AlphaCinemaContext context;

        public Data(AlphaCinemaContext context)
        {
            this.context = context;
        }

        public void Load()
        {
            if (!IsEmpty()) return;

            Clear();
            //Fill Users Table
            var usersAsString = File.ReadAllText("../../../../AlphaCinemaData/Files/Users.json");
            var users = JsonConvert.DeserializeObject<List<User>>(usersAsString);
            context.Users.AddRange(users);
            context.SaveChanges();

            //Fill Genres Table
            var genresAsString = File.ReadAllText("../../../../AlphaCinemaData/Files/Genres.json");
            var genres = JsonConvert.DeserializeObject<List<Genre>>(genresAsString);
            context.Genres.AddRange(genres);
            context.SaveChanges();

            //Fill Movies Table
            var moviesAsString = File.ReadAllText("../../../../AlphaCinemaData/Files/Movies.json");
            var movies = JsonConvert.DeserializeObject<List<Movie>>(moviesAsString);
            context.Movies.AddRange(movies);
            context.SaveChanges();

            //Fill MovieGenres Table

            var indexOfMovies = new List<int>() { 0, 0, 1, 1, 2, 3, 3, };
            var indexOfGenres = new List<int>() { 0, 1, 0, 1, 5, 4, 2, };
            for (int i = 0; i < 7; i++)
            {
                var movieGenre = new MovieGenre
                {
                    Movie = movies[indexOfMovies[i]],
                    Genre = genres[indexOfGenres[i]]
                };
                context.MoviesGenres.Add(movieGenre);
            }
            context.SaveChanges();

            //Fill OpenHours Table
            var openHoursAsString = File.ReadAllText("../../../../AlphaCinemaData/Files/OpenHours.json");
            var openHours = JsonConvert.DeserializeObject<List<OpenHour>>(openHoursAsString);
            context.OpenHours.AddRange(openHours);
            context.SaveChanges();

            //Fill Cities Table
            var citiesAsString = File.ReadAllText("../../../../AlphaCinemaData/Files/Cities.json");
            var cities = JsonConvert.DeserializeObject<List<City>>(citiesAsString);
            context.Cities.AddRange(cities);
            context.SaveChanges();

            //Fill Projections Table
            indexOfMovies = new List<int>(){ 1, 1, 1, 1, 1, 1, 1, 2, 2, 2, 2, 2, 2, 3, 3, 3, 3, 3, 3, 3 };
            var indexOfCities = new List<int>(){ 1, 2, 2, 2, 5, 2, 2, 1, 4, 5, 5, 5, 3, 3, 2, 1, 2, 3, 4, 5 };
            var indexOfOpenHours = new List<int>(){ 1, 2, 3, 4, 5, 1, 5, 3, 4, 5, 2, 3, 4, 1, 2, 3, 5, 4, 3, 1 };
            Random rnd = new Random();
            for (int i = 0; i < 20; i++)
            {
                var projection = new Projection
                {
                    MovieId = indexOfMovies[i],
                    CityId = indexOfCities[i],
                    OpenHourId = indexOfOpenHours[i],
                    Date = DateTime.Now.AddDays(-rnd.Next(2))
                };
                context.Projections.Add(projection);
                context.SaveChanges();
            }
            context.SaveChanges();
        }

        private void Clear()
        {
            context.Users.RemoveRange(context.Users);
            context.Cities.RemoveRange(context.Cities);
            context.WatchedMovies.RemoveRange(context.WatchedMovies);
            context.Projections.RemoveRange(context.Projections);
            context.OpenHours.RemoveRange(context.OpenHours);
            context.Movies.RemoveRange(context.Movies);
            context.MoviesGenres.RemoveRange(context.MoviesGenres);
            context.Genres.RemoveRange(context.Genres);
            context.SaveChanges();
        }

        private bool IsEmpty()
        {
            if (!context.Cities.Any()
                && !context.Genres.Any()
                && !context.Movies.Any()
                && !context.OpenHours.Any()
                && !context.Users.Any())
            {
                return true;
            }
            return false;
        }
    }
}
