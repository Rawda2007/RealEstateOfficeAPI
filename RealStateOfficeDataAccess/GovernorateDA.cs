using Microsoft.Data.SqlClient;
using RealStateOfficeModels.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateOfficeDataAccess
{
   public class GovernorateDA
    {
        public List<GovernorateModel> GetAll()
        {
            List<GovernorateModel> governorates =
            new List<GovernorateModel>();


            using SqlConnection con =
            new SqlConnection(
            DbConnection.StringConnection);


            string query =
            @"
    SELECT
    GovernorateID,
    Name

    FROM Governorate
    ";


            SqlCommand cmd =
            new SqlCommand(
            query,
            con);


            con.Open();


            SqlDataReader reader =
            cmd.ExecuteReader();


            while (reader.Read())
            {
                GovernorateModel governorate =
                new GovernorateModel()
                {
                    GovernorateID =
                    Convert.ToInt32(
                    reader["GovernorateID"]),

                    Name =
                    reader["Name"].ToString()
                };

                governorates.Add(governorate);
            }


            return governorates;
        }
    }
}
