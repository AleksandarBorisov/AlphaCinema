using System;
using System.Collections.Generic;
using System.Text;

namespace AlphaCinemaServices.Contracts
{
    public interface IUserServices
    {
        string GetID(string userName);

        List<string> GetUsersNames();
    }
}
