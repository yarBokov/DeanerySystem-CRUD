namespace DeanerySystem.Models.Authentication
{
    public record struct LoggedInAdmin(int UserId, string DisplayName)
    {
        public readonly bool IsEmpty => UserId == 0;
    }
}
