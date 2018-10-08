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
