using AlphaCinemaData.Context;
using AlphaCinemaData.Models;
using AlphaCinemaData.Models.Associative;
using AlphaCinemaData.Models.Contracts;
using AlphaCinemaData.Repository;
using System;
using System.Collections.Generic;

namespace AlphaCinemaData.UnitOfWork
{
	public class UnitOfWork : IUnitOfWork
	{
		private readonly AlphaCinemaContext context;
		private readonly Dictionary<Type, object> repos = new Dictionary<Type, object>();

		public IRepository<City> Cities
		{
			get
			{
				return this.GetRepository<City>();
			}
		}

		public IRepository<Genre> Genres
		{
			get
			{
				return this.GetRepository<Genre>();
			}
		}

		public IRepository<Movie> Movies
		{
			get
			{
				return this.GetRepository<Movie>();
			}
		}

		public IRepository<OpenHour> OpenHours
		{
			get
			{
				return this.GetRepository<OpenHour>();
			}
		}

		public IRepository<User> Users
		{
			get
			{
				return this.GetRepository<User>();
			}
		}

		public IRepository<MovieGenre> MovieGenres
		{
			get
			{
				return this.GetRepository<MovieGenre>();
			}
		}

		public IRepository<Projection> Projections
		{
			get
			{
				return this.GetRepository<Projection>();
			}
		}

		public IRepository<WatchedMovie> WatchedMovies
		{
			get
			{
				return this.GetRepository<WatchedMovie>();
			}
		}

		public UnitOfWork(AlphaCinemaContext context)
		{
			this.context = context;
		}

		public int SaveChanges()
		{
			return this.context.SaveChanges();
		}

		private IRepository<T> GetRepository<T>() where T : class, IDeletable
		{
			var repoType = typeof(Repository<T>);

			if (!repos.ContainsKey(repoType))
			{
				var repo = Activator.CreateInstance(repoType, this.context);
				repos[repoType] = repo;
			}

			return (IRepository<T>)repos[repoType];
		}
	}
}