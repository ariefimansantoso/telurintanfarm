using System;
using System.Threading.Tasks;
using DocumentFormat.OpenXml;
using Microsoft.EntityFrameworkCore;
using QuickAccounting.Data;
using QuickAccounting.Data.Recording;
using QuickAccounting.Helpers;
using QuickAccounting.Repository.Interface;

namespace QuickAccounting.Repository.Repository
{
    public class DailyRecordingService : IDailyRecordingService
	{
		private readonly ApplicationDbContext _context;

		public DailyRecordingService(ApplicationDbContext context)
		{
			_context = context;
		}

        public async Task<List<DailyRecording>> GetRecordingsByKandang(string cageNumber, DateTime from, DateTime to)
        {
			List<DailyRecording> results = new List<DailyRecording>();

			if (cageNumber != "ALL")
			{
				cageNumber = Helper.ConvertCageNumber(cageNumber);

				results = await _context.DailyRecording.Where(x => x.CageNumber == cageNumber &&
										x.RecordDate >= from && x.RecordDate <= to)
										.OrderByDescending(x => x.RecordDate).ToListAsync();
			}
			else
			{
				results = await _context.DailyRecording.Where(x => 
										x.RecordDate >= from && x.RecordDate <= to)
										.OrderByDescending(x => x.RecordDate).ToListAsync();
			}

            return results;
        }

		// Save Hen Population Data
		public async Task<bool> SaveHenPopulationAsync(string cageNumber, DateTime recordDate, string strainName,
			int populationStart, int populationEnd, int deadHenCount, int unproductiveHenCount,
			int sickHenCount, int newHenCount, int moveOutHenCount, int modifiedBy)
		{
            cageNumber = Helper.ConvertCageNumber(cageNumber);

            var record = await _context.DailyRecording
				.FirstOrDefaultAsync(r => r.CageNumber == cageNumber && r.RecordDate == recordDate && r.StrainName == strainName);

			if (record != null)
			{
				record.PopulationStart = populationStart;
				record.PopulationEnd = populationEnd;
				record.DeadHenCount = deadHenCount;
				record.UnproductiveHenCount = unproductiveHenCount;
				record.SickHenCount = sickHenCount;
				record.MoveInHenCount = newHenCount;
				record.MoveOutHenCount = moveOutHenCount;

				record.ActualHenDay = (decimal)record.TotalEggCount / (decimal)populationStart;
				record.ActualEggMassKg = record.ActualHenDay * (record.TotalEggKg / 1000) / 100;
				record.ActualEggWeightG = record.TotalEggKg / record.TotalEggCount;

                record.ModifiedBy = modifiedBy;
				record.ModifiedDate = DateTime.Now;

				_context.DailyRecording.Update(record);

				await _context.SaveChangesAsync();

				return true;
			}
			else
			{
				return false;
			}
		}

		// Save Food Intake Data
		public async Task<bool> SaveFoodIntakeAsync(string cageNumber, string strainName, DateTime recordDate,
			string concentrateType, decimal foodIntakePerHen, decimal remainingFoodKg, decimal foodNeededTodayKg,
			decimal actualFoodNeededKG, decimal saldoFoodKG, decimal foodIntakeDeviation, int modifiedBy)
		{
            cageNumber = Helper.ConvertCageNumber(cageNumber);

            var record = await _context.DailyRecording
				.FirstOrDefaultAsync(r => r.CageNumber == cageNumber && r.RecordDate == recordDate && r.StrainName == strainName);

			if (record != null)
			{
				record.ConcentrateType = concentrateType;
				record.FoodIntakePerHen = foodIntakePerHen;
				record.RemainingFoodKg = remainingFoodKg;
				record.FoodNeededTodayKg = foodNeededTodayKg;
				record.ActualFoodNeededKG = actualFoodNeededKG;
				record.SaldoFoodKG = saldoFoodKG;
				record.FoodIntakeDeviation = foodIntakeDeviation;
				record.ModifiedBy = modifiedBy;
				record.ModifiedDate = DateTime.Now;

				record.FeedConversionRatio = record.TotalEggKg / foodNeededTodayKg;

                _context.DailyRecording.Update(record);
				await _context.SaveChangesAsync();
				return true;
			}
			else
			{
				return false;
			}
		}

