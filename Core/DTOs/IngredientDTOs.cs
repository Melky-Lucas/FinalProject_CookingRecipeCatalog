using System;
using System.Collections.Generic;
using System.Text;

namespace Core.DTOs
{
    public class IngredientDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string ImageUrl { get; set; } = null!;
        public string CategoryName { get; set; } = null!;
    }
}
