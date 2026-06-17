namespace Core.Models
{
    public class RecipeCategory
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public string Description { get; set; } = string.Empty;

        // Navigation property
        public ICollection<Recipe_Category> Recipe_Categories { get; set; } = [];
    }
}
