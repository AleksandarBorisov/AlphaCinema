using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace AlphaCinemaData.Models.Associative
{
    public class WatchedMovie
    {
        //[ForeignKey("User")]
        public Guid UserId { get; set; }

       // [ForeignKey("Projection")]
        public Guid ProjectionId { get; set; }

        public User User { get; set; }
        public Projection Projection { get; set; }
    }
}
