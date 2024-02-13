using Npgsql;
using Product_API.Models;
using static System.Net.WebRequestMethods;

namespace Product_API.Repositories
{
    public class ProductPostgressRepository : IProductRepository
    {
        public string connectionString = "Host=localhost;Port=5432;Database=ConnectToVisualStudio;User Id=postgres;Password=abdullayev;";
        public Product Add(Product product)
        {
            using NpgsqlConnection connection = new NpgsqlConnection(connectionString);
            connection.Open();
            string query = $"Insert Into product(Name,Description,PhotoPath) values({product.Name},{product.Description},{product.PhotoPath});";


            NpgsqlCommand command = new NpgsqlCommand(query, connection);
            command.ExecuteNonQuery();
            return product;


        }

        public List<Product> GetAll()
        {
            List<Product> products = new List<Product>();
            using NpgsqlConnection connection = new NpgsqlConnection(connectionString);
            connection.Open();
            try
            {
                string query = "select * from product";
                Product product = new Product();
                using NpgsqlCommand cmd = new NpgsqlCommand(query, connection);
                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    product.Name = reader.GetString(0);
                    product.Description = reader.GetString(1);
                    product.PhotoPath = reader.GetString(2);
                    products.Add(product);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            connection.Close();

            return products;



        }
    }
}
