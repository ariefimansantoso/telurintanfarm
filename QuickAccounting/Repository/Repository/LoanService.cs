using Microsoft.EntityFrameworkCore;
using QuickAccounting.Data;
using QuickAccounting.Data.HrPayroll;
using QuickAccounting.Repository.Interface;
using System.Runtime.CompilerServices;

namespace QuickAccounting.Repository.Repository
{
    public class LoanService : ILoanService
    {
        private readonly ApplicationDbContext _context;

        public LoanService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<LoanMaster>> GetAllAsync()
        {
            return await _context.LoanMaster
                .Include(l => l.Details)
                .OrderByDescending(l => l.CreatedDate)
                .ToListAsync();
        }

        public async Task<LoanMaster?> GetByIdAsync(int id)
        {
            return await _context.LoanMaster
                .Include(l => l.Details)
                .FirstOrDefaultAsync(l => l.ID == id);
        }

        public async Task<int> CreateLoanAsync(int employeeId, decimal loanTotal, int paymentTermsInMonths, int createdBy)
        {
            if (paymentTermsInMonths <= 0) throw new ArgumentException("Payment terms must be greater than zero.", nameof(paymentTermsInMonths));

            var loan = new LoanMaster
            {
                EmployeeID = employeeId,
                LoanTotal = Math.Round(loanTotal, 2),
                CreatedDate = DateTime.Now,
                CreatedBy = createdBy,
                PaymentTermsInMonths = paymentTermsInMonths,
                LoanCompleted = false
            };

            await _context.LoanMaster.AddAsync(loan);
            await _context.SaveChangesAsync(); // to get loan.ID

            // Calculate equal installments rounding to 2 decimals and adjust last installment for remainder
            var baseInstallment = Math.Floor((loan.LoanTotal / paymentTermsInMonths) * 100) / 100m; // truncate to 2 decimals
            var installments = new List<LoanDetails>();
            decimal sumAssigned = 0m;

            for (int i = 0; i < paymentTermsInMonths; i++)
            {
                var dueDate = DateTime.Now.AddMonths(i);
                var month = dueDate.Month;
                var year = dueDate.Year;

                decimal amount = baseInstallment;
                // last installment gets the remainder
                if (i == paymentTermsInMonths - 1)
                {
                    amount = loan.LoanTotal - sumAssigned;
                    amount = Math.Round(amount, 2);
                }

                sumAssigned += amount;

                installments.Add(new LoanDetails
                {
                    LoanMasterId = loan.ID,
                    Month = month,
                    Year = year,
                    Paid = false,
                    PaidDate = DateTime.Now,
                    PaidAmount = amount
                });
            }

            await _context.LoanDetails.AddRangeAsync(installments);
            await _context.SaveChangesAsync();

            return loan.ID;
        }

        public async Task<bool> UpdateAsync(LoanMaster model)
        {
            var existing = await _context.LoanMaster.FindAsync(model.ID);
            if (existing == null) return false;

            existing.EmployeeID = model.EmployeeID;
            existing.LoanTotal = model.LoanTotal;
            existing.PaymentTermsInMonths = model.PaymentTermsInMonths;
            existing.LoanCompleted = model.LoanCompleted;
            // do not overwrite CreatedDate/CreatedBy

            _context.LoanMaster.Update(existing);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var existing = await _context.LoanMaster.FindAsync(id);
            if (existing == null) return false;

            // remove details first
            var details = _context.LoanDetails.Where(d => d.LoanMasterId == id);
            _context.LoanDetails.RemoveRange(details);

            _context.LoanMaster.Remove(existing);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<LoanMaster>> GetByEmployeeAsync(int employeeId)
        {
            return await _context.LoanMaster
                .Where(l => l.EmployeeID == employeeId && l.LoanCompleted == false)
                .Include(l => l.Details)
                .OrderByDescending(l => l.CreatedDate)
                .ToListAsync();
        }

        public async Task<List<LoanDetails>> GetLoanDetailsAsync(int loanMasterId)
        {
            return await _context.LoanDetails
                .Where(d => d.LoanMasterId == loanMasterId)
                .OrderBy(d => d.Year).ThenBy(d => d.Month)
                .ToListAsync();
        }

        public async Task<bool> MarkInstallmentPaidAsync(int loanDetailId, decimal paidAmount, DateTime paidDate)
        {
            var detail = await _context.LoanDetails.FindAsync(loanDetailId);
            if (detail == null) return false;

            detail.Paid = true;
            detail.PaidAmount = Math.Round(paidAmount, 2);
            detail.PaidDate = paidDate;

            _context.LoanDetails.Update(detail);
            await _context.SaveChangesAsync();

            // check if all installments paid and update LoanMaster.LoanCompleted
            var loanId = detail.LoanMasterId;
            var pending = await _context.LoanDetails.AnyAsync(d => d.LoanMasterId == loanId && d.Paid == false);
            var loan = await _context.LoanMaster.FindAsync(loanId);
            if (loan != null)
            {
                loan.LoanCompleted = !pending;
                _context.LoanMaster.Update(loan);
                await _context.SaveChangesAsync();
            }

            return true;
        }

        public async Task<decimal> GetCurrentMonthPayment(int employeeId, int month, int year)
        {
            var loanDetails = await _context.LoanDetails
                .Include(d => d.LoanMaster)
                .Where(d => d.LoanMaster.LoanCompleted == false && d.LoanMaster.EmployeeID == employeeId && d.Month == month && d.Year == year)
                .FirstOrDefaultAsync();

            return loanDetails != null ? loanDetails.PaidAmount : 0;
        }

        // New: Recreate details for an existing loan master (delete existing details then insert newly calculated schedule)
        public async Task<bool> RecreateLoanDetailsAsync(int loanMasterId, decimal loanTotal, int paymentTermsInMonths)
        {
            if (paymentTermsInMonths <= 0) throw new ArgumentException("Payment terms must be greater than zero.", nameof(paymentTermsInMonths));

            var loan = await _context.LoanMaster.FindAsync(loanMasterId);
            if (loan == null) return false;

            // Remove existing details
            var existing = _context.LoanDetails.Where(d => d.LoanMasterId == loanMasterId);
            _context.LoanDetails.RemoveRange(existing);

            // Update master
            loan.LoanTotal = Math.Round(loanTotal, 2);
            loan.PaymentTermsInMonths = paymentTermsInMonths;
            loan.LoanCompleted = false;
            _context.LoanMaster.Update(loan);
            await _context.SaveChangesAsync();

            // Recreate installments
            var baseInstallment = Math.Floor((loan.LoanTotal / paymentTermsInMonths) * 100) / 100m;
            var installments = new List<LoanDetails>();
            decimal sumAssigned = 0m;

            for (int i = 0; i < paymentTermsInMonths; i++)
            {
                var dueDate = DateTime.Now.AddMonths(i);
                var month = dueDate.Month;
                var year = dueDate.Year;

                decimal amount = baseInstallment;
                if (i == paymentTermsInMonths - 1)
                {
                    amount = loan.LoanTotal - sumAssigned;
                    amount = Math.Round(amount, 2);
                }

                sumAssigned += amount;

                installments.Add(new LoanDetails
                {
                    LoanMasterId = loanMasterId,
                    Month = month,
                    Year = year,
                    Paid = false,
                    PaidDate = DateTime.Now,
                    PaidAmount = amount
                });
            }

            await _context.LoanDetails.AddRangeAsync(installments);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}