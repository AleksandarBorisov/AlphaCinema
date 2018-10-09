using System;

namespace AlphaCinemaData.Models.Contracts
{
    public interface IAuditable
    {
        DateTime? CreatedOn { get; set; }

        DateTime? ModifiedOn { get; set; }
    }
}
