using System;
using System.Linq;
using System.Reflection;
using AlphaCinema.Core;
using AlphaCinema.Core.Commands;
using AlphaCinema.Core.Commands.Factory;
using AlphaCinema.Core.Contracts;
using AlphaCinema.Core.Utilities;
using AlphaCinemaData.Context;
using AlphaCinemaData.Models.Associative;
using AlphaCinemaData.Repository;
using AlphaCinemaData.UnitOfWork;
using AlphaCinemaServices;
using AlphaCinemaServices.Contracts;
using Autofac;

namespace AlphaCinema.Config
{
   public class Container : Autofac.Module
    {
		public Container() { }
        // Метода, който се изппълява когато модула се зареди
        protected override void Load(ContainerBuilder builder)
        {
            this.RegisterCoreComponents(builder);
            this.RegisterMenus(builder);
            base.Load(builder);
        }

        private void RegisterMenus(ContainerBuilder builder)
        {
            var assembly = Assembly.GetExecutingAssembly();

            var commandTypes = assembly.DefinedTypes
                .Where(typeInfo =>
                    typeInfo.ImplementedInterfaces.Contains(typeof(ICommand)))
                .ToList();
            // Намираме всички класове които имплементират ICommand
            foreach (var commandType in commandTypes)
            {
                builder.RegisterType(commandType.AsType())
                    .Named<ICommand>(commandType.Name);
            }
            // Регистрираме всяка команда с името на класа си
        }

        private void RegisterCoreComponents(ContainerBuilder builder)
        {
            builder.RegisterType<Engine>().As<IEngine>().SingleInstance();
            builder.RegisterType<CommandProcessor>().As<ICommandProcessor>().SingleInstance();
            builder.RegisterType<CommandFactory>().As<ICommandFactory>().SingleInstance();
            builder.RegisterType<ItemSelector>().As<IItemSelector>().SingleInstance();
			builder.RegisterType<AlphaCinemaContext>().AsSelf();
			builder.RegisterType<Data>().As<IData>();
            builder.RegisterType<CityServices>().As<ICityServices>();
			builder.RegisterType<MovieServices>().As<IMovieServices>();
			builder.RegisterType<OpenHourServices>().As<IOpenHourServices>();
			builder.RegisterType<ProjectionServices>().As<IProjectionsServices>();
            builder.RegisterType<GenreServices>().As<IGenreServices>();
            builder.RegisterType<MovieGenreServices>().As<IMovieGenreServices>();
            builder.RegisterType<UserServices>().As<IUserServices>();
            builder.RegisterType<WatchedMovieServices>().As<IWatchedMovieServices>();
			builder.RegisterType<CommandHandler>().As<ICommandHandler>();
			builder.RegisterType<AlphaCinemaConsole>().As<IAlphaCinemaConsole>();
			builder.RegisterType<UnitOfWork>().As<IUnitOfWork>();

		}
    }
}
