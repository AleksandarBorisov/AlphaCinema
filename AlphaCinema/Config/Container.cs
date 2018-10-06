using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using AlphaCinema.Core;
using AlphaCinema.Core.Commands.Factory;
using AlphaCinema.Core.Contracts;
using AlphaCinema.Core.Utilities;
using Autofac;

namespace AlphaCinema.Config
{
    class Container : Autofac.Module
    {
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
            // Пичове тука ще регистрираме нещата от Core
        }
    }
}
