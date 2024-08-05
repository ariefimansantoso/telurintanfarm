﻿using QuickAccounting.Data.Inventory;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuickAccounting.Data.HrPayroll
{
	public class SalaryPackage
	{
		[Key]
		public int SalaryPackageId { get; set; }
		[Required]
		public string SalaryPackageName { get; set; }
		public bool IsActive { get; set; }
		public string Narration { get; set; }
		public decimal TotalAmount { get; set; }
		public DateTime? AddedDate { get; set; }
		public DateTime? ModifyDate { get; set; }
		[NotMapped]
		public List<SalaryPackageDetails> listPackage { get; set; } = new List<SalaryPackageDetails>();
        [NotMapped]
        public List<DeleteItem> listDelete { get; set; } = new List<DeleteItem>();
    }
}
