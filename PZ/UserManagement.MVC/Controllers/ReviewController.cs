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
    public class ReviewController : Controller
    {
        private readonly ILogger<ReviewController> _logger;
        ReviewDataAccessLayer ReviewContext = new ReviewDataAccessLayer();

        public ReviewController(ILogger<ReviewController> logger)
        {
            _logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> AddReview(string restaurantId, string comment, string userId)
        {
            if (restaurantId != null && comment != null)
            {
                ReviewContext.AddReview(restaurantId, userId, comment );
            }
            return RedirectToAction("ViewRestaurant", "Restaurant", new {Id=restaurantId});
        }

        
        public async Task<IActionResult> DeleteReview(string Id, string restaurant)
        {
            if (Id != null)
            {
                ReviewContext.DeleteReview(Id);
            }
            return RedirectToAction("ViewRestaurant", "Restaurant", new { Id = restaurant });
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
