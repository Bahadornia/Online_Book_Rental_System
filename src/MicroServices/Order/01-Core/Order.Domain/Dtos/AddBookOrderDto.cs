namespace Order.Domain.Dtos
{
    public record AddBookOrderDto(long Id, string UserId, long BookId, DateTime BorrowDate);
    
      
}
