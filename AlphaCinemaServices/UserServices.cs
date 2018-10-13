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

		public UserServices(IUnitOfWork unitOfWork)
		{
			this.unitOfWork = unitOfWork;
		}

		public int GetID(string userName)
		{
			if (IfExist(userName) == null && IsDeleted(userName))
			{
				throw new EntityDoesntExistException($"\n{userName} is not present in the database.");
			}
			var id = this.unitOfWork.Users.All()
				.Where(user => user.Name == userName)
				.Select(user => user.Id)
				.FirstOrDefault();

			return id;
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
				throw new EntityAlreadyExistsException("You have already booked reservation");
			}
			var newUser = new User()
			{
				Name = name,
				Age = age
			};
			this.unitOfWork.Users.Add(newUser);
			this.unitOfWork.SaveChanges();
			return newUser;

		}

		public List<int> GetProjectionsIDsByUserID(int userID)
		{
			var projectionsIDs = this.unitOfWork.Users.All()
				.Where(us => us.Id == userID)
				.Select(us => us.WatchedMovies
					.Select(wm => wm.ProjectionId)
					.ToList())
				.FirstOrDefault();

			return projectionsIDs;
		}

        public List<ProjectionDetailsViewModel> GetMoviesByUserAge(int minAge, int maxAge)
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

		private User IfExist(string userName)
		{
			return this.unitOfWork.Users.AllAndDeleted()
				.Where(user => user.Name == userName)
				.FirstOrDefault();
		}

		private bool IsDeleted(string userName)
		{
			var result = this.unitOfWork.Users.AllAndDeleted()
				.Where(u => u.Name == userName)
				.FirstOrDefault()
				.IsDeleted;
			return result;
		}
	}
}