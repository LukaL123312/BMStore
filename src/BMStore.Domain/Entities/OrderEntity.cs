using BMStore.Domain.Entities.Enums;

namespace BMStore.Domain.Entities;

public record OrderEntity
{
    public int Id { get; init; }
    public int OrderId { get; init; }
    public DateTime OrderedOn { get; init; }
    public OrderStatus Status { get; init; }
    public IReadOnlyCollection<ProductEntity> Products { get; init; }
    public decimal OrderPrice { get; init; }
    public decimal ShippingPrice { get; init; }
    public EntityType EntityType { get; init; }
    public UserEntity Customer { get; init; }
    public string Address { get; init; } = string.Empty;
    public ShippingOption ShippingOption { get; init; }
    public OrderPaymentOption OrderPaymentOption { get; init; }
}
