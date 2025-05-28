using System.ComponentModel.DataAnnotations;

namespace QuickAccounting.Data
{
	public class Pengumuman
	{
		[Key]
		public int ID { get; set; }
		public string PengumumanContent { get; set; }
	}
}
