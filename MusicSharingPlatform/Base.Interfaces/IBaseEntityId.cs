namespace Base.Interfaces;

public interface IBaseEntityId<TKey>
{
    TKey Id { get; set; }
}