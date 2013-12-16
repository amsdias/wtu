//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WebApplication1.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    
    public partial class user
    {
        public user()
        {
            this.stories = new HashSet<story>();
        }
    
        public int Id { get; set; }
        
        [Display(Name = "Student ID")]
        public int studentId { get; set; }
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Country { get; set; }
        [Display(Name = "Home University")]
        public string HomeU { get; set; }
        [Display(Name = "Birthdate")]
        public System.DateTime Dob { get; set; }
        public string Course { get; set; }
        public string Avatar { get; set; }
    
        public virtual ICollection<story> stories { get; set; }
    }
}
