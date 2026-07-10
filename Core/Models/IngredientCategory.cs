using Core.Base;

namespace Core.Models
{
    public class IngredientCategory : BaseEntity
    {
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;

        // Navigation property
        public ICollection<Ingredient> Ingredients { get; set; } = [];
    }
}
