using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Model
{
    public class Recipe
    {
        public int Id { get; set; }
        public required string Title { get; set; }
        public required string Description { get; set; }
        public required string Ingredients { get; set; }
        public required string Instructions { get; set; }
        public string Tips { get; set; } = string.Empty;
    }
}
