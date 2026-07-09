namespace Core.DTOs
{
    public class RecipeCookingStepDTO
    {
        public int Id { get; set; }
        public int StepNumber { get; set; }
        public string Title { get; set; } = null!;
        public TimeSpan EstimatedDuration { get; set; }
        public string Instruction { get; set; } = null!;

    }

    public class CreateCookingStepDTO
    {
        public int RecipeId { get; set; }
        public int StepNumber { get; set; }
        public string Title { get; set; } = null!;
        public TimeSpan EstimatedDuration { get; set; }
        public string Instruction { get; set; } = null!;
    }

    public class UpdateCookingStepDTO
    {
        public int Id { get; set; }
        public int StepNumber { get; set; }
        public string Title { get; set; } = null!;
        public TimeSpan EstimatedDuration { get; set; }
        public string Instruction { get; set; } = null!;
    }
}
