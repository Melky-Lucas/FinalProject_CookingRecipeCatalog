using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Models
{
    public class MeasureUnit
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Abbreviation { get; set; } = null!;
        public ICollection<Recipe_Ingredient> Recipe_Ingredients { get; set; } = [];
    }
}
