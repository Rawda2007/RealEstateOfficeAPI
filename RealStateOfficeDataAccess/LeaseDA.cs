using Microsoft.Data.SqlClient;
using RealStateOfficeModels.Transactions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateOfficeDataAccess
{
    public class LeaseDA
    {
       

        public static bool IsDealExists(int dealID)
        {
            using SqlConnection con =
                new SqlConnection(DbConnection.StringConnection);

            string query =
            @"SELECT 1
      FROM Deal
      WHERE DealID=@DealID";

            SqlCommand cmd =
                new SqlCommand(query, con);

            cmd.Parameters.AddWithValue("@DealID", dealID);

            con.Open();

            return cmd.ExecuteScalar() != null;
        }

        public static bool IsApproved(int dealID)
        {
            using SqlConnection con =
                new SqlConnection(DbConnection.StringConnection);

            string query =
            @"
SELECT 1
FROM Deal
WHERE DealID=@DealID
AND StatusID=2
";

            SqlCommand cmd =
                new SqlCommand(query, con);

            cmd.Parameters.AddWithValue("@DealID", dealID);

            con.Open();

            return cmd.ExecuteScalar() != null;
        }

        public static bool HasLease(int dealID)
        {
            using SqlConnection con =
                new SqlConnection(DbConnection.StringConnection);

            string query =
            @"
SELECT 1
FROM Lease
WHERE DealID=@DealID
";

            SqlCommand cmd =
                new SqlCommand(query, con);

            cmd.Parameters.AddWithValue("@DealID", dealID);

            con.Open();

            return cmd.ExecuteScalar() != null;
        }

        public static bool Add(LeaseModel lease)
        {
            using SqlConnection con =
                new SqlConnection(DbConnection.StringConnection);

            string query =
            @"
INSERT INTO Lease
(
DealID,
StartDate,
EndDate,
MonthlyRent
)

VALUES
(
@DealID,
@StartDate,
@EndDate,
@MonthlyRent
)
";

            SqlCommand cmd =
                new SqlCommand(query, con);

            cmd.Parameters.AddWithValue("@DealID", lease.DealID);
            cmd.Parameters.AddWithValue("@StartDate", lease.StartDate);
            cmd.Parameters.AddWithValue("@EndDate", lease.EndDate);
            cmd.Parameters.AddWithValue("@MonthlyRent", lease.MonthlyRent);

            con.Open();

            return cmd.ExecuteNonQuery() > 0;
        }


        public static List<LeaseDetailsModel> GetAll()
        {
            List<LeaseDetailsModel> leases = new();

            using SqlConnection con =
                new SqlConnection(DbConnection.StringConnection);

            string query =
            @"
SELECT

L.LeaseID,

P.Title,

U.Username AS UserName,

L.StartDate,

L.EndDate,

L.MonthlyRent

FROM Lease L

INNER JOIN Deal D
ON L.DealID = D.DealID

INNER JOIN Property P
ON D.PropertyID = P.PropertyID

INNER JOIN Client C
ON D.ClientID = C.ClientID

inner join Users U
on c.UserID=u.UserID

";

            SqlCommand cmd =
                new SqlCommand(query, con);

            con.Open();

            SqlDataReader reader =
                cmd.ExecuteReader();

            while (reader.Read())
            {
                leases.Add(new LeaseDetailsModel
                {
                    LeaseID = Convert.ToInt32(reader["LeaseID"]),
                    PropertyTitle = reader["Title"].ToString()!,
                    ClientName = reader["UserName"].ToString()!,
                    StartDate = Convert.ToDateTime(reader["StartDate"]),
                    EndDate = Convert.ToDateTime(reader["EndDate"]),
                    MonthlyRent = Convert.ToDecimal(reader["MonthlyRent"])
                });
            }

            return leases;
        }

        public static LeaseDetailsModel? GetByID(int leaseID)
        {
            using SqlConnection con =
                new SqlConnection(DbConnection.StringConnection);

            string query =
            @"
SELECT

L.LeaseID,

P.Title,

U.Username AS UserName,

L.StartDate,

L.EndDate,

L.MonthlyRent

FROM Lease L

INNER JOIN Deal D
ON L.DealID = D.DealID

INNER JOIN Property P
ON D.PropertyID = P.PropertyID

INNER JOIN Client C
ON D.ClientID = C.ClientID

inner join Users U
on c.UserID=u.UserID


WHERE L.LeaseID=@LeaseID
";

            SqlCommand cmd =
                new SqlCommand(query, con);

            cmd.Parameters.AddWithValue("@LeaseID", leaseID);

            con.Open();

            SqlDataReader reader =
                cmd.ExecuteReader();

            if (reader.Read())
            {
                return new LeaseDetailsModel
                {
                    LeaseID = Convert.ToInt32(reader["LeaseID"]),
                    PropertyTitle = reader["Title"].ToString()!,
                    ClientName = reader["UserName"].ToString()!,
                    StartDate = Convert.ToDateTime(reader["StartDate"]),
                    EndDate = Convert.ToDateTime(reader["EndDate"]),
                    MonthlyRent = Convert.ToDecimal(reader["MonthlyRent"])
                };
            }

            return null;
        }


        public static bool Update(LeaseModel lease)
        {
            using SqlConnection con =
                new SqlConnection(DbConnection.StringConnection);

            string query =
            @"
UPDATE Lease

SET

StartDate=@StartDate,

EndDate=@EndDate,

MonthlyRent=@MonthlyRent

WHERE LeaseID=@LeaseID
";

            SqlCommand cmd =
                new SqlCommand(query, con);

            cmd.Parameters.AddWithValue("@LeaseID", lease.LeaseID);
            cmd.Parameters.AddWithValue("@StartDate", lease.StartDate);
            cmd.Parameters.AddWithValue("@EndDate", lease.EndDate);
            cmd.Parameters.AddWithValue("@MonthlyRent", lease.MonthlyRent);

            con.Open();

            return cmd.ExecuteNonQuery() > 0;
        }


        public static bool Delete(int leaseID)
        {
            using SqlConnection con =
                new SqlConnection(DbConnection.StringConnection);

            string query =
            @"
DELETE FROM Lease

WHERE LeaseID=@LeaseID
";

            SqlCommand cmd =
                new SqlCommand(query, con);

            cmd.Parameters.AddWithValue("@LeaseID", leaseID);

            con.Open();

            return cmd.ExecuteNonQuery() > 0;
        }


        public static List<LeaseDetailsModel> GetByClientID(int clientID)
        {
            List<LeaseDetailsModel> leases = new();

            using SqlConnection con =
                new SqlConnection(DbConnection.StringConnection);

            string query =
            @"
        SELECT

        L.LeaseID,

        P.Title,

        L.StartDate,

        L.EndDate,

        L.MonthlyRent

        FROM Lease L

        INNER JOIN Deal D
        ON L.DealID = D.DealID

        INNER JOIN Property P
        ON D.PropertyID = P.PropertyID

        WHERE D.ClientID=@ClientID
        ";

            SqlCommand cmd =
                new SqlCommand(query, con);

            cmd.Parameters.AddWithValue(
                "@ClientID",
                clientID);

            con.Open();

            SqlDataReader reader =
                cmd.ExecuteReader();

            while (reader.Read())
            {
                leases.Add(new LeaseDetailsModel
                {
                    LeaseID =
                    Convert.ToInt32(reader["LeaseID"]),

                    PropertyTitle =
                    reader["Title"].ToString()!,

                    StartDate =
                    Convert.ToDateTime(reader["StartDate"]),

                    EndDate =
                    Convert.ToDateTime(reader["EndDate"]),

                    MonthlyRent =
                    Convert.ToDecimal(reader["MonthlyRent"])
                });
            }

            return leases;
        }


        public static LeaseDetailsModel? GetByIDForClient(
    int leaseID,
    int clientID)
        {
            using SqlConnection con =
                new SqlConnection(DbConnection.StringConnection);

            string query =
            @"
SELECT

L.LeaseID,

P.Title,

L.StartDate,

L.EndDate,

L.MonthlyRent

FROM Lease L

INNER JOIN Deal D
ON L.DealID = D.DealID

INNER JOIN Property P
ON D.PropertyID = P.PropertyID

WHERE

L.LeaseID=@LeaseID

AND

D.ClientID=@ClientID
";

            SqlCommand cmd =
                new SqlCommand(query, con);

            cmd.Parameters.AddWithValue("@LeaseID", leaseID);

            cmd.Parameters.AddWithValue("@ClientID", clientID);

            con.Open();

            SqlDataReader reader =
                cmd.ExecuteReader();

            if (reader.Read())
            {
                return new LeaseDetailsModel
                {
                    LeaseID =
                    Convert.ToInt32(reader["LeaseID"]),

                    PropertyTitle =
                    reader["Title"].ToString()!,

                    StartDate =
                    Convert.ToDateTime(reader["StartDate"]),

                    EndDate =
                    Convert.ToDateTime(reader["EndDate"]),

                    MonthlyRent =
                    Convert.ToDecimal(reader["MonthlyRent"])
                };
            }

            return null;
        }
    }
}
