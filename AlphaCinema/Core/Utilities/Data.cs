﻿using AlphaCinema.Core.Contracts;
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
            Clear(this.context);
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
            var listOfMovies = context.Movies.Select(movie => movie).ToList();
            var listOfGenres = context.Genres.Select(genre => genre).ToList();
            var indexOfMovies = new List<int>() { 0, 0, 1, 1, 1, 2, 2, 2, 2, 2 };
            var indexOfGenres = new List<int>() { 0, 1, 2, 3, 4, 0, 1, 2, 3, 4 };
            for (int i = 0; i < 10; i++)
            {
                var movieGenre = new MovieGenre
                {
                    Movie = listOfMovies[indexOfMovies[i]],
                    Genre = listOfGenres[indexOfGenres[i]]
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
            listOfMovies = context.Movies.Select(movie => movie).ToList();
            var listOfCities = context.Cities.Select(city => city).ToList();
            var listOfOpenHours = context.OpenHours.Select(openHour => openHour).ToList();
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
                context.Projections.Add(projection);
                context.SaveChanges();
            }
            context.SaveChanges();

            //Fill WatchedMovies Table
            var listOfProjections = context.Projections.Select(projection => projection).ToList();
            var listOfUsers = context.Users.Select(user => user).ToList();
            var indexOfPojections = new List<int>() { 0, 0, 1, 1, 2, 2, 3, 4, 5, 6, 7, 8, 9, 9, 10, 11, 12, 13, 14, 14, 15, 16, 17, 18, 19 };
            var indexOfUsers = new List<int>() { 0, 1, 2, 0, 0, 1, 1, 2, 0, 1, 2, 0, 0, 1, 1, 2, 0, 1, 2, 0, 1, 0, 2, 2, 0 };
            for (int i = 0; i < 20; i++)
            {
                var watchedMovie = new WatchedMovie
                {
                    Projection = listOfProjections[indexOfPojections[i]],
                    User = listOfUsers[indexOfUsers[i]]
                };
                context.WatchedMovies.Add(watchedMovie);
            }
            context.SaveChanges();
        }

		public void Clear(AlphaCinemaContext context)
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
	}
}
