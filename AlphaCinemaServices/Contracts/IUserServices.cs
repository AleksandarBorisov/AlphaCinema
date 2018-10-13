using System;
using System.Collections.Generic;
using System.Text;
using AlphaCinemaData.Models;
using AlphaCinemaData.ViewModels;

namespace AlphaCinemaServices.Contracts
{
    public interface IUserServices
    {
		int GetID(string userName);

        User AddNewUser(string name, int age);

		List<int> GetProjectionsIDsByUserID(int userID);

        List<ProjectionDetailsViewModel> GetMoviesByUserAge(int minAge, int maxAge);
    }
}
