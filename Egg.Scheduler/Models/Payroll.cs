using System;
using System.Collections.Generic;

namespace Egg.Scheduler.Models;

public partial class Payroll
{
    public int Id { get; set; }

    public int CutoffId { get; set; }

    public int EmployeeId { get; set; }

    public int Masuk { get; set; }

    public int Libur { get; set; }

    public int Ijin { get; set; }

    public int Sakit { get; set; }

    public int Alpha { get; set; }

    public decimal MonthSalary { get; set; }

    public decimal BpjsKes { get; set; }

    public decimal BpjsTk { get; set; }

    public decimal RpPremi { get; set; }

    public decimal Potongan { get; set; }

    public decimal Cicilan { get; set; }

    public decimal GajiBersih { get; set; }

    public int CreatedBy { get; set; }

    public DateTime CreatedDate { get; set; }

    public decimal GajiHarian { get; set; }

    public decimal Tambahan { get; set; }

    public decimal Tunjangan { get; set; }

    public int Lembur { get; set; }

    public int DinasLuar { get; set; }

    public int SetengahHari { get; set; }

    public int Telat { get; set; }

    public decimal RpLembur { get; set; }

    public string NoRek { get; set; } = null!;

    public decimal ProsentasePremi { get; set; }

    public decimal GajiNonPremi { get; set; }

    public DateTime TanggalGajian { get; set; }
}
