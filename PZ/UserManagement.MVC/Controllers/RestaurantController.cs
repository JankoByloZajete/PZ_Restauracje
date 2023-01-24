using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using UserManagement.MVC.Models;

namespace UserManagement.MVC.Controllers
{
    public class RestaurantController : Controller
    {
        private readonly ILogger<RestaurantController> _logger;
        RestaurantDataAccessLayer RestaurantContext = new RestaurantDataAccessLayer();
        ReviewDataAccessLayer ReviewContext = new ReviewDataAccessLayer();

        public RestaurantController(ILogger<RestaurantController> logger)
        {
            _logger = logger;
        }

        

        public IActionResult Index()
        {
            List<Restaurant> restaurants = new List<Restaurant>();
            restaurants = RestaurantContext.GetAllRestaurants().ToList();
            return View(restaurants);
        }

        [HttpPost]
        public async Task<IActionResult> AddRestaurant(string restaurantName, string restaurantAddress)
        {
            if (restaurantName != null)
            {
                RestaurantContext.AddRestaurant(restaurantName, restaurantAddress);
            }
            return RedirectToAction("Index");
        }

        
        public async Task<IActionResult> DeleteRestaurant(string Id)
        {
            if (Id != null)
            {
                RestaurantContext.DeleteRestaurant(Id);
            }
            return RedirectToAction("Index");
        }

        public IActionResult ViewRestaurant(string Id)
        {
            Restaurant restaurant = new Restaurant();
            restaurant = RestaurantContext.GetRestaurant(Id);
            List<Review> Reviews = new List<Review>();
            Reviews = ReviewContext.GetAllReviews(Id).ToList();
            RestaurantReviewsModel restaurantReviewsModel = new RestaurantReviewsModel();
            restaurantReviewsModel.Restaurant = restaurant;
            restaurantReviewsModel.Reviews = Reviews;
            return View(restaurantReviewsModel);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
