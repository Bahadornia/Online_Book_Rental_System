namespace Order.Domain.Dtos
{
    public record BookOrderDto(long Id, long UserId, long BookId, DateTime BorrowDate);
    
      
}
