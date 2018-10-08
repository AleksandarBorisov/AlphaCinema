using AlphaCinemaData.Context;
using AlphaCinemaData.Models;
using AlphaCinemaServices.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AlphaCinemaServices
{
	public class CityServices : ICityServices
	{
		private readonly IAlphaCinemaContext context;

		public CityServices(IAlphaCinemaContext context)
		{
			this.context = context;
		}

		public IList<Guid> GetId()
		{
			var cityId = context.Cities.Select(x => x.Id).ToList();
			return cityId;
		}
	}
}
