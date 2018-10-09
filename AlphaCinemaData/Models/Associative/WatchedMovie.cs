using AlphaCinemaData.Models.Contracts;
using System;

namespace AlphaCinemaData.Models.Associative
{
    public class WatchedMovie : IDeletable
	{
        public Guid UserId { get; set; }

        public Guid ProjectionId { get; set; }

        public User User { get; set; }
        public Projection Projection { get; set; }

		public bool IsDeleted { get; set; }

		public DateTime? DeletedOn { get; set; }
	}
}
