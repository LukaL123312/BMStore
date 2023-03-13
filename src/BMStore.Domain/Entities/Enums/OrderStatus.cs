using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace BMStore.Domain.Entities.Enums;

[JsonConverter(typeof(JsonStringEnumMemberConverter))]
public enum OrderStatus
{
    [EnumMember(Value = "NOTFINISHED")]
    NotFinished,

    [EnumMember(Value = "FAILED")]
    Failed,
    
    [EnumMember(Value = "INPROGRESS")]
    InProgress,

    [EnumMember(Value = "ONTHEWAY")]
    OnTheWay,

    [EnumMember(Value = "FINISHED")]
    Finished
}
