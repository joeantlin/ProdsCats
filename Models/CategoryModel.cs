using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ProdsCats.Models
{
    public class Category
    {
        [Key]
        public int Id {get; set;}
        [Required]
        public string Name {get; set;}
        public List<Association> Products {get; set;}
        public DateTime CreatedAt {get;set;} = DateTime.Now;
        public DateTime UpdatedAt {get;set;} = DateTime.Now;
    }
}