using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace RecordingScheduler
{
    public partial class Calculate : System.Web.UI.Page
    {
        TelurIntanDataContext _context = new TelurIntanDataContext(ConfigurationManager.ConnectionStrings["telurint_sqlserverConnectionString"].ConnectionString);
        string status = "Not Yet";
        protected void Page_Load(object sender, EventArgs e)
        {
            var now = new DateTime(2025, 5, 16, 0, 0, 0);
            var dateNow = DateTime.Now.Date;

            var daysDifference = (dateNow - now).Days;
            
            for (int i = 1; i <= daysDifference; i++)
            {
                CalculateDailyRecording(now, 5);
                now = now.AddDays(1);
                status = "Done " + i;

            }

            status = "COMPLETED";
            lblStatus.Text = status;

        }

        protected void CalculateDailyRecording(DateTime recordDate, int modifiedBy)
        {
            // Get all cage numbers and their population from the previous day
            var todayRecords = _context.DailyRecordings
                .Where(r => r.RecordDate.Date == recordDate.Date)
                .ToList();


            foreach (var record in todayRecords)
            {
                if (record.CageNumber == "5")
                {
                    var x = 0;
                }
                record.FoodNeededTodayKg = (record.PopulationEnd * record.FoodIntakePerHen) / 1000;
                record.ActualFoodNeededKG = (record.PopulationEnd * record.FoodIntakePerHen) / 1000;

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

                _context.SubmitChanges();
            }

        }
    }
}