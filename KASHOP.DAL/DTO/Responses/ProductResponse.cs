using KASHOP.DAL.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace KASHOP.DAL.DTO.Responses
{
    public class ProductResponse
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public string ImageUrl { get; set; } = null!;
        public String Rate { get; set; } = null!;
        public decimal Discount { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; } = null!;

       
    }
}
