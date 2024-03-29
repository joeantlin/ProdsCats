using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ProdsCats.Models
{
    public class Association
    {
        [Key]
        public int Id {get; set;}
        [Required]
        public int ProductId {get; set;}
        public int CategoryId {get; set;}
        public Product Product {get; set;}
        public Category Category {get; set;}
        public DateTime CreatedAt {get;set;} = DateTime.Now;
        public DateTime UpdatedAt {get;set;} = DateTime.Now;
    }
}