		// Save Egg Production Data
		public async Task<bool> SaveEggProductionAsync(string cageNumber, string strainName, DateTime recordDate,
			int perfectEggCount, decimal perfectEggKg, int brokenEggCount, decimal brokenEggKg,
			int totalEggCount, decimal totalEggKg, int modifiedBy)
		{
            cageNumber = Helper.ConvertCageNumber(cageNumber);

            var record = await _context.DailyRecording
				.FirstOrDefaultAsync(r => r.CageNumber == cageNumber &&
					r.RecordDate == recordDate && r.StrainName == strainName);

			if (record != null)
			{
				record.PerfectEggCount = record.PerfectEggCount + perfectEggCount;
				record.PerfectEggKg = record.PerfectEggKg + perfectEggKg;
				record.BrokenEggCount = record.BrokenEggCount + brokenEggCount;
				record.BrokenEggKg = record.BrokenEggKg + brokenEggKg;
				record.TotalEggCount = record.TotalEggCount + totalEggCount;
				record.TotalEggKg = record.TotalEggKg + totalEggKg;

				if (record.PopulationStart > 0 && record.TotalEggCount > 0)
				{
					record.ActualHenDay = (decimal)record.TotalEggCount / (decimal)record.PopulationStart;
				}

				if (record.TotalEggKg > 0 && record.FoodNeededTodayKg > 0)
				{
					record.FeedConversionRatio = record.FoodNeededTodayKg / record.TotalEggKg;
				}

                record.ModifiedBy = modifiedBy;
				record.ModifiedDate = DateTime.Now.Date;

				_context.DailyRecording.Update(record);
				await _context.SaveChangesAsync();
				return true;
			}
			else
			{
				return false;
			}
		}

		// Save Hen Day Data
		public async Task<bool> SaveHenDayDataAsync(string cageNumber, string strainName, DateTime recordDate, decimal henAgeWeeks, decimal henAgeDays,
			decimal actualHenDay, decimal standardHenDay, decimal dailyHenDayDifference, decimal actualEggMassKg,
			decimal standardEggMassKg, decimal eggMassDeviation, decimal actualEggWeightG,
			decimal standardEggWeightG, decimal eggWeightDeviation, decimal feedConversionRatio,
			int modifiedBy)
		{
            cageNumber = Helper.ConvertCageNumber(cageNumber);

            var record = await _context.DailyRecording
				.FirstOrDefaultAsync(r => r.CageNumber == cageNumber
                    && r.RecordDate == recordDate && r.StrainName == strainName);

			if (record != null)
			{
				record.ActualHenDay = actualHenDay;
				record.StandardHenDay = standardHenDay;
				record.DailyHenDayDifference = dailyHenDayDifference;
				record.ActualEggMassKg = actualEggMassKg;
				record.StandardEggMassKg = standardEggMassKg;
				record.EggMassDeviation = eggMassDeviation;
				record.ActualEggWeightG = actualEggWeightG;
				record.StandardEggWeightG = standardEggWeightG;
				record.EggWeightDeviation = eggWeightDeviation;
				record.FeedConversionRatio = feedConversionRatio;
				record.ModifiedBy = modifiedBy;
				record.ModifiedDate = DateTime.Now;

				_context.DailyRecording.Update(record);
				await _context.SaveChangesAsync();
				return true;
			}
			else
			{
				return false;
			}
		}

		// Initiate Daily Record for the 3 AM Scheduler
		public async Task InitiateDailyRecordingAsync(string cageNumber, string strainName, DateTime recordDate, int modifiedBy)
		{
			var lastRecordDate = recordDate.AddDays(-1); // Get the previous day's record

			// Get all cage numbers and their population from the previous day
			var previousDayRecords = await _context.DailyRecording
				.Where(r => r.RecordDate == lastRecordDate)
				.Select(r => new
				{
					r.CageNumber,
					r.PopulationEnd,
					r.StrainName,
					r.FoodIntakePerHen
				})
				.ToListAsync();

			foreach (var record in previousDayRecords)
			{
				// Initialize the record for today using the previous day's population
				var newRecord = new DailyRecording
				{
					CageNumber = record.CageNumber,
					RecordDate = recordDate,
					StrainName = record.StrainName,
					PopulationStart = record.PopulationEnd,
					PopulationEnd = record.PopulationEnd, // Initial population is the same at the start and end of the day
					DeadHenCount = 0,
					UnproductiveHenCount = 0,
					SickHenCount = 0,
					MoveInHenCount = 0,
					MoveOutHenCount = 0,
					FoodIntakePerHen = record.FoodIntakePerHen,
					RemainingFoodKg = 0,
					FoodNeededTodayKg = 0,
					ActualFoodNeededKG = 0,
					PerfectEggCount = 0,
					PerfectEggKg = 0,
					BrokenEggCount = 0,
					BrokenEggKg = 0,
					TotalEggCount = 0,
					TotalEggKg = 0,
					ActualHenDay = 0,
					StandardHenDay = 0,
					DailyHenDayDifference = 0,
					ActualEggMassKg = 0,
					StandardEggMassKg = 0,
					EggMassDeviation = 0,
					ActualEggWeightG = 0,
					StandardEggWeightG = 0,
					EggWeightDeviation = 0,
					FeedConversionRatio = 0,
					HenAgeWeeks = 0, // Age should be updated later
					ModifiedBy = modifiedBy,
					ModifiedDate = DateTime.Now
				};

				await _context.DailyRecording.AddAsync(newRecord);
			}

			await _context.SaveChangesAsync();
		}

