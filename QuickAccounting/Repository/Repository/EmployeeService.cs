using DocumentFormat.OpenXml.InkML;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using QuickAccounting.Data;
using QuickAccounting.Data.HrPayroll;
using QuickAccounting.Data.HrPayrollModel;
using QuickAccounting.Repository.Interface;
using System.Data;

namespace QuickAccounting.Repository.Repository
{
    public class EmployeeService : IEmployee
    {
        private readonly ApplicationDbContext _context;
        private readonly DatabaseConnection _conn;
        public EmployeeService(ApplicationDbContext context , DatabaseConnection conn)
        {
            _context = context;
            _conn = conn;
        }
        public async Task<bool> CheckName(string name)
        {
            var checkResult = (from progm in _context.Employee
                               where progm.EmployeeName == name
                               select progm.EmployeeId).Count();
            if (checkResult > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<int> CheckNameId(string name)
        {
            var checkResult = (from progm in _context.Employee
							   where progm.EmployeeName == name
                               select progm.EmployeeId).Count();
            if (checkResult > 0)
            {

                var checkAccount = (from progm in _context.Employee
									where progm.EmployeeName == name
                                    select progm.EmployeeId).FirstOrDefault();
                return checkAccount;
            }
            else
            {
                return 0;
            }
        }

        public async Task<bool> Delete(int EmployeeId)
        {
            SqlConnection sqlcon = new SqlConnection(_conn.DbConn);
            try
            {
                if (sqlcon.State == ConnectionState.Closed)
                {
                    sqlcon.Open();
                }
                SqlCommand cmd = new SqlCommand("EmployeeDelete", sqlcon);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter para = new SqlParameter();
                para = cmd.Parameters.Add("@EmployeeId", SqlDbType.Int);
                para.Value = EmployeeId;
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

        public async Task<List<EmployeeView>> GetAll()
        {
            var result = await(from a in _context.Employee
							   join b in _context.Designation on a.DesignationId equals b.DesignationId
							   select new EmployeeView
							   {
                                   EmployeeId = a.EmployeeId,
                                   EmployeeName = a.EmployeeName,
                                   EmployeeCode = a.EmployeeCode,
                                   Dob = a.Dob,
                                   Gender = a.Gender,
                                   Address = a.Address,
                                   PhoneNumber = a.PhoneNumber,
                                   Email = a.Email,
                                   DesignationName = b.DesignationName
							   }).ToListAsync();
            return result;
        }

        public async Task<List<Employee>> GetAllEmployeeActive()
        {
            return await _context.Employee.Where(x => x.DesignationId == 1 && x.isActive).ToListAsync();
        }

        public async Task<List<Employee>> GetAllEmployeeActiveBySupervisorID(int supervisorID)
        {
            return await _context.Employee.Where(x => x.DesignationId == 1 && x.isActive && x.SupervisorID == supervisorID).ToListAsync();
        }

        public async Task<List<Employee>> GetAllEmployees()
        {
            return await _context.Employee.Where(x => x.DesignationId == 1).ToListAsync();
        }

        public async Task<List<Employee>> GetSupervisors()
        {
            var result = from e in _context.Employee
                         join u in _context.UserMaster on e.UserID equals u.UserId
                         where u.RoleId == 15 || e.EmployeeId == 6
                         select e;

            return await result.ToListAsync();
        }

        public async Task<int> GetLastEmployeeCode()
        {
            var maxCode = await _context.Employee
                            .MaxAsync(e => Convert.ToInt32(e.EmployeeCode));

            return maxCode;
        }

        public async Task<Employee> GetbyId(int id)
        {
			Employee model = await _context.Employee.FindAsync(id);
            return model;
        }

        public async Task<Employee> GetbyUserId(int id)
        {
            Employee model = await _context.Employee.FirstOrDefaultAsync(x => x.UserID == id);
            return model;
        }

        public async Task<int> Save(Employee model)
        {
            await _context.Employee.AddAsync(model);
            await _context.SaveChangesAsync();
            int id = model.EmployeeId;
            return id;
        }

        public async Task<bool> Update(Employee model)
        {
            _context.Employee.Update(model);            
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<int> InsertSalaryHistory(SalaryHistory model)
        {
            await _context.SalaryHistory.AddAsync(model);
            await _context.SaveChangesAsync();
            int id = model.ID;
            return id;
        }

        public async Task<List<SalaryHistory>> GetSalaryHistoryByEmployeeId(int employeeId)
        {
            var result = await (from sh in _context.SalaryHistory
                                where sh.EmployeeID == employeeId
                                select sh).ToListAsync();
            return result;
        }

        public async Task DeleteSalaryHistory(int id)
        {
            var entity = await _context.SalaryHistory.FindAsync(id);
            if (entity != null)
            {
                _context.SalaryHistory.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }
    }
}
