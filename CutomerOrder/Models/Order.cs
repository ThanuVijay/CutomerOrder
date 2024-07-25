using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CutomerOrder.Models
{
	public class Order
	{
		[Key]
		public Guid OrderId { get; set; }
		[ForeignKey("ProductId")]
		public Guid ProductId { get; set; }
		public int OrderStatus { get; set; }
		public int OrderType { get; set; }
		[ForeignKey("UserID")]
		public Guid OrderBy { get; set; }
		public DateTime OrderedOn { get; set; }
		public DateTime ShippedOn { get; set; }
		public bool IsActive { get; set; }
	}
}
