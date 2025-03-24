using System;
using System.ComponentModel.DataAnnotations;

namespace QuickAccounting.Data.Recording
{	
	public class DailyRecording
	{
		public string CageNumber { get; set; }                        // Cage number identifier
		public DateTime RecordDate { get; set; }                    // Date of the record
        public string StrainName { get; set; }
        public decimal HenAgeWeeks { get; set; }                    // Age of hens in weeks with decimal precision
        public decimal HenAgeDays { get; set; }
        public int PopulationStart { get; set; }                     // Population at the start of the day
		public int PopulationEnd { get; set; }                       // Population at the end of the day
		public int DeadHenCount { get; set; }                        // Number of dead hens
		public int UnproductiveHenCount { get; set; }                // Number of unproductive hens
		public int SickHenCount { get; set; }                        // Number of sick hens
		public int MoveInHenCount { get; set; }                         // Number of new hens added
        public int MoveOutHenCount { get; set; }                         // Number of new hens move out
        public string ConcentrateType { get; set; }
        public decimal FoodIntakePerHen { get; set; }                // Food intake per hen in grams
		public decimal RemainingFoodKg { get; set; }                 // Total remaining food from the previous day in KG
		public decimal FoodNeededTodayKg { get; set; }               // Calculated food needed for the day in KG
		public decimal ActualFoodNeededKG { get; set; }              // Actual food needed in KG
        public decimal SaldoFoodKG { get; set; }
        public decimal FoodIntakeDeviation { get; set; }
        public int PerfectEggCount { get; set; }                     // Number of perfect eggs in pcs
		public decimal PerfectEggKg { get; set; }                    // Weight of perfect eggs in KG
		public int BrokenEggCount { get; set; }                      // Number of broken eggs in pcs
		public decimal BrokenEggKg { get; set; }                     // Weight of broken eggs in KG
        public int TelurPutihButir { get; set; }                      // Number of broken eggs in pcs
        public decimal TelurPutihKG { get; set; }
        public int TotalEggCount { get; set; }                       // Total eggs in pcs
		public decimal TotalEggKg { get; set; }                      // Total eggs in KG
		public decimal ActualHenDay { get; set; }                        // Actual hen day percentage
		public decimal StandardHenDay { get; set; }                      // Standard hen day percentage
		public decimal DailyHenDayDifference { get; set; }               // Difference in hen day percentage
		public decimal ActualEggMassKg { get; set; }                 // Actual egg mass in KG
		public decimal StandardEggMassKg { get; set; }               // Standard egg mass in KG
		public decimal EggMassDeviation { get; set; }                // Deviation in egg mass
		public decimal ActualEggWeightG { get; set; }                // Actual egg weight in grams
		public decimal StandardEggWeightG { get; set; }              // Standard egg weight in grams
		public decimal EggWeightDeviation { get; set; }              // Deviation in egg weight
		public decimal FeedConversionRatio { get; set; }             // Calculated feed conversion ratio
		public int ModifiedBy { get; set; }                           // ID of the person who last modified the record
		public DateTime ModifiedDate { get; set; }                    // Date and time of the last modification
        public bool PeriodeStart { get; set; }
        public bool PeriodeEnd { get; set; }
    }

	public class DailyRecordingVOV
	{
		public long ID { get; set; }

		public string CageNumber { get; set; } = string.Empty;

		public string StrainName { get; set; } = string.Empty;

		public DateTime RecordDate { get; set; }

		public decimal HenAgeWeeks { get; set; }

		public decimal HenAgeDays { get; set; }

		public int VOVProductID { get; set; }

		public string VOVProductName { get; set; } = string.Empty;

		public decimal Usage { get; set; }

		public int Day { get; set; }

		public DateTime DateCreated { get; set; }

		public int CreatedBy { get; set; }
	}

    public class ChecklistPakanItem
    {
        public long ID { get; set; }
        public string JenisCentrat { get; set; }
        public int Jagung { get; set; }
        public int Katul { get; set; }
        public int Centrat { get; set; }
        public DateTime DateCreated { get; set; }
        public int CreatedBy { get; set; }
        public int VerifiedBy { get; set; }
        public DateTime? VerifiedOn { get; set; } // Nullable in case it's not verified yet
    }

    public class RecordNota
    {
        public int ID { get; set; }
        public string JenisTelur { get; set; } = string.Empty; // Ensures a non-null default value
        public decimal BeratTelur { get; set; }
        public DateTime DateCreated { get; set; }
        public int CreatedBy { get; set; }
        public string NomorNota { get; set; } = string.Empty; // Ensures a non-null default value
    }

    public class MutasiStockTelurHarian
    {
        public long ID { get; set; } // bigint in SQL maps to long in C#
        public string JenisTelur { get; set; } = string.Empty; // Ensures a non-null default value
        public decimal BeratTelurIn { get; set; } // decimal(18, 2) in SQL
        public decimal BeratTelurOut { get; set; } // decimal(18, 2) in SQL
        public DateTime DateCreated { get; set; } // datetime2(7) in SQL maps to DateTime in C#
        public int CreatedBy { get; set; } // int in SQL
		public string Reason { get; set; } = "Mutasi";
    }
}
