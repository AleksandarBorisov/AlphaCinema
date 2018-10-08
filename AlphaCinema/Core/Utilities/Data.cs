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
        private IAlphaCinemaContext alphaCinemaContext;

        public Data(IAlphaCinemaContext alphaCinemaContext)
        {
            this.alphaCinemaContext = alphaCinemaContext;
        }

        public void Load()
        {
            alphaCinemaContext.Clear();
            //Fill Users Table
            var usersAsString = File.ReadAllText("../../../../AlphaCinemaData/Files/Users.json");
            var users = JsonConvert.DeserializeObject<List<User>>(usersAsString);
            alphaCinemaContext.Users.AddRange(users);
            alphaCinemaContext.SaveChanges();

            //Fill Genres Table
            var genresAsString = File.ReadAllText("../../../../AlphaCinemaData/Files/Genres.json");
            var genres = JsonConvert.DeserializeObject<List<Genre>>(genresAsString);
            alphaCinemaContext.Genres.AddRange(genres);
            alphaCinemaContext.SaveChanges();

            //Fill Movies Table
            var moviesAsString = File.ReadAllText("../../../../AlphaCinemaData/Files/Movies.json");
            var movies = JsonConvert.DeserializeObject<List<Movie>>(moviesAsString);
            alphaCinemaContext.Movies.AddRange(movies);
            alphaCinemaContext.SaveChanges();

            //Fill MovieGenres Table
            var listOfMovies = alphaCinemaContext.Movies.Select(movie => movie).ToList();
            var listOfGenres = alphaCinemaContext.Genres.Select(genre => genre).ToList();
            var indexOfMovies = new List<int>() { 0, 0, 1, 1, 1, 2, 2, 2, 2, 2 };
            var indexOfGenres = new List<int>() { 0, 1, 2, 3, 4, 0, 1, 2, 3, 4 };
            for (int i = 0; i < 10; i++)
            {
                var movieGenre = new MovieGenre
                {
                    Movie = listOfMovies[indexOfMovies[i]],
                    Genre = listOfGenres[indexOfGenres[i]]
                };
                alphaCinemaContext.MoviesGenres.Add(movieGenre);
            }
            alphaCinemaContext.SaveChanges();

            //Fill OpenHours Table
            var openHoursAsString = File.ReadAllText("../../../../AlphaCinemaData/Files/OpenHours.json");
            var openHours = JsonConvert.DeserializeObject<List<OpenHour>>(openHoursAsString);
            alphaCinemaContext.OpenHours.AddRange(openHours);
            alphaCinemaContext.SaveChanges();

            //Fill Cities Table
            var citiesAsString = File.ReadAllText("../../../../AlphaCinemaData/Files/Cities.json");
            var cities = JsonConvert.DeserializeObject<List<City>>(citiesAsString);
            alphaCinemaContext.Cities.AddRange(cities);
            alphaCinemaContext.SaveChanges();

            //Fill Projections Table
            listOfMovies = alphaCinemaContext.Movies.Select(movie => movie).ToList();
            var listOfCities = alphaCinemaContext.Cities.Select(city => city).ToList();
            var listOfOpenHours = alphaCinemaContext.OpenHours.Select(openHour => openHour).ToList();
            indexOfMovies = new List<int>() { 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 1, 1, 1, 2, 2, 2, 2, 2, 2, 2 };
            var indexOfCities = new List<int>() { 0, 1, 1, 1, 4, 1, 1, 2, 3, 4, 4, 4, 2, 2, 1, 0, 1, 2, 3, 4 };
            var indexOfOpenHours = new List<int>() { 0, 1, 2, 3, 4, 0, 4, 2, 3, 4, 1, 2, 3, 0, 1, 2, 4, 3, 2, 0 };
            Random rnd = new Random();
            for (int i = 0; i < 20; i++)
            {
                var projection = new Projection
                {
                    Movie = listOfMovies[indexOfMovies[i]],
                    City = listOfCities[indexOfCities[i]],
                    OpenHour = listOfOpenHours[indexOfOpenHours[i]],
                    Date = DateTime.Now.AddDays(-rnd.Next(2))
                };
                alphaCinemaContext.Projections.Add(projection);
                alphaCinemaContext.SaveChanges();
            }
            alphaCinemaContext.SaveChanges();

            //Fill WatchedMovies Table
            var listOfProjections = alphaCinemaContext.Projections.Select(projection => projection).ToList();
            var listOfUsers = alphaCinemaContext.Users.Select(user => user).ToList();
            var indexOfPojections = new List<int>() { 0, 0, 1, 1, 2, 2, 3, 4, 5, 6, 7, 8, 9, 9, 10, 11, 12, 13, 14, 14, 15, 16, 17, 18, 19 };
            var indexOfUsers = new List<int>() { 0, 1, 2, 0, 0, 1, 1, 2, 0, 1, 2, 0, 0, 1, 1, 2, 0, 1, 2, 0, 1, 0, 2, 2, 0 };
            for (int i = 0; i < 20; i++)
            {
                var watchedMovie = new WatchedMovie
                {
                    Projection = listOfProjections[indexOfPojections[i]],
                    User = listOfUsers[indexOfUsers[i]]
                };
                alphaCinemaContext.WatchedMovies.Add(watchedMovie);
            }
            alphaCinemaContext.SaveChanges();
        }
    }
}
