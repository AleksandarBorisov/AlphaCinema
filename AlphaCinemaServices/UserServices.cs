using AlphaCinemaData.Models;
using AlphaCinemaData.UnitOfWork;
using AlphaCinemaServices.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using AlphaCinemaServices.Exceptions;
using AlphaCinemaData.ViewModels;

namespace AlphaCinemaServices
{
	public class UserServices : IUserServices
	{
		private readonly IUnitOfWork unitOfWork;
		private User user;

		public UserServices(IUnitOfWork unitOfWork)
		{
			this.unitOfWork = unitOfWork;
		}

		public int GetID(string userName, int age)
		{
			user = IfExist(userName, age);

			if (user == null || user.IsDeleted)
			{//Ако няма такова или е изтрито
				throw new EntityDoesntExistException($"StartHour {userName} is not present in the database.");
			}
			return user.Id;
		}

		public User AddNewUser(string name, int age)
		{
			user = IfExist(name, age);
			if (name.Length > 50)
			{
				throw new ArgumentException("User Name can't be more than 50 characters");
			}

			if (age < 0)
			{
				throw new ArgumentException("Age can't be negative");
			}

			if (user != null)
			{
				if (user.IsDeleted)
				{
					user.IsDeleted = false;
				}
				return user;
			}
			user = new User()
			{
				Name = name,
				Age = age
			};

			this.unitOfWork.Users.Add(user);
			this.unitOfWork.SaveChanges();

            return user;
		}

		public ICollection<ProjectionDetailsViewModel> GetProjectionsByUserID(int userID)
		{
            var projections = this.unitOfWork.Users.All()
                .Where(us => us.Id == userID)
                .SelectMany(us => us.WatchedMovies)
                    .Select(wm => wm.Projection)
                        .Select(pr => new ProjectionDetailsViewModel
                        {
                            CityName = pr.City.Name,
                            MovieName = pr.Movie.Name,
                            Hour = pr.OpenHour.StartHour,
                        })
                        .ToList();
            
            return projections;
		}

        public ICollection<ProjectionDetailsViewModel> GetMoviesByUserAge(int minAge, int maxAge)
        {
            if (minAge < 0)
            {
                throw new ArgumentException("The minimum Age must be non-negative number");
            }
            if (maxAge < 0)
            {
                throw new ArgumentException("The maximum Age must be non-negative number");
            }

            var movieInfo = this.unitOfWork.Users.All()
                .Where(user => user.Age >= minAge && user.Age <= maxAge)
                .SelectMany(user => user.WatchedMovies)//За всеки user взимаме всички гледани прожекции
                .Select(watchetMovie => watchetMovie.Projection)//За всяка гледана прожекция на филм взимаме отговарящата и прожекция
                .Select(projection => new ProjectionDetailsViewModel//Накрая от всяка прожекция взимаме данните, които ни трябват
                {
                    CityName = projection.City.Name,
                    MovieName = projection.Movie.Name,
                    Hour = projection.OpenHour.StartHour
                })
                .ToList();

            return movieInfo;
        }

		private User IfExist(string userName, int age)
		{
			return this.unitOfWork.Users.AllAndDeleted()
				.Where(user => user.Name == userName)
				.FirstOrDefault();
		}

		public HashSet<User> GetUsers()
		{
			return this.unitOfWork.Users.All()
				.ToHashSet();
		}
	}
}