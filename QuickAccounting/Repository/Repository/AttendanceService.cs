using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using QuickAccounting.Data;
using QuickAccounting.Data.HrPayroll;
using QuickAccounting.Data.HrPayrollModel;
using QuickAccounting.Data.Setting;
using QuickAccounting.Pages.HumanResorcePage.EmployeePage;
using QuickAccounting.Repository.Interface;
using System.Data;
using static MudBlazor.CategoryTypes;

namespace QuickAccounting.Repository.Repository
{
	public class AttendanceService : IAttendance
    {
		private readonly ApplicationDbContext _context;
		private readonly DatabaseConnection _conn;
		public AttendanceService(ApplicationDbContext context, DatabaseConnection conn)
		{
			_context = context;
			_conn = conn;
		}

		public decimal HolliDayChecking(DateTime date)
		{
			decimal decResult = 0;
			SqlConnection sqlcon = new SqlConnection(_conn.DbConn);
			sqlcon.Open();
			SqlCommand sccmd = new SqlCommand("HolliDayChecking", sqlcon);
				sccmd.CommandType = CommandType.StoredProcedure;
				SqlParameter sprmparam = new SqlParameter();
				sprmparam = sccmd.Parameters.Add("@date", SqlDbType.DateTime);
				sprmparam.Value = date;
				decResult = Convert.ToDecimal(sccmd.ExecuteScalar());
				sqlcon.Close();
			return decResult;
		}
        public decimal HolidaySettings(DateTime date)
        {
            decimal decResult = 0;
            SqlConnection sqlcon = new SqlConnection(_conn.DbConn);
            sqlcon.Open();
            SqlCommand sccmd = new SqlCommand("HolliDayChecking", sqlcon);
                sccmd.CommandType = CommandType.StoredProcedure;
                SqlParameter sprmparam = new SqlParameter();
                sprmparam = sccmd.Parameters.Add("@date", SqlDbType.DateTime);
                sprmparam.Value = date;
                decResult = Convert.ToDecimal(sccmd.ExecuteScalar());
                sqlcon.Close();
            return decResult;
        }
        public async Task<bool> Delete(int DailyAttendanceMasterId)
        {
            SqlConnection sqlcon = new SqlConnection(_conn.DbConn);
            try
            {
                if (sqlcon.State == ConnectionState.Closed)
                {
                    sqlcon.Open();
                }
                SqlCommand cmd = new SqlCommand("AttendanceDelete", sqlcon);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter para = new SqlParameter();
                para = cmd.Parameters.Add("@DailyAttendanceMasterId", SqlDbType.Int);
                para.Value = DailyAttendanceMasterId;
                long rowAffacted = cmd.ExecuteNonQuery();
                if (rowAffacted > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
            finally
            {
                sqlcon.Close();
            }
        }

        public async Task<List<DailyAttendanceMaster>> GetTodaysAttendanceList(int employeeID)
        {
            DateTime today = DateTime.Now;
            var attendance = await _context.DailyAttendanceMaster.Where(x => x.EmployeeID == employeeID && x.Date.Date.Equals(today.Date)).ToListAsync();
            return attendance;
        }

        public List<DailyAttendanceMaster> GetAttendanceListByDateAndEmployeeId(int employeeID, DateTime date)
        {
            var attendance = _context.DailyAttendanceMaster.Where(x => x.EmployeeID == employeeID && x.Date.Date.Equals(date.Date)).ToList();
            return attendance;
        }

        public async Task<List<PayrollCutoff>> GetPayrollCutoffs()
        {            
            return await _context.PayrollCutoff.OrderByDescending(x => x.PayrollDate).ToListAsync();
        }

        public async Task<PayrollCutoff> GetLastPayrollCutoff()
        {
            var today = DateTime.Today;
            return await _context.PayrollCutoff.OrderByDescending(x => x.PayrollDate).Where(x => x.PayrollDate.Date < today).FirstOrDefaultAsync();
        }

        public async Task<List<DailyAttendanceMaster>> GetAttendanceCurrentPeriodeByEmployeeId(int employeeID, DateTime startingPeriode, DateTime endingPeriode)
        {            
            var absensiList = await _context.DailyAttendanceMaster.Where(x => x.EmployeeID == employeeID && x.Date.Date >= startingPeriode && x.Date.Date < endingPeriode).ToListAsync();
            return absensiList;
        }

        public async Task<List<DailyAttendanceMaster>> GetAttendanceCurrentPeriodeAllEmployee(DateTime startingPeriode, DateTime endingPeriode)
        {
            var absensiList = await (from d in _context.DailyAttendanceMaster
                                     join e in _context.Employee on d.EmployeeID equals e.EmployeeId
                                     where d.Date.Date >= startingPeriode && d.Date.Date < endingPeriode && e.isActive
                                     select d).ToListAsync();

            return absensiList;
        }

        public async Task<List<DailyAttendanceMaster>> GetAttendanceAllUserPerDay(DateTime date)
        {
            var absensiList = await _context.DailyAttendanceMaster.Where(x => x.Date.Date == date).ToListAsync();
            return absensiList;
        }

        public async Task<DailyAttendanceDetails> GetAttandanceDetails(string date, int employeeid)
        {
            using (SqlConnection sqlcon = new SqlConnection(_conn.DbConn))
            {
                var para = new DynamicParameters();
                para.Add("@Narration", date);
                para.Add("@Employeeid", employeeid);
                var ListofPlan = sqlcon.Query<DailyAttendanceDetails>("GetAttandanceDetails", para, null, true, 0, commandType: CommandType.StoredProcedure).FirstOrDefault();
                return ListofPlan;
            }
        }
        public IList<DailyAttendanceView> DailyAttendanceDetailsSearchGridFill()
        {
			using (SqlConnection sqlcon = new SqlConnection(_conn.DbConn))
			{
                //var para = new DynamicParameters();
                //para.Add("@Narration", strDate);
                var ListofPlan = sqlcon.Query<DailyAttendanceView>("DailyAttendanceDetailsSearchGridFill", null, null, true, 0, commandType: CommandType.StoredProcedure).ToList();
				return ListofPlan;
			}
		}
		public bool DailyAttendanceMasterMasterIdSearch(DateTime strDate)
		{
			decimal deccountMasterId = 0;
				SqlConnection sqlcon = new SqlConnection(_conn.DbConn);
			sqlcon.Open();
			SqlCommand sqlcmd = new SqlCommand("Select DailyAttendanceMasterId  from DailyAttendanceMaster  where Date= @date", sqlcon);
				sqlcmd.CommandType = CommandType.Text;
				sqlcmd.Parameters.Add("@date", SqlDbType.NVarChar).Value = strDate;
				Object obj = sqlcmd.ExecuteScalar();
				if (obj != null)
				{
					deccountMasterId = decimal.Parse(obj.ToString());
				}
				if (deccountMasterId > 0)
				{
					return true;
				}
				else
				{
					return false;
				}
				sqlcon.Close();
		}
		public async Task<int> Save(DailyAttendanceMaster model)
        {            
            int id = 0;
            await _context.DailyAttendanceMaster.AddAsync(model);
            await _context.SaveChangesAsync();
            id = model.DailyAttendanceMasterId;

            return id;            
        }

        public async Task<bool> Update(DailyAttendanceMaster model)
        {
            _context.DailyAttendanceMaster.Update(model);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IList<DailyAttendanceView>> GetAll()
        {
            var result = await (from a in _context.DailyAttendanceMaster
                                select new DailyAttendanceView
                                {
                                    DailyAttendanceMasterId = a.DailyAttendanceMasterId,
                                    Date = a.Date
                                }).ToListAsync();
            return result;
        }
    }
}
