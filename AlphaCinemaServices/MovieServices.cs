﻿using AlphaCinemaData.Context;
using AlphaCinemaData.Models;
using AlphaCinemaData.Repository;
using AlphaCinemaData.UnitOfWork;
using AlphaCinemaServices.Contracts;
using AlphaCinemaServices.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AlphaCinemaServices
{
    public class MovieServices : IMovieServices
    {
        private readonly IUnitOfWork unitOfWork;

        public MovieServices(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public string GetID(string movieName)
        {
            var id = this.unitOfWork.Movies.All()
                .Where(m => m.Name == movieName)
                .Select(m => m.Id).FirstOrDefault();

            return id.ToString();
        }

        public List<string> GetMovieNames()
        {
            var movieNames = this.unitOfWork.Movies.All()
                .Select(movie => movie.Name)
                .ToList();

            return movieNames;
        }

        public void AddNewMovie(string name, string description, int releaseYear, int duration)
        {
            if (name.Length > 50)
            {
                throw new ArgumentException("Movie Name can't be more than 50 characters");
            }

            if (description.Length > 150)
            {
                throw new ArgumentException("Movie Description can't be more than 150 characters");
            }

            if (IfExist(name) && IsDeleted(name))
            {
                var genre = this.unitOfWork.Genres.All()
                    .FirstOrDefault(g => g.Name == name);
                genre.IsDeleted = false;
                this.unitOfWork.SaveChanges();
            }
            else if (IfExist(name) && !IsDeleted(name))
            {
                throw new EntityAlreadyExistsException("Movie is already present in the database.");
            }
            else
            {
                var movie = new Movie()
                {
                    Name = name,
                    Description = description,
                    ReleaseYear = releaseYear,
                    Duration = duration
                };
                this.unitOfWork.Movies.Add(movie);
                this.unitOfWork.SaveChanges();

            }
        }

        private bool IfExist(string movieName)
        {
            return this.unitOfWork.Movies.AllAndDeleted()
                .Where(movie => movie.Name == movieName)
                .FirstOrDefault() == null ? false : true;
        }

        private bool IsDeleted(string movieName)
        {
            return this.unitOfWork.Movies.AllAndDeleted()
                .Where(g => g.Name == movieName)
                .FirstOrDefault()
                .IsDeleted;
        }

        public List<string> GetMovieNamesByCityIDGenreID(
            string genreIDAsString,
            string cityIDAsString)
        {
            int genreID = int.Parse(genreIDAsString);
            int cityID = int.Parse(cityIDAsString);
            var movies = this.unitOfWork.Movies.All()
            .Where(movie => movie.MovieGenres.Any(mg => mg.GenreId == genreID))
            .Where(movie => movie.Projections.Any(p => p.CityId == cityID))
            .Select(movie => movie.Name).ToList();

            return movies;
        }
    }
}
