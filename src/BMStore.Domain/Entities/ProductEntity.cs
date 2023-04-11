namespace BMStore.Domain.Entities;

public record ProductEntity
{
    public int Id { get; init; }
    public int ProductId { get; init; }
    public string Name { get; init; } = string.Empty;
    public string Link { get; init; } = string.Empty;
    public CategoryEntity Category { get; init; }
    public IReadOnlyCollection<ImageEntity> ImageUrls { get; init; }
    //პროდუქტზე ფოტოს ატვირთვისას უნდა შეეძლეს უკვე ატვირთული(ზოგადად ყველა)
    //ფოტოს ნახვა და იქედან არჩევა და პროდუქტის ფოტოდ დამატება
    public string ShortDescription { get; init; } = string.Empty;
    public string FullDescription { get; init; } = string.Empty;
    public string? DescriptionForAdmin { get; init; } = string.Empty;
    public decimal Price { get; init; }
    public bool IsInStock { get; init; }
    public decimal Weight { get; init; }
    public decimal Volume { get; init; }
    public decimal PriceOnSale { get; init; }
    public bool IsOnSale { get; init; }
    public TimeSpan SaleDuration { get; init; }
    public IReadOnlyCollection<ProductEntity>? SimilarProducts { get; init; }
    public IReadOnlyCollection<KeywordEntity>? SearchKeywords { get; init; }
    public IReadOnlyCollection<CommentEntity>? UserComments { get; init; }
    public decimal UserRating { get; init; }
}
