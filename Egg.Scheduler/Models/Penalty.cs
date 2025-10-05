using System;
using System.Collections.Generic;

namespace Egg.Scheduler.Models;

public partial class Penalty
{
    public int Id { get; set; }

    public int EmployeeId { get; set; }

    public int AdminUserId { get; set; }

    public DateTime DateSubmitted { get; set; }

    public DateTime ForDate { get; set; }

    public string PenaltyDescription { get; set; } = null!;

    public decimal PenaltyAmount { get; set; }

    public string PenaltyPhoto { get; set; } = null!;

    public bool IsPenalty { get; set; }
}
