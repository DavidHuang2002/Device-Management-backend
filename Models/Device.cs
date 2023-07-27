using System;
using System.Collections.Generic;

// TODO have a separate service handling the inserting different types of devices. For example adding a raspberryPi type device will also create corresponding entry in raspberryPi table
// also make sure device type dont change easily

namespace Device_Management.Models;

public partial class Device
{
    public int Id { get; set; }

    public string? Name { get; set; } = null!;

    public string Type { get; set; } = null!;

    public string Status { get; set; } = null!;

    public string? Location { get; set; }

    public DateTime AddedDate { get; set; }

    public DateTime? LastCheckInTime { get; set; }

    public string? Notes { get; set; }

    public string? AzureDeviceId { get; set; }

    public string? AzureDeviceKey { get; set; }

    public string? AzureConnectionString { get; set; }
}
