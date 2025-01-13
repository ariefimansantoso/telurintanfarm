using Microsoft.EntityFrameworkCore;
using OfficeOpenXml.FormulaParsing.Excel.Functions.DateTime;
using QuickAccounting.Data;
using QuickAccounting.Data.HrPayroll;
using QuickAccounting.Pages.HumanResorcePage.EmployeePage;
using QuickAccounting.Repository.Interface;

namespace QuickAccounting.Repository.Repository
{
    public class HRServices : IHRServices
    {
        private readonly ApplicationDbContext _context;

        public HRServices(ApplicationDbContext context)
        {
            _context = context;
        }

        public IQueryable<Perijinan> PerijinanQuery()
        {
            return _context.Perijinan.AsQueryable();
        }

        public List<dynamic> GetPerijinanBySupervisorID(int supervisorID)
        {
            var employeeUnderSupervisor = _context.Employee.Where(x => x.SupervisorID == supervisorID).Select(x => x.EmployeeId).ToList();

			var perijinanWithName = (from p in _context.Perijinan
									 join em in _context.Employee on p.EmployeeID equals em.EmployeeId
									 where p.ForDate >= DateTime.Now.Date && employeeUnderSupervisor.Contains(p.EmployeeID)
									 orderby p.ForDate ascending
									 select new
									 {
										 ID = p.ID,
										 EmployeeID = p.EmployeeID,
										 DateSubmitted = p.DateSubmitted,
										 SubmittedFor = p.SubmittedFor,
										 SubmittedDesc = p.SubmittedDesc,
										 DocPhoto = p.DocPhoto,
										 ForDate = p.ForDate,
										 IsApproved = p.IsApproved,
										 ApprovalDescription = p.ApprovalDescription,
										 ActionDate = p.ActionDate,
										 ActionByEmployeeID = p.ActionByEmployeeID,
										 EmployeeName = em.EmployeeName
									 }).ToList<dynamic>();

			return perijinanWithName;
		}

        public List<dynamic> GetListPerijinanByEmployeeID(int employeeID)
        {
			var perijinanWithName = (from p in _context.Perijinan
									 join em in _context.Employee on p.EmployeeID equals em.EmployeeId                                     
									 where p.ForDate >= DateTime.Now.Date && p.EmployeeID == employeeID
									 orderby p.ForDate ascending
									 select new
									 {
										 ID = p.ID,
										 EmployeeID = p.EmployeeID,
										 DateSubmitted = p.DateSubmitted,
										 SubmittedFor = p.SubmittedFor,
										 SubmittedDesc = p.SubmittedDesc,
										 DocPhoto = p.DocPhoto,
										 ForDate = p.ForDate,
										 IsApproved = p.IsApproved,
										 ApprovalDescription = p.ApprovalDescription,
										 ActionDate = p.ActionDate,
										 ActionByEmployeeID = p.ActionByEmployeeID,
										 EmployeeName = em.EmployeeName
									 }).ToList<dynamic>();

			return perijinanWithName;
		}

		public List<dynamic> GetPerijinanUnApproved()
        {
            var perijinanWithName = (from p in _context.Perijinan
                                     join em in _context.Employee on p.EmployeeID equals em.EmployeeId
                                     where p.ForDate >= DateTime.Now.Date
                                     orderby p.ForDate ascending
                                     select new 
                                     {
										 ID = p.ID,
										 EmployeeID = p.EmployeeID,
										 DateSubmitted = p.DateSubmitted,
										 SubmittedFor = p.SubmittedFor,
										 SubmittedDesc = p.SubmittedDesc,
										 DocPhoto = p.DocPhoto,
										 ForDate = p.ForDate,
										 IsApproved = p.IsApproved,
										 ApprovalDescription = p.ApprovalDescription,
										 ActionDate = p.ActionDate,
										 ActionByEmployeeID = p.ActionByEmployeeID,
										 EmployeeName = em.EmployeeName
                                     }).ToList<dynamic>();

            return perijinanWithName;

		}

		public List<dynamic> GetPerijinanUnApprovedForAdmin(int month, int year)
		{
			var perijinanWithName = (from p in _context.Perijinan
									 join em in _context.Employee on p.EmployeeID equals em.EmployeeId
                                     where p.ForDate.Year == year && p.ForDate.Month == month
									 orderby p.ForDate descending
									 select new
									 {
										 ID = p.ID,
										 EmployeeID = p.EmployeeID,
										 DateSubmitted = p.DateSubmitted,
										 SubmittedFor = p.SubmittedFor,
										 SubmittedDesc = p.SubmittedDesc,
										 DocPhoto = p.DocPhoto,
										 ForDate = p.ForDate,
										 IsApproved = p.IsApproved,
										 ApprovalDescription = p.ApprovalDescription,
										 ActionDate = p.ActionDate,
										 ActionByEmployeeID = p.ActionByEmployeeID,
										 EmployeeName = em.EmployeeName
									 }).ToList<dynamic>();

			return perijinanWithName;

		}

