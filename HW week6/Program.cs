using HW_week6;
public class Program
{
    static List<User> users = new List<User>();
    static List<Book> books = new List<Book>()
    {
        new Book() { Title = "Book1" },
        new Book() { Title = "Book2" },
        new Book() { Title = "Book3" }
    };

    static User currentUser = null;

    public static void Main(string[] args)
    {
        while (true)
        {
            Console.WriteLine("Do you want to (1) Register or (2) Login?");
            string choice = Console.ReadLine();

            if (choice == "1")
            {
                Register();
            }
            else if (choice == "2")
            {
                Login();
            }
        }
    }

    static void Register()
    {
        Console.WriteLine("Enter your username:");
        string username = Console.ReadLine();

        Console.WriteLine("Enter your password:");
        string password = Console.ReadLine();

        users.Add(new User() { Username = username, Password = password });
        Console.WriteLine("Registration successful!");

        currentUser = users.Find(u => u.Username == username);
        ShowMenu();
    }

    static void Login()
    {
        Console.WriteLine("Enter your username:");
        string username = Console.ReadLine();

        Console.WriteLine("Enter your password:");
        string password = Console.ReadLine();

        User user = users.Find(u => u.Username == username && u.Password == password);
        if (user != null)
        {
            Console.WriteLine("Login successful!");
            currentUser = user;

            ShowMenu();
        }
        else
        {
            Console.WriteLine("Invalid username or password.");
        }
    }

    static void ShowMenu()
    {
        while (true)
        {
            Console.WriteLine("Menu:");
            Console.WriteLine("1. Borrow a Book");
            Console.WriteLine("2. Return a Book");
            Console.WriteLine("3. Show Borrowed Book");
            Console.WriteLine("4. Exit");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    BorrowBook();
                    break;
                case "2":
                    ReturnBook();
                    break;
                case "3":
                    ShowBorrowedBook();
                    break;
                case "4":
                    currentUser = null;
                    return;
                default:
                    Console.WriteLine("Invalid option.");
                    break;
            }
        }
    }

    static void BorrowBook()
    {
        if (currentUser.HasBorrowedBook)
        {
            Console.WriteLine($"You already borrowed the book: {currentUser.BorrowedBookTitle}. Please return it first.");
            return;
        }

        Console.WriteLine("Available books:");
        foreach (var book in books)
        {
            if (!book.IsBorrowed)
            {
                Console.WriteLine(book.Title);
            }
        }

        Console.WriteLine("Enter the book title you want to borrow:");
        string title = Console.ReadLine();
        Book bookToBorrow = books.Find(b => b.Title == title && !b.IsBorrowed);

        if (bookToBorrow != null)
        {
            bookToBorrow.IsBorrowed = true;
            currentUser.HasBorrowedBook = true;
            currentUser.BorrowedBookTitle = bookToBorrow.Title;
            Console.WriteLine($"You borrowed the book: {bookToBorrow.Title}");
        }
        else
        {
            Console.WriteLine("Book not available or already borrowed.");
        }
    }

    static void ReturnBook()
    {
        if (!currentUser.HasBorrowedBook)
        {
            Console.WriteLine("You have not borrowed any book.");
            return;
        }

        Book borrowedBook = books.Find(b => b.Title == currentUser.BorrowedBookTitle);
        borrowedBook.IsBorrowed = false;
        Console.WriteLine($"You returned the book: {borrowedBook.Title}");

        currentUser.HasBorrowedBook = false;
        currentUser.BorrowedBookTitle = null;
    }

    static void ShowBorrowedBook()
    {
        if (currentUser.HasBorrowedBook)
        {
            Console.WriteLine($"You have borrowed: {currentUser.BorrowedBookTitle}");
        }
        else
        {
            Console.WriteLine("You have not borrowed any book.");
        }
    }
}


