// See https://aka.ms/new-console-template for more information
//using Egg.Scheduler;
using Egg.Scheduler.Models;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Xml;

System.Globalization.CultureInfo.DefaultThreadCurrentCulture = System.Globalization.CultureInfo.InvariantCulture;
System.Globalization.CultureInfo.DefaultThreadCurrentUICulture = System.Globalization.CultureInfo.InvariantCulture;

Console.WriteLine("Hello, Balung!");

if(args.Length > 0)
{
    string executionType = args[0];

    Console.WriteLine("Executing " +  executionType);
    
    if(executionType == "Daily")
    {
        var now = DateTime.Now.Date;

        InitiateDailyRecording(now, 5);
    }
    else if(executionType == "Hourly")
    {
        var now = DateTime.Now.AddDays(-1);
        var dateNow = DateTime.Now;

        var daysDifference = (dateNow - now).Days;

        for (int i = 0; i <= daysDifference; i++)
        {
            CalculateHourlyRecording(now, 5);
            now = now.AddDays(1);            
        }
    }
}

public partial class Program
{    
    protected static void InitiateDailyRecording(DateTime recordDate, int modifiedBy)
    {
        TelurintDbContext _context = new TelurintDbContext();

        string auditMessage = ".NET Core Console Schedule Task LoadRecordingData is running: " + recordDate.ToString("dd/MM/yyyy hh:mm");
        AuditLog auditLog = new AuditLog();
        auditLog.ActionType = "CORE NEWLoadRecordingData";
        auditLog.ActionDescription = auditMessage;
        auditLog.CreatedDate = DateTime.Now;
        auditLog.LogType = LogTypes.LogInfo;
        auditLog.EmployeeId = -99;
        auditLog.EmployeeName = "CORE System";
        _context.AuditLogs.Add(auditLog);

        var lastRecordDate = recordDate.AddDays(-1); // Get the previous day's record

        var recordings = _context.DailyRecordings.OrderByDescending(x => x.RecordDate).Take(5).ToList();

        // Get all cage numbers and their population from the previous day
        var previousDayRecords = _context.DailyRecordings
            .Where(r => r.RecordDate.Date == lastRecordDate.Date)
            .Select(r => new
            {
                r.CageNumber,
                r.PopulationEnd,
                r.StrainName,
                r.FoodIntakePerHen,
                r.FoodNeededTodayKg,
                r.HenAgeDays,
                r.ConcentrateType
            })
            .ToList();

        foreach (var record in previousDayRecords)
        {            
            var todayRecords = _context.DailyRecordings
                                .Where(r => r.RecordDate.Date == recordDate.Date && r.CageNumber == record.CageNumber).FirstOrDefault();

            if (todayRecords != null)
            {
                todayRecords.FoodIntakePerHen = record.FoodIntakePerHen;
                todayRecords.StrainName = record.StrainName;
                if (string.IsNullOrEmpty(todayRecords.StrainName))
                {
                    todayRecords.StrainName = "";
                }
            }
            else
            {
                if (record.PopulationEnd >= 0)
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
                        FoodNeededTodayKg = (record.PopulationEnd * record.FoodIntakePerHen) / 1000,
                        ActualFoodNeededKg = (record.PopulationEnd * record.FoodIntakePerHen) / 1000,
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
                        HenAgeDays = record.HenAgeDays + 1,
                        SaldoFoodKg = 0,
                        FoodIntakeDeviation = 0,
                        TelurPutihButir = 0,
                        TelurPutihKg = 0,
                        PeriodeStart = false,
                        PeriodeEnd = false,
                        GroupName = "",
                        ConcentrateType = string.IsNullOrEmpty(record.ConcentrateType) ? "" : record.ConcentrateType,
                        HenAgeWeeks = 0, // Age should be updated later
                        ModifiedBy = modifiedBy,
                        ModifiedDate = DateTime.Now
                    };

                    if (string.IsNullOrEmpty(newRecord.StrainName))
                    {
                        newRecord.StrainName = "";
                    }

                    auditMessage = ".Net Core Schedule Task LoadRecordingData is adding: " + SerializeObjectToString(newRecord);
                    AuditLog auditLog2 = new AuditLog();
                    auditLog2.ActionType = "CORE LoadRecordingData";
                    auditLog2.ActionDescription = auditMessage;
                    auditLog2.CreatedDate = DateTime.Now;
                    auditLog2.LogType = LogTypes.LogInfo;
                    auditLog2.EmployeeId = -99;
                    auditLog2.EmployeeName = "CORE System";
                    _context.AuditLogs.Add(auditLog2);

                    _context.DailyRecordings.Add(newRecord);
                }
                else
                {
                    auditMessage = ".Net Core Schedule Task LoadRecordingData is adding: 0 record, masa akhir / afkir";
                    AuditLog auditLog2 = new AuditLog();
                    auditLog2.ActionType = "CORE LoadRecordingData";
                    auditLog2.ActionDescription = auditMessage;
                    auditLog2.CreatedDate = DateTime.Now;
                    auditLog2.LogType = LogTypes.LogInfo;
                    auditLog2.EmployeeId = -99;
                    auditLog2.EmployeeName = "CORE System";
                    _context.AuditLogs.Add(auditLog2);
                }
            }
        }

        auditMessage = ".Net Core Schedule Task LoadRecordingData is DONE";
        AuditLog auditLog3 = new AuditLog();
        auditLog3.ActionType = "CORE LoadRecordingData";
        auditLog3.ActionDescription = auditMessage;
        auditLog3.CreatedDate = DateTime.Now;
        auditLog3.LogType = LogTypes.LogInfo;
        auditLog3.EmployeeId = -99;
        auditLog3.EmployeeName = "CORE System";
        _context.AuditLogs.Add(auditLog3);

        _context.SaveChanges();
    }

    protected static void CalculateHourlyRecording(DateTime recordDate, int modifiedBy)
    {
        var _context = new TelurintDbContext(
                new DbContextOptionsBuilder<TelurintDbContext>()
                .UseSqlServer("Data Source=balungfarm.javahome.co.id;Initial Catalog=telurint_db;Persist Security Info=True;User ID=sa;Password=hYu7372Svderd#$;Trust Server Certificate=True")
                //.LogTo(Console.WriteLine)
                .Options);

        try
        {
            // Attempt to connect immediately and force an exception
            if (!_context.Database.CanConnect())
            {
                Console.WriteLine("Could not connect to database!");
                // You might add a throw here to get a stack trace
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Connection test failed: {ex.Message}");
            throw;
        }

        Console.WriteLine("recordDate: " + recordDate);

        // Get all cage numbers and their population from the previous day
        if (_context == null)
        {
            Console.WriteLine("_context null");
            _context = new TelurintDbContext(
                    new DbContextOptionsBuilder<TelurintDbContext>()
                    .UseSqlServer("Data Source=balungfarm.javahome.co.id;Initial Catalog=telurint_db;Persist Security Info=True;User ID=sa;Password=hYu7372Svderd#$;Trust Server Certificate=True")
                    //.LogTo(Console.WriteLine)
                    .Options);
        }

            if (_context.DailyRecordings == null)
            {
                Console.WriteLine("Recording null");
                _context = new TelurintDbContext(
                    new DbContextOptionsBuilder<TelurintDbContext>()
                    .UseSqlServer("Data Source=balungfarm.javahome.co.id;Initial Catalog=telurint_db;Persist Security Info=True;User ID=sa;Password=hYu7372Svderd#$;Trust Server Certificate=True")
                    //.LogTo(Console.WriteLine)
                    .Options);
            }

            var todayRecords = _context.DailyRecordings
                .Where(r => r.RecordDate.Date == recordDate.Date)
                .ToList();

            foreach (var record in todayRecords)
            {

                record.FoodNeededTodayKg = (record.PopulationEnd * record.FoodIntakePerHen) / 1000;
                record.ActualFoodNeededKg = (record.PopulationEnd * record.FoodIntakePerHen) / 1000;

                if (record.HenAgeWeeks < 1)
                {
                    record.HenAgeWeeks = record.HenAgeDays.Value / 7;
                }

                if (record.PopulationStart > 0 && record.TotalEggCount > 0)
                {
                    record.ActualHenDay = ((decimal)record.TotalEggCount / (decimal)record.PopulationStart) * (decimal)100;
                }

                if (record.TotalEggKg > 0 && record.FoodNeededTodayKg > 0)
                {
                    record.FeedConversionRatio = record.FoodNeededTodayKg / record.TotalEggKg;
                }

                if (record.PopulationStart > 0)
                {
                    record.ActualEggMassKg = record.TotalEggKg / (decimal)record.PopulationStart * (decimal)1000;
                }

                if (record.TotalEggCount > 0)
                {
                    record.ActualEggWeightG = (record.TotalEggKg / record.TotalEggCount) * 1000;
                }

                record.ModifiedBy = modifiedBy;
                record.ModifiedDate = DateTime.Now;

                _context.SaveChanges();
            }

            string auditMessage = ".NET Core Console Schedule Task CalculateHourlyRecording is running: " + recordDate.ToString("dd/MM/yyyy hh:mm");
            AuditLog auditLog = new AuditLog();
            auditLog.ActionType = "CORE CalculateHourlyRecording";
            auditLog.ActionDescription = auditMessage;
            auditLog.CreatedDate = DateTime.Now;
            auditLog.LogType = LogTypes.LogInfo;
            auditLog.EmployeeId = -99;
            auditLog.EmployeeName = "CORE System";
            _context.AuditLogs.Add(auditLog);
            _context.SaveChanges();

            _context.Dispose();
        
    }

    public static string SerializeObjectToString(object obj)
    {
        return JsonConvert.SerializeObject(obj, Newtonsoft.Json.Formatting.Indented);
    }
}

public class LogTypes
{
    public const string LogWarning = "WARNING";
    public const string LogInfo = "INFO";
    public const string LogError = "ERROR";
}