using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace BMStore.Domain.Entities.Enums;

[JsonConverter(typeof(JsonStringEnumMemberConverter))]
public enum ShippingOption
{
    [EnumMember(Value = "SHIPONADDRESS")]
    ShipOnAddress,

    [EnumMember(Value = "TAKEAWAY")]
    TakeAway
}
