using Microsoft.AspNetCore.Identity;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace UserManagement.MVC.Models
{
    public class RestaurantDataAccessLayer
    {
        string connectionString = "Server=(localdb)\\mssqllocaldb;Database=aspnet-UserManagement.MVC-AE2969B3-75AD-4EA0-A236-44AF57519291;Trusted_Connection=True;MultipleActiveResultSets=true";
        public IEnumerable<Restaurant> GetAllRestaurants()
        {
            List<Restaurant> listRestaurant = new List<Restaurant>();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("select * from Restaurants", con);
                
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    Restaurant Restaurant = new Restaurant();
                   
                    Restaurant.Id = rdr["Id"].ToString();
                    Restaurant.Name = rdr["Name"].ToString();
                    Restaurant.Address = rdr["Address"].ToString();

                    listRestaurant.Add(Restaurant);
                }
                con.Close();
            }
            return listRestaurant;
        }
        public void AddRestaurant(string Name, string Address)
        {
            string _query = "INSERT INTO Restaurants (Id, Name, Address) values (@first, @second, @third)";
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand comm = new SqlCommand())
                {
                    comm.Connection = conn;
                    comm.CommandType = CommandType.Text;
                    comm.CommandText = _query;

                    comm.Parameters.AddWithValue("@first", Guid.NewGuid().ToString());
                    comm.Parameters.AddWithValue("@second", Name);
                    comm.Parameters.AddWithValue("@third", Address);

                    try
                    {
                        conn.Open();
                        comm.ExecuteNonQuery();
                    }
                    catch (SqlException ex)
                    {
                        throw new Exception(ex.ToString());
                    }
                }
            }
        }
        public void DeleteRestaurant(string Id)
        {
            string _query = "DELETE FROM Restaurants where Id = @first";
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand comm = new SqlCommand())
                {
                    comm.Connection = conn;
                    comm.CommandType = CommandType.Text;
                    comm.CommandText = _query;

                    comm.Parameters.AddWithValue("@first", Id);

                    try
                    {
                        conn.Open();
                        comm.ExecuteNonQuery();
                    }
                    catch (SqlException ex)
                    {
                        throw new Exception(ex.ToString());
                    }
                }
            }
        }
        public Restaurant GetRestaurant(string Id)
        {
            string _query = "SELECT * FROM Restaurants where Id = '" + Id+"'";
            Restaurant Restaurant = new Restaurant();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(_query, conn);

                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    Restaurant.Id = rdr["Id"].ToString();
                    Restaurant.Name = rdr["Name"].ToString();
                    Restaurant.Address = rdr["Address"].ToString();
                }
                conn.Close();
            }
            return Restaurant;
        }
    }
 
}
