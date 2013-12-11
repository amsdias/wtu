using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class User_List_ViewModel
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Pais { get; set; }
        public string Univ { get; set; }
        public string Curso { get; set; }
        
    }

    public class User_Details_ViewModel
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Pais { get; set; }
        public string Univ { get; set; }
        public string Curso { get; set; }
        public string Foto { get; set; }

    }
}