using AlphaCinemaData.Context;
using AlphaCinemaServices.Contracts;

namespace AlphaCinemaServices
{
	public class OpenHourServices : IOpenHourServices
	{
		private readonly IAlphaCinemaContext context;

		public OpenHourServices(IAlphaCinemaContext context)
		{
			this.context = context;
		}


	}
}
