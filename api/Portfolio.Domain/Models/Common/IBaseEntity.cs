namespace Portfolio.Domain.Models.Common;

public interface IBaseEntity<TKey>
{
    TKey Id { get; set; }
}

public interface IBaseEntity: IBaseEntity<int>
{
}