		public async Task<DailyRecording> GetDailyRecordForTodayForConsoleAsync(string cageNumber, DateTime recordDate)
		{
            cageNumber = Helper.ConvertCageNumber(cageNumber);
            // Fetch the record for the specified cage ID, date, and hen week
            var record = await _context.DailyRecording
				.FirstOrDefaultAsync(r => r.CageNumber == cageNumber && r.RecordDate.Date == recordDate.Date);
			
			// Return the record, or null if not found
			return record;
		}

		public async Task<DailyRecording> GetForUpdate(string cageNumber, DateTime recordDate)
		{
            cageNumber = Helper.ConvertCageNumber(cageNumber);

            var record = await _context.DailyRecording
                .FirstOrDefaultAsync(r => r.CageNumber == cageNumber && r.RecordDate.Date == recordDate.Date);

			return record;
        }
        public async Task<DailyRecording> GetDailyRecordForTodayForFormAsync(string cageNumber, DateTime recordDate, int modifiedBy)
        {
            cageNumber = Helper.ConvertCageNumber(cageNumber);

            // Fetch the record for the specified cage ID, date, and hen week
            var record = await _context.DailyRecording
                .FirstOrDefaultAsync(r => r.CageNumber == cageNumber && r.RecordDate.Date == recordDate.Date);

            if (record == null)
            {
				record = await _context.DailyRecording
					.OrderByDescending(r => r.RecordDate)
					.FirstOrDefaultAsync(r => r.CageNumber == cageNumber);

				var previousRecordDate = record.RecordDate;
				var numberOfDays = (recordDate - previousRecordDate).Days;
                decimal weekDifference = (decimal)numberOfDays / (decimal)7;

                var newRecord = new DailyRecording
                {
                    CageNumber = record.CageNumber,
                    RecordDate = recordDate,
                    StrainName = record.StrainName,
                    PopulationStart = record.PopulationEnd,
                    PopulationEnd = record.PopulationEnd, // Initial population is the same at the start and end of the day
					ConcentrateType = record.ConcentrateType,
                    DeadHenCount = 0,
                    UnproductiveHenCount = 0,
                    SickHenCount = 0,
                    MoveInHenCount = 0,
                    MoveOutHenCount = 0,
                    FoodIntakePerHen = record.FoodIntakePerHen,
                    RemainingFoodKg = record.RemainingFoodKg,
                    FoodNeededTodayKg = 0,
                    ActualFoodNeededKG = 0,
                    PerfectEggCount = 0,
                    PerfectEggKg = 0,
                    BrokenEggCount = 0,
                    BrokenEggKg = 0,
                    TotalEggCount = 0,
                    TotalEggKg = 0,
                    ActualHenDay = 0,
                    StandardHenDay = 0,
                    DailyHenDayDifference = 0,
                    ActualEggMassKg = 0,
                    StandardEggMassKg = 0,
                    EggMassDeviation = 0,
                    ActualEggWeightG = 0,
                    StandardEggWeightG = 0,
                    EggWeightDeviation = 0,
                    FeedConversionRatio = 0,
                    HenAgeWeeks = weekDifference, // Age should be updated later
					HenAgeDays = record.HenAgeDays + numberOfDays,
                    ModifiedBy = modifiedBy,
                    ModifiedDate = DateTime.Now
                };

                _context.DailyRecording.Add(newRecord);
				await _context.SaveChangesAsync();

                return newRecord;
            }

            // Return the record, or null if not found
            return record;
        }

        public async Task<List<DailyRecording>> GetDailyRecordingStrain(string strainName, DateTime from, DateTime to)
		{			
			var strainData = await _context.DailyRecording
				.Where(recording => recording.StrainName == strainName &&
					recording.RecordDate >= from && recording.RecordDate <= to)
				.OrderBy(recording => recording.RecordDate)				
				.ToListAsync();

			return strainData;
		}

		public async Task<List<string>> GetStrainNameList()
		{
			var listNames = await _context.DailyRecording
								.Select(recording => recording.StrainName)
								.Distinct()
								.ToListAsync();

			return listNames;
		}

		public async Task<bool> UpdateRecording(DailyRecording recording)
		{
            _context.Attach(recording); // Explicitly attach
            _context.Entry(recording).State = EntityState.Modified; //            
			await _context.SaveChangesAsync();
			return true;
		}

	}
}