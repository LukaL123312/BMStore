using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace BMStore.Domain.Entities.Enums;

[JsonConverter(typeof(JsonStringEnumMemberConverter))]
public enum EntityType
{
    [EnumMember(Value= "PHYSICAL")]
    Physical,

    [EnumMember(Value = "PHYSICAL")]
    Legal
}
