namespace Rental.Domain.Dtos
{
    public record BookRentalDto(long Id, long UserId, long BookId, DateTime BorrowDate);
    
      
}
