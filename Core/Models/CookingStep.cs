using Core.Base;

namespace Core.Models
{
    public class CookingStep : BaseEntity
    {
        public int RecipeId { get; set; }
        public int StepNumber { get; set; }
        public string Title { get; set; } = null!;
        public TimeSpan EstimatedDuration { get; set; }
        public string Instruction { get; set; } = null!;

        // Navigation property
        public Recipe Recipe { get; set; } = null!;
    }
}
