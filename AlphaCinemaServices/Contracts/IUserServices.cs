using System.Collections.Generic;
using AlphaCinemaData.Models;
using AlphaCinemaData.ViewModels;

namespace AlphaCinemaServices.Contracts
{
    public interface IUserServices
    {
		int GetID(string userName);

        User AddNewUser(string name, int age);

        List<ProjectionDetailsViewModel> GetProjectionsByUserID(int userID);

        List<ProjectionDetailsViewModel> GetMoviesByUserAge(int minAge, int maxAge);
		bool IfExist(string userName);
		bool IsDeleted(string userName);
		User GetUserByID(int userID);

	}
}
