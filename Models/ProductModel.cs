using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ProdsCats.Models
{
    public class Product
    {
        [Key]
        public int Id {get; set;}
        [Required]
        public string Name {get; set;}
        [Required]
        public string Description {get; set;}
        [Required]
        public decimal Price {get; set;}
        public List<Association> Categories {get; set;}
        public DateTime CreatedAt {get;set;} = DateTime.Now;
        public DateTime UpdatedAt {get;set;} = DateTime.Now;
    }
}