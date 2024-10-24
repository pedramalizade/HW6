public class User
{
    public string Username { get; set; }
    public string Password { get; set; }
    public bool HasBorrowedBook { get; set; } = false;
    public string BorrowedBookTitle { get; set; }
}
