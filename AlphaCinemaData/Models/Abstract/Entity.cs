using AlphaCinemaData.Models.Contracts;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AlphaCinemaData.Models.Abstract
{
    public abstract class Entity : IDeletable
    {

		public int Id { get; set; }
		public bool IsDeleted { get; set; }

    }
}
