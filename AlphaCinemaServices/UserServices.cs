using AlphaCinemaData.Models;
using AlphaCinemaData.UnitOfWork;
using AlphaCinemaServices.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AlphaCinemaServices
{
    public class UserServices : IUserServices
    {
        private readonly IUnitOfWork unitOfWork;

        public UserServices(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public string GetID(string userName)
        {
            var id = this.unitOfWork.Users.All()
                .Where(user => user.Name == userName)
                .Select(user => user.Id)
                .FirstOrDefault();

            return id.ToString();
        }

        public List<string> GetUsersNames()
        {
            var users = this.unitOfWork.Users.All()
                .Select(user => user.Name)
                .ToList();

            return users;
        }

        public User AddNewUser(string name, int age)
        {
            if (name.Length > 50)
            {
                throw new ArgumentException("User Name can't be more than 50 characters");
            }

            if (age < 0)
            {
                throw new ArgumentException("Age can't be negative");
            }

            var user = IfExist(name);

            if (user != null)
            {
                user.IsDeleted = false;
                this.unitOfWork.SaveChanges();
                return user;
            }
            else
            {
                var newUser = new User()
                {
                    Name = name,
                    Age = age
                };
                this.unitOfWork.Users.Add(newUser);
                this.unitOfWork.SaveChanges();
                return newUser;
            }

        }

        private User IfExist(string userName)
        {
            return this.unitOfWork.Users.AllAndDeleted()
                .Where(user => user.Name == userName)
                .FirstOrDefault();
        }
    }
}
