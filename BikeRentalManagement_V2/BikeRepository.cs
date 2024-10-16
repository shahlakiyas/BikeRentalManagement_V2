﻿
using BikeRentalManagement_V2;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BikeRentalManagement_V2
{
    internal class BikeRepository
    {
        private string bikedbconnection = "Server=(localdb)\\MSSQLLocalDB;Database=BikeDatabase;Trusted_Connection=true;TrustServerCertificate=true";

        public void createbike(string BikeId,string Brand,string Modal,decimal Rentalprice)
        {
            string insertquary = @"
                              INSERT INTO bike(Bikeid,BRAND,MODAL,RENTPRICE)
                                VALUES('BIKE_001','Honda','CB-Shine',5.00)";

            try
            {
                using (SqlConnection connection = new SqlConnection(bikedbconnection))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(insertquary, connection))
                    {
                        command.Parameters.AddWithValue(@"Bikeid", BikeId);
                        command.Parameters.AddWithValue(@"brand", Brand);
                        command.Parameters.AddWithValue(@"modal", Modal);
                        command.Parameters.AddWithValue(@"rentprice", Rentalprice);
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
        public void updatebikes(string BikeId, string Brand, string Modal, decimal Rentalprice)
        {
            string updatequary = @"UPDATE bike SET Bikeid=@id,BRAND=@brand,MODAL=@modal,RENTPRICE=@Rentalprice WHERE @BikeId =BikeId";

            try
            {
                using (SqlConnection connection = new SqlConnection(bikedbconnection))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(updatequary, connection))
                    {
                        command.Parameters.AddWithValue(@"BikeId", BikeId);
                        command.Parameters.AddWithValue(@"Brand", Brand);
                        command.Parameters.AddWithValue(@"Modal", Modal);
                        command.Parameters.AddWithValue(@"Rentalprice", Rentalprice);
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
        public void removebike(string id)
        {
            string deletequary = @"DELETE FROM bike WHERE Bikeid=@id;";
            try
            {
                using (SqlConnection connection = new SqlConnection(bikedbconnection))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(deletequary, connection))
                    {
                        command.Parameters.AddWithValue(@"Bikeid", id);
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
                            string bikeid = reader.GetString(0);
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
                            string bikeid = reader.GetString(0);
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



