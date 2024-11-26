using QuickAccounting.Data.Recording;

namespace QuickAccounting.Repository.Interface
{
	public interface IDailyRecordingService
	{
		Task<DailyRecording> GetDailyRecordForTodayAsync(string cageNumber, DateTime recordDate);
		Task InitiateDailyRecordingAsync(string cageNumber, string strainName, DateTime recordDate, int modifiedBy);
		Task<bool> SaveEggProductionAsync(string cageNumber, string strainName, DateTime recordDate, decimal henAgeWeeks, decimal henAgeDays, int perfectEggCount, decimal perfectEggKg, int brokenEggCount, decimal brokenEggKg, int totalEggCount, decimal totalEggKg, int modifiedBy);
		Task<bool> SaveFoodIntakeAsync(string cageNumber, string strainName, DateTime recordDate, string concentrateType, decimal foodIntakePerHen, decimal remainingFoodKg, decimal foodNeededTodayKg, decimal actualFoodNeededKG, decimal saldoFoodKG, decimal foodIntakeDeviation, int modifiedBy);
		Task<bool> SaveHenDayDataAsync(string cageNumber, string strainName, DateTime recordDate, decimal henAgeWeeks, decimal henAgeDays, decimal actualHenDay, decimal standardHenDay, decimal dailyHenDayDifference, decimal actualEggMassKg, decimal standardEggMassKg, decimal eggMassDeviation, decimal actualEggWeightG, decimal standardEggWeightG, decimal eggWeightDeviation, decimal feedConversionRatio, int modifiedBy);
		Task<bool> SaveHenPopulationAsync(string cageNumber, DateTime recordDate, string strainName, int populationStart, int populationEnd, int deadHenCount, int unproductiveHenCount, int sickHenCount, int newHenCount, int moveOutHenCount, int modifiedBy);
		Task<List<DailyRecording>> GetDailyRecordingStrain(string strainName, DateTime from, DateTime to);
		Task<List<string>> GetStrainNameList();
	}
}