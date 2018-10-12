using AlphaCinemaData.Models;
using AlphaCinemaData.UnitOfWork;
using AlphaCinemaServices.Contracts;
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

		public int GetID(string userName)
        {
            var id = this.unitOfWork.Users.All()
                .Where(user => user.Name == userName)
                .Select(user => user.Id)
                .FirstOrDefault();

            return id;
        }

        public List<string> GetUsersNames()
        {
            return this.unitOfWork.Users.All()
                .Select(user => user.Name)
                .ToList();

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

        public User AddNewUser(string name, int age)
        {
            throw new System.NotImplementedException();
        }
    }
}
