namespace BMStore.Domain.Entities;

public record AddressEntity
{
    public int Id { get; init; }
    public string Address { get; init; } = string.Empty;
    public UserEntity AddressOwner { get; init; }
}
