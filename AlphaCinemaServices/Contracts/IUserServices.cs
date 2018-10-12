using System;
using System.Collections.Generic;
using System.Text;

namespace AlphaCinemaServices.Contracts
{
    public interface IUserServices
    {
		int GetID(string userName);

        List<string> GetUsersNames();
    }
}
