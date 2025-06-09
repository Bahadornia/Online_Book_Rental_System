using Catalog.Domain.Dtos;
using Catalog.Domain.IRepositories;
using Catalog.Domain.IServices;
using Catalog.Domain.Models.BookAggregate.Entities;
using StackExchange.Redis;
using System.Text.Json;

namespace Catalog.Infrastructure.Repositories
{
    internal class CachedBookRepository : IBookRepository
    {
        private const string HASH_KEY = "CATALOG_APP";
        private const string KEY = "BOOKS";
        private readonly IBookRepository _bookRepository;
        private readonly IDatabase _db;

        public CachedBookRepository(IBookRepository bookRepository, IConnectionMultiplexer redis)
        {
            _bookRepository = bookRepository;
            _db = redis.GetDatabase(2);
        }

        async Task IBookRepository.AddBook(Book book, CancellationToken ct)
        {
            await _db.HashDeleteAsync(HASH_KEY, KEY);
            await _bookRepository.AddBook(book, ct);
        }

        Task IBookRepository.DeleteBook(BookDto book, CancellationToken ct)
        {
            throw new NotImplementedException();
        }

        async Task<IReadOnlyCollection<BookDto>> IBookRepository.GetAll(CancellationToken ct)
        {

            var value = await _db.HashGetAsync(HASH_KEY, KEY);

            if (!string.IsNullOrWhiteSpace(value))
            {
                var rs = JsonSerializer.Deserialize<IReadOnlyCollection<BookDto>>(value);
                return rs;
            }

            var books  = await _bookRepository.GetAll(ct);

            await _db.HashSetAsync(HASH_KEY, KEY, JsonSerializer.Serialize(books));
            return books;
        }

        async Task<BookDto> IBookRepository.GetBookById(long id, CancellationToken ct)
        {
            var book = await _bookRepository.GetBookById(id, ct);
            return book;
        }

        Task<IReadOnlyCollection<BookDto>> IBookRepository.SearchBook(BookFilterDto filter, CancellationToken ct)
        {
            throw new NotImplementedException();
        }

        Task IBookRepository.UpdateBook(BookDto book, CancellationToken ct)
        {
            throw new NotImplementedException();
        }
    }
}
