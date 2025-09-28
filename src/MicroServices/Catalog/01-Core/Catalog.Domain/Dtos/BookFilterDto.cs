namespace Catalog.Domain.Dtos;

public record BookFilterDto
{
    public string Title { get; init; } = default!;
    public string Author { get; init; } = default!;
    public string Publisher { get; init; } = default!;
    public string Category { get; init; } = default!;
    public string ISBN { get; init; } = default!;
};

