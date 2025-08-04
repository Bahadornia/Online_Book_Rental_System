namespace Order.Domain.IServices;

public interface ICheckBooksOverDueDate
{
    void Execute(CancellationToken ct);
}
