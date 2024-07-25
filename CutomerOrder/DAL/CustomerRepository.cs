using CutomerOrder.Models;
using System.Data.SqlClient;

namespace CutomerOrder.DAL
{
	public class CustomerRepository
	{
		private readonly string _connectionString;

        public CustomerRepository(IConfiguration configuration)
        {
			_connectionString = configuration.GetConnectionString("customerconn");

		}

		public async Task CreateCustomer(Customer customer)
		{
			using (SqlConnection conn = new SqlConnection(_connectionString))
			{
				await conn.OpenAsync();
				string query = "INSERT INTO Customer (UserId, Username, Email, FirstName, LastName, CreatedOn, IsActive) VALUES (@UserId, @Username, @Email, @FirstName, @LastName, @CreatedOn, @IsActive)";
				SqlCommand cmd = new SqlCommand(query, conn);
				cmd.Parameters.AddWithValue("@UserId", customer.UserId);
				cmd.Parameters.AddWithValue("@Username", customer.Username);
				cmd.Parameters.AddWithValue("@Email", customer.Email);
				cmd.Parameters.AddWithValue("@FirstName", customer.FirstName);
				cmd.Parameters.AddWithValue("@LastName", customer.LastName);
				cmd.Parameters.AddWithValue("@CreatedOn", customer.CreatedOn);
				cmd.Parameters.AddWithValue("@IsActive", customer.IsActive);
				await cmd.ExecuteNonQueryAsync();
			}
		}

		public async Task<IEnumerable<Customer>> GetAllCustomers()
		{
			var customers = new List<Customer>();
			using (SqlConnection conn = new SqlConnection(_connectionString))
			{
				await conn.OpenAsync();
				string query = "SELECT * FROM Customer";
				SqlCommand cmd = new SqlCommand(query,conn);
				using (SqlDataReader reader = await cmd.ExecuteReaderAsync()) 
				{
					while(await reader.ReadAsync()) 
					{
						customers.Add(new Customer
						{
							UserId = reader.GetGuid(0),
							Username = reader.GetString(1),
							Email = reader.GetString(2),
							FirstName = reader.GetString(3),
							LastName = reader.GetString(4),
							CreatedOn = reader.GetDateTime(5),
							IsActive = reader.GetBoolean(6)
						});
					}
				}
			}
			return customers;
		}

		public async Task UpdateCustomer(Customer customer)
		{
			using (SqlConnection conn = new SqlConnection(_connectionString))
			{
				await conn.OpenAsync();
				string query = "UPDATE Customer SET Username=@Username, Email=@Email, FirstName=@FirstName, LastName=@LastName, IsActive=@IsActive WHERE UserId=@UserId";
				SqlCommand cmd = new SqlCommand(query, conn);
				cmd.Parameters.AddWithValue("@Username", customer.Username);
				cmd.Parameters.AddWithValue("@Email", customer.Email);
				cmd.Parameters.AddWithValue("@FirstName", customer.FirstName);
				cmd.Parameters.AddWithValue("@LastName", customer.LastName);
				cmd.Parameters.AddWithValue("@IsActive", customer.IsActive);
				cmd.Parameters.AddWithValue("@UserId", customer.UserId);
				await cmd.ExecuteNonQueryAsync();
			}
		}

		public async Task DeleteCustomer(Guid userId)
		{
			using (SqlConnection conn = new SqlConnection(_connectionString))
			{
				await conn.OpenAsync();
				string query = "DELETE FROM Customer WHERE UserId=@UserId";
				SqlCommand cmd = new SqlCommand(query, conn);
				cmd.Parameters.AddWithValue("@UserId", userId);
				await cmd.ExecuteNonQueryAsync();
			}
		}

		public async Task<IEnumerable<Order>> GetActiveOrdersByCustomer(Guid userId)
		{
			var orders = new List<Order>();
			using (SqlConnection conn = new SqlConnection(_connectionString))
			{
				await conn.OpenAsync();
				string query = @"
                SELECT o.*, p.ProductName, p.UnitPrice, s.SupplierName
                FROM Orders o
                INNER JOIN Products p ON o.ProductId = p.ProductId
                INNER JOIN Suppliers s ON p.SupplierId = s.SupplierId
                WHERE o.OrderBy = @UserId AND o.IsActive = 1";
				SqlCommand cmd = new SqlCommand(query, conn);
				cmd.Parameters.AddWithValue("@UserId", userId);
				using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
				{
					while (await reader.ReadAsync())
					{
						orders.Add(new Order
						{
							OrderId = reader.GetGuid(0),
							ProductId = reader.GetGuid(1),
							OrderStatus = reader.GetInt32(2),
							OrderType = reader.GetInt32(3),
							OrderBy = reader.GetGuid(4),
							OrderedOn = reader.GetDateTime(5),
							ShippedOn = reader.GetDateTime(6),
							IsActive = reader.GetBoolean(7)
						});
					}
				}
			}
			return orders;
		}
	}
}
