using QuickAccounting.Data.HrPayroll;

namespace QuickAccounting.Repository.Interface
{
    public interface ILoanService
    {
        Task<List<LoanMaster>> GetAllAsync();
        Task<LoanMaster?> GetByIdAsync(int id);
        Task<int> CreateLoanAsync(int employeeId, decimal loanTotal, int paymentTermsInMonths, int createdBy);
        Task<bool> UpdateAsync(LoanMaster model);
        Task<bool> DeleteAsync(int id);
        Task<List<LoanMaster>> GetByEmployeeAsync(int employeeId);
        Task<List<LoanDetails>> GetLoanDetailsAsync(int loanMasterId);
        Task<bool> MarkInstallmentPaidAsync(int loanDetailId, decimal paidAmount, DateTime paidDate);
        Task<decimal> GetCurrentMonthPayment(int employeeId, int month, int year);
        Task<bool> RecreateLoanDetailsAsync(int loanMasterId, decimal loanTotal, int paymentTermsInMonths);
    }
}