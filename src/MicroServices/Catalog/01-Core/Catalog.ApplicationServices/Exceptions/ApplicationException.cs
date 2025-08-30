namespace Catalog.ApplicationServices.Exceptions;

internal class ApplicationException : Exception
{
    public ApplicationException(string? message) : base(message)
    {
    }
}
