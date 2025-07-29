using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace RecordingScheduler
{
    public partial class Default : System.Web.UI.Page
    {
        TelurIntanDataContext _context = new TelurIntanDataContext(ConfigurationManager.ConnectionStrings["telurint_sqlserverConnectionString"].ConnectionString);

        protected void Page_Load(object sender, EventArgs e)
        {
            var now = DateTime.Now.Date;

            InitiateDailyRecording(now, 5);
        }

        protected void InitiateDailyRecording(DateTime recordDate, int modifiedBy)
        {
            string auditMessage = "Schedule Task LoadRecordingData is running: " + recordDate.ToString("dd/MM/yyyy hh:mm");
            AuditLog auditLog = new AuditLog();
            auditLog.ActionType = "NEWLoadRecordingData";
            auditLog.ActionDescription = auditMessage;
            auditLog.CreatedDate = DateTime.Now;
            auditLog.LogType = LogTypes.LogInfo;
            auditLog.EmployeeID = -99;
            auditLog.EmployeeName = "System";
            _context.AuditLogs.InsertOnSubmit(auditLog);

            var lastRecordDate = recordDate.AddDays(-1); // Get the previous day's record

            // Get all cage numbers and their population from the previous day
            var previousDayRecords = _context.DailyRecordings
                .Where(r => r.RecordDate == lastRecordDate)
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
                                    .Where(r => r.RecordDate == recordDate && r.CageNumber == record.CageNumber).FirstOrDefault();

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
                        FoodNeededTodayKg = record.PopulationEnd * record.FoodIntakePerHen,
                        ActualFoodNeededKG = record.PopulationEnd * record.FoodIntakePerHen,
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
                        SaldoFoodKG = 0,
                        FoodIntakeDeviation = 0,
                        TelurPutihButir = 0,
                        TelurPutihKG = 0,
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

                    auditMessage = "Schedule Task LoadRecordingData is adding: " + SerializeObjectToString(newRecord);
                    AuditLog auditLog2 = new AuditLog();
                    auditLog2.ActionType = "LoadRecordingData";
                    auditLog2.ActionDescription = auditMessage;
                    auditLog2.CreatedDate = DateTime.Now;
                    auditLog2.LogType = LogTypes.LogInfo;
                    auditLog2.EmployeeID = -99;
                    auditLog2.EmployeeName = "System";
                    _context.AuditLogs.InsertOnSubmit(auditLog2);

                    _context.DailyRecordings.InsertOnSubmit(newRecord);
                }
            }

            auditMessage = "Schedule Task LoadRecordingData is DONE";
            AuditLog auditLog3 = new AuditLog();
            auditLog3.ActionType = "LoadRecordingData";
            auditLog3.ActionDescription = auditMessage;
            auditLog3.CreatedDate = DateTime.Now;
            auditLog3.LogType = LogTypes.LogInfo;
            auditLog3.EmployeeID = -99;
            auditLog3.EmployeeName = "System";
            _context.AuditLogs.InsertOnSubmit(auditLog3);


            _context.SubmitChanges();

        }

        public string SerializeObjectToString(object obj)
        {
            return ""; JsonConvert.SerializeObject(obj, Formatting.Indented);
        }
    }

    public class LogTypes
    {
        public const string LogWarning = "WARNING";
        public const string LogInfo = "INFO";
        public const string LogError = "ERROR";
    }
}