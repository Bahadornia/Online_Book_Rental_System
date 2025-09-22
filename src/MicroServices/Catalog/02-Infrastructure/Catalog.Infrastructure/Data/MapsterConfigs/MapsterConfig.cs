using Catalog.Domain.Dtos;
using Catalog.Domain.Models.BookAggregate.Entities;
using Mapster;

namespace Catalog.Infrastructure.Data.MapsterConfigs;

public class MapsterConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<Book, BookDto>()
            .Map(d => d.PublisherId, s => s.PublisherId.Value)
            .Map(d => d.CategoryId, s => s.CategoryId.Value)
            .Map(d => d.ISBN, s => s.ISBN);
    }
}
