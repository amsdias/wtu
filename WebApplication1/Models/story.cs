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
    
    public partial class story
    {
        public int idstory { get; set; }
        public int user_Id { get; set; }
        public string description { get; set; }
        public string text { get; set; }
        public byte[] image1 { get; set; }
        public byte[] image2 { get; set; }
        public byte[] image3 { get; set; }
        public int rating { get; set; }
    
        public virtual user user { get; set; }
    }
}
