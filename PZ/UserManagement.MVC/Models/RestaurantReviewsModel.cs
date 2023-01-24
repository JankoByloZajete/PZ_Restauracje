using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UserManagement.MVC.Models
{
    public class RestaurantReviewsModel
    {
        public Restaurant Restaurant { get; set; }
        public List<Review> Reviews { get; set; }

        public RestaurantReviewsModel()
        {
            this.Restaurant = new Restaurant();
            this.Reviews = new List<Review>();
        }

    }
}
