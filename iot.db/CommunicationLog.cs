using System;
using System.Collections.Generic;

namespace iot.db;

public partial class CommunicationLog
{
    public string Id { get; set; } = null!;

    public string? InputDeviceName { get; set; }

    public string? OutputDeviceName { get; set; }

    public string? Request { get; set; }

    public string? Status { get; set; }

    public DateTime UpdatedDateTime { get; set; }

    public string UpdatedBy { get; set; } = null!;
}
