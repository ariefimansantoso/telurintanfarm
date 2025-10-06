using System;
using System.Collections.Generic;

namespace Egg.Scheduler.Models;

public partial class AbsensiPotongan
{
    public int AbsensiPotonganId { get; set; }

    public int CutOffBulan { get; set; }

    public int CutOffTahun { get; set; }

    public int KaryawanId { get; set; }

    public string StatusPotongan { get; set; } = null!;

    public decimal RpPotongan { get; set; }

    public int PotKe { get; set; }
}
