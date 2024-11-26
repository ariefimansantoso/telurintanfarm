using System;
using System.Threading.Tasks;
using DocumentFormat.OpenXml;
using Microsoft.EntityFrameworkCore;
using QuickAccounting.Data;
using QuickAccounting.Data.Recording;
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

		// Save Hen Population Data
		public async Task<bool> SaveHenPopulationAsync(string cageNumber, DateTime recordDate, string strainName,
			int populationStart, int populationEnd, int deadHenCount, int unproductiveHenCount,
			int sickHenCount, int newHenCount, int moveOutHenCount, int modifiedBy)
		{
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
		public async Task<bool> SaveEggProductionAsync(string cageNumber, string strainName, DateTime recordDate, decimal henAgeWeeks, decimal henAgeDays,
			int perfectEggCount, decimal perfectEggKg, int brokenEggCount, decimal brokenEggKg,
			int totalEggCount, decimal totalEggKg, int modifiedBy)
		{
			var record = await _context.DailyRecording
				.FirstOrDefaultAsync(r => r.CageNumber == cageNumber &&
					r.RecordDate == recordDate && r.StrainName == strainName);

			if (record != null)
			{
				record.PerfectEggCount = perfectEggCount;
				record.PerfectEggKg = perfectEggKg;
				record.BrokenEggCount = brokenEggCount;
				record.BrokenEggKg = brokenEggKg;
				record.TotalEggCount = totalEggCount;
				record.TotalEggKg = totalEggKg;
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

		// Save Hen Day Data
		public async Task<bool> SaveHenDayDataAsync(string cageNumber, string strainName, DateTime recordDate, decimal henAgeWeeks, decimal henAgeDays,
			decimal actualHenDay, decimal standardHenDay, decimal dailyHenDayDifference, decimal actualEggMassKg,
			decimal standardEggMassKg, decimal eggMassDeviation, decimal actualEggWeightG,
			decimal standardEggWeightG, decimal eggWeightDeviation, decimal feedConversionRatio,
			int modifiedBy)
		{
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

		public async Task<DailyRecording> GetDailyRecordForTodayAsync(string cageNumber, DateTime recordDate)
		{
			// Fetch the record for the specified cage ID, date, and hen week
			var record = await _context.DailyRecording
				.FirstOrDefaultAsync(r => r.CageNumber == cageNumber && r.RecordDate.Date == recordDate.Date);

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
	}
}