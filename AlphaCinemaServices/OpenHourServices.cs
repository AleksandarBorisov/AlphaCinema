using AlphaCinemaData.Context;
using AlphaCinemaServices.Contracts;

namespace AlphaCinemaServices
{
	public class OpenHourServices : IOpenHourServices
	{
		private readonly AlphaCinemaContext context;

		public OpenHourServices(AlphaCinemaContext context)
		{
			this.context = context;
		}


	}
}
