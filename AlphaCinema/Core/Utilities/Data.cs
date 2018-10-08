using AlphaCinema.Core.Contracts;
using AlphaCinemaData.Context;
using AlphaCinemaData.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

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

            //Fill Movies Table
            var moviesAsString = File.ReadAllText("../../../../AlphaCinemaData/Files/Movies.json");
            var movies = JsonConvert.DeserializeObject<List<Movie>>(moviesAsString);
            alphaCinemaContext.Movies.AddRange(movies);
            alphaCinemaContext.SaveChanges();

            //Fill Genres Table
            var genresAsString = File.ReadAllText("../../../../AlphaCinemaData/Files/Genres.json");
            var genres = JsonConvert.DeserializeObject<List<Genre>>(genresAsString);
            alphaCinemaContext.Genres.AddRange(genres);
            alphaCinemaContext.SaveChanges();

            //Fill OpenHours Table
            var openHoursAsString = File.ReadAllText("../../../../AlphaCinemaData/Files/OpenHours.json");
            var openHours = JsonConvert.DeserializeObject<List<OpenHour>>(openHoursAsString);
            alphaCinemaContext.OpenHours.AddRange(openHours);
            alphaCinemaContext.SaveChanges();

            //Random rnd = new Random();
            //foreach (var user in users)
            //{
            //    user.CreatedOn = DateTime.Now.AddDays(-rnd.Next(30));
            //}

            //var pesho = JObject.Parse(@"{""Name"":""Borisov""}");
            //Console.WriteLine(pesho["Name"]);
        }
    }
}
