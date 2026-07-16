namespace Application.DTOs
{
    public class Recipe_IngredientDTO
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        public bool IsOptional { get; set; }
        public IngredientDTO Ingredient { get; set; } = null!;
        public MeasureUnitDTO Unit { get; set; } = null!;
    }

    public class CreateRecipe_IngredientDTO
    {
        public int IngredientId { get; set; }
        public int Quantity { get; set; }
        public int UnitId { get; set; }
        public bool IsOptional { get; set; }
    }

    public class UpdateRecipe_IngredientDTO
    {
        public int Id { get; set; }
        public int RecipeId { get; set; }
        public int IngredientId { get; set; }
        public int Quantity { get; set; }
        public int UnitId { get; set; }
        public bool IsOptional { get; set; }
    }
}
