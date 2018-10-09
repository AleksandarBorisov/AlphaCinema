using System;
using System.Collections.Generic;
using System.Text;

namespace AlphaCinemaData.Models.Contracts
{
    public interface IAuditable
    {
        DateTime? CreatedOn { get; set; }

        DateTime? ModifiedOn { get; set; }
    }
}
