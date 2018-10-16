using System.Collections.Generic;
using AlphaCinemaData.Models;
using AlphaCinemaData.ViewModels;

namespace AlphaCinemaServices.Contracts
{
    public interface IUserServices
    {
		int GetID(string userName);
        User AddNewUser(string name, int age);
		ICollection<ProjectionDetailsViewModel> GetProjectionsByUserID(int userID);
		ICollection<ProjectionDetailsViewModel> GetMoviesByUserAge(int minAge, int maxAge);
        User GetUser(string userName);
        HashSet<User> GetUsers();
	}
}
