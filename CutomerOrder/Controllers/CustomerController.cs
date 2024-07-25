using CutomerOrder.DAL;
using CutomerOrder.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CutomerOrder.Controllers
{
	[Route("api/v1/[controller]")]
	[ApiController]
	public class CustomerController : ControllerBase
	{
		private readonly CustomerRepository _customerRepository;
        public CustomerController(CustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

		[HttpPost]
		public async Task<IActionResult> CreateCustomer(Customer customer)
		{
			try
			{
				await _customerRepository.CreateCustomer(customer);
				return Ok(new { success = true, message = "Customer created successfully." });
			}
			catch (Exception ex)
			{
				return StatusCode(500, new { success = false, message = $"An error occurred while creating the customer: {ex.Message}" });
			}
		}

		[HttpGet]
		public async Task<IActionResult>GetAllCustomers()
		{
			try
			{
				var customers = await _customerRepository.GetAllCustomers();
				return Ok(new { success = true, data = customers });
			}
			catch (Exception ex)
			{
				return StatusCode(500, new { success = false, message = $"An error occurred while retrieving customers: {ex.Message}" });
			}
		}

		[HttpPost("update")]
		public async Task<IActionResult> UpdateCustomer(Customer customer)
		{
			try
			{
				await _customerRepository.UpdateCustomer(customer);
				return Ok(new { success = true, message = "Customer updated successfully." });
			}
			catch (Exception ex)
			{
				return StatusCode(500, new { success = false, message = $"An error occurred while updating the customer: {ex.Message}" });
			}
		}

		[HttpDelete("{userId}")]
		public async Task<IActionResult> DeleteCustomer(Guid userId)
		{
			try
			{
				await _customerRepository.DeleteCustomer(userId);
				return Ok(new { success = true, message = "Customer deleted successfully." });
			}
			catch (Exception ex)
			{
				return StatusCode(500, new { success = false, message = $"An error occurred while deleting the customer: {ex.Message}" });
			}
		}

		[HttpGet("active-orders/{userId}")]
		public async Task<IActionResult> GetActiveOrdersByCustomer(Guid userId)
		{
			try
			{
				var orders = await _customerRepository.GetActiveOrdersByCustomer(userId);
				return Ok(new { success = true, data = orders });
			}
			catch (Exception ex)
			{
				return StatusCode(500, new { success = false, message = $"An error occurred while retrieving active orders: {ex.Message}" });
			}
		}
	}
}
