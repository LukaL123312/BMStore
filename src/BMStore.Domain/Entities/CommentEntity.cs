namespace BMStore.Domain.Entities;

public record CommentEntity
{
    public int Id { get; init; }
    public ProductEntity? CommentedProduct { get; init; }
    public UserEntity? CommentAuthor { get; init; }
    public string Comment { get; init; } = string.Empty;
}
