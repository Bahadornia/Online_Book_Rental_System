namespace Catalog.Domain.Models.BookAggregate.ValueObjects
{
    public record BookId
    {
        public long Value { get; private set; }
        private BookId(long id) {
            Value = id;
        }

        public static BookId Create(long id)
        {
            Validate(id);
            return new BookId(id);
            
        }

        private static void Validate(long value)
        {
          return;
            
        }

        public static implicit operator BookId(long id)
        {
            return Create(id);
        }

        public static implicit operator long(BookId id)
        {
            return id.Value;
        }
    }
    
}