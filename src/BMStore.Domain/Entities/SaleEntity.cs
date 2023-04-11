namespace BMStore.Domain.Entities;

public record SaleEntity
{
    public int Id { get; init; }
    public string Name { get; init; } = string.Empty;
    public IReadOnlyCollection<ProductEntity>? ProductsOnSale { get; init; }
    public TimeSpan Duration { get; init; }
}
