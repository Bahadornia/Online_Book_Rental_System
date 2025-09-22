namespace Framework.Exceptions;

public sealed class NotFoundException : Exception
{
    public NotFoundException(long id, string entity) : base($"{entity} with id: {id} not found.")
    {
    }
}
