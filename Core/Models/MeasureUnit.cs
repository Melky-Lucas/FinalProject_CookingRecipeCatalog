using Core.Base;

namespace Core.Models
{
    public class MeasureUnit : BaseEntity
    {
        public required string Name { get; set; }
        public string Abbreviation { get; set; } = null!;

        // Navigation property
        public ICollection<Recipe_Ingredient> Recipe_Ingredients { get; set; } = [];
    }
}
