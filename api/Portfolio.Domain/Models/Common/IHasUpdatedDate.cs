using System;

namespace Portfolio.Domain.Models.Common
{
    public interface IHasUpdatedDate
    {
        #region Properties

        public DateTime UpdatedAtUtc { get; set; }

        #endregion
    }
}
