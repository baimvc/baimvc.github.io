using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogWeb.UI.ViewModels
{
    public class UserViewModel
    {
        public string Email { get; set; }
        public IFormFile Avatar { get; set; }
        public string Profile { get; set; }
        public string Url { get; set; }
        public string GitHub { get; set; }
    }
}
