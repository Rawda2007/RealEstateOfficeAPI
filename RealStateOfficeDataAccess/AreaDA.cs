using Microsoft.Data.SqlClient;
using RealStateOfficeModels.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateOfficeDataAccess
{
    public class AreaDA
    {
        public List<AreaModel> GetAll()
        {
            List<AreaModel> areas =
            new List<AreaModel>();


            using SqlConnection con =
            new SqlConnection(
            DbConnection.StringConnection);


            string query =
            @"
    SELECT
    AreaID,
    GovernorateID,
    Name

    FROM Area
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
                AreaModel area =
                new AreaModel()
                {
                    AreaID =
                    Convert.ToInt32(
                    reader["AreaID"]),

                    GovernorateID =
                    Convert.ToInt32(
                    reader["GovernorateID"]),

                    Name =
                    reader["Name"].ToString()
                };

                areas.Add(area);
            }


            return areas;
        }


        public List<AreaModel> GetByGovernorate(int governorateID)
        {
            List<AreaModel> areas = new();

            using SqlConnection con =
            new SqlConnection(DbConnection.StringConnection);

            string query =
            @"
    SELECT
    AreaID,
    GovernorateID,
    Name

    FROM Area

    WHERE GovernorateID=@GovernorateID
    ";

            SqlCommand cmd =
            new SqlCommand(query, con);

            cmd.Parameters.AddWithValue(
            "@GovernorateID",
            governorateID);

            con.Open();

            SqlDataReader reader =
            cmd.ExecuteReader();

            while (reader.Read())
            {
                areas.Add(new AreaModel
                {
                    AreaID =
                    Convert.ToInt32(reader["AreaID"]),

                    GovernorateID =
                    Convert.ToInt32(reader["GovernorateID"]),

                    Name =
                    reader["Name"].ToString()
                });
            }

            return areas;
        }
    }
}
