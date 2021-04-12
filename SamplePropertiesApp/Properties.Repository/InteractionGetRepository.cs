using Newtonsoft.Json;
using Properties.Common;
using Properties.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Net.Http;

namespace Properties.Repository
{
    public interface IInteractionGetRepository
    {
        public List<PropertyInfo> GetResponse();
    }

    public class InteractionGetRepository : IInteractionGetRepository
    {
        private readonly HttpClient _httpClient;
        private string connectionString = AppConfig.Get("Config")["ConnectionString"];

        public InteractionGetRepository()
        {
            _httpClient = new HttpClient();
        }

        public InteractionGetRepository(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public List<PropertyInfo> GetResponse()
        {
            var url = AppConfig.Get("Config")["SourceUrl"];
            var response = _httpClient.GetAsync(url).Result;

            if (response.IsSuccessStatusCode)
            {
                var result = response.Content.ReadAsStringAsync();
                var data = JsonConvert.DeserializeObject<PropertiesResponseDto>(result.Result);
                var ExistingList = GetExistingPropertyIds();
                return data.GetRequiredProperties(ExistingList);
            }

            return null;
        }

        private List<int> GetExistingPropertyIds()
        {
            List<int> response = new List<int>();
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    var cmdText = "usp_GetPropertyIds";
                    SqlCommand cmd = new SqlCommand(cmdText, connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlDataReader dataReader = cmd.ExecuteReader();
                    if (dataReader.HasRows)
                    {
                        while (dataReader.Read())
                        {
                            int.TryParse(dataReader[0].ToString(), out var id);
                            response.Add(id);
                        };
                    }
                }
            }
            catch(Exception ex)
            {
                return null;
            }

            return response;
        }
    }
}
