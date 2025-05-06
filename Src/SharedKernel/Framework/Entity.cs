namespace Framework;

public abstract class Entity<T>
    where T: notnull

{
    public T Id { get; set; } = default!;

    public override bool Equals(object? obj)
    {
        if (obj is null) return false;
        return obj is Entity<T> entity && entity.Id.Equals(Id) ;
        
    }

    public override int GetHashCode()
    {
        return Id.GetHashCode();
    }

}
