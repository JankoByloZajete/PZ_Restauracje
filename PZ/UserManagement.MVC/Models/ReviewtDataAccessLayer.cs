using Microsoft.AspNetCore.Identity;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using UserManagement.MVC.Data;

namespace UserManagement.MVC.Models
{
    public class ReviewDataAccessLayer
    {
        string connectionString = "Server=(localdb)\\mssqllocaldb;Database=aspnet-UserManagement.MVC-AE2969B3-75AD-4EA0-A236-44AF57519291;Trusted_Connection=True;MultipleActiveResultSets=true";
        public IEnumerable<Review> GetAllReviews(string restaurantId)
        {
            List<Review> listReviews = new List<Review>();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("select * from Reviews RIGHT JOIN [Identity].[User] ON Reviews.UserId=[Identity].[User].Id WHERE Reviews.RestaurantId = '" + restaurantId+"'", con);
                
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    Review Review = new Review();

                    Review.Id = rdr["Id"].ToString();
                    Review.UserId = rdr["UserId"].ToString();
                    Review.Comment = rdr["Comment"].ToString();
                    Review.User = rdr["UserName"].ToString();

                    listReviews.Add(Review);
                }
                con.Close();
            }
            return listReviews;
        }

        
       
        public void AddReview(string RestaurantId, string userId, string Comment)
        {
            string _query = "INSERT INTO Reviews (Id, UserId, RestaurantId, Comment) values (@first, @second, @third, @fourth)";
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand comm = new SqlCommand())
                {
                    comm.Connection = conn;
                    comm.CommandType = CommandType.Text;
                    comm.CommandText = _query;
                    comm.Parameters.AddWithValue("@first", Guid.NewGuid().ToString());
                    comm.Parameters.AddWithValue("@second", userId);
                    comm.Parameters.AddWithValue("@third", RestaurantId);
                    comm.Parameters.AddWithValue("@fourth", Comment);
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
        public void DeleteReview(string Id)
        {
            string _query = "DELETE FROM Reviews where Id = @first";
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
