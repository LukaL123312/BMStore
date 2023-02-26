namespace BMStore.Domain.Entities.Enums;

using System.Runtime.Serialization;
using System.Text.Json.Serialization;

[JsonConverter(typeof(JsonStringEnumMemberConverter))]
public enum PackagePaymentOption
{
    [EnumMember(Value = "RENT")]
    Rent,

    [EnumMember(Value = "PURCHASE")]
    Purchase,
}
