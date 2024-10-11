
using BikeRentalManagement_V2;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BikeRentalManagement_V2
{
    internal class BikeRepository
    {
        private string bikedbconnection = "Server=(localdb)\\MSSQLLocalDb;Database=BikeDatabase;Trusted_Connection=true;TrustServerCertificate=true";

        public void createbike(string brand, string modal, decimal rentprice)
        {
            string insertquary = @"INSERT INTO bike(BRAND,MODAL,RENTPRICE) VALUES(@brand,@modal,@rentprice);";

            try
            {
                using (SqlConnection connection = new SqlConnection(bikedbconnection))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(insertquary, connection))
                    {
                        command.Parameters.AddWithValue(@"brand", brand);
                        command.Parameters.AddWithValue(@"modal", modal);
                        command.Parameters.AddWithValue(@"rentprice", rentprice);
                        command.ExecuteNonQuery();
                        Console.WriteLine("car added");

                    }

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("error");
            }
        }
        public void updatebike(int id, string brand, string modal, decimal rentprice)
        {
            string updatequary = @"UPDATE bike SET BRAND=@brand,MODAL=@modal,RENTPRICE=@rentprice WHERE Bikeid=@id;";

            try
            {
                using (SqlConnection connection = new SqlConnection(bikedbconnection))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(updatequary, connection))
                    {
                        command.Parameters.AddWithValue(@"id", id);

                        command.Parameters.AddWithValue(@"brand", brand);
                        command.Parameters.AddWithValue(@"modal", modal);
                        command.Parameters.AddWithValue(@"rentprice", rentprice);
                        command.ExecuteNonQuery();
                        Console.WriteLine("bike updated");

                    }

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("error");
            }
        }
        public void removebike(int id)
        {
            string deletequary = @"DELETE FROM bike WHERE Bikeid=@id;";
            try
            {
                using (SqlConnection connection = new SqlConnection(bikedbconnection))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(deletequary, connection))
                    {
                        command.Parameters.AddWithValue(@"id", id);
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("error");
            }
        }
        public List<Bike> Readbike()
        {
            var bikes = new List<Bike>();
            string getquary = @"SELECT * FROM bike";
            using (SqlConnection connection = new SqlConnection(bikedbconnection))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(getquary, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        Console.WriteLine("bike list");
                        while (reader.Read())
                        {
                            int bikeid = reader.GetInt32(0);
                            string brand = reader.GetString(1);
                            string modal = reader.GetString(2);
                            decimal rentprice = reader.GetDecimal(3);
                            var bike = new Bike(bikeid, brand, modal, rentprice);
                            bikes.Add(bike);


                        }

                    }

                }

            }
            return bikes;


        }
        public Bike getbyid(int id)
        {
            Bike bike = null;
            string getidquary = @"SELECT * FROM bike WHERE bikeid=@id";
            using (SqlConnection connection = new SqlConnection(bikedbconnection))
            {
                connection.Open();
                using (SqlCommand cmd = new SqlCommand(getidquary, connection))
                {
                    cmd.Parameters.AddWithValue(@"id", id);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            int bikeid = reader.GetInt32(0);
                            string brand = reader.GetString(1);
                            string modal = reader.GetString(2);
                            decimal rentprice = reader.GetDecimal(3);
                            bike = new Bike(bikeid, brand, modal, rentprice);

                        }

                    }


                }
                return bike;
            }


        }
    }
}



