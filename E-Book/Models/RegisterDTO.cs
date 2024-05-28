namespace E_Book.Models
{
    public class RegisterDTO
    {
        public required string FirstName { get; set; }
        public required string Email { get; set; }

        public required string Password { get; set; }

        public UserRole Role { get; set; } = UserRole.User;
    }

    public enum UserRole
    {
        Admin,
        User,
        Guest
    }
}
