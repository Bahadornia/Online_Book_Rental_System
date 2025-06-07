using Catalog.Domain.Dtos;
using Catalog.Infrastructure.Data.BookAggregate;
using Mapster;

namespace Catalog.Infrastructure.Data.MapsterConfigs;

public class MapsterConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<BookData, BookDto>()
            .Map(dest => dest.Publisher, src => src.Publisher.Name)
            .Map(dest => dest.Category, src => src.Category.Name);
    }
}
