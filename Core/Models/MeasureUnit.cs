namespace Core.Models
{
    public class MeasureUnit
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Abbreviation { get; set; } = null!;

        // Navigation property
        public ICollection<Recipe_Ingredient> Recipe_Ingredients { get; set; } = [];
    }
}
