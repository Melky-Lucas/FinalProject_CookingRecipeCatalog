using System;
using System.Collections.Generic;
using System.Text;

namespace Application.DTOs
{
    public class IngredientCategoryDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
    }

    public class CreateIngredientCategoryDTO
    {
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
    }

    public class UpdateIngredientCategoryDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
    }
}
