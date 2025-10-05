using System;
using System.Collections.Generic;

namespace Egg.Scheduler.Models;

public partial class AccountGroup
{
    public int AccountGroupId { get; set; }

    public string AccountGroupName { get; set; } = null!;

    public int GroupUnder { get; set; }

    public string GroupCode { get; set; } = null!;

    public int CompanyId { get; set; }

    public string? Narration { get; set; }

    public bool IsDefault { get; set; }

    public string? Nature { get; set; }

    public string? AffectGrossProfit { get; set; }

    public int? CreatedBy { get; set; }

    public int? ModifyBy { get; set; }

    public DateTime? AddedDate { get; set; }

    public DateTime? ModifyDate { get; set; }
}
