using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AlphaCinemaData.Models
{
	public class Movies
	{
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		[Key]
		public Guid UserId { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public int ReleaseYear { get; set; }
		public int Duration { get; set; }

		public Movies()
		{

		}
	}
}
