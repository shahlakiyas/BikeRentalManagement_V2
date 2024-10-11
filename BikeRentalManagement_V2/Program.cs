using Microsoft.Data.SqlClient;

namespace BikeRentalManagement_V2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            setconnection();
            BikeRepository repository = new BikeRepository();

            bool exit = false;
            while (!exit)
            {
                Console.WriteLine("Bike Rental Management System:");
                Console.WriteLine("1.Add a bike");
                Console.WriteLine("2.View all bike");
                Console.WriteLine("3.Update a bike");
                Console.WriteLine("4.Delete a bike");
                Console.WriteLine("5.Exit");
                Console.WriteLine("Choose an option");

                string option = Console.ReadLine();

                switch (option)
                {
                    case "1":
                        Console.Clear();
                        addbike(repository);
                        break;
                    case "2":
                        Console.Clear();
                        repository.Readbike();
                        break;

                    case "3":
                        Console.Clear();
                        updatebike(repository);
                        break;
                    case "4":
                        Console.Clear();
                        removebike(repository);
                        break;
                    case "5":
                        exit = true;
                        break;
                    default:
                        Console.WriteLine("invalid option");
                        break;





                }
                static void addbike(BikeRepository repository)
                {
                    Console.WriteLine("enter bike id");
                    string bikeid = Console.ReadLine();

                    Console.WriteLine("enter bike brand");
                    string brand = Console.ReadLine();

                    Console.WriteLine("enter bike modal");
                    string modal = Console.ReadLine();

                    Console.WriteLine("enter rentalprice");
                    decimal rentalprice = decimal.Parse(Console.ReadLine());

                    //decimal rentalprice = ValidateBikeRentalPrice(manager);

                    repository.createbike(bikeid, brand, modal, rentalprice);


                }

                static void updatebike(BikeRepository repository)
                {
                    Console.WriteLine("enter bike id");
                    string bikeid = Console.ReadLine();

                    Console.WriteLine("enter bike brand");
                    string brand = Console.ReadLine();

                    Console.WriteLine("enter bike modal");
                    string modal = Console.ReadLine();

                    Console.WriteLine("enter rentalprice");
                    decimal rentalprice = decimal.Parse(Console.ReadLine());

                    repository.updatebikes(bikeid, brand, modal, rentalprice);


                }
                static void removebike(BikeRepository repository)
                {
                    Console.WriteLine("enter bike id");
                    string bikeid = Console.ReadLine();

                    repository.removebike(bikeid);
                    Console.WriteLine("bike removed success");
                }


            }

            static void setconnection()
            {
                string masterdbconnection = "Server=(localdb)\\MSSQLLocalDB;Database=master;Trusted_Connection=true;TrustServerCertificate=true";
                string bikedbconnection = "Server=(localdb)\\MSSQLLocalDB;Database=BikeDatabase;Trusted_Connection=true;TrustServerCertificate=true";

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
}
