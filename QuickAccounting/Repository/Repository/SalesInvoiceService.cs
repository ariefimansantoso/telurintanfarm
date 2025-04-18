﻿using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using QuickAccounting.Data;
using QuickAccounting.Data.Inventory;
using QuickAccounting.Data.Recording;
using QuickAccounting.Data.Setting;
using QuickAccounting.Repository.Interface;
using QuickAccounting.Services;
using System.Data;

namespace QuickAccounting.Repository.Repository
{
    public class SalesInvoiceService : ISalesInvoice
    {
        private readonly ApplicationDbContext _context;
        private readonly DatabaseConnection _conn;
        public SalesInvoiceService(ApplicationDbContext context, DatabaseConnection conn)
        {
            _context = context;
            _conn = conn;
        }
        public async Task<bool> CheckName(string name)
        {
            var checkResult = (from progm in _context.SalesMaster
                               where progm.VoucherNo == name
                               select progm.SalesMasterId).Count();
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
            var checkResult = (from progm in _context.SalesMaster
                               where progm.VoucherNo == name
                               select progm.SalesMasterId).Count();
            if (checkResult > 0)
            {

                var checkAccount = (from progm in _context.SalesMaster
                                    where progm.VoucherNo == name
                                    select progm.SalesMasterId).FirstOrDefault();
                return checkAccount;
            }
            else
            {
                return 0;
            }
        }

