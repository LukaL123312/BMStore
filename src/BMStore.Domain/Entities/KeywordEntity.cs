namespace BMStore.Domain.Entities;

public record KeywordEntity
{
    public int Id { get; init; }
    public string Keyword { get; init; } = string.Empty;
}
