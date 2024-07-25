using System.ComponentModel.DataAnnotations;

namespace CutomerOrder.Models
{
	public class Customer
	{
		[Key]
		public Guid UserId { get; set; }
		public string? Username { get; set; }
		public string? Email { get; set; }
		public string? FirstName { get; set; }
		public string? LastName { get; set; }
		public DateTime CreatedOn { get; set; }
		public bool IsActive { get; set; }
	}
}
