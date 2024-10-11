using Microsoft.Data.SqlClient;

namespace BikeRentalManagement_V2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            setconnection();



        }

        static void setconnection()
        {
            string masterdbconnection = "Server=(localdb)\\MSSQLLocalDb;Database=master;Trusted_Connection=true;TrustServerCertificate=true";
            string bikedbconnection = "Server=(localdb)\\MSSQLLocalDb;Database=BikeDatabase;Trusted_Connection=true;TrustServerCertificate=true";

            string dbquery = @"
                                IF NOT EXISTS (SELECT * FROM sys.databases WHERE name='BikeDatabase')
                                    CREATE DATABASE BikeDatabase;";

            string tablequery = @"
                                IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='bike' AND xtype='U')
                                    CREATE TABLE bike(
                                        Bikeid NVARCHAR(20) PRIMARY KEY,
                                        BRAND NVARCHAR(50) NOT NULL,
                                        MODAL NVARCHAR(50) NOT NULL,
                                        RENTPRICE DECIMAL(10,2) NOT NULL
                                        );";

            string insertquery = @"
                              INSERT INTO bike(Bikeid,BRAND,MODAL,RENTPRICE)
                                VALUES('BIKE_001','Honda','CB-Shine',5.00)";

            using (SqlConnection connection = new SqlConnection(masterdbconnection))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(dbquery, connection))
                {
                    command.ExecuteNonQuery();
                    Console.WriteLine("db created");
                }
                using (SqlConnection conn = new SqlConnection(bikedbconnection))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(tablequery, conn))
                    {
                        cmd.ExecuteNonQuery();
                        Console.WriteLine("table created");

                    }
                    using (SqlCommand cmd = new SqlCommand(insertquery, conn))
                    {
                        cmd.ExecuteNonQuery();
                        Console.WriteLine("field created");

                    }

                }
                Console.ReadLine();
            }
        }
    }
}
