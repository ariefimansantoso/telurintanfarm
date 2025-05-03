using QuickAccounting.Data.Recording;

namespace QuickAccounting.Repository.Interface
{
	public interface IDailyRecordingService
	{
		Task<List<DailyRecording>> GetRecordingsByKandang(string cageNumber, DateTime from, DateTime to);

        Task<DailyRecording> GetDailyRecordForTodayForConsoleAsync(string cageNumber, DateTime recordDate);
		Task InitiateDailyRecordingAsync(string cageNumber, string strainName, DateTime recordDate, int modifiedBy);
		Task<bool> SaveEggProductionAsync(string cageNumber, string strainName, DateTime recordDate,
			int perfectEggCount, decimal perfectEggKg, int brokenEggCount, decimal brokenEggKg, int telurPutihButir, decimal telurPutihKG,
			int totalEggCount, decimal totalEggKg, int modifiedBy);

		Task<bool> SaveHenStartPopulationAsync(string cageNumber, DateTime recordDate, string strainName,
			int populationStart, int populationEnd, int deadHenCount, int unproductiveHenCount,
			int sickHenCount, int newHenCount, int moveOutHenCount, int modifiedBy, bool periodeStart, bool periodeEnd, decimal henAgeWeeks, decimal henAgeDays);

        Task<bool> SaveFoodIntakeAsync(string cageNumber, string strainName, DateTime recordDate, string concentrateType, decimal foodIntakePerHen, decimal remainingFoodKg, decimal foodNeededTodayKg, decimal actualFoodNeededKG, decimal saldoFoodKG, decimal foodIntakeDeviation, int modifiedBy);
		Task<bool> SaveHenDayDataAsync(string cageNumber, string strainName, DateTime recordDate, decimal henAgeWeeks, decimal henAgeDays, decimal actualHenDay, decimal standardHenDay, decimal dailyHenDayDifference, decimal actualEggMassKg, decimal standardEggMassKg, decimal eggMassDeviation, decimal actualEggWeightG, decimal standardEggWeightG, decimal eggWeightDeviation, decimal feedConversionRatio, int modifiedBy);
		Task<bool> SaveHenPopulationAsync(string cageNumber, DateTime recordDate, string strainName,
			int populationStart, int populationEnd, int deadHenCount, int unproductiveHenCount,
			int sickHenCount, int newHenCount, int moveOutHenCount, int modifiedBy, bool periodeStart, bool periodeEnd);

        Task<List<DailyRecording>> GetDailyRecordingStrain(string strainName, DateTime from, DateTime to);
		Task<List<string>> GetStrainNameList();
		Task<DailyRecording> GetDailyRecordForTodayForFormAsync(string cageNumber, DateTime recordDate, int modifiedBy);
		Task<bool> UpdateRecording(DailyRecording recording);
		Task<DailyRecording> GetForUpdate(string cageNumber, DateTime recordDate);
    }
}