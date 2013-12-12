using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class User_List_ViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Country { get; set; }
        public string University { get; set; }
        public string Course { get; set; }
        
    }

    public class User_Details_ViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Country { get; set; }
        public string University { get; set; }
        public string Course { get; set; }
        public string Foto { get; set; }

    }
}