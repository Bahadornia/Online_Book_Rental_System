using Mapster;
using System.Reflection;

namespace Library.Repository.Infrastructure;

public class MapsterConfig
{
    public static void RegisterMapsterConfigurations()
    {
        var config = TypeAdapterConfig.GlobalSettings;

        // Apply all mappings from assemblies
        config.Scan(Assembly.GetExecutingAssembly());
    }
}
