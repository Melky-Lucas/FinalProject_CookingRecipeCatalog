namespace Core.DTOs
{
    public class ProfileDTO
    {
        public int Id { get; set; }
        public string Username { get; set; } = null!;
        public string Email { get; set; } = null!;
        public ICollection<RecipeDTO> Recipes { get; set; } = [];
    }

    public class UpdateUsernameDTO
    {
        public string NewUsername { get; set; } = null!;
    }

    public class ChangePasswordDTO
    {
        public string CurrentPassword { get; set; } = null!;
        public string NewPassword { get; set; } = null!;
    }

}
