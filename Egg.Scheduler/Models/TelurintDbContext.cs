using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Egg.Scheduler.Models;

public partial class TelurintDbContext : DbContext
{
    public TelurintDbContext()
    {
    }

    public TelurintDbContext(DbContextOptions<TelurintDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AbsensiPotongan> AbsensiPotongans { get; set; }

    public virtual DbSet<AccountGroup> AccountGroups { get; set; }

    public virtual DbSet<AccountLedger> AccountLedgers { get; set; }

    public virtual DbSet<AdvancePayment> AdvancePayments { get; set; }

    public virtual DbSet<AuditLog> AuditLogs { get; set; }

    public virtual DbSet<BonusDeduction> BonusDeductions { get; set; }

    public virtual DbSet<Brand> Brands { get; set; }

    public virtual DbSet<BudgetDetail> BudgetDetails { get; set; }

    public virtual DbSet<BudgetMaster> BudgetMasters { get; set; }

    public virtual DbSet<ChecklistPakan> ChecklistPakans { get; set; }

    public virtual DbSet<Company> Companies { get; set; }

    public virtual DbSet<Currency> Currencies { get; set; }

    public virtual DbSet<DailyAttendanceDetail> DailyAttendanceDetails { get; set; }

    public virtual DbSet<DailyAttendanceMaster> DailyAttendanceMasters { get; set; }

    public virtual DbSet<DailyEggPickup> DailyEggPickups { get; set; }

    public virtual DbSet<DailyRecording> DailyRecordings { get; set; }

    public virtual DbSet<DailyRecordingVov> DailyRecordingVovs { get; set; }

    public virtual DbSet<DailySalaryVoucherDetail> DailySalaryVoucherDetails { get; set; }

    public virtual DbSet<DailySalaryVoucherMaster> DailySalaryVoucherMasters { get; set; }

    public virtual DbSet<Designation> Designations { get; set; }

    public virtual DbSet<EmailSetting> EmailSettings { get; set; }

    public virtual DbSet<Employee> Employees { get; set; }

    public virtual DbSet<EmployeeKandang> EmployeeKandangs { get; set; }

    public virtual DbSet<ExpenseMaster> ExpenseMasters { get; set; }

    public virtual DbSet<ExpensesDetail> ExpensesDetails { get; set; }

    public virtual DbSet<FinancialYear> FinancialYears { get; set; }

    public virtual DbSet<GeneralSetting> GeneralSettings { get; set; }

    public virtual DbSet<Holiday> Holidays { get; set; }

    public virtual DbSet<IncomeMaster> IncomeMasters { get; set; }

    public virtual DbSet<InvoiceSetting> InvoiceSettings { get; set; }

    public virtual DbSet<JenisAyam> JenisAyams { get; set; }

    public virtual DbSet<JenisPakan> JenisPakans { get; set; }

    public virtual DbSet<JournalDetail> JournalDetails { get; set; }

    public virtual DbSet<JournalMaster> JournalMasters { get; set; }

    public virtual DbSet<Kandang> Kandangs { get; set; }

    public virtual DbSet<LedgerPosting> LedgerPostings { get; set; }

    public virtual DbSet<LogPopulasiAnakKandang> LogPopulasiAnakKandangs { get; set; }

    public virtual DbSet<MonthlySalary> MonthlySalaries { get; set; }

    public virtual DbSet<MonthlySalaryDetail> MonthlySalaryDetails { get; set; }

    public virtual DbSet<MutasiStockTelurHarian> MutasiStockTelurHarians { get; set; }

    public virtual DbSet<PayHead> PayHeads { get; set; }

    public virtual DbSet<PaymentDetail> PaymentDetails { get; set; }

    public virtual DbSet<PaymentMaster> PaymentMasters { get; set; }

    public virtual DbSet<Payroll> Payrolls { get; set; }

    public virtual DbSet<PayrollCutoff> PayrollCutoffs { get; set; }

    public virtual DbSet<Penalty> Penalties { get; set; }

    public virtual DbSet<Pengumuman> Pengumumen { get; set; }

    public virtual DbSet<Perijinan> Perijinans { get; set; }

    public virtual DbSet<PriceHistory> PriceHistories { get; set; }

    public virtual DbSet<Privilege> Privileges { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<ProductGroup> ProductGroups { get; set; }

    public virtual DbSet<PurchaseDetail> PurchaseDetails { get; set; }

    public virtual DbSet<PurchaseMaster> PurchaseMasters { get; set; }

    public virtual DbSet<PurchaseReturnDetail> PurchaseReturnDetails { get; set; }

    public virtual DbSet<PurchaseReturnMaster> PurchaseReturnMasters { get; set; }

    public virtual DbSet<ReceiptDetail> ReceiptDetails { get; set; }

    public virtual DbSet<ReceiptMaster> ReceiptMasters { get; set; }

    public virtual DbSet<RecordNotum> RecordNota { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<SalaryHistory> SalaryHistories { get; set; }

    public virtual DbSet<SalaryPackage> SalaryPackages { get; set; }

    public virtual DbSet<SalaryPackageDetail> SalaryPackageDetails { get; set; }

    public virtual DbSet<SalaryVoucherDetail> SalaryVoucherDetails { get; set; }

    public virtual DbSet<SalaryVoucherMaster> SalaryVoucherMasters { get; set; }

    public virtual DbSet<SalesDetail> SalesDetails { get; set; }

    public virtual DbSet<SalesMaster> SalesMasters { get; set; }

    public virtual DbSet<SalesQuotationDetail> SalesQuotationDetails { get; set; }

    public virtual DbSet<SalesQuotationMaster> SalesQuotationMasters { get; set; }

    public virtual DbSet<SalesReturnDetail> SalesReturnDetails { get; set; }

    public virtual DbSet<SalesReturnMaster> SalesReturnMasters { get; set; }

    public virtual DbSet<StandardDatum> StandardData { get; set; }

    public virtual DbSet<StockGudang> StockGudangs { get; set; }

    public virtual DbSet<StockPenjualan> StockPenjualans { get; set; }

    public virtual DbSet<StockPosting> StockPostings { get; set; }

    public virtual DbSet<StockTelurUtuh> StockTelurUtuhs { get; set; }

    public virtual DbSet<Task> Tasks { get; set; }

    public virtual DbSet<TaskCategory> TaskCategories { get; set; }

    public virtual DbSet<TaskPriority> TaskPriorities { get; set; }

    public virtual DbSet<Tax> Taxes { get; set; }

    public virtual DbSet<Unit> Units { get; set; }

    public virtual DbSet<UnitConvertion> UnitConvertions { get; set; }

    public virtual DbSet<UserMaster> UserMasters { get; set; }

    public virtual DbSet<Warehouse> Warehouses { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    #warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
    {
        Console.WriteLine("OnConfiguring called");
        try
        {
            optionsBuilder.UseSqlServer("Data Source=balungfarm.javahome.co.id;Initial Catalog=telurint_db;Persist Security Info=True;User ID=sa;Password=hYu7372Svderd#$;Trust Server Certificate=True");
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error configuring DbContext: " + ex.Message);
        }        
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        Console.WriteLine("OnModelCreating called");
        modelBuilder.Entity<AbsensiPotongan>(entity =>
        {
            entity.HasKey(e => e.AbsensiPotonganId).HasName("PK__AbsensiP__BF31D3048D8C5FCD");

            entity.ToTable("AbsensiPotongan");

            entity.Property(e => e.AbsensiPotonganId).HasColumnName("ABSENSI_POTONGAN_ID");
            entity.Property(e => e.CutOffBulan).HasColumnName("CUT_OFF_BULAN");
            entity.Property(e => e.CutOffTahun).HasColumnName("CUT_OFF_TAHUN");
            entity.Property(e => e.KaryawanId).HasColumnName("KARYAWAN_ID");
            entity.Property(e => e.PotKe).HasColumnName("POT_KE");
            entity.Property(e => e.RpPotongan)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("RP_POTONGAN");
            entity.Property(e => e.StatusPotongan)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("STATUS_POTONGAN");
        });

        modelBuilder.Entity<AccountGroup>(entity =>
        {
            entity.HasKey(e => e.AccountGroupId).HasName("PK_AccountGroup_1");

            entity.ToTable("AccountGroup");

            entity.Property(e => e.AccountGroupId).ValueGeneratedNever();
        });

        modelBuilder.Entity<AccountLedger>(entity =>
        {
            entity.HasKey(e => e.LedgerId);

            entity.ToTable("AccountLedger");

            entity.Property(e => e.AccountName).HasMaxLength(50);
            entity.Property(e => e.AccountNo).HasMaxLength(50);
            entity.Property(e => e.CreditLimit).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.OpeningBalance).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.TaxNo).HasMaxLength(50);
        });

        modelBuilder.Entity<AdvancePayment>(entity =>
        {
            entity.ToTable("AdvancePayment");

            entity.Property(e => e.AddedDate).HasColumnType("datetime");
            entity.Property(e => e.Amount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.Date).HasColumnType("datetime");
            entity.Property(e => e.InvoiceNo).HasMaxLength(50);
            entity.Property(e => e.ModifyDate).HasColumnType("datetime");
            entity.Property(e => e.SalaryMonth).HasColumnType("datetime");
            entity.Property(e => e.SerialNo).HasMaxLength(50);
            entity.Property(e => e.VoucherNo).HasMaxLength(50);
            entity.Property(e => e.YearMonth).HasMaxLength(50);
        });

        modelBuilder.Entity<AuditLog>(entity =>
        {
            entity.ToTable("AuditLog");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.ActionType).HasMaxLength(50);
            entity.Property(e => e.EmployeeId).HasColumnName("EmployeeID");
            entity.Property(e => e.EmployeeName).HasMaxLength(50);
            entity.Property(e => e.LogType).HasMaxLength(50);
        });

        modelBuilder.Entity<BonusDeduction>(entity =>
        {
            entity.ToTable("BonusDeduction");

            entity.Property(e => e.AddedDate).HasColumnType("datetime");
            entity.Property(e => e.BonusAmount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.Date).HasColumnType("datetime");
            entity.Property(e => e.DeductionAmount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.ModifyDate).HasColumnType("datetime");
            entity.Property(e => e.Month).HasColumnType("datetime");
            entity.Property(e => e.YearMonth).HasMaxLength(50);
        });

        modelBuilder.Entity<Brand>(entity =>
        {
            entity.ToTable("Brand");
        });

        modelBuilder.Entity<BudgetDetail>(entity =>
        {
            entity.HasKey(e => e.BudgetDetailsId);

            entity.Property(e => e.Credit).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.Debit).HasColumnType("decimal(18, 2)");
        });

        modelBuilder.Entity<BudgetMaster>(entity =>
        {
            entity.ToTable("BudgetMaster");

            entity.Property(e => e.FromDate).HasColumnType("datetime");
            entity.Property(e => e.ToDate).HasColumnType("datetime");
            entity.Property(e => e.TotalCr).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.TotalDr).HasColumnType("decimal(18, 2)");
        });

