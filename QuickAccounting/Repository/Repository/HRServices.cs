﻿using Microsoft.EntityFrameworkCore;
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
									 where p.ForDate >= DateTime.Now && employeeUnderSupervisor.Contains(p.EmployeeID)
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
									 where p.ForDate >= DateTime.Now && p.EmployeeID == employeeID
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
                                     where p.ForDate >= DateTime.Now
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

        public async Task<bool> DeletePerijinan(Penalty model)
        {
            _context.Penalty.Remove(model);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