        public async Task<bool> Delete(SalesMaster master)
        {
            SqlConnection sqlcon = new SqlConnection(_conn.DbConn);
            try
            {

                if (sqlcon.State == ConnectionState.Closed)
                {
                    sqlcon.Open();
                }
                SqlCommand cmd = new SqlCommand("SalesInvoiceDelete", sqlcon);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter para = new SqlParameter();
                para = cmd.Parameters.Add("@SalesMasterId", SqlDbType.Int);
                para.Value = master.SalesMasterId;
                para = cmd.Parameters.Add("@VoucherTypeId", SqlDbType.Int);
                para.Value = master.VoucherTypeId;
                para = cmd.Parameters.Add("@VoucherNo", SqlDbType.NVarChar);
                para.Value = master.VoucherNo;
                int rowAffacted = cmd.ExecuteNonQuery();
                if (rowAffacted > 0)
                {

                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                return false;
            }
            finally
            {
                sqlcon.Close();
            }
        }
        public async Task<List<SalesMasterView>> GetAll()
        {
            var result = await (from a in _context.SalesMaster
                                join b in _context.InvoiceSetting on a.VoucherTypeId equals b.VoucherTypeId
                                join c in _context.AccountLedger on a.LedgerId equals c.LedgerId
                                select new SalesMasterView
                                {
                                    SalesMasterId = a.SalesMasterId,
                                    VoucherNo = a.VoucherNo,
                                    Date = a.Date,
                                    Reference = a.Reference,
                                    GrandTotal = a.GrandTotal,
                                    BillDiscount = a.BillDiscount,
                                    TotalTax = a.TotalTax,
                                    Narration = a.Narration,
                                    Status = a.Status,
                                    PaymentStatus = a.PaymentStatus,
                                    VoucherTypeName = b.VoucherTypeName,
                                    LedgerName = c.LedgerName,
									Bayar = a.Bayar,
									Kembali = a.Kembali,
									KP10 = a.KP10,
									KP15 = a.KP15,
                                    TipePembayaran = a.TipePembayaran
                                }).ToListAsync();
            return result;
        }
        public async Task<List<SalesMasterView>> SalesInvoiceSearch(DateTime FromDate, DateTime ToDate, string VoucherNo, int LedgerId)
        {
            using (SqlConnection sqlcon = new SqlConnection(_conn.DbConn))
            {
                var para = new DynamicParameters();
				FromDate = new DateTime(FromDate.Year, FromDate.Month, FromDate.Day, 0, 0, 0);
                ToDate = new DateTime(ToDate.Year, ToDate.Month, ToDate.Day, ToDate.Hour, ToDate.Minute, ToDate.Second);

				para.Add("@FromDate", FromDate);
                para.Add("@ToDate", ToDate);
                para.Add("@VoucherNo", VoucherNo);
                para.Add("@LedgerId", LedgerId);
                var ListofPlan = sqlcon.Query<SalesMasterView>("SalesInvoiceSearch", para, null, true, 0, commandType: CommandType.StoredProcedure).ToList();
                return ListofPlan;
            }
        }

        public async Task<SalesMaster> GetbyId(int id)
        {
            SalesMaster model = await _context.SalesMaster.FindAsync(id);
            return model;
        }

        public async Task<int> Save(SalesMaster model)
        {
            _context.SalesMaster.Add(model);
            await _context.SaveChangesAsync();
            int id = model.SalesMasterId;



            //SalesDetails table
            foreach (var item in model.listOrder)
            {
                //AddSalesDetails
                SalesDetails details = new SalesDetails();
                details.SalesMasterId = id;
                details.ProductId = item.ProductId;
                details.Qty = item.Qty;
                details.UnitId = item.UnitId;
                details.BatchId = item.BatchId;
                details.Rate = item.Rate;
                details.Amount = item.Amount;
                details.NetAmount = item.NetAmount;
                details.GrossAmount = item.GrossAmount;
                details.Discount = item.Discount;
                details.DiscountAmount = item.DiscountAmount;
                details.TaxAmount = item.TaxAmount;
                details.TaxRate = item.TaxRate;
                details.TaxId = item.TaxId;
                details.OrderDetailsId = item.OrderDetailsId;
                details.QuotationDetailsId = item.QuotationDetailsId;
                _context.SalesDetails.Add(details);
                await _context.SaveChangesAsync();
                int intPurchaseDId = details.SalesDetailsId;
                //AddStockPosting

                
            }
            return id;
        }

        public async Task<bool> Update(SalesMaster model)
        {
            try
            {
                _context.SalesMaster.Update(model);
                await _context.SaveChangesAsync();



                //SalesDetails table
                foreach (var item in model.listOrder)
                {
                    //AddSalesDetails
                    if (item.SalesDetailsId == 0)
                    {
                        SalesDetails details = new SalesDetails();
                        details.SalesMasterId = model.SalesMasterId;
                        details.ProductId = item.ProductId;
                        details.Qty = item.Qty;
                        details.UnitId = item.UnitId;
                        details.BatchId = item.BatchId;
                        details.Rate = item.Rate;
                        details.Amount = item.Amount;
                        details.NetAmount = item.NetAmount;
                        details.GrossAmount = item.GrossAmount;
                        details.Discount = item.Discount;
                        details.DiscountAmount = item.DiscountAmount;
                        details.TaxAmount = item.TaxAmount;
                        details.TaxRate = item.TaxRate;
                        details.TaxId = item.TaxId;
                        details.OrderDetailsId = item.OrderDetailsId;
                        details.QuotationDetailsId = item.QuotationDetailsId;
                        _context.SalesDetails.Add(details);
                        await _context.SaveChangesAsync();
                        int intPurchaseDId = details.SalesDetailsId;
                    }
                    else
                    {
                        SalesDetails details = new SalesDetails();
                        details.SalesDetailsId = item.SalesDetailsId;
                        details.SalesMasterId = model.SalesMasterId;
                        details.ProductId = item.ProductId;
                        details.Qty = item.Qty;
                        details.UnitId = item.UnitId;
                        details.BatchId = item.BatchId;
                        details.Rate = item.Rate;
                        details.Amount = item.Amount;
                        details.NetAmount = item.NetAmount;
                        details.GrossAmount = item.GrossAmount;
                        details.Discount = item.Discount;
                        details.DiscountAmount = item.DiscountAmount;
                        details.TaxAmount = item.TaxAmount;
                        details.TaxRate = item.TaxRate;
                        details.TaxId = item.TaxId;
                        details.OrderDetailsId = item.OrderDetailsId;
                        details.QuotationDetailsId = item.QuotationDetailsId;
                        _context.SalesDetails.Update(details);
                        await _context.SaveChangesAsync();
                    }
                }
                return true;
            }
            catch (Exception)
            {
                //dbTran.Rollback();
                return false;
            }
        }
        public async Task<bool> Approved(SalesMaster model)
        {
            try
            {
                //GetBalance
                var saleMaster = (from progm in _context.SalesMaster
                                  where progm.SalesMasterId == model.SalesMasterId
                                  select progm.PayAmount).FirstOrDefault();
                if (saleMaster > 0)
                {
                    model.PayAmount = saleMaster;
                    model.PreviousDue = model.GrandTotal - model.PayAmount;
                    model.BalanceDue = model.GrandTotal - model.PayAmount;
                    if (model.GrandTotal == model.PayAmount)
                    {
                        model.Status = "Paid";
                    }
                }
                model.PaymentStatus = "Approved";
                _context.SalesMaster.Update(model);
                await _context.SaveChangesAsync();



                //SalesDetails table
                foreach (var item in model.listOrder)
                {
                    //AddSalesDetails
                    //AddStockPosting
                    
                    // kalau ini adalah telur utuh, tambahin dulu StockPostingnya dari stock gudang
                    AddInwardQtyToTelurUtuh(item.ProductId, item.Qty, model.UserId);

						StockPosting stockposting = new StockPosting();
                        stockposting.Date = model.Date;
                        stockposting.ProductId = item.ProductId;
                    stockposting.InwardQty = 0;
                        stockposting.OutwardQty = item.Qty;
					stockposting.UnitId = item.UnitId;
                        stockposting.BatchId = item.BatchId;
                        stockposting.Rate = item.Rate;
                        stockposting.DetailsId = item.SalesDetailsId;
                        stockposting.InvoiceNo = model.VoucherNo;
                        stockposting.VoucherNo = model.VoucherNo;
                        stockposting.VoucherTypeId = model.VoucherTypeId;
                        stockposting.AgainstInvoiceNo = String.Empty;
                        stockposting.AgainstVoucherNo = String.Empty;
                        stockposting.AgainstVoucherTypeId = 0;
                        stockposting.WarehouseId = model.WarehouseId;
                        stockposting.StockCalculate = "Sales";
                        stockposting.CompanyId = model.CompanyId;
                        stockposting.FinancialYearId = model.FinancialYearId;
                        stockposting.AddedDate = DateTime.Now;
                        _context.StockPosting.Add(stockposting);
                        await _context.SaveChangesAsync();
                }


                    //LedgerPosting
                    //Customer
                    LedgerPosting ledgerPosting = new LedgerPosting();
                    ledgerPosting.Date = model.Date;
                    ledgerPosting.NepaliDate = String.Empty;
                    ledgerPosting.LedgerId = model.LedgerId;
                    ledgerPosting.Debit = model.GrandTotal;
                ledgerPosting.Credit = 0;
                    ledgerPosting.VoucherNo = model.VoucherNo;
                    ledgerPosting.DetailsId = model.SalesMasterId;
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

                    //SalesAccount Ledger send with out vat
                    LedgerPosting purchasePosting = new LedgerPosting();
                    decimal decSupplierCustomerAmount = Math.Round(model.GrandTotal - model.TotalTax, 2);
                    purchasePosting.Date = model.Date;
                    purchasePosting.NepaliDate = String.Empty;
                    purchasePosting.LedgerId = 2;
                    purchasePosting.Debit = 0;
                    purchasePosting.Credit = decSupplierCustomerAmount;
                    purchasePosting.VoucherNo = model.VoucherNo;
                    purchasePosting.DetailsId = model.SalesMasterId;
                    purchasePosting.YearId = model.FinancialYearId;
                    purchasePosting.InvoiceNo = model.VoucherNo;
                    purchasePosting.VoucherTypeId = model.VoucherTypeId;
                    purchasePosting.CompanyId = model.CompanyId;
                    purchasePosting.LongReference = model.Narration;
                    purchasePosting.ReferenceN = model.Narration;
                    purchasePosting.ChequeNo = String.Empty;
                    purchasePosting.ChequeDate = String.Empty;
                    purchasePosting.AddedDate = DateTime.Now;
                    _context.LedgerPosting.Add(purchasePosting);
                    await _context.SaveChangesAsync();

                    //Tax
                    if (model.TotalTax > 0)
                    {
                        LedgerPosting vatPosting = new LedgerPosting();
                        vatPosting.Date = model.Date;
                        vatPosting.NepaliDate = String.Empty;
                        vatPosting.LedgerId = 4;
                    vatPosting.Debit = 0;
                        vatPosting.Credit = model.TotalTax;
                    vatPosting.VoucherNo = model.VoucherNo;
                        vatPosting.DetailsId = model.SalesMasterId;
                        vatPosting.YearId = model.FinancialYearId;
                        vatPosting.InvoiceNo = model.VoucherNo;
                        vatPosting.VoucherTypeId = model.VoucherTypeId;
                        vatPosting.CompanyId = model.CompanyId;
                        vatPosting.LongReference = model.Narration;
                        vatPosting.ReferenceN = model.Narration;
                        vatPosting.ChequeNo = String.Empty;
                        vatPosting.ChequeDate = String.Empty;
                        vatPosting.AddedDate = DateTime.Now;
                        _context.LedgerPosting.Add(vatPosting);
                        await _context.SaveChangesAsync();
                    }
                return true;
                //dbTran.Commit();
            }
            catch (Exception)
            {
                //dbTran.Rollback();
                return false;
            }
        }
        public async Task<bool> UpdateSalesInvoice(SalesMaster model)
        {
            try
            {
                //GetBalance
                var saleMaster = (from progm in _context.SalesMaster
                                  where progm.SalesMasterId == model.SalesMasterId
                                  select progm.PayAmount).FirstOrDefault();
                if (saleMaster > 0)
                {
                    model.PayAmount = saleMaster;
                    model.PreviousDue = model.GrandTotal - model.PayAmount;
                    model.BalanceDue = model.GrandTotal - model.PayAmount;
                    if (model.GrandTotal == model.PayAmount)
                    {
                        model.Status = "Paid";
                    }
                }
                model.PaymentStatus = "Approved";
                _context.SalesMaster.Update(model);
                await _context.SaveChangesAsync();



                //SalesDetails table
                foreach (var item in model.listOrder)
                {
                    ///AddSalesDetails
                    if (item.SalesDetailsId == 0)
                    {
                        SalesDetails details = new SalesDetails();
                        details.SalesMasterId = model.SalesMasterId;
                        details.ProductId = item.ProductId;
                        details.Qty = item.Qty;
                        details.UnitId = item.UnitId;
                        details.BatchId = item.BatchId;
                        details.Rate = item.Rate;
                        details.Amount = item.Amount;
                        details.NetAmount = item.NetAmount;
                        details.GrossAmount = item.GrossAmount;
                        details.Discount = item.Discount;
                        details.DiscountAmount = item.DiscountAmount;
                        details.TaxAmount = item.TaxAmount;
                        details.TaxRate = item.TaxRate;
                        details.TaxId = item.TaxId;
                        details.OrderDetailsId = item.OrderDetailsId;
                        details.QuotationDetailsId = item.QuotationDetailsId;
                        _context.SalesDetails.Add(details);
                        await _context.SaveChangesAsync();
                        int intPurchaseDId = details.SalesDetailsId;

                        var productItem = await _context.Product.FirstOrDefaultAsync(x => x.ProductId == item.ProductId);
                        var userItem = await _context.UserMaster.FirstOrDefaultAsync(x => x.UserName == model.UserId);
                        var employeeItem = await _context.Employee.FirstOrDefaultAsync(x => x.UserID == userItem.UserId);
                        if (productItem.ProductCode.StartsWith("TA-"))
                        {
                            StockTelurUtuh stockTelurUtuh = _context.StockTelurUtuh.FirstOrDefault(x => x.ID == 1);
                            // kurangi stock telur utuh dari gudang
                            stockTelurUtuh.StockKG = stockTelurUtuh.StockKG - item.Qty;
                            stockTelurUtuh.ModifiedBy = employeeItem.EmployeeId;
                            stockTelurUtuh.ModifiedOn = DateTime.Now;
                                                        
                            MutasiStockTelurHarian mutasi = new MutasiStockTelurHarian();
                            mutasi.JenisTelur = "UTUH";
                            mutasi.BeratTelurIn = 0;
                            mutasi.BeratTelurOut = item.Qty;
                            mutasi.DateCreated = DateTime.Now;
                            mutasi.CreatedBy = employeeItem.EmployeeId;
                            _context.MutasiStockTelurHarian.Add(mutasi);
                        }
                        
                        StockPosting stockposting = new StockPosting();
                        stockposting.Date = model.Date;
                        stockposting.ProductId = item.ProductId;
                        stockposting.InwardQty = 0;
                        stockposting.OutwardQty = item.Qty;
						stockposting.UnitId = item.UnitId;
                        stockposting.BatchId = item.BatchId;
                        stockposting.Rate = item.Rate;
                        stockposting.DetailsId = intPurchaseDId;
                        stockposting.InvoiceNo = model.VoucherNo;
                        stockposting.VoucherNo = model.VoucherNo;
                        stockposting.VoucherTypeId = model.VoucherTypeId;
                        stockposting.AgainstInvoiceNo = String.Empty;
                        stockposting.AgainstVoucherNo = String.Empty;
                        stockposting.AgainstVoucherTypeId = 0;
                        stockposting.WarehouseId = model.WarehouseId;
                        stockposting.StockCalculate = "Sales";
                        stockposting.CompanyId = model.CompanyId;
                        stockposting.FinancialYearId = model.FinancialYearId;
                        stockposting.AddedDate = DateTime.Now;
                        _context.StockPosting.Add(stockposting);
                        await _context.SaveChangesAsync();
                    }
                    else
                    {
                        SalesDetails details = new SalesDetails();
                        details.SalesDetailsId = item.SalesDetailsId;
                        details.SalesMasterId = model.SalesMasterId;
                        details.ProductId = item.ProductId;
                        details.Qty = item.Qty;
                        details.UnitId = item.UnitId;
                        details.BatchId = item.BatchId;
                        details.Rate = item.Rate;
                        details.Amount = item.Amount;
                        details.NetAmount = item.NetAmount;
                        details.GrossAmount = item.GrossAmount;
                        details.Discount = item.Discount;
                        details.DiscountAmount = item.DiscountAmount;
                        details.TaxAmount = item.TaxAmount;
                        details.TaxRate = item.TaxRate;
                        details.TaxId = item.TaxId;
                        details.OrderDetailsId = item.OrderDetailsId;
                        details.QuotationDetailsId = item.QuotationDetailsId;
                        _context.SalesDetails.Update(details);
                        await _context.SaveChangesAsync();
                        //UpdateStockPosting

                        var productItem = await _context.Product.FirstOrDefaultAsync(x => x.ProductId == item.ProductId);
                        var userItem = await _context.UserMaster.FirstOrDefaultAsync(x => x.UserName == model.UserId);
                        var employeeItem = await _context.Employee.FirstOrDefaultAsync(x => x.UserID == userItem.UserId);                        

                        //GetStockkPostingId
                        var returnstockpostingG = (from progm in _context.StockPosting
                                                   where progm.VoucherTypeId == model.VoucherTypeId && progm.VoucherNo == model.VoucherNo && progm.DetailsId == item.SalesDetailsId
                                                   select progm.StockPostingId).FirstOrDefault();

                        StockPosting stockposting = new StockPosting();
                        stockposting.StockPostingId = returnstockpostingG;
                        stockposting.Date = model.Date;
                        stockposting.ProductId = item.ProductId;
                        stockposting.InwardQty = 0;
						if (productItem.ProductCode.StartsWith("TA-"))
						{
							StockTelurUtuh stockTelurUtuh = _context.StockTelurUtuh.FirstOrDefault(x => x.ID == 1);
							// kurangi stock telur utuh dari gudang
							stockTelurUtuh.StockKG = stockTelurUtuh.StockKG + (stockposting.OutwardQty - item.Qty);
							stockTelurUtuh.ModifiedBy = employeeItem.EmployeeId;
							stockTelurUtuh.ModifiedOn = DateTime.Now;

                            string auditMessage = "Update stock UTUH dengan jumlah: " + item.Qty;
                            var log = new AuditLog
                            {
                                EmployeeName = employeeItem.EmployeeName,
                                CreatedDate = DateTime.Now,
                                ActionType = "SalesInvoiceService",
                                ActionDescription = auditMessage,
                                EmployeeID = employeeItem.EmployeeId,
                                LogType = LogTypes.LogInfo
                            };

                            _context.AuditLog.Add(log);

                            MutasiStockTelurHarian mutasi = new MutasiStockTelurHarian();
                            mutasi.JenisTelur = "UTUH";
                            mutasi.BeratTelurIn = (stockposting.OutwardQty - item.Qty);
                            mutasi.BeratTelurOut = 0;
                            mutasi.DateCreated = DateTime.Now;
                            mutasi.CreatedBy = employeeItem.EmployeeId;
                            mutasi.Reason = "UPDATE";
                            _context.MutasiStockTelurHarian.Add(mutasi);
                        }
						stockposting.OutwardQty = item.Qty;
						stockposting.UnitId = item.UnitId;
                        stockposting.BatchId = item.BatchId;
                        stockposting.Rate = item.Rate;
                        stockposting.DetailsId = item.SalesDetailsId;
                        stockposting.InvoiceNo = model.VoucherNo;
                        stockposting.VoucherNo = model.VoucherNo;
                        stockposting.VoucherTypeId = model.VoucherTypeId;
                        stockposting.AgainstInvoiceNo = String.Empty;
                        stockposting.AgainstVoucherNo = String.Empty;
                        stockposting.AgainstVoucherTypeId = 0;
                        stockposting.WarehouseId = model.WarehouseId;
                        stockposting.StockCalculate = "Sales";
                        stockposting.CompanyId = model.CompanyId;
                        stockposting.FinancialYearId = model.FinancialYearId;
                        stockposting.ModifyDate = DateTime.Now;
                        _context.StockPosting.Update(stockposting);
                        await _context.SaveChangesAsync();
                    }
                    
                }

                //DeletePurchaseInvoiceLedger
                using (SqlConnection sqlcon = new SqlConnection(_conn.DbConn))
                {
                    var paraScDelete = new DynamicParameters();
                    paraScDelete.Add("@DetailsId", model.SalesMasterId);
                    paraScDelete.Add("@VoucherTypeId", model.VoucherTypeId);
                    var valueScDelete = sqlcon.Query<int>("DELETE FROM LedgerPosting where DetailsId=@DetailsId AND VoucherTypeId=@VoucherTypeId", paraScDelete, null, true, 0, commandType: CommandType.Text);
                }
                //LedgerPosting
                //Customer
                LedgerPosting ledgerPosting = new LedgerPosting();
                ledgerPosting.Date = model.Date;
                ledgerPosting.NepaliDate = String.Empty;
                ledgerPosting.LedgerId = model.LedgerId;
                ledgerPosting.Debit = model.GrandTotal;
                ledgerPosting.Credit = 0;
                ledgerPosting.VoucherNo = model.VoucherNo;
                ledgerPosting.DetailsId = model.SalesMasterId;
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

                //SalesAccount Ledger send with out vat
                LedgerPosting purchasePosting = new LedgerPosting();
                decimal decSupplierCustomerAmount = Math.Round(model.GrandTotal - model.TotalTax, 2);
                purchasePosting.Date = model.Date;
                purchasePosting.NepaliDate = String.Empty;
                purchasePosting.LedgerId = 2;
                purchasePosting.Debit = 0;
                purchasePosting.Credit = decSupplierCustomerAmount;
                purchasePosting.VoucherNo = model.VoucherNo;
                purchasePosting.DetailsId = model.SalesMasterId;
                purchasePosting.YearId = model.FinancialYearId;
                purchasePosting.InvoiceNo = model.VoucherNo;
                purchasePosting.VoucherTypeId = model.VoucherTypeId;
                purchasePosting.CompanyId = model.CompanyId;
                purchasePosting.LongReference = model.Narration;
                purchasePosting.ReferenceN = model.Narration;
                purchasePosting.ChequeNo = String.Empty;
                purchasePosting.ChequeDate = String.Empty;
                purchasePosting.AddedDate = DateTime.Now;
                _context.LedgerPosting.Add(purchasePosting);
                await _context.SaveChangesAsync();

                //Tax
                if (model.TotalTax > 0)
                {
                    LedgerPosting vatPosting = new LedgerPosting();
                    vatPosting.Date = model.Date;
                    vatPosting.NepaliDate = String.Empty;
                    vatPosting.LedgerId = 4;
                    vatPosting.Debit = 0;
                    vatPosting.Credit = model.TotalTax;
                    vatPosting.VoucherNo = model.VoucherNo;
                    vatPosting.DetailsId = model.SalesMasterId;
                    vatPosting.YearId = model.FinancialYearId;
                    vatPosting.InvoiceNo = model.VoucherNo;
                    vatPosting.VoucherTypeId = model.VoucherTypeId;
                    vatPosting.CompanyId = model.CompanyId;
                    vatPosting.LongReference = model.Narration;
                    vatPosting.ReferenceN = model.Narration;
                    vatPosting.ChequeNo = String.Empty;
                    vatPosting.ChequeDate = String.Empty;
                    vatPosting.AddedDate = DateTime.Now;
                    _context.LedgerPosting.Add(vatPosting);
                    await _context.SaveChangesAsync();
                }
                foreach (var deleteitem in model.listDelete)
                {
                    SalesDetails x = _context.SalesDetails.Find(deleteitem.SalesDetailsId);
                    _context.SalesDetails.Remove(x);
                    await _context.SaveChangesAsync();
                }
                foreach (var deleteitem in model.listDelete)
                {
                    var returnStockposting = (from ledgerpostingss in _context.StockPosting
                                              where ledgerpostingss.DetailsId == deleteitem.SalesDetailsId && ledgerpostingss.VoucherNo == model.VoucherNo && ledgerpostingss.VoucherTypeId == model.VoucherTypeId
                                              select ledgerpostingss.StockPostingId).FirstOrDefault();
                    StockPosting returnView = _context.StockPosting.Find(returnStockposting);
                    _context.StockPosting.Remove(returnView);
                    _context.SaveChanges();
                }
                return true;
                //dbTran.Commit();
            }
            catch (Exception)
            {
                //dbTran.Rollback();
                return false;
            }
        }
        public async Task<SalesMasterView> SalesInvoiceMasterView(int id)
        {
            var result = await (from a in _context.SalesMaster
                                join b in _context.InvoiceSetting on a.VoucherTypeId equals b.VoucherTypeId
                                join c in _context.AccountLedger on a.LedgerId equals c.LedgerId
                                where a.SalesMasterId == id
                                select new SalesMasterView
                                {
                                    SalesMasterId = a.SalesMasterId,
                                    VoucherNo = a.VoucherNo,
                                    Date = a.Date,
                                    Reference = a.Reference,
                                    GrandTotal = a.GrandTotal,
                                    BillDiscount = a.BillDiscount,
                                    TotalTax = a.TotalTax,
                                    TotalAmount = a.TotalAmount,
                                    NetAmounts = a.NetAmounts,
                                    Narration = a.Narration,
                                    Status = a.Status,
                                    PaymentStatus = a.PaymentStatus,
                                    VoucherTypeName = b.VoucherTypeName,
                                    LedgerName = c.LedgerName,
                                    Email = c.Email,
                                    Phone = c.Phone,
                                    Address = c.Address,
                                    UserId = a.UserId,
                                    AddedDate = a.AddedDate,
                                    Bayar = a.Bayar,
                                    Kembali = a.Kembali,
                                    KP10 = a.KP10,
                                    KP15 = a.KP15,
                                    TipePembayaran = a.TipePembayaran
                                }).FirstOrDefaultAsync();
            return result;
        }

        public async Task<List<ProductView>> SalesInvoiceDetailsView(int id)
        {
            var varlist = (from a in _context.SalesDetails
                           join b in _context.Product on a.ProductId equals b.ProductId
                           join c in _context.Unit on a.UnitId equals c.UnitId
                           where a.SalesMasterId == id
                           select new ProductView
                           {
                               SalesDetailsId = a.SalesDetailsId,
                               ProductId = a.ProductId,
                               Qty = a.Qty,
                               UnitId = a.UnitId,
                               TaxId = a.TaxId,
                               TaxAmount = a.TaxAmount,
                               SalesRate = a.Rate,
                               PurchaseRate = a.Rate,
                               Amount = a.Amount,
                               TotalAmount = a.NetAmount,
                               Discount = a.Discount,
                               DiscountAmount = a.DiscountAmount,
                               NetAmount = a.NetAmount,
                               ProductName = b.ProductName,
                               ProductCode = b.ProductCode,
                               UnitName = c.UnitName
                           }).ToList();

            return varlist;
        }

        public async Task<string> GetSerialNo()
        {
            using (SqlConnection sqlcon = new SqlConnection(_conn.DbConn))
            {
                var val = sqlcon.Query<string>("SELECT ISNULL(MAX(SerialNo+1),1) as VoucherNo FROM SalesMaster", null, null, true, 0, commandType: CommandType.Text).FirstOrDefault();
                return val;
            }
        }

        public async Task<List<SalesMasterView>> PaymentInAllocations(int SalesMasterId)
        {
            using (SqlConnection sqlcon = new SqlConnection(_conn.DbConn))
            {
                var para = new DynamicParameters();
                para.Add("@SalesMasterId", SalesMasterId);
                var ListofPlan = sqlcon.Query<SalesMasterView>("PaymentInAllocations", para, null, true, 0, commandType: CommandType.StoredProcedure).ToList();
                return ListofPlan;
            }
        } 
        
        public decimal GetPenjualanTelurUtuh(DateTime dateFrom, DateTime to)
        {
            return (from s in _context.SalesMaster
                    join sd in _context.SalesDetails on s.SalesMasterId equals sd.SalesMasterId
                    join p in _context.Product on sd.ProductId equals p.ProductId
                    where s.Date >= dateFrom && s.Date <= to && p.ProductCode.StartsWith("TA-")
                    select sd.Qty).Sum();
        }

		public decimal GetPenjualanTelurUtuhEcer(DateTime dateFrom, DateTime to)
		{
			return (from s in _context.SalesMaster
					join sd in _context.SalesDetails on s.SalesMasterId equals sd.SalesMasterId
					join p in _context.Product on sd.ProductId equals p.ProductId
					where s.Date >= dateFrom && s.Date <= to && p.ProductCode.StartsWith("TA-") && s.LedgerId == 19
					select sd.Qty).Sum();
		}

		public decimal GetPenjualanTelurUtuhPartai(DateTime dateFrom, DateTime to)
		{
			return (from s in _context.SalesMaster
					join sd in _context.SalesDetails on s.SalesMasterId equals sd.SalesMasterId
					join p in _context.Product on sd.ProductId equals p.ProductId
					where s.Date >= dateFrom && s.Date <= to && p.ProductCode.StartsWith("TA-") && s.LedgerId > 19
					select sd.Qty).Sum();
		}

		public decimal GetPenjualanTelurPutih(DateTime dateFrom, DateTime to)
		{
			return (from s in _context.SalesMaster
									join sd in _context.SalesDetails on s.SalesMasterId equals sd.SalesMasterId
									join p in _context.Product on sd.ProductId equals p.ProductId
									where s.Date >= dateFrom && s.Date <= to && p.ProductCode.StartsWith("TB")
									select sd.Qty).Sum();
		}

		public decimal GetPenjualanTelurBentes(DateTime dateFrom, DateTime to)
		{
			return (from s in _context.SalesMaster
									join sd in _context.SalesDetails on s.SalesMasterId equals sd.SalesMasterId
									join p in _context.Product on sd.ProductId equals p.ProductId
									where s.Date >= dateFrom && s.Date <= to && p.ProductCode.StartsWith("TC")
									select sd.Qty).Sum();
		}

		public decimal GetPenjualanTelurParah(DateTime dateFrom, DateTime to)
		{
			return (from s in _context.SalesMaster
									join sd in _context.SalesDetails on s.SalesMasterId equals sd.SalesMasterId
									join p in _context.Product on sd.ProductId equals p.ProductId
									where s.Date >= dateFrom && s.Date <= to && p.ProductCode.StartsWith("TD")
									select sd.Qty).Sum();
		}

		public decimal GetPenjualanTelurParahSekali(DateTime dateFrom, DateTime to)
		{
			return (from s in _context.SalesMaster
									join sd in _context.SalesDetails on s.SalesMasterId equals sd.SalesMasterId
									join p in _context.Product on sd.ProductId equals p.ProductId
									where s.Date >= dateFrom && s.Date <= to && p.ProductCode.StartsWith("TE")
									select sd.Qty).Sum();
		}

        public List<dynamic> GetPenjualanTelurUtuhGraph(DateTime dateFrom, DateTime to)
        {
            return (from s in _context.SalesMaster
                    join sd in _context.SalesDetails on s.SalesMasterId equals sd.SalesMasterId
                    join p in _context.Product on sd.ProductId equals p.ProductId
                    where s.Date >= dateFrom && s.Date <= to && p.ProductCode.StartsWith("TA")
                    select new
                    {
                        Date = s.Date,
                        Amount = s.NetAmounts
                    }).ToList<dynamic>();
        }

        private void AddInwardQtyToTelurUtuh(int productId, decimal stockToAdd, string userName)
        {
            var product = _context.Product.FirstOrDefault(p => p.ProductId == productId);
            if(product != null)
            {
                if(product.ProductCode.StartsWith("TA-"))
                {                    
                    var userItem = _context.UserMaster.FirstOrDefault(x => x.UserName == userName);
                    var employeeItem = _context.Employee.FirstOrDefault(x => x.UserID == userItem.UserId);
                    
                    StockTelurUtuh stockTelurUtuh = _context.StockTelurUtuh.FirstOrDefault(x => x.ID == 1);
                    // kurangi stock telur utuh dari gudang
                    stockTelurUtuh.StockKG = stockTelurUtuh.StockKG - stockToAdd;
                    stockTelurUtuh.ModifiedBy = employeeItem.EmployeeId;
                    stockTelurUtuh.ModifiedOn = DateTime.Now;

                    MutasiStockTelurHarian mutasi = new MutasiStockTelurHarian();
                    mutasi.JenisTelur = "UTUH";
                    mutasi.BeratTelurIn = 0;
                    mutasi.BeratTelurOut = stockToAdd;
                    mutasi.DateCreated = DateTime.Now;
                    mutasi.CreatedBy = employeeItem.EmployeeId;
                    mutasi.Reason = "SalesRestock";
                    _context.MutasiStockTelurHarian.Add(mutasi);

                    if (stockTelurUtuh != null)
                    {
						StockPosting stockposting = new StockPosting();
						stockposting.Date = DateTime.Now;
						stockposting.ProductId = productId;
						stockposting.InwardQty = stockToAdd;
						stockposting.OutwardQty = 0;
						stockposting.UnitId = 0;
						stockposting.BatchId = 0;
						stockposting.Rate = 0;
						stockposting.DetailsId = 0;
						stockposting.InvoiceNo = productId.ToString();
						stockposting.VoucherNo = productId.ToString();
						stockposting.VoucherTypeId = 0;
						stockposting.AgainstInvoiceNo = String.Empty;
						stockposting.AgainstVoucherNo = String.Empty;
						stockposting.AgainstVoucherTypeId = 0;
						stockposting.WarehouseId = 1;
						stockposting.StockCalculate = "SalesRestock";
						stockposting.CompanyId = 1;
						stockposting.FinancialYearId = 2;
						stockposting.AddedDate = DateTime.Now;
						_context.StockPosting.Add(stockposting);
					}

                    _context.SaveChanges();
				}
            }
        }
	}
}
