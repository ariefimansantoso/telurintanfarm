using System;
using System.Collections.Generic;

namespace Egg.Scheduler.Models;

public partial class Pengumuman
{
    public int Id { get; set; }

    public string PengumumanContent { get; set; } = null!;
}
