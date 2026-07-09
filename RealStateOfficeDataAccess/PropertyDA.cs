using Microsoft.Data.SqlClient;
using RealStateOfficeModels.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateOfficeDataAccess
{
    public class PropertyDA
    {
        public List<PropertyDetailsModel> GetAll()
        {
            List<PropertyDetailsModel> properties =
            new List<PropertyDetailsModel>();


            using SqlConnection con =
            new SqlConnection(
            DbConnection.StringConnection);


            string query =
            @"
SELECT

P.PropertyID,
P.Title,
P.Description,
P.Price,
P.AreaSize,
P.NumRooms,
P.NumBathrooms,

A.Name AS Area,

G.Name AS Governorate,

PU.Name AS Purpose,

PS.Name AS Status
,
(
    SELECT TOP 1 ImagePath
    FROM Property_Image
    WHERE PropertyID = P.PropertyID 
 ORDER BY ImageID ASC
) AS FirstImage FROM Property P

INNER JOIN Area A
ON P.AreaID = A.AreaID

INNER JOIN Governorate G
ON A.GovernorateID = G.GovernorateID

INNER JOIN Purpose PU
ON P.PurposeID = PU.PurposeID

INNER JOIN PropertyStatus PS
ON P.StatusID = PS.StatusID

";


            SqlCommand cmd =
            new SqlCommand(query, con);


            con.Open();


            SqlDataReader reader =
            cmd.ExecuteReader();


            while (reader.Read())
            {
                PropertyDetailsModel property =
                new PropertyDetailsModel()
                {
                    PropertyID =
                    Convert.ToInt32(
                    reader["PropertyID"]),

                    Title =
                    reader["Title"].ToString(),

                    Description =
                    reader["Description"].ToString(),

                    Price =
                    Convert.ToDecimal(
                    reader["Price"]),

                    AreaSize =
                    Convert.ToDecimal(
                    reader["AreaSize"]),

                    NumRooms =
                    Convert.ToInt32(
                    reader["NumRooms"]),

                    NumBathrooms =
                    Convert.ToInt32(
                    reader["NumBathrooms"]),

                    Area =
                    reader["Area"].ToString(),

                    Governorate =
                    reader["Governorate"].ToString(),

                    Purpose =
                    reader["Purpose"].ToString(),

                    Status =
                    reader["Status"].ToString() ,
                    FirstImage=
                    reader["FirstImage"] != DBNull.Value ? reader["FirstImage"].ToString() : null
                };

                properties.Add(property);
            }


            return properties;
        }

        public PropertyDetailsWithImagesModel? GetByID(int propertyID)
        {
            PropertyDetailsWithImagesModel? property = null;


            using SqlConnection con =
            new SqlConnection(
            DbConnection.StringConnection);


            string query =
            @"
SELECT

P.PropertyID,
P.Title,
P.Description,
P.Price,
P.AreaSize,
P.NumRooms,
P.NumBathrooms,

A.Name AS Area,

G.Name AS Governorate,

PU.Name AS Purpose,

PS.Name AS Status

FROM Property P

INNER JOIN Area A
ON P.AreaID = A.AreaID

INNER JOIN Governorate G
ON A.GovernorateID = G.GovernorateID

INNER JOIN Purpose PU
ON P.PurposeID = PU.PurposeID

INNER JOIN PropertyStatus PS
ON P.StatusID = PS.StatusID

WHERE P.PropertyID=@PropertyID
";


            SqlCommand cmd =
            new SqlCommand(query, con);


            cmd.Parameters.AddWithValue(
            "@PropertyID",
            propertyID);


            con.Open();


            SqlDataReader reader =
            cmd.ExecuteReader();


            if (reader.Read())
            {
                property =
                new PropertyDetailsWithImagesModel()
                {
                    PropertyID =
                    Convert.ToInt32(
                    reader["PropertyID"]),

                    Title =
                    reader["Title"].ToString(),

                    Description =
                    reader["Description"].ToString(),

                    Price =
                    Convert.ToDecimal(
                    reader["Price"]),

                    AreaSize =
                    Convert.ToDecimal(
                    reader["AreaSize"]),

                    NumRooms =
                    Convert.ToInt32(
                    reader["NumRooms"]),

                    NumBathrooms =
                    Convert.ToInt32(
                    reader["NumBathrooms"]),

                    Area =
                    reader["Area"].ToString(),

                    Governorate =
                    reader["Governorate"].ToString(),

                    Purpose =
                    reader["Purpose"].ToString(),

                    Status =
                    reader["Status"].ToString()
                };
            }


            reader.Close();

            return property;
        }

        public List<PropertyDetailsModel> GetByGovernorate(int governorateID)
        {
            List<PropertyDetailsModel> properties =
            new List<PropertyDetailsModel>();


            using SqlConnection con =
            new SqlConnection(
            DbConnection.StringConnection);


            string query =
            @"
SELECT

P.PropertyID,
P.Title,
P.Description,
P.Price,
P.AreaSize,
P.NumRooms,
P.NumBathrooms,

A.Name AS Area,

G.Name AS Governorate,

PU.Name AS Purpose,

PS.Name AS Status

FROM Property P

INNER JOIN Area A
ON P.AreaID = A.AreaID

INNER JOIN Governorate G
ON A.GovernorateID = G.GovernorateID

INNER JOIN Purpose PU
ON P.PurposeID = PU.PurposeID

INNER JOIN PropertyStatus PS
ON P.StatusID = PS.StatusID

WHERE G.GovernorateID=@GovernorateID
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
                PropertyDetailsModel property =
                new PropertyDetailsModel()
                {
                    PropertyID =
                    Convert.ToInt32(reader["PropertyID"]),

                    Title =
                    reader["Title"].ToString(),

                    Description =
                    reader["Description"].ToString(),

                    Price =
                    Convert.ToDecimal(reader["Price"]),

                    AreaSize =
                    Convert.ToDecimal(reader["AreaSize"]),

                    NumRooms =
                    Convert.ToInt32(reader["NumRooms"]),

                    NumBathrooms =
                    Convert.ToInt32(reader["NumBathrooms"]),

                    Area =
                    reader["Area"].ToString(),

                    Governorate =
                    reader["Governorate"].ToString(),

                    Purpose =
                    reader["Purpose"].ToString(),

                    Status =
                    reader["Status"].ToString()
                };

                properties.Add(property);
            }


            reader.Close();

            return properties;
        }


        public bool Add(PropertyModel model)
        {
            using SqlConnection con =
            new SqlConnection(
            DbConnection.StringConnection);


            string query =
            @"
INSERT INTO Property
(
    AreaID,ownerID,
    PurposeID,
    Title,
    Description,
    Price,
    AreaSize,
    NumRooms,
    NumBathrooms,
    StatusID
)

VALUES
(
    @AreaID,@OwnerID,
    @PurposeID,
    @Title,
    @Description,
    @Price,
    @AreaSize,
    @NumRooms,
    @NumBathrooms,
    @StatusID
)
";


            SqlCommand cmd =
            new SqlCommand(query, con);


            cmd.Parameters.AddWithValue("@AreaID", model.AreaID);
            cmd.Parameters.AddWithValue("@OwnerID", model.OwnerID);


            cmd.Parameters.AddWithValue("@PurposeID", model.PurposeID);

            cmd.Parameters.AddWithValue("@Title", model.Title);

            cmd.Parameters.AddWithValue("@Description", model.Description);

            cmd.Parameters.AddWithValue("@Price", model.Price);

            cmd.Parameters.AddWithValue("@AreaSize", model.AreaSize);

            cmd.Parameters.AddWithValue("@NumRooms", model.NumRooms);

            cmd.Parameters.AddWithValue("@NumBathrooms", model.NumBathrooms);

            cmd.Parameters.AddWithValue("@StatusID", model.StatusID);


            con.Open();

            return cmd.ExecuteNonQuery() > 0;
        }

        public bool Update(PropertyModel model)
        {
            using SqlConnection con =
            new SqlConnection(
            DbConnection.StringConnection);


            string query =
            @"
UPDATE Property

SET

AreaID=@AreaID,

PurposeID=@PurposeID,

Title=@Title,

Description=@Description,

Price=@Price,

AreaSize=@AreaSize,

NumRooms=@NumRooms,

NumBathrooms=@NumBathrooms,

StatusID=@StatusID

WHERE PropertyID=@PropertyID
";


            SqlCommand cmd =
            new SqlCommand(query, con);


            cmd.Parameters.AddWithValue(
            "@PropertyID",
            model.PropertyID);

            cmd.Parameters.AddWithValue(
            "@AreaID",
            model.AreaID);

            cmd.Parameters.AddWithValue(
            "@PurposeID",
            model.PurposeID);

            cmd.Parameters.AddWithValue(
            "@Title",
            model.Title);

            cmd.Parameters.AddWithValue(
            "@Description",
            model.Description);

            cmd.Parameters.AddWithValue(
            "@Price",
            model.Price);

            cmd.Parameters.AddWithValue(
            "@AreaSize",
            model.AreaSize);

            cmd.Parameters.AddWithValue(
            "@NumRooms",
            model.NumRooms);

            cmd.Parameters.AddWithValue(
            "@NumBathrooms",
            model.NumBathrooms);

            cmd.Parameters.AddWithValue(
            "@StatusID",
            model.StatusID);


            con.Open();


            return cmd.ExecuteNonQuery() > 0;
        }


        public bool Delete(int propertyID)
        {
            using SqlConnection con =
            new SqlConnection(
            DbConnection.StringConnection);


            string query =
            @"
DELETE FROM Property

WHERE PropertyID=@PropertyID
";


            SqlCommand cmd =
            new SqlCommand(query, con);


            cmd.Parameters.AddWithValue(
            "@PropertyID",
            propertyID);


            con.Open();


            return cmd.ExecuteNonQuery() > 0;
        }

        public static bool IsExist(int propertyID)
        {
            using SqlConnection con =
            new SqlConnection(DbConnection.StringConnection);

            string query =
            @"SELECT COUNT(*)
      FROM Property
      WHERE PropertyID = @PropertyID";

            SqlCommand cmd =
            new SqlCommand(query, con);

            cmd.Parameters.AddWithValue(
                "@PropertyID",
                propertyID);

            con.Open();

            return (int)cmd.ExecuteScalar() > 0;
        }
    }
}
