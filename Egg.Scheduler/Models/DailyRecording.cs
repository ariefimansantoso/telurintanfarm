using System;
using System.Collections.Generic;

namespace Egg.Scheduler.Models;

public partial class DailyRecording
{
    public string CageNumber { get; set; } = null!;

    public string StrainName { get; set; } = null!;

    public DateTime RecordDate { get; set; }

    public decimal HenAgeWeeks { get; set; }

    public decimal? HenAgeDays { get; set; }

    public int? PopulationStart { get; set; }

    public int? DeadHenCount { get; set; }

    public int? UnproductiveHenCount { get; set; }

    public int? SickHenCount { get; set; }

    public int? MoveOutHenCount { get; set; }

    public int? MoveInHenCount { get; set; }

    public int? PopulationEnd { get; set; }

    public string? ConcentrateType { get; set; }

    public decimal? ActualFoodNeededKg { get; set; }

    public decimal? SaldoFoodKg { get; set; }

    public decimal? FoodNeededTodayKg { get; set; }

    public decimal? RemainingFoodKg { get; set; }

    public decimal? FoodIntakePerHen { get; set; }

    public decimal? FoodIntakeDeviation { get; set; }

    public int? PerfectEggCount { get; set; }

    public decimal? PerfectEggKg { get; set; }

    public int? BrokenEggCount { get; set; }

    public decimal? BrokenEggKg { get; set; }

    public int? TotalEggCount { get; set; }

    public decimal? TotalEggKg { get; set; }

    public decimal? ActualHenDay { get; set; }

    public decimal? StandardHenDay { get; set; }

    public decimal? DailyHenDayDifference { get; set; }

    public decimal? ActualEggMassKg { get; set; }

    public decimal? StandardEggMassKg { get; set; }

    public decimal? EggMassDeviation { get; set; }

    public decimal? ActualEggWeightG { get; set; }

    public decimal? StandardEggWeightG { get; set; }

    public decimal? EggWeightDeviation { get; set; }

    public decimal? FeedConversionRatio { get; set; }

    public int ModifiedBy { get; set; }

    public DateTime ModifiedDate { get; set; }

    public int? TelurPutihButir { get; set; }

    public decimal? TelurPutihKg { get; set; }

    public bool? PeriodeStart { get; set; }

    public bool? PeriodeEnd { get; set; }

    public string? GroupName { get; set; }
}
