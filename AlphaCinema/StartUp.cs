using System.Reflection;
using AlphaCinema.Core.Contracts;
using Autofac;

namespace AlphaCinema
{
   public class StartUp
    {
		public static void Main(string[] args)
        {
            var builder = new ContainerBuilder();
            builder.RegisterAssemblyModules(Assembly.GetExecutingAssembly());
            var container = builder.Build();
            var engine = container.Resolve<IEngine>();
			engine.Run();
        }
    }
}
