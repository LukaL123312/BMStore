namespace BMStore.Domain.Entities;

public record UserEntity
{
    public int Id { get; init; }
    public string Username { get; init; } = string.Empty;
    public string Password { get; init; } = string.Empty;
    public string Name { get; init; } = string.Empty;
    public string Surname { get; init; } = string.Empty;
    public string SubDomainName { get; init; } = string.Empty;
    public string PhoneNumber { get; init; } = string.Empty;
    public string Email { get; init; } = string.Empty;
    public IReadOnlyCollection<AddressEntity>? Addresses { get; init; }
    public IReadOnlyCollection<CommentEntity>? Comments { get; init; }
    public IReadOnlyCollection<PackageEntity>? Cart { get; init; }
    public IReadOnlyCollection<PackageEntity>? OrderedPackages { get; init; }
    public IReadOnlyCollection<PackageEntity>? FavoritePackages { get; init; }

}
