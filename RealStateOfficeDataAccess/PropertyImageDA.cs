using Microsoft.Data.SqlClient;
using RealStateOfficeModels.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateOfficeDataAccess
{
    public class PropertyImageDA
    {

        public bool Add(PropertyImageDataModel model)
        {
            using SqlConnection con =
            new SqlConnection(
            DbConnection.StringConnection);


            string query =
            @"
INSERT INTO Property_Image
(
    PropertyID,
    ImagePath
)

VALUES
(
    @PropertyID,
    @ImagePath
)
";


            SqlCommand cmd =
            new SqlCommand(query, con);


            cmd.Parameters.AddWithValue(
            "@PropertyID",
            model.PropertyID);


            cmd.Parameters.AddWithValue(
            "@ImagePath",
            model.ImagePath);


            con.Open();


            return cmd.ExecuteNonQuery() > 0;
        }


        public static List<PropertyImageModel> GetByPropertyID(int propertyID)
        {
            List<PropertyImageModel> images = new();

            using SqlConnection con =
            new SqlConnection(DbConnection.StringConnection);

            string query =
            @"
    SELECT
        ImageID,
        ImagePath
    FROM Property_Image
    WHERE PropertyID = @PropertyID
    ";

            SqlCommand cmd =
            new SqlCommand(query, con);

            cmd.Parameters.AddWithValue(
                "@PropertyID",
                propertyID);

            con.Open();

            SqlDataReader reader =
            cmd.ExecuteReader();

            while (reader.Read())
            {
                images.Add(new PropertyImageModel
                {
                    ImageID = Convert.ToInt32(reader["ImageID"]),

                    ImagePath = reader["ImagePath"].ToString()!
                });
            }

            return images;
        }


        public PropertyImageModel? GetByID(int imageID)
        {
            using SqlConnection con =
            new SqlConnection(DbConnection.StringConnection);

            string query =
            @"
    SELECT
        ImageID,
        PropertyID,
        ImagePath
    FROM Property_Image
    WHERE ImageID=@ImageID
    ";

            SqlCommand cmd =
            new SqlCommand(query, con);

            cmd.Parameters.AddWithValue(
                "@ImageID",
                imageID);

            con.Open();

            SqlDataReader reader =
            cmd.ExecuteReader();

            if (reader.Read())
            {
                return new PropertyImageModel
                {
                    ImageID =
                    Convert.ToInt32(reader["ImageID"]),

                    ImagePath =
                    reader["ImagePath"].ToString()!
                };
            }

            return null;
        }

        public bool Delete(int imageID)
        {
            using SqlConnection con =
            new SqlConnection(DbConnection.StringConnection);

            string query =
            @"
    DELETE FROM Property_Image
    WHERE ImageID=@ImageID
    ";

            SqlCommand cmd =
            new SqlCommand(query, con);

            cmd.Parameters.AddWithValue(
                "@ImageID",
                imageID);

            con.Open();

            return cmd.ExecuteNonQuery() > 0;
        }
    }
}
