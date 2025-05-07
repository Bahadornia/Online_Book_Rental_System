namespace Catalog.Domain.Models.BookAggregate.ValueObjects
{
    public record BookId
    {
        public Guid Value { get; private set; }
        private BookId(Guid id) {
            Value = id;
        }

        public static BookId Create(Guid id)
        {
            Validate(id);
            return new BookId(id);
            
        }

        private static void Validate(Guid value)
        {
          return;
            
        }

        public static implicit operator BookId(Guid id)
        {
            return Create(id);
        }

        public static implicit operator Guid(BookId id)
        {
            return id.Value;
        }
    }
    
}