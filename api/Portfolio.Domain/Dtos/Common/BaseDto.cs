namespace Portfolio.Domain.Dtos.Common
{
    public abstract class BaseDto<TKey> : IBaseDto<TKey>
    {
        #region Properties

        public virtual TKey Id { get; set; }

        #endregion
    }

    public abstract class BaseDto: BaseDto<int>
    {

    }
}
