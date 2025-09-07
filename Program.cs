using ConsoleApp1;

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


Book myBook = new Book(title, description, author, creationDate, pages, price, quantity, genre);

Console.WriteLine();
Console.WriteLine("Book was created successfully!");
Console.WriteLine(myBook);
