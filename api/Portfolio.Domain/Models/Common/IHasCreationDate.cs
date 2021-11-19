using System;

namespace Portfolio.Domain.Models.Common
{
    public interface IHasCreationDate
    {
        #region Properties

        public DateTime CreatedAtUTC { get; set; }

        #endregion
    }
}
