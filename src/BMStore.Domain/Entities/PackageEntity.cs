using BMStore.Domain.Entities.Enums;

namespace BMStore.Domain.Entities;

public record PackageEntity
{
    public int Id { get; init; }
    public string Name { get; init; } = string.Empty;
    public string Description { get; init; } = string.Empty;
    public decimal? PackagePrice { get; init; }
    public PackagePaymentOption PaymentOption { get; init; }
    public LayoutEntity Layout { get; init; }
    public IReadOnlyCollection<UserEntity>? Users { get; init; }

}
