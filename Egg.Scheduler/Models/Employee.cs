using System;
using System.Collections.Generic;

namespace Egg.Scheduler.Models;

public partial class Employee
{
    public int EmployeeId { get; set; }

    public int OldEmployeeId { get; set; }

    public int? DesignationId { get; set; }

    public string? EmployeeName { get; set; }

    public string? EmployeeCode { get; set; }

    public DateTime? Dob { get; set; }

    public string? MaritalStatus { get; set; }

    public string? Gender { get; set; }

    public string? Qualification { get; set; }

    public string? Address { get; set; }

    public string? PhoneNumber { get; set; }

    public string? MobileNumber { get; set; }

    public string? Email { get; set; }

    public DateTime? JoiningDate { get; set; }

    public bool? IsActive { get; set; }

    public string? Narration { get; set; }

    public string? SalaryType { get; set; }

    public decimal? DailyWage { get; set; }

    public int? DefaultPackageId { get; set; }

    public int? UserId { get; set; }

    public string? BirthPlace { get; set; }

    public string? Nik { get; set; }

    public string? IdBpjsKes { get; set; }

    public string? IdBpjsTk { get; set; }

    public string? BankAccount { get; set; }

    public int? SupervisorId { get; set; }

    public decimal? BpjsKes { get; set; }

    public decimal? BpjsTk { get; set; }

    public decimal Lembur { get; set; }

    public decimal Tunjangan { get; set; }

    public string JobDesc { get; set; } = null!;

    public string Sop { get; set; } = null!;
}
