using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using QuickAccounting.Data;
using QuickAccounting.Data.Inventory;
using QuickAccounting.Data.Setting;
using QuickAccounting.Data.ViewModel;
using QuickAccounting.Repository.Interface;
using System.Data;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace QuickAccounting.Repository.Repository
{
    public class PaymentInService : IPaymentIn
    {
        private readonly ApplicationDbContext _context;
        private readonly DatabaseConnection _conn;
        public PaymentInService(ApplicationDbContext context, DatabaseConnection conn)
        {
            _context = context;
            _conn = conn;
        }
        public async Task<bool> Delete(ReceiptMaster master)
        {
            try
            {
                //GetBalance
                var result = await (from a in _context.ReceiptDetails
                                    where a.ReceiptMasterId == master.ReceiptMasterId
                                    select new
                                    {
                                        SalesMasterId = a.SalesMasterId,
                                        ReceiveAmount = a.ReceiveAmount
                                    }).ToListAsync();
                if (result != null)
                {
                    foreach (var item in result)
                    {
                        SalesMaster purchase = new SalesMaster();
                        using (SqlConnection sqlconn = new SqlConnection(_conn.DbConn))
                        {
                            var paras = new DynamicParameters();
                            paras.Add("@SalesMasterId", item.SalesMasterId);
                            purchase = sqlconn.Query<SalesMaster>("SELECT *FROM SalesMaster where SalesMasterId=@SalesMasterId", paras, null, true, 0, commandType: CommandType.Text).FirstOrDefault();
                        }
                        if (purchase != null)
                        {
                            decimal decPay = purchase.PayAmount;
                            purchase.PayAmount = item.ReceiveAmount - decPay;
                            purchase.PreviousDue = (purchase.GrandTotal) - (item.ReceiveAmount - decPay);
                            purchase.BalanceDue = (purchase.GrandTotal) - (item.ReceiveAmount - decPay);
                            if (purchase.BalanceDue == 0)
                            {
                                purchase.Status = "Paid";
                            }
                            else
                            {
                                purchase.Status = "Partial";
                            }
                            _context.SalesMaster.Update(purchase);
                            await _context.SaveChangesAsync();
                        }
                    }
                }
                SqlConnection sqlcon = new SqlConnection(_conn.DbConn);
                if (sqlcon.State == ConnectionState.Closed)
                {
                    sqlcon.Open();
                }
                SqlCommand cmd = new SqlCommand("PaymentInDelete", sqlcon);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter para = new SqlParameter();
                para = cmd.Parameters.Add("@ReceiptMasterId", SqlDbType.Int);
                para.Value = master.ReceiptMasterId;
                para = cmd.Parameters.Add("@VoucherTypeId", SqlDbType.Int);
                para.Value = master.VoucherTypeId;
                int rowAffacted = cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<IList<PaymentReceiveView>> GetAll(DateTime FromDate, DateTime ToDate, string VoucherNo, string type)
        {
            using (SqlConnection sqlcon = new SqlConnection(_conn.DbConn))
            {
                var para = new DynamicParameters();
                para.Add("@FromDate", FromDate);
                para.Add("@ToDate", ToDate);
                para.Add("@VoucherNo", VoucherNo);
                para.Add("@Type", type);
                var ListofPlan = sqlcon.Query<PaymentReceiveView>("ReceiptViewAll", para, null, true, 0, commandType: CommandType.StoredProcedure).ToList();
                return ListofPlan;
            }
        }
        public async Task<IList<ReceiptDetailsView>> ReceiptDetailsAllView(int id)
        {
            var result = await (from a in _context.ReceiptDetails
                                join c in _context.AccountLedger on a.LedgerId equals c.LedgerId
                                where a.ReceiptMasterId == id
                                select new ReceiptDetailsView
                                {
                                    Id = id + 1,
                                    ReceiptDetailsId = a.ReceiptDetailsId,
                                    ReceiptMasterId = a.ReceiptMasterId,
                                    LedgerId = a.LedgerId,
                                    TotalAmount = a.TotalAmount,
                                    DueAmount = a.DueAmount,
                                    ReceiveAmount = a.ReceiveAmount,
                                    SalesMasterId = a.SalesMasterId,
                                    LedgerName = c.LedgerName
                                }).ToListAsync();
            return result;
        }

        public async Task<ReceiptMaster> GetbyId(int id)
        {
            ReceiptMaster model = await _context.ReceiptMaster.FindAsync(id);
            return model;
        }

        public async Task<string> GetSerialNo()
        {
            using (SqlConnection sqlcon = new SqlConnection(_conn.DbConn))
            {
                var val = sqlcon.Query<string>("SELECT ISNULL(MAX(SerialNo+1),1) as VoucherNo FROM ReceiptMaster", null, null, true, 0, commandType: CommandType.Text).FirstOrDefault();
                return val;
            }
        }

        public async Task<PaymentReceiveView> PaymentInView(int id)
        {
            var result = await (from a in _context.ReceiptMaster
                                join c in _context.AccountLedger on a.LedgerId equals c.LedgerId
                                where a.ReceiptMasterId == id
                                select new PaymentReceiveView
                                {
                                    ReceiptMasterId = a.ReceiptMasterId,
                                    Date = a.Date,
                                    VoucherNo = a.VoucherNo,
                                    Amount = a.Amount,
                                    Narration = a.Narration,
                                    PaymentType = a.PaymentType,
                                    Type = a.Type,
                                    UserId = a.UserId,
                                    AddedDate = a.AddedDate,
                                    LedgerName = c.LedgerName,
                                    Email = c.Email,
                                    Address = c.Address
                                }).FirstOrDefaultAsync();
            return result;
        }
        public async Task<IList<PaymentReceiveView>> PaymentInDetailsView(int id)
        {
            var result = await (from a in _context.ReceiptDetails
                                join c in _context.AccountLedger on a.LedgerId equals c.LedgerId
                                where a.ReceiptMasterId == id
                                select new PaymentReceiveView
                                {
                                    ReceiptDetailsId = a.ReceiptDetailsId,
                                    ReceiptMasterId = a.ReceiptMasterId,
                                    LedgerId = a.LedgerId,
                                    Amount = a.TotalAmount,
                                    DueAmount = a.DueAmount,
                                    ReceiveAmount = a.ReceiveAmount,
                                    SalesMasterId = a.SalesMasterId,
                                    LedgerName = c.LedgerName
                                }).ToListAsync();
            return result;
        }

        public async Task<int> Save(ReceiptMaster model)
        {
            _context.ReceiptMaster.Add(model);
            await _context.SaveChangesAsync();
            int id = model.ReceiptMasterId;



            //ReceiptDetails table
            foreach (var item in model.listOrder)
            {
                //AddReceiptDetails
                ReceiptDetails details = new ReceiptDetails();
                if (item.LedgerId > 0)
                {
                    details.ReceiptMasterId = id;
                    details.LedgerId = item.LedgerId;
                    details.SalesMasterId = item.SalesMasterId;
                    details.TotalAmount = item.TotalAmount;
                    details.ReceiveAmount = item.ReceiveAmount;
                    details.DueAmount = item.DueAmount;
                    _context.ReceiptDetails.Add(details);
                    await _context.SaveChangesAsync();
                    int intPurchaseDId = details.ReceiptDetailsId;
                }
            }
            return id;
        }

        public async Task<bool> ApprovedOk(ReceiptMaster model)
        {
            try
            {
                _context.ReceiptMaster.Update(model);
                await _context.SaveChangesAsync();
                //ReceiptDetails table
                foreach (var item in model.listOrder)
                {
                    //AddReceiptDetails
                    ReceiptDetails details = new ReceiptDetails();
                    if (model.Amount > 0)
                    {
                        details.ReceiptDetailsId = item.ReceiptDetailsId;
                        details.ReceiptMasterId = model.ReceiptMasterId;
                        details.LedgerId = item.LedgerId;
                        details.SalesMasterId = item.SalesMasterId;
                        details.TotalAmount = item.TotalAmount;
                        details.ReceiveAmount = item.ReceiveAmount;
                        details.DueAmount = item.DueAmount;
                        _context.ReceiptDetails.Update(details);
                        await _context.SaveChangesAsync();

                        if (item.SalesMasterId > 0)
                        {
                            SalesMaster master = new SalesMaster();
                            using (SqlConnection sqlcon = new SqlConnection(_conn.DbConn))
                            {
                                var para = new DynamicParameters();
                                para.Add("@SalesMasterId", item.SalesMasterId);
                                master = sqlcon.Query<SalesMaster>("SELECT *FROM SalesMaster where SalesMasterId=@SalesMasterId", para, null, true, 0, commandType: CommandType.Text).FirstOrDefault();
                            }
                            decimal decPay = master.PayAmount;
                            master.PayAmount = item.ReceiveAmount + decPay;
                            master.PreviousDue = (master.GrandTotal) - (item.ReceiveAmount + decPay);
                            master.BalanceDue = (master.GrandTotal) - (item.ReceiveAmount + decPay);
                            if (master.BalanceDue == 0)
                            {
                                master.Status = "Paid";
                            }
                            else
                            {
                                master.Status = "Partial";
                            }
                            _context.SalesMaster.Update(master);
                            await _context.SaveChangesAsync();
                        }
                        //CashAndBank
                        LedgerPosting cashPosting = new LedgerPosting();
                        cashPosting.Date = model.Date;
                        cashPosting.NepaliDate = String.Empty;
                        cashPosting.LedgerId = item.LedgerId;
                        cashPosting.Debit = model.Amount;
                        cashPosting.Credit = 0;
                        cashPosting.VoucherNo = model.VoucherNo;
                        cashPosting.DetailsId = model.ReceiptMasterId;
                        cashPosting.YearId = model.FinancialYearId;
                        cashPosting.InvoiceNo = model.VoucherNo;
                        cashPosting.VoucherTypeId = model.VoucherTypeId;
                        cashPosting.CompanyId = model.CompanyId;
                        cashPosting.LongReference = model.Narration;
                        cashPosting.ReferenceN = model.Narration;
                        cashPosting.ChequeNo = String.Empty;
                        cashPosting.ChequeDate = String.Empty;
                        cashPosting.AddedDate = DateTime.Now;
                        _context.LedgerPosting.Add(cashPosting);
                        await _context.SaveChangesAsync();
                    }
                }
                //LedgerPosting
                //Customer
                LedgerPosting ledgerPosting = new LedgerPosting();
                ledgerPosting.Date = model.Date;
                ledgerPosting.NepaliDate = String.Empty;
                ledgerPosting.LedgerId = model.LedgerId;
                ledgerPosting.Debit = 0;
                ledgerPosting.Credit = model.Amount;
                ledgerPosting.VoucherNo = model.VoucherNo;
                ledgerPosting.DetailsId = model.ReceiptMasterId;
                ledgerPosting.YearId = model.FinancialYearId;
                ledgerPosting.InvoiceNo = model.VoucherNo;
                ledgerPosting.VoucherTypeId = model.VoucherTypeId;
                ledgerPosting.CompanyId = model.CompanyId;
                ledgerPosting.LongReference = model.Narration;
                ledgerPosting.ReferenceN = model.Narration;
                ledgerPosting.ChequeNo = String.Empty;
                ledgerPosting.ChequeDate = String.Empty;
                ledgerPosting.AddedDate = DateTime.Now;
                _context.LedgerPosting.Add(ledgerPosting);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        //public async Task SettlementPOS(string userName)
        //{
        //    var salesMaster = await _context.SalesMaster.ToListAsync();

        //    foreach (var sales in salesMaster)
        //    {
        //        var receiptMaster = await _context.ReceiptMaster.Where(x => x.SalesMasterId == sales.SalesMasterId).FirstOrDefaultAsync();
        //        if (receiptMaster == null)
        //        {
        //            ReceiptMaster rm = new ReceiptMaster();
        //            rm.VoucherTypeId = 3;
        //            rm.PaymentType = "Draft";
        //            rm.Type = "CustomerPayment";
        //            rm.UserId = userName;
        //            rm.FinancialYearId = 2;
        //            rm.CompanyId = 1;
        //            rm.SalesMasterId = sales.SalesMasterId;
        //            rm.Narration = string.Empty;
        //            rm.AddedDate = DateTime.Now;

        //            foreach (var item in TodoList)
        //            {
        //                ReceiptDetails details = new ReceiptDetails();
        //                if (item != null)
        //                {
        //                    details.SalesMasterId = item.SalesMasterId;
        //                    details.LedgerId = intCashBank;
        //                    details.TotalAmount = item.GrandTotal;
        //                    details.ReceiveAmount = item.ReceiveAmount;
        //                    details.DueAmount = 0;
        //                    master.listOrder.Add(details);
        //                }
        //            }
        //            var result = await _payment.Save(master);
        //        }
        //    }

        //}

        public async Task<List<SalesMaster>> GetLedgerPostingsRetail(DateTime dtFrom, DateTime dtTo, string paymentType)
        {
            var ledgerPostings = await (from sm in _context.SalesMaster
                                        where
                                              sm.TipePembayaran.ToLower() == paymentType &&
                                              sm.Date >= dtFrom && sm.Date <= dtTo &&
                                              sm.PaymentStatus == "Approved" &&
                                              sm.LedgerId == 19
                                        select sm).ToListAsync();

            return ledgerPostings;
        }

        public async Task<dynamic> GetSalesPartai(DateTime dtFrom, DateTime dtTo, int ledgerId)
        {
            var ledgerPostings = await (from rm in _context.SalesMaster
                                        join rem in _context.ReceiptMaster on rm.SalesMasterId equals rem.SalesMasterId
                                        join lp in _context.ReceiptDetails on rem.ReceiptMasterId equals lp.ReceiptMasterId
                                        join al in _context.AccountLedger on rm.LedgerId equals al.LedgerId
                                        join sd in _context.SalesDetails on rm.SalesMasterId equals sd.SalesMasterId
                                        join p in _context.Product on sd.ProductId equals p.ProductId
                                        where
                                                rm.LedgerId >= 19 &&
                                              rem.Date >= dtFrom && rem.Date <= dtTo &&
                                              lp.LedgerId == ledgerId &&
                                              p.ProductCode.StartsWith("TA-")
                                        select new
                                        {
                                            LedgerName = al.LedgerName,
                                            Amount = lp.ReceiveAmount
                                        }).ToListAsync<dynamic>();

            return ledgerPostings;
        }

        public async Task<dynamic> GetSalesNonTelur(DateTime dtFrom, DateTime dtTo, int ledgerId)
        {
            var ledgerPostings = await (from rm in _context.SalesMaster
                                        join rem in _context.ReceiptMaster on rm.SalesMasterId equals rem.SalesMasterId
                                        join lp in _context.ReceiptDetails on rem.ReceiptMasterId equals lp.ReceiptMasterId
                                        join al in _context.AccountLedger on rm.LedgerId equals al.LedgerId
                                        join sd in _context.SalesDetails on rm.SalesMasterId equals sd.SalesMasterId
                                        join p in _context.Product on sd.ProductId equals p.ProductId
                                        where
                                                rm.LedgerId >= 19 &&
                                              rem.Date >= dtFrom && rem.Date <= dtTo &&
                                              lp.LedgerId == ledgerId &&
                                              (!p.ProductCode.StartsWith("TA-") && !p.ProductCode.StartsWith("TB-") &&
                                               !p.ProductCode.StartsWith("TC-") && !p.ProductCode.StartsWith("TD-") &&
                                               !p.ProductCode.StartsWith("TE-") && !p.ProductCode.StartsWith("TF-"))
                                        select new
                                        {
                                            LedgerName = al.LedgerName,
                                            Amount = lp.ReceiveAmount
                                        }).ToListAsync<dynamic>();

            return ledgerPostings;
        }

        public async Task<dynamic> GetSalesPartaiKG(DateTime dtFrom, DateTime dtTo, int ledgerId)
		{
			var ledgerPostings = await (from rm in _context.SalesMaster
                                        join sd in _context.SalesDetails on rm.SalesMasterId equals sd.SalesMasterId
										join rem in _context.ReceiptMaster on rm.SalesMasterId equals rem.SalesMasterId
										join lp in _context.ReceiptDetails on rem.ReceiptMasterId equals lp.ReceiptMasterId
										join al in _context.AccountLedger on rm.LedgerId equals al.LedgerId
                                        join p in _context.Product on sd.ProductId equals p.ProductId
                                        where
												rm.LedgerId >= 19 &&
											  rem.Date >= dtFrom && rem.Date <= dtTo &&
											  lp.LedgerId == ledgerId &&
                                              p.ProductCode.StartsWith("TA-")
                                        select new
										{
											LedgerName = al.LedgerName,
											TotalKG = sd.Qty
										}).ToListAsync<dynamic>();

			return ledgerPostings;
		}

		public async Task<bool> Update(ReceiptMaster model)
        {
            try
            {
                _context.ReceiptMaster.Update(model);
                await _context.SaveChangesAsync();

                //DeletePaymetLedger
                using (SqlConnection sqlcon = new SqlConnection(_conn.DbConn))
                {
                    var paraScDelete = new DynamicParameters();
                    paraScDelete.Add("@DetailsId", model.ReceiptMasterId);
                    paraScDelete.Add("@VoucherTypeId", model.VoucherTypeId);
                    var valueScDelete = sqlcon.Query<int>("DELETE FROM LedgerPosting where DetailsId=@DetailsId AND VoucherTypeId=@VoucherTypeId", paraScDelete, null, true, 0, commandType: CommandType.Text);
                }
                //ReceiptDetails table
                foreach (var item in model.listOrder)
                {
                    //AddReceiptDetails
                    ReceiptDetails details = new ReceiptDetails();
                    if (item.ReceiptDetailsId == 0)
                    {
                        details.ReceiptMasterId = model.ReceiptMasterId;
                        details.LedgerId = item.LedgerId;
                        details.TotalAmount = item.TotalAmount;
                        details.ReceiveAmount = item.ReceiveAmount;
                        details.DueAmount = item.DueAmount;
                        _context.ReceiptDetails.Add(details);
                        await _context.SaveChangesAsync();

                        //PostingCashBankLedger
                        LedgerPosting cashPosting = new LedgerPosting();
                        cashPosting.Date = model.Date;
                        cashPosting.NepaliDate = String.Empty;
                        cashPosting.LedgerId = item.LedgerId;
                        cashPosting.Debit = item.TotalAmount;
                        cashPosting.Credit = 0;
                        cashPosting.VoucherNo = model.VoucherNo;
                        cashPosting.DetailsId = model.ReceiptMasterId;
                        cashPosting.YearId = model.FinancialYearId;
                        cashPosting.InvoiceNo = model.VoucherNo;
                        cashPosting.VoucherTypeId = model.VoucherTypeId;
                        cashPosting.CompanyId = model.CompanyId;
                        cashPosting.LongReference = model.Narration;
                        cashPosting.ReferenceN = model.Narration;
                        cashPosting.ChequeNo = String.Empty;
                        cashPosting.ChequeDate = String.Empty;
                        cashPosting.AddedDate = DateTime.Now;
                        _context.LedgerPosting.Add(cashPosting);
                        await _context.SaveChangesAsync();
                    }
                    else
                    {
                        details.ReceiptDetailsId = item.ReceiptDetailsId;
                        details.ReceiptMasterId = model.ReceiptMasterId;
                        details.LedgerId = item.LedgerId;
                        details.TotalAmount = item.TotalAmount;
                        details.ReceiveAmount = item.ReceiveAmount;
                        details.DueAmount = item.DueAmount;
                        _context.ReceiptDetails.Update(details);
                        await _context.SaveChangesAsync();

                        //PostingCashBankLedger
                        LedgerPosting cashPosting = new LedgerPosting();
                        cashPosting.Date = model.Date;
                        cashPosting.NepaliDate = String.Empty;
                        cashPosting.LedgerId = item.LedgerId;
                        cashPosting.Debit = item.TotalAmount;
                        cashPosting.Credit = 0;
                        cashPosting.VoucherNo = model.VoucherNo;
                        cashPosting.DetailsId = model.ReceiptMasterId;
                        cashPosting.YearId = model.FinancialYearId;
                        cashPosting.InvoiceNo = model.VoucherNo;
                        cashPosting.VoucherTypeId = model.VoucherTypeId;
                        cashPosting.CompanyId = model.CompanyId;
                        cashPosting.LongReference = model.Narration;
                        cashPosting.ReferenceN = model.Narration;
                        cashPosting.ChequeNo = String.Empty;
                        cashPosting.ChequeDate = String.Empty;
                        cashPosting.AddedDate = DateTime.Now;
                        _context.LedgerPosting.Add(cashPosting);
                        await _context.SaveChangesAsync();
                    }
                }

                //LedgerPosting
                //Customer
                LedgerPosting ledgerPosting = new LedgerPosting();
                ledgerPosting.Date = model.Date;
                ledgerPosting.NepaliDate = String.Empty;
                ledgerPosting.LedgerId = model.LedgerId;
                ledgerPosting.Debit = 0;
                ledgerPosting.Credit = model.Amount;
                ledgerPosting.VoucherNo = model.VoucherNo;
                ledgerPosting.DetailsId = model.ReceiptMasterId;
                ledgerPosting.YearId = model.FinancialYearId;
                ledgerPosting.InvoiceNo = model.VoucherNo;
                ledgerPosting.VoucherTypeId = model.VoucherTypeId;
                ledgerPosting.CompanyId = model.CompanyId;
                ledgerPosting.LongReference = model.Narration;
                ledgerPosting.ReferenceN = model.Narration;
                ledgerPosting.ChequeNo = String.Empty;
                ledgerPosting.ChequeDate = String.Empty;
                ledgerPosting.AddedDate = DateTime.Now;
                _context.LedgerPosting.Add(ledgerPosting);
                await _context.SaveChangesAsync();

                foreach (var deleteitem in model.listDelete)
                {
                    ReceiptDetails x = _context.ReceiptDetails.Find(deleteitem.ReceiptDetailsId);
                    _context.ReceiptDetails.Remove(x);
                    await _context.SaveChangesAsync();
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
