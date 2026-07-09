using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RealStateOfficeModels.Properties;
namespace RealEstateOfficeDataAccess
{
    public class PurposeDA
    {
        public static List<Purpose> GetAll()
        {
            List<Purpose> purposes = new();

            using SqlConnection con =
            new SqlConnection(DbConnection.StringConnection);

            string query =
            @"SELECT PurposeID,Name
      FROM Purpose";

            SqlCommand cmd =
            new SqlCommand(query, con);

            con.Open();

            SqlDataReader reader =
            cmd.ExecuteReader();

            while (reader.Read())
            {
                purposes.Add(new Purpose
                {
                    PurposeID =
                    Convert.ToInt32(reader["PurposeID"]),

                    Name =
                    reader["Name"].ToString()
                });
            }

            return purposes;
        }

       
    }
}