        modelBuilder.Entity<ChecklistPakan>(entity =>
        {
            entity.ToTable("ChecklistPakan");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.JenisCentrat).HasMaxLength(50);
        });

        modelBuilder.Entity<Company>(entity =>
        {
            entity.ToTable("Company");
        });

        modelBuilder.Entity<Currency>(entity =>
        {
            entity.ToTable("Currency");

            entity.Property(e => e.NoOfDecimalPlaces).HasColumnType("decimal(18, 2)");
        });

        modelBuilder.Entity<DailyAttendanceDetail>(entity =>
        {
            entity.HasKey(e => e.DailyAttendanceDetailsId);

            entity.Property(e => e.Status).HasMaxLength(50);
        });

        modelBuilder.Entity<DailyAttendanceMaster>(entity =>
        {
            entity.ToTable("DailyAttendanceMaster");

            entity.Property(e => e.AttendanceStatus).HasMaxLength(50);
            entity.Property(e => e.Date).HasColumnType("datetime");
            entity.Property(e => e.EmployeeId).HasColumnName("EmployeeID");
            entity.Property(e => e.Lat)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Long)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.ModifyDate).HasColumnType("datetime");
        });

        modelBuilder.Entity<DailyEggPickup>(entity =>
        {
            entity.ToTable("DailyEggPickup");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.CageNumber).HasMaxLength(50);
            entity.Property(e => e.CreatedBy).HasDefaultValue(-99);
            entity.Property(e => e.StrainName).HasMaxLength(250);
            entity.Property(e => e.TelurBentesKg)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("TelurBentesKG");
            entity.Property(e => e.TelurBentesNettKg)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("TelurBentesNettKG");
            entity.Property(e => e.TelurPutihKg)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("TelurPutihKG");
            entity.Property(e => e.TelurPutihNettKg)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("TelurPutihNettKG");
            entity.Property(e => e.TelurUtuhKg)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("TelurUtuhKG");
            entity.Property(e => e.TelurUtuhNettKg)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("TelurUtuhNettKG");
            entity.Property(e => e.TotalNettKg)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("TotalNettKG");
            entity.Property(e => e.VerifiedBy).HasDefaultValue(-99);
            entity.Property(e => e.VerifiedOn).HasDefaultValueSql("(getdate())");
        });

        modelBuilder.Entity<DailyRecording>(entity =>
        {
            entity.HasKey(e => new { e.CageNumber, e.StrainName, e.RecordDate });

            entity.ToTable("DailyRecording");

            entity.Property(e => e.CageNumber).HasMaxLength(50);
            entity.Property(e => e.StrainName).HasMaxLength(250);
            entity.Property(e => e.ActualEggMassKg)
                .HasDefaultValue(0m)
                .HasColumnType("decimal(10, 2)");
            entity.Property(e => e.ActualEggWeightG)
                .HasDefaultValue(0m)
                .HasColumnType("decimal(10, 2)");
            entity.Property(e => e.ActualFoodNeededKg)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("ActualFoodNeededKG");
            entity.Property(e => e.ActualHenDay)
                .HasDefaultValue(0m)
                .HasColumnType("decimal(10, 2)");
            entity.Property(e => e.BrokenEggCount).HasDefaultValue(0);
            entity.Property(e => e.BrokenEggKg)
                .HasDefaultValue(0m)
                .HasColumnType("decimal(10, 2)");
            entity.Property(e => e.ConcentrateType)
                .HasMaxLength(50)
                .HasDefaultValue("PAKAN");
            entity.Property(e => e.DailyHenDayDifference)
                .HasDefaultValue(0m)
                .HasColumnType("decimal(10, 2)");
            entity.Property(e => e.DeadHenCount).HasDefaultValue(0);
            entity.Property(e => e.EggMassDeviation)
                .HasDefaultValue(0m)
                .HasColumnType("decimal(10, 2)");
            entity.Property(e => e.EggWeightDeviation)
                .HasDefaultValue(0m)
                .HasColumnType("decimal(10, 2)");
            entity.Property(e => e.FeedConversionRatio).HasColumnType("decimal(10, 4)");
            entity.Property(e => e.FoodIntakeDeviation).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.FoodIntakePerHen).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.FoodNeededTodayKg).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.GroupName)
                .HasMaxLength(250)
                .HasDefaultValue("");
            entity.Property(e => e.HenAgeDays).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.HenAgeWeeks).HasColumnType("decimal(5, 2)");
            entity.Property(e => e.ModifiedBy).HasDefaultValue(-99);
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            entity.Property(e => e.MoveInHenCount).HasDefaultValue(0);
            entity.Property(e => e.PerfectEggCount).HasDefaultValue(0);
            entity.Property(e => e.PerfectEggKg)
                .HasDefaultValue(0m)
                .HasColumnType("decimal(10, 2)");
            entity.Property(e => e.PeriodeEnd).HasDefaultValue(false);
            entity.Property(e => e.PeriodeStart).HasDefaultValue(false);
            entity.Property(e => e.RemainingFoodKg).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.SaldoFoodKg)
                .HasColumnType("decimal(18, 0)")
                .HasColumnName("SaldoFoodKG");
            entity.Property(e => e.SickHenCount).HasDefaultValue(0);
            entity.Property(e => e.StandardEggMassKg)
                .HasDefaultValue(0m)
                .HasColumnType("decimal(10, 2)");
            entity.Property(e => e.StandardEggWeightG)
                .HasDefaultValue(0m)
                .HasColumnType("decimal(10, 2)");
            entity.Property(e => e.StandardHenDay)
                .HasDefaultValue(0m)
                .HasColumnType("decimal(10, 2)");
            entity.Property(e => e.TelurPutihKg)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("TelurPutihKG");
            entity.Property(e => e.TotalEggCount).HasDefaultValue(0);
            entity.Property(e => e.TotalEggKg)
                .HasDefaultValue(0m)
                .HasColumnType("decimal(10, 2)");
            entity.Property(e => e.UnproductiveHenCount).HasDefaultValue(0);
        });

        modelBuilder.Entity<DailyRecordingVov>(entity =>
        {
            entity.ToTable("DailyRecordingVOV");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.CageNumber).HasMaxLength(50);
            entity.Property(e => e.HenAgeDays).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.HenAgeWeeks).HasColumnType("decimal(5, 2)");
            entity.Property(e => e.StrainName).HasMaxLength(250);
            entity.Property(e => e.Usage).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.VovproductId).HasColumnName("VOVProductID");
            entity.Property(e => e.VovproductName)
                .HasMaxLength(250)
                .HasColumnName("VOVProductName");
        });

        modelBuilder.Entity<DailySalaryVoucherDetail>(entity =>
        {
            entity.HasKey(e => e.DailySalaryVoucherDetailsId);

            entity.Property(e => e.Status).HasMaxLength(50);
            entity.Property(e => e.Wage).HasColumnType("decimal(18, 5)");
        });

        modelBuilder.Entity<DailySalaryVoucherMaster>(entity =>
        {
            entity.ToTable("DailySalaryVoucherMaster");

            entity.Property(e => e.AddedDate).HasColumnType("datetime");
            entity.Property(e => e.Date).HasColumnType("datetime");
            entity.Property(e => e.InvoiceNo).HasMaxLength(50);
            entity.Property(e => e.ModifyDate).HasColumnType("datetime");
            entity.Property(e => e.SalaryDate).HasColumnType("datetime");
            entity.Property(e => e.SerialNo).HasMaxLength(50);
            entity.Property(e => e.TotalAmount).HasColumnType("decimal(18, 5)");
            entity.Property(e => e.VoucherNo).HasMaxLength(50);
            entity.Property(e => e.YearMonthDay).HasMaxLength(50);
        });

        modelBuilder.Entity<Designation>(entity =>
        {
            entity.ToTable("Designation");

            entity.Property(e => e.AdvanceAmount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.DesignationName).HasMaxLength(50);
            entity.Property(e => e.LeaveDays).HasColumnType("decimal(18, 2)");
        });

        modelBuilder.Entity<EmailSetting>(entity =>
        {
            entity.ToTable("EmailSetting");

            entity.Property(e => e.EmailSettingId).ValueGeneratedNever();
            entity.Property(e => e.EncryptionName).HasMaxLength(50);
            entity.Property(e => e.MailFromname).HasMaxLength(50);
            entity.Property(e => e.MailHost).HasMaxLength(50);
            entity.Property(e => e.Password).HasMaxLength(50);
        });

        modelBuilder.Entity<Employee>(entity =>
        {
            entity.ToTable("Employee");

            entity.Property(e => e.Address)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.BankAccount).HasMaxLength(50);
            entity.Property(e => e.BirthPlace).HasMaxLength(250);
            entity.Property(e => e.BpjsKes)
                .HasColumnType("decimal(18, 0)")
                .HasColumnName("BPJS_KES");
            entity.Property(e => e.BpjsTk)
                .HasColumnType("decimal(18, 0)")
                .HasColumnName("BPJS_TK");
            entity.Property(e => e.DailyWage).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.Dob).HasColumnType("datetime");
            entity.Property(e => e.EmployeeCode).HasMaxLength(50);
            entity.Property(e => e.EmployeeName).HasMaxLength(50);
            entity.Property(e => e.Gender).HasMaxLength(50);
            entity.Property(e => e.IdBpjsKes)
                .HasMaxLength(50)
                .HasColumnName("ID_BPJS_KES");
            entity.Property(e => e.IdBpjsTk)
                .HasMaxLength(50)
                .HasColumnName("ID_BPJS_TK");
            entity.Property(e => e.IsActive).HasColumnName("isActive");
            entity.Property(e => e.JobDesc).HasDefaultValue("");
            entity.Property(e => e.JoiningDate).HasColumnType("datetime");
            entity.Property(e => e.Lembur).HasColumnType("decimal(18, 0)");
            entity.Property(e => e.MaritalStatus).HasMaxLength(50);
            entity.Property(e => e.Nik)
                .HasMaxLength(50)
                .HasColumnName("NIK");
            entity.Property(e => e.OldEmployeeId).HasDefaultValue(-99);
            entity.Property(e => e.Sop)
                .HasDefaultValue("")
                .HasColumnName("SOP");
            entity.Property(e => e.SupervisorId).HasColumnName("SupervisorID");
            entity.Property(e => e.Tunjangan).HasColumnType("decimal(18, 0)");
            entity.Property(e => e.UserId).HasColumnName("UserID");
        });

        modelBuilder.Entity<EmployeeKandang>(entity =>
        {
            entity.ToTable("EmployeeKandang");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.EmployeeId).HasColumnName("EmployeeID");
            entity.Property(e => e.NomorKandang).HasMaxLength(50);
        });

        modelBuilder.Entity<ExpenseMaster>(entity =>
        {
            entity.HasKey(e => e.ExpensiveMasterId);

            entity.ToTable("ExpenseMaster");

            entity.Property(e => e.Amount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.Status).HasMaxLength(50);
        });

        modelBuilder.Entity<ExpensesDetail>(entity =>
        {
            entity.HasKey(e => e.ExpensesDetailsId);

            entity.Property(e => e.Amount).HasColumnType("decimal(18, 2)");
        });

        modelBuilder.Entity<FinancialYear>(entity =>
        {
            entity.ToTable("FinancialYear");
        });

        modelBuilder.Entity<GeneralSetting>(entity =>
        {
            entity.HasKey(e => e.GeneralId);

            entity.ToTable("GeneralSetting");

            entity.Property(e => e.GeneralId).ValueGeneratedNever();
            entity.Property(e => e.CreditLimit).HasMaxLength(50);
            entity.Property(e => e.Discount).HasMaxLength(50);
            entity.Property(e => e.MaxTimbangan).HasColumnType("decimal(18, 0)");
            entity.Property(e => e.NegativeCash).HasMaxLength(50);
            entity.Property(e => e.NegativeStock).HasMaxLength(50);
            entity.Property(e => e.ShowCurrency).HasMaxLength(50);
            entity.Property(e => e.StockCalculationMode).HasMaxLength(50);
            entity.Property(e => e.VatOnPurchase).HasMaxLength(50);
            entity.Property(e => e.VatOnSales).HasMaxLength(50);
        });

        modelBuilder.Entity<Holiday>(entity =>
        {
            entity.ToTable("Holiday");

            entity.Property(e => e.Date)
                .HasColumnType("datetime")
                .HasColumnName("date");
        });

        modelBuilder.Entity<IncomeMaster>(entity =>
        {
            entity.ToTable("IncomeMaster");

            entity.Property(e => e.Amount).HasColumnType("decimal(18, 2)");
        });

        modelBuilder.Entity<InvoiceSetting>(entity =>
        {
            entity.HasKey(e => e.VoucherTypeId);

            entity.ToTable("InvoiceSetting");

            entity.Property(e => e.Ponumber).HasColumnName("PONumber");
            entity.Property(e => e.TypeOfVoucher).HasMaxLength(50);
        });

        modelBuilder.Entity<JenisAyam>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__JenisAya__3214EC27459A5CBD");

            entity.ToTable("JenisAyam");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Nama)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasDefaultValueSql("(NULL)");
        });

        modelBuilder.Entity<JenisPakan>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__JenisPak__3214EC279A8CFDC5");

            entity.ToTable("JenisPakan");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Nama)
                .HasMaxLength(20)
                .IsUnicode(false);
        });

        modelBuilder.Entity<JournalDetail>(entity =>
        {
            entity.HasKey(e => e.JournalDetailsId);

            entity.Property(e => e.Credit).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.Debit).HasColumnType("decimal(18, 2)");
        });

        modelBuilder.Entity<JournalMaster>(entity =>
        {
            entity.ToTable("JournalMaster");

            entity.Property(e => e.AddedDate).HasColumnType("datetime");
            entity.Property(e => e.Amount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.Date).HasColumnType("datetime");
            entity.Property(e => e.Extra2)
                .IsUnicode(false)
                .HasColumnName("extra2");
            entity.Property(e => e.InvoiceNo).HasMaxLength(50);
            entity.Property(e => e.ModifyDate).HasColumnType("datetime");
            entity.Property(e => e.SerialNo).HasMaxLength(50);
            entity.Property(e => e.Status).HasMaxLength(50);
            entity.Property(e => e.VoucherNo).HasMaxLength(50);
        });

        modelBuilder.Entity<Kandang>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Kandang__3214EC27467B88F1");

            entity.ToTable("Kandang");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.NoKandang)
                .HasMaxLength(11)
                .IsUnicode(false);
            entity.Property(e => e.PopulasiMaks)
                .HasMaxLength(11)
                .IsUnicode(false);
        });

        modelBuilder.Entity<LedgerPosting>(entity =>
        {
            entity.ToTable("LedgerPosting");

            entity.Property(e => e.Credit).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.Debit).HasColumnType("decimal(18, 2)");
        });

        modelBuilder.Entity<LogPopulasiAnakKandang>(entity =>
        {
            entity.ToTable("LogPopulasiAnakKandang");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.CageNumber).HasMaxLength(50);
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.EmployeeId).HasColumnName("EmployeeID");
            entity.Property(e => e.RecordDate).HasColumnType("datetime");
        });

        modelBuilder.Entity<MonthlySalary>(entity =>
        {
            entity.ToTable("MonthlySalary");

            entity.Property(e => e.AddedDate).HasColumnType("datetime");
            entity.Property(e => e.ModifyDate).HasColumnType("datetime");
            entity.Property(e => e.SalaryMonth).HasColumnType("datetime");
            entity.Property(e => e.YearMonth).HasMaxLength(50);
        });

        modelBuilder.Entity<MonthlySalaryDetail>(entity =>
        {
            entity.HasKey(e => e.MonthlySalaryDetailsId);
        });

        modelBuilder.Entity<MutasiStockTelurHarian>(entity =>
        {
            entity.ToTable("MutasiStockTelurHarian");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.BeratTelurIn).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.BeratTelurOut).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.JenisTelur).HasMaxLength(50);
            entity.Property(e => e.Reason)
                .HasMaxLength(250)
                .HasDefaultValue("Mutasi");
        });

        modelBuilder.Entity<PayHead>(entity =>
        {
            entity.ToTable("PayHead");

            entity.Property(e => e.PayHeadName).HasMaxLength(50);
            entity.Property(e => e.Type).HasMaxLength(50);
        });

        modelBuilder.Entity<PaymentDetail>(entity =>
        {
            entity.HasKey(e => e.PaymentDetailsId);

            entity.Property(e => e.DueAmount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.ReceiveAmount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.TotalAmount).HasColumnType("decimal(18, 2)");
        });

        modelBuilder.Entity<PaymentMaster>(entity =>
        {
            entity.ToTable("PaymentMaster");

            entity.Property(e => e.Amount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.PaidAmount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.Type).HasMaxLength(50);
        });

        modelBuilder.Entity<Payroll>(entity =>
        {
            entity.ToTable("Payroll");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.BpjsKes)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("BPJS_KES");
            entity.Property(e => e.BpjsTk)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("BPJS_TK");
            entity.Property(e => e.Cicilan).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.CutoffId).HasColumnName("CutoffID");
            entity.Property(e => e.EmployeeId).HasColumnName("EmployeeID");
            entity.Property(e => e.GajiBersih).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.GajiHarian).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.GajiNonPremi).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.MonthSalary).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.NoRek).HasMaxLength(50);
            entity.Property(e => e.Potongan).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.ProsentasePremi).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.RpLembur).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.RpPremi).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.Tambahan).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.Tunjangan).HasColumnType("decimal(18, 2)");
        });

        modelBuilder.Entity<PayrollCutoff>(entity =>
        {
            entity.ToTable("PayrollCutoff");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.PayrollDateStart).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.PayrollPeriode)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.PayrollTotal).HasColumnType("decimal(18, 0)");
            entity.Property(e => e.RealPayrollDate).HasDefaultValueSql("(getdate())");
        });

        modelBuilder.Entity<Penalty>(entity =>
        {
            entity.ToTable("Penalty");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.AdminUserId).HasColumnName("AdminUserID");
            entity.Property(e => e.DateSubmitted).HasColumnType("datetime");
            entity.Property(e => e.EmployeeId).HasColumnName("EmployeeID");
            entity.Property(e => e.ForDate).HasColumnType("datetime");
            entity.Property(e => e.PenaltyAmount).HasColumnType("decimal(18, 0)");
            entity.Property(e => e.PenaltyPhoto).HasDefaultValue("");
        });

        modelBuilder.Entity<Pengumuman>(entity =>
        {
            entity.ToTable("Pengumuman");

            entity.Property(e => e.Id).HasColumnName("ID");
        });

        modelBuilder.Entity<Perijinan>(entity =>
        {
            entity.ToTable("Perijinan");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.ActionByEmployeeId)
                .HasDefaultValue(6)
                .HasColumnName("ActionByEmployeeID");
            entity.Property(e => e.ActionDate).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.DateSubmitted).HasColumnType("datetime");
            entity.Property(e => e.EmployeeId).HasColumnName("EmployeeID");
            entity.Property(e => e.ForDate).HasColumnType("datetime");
            entity.Property(e => e.SubmittedFor).HasMaxLength(50);
        });

        modelBuilder.Entity<PriceHistory>(entity =>
        {
            entity.ToTable("PriceHistory");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Price).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.PriceGroup)
                .HasMaxLength(255)
                .HasDefaultValue("");
            entity.Property(e => e.ProductId).HasColumnName("ProductID");
        });

        modelBuilder.Entity<Privilege>(entity =>
        {
            entity.ToTable("Privilege");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.ToTable("Product");

            entity.Property(e => e.Mrp).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.PurchaseRate).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.QueueNumber).HasDefaultValue(15);
            entity.Property(e => e.SalesRate).HasColumnType("decimal(18, 2)");
        });

        modelBuilder.Entity<ProductGroup>(entity =>
        {
            entity.HasKey(e => e.GroupId);

            entity.ToTable("ProductGroup");
        });

        modelBuilder.Entity<PurchaseDetail>(entity =>
        {
            entity.HasKey(e => e.PurchaseDetailsId);

            entity.Property(e => e.Amount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.Discount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.DiscountAmount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.GrossAmount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.NetAmount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.Qty).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.Rate).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.TaxAmount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.TaxRate).HasColumnType("decimal(18, 2)");
        });

        modelBuilder.Entity<PurchaseMaster>(entity =>
        {
            entity.ToTable("PurchaseMaster");

            entity.Property(e => e.BalanceDue).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.BillDiscount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.DisPer).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.GrandTotal).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.NetAmounts).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.PayAmount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.PaymentStatus).HasMaxLength(50);
            entity.Property(e => e.PreviousDue).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.ShippingAmount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.TaxRate).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.TotalAmount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.TotalTax).HasColumnType("decimal(18, 2)");
        });

        modelBuilder.Entity<PurchaseReturnDetail>(entity =>
        {
            entity.HasKey(e => e.PurchaseReturnDetailsId);

            entity.Property(e => e.Amount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.Discount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.DiscountAmount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.GrossAmount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.NetAmount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.Qty).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.Rate).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.TaxAmount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.TaxRate).HasColumnType("decimal(18, 2)");
        });

        modelBuilder.Entity<PurchaseReturnMaster>(entity =>
        {
            entity.ToTable("PurchaseReturnMaster");

            entity.Property(e => e.BalanceDue).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.BillDiscount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.DisPer).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.GrandTotal).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.NetAmounts).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.PayAmount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.PreviousDue).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.ShippingAmount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.TaxRate).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.TotalAmount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.TotalTax).HasColumnType("decimal(18, 2)");
        });

        modelBuilder.Entity<ReceiptDetail>(entity =>
        {
            entity.HasKey(e => e.ReceiptDetailsId);

            entity.Property(e => e.DueAmount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.ReceiveAmount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.TotalAmount).HasColumnType("decimal(18, 2)");
        });

        modelBuilder.Entity<ReceiptMaster>(entity =>
        {
            entity.ToTable("ReceiptMaster");

            entity.Property(e => e.Amount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.Type).HasMaxLength(50);
        });

        modelBuilder.Entity<RecordNotum>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_NotaPenjualan");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.BeratTelur).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.JenisTelur).HasMaxLength(50);
            entity.Property(e => e.NomorNota).HasMaxLength(50);
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.ToTable("Role");
        });

        modelBuilder.Entity<SalaryHistory>(entity =>
        {
            entity.ToTable("SalaryHistory");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Amount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.EmployeeId).HasColumnName("EmployeeID");
            entity.Property(e => e.SalaryType)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<SalaryPackage>(entity =>
        {
            entity.ToTable("SalaryPackage");

            entity.Property(e => e.AddedDate).HasColumnType("datetime");
            entity.Property(e => e.ModifyDate).HasColumnType("datetime");
            entity.Property(e => e.TotalAmount).HasColumnType("decimal(18, 2)");
        });

        modelBuilder.Entity<SalaryPackageDetail>(entity =>
        {
            entity.HasKey(e => e.SalaryPackageDetailsId);

            entity.Property(e => e.Amount).HasColumnType("decimal(18, 2)");
        });

        modelBuilder.Entity<SalaryVoucherDetail>(entity =>
        {
            entity.HasKey(e => e.SalaryVoucherDetailsId);

            entity.Property(e => e.Advance).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.Bonus).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.Deduction).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.Lop).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.Salary).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.Status).HasMaxLength(50);
        });

        modelBuilder.Entity<SalaryVoucherMaster>(entity =>
        {
            entity.ToTable("SalaryVoucherMaster");

            entity.Property(e => e.AddedDate).HasColumnType("datetime");
            entity.Property(e => e.Date).HasColumnType("datetime");
            entity.Property(e => e.FinancialYearId).HasColumnName("financialYearId");
            entity.Property(e => e.InvoiceNo).HasMaxLength(50);
            entity.Property(e => e.LedgerId).HasMaxLength(50);
            entity.Property(e => e.ModifyDate).HasColumnType("datetime");
            entity.Property(e => e.Month).HasColumnType("datetime");
            entity.Property(e => e.SerialNo).HasMaxLength(50);
            entity.Property(e => e.TotalAmount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.VoucherNo).HasMaxLength(50);
            entity.Property(e => e.YearMonth).HasMaxLength(50);
        });

        modelBuilder.Entity<SalesDetail>(entity =>
        {
            entity.HasKey(e => e.SalesDetailsId);

            entity.Property(e => e.Amount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.Discount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.DiscountAmount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.GrossAmount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.NetAmount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.Qty).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.Rate).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.TaxAmount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.TaxRate).HasColumnType("decimal(18, 2)");
        });

        modelBuilder.Entity<SalesMaster>(entity =>
        {
            entity.ToTable("SalesMaster");

            entity.Property(e => e.BalanceDue).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.Bayar).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.BillDiscount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.DisPer).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.GrandTotal).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.Kembali).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.Kp10).HasColumnName("KP10");
            entity.Property(e => e.Kp15).HasColumnName("KP15");
            entity.Property(e => e.NetAmounts).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.PayAmount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.PaymentStatus).HasMaxLength(50);
            entity.Property(e => e.Pos).HasColumnName("POS");
            entity.Property(e => e.PreviousDue).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.Reference).HasMaxLength(50);
            entity.Property(e => e.ShippingAmount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.TaxRate).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.TipePembayaran)
                .HasMaxLength(50)
                .HasDefaultValue("Tunai");
            entity.Property(e => e.TotalAmount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.TotalTax).HasColumnType("decimal(18, 2)");
        });

        modelBuilder.Entity<SalesQuotationDetail>(entity =>
        {
            entity.HasKey(e => e.QuotationDetailsId);

            entity.Property(e => e.Amount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.Discount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.DiscountAmount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.GrossAmount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.NetAmount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.Qty).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.Rate).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.TaxAmount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.TaxRate).HasColumnType("decimal(18, 2)");
        });

        modelBuilder.Entity<SalesQuotationMaster>(entity =>
        {
            entity.HasKey(e => e.QuotationMasterId);

            entity.ToTable("SalesQuotationMaster");

            entity.Property(e => e.BillDiscount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.DisPer).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.GrandTotal).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.ShippingAmount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.TaxId).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.TaxRate).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.TotalAmount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.TotalTax).HasColumnType("decimal(18, 2)");
        });

        modelBuilder.Entity<SalesReturnDetail>(entity =>
        {
            entity.HasKey(e => e.SalesReturnDetailsId);

            entity.Property(e => e.Amount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.Discount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.DiscountAmount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.GrossAmount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.NetAmount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.Qty).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.Rate).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.TaxAmount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.TaxRate).HasColumnType("decimal(18, 2)");
        });

        modelBuilder.Entity<SalesReturnMaster>(entity =>
        {
            entity.ToTable("SalesReturnMaster");

            entity.Property(e => e.BalanceDue).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.BillDiscount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.DisPer).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.GrandTotal).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.NetAmounts).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.PayAmount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.PreviousDue).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.ShippingAmount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.TaxRate).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.TotalAmount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.TotalTax).HasColumnType("decimal(18, 2)");
        });

        modelBuilder.Entity<StandardDatum>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_StarndardData");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.EmassStandard)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("EMass_Standard");
            entity.Property(e => e.FiStandard)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("FI_Standard");
            entity.Property(e => e.GrBtStandard)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("GrBt_Standard");
            entity.Property(e => e.HdStandard)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("HD_Standard");
        });

        modelBuilder.Entity<StockGudang>(entity =>
        {
            entity.ToTable("StockGudang");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.EggType).HasMaxLength(50);
            entity.Property(e => e.TotalDitimbang).HasColumnType("decimal(18, 2)");
        });

        modelBuilder.Entity<StockPenjualan>(entity =>
        {
            entity.ToTable("StockPenjualan");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.EggType).HasMaxLength(50);
            entity.Property(e => e.StockKg).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.VerifiedOn).HasDefaultValueSql("(getdate())");
        });

        modelBuilder.Entity<StockPosting>(entity =>
        {
            entity.ToTable("StockPosting");

            entity.Property(e => e.InwardQty).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.OutwardQty).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.Rate).HasColumnType("decimal(18, 2)");
        });

        modelBuilder.Entity<StockTelurUtuh>(entity =>
        {
            entity.ToTable("StockTelurUtuh");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.StockDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.StockKg)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("StockKG");
        });

        modelBuilder.Entity<Task>(entity =>
        {
            entity.HasKey(e => e.TaskId).HasName("PK_Task");

            entity.Property(e => e.AddedDate).HasColumnType("datetime");
            entity.Property(e => e.EndDate).HasColumnType("datetime");
            entity.Property(e => e.ModifyDate).HasColumnType("datetime");
            entity.Property(e => e.StartDate).HasColumnType("datetime");
        });

        modelBuilder.Entity<TaskCategory>(entity =>
        {
            entity.ToTable("TaskCategory");

            entity.Property(e => e.Color).HasMaxLength(50);
            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<TaskPriority>(entity =>
        {
            entity.ToTable("TaskPriority");

            entity.Property(e => e.Color).HasMaxLength(50);
            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<Tax>(entity =>
        {
            entity.ToTable("Tax");

            entity.Property(e => e.Rate).HasColumnType("decimal(18, 2)");
        });

        modelBuilder.Entity<Unit>(entity =>
        {
            entity.ToTable("Unit");
        });

        modelBuilder.Entity<UnitConvertion>(entity =>
        {
            entity.HasKey(e => e.UnitconversionId);

            entity.ToTable("UnitConvertion");

            entity.Property(e => e.ConversionRate).HasColumnType("decimal(18, 2)");
        });

        modelBuilder.Entity<UserMaster>(entity =>
        {
            entity.HasKey(e => e.UserId);

            entity.ToTable("UserMaster");

            entity.Property(e => e.Active).HasDefaultValue(true);
            entity.Property(e => e.Image).HasDefaultValue("");
            entity.Property(e => e.Phone).HasDefaultValue("");
        });

        modelBuilder.Entity<Warehouse>(entity =>
        {
            entity.ToTable("Warehouse");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
