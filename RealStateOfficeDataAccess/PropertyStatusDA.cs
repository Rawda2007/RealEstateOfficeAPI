using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RealStateOfficeModels.Properties;

namespace RealEstateOfficeDataAccess
{
    public class PropertyStatusDA
    {
        public List<PropertyStatus> GetAll()
        {
            List<PropertyStatus> status = new();

            using SqlConnection con =
            new SqlConnection(DbConnection.StringConnection);

            string query =
            @"SELECT StatusID,Name
      FROM PropertyStatus";

            SqlCommand cmd =
            new SqlCommand(query, con);

            con.Open();

            SqlDataReader reader =
            cmd.ExecuteReader();

            while (reader.Read())
            {
                status.Add(new PropertyStatus
                {
                    StatusID =
                    Convert.ToInt32(reader["StatusID"]),

                    Name =
                    reader["Name"].ToString()
                });
            }

            return status;
        }
    }
}
