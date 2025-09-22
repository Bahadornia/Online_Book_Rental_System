using Catalog.Domain.Dtos;
using Catalog.Domain.IRepositories;
using Catalog.Domain.IServices;
using Catalog.Domain.Models.BookAggregate.Entities;
using Catalog.Infrastructure.Data;
using Framework.CQRS;
using Framework.Domain;
using Framework.SnowFlake;
using MapsterMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using SharedKernel.Messaging;
using SharedKernel.Messaging.Events;

namespace Catalog.ApplicationServices.Commands;

public class AddBookCommandHandler : ICommandHandler<AddBookCommand>
{
    private readonly IBookRepository _bookRepository;
    private readonly IPubliserRepository _publisherRepository;
    private readonly ICategoryRepository _categroyRepository;
    private readonly IPublisherService _publisherService;
    private readonly ICategoryService _categoryService;
    private readonly IMapper _mapper;
    private readonly IFileService _fileService;
    private readonly ISnowFlakeService _sonowFlakeService;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<AddBookCommandHandler> _logger;
    private readonly IIntegrationEventPublisher _eventPublisher;
    private readonly CatalogDbContext _dbContext;

    public AddBookCommandHandler(
        IBookRepository bookService, IMapper mapper, IFileService fileService,
        ISnowFlakeService sonowFlakeService, IDomainEventPublisher publisher,
        ILogger<AddBookCommandHandler> logger, IUnitOfWork unitOfWork,
        IIntegrationEventPublisher eventPublisher,
        IPubliserRepository publisherRepository,
        ICategoryRepository categroyRepository,
        IPublisherService publisherService,
        ICategoryService categoryService, CatalogDbContext dbContext)
    {
        _bookRepository = bookService;
        _mapper = mapper;
        _fileService = fileService;
        _sonowFlakeService = sonowFlakeService;
        _logger = logger;
        _unitOfWork = unitOfWork;
        _eventPublisher = eventPublisher;
        _publisherRepository = publisherRepository;
        _categroyRepository = categroyRepository;
        _publisherService = publisherService;
        _categoryService = categoryService;
        _dbContext = dbContext;
    }

    public async Task<Unit> Handle(AddBookCommand command, CancellationToken ct)
    {
        var bookDto = _mapper.Map<BookDto>(command);
        var bookId = _sonowFlakeService.CreateId();
        var publisherId = bookDto.PublisherId;
        var categoryId = bookDto.CategoryId;
        await using var transaction = await _dbContext.Database.BeginTransactionAsync(ct);
        try
        {
            var fileName = await UplodaImage(bookDto.Id, command.Title, command.Image, command.ContentType, ct);
            bookDto.ImageUrl = fileName;

            var book = Book.Create(bookId, bookDto.Title, bookDto.Author, bookDto.PublisherId, bookDto.CategoryId, bookDto.ISBN, bookDto.Description, bookDto.ImageUrl, bookDto.AvailableCopies);
            _bookRepository.AddBook(book, ct);

            if (publisherId == 0)
                await _publisherService.AddIfPublisherNotExists(bookDto.PublisherName, ct);

            if (categoryId == 0)
                await _categoryService.AddIfCategoryNotExists(bookDto.CategoryName, ct);

            var domainEvents = book.ClearDomainEvents();
            var bookAddedEvent = new BookAddedIntegrationEvent
            {
                CorrelationId = Guid.CreateVersion7(),
                BookId = book.Id.Value,
                AvailableCopies = book.AvailableCopies,
            };
            await _eventPublisher.Publish<BookAddedIntegrationEvent>(bookAddedEvent, ct);


            await _unitOfWork.SaveChangesAsync(ct);
            await transaction.CommitAsync(ct);
            _logger.LogAddBook(book.Id.Value);
        }
        catch (Exception)
        {
            await transaction.RollbackAsync(ct);
            throw;
        }
        return Unit.Value;
    }

    private async Task<string> UplodaImage(long id, string title, byte[] image, string contentType, CancellationToken ct)
    {
        var extension = contentType.Split('/')[1];
        var fileUrl = $"{id}/{title}.{extension}";
        await _fileService.UploadFileAsync(image, fileUrl, contentType, ct);
        return fileUrl;
    }
}
