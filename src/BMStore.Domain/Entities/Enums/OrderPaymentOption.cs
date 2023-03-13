using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace BMStore.Domain.Entities.Enums;

[JsonConverter(typeof(JsonStringEnumMemberConverter))]
public enum OrderPaymentOption
{
    [EnumMember(Value = "CARD")]
    Card,

    [EnumMember(Value = "REMITTANCE")]
    Remittance,

    [EnumMember(Value = "CASH")]
    Cash
}
