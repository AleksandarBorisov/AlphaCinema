using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using AlphaCinema.Core;
using AlphaCinema.Core.Commands.Factory;
using AlphaCinema.Core.Contracts;
using AlphaCinema.Core.Utilities;
using AlphaCinemaData.Context;
using AlphaCinemaData.Repository;
using AlphaCinemaServices;
using AlphaCinemaServices.Contracts;
using Autofac;

namespace AlphaCinema.Config
{
   public class Container : Autofac.Module
    {
        public Container()
        {

        }
        // Метода, който се изппълява когато модула се зареди
        protected override void Load(ContainerBuilder builder)
        {
            this.RegisterCoreComponents(builder);
            this.RegisterMenus(builder);
            //base.Load(builder);
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
            builder.RegisterGeneric(typeof(Repository<>)).As(typeof(IRepository<>));
            builder.RegisterType<CityServices>().As<ICityServices>();
			builder.RegisterType<MovieServices>().As<IMovieServices>();
            
            // Пичове тука ще регистрираме нещата от Core
        }
    }
}
