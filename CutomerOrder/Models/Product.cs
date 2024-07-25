using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CutomerOrder.Models
{
	public class Product
	{
		[Key]
		public Guid ProductId { get; set; }
		public string? ProductName { get; set; }
		public decimal UnitPrice { get; set; }
		[ForeignKey("SupplierId")]
		public Guid SupplierId { get; set; }
		public DateTime CreatedOn { get; set; }
		public bool IsActive { get; set; }
	}
}
