namespace Catalog.Infrastructure.Exceptions;

internal class InfrastructureException : Exception
{
    public InfrastructureException(string? message) : base(message)
    {
    }
}
