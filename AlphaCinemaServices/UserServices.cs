using AlphaCinemaData.Models;
using AlphaCinemaData.Repository;
using AlphaCinemaServices.Contracts;
using System.Collections.Generic;
using System.Linq;

namespace AlphaCinemaServices
{
    public class UserServices : IUserServices
    {
        private readonly IRepository<User> repository;

        public UserServices(IRepository<User> repository)
        {
            this.repository = repository;
        }

        public string GetID(string userName)
        {
            var id = this.repository.All()
                .Where(user => user.Name == userName)
                .Select(user => user.Id)
                .FirstOrDefault();

            return id.ToString();
        }

        public List<string> GetUsersNames()
        {
            var users = this.repository.All()
                .Select(user => user.Name)
                .ToList();

            return users;
        }
    }
}
