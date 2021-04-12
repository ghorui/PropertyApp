using Properties.Common;
using Properties.Model;
using System.Configuration;
using System.Data.SqlClient;

namespace Properties.Repository
{
    public interface IInteractionPostRepository
    {
        bool SaveDataToDb(PropertyInfo input);
    }

    public class InteractionPostRepository : IInteractionPostRepository
    {
        private string connectionString =  AppConfig.Get("Config")["ConnectionString"];
        public bool SaveDataToDb(PropertyInfo input)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    var cmdText = "usp_saveProperty";
                    SqlCommand cmd = new SqlCommand(cmdText, connection);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Id", input.Id);
                    cmd.Parameters.AddWithValue("@year_built", input.YearBuilt);
                    cmd.Parameters.AddWithValue("@list_price", input.ListPrice);
                    cmd.Parameters.AddWithValue("@monthly_rent", input.MonthlyRent);
                    cmd.Parameters.AddWithValue("@gross_yield", input.GrossYield);
                    cmd.Parameters.AddWithValue("@address1", input.Address.Address1);
                    cmd.Parameters.AddWithValue("@address2", input.Address.Address2);
                    cmd.Parameters.AddWithValue("@city", input.Address.City);
                    cmd.Parameters.AddWithValue("@country", input.Address.Country);
                    cmd.Parameters.AddWithValue("@district", input.Address.District);
                    cmd.Parameters.AddWithValue("@state", input.Address.State);
                    cmd.Parameters.AddWithValue("@zip", input.Address.Zip);
                    cmd.Parameters.AddWithValue("@zip_plus4", input.Address.ZipPlus4);

                    return cmd.ExecuteNonQuery() > 0;
                }
            }
            catch (System.Exception ex)
            {
                return false;
            }
        }
    }
}
