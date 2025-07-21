namespace Order.Domain.Dtos
{
    public record AddBookOrderDto(long Id, long UserId, long BookId, DateTime BorrowDate);
    
      
}