		public List<dynamic> GetPerijinanUnApprovedForSupervisor(int supervisorId, int month, int year)
		{
			var perijinanWithName = (from p in _context.Perijinan
									 join em in _context.Employee on p.EmployeeID equals em.EmployeeId
									 where p.ForDate.Year == year && p.ForDate.Month == month && em.SupervisorID == supervisorId
									 orderby p.ForDate descending
									 select new
									 {
										 ID = p.ID,
										 EmployeeID = p.EmployeeID,
										 DateSubmitted = p.DateSubmitted,
										 SubmittedFor = p.SubmittedFor,
										 SubmittedDesc = p.SubmittedDesc,
										 DocPhoto = p.DocPhoto,
										 ForDate = p.ForDate,
										 IsApproved = p.IsApproved,
										 ApprovalDescription = p.ApprovalDescription,
										 ActionDate = p.ActionDate,
										 ActionByEmployeeID = p.ActionByEmployeeID,
										 EmployeeName = em.EmployeeName
									 }).ToList<dynamic>();

			return perijinanWithName;

		}

		public List<Perijinan> GetPerijinanByEmployeeIDInPeriodePayroll(int employeeID, DateTime from, DateTime to)
        {
            var perijinans = _context.Perijinan.Where(x => x.EmployeeID == employeeID && x.IsApproved.HasValue && x.IsApproved.Value == true &&
                                                        x.ForDate.Date >= from && x.ForDate <= to).ToList();
            return perijinans;
        }

        public List<Perijinan> GetPerijinanByEmployeeIDAndDate(int employeeID, DateTime atDate)
        {
            var perijinans = _context.Perijinan.Where(x => x.EmployeeID == employeeID && x.IsApproved.HasValue && x.IsApproved.Value == true &&
                                                        x.ForDate.Date == atDate.Date).ToList();
            return perijinans;
        }

        public List<AbsensiPotongan> GetByCurrentMonthYearAndEmployeeId(int employeeId, int month, int year)
        {
            var potongan = _context.AbsensiPotongan.Where(x => x.KARYAWAN_ID == employeeId && x.CUT_OFF_BULAN == month && x.CUT_OFF_TAHUN == year).ToList();
            return potongan;
        }

        public List<Penalty> GetPenaltyByCurrentMonthYearAndEmployeeId(int employeeId, DateTime from, DateTime to)
        {
            var potongan = _context.Penalty.Where(x => x.EmployeeID == employeeId && 
											x.ForDate.Date >= from.Date && 
											x.ForDate.Date <= to.Date).OrderByDescending(x => x.ForDate).ToList();
            return potongan;
        }

        public List<Penalty> GetPenaltyByCurrentMonthYearAndSupervisorId(int supervisorId, DateTime fromDate, DateTime toDate)
        {
            var potongan = (from p in _context.Penalty
						   join em in _context.Employee on p.EmployeeID equals em.EmployeeId
						   where p.ForDate.Date >= fromDate.Date &&
                                 p.ForDate.Date <= toDate.Date &&
								 em.SupervisorID == supervisorId
                            orderby p.ForDate descending
                            select p).ToList();

            return potongan;
        }

        public List<Penalty> GetAllPenaltyByCurrentMonthYear(DateTime fromDate, DateTime toDate)
        {
            var potongan = (from p in _context.Penalty
                            where p.ForDate.Date >= fromDate.Date &&
                                  p.ForDate.Date <= toDate.Date
							orderby p.ForDate descending
                            select p).ToList();

            return potongan;
        }

        public async Task<int> InsertPerijinan(Perijinan perijinan)
        {
            await _context.Perijinan.AddAsync(perijinan);
            return await _context.SaveChangesAsync();
        }

        public async Task<bool> UpdatePerijinan(Perijinan model)
        {            
			_context.Perijinan.Attach(model);
			_context.Entry(model).State = EntityState.Modified;
			await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeletePerijinan(Perijinan model)
        {
            _context.Perijinan.Remove(model);
            await _context.SaveChangesAsync();
            return true;
        }

        public IQueryable<Penalty> PenaltyQuery()
        {
            return _context.Penalty.AsQueryable();
        }

        public async Task<int> InsertPenalty(Penalty perijinan)
        {
            await _context.Penalty.AddAsync(perijinan);
            return await _context.SaveChangesAsync();
        }

        public async Task<bool> UpdatePenalty(Penalty model)
        {
            _context.Penalty.Update(model);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeletePenalty(Penalty model)
        {
            _context.Penalty.Remove(model);
            await _context.SaveChangesAsync();
            return true;
        }

		public decimal GetProsentasePremi(int masaKerjaDalamTahun)
		{
            double O2 = Convert.ToDouble(masaKerjaDalamTahun); // Replace with the value of O2 (in years)

            double result = Math.Max(15, Math.Min((O2 / 11) * 25, 25));

			return Convert.ToDecimal(result);
        }

		public int GetMasaKerja(DateTime tanggalMasukKerja)
		{            
            DateTime today = DateTime.Today;

            // Calculate the difference in years
            int differenceInYears = today.Year - tanggalMasukKerja.Year;

            // Adjust if the current date hasn't reached the anniversary of L2 this year
            if (tanggalMasukKerja.Date > today.AddYears(-differenceInYears))
            {
                differenceInYears--;
            }

			return differenceInYears;
        }
    }
}
