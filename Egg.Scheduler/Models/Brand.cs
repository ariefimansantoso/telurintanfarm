using System;
using System.Collections.Generic;

namespace Egg.Scheduler.Models;

public partial class Brand
{
    public int BrandId { get; set; }

    public string Name { get; set; } = null!;

    public string Image { get; set; } = null!;

    public int CompanyId { get; set; }

    public DateTime? AddedDate { get; set; }

    public DateTime? ModifyDate { get; set; }
}
