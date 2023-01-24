using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UserManagement.MVC.Models
{
    public class Review
    {
        public string Id { get; set; }
        public string UserId { get; set; }
        public string RestaurantId { get; set; }
        public string Comment { get; set; }
        public string? User { get; set; }

    }
}
