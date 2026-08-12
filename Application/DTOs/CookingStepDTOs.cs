namespace Application.DTOs
{
    public class RecipeCookingStepDTO
    {
        public int Id { get; set; }
        public int StepNumber { get; set; }
        public string Title { get; set; } = null!;
        public TimeSpan EstimatedDuration { get; set; }
        public string Instruction { get; set; } = null!;

    }

    public class CreateRecipeStepDTO
    {
        public int StepNumber { get; set; }
        public string Title { get; set; } = null!;
        public TimeSpan EstimatedDuration { get; set; }
        public string Instruction { get; set; } = null!;
    }

    public class UpdateRecipeStepDTO
    {
        public int Id { get; set; }
        public int StepNumber { get; set; }
        public string Title { get; set; } = null!;
        public TimeSpan EstimatedDuration { get; set; }
        public string Instruction { get; set; } = null!;
    }
}
