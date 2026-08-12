namespace Application.DTOs
{
    public class RecipeCategoryDTO
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public string Description { get; set; } = null!;
    }

    public class CreateRecipeCategoryDTO
    {
        public required string Name { get; set; }
        public string Description { get; set; } = null!;
    }

    public class UpdateRecipeCategoryDTO
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public string Description { get; set; } = null!;
    }
}
