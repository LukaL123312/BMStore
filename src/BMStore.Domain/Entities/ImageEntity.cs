namespace BMStore.Domain.Entities;

public record ImageEntity
{
    public int Id { get; init; }
    public string ImageUrl { get; init; } = string.Empty;
}
