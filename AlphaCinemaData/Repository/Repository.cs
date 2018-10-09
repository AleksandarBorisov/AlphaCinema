﻿using AlphaCinemaData.Context;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore;
using AlphaCinemaData.Models.Contracts;

namespace AlphaCinemaData.Repository
{
    public class Repository<T> : IRepository<T> where T : class, IDeletable
    {
        private readonly AlphaCinemaContext context;

        public Repository(AlphaCinemaContext context)
        {
            this.context = context;
        }

        public IEnumerable<T> All()
        {
            return this.context.Set<T>().Where(x => !x.IsDeleted);
        }

        public IEnumerable<T> AllAndDeleted()
        {
            return this.context.Set<T>();
        }

        public void Add(T entity)
        {
            EntityEntry entry = this.context.Entry(entity);

            if (entry.State != EntityState.Detached)
            {
                entry.State = EntityState.Added;
            }
            else
            {
                this.context.Set<T>().Add(entity);
            }
        }

        public void Delete(T entity)
        {
            entity.IsDeleted = true;
            entity.DeletedOn = DateTime.Now;

            var entry = this.context.Entry(entity);
            entry.State = EntityState.Modified;
        }

        public void Update(T entity)
        {
            EntityEntry entry = this.context.Entry(entity);
            if (entry.State == EntityState.Detached)
            {
                this.context.Set<T>().Attach(entity);
            }

            entry.State = EntityState.Modified;
        }

        public void Save()
        {
            this.context.SaveChanges();
        }

    }
}
