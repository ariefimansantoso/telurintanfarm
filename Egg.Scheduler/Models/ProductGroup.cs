using System;
using System.Collections.Generic;

namespace Egg.Scheduler.Models;

public partial class ProductGroup
{
    public int GroupId { get; set; }

    public string GroupName { get; set; } = null!;

    public int GroupUnder { get; set; }

    public string? Image { get; set; }

    public string? Narration { get; set; }

    public int? CompanyId { get; set; }

    public DateTime? AddedDate { get; set; }

    public DateTime? ModifyDate { get; set; }
}
