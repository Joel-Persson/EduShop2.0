//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace EduShop_Database
{
    using System;
    using System.Collections.Generic;
    
    public partial class Review
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public double Rating { get; set; }
        public System.DateTime DateAdded { get; set; }
        public int ProductId { get; set; }
    
        public virtual Product Product { get; set; }
    }
}
