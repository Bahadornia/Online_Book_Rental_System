using Framework.Domain;
using Inventory.Domain.Models.InventoryAggregate.Events;
using Inventory.Domain.Models.InventoryAggregate.ValueObjects;

namespace Inventory.Domain.Models.InventoryAggregate.Entities;

public class BookInventory : AggregateRoot<BookId>
{
    public int TotalCopies { get; set; }
    public int AvailableCopies { get; set; }

    private BookInventory() { }

    public static BookInventory Create(long bookId, int totalCopies, int availabeCopies)
    {
        var bookInventory = new BookInventory
        {
            Id = bookId,
            TotalCopies = totalCopies,
            AvailableCopies = availabeCopies,
        };
        bookInventory.Emit(new InventoryAdded(bookInventory));
        return bookInventory;
    }

    public void Update(long bookId, int totalCopies, int availabeCopies)
    {
        Id = bookId;
        TotalCopies = totalCopies;
        AvailableCopies = availabeCopies;
        Emit(new InventoryUpdated(this));
    }

    public void DecreaseInventory()
    {
        AvailableCopies--;
        Emit(new InventoryDecreased(this));
    }
    public void IncreaseInventory()
    {
        AvailableCopies++;
        Emit(new InventoryIncreased(this));
    }
    protected override void ValidateInvariants()
    {
        if(AvailableCopies < 0)
        {
            throw new Exception("Available copies are zero!");
        }
        
    }
}
