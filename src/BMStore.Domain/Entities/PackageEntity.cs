using BMStore.Domain.Entities.Enums;

namespace BMStore.Domain.Entities;

public record PackageEntity
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal? PackagePrice { get; set; }
    public PackagePaymentOption PaymentOption { get; set; }
    public IReadOnlyCollection<UserEntity>? Users { get; set; }

}
