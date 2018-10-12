using AlphaCinemaData.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace AlphaCinemaServices.Contracts
{
    public interface IUserServices
    {
		int GetID(string userName);

        User AddNewUser(string name, int age);

		List<int> GetProjectionsIDsByUserID(int userID);



	}
}
