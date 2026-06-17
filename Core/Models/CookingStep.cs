using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Models
{
    public class CookingStep
    {
        public int Id { get; set; }
        public int RecipeId { get; set; }
        public int StepNumber { get; set; }
        public string Title { get; set; } = null!;
        public TimeSpan EstimatedDuration { get; set; }
        public string Description { get; set; } = null!;
        public Recipe Recipe { get; set; } = null!;
    }
}
