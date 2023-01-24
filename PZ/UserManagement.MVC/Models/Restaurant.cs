using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UserManagement.MVC.Models
{
    public class Restaurant
    {
        public string Id { get; internal set; }
        public string Name { get; set; }
        public string? Address { get; set; }
        public byte[]? Image { get; set; }
    }
}
