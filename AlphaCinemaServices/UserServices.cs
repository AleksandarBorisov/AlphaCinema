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
    }
}
