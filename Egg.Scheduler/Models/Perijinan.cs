using System;
using System.Collections.Generic;

namespace Egg.Scheduler.Models;

public partial class Perijinan
{
    public int Id { get; set; }

    public int EmployeeId { get; set; }

    public DateTime DateSubmitted { get; set; }

    public string SubmittedFor { get; set; } = null!;

    public string SubmittedDesc { get; set; } = null!;

    public string DocPhoto { get; set; } = null!;

    public DateTime ForDate { get; set; }

    public bool? IsApproved { get; set; }

    public string ApprovalDescription { get; set; } = null!;

    public DateTime ActionDate { get; set; }

    public int ActionByEmployeeId { get; set; }
}
