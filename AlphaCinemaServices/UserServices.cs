using AlphaCinemaData.Models;
using AlphaCinemaData.UnitOfWork;
using AlphaCinemaServices.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using AlphaCinemaServices.Exceptions;

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


		private User IfExist(string userName)
		{
			return this.unitOfWork.Users.AllAndDeleted()
				.Where(user => user.Name == userName)
				.FirstOrDefault();
		}
	}
}