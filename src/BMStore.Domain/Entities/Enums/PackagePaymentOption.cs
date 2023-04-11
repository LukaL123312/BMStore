﻿using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace BMStore.Domain.Entities.Enums;

[JsonConverter(typeof(JsonStringEnumMemberConverter))]
public enum PackagePaymentOption
{
    [EnumMember(Value = "RENT")]
    Rent,

    [EnumMember(Value = "PURCHASE")]
    Purchase,
}
