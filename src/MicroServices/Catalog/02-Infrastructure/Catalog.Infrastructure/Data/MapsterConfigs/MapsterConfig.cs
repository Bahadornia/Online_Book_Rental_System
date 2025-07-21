using Catalog.Domain.Dtos;
using Catalog.Domain.Models.BookAggregate.Entities;
using Catalog.Domain.Models.BookAggregate.ValueObjects;
using Catalog.Infrastructure.Data.BookAggregate;
using Mapster;

namespace Catalog.Infrastructure.Data.MapsterConfigs;

public class MapsterConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<Book, BookData>()
             .Map<ISBN, string>(dest => dest.ISBN, src => src.ISBN.Value);
    }
}
