using ConsoleApp1;
Console.OutputEncoding = System.Text.Encoding.UTF8;


Console.Write("Enter maximum number of books (N > 0): ");
int N;
while (!int.TryParse(Console.ReadLine(), out N) || N <= 0)
{
    Console.Write("Invalid number. Enter again (N > 0): ");
}

List<Book> books = new List<Book>();

while (true)
{
    Console.WriteLine("\n=== MENU ===");
    Console.WriteLine("1 - Додати об'єкт");
    Console.WriteLine("2 - Переглянути всі обʼєкти");
    Console.WriteLine("3 - Знайти обʼєкт");
    Console.WriteLine("4 - Продемонструвати поведінку");
    Console.WriteLine("5 - Видалити обʼєкт");
    Console.WriteLine("0 - Вийти з програми");
    Console.Write("Choose option: ");

    string choice = Console.ReadLine();
    Console.WriteLine();

    switch (choice)
    {
        case "1":
            if (books.Count >= N)
            {
                Console.WriteLine("Maximum number of books reached.");
                break;
            }

            Book newBook = CreateBookFromConsole();
            books.Add(newBook);
            Console.WriteLine("Book added successfully!");
            break;

        case "2":
            if (books.Count == 0)
                Console.WriteLine("No books yet.");
            else
            {
                Console.WriteLine("=== All Books ===");
                foreach (var book in books)
                {
                    Console.WriteLine(book);
                    Console.WriteLine("------------------------");
                }
            }
            break;

        case "3":
            Console.Write("Enter title to search: ");
            string searchTitle = Console.ReadLine();
            var found = books.FirstOrDefault(b => b.title.Equals(searchTitle, StringComparison.OrdinalIgnoreCase));
            if (found != null)
                Console.WriteLine(found);
            else
                Console.WriteLine("Book not found.");
            break;

        case "4":
            Console.Write("Enter title to modify: ");
            string modifyTitle = Console.ReadLine();
            var bookToModify = books.FirstOrDefault(b => b.title.Equals(modifyTitle, StringComparison.OrdinalIgnoreCase));
            if (bookToModify != null)
            {
                Console.WriteLine("1 - Change price");
                Console.WriteLine("2 - Increase quantity");
                Console.WriteLine("3 - Decrease quantity");
                Console.Write("Choose option: ");
                string action = Console.ReadLine();

                switch (action)
                {
                    case "1":
                        Console.Write("Enter new price: ");
                        if (decimal.TryParse(Console.ReadLine(), out decimal newPrice))
                            bookToModify.ChangePrice(newPrice);
                        else
                            Console.WriteLine("Invalid price.");
                        break;
                    case "2":
                        bookToModify.IncreaseQuantity();
                        break;
                    case "3":
                        bookToModify.DecreaseQuantity();
                        break;
                    default:
                        Console.WriteLine("Invalid option.");
                        break;
                }
            }
            else
                Console.WriteLine("Book not found.");
            break;

        case "5":
            Console.Write("Enter title to delete: ");
            string deleteTitle = Console.ReadLine();
            var bookToDelete = books.FirstOrDefault(b => b.title.Equals(deleteTitle, StringComparison.OrdinalIgnoreCase));
            if (bookToDelete != null)
            {
                books.Remove(bookToDelete);
                Console.WriteLine("Book deleted.");
            }
            else
                Console.WriteLine("Book not found.");
            break;

        case "0":
            Console.WriteLine("Exiting program...");
            return;

        default:
            Console.WriteLine("Invalid choice.");
            break;
    }
}


Book CreateBookFromConsole()
{
    string title;
    do
    {
        Console.Write("Enter book title: ");
        title = Console.ReadLine();
    } while (string.IsNullOrWhiteSpace(title));

    Console.Write("Enter description: ");
    string description = Console.ReadLine();

    string author;
    do
    {
        Console.Write("Enter author: ");
        author = Console.ReadLine();
    } while (string.IsNullOrWhiteSpace(author));

    Console.Write("Enter creation date (yyyy-mm-dd): ");
    DateOnly creationDate;
    while (!DateOnly.TryParse(Console.ReadLine(), out creationDate) || creationDate > DateOnly.FromDateTime(DateTime.Today))
    {
        Console.Write("Invalid date. Enter again (yyyy-mm-dd, not later than today): ");
    }

    Console.Write("Enter number of pages: ");
    int pages;
    while (!int.TryParse(Console.ReadLine(), out pages) || pages <= 0)
    {
        Console.Write("Invalid number. Enter again: ");
    }

    Console.Write("Enter price: ");
    decimal price;
    while (!decimal.TryParse(Console.ReadLine(), out price) || price < 0)
    {
        Console.Write("Invalid price. Enter again: ");
    }

    Console.Write("Enter quantity: ");
    int quantity;
    while (!int.TryParse(Console.ReadLine(), out quantity) || quantity < 0)
    {
        Console.Write("Invalid number. Enter again: ");
    }

    Console.WriteLine("Choose genre:");
    foreach (var g in Enum.GetValues(typeof(Genre)))
    {
        Console.WriteLine($"{(int)g} - {g}");
    }
    int genreNumber;
    while (!int.TryParse(Console.ReadLine(), out genreNumber) || !Enum.IsDefined(typeof(Genre), genreNumber))
    {
        Console.Write("Invalid choice. Try again: ");
    }
    Genre genre = (Genre)genreNumber;

    return new Book(title, description, author, creationDate, pages, price, quantity, genre);
}
