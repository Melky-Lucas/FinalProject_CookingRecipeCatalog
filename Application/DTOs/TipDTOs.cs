namespace Application.DTOs
{
    public class RecipeTipDTO
    {
        public int Id { get; set; }
        public int Username { get; set; }
        public string Content { get; set; } = null!;
    }

    public class CreateTipDTO
    {
        public int RecipeId { get; set; }
        public int UserId { get; set; }
        public string Content { get; set; } = null!;
    }

    public class UpdateTipDTO
    {
        public int Id { get; set; }
        public string Content { get; set; } = null!;
    }


}
