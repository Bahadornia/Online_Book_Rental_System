using Catalog.Domain.Dtos;
using Catalog.Domain.IRepositories;
using Catalog.Domain.IServices;
using Catalog.Domain.Models.BookAggregate.Entities;
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

    public AddBookCommandHandler(IBookRepository bookService, IMapper mapper, IFileService fileService, ISnowFlakeService sonowFlakeService, IDomainEventPublisher publisher, ILogger<AddBookCommandHandler> logger, IUnitOfWork unitOfWork, IIntegrationEventPublisher eventPublisher, IPubliserRepository publisherRepository, ICategoryRepository categroyRepository, IPublisherService publisherService, ICategoryService categoryService)
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
    }

    public async Task<Unit> Handle(AddBookCommand command, CancellationToken ct)
    {
        try
        {
            var bookDto = _mapper.Map<BookDto>(command);
            bookDto.Id = _sonowFlakeService.CreateId();
            var fileName = await UplodaImage(bookDto.Id, command.Title, command.Image, command.ContentType, ct);
            bookDto.ImageUrl = fileName;

            var book = Book.Create(bookDto.Id, bookDto.Title, bookDto.Author, bookDto.Publisher, bookDto.Category, bookDto.ISBN, bookDto.Description, bookDto.ImageUrl, bookDto.AvailableCopies);

            await _unitOfWork.BeginTransaction(ct);
            await _bookRepository.AddBook(book, ct);
            await _publisherService.AddIfPublisherNotExists(_unitOfWork.Session, book.Publisher, ct);
            await _categoryService.AddIfCategoryNotExists(_unitOfWork.Session, book.Category, ct);
          
            var domainEvents = book.ClearDomainEvents();
            var bookAddedEvent = new BookAddedIntegrationEvent
            {
                CorrelationId = Guid.NewGuid(),
                BookId = book.Id.Value,
                AvailableCopies = book.AvailableCopies,
            };
            await _eventPublisher.Publish<BookAddedIntegrationEvent>(bookAddedEvent, ct);

            _logger.LogAddBook(book.Id.Value);

            await _unitOfWork.CommitTransaction(ct);
        }
        catch (Exception)
        {
            await _unitOfWork.AbortTransaction(ct);
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
