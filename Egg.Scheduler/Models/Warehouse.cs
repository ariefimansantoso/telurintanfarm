using System;
using System.Collections.Generic;

namespace Egg.Scheduler.Models;

public partial class Warehouse
{
    public int WarehouseId { get; set; }

    public string Name { get; set; } = null!;

    public string Mobile { get; set; } = null!;

    public string Country { get; set; } = null!;

    public string City { get; set; } = null!;

    public string Email { get; set; } = null!;

    public int CompanyId { get; set; }

    public DateTime? AddedDate { get; set; }

    public DateTime? ModifyDate { get; set; }
}
