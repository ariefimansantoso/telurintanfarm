using System;
using System.Collections.Generic;

namespace Egg.Scheduler.Models;

public partial class LogPopulasiAnakKandang
{
    public int Id { get; set; }

    public int EmployeeId { get; set; }

    public DateTime RecordDate { get; set; }

    public string CageNumber { get; set; } = null!;

    public DateTime CreatedDate { get; set; }
}
