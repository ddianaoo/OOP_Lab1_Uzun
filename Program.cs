using ConsoleApp1;
Console.OutputEncoding = System.Text.Encoding.UTF8;

Console.Write("Введіть максимальну кількість книг (N > 0): ");
int N;
while (!int.TryParse(Console.ReadLine(), out N) || N <= 0)
{
    Console.Write("Неправильне число. Введіть ще раз (N > 0): ");
}

List<Book> books = new List<Book>();

while (true)
{
    Console.WriteLine("\n=== МЕНЮ ===");
    Console.WriteLine("1 - Додати об'єкт");
    Console.WriteLine("2 - Переглянути всі обʼєкти");
    Console.WriteLine("3 - Знайти обʼєкт");
    Console.WriteLine("4 - Продемонструвати поведінку");
    Console.WriteLine("5 - Видалити обʼєкт");
    Console.WriteLine("0 - Вийти з програми");
    Console.Write("Виберіть опцію: ");

    string choice = Console.ReadLine();
    Console.WriteLine();

    switch (choice)
    {
        case "1":
            if (books.Count >= N)
            {
                Console.WriteLine("Досягнуто максимальної кількості книг.");
                break;
            }

            Book newBook = CreateBookFromConsole();
            books.Add(newBook);
            Console.WriteLine("Книгу успішно додано!");
            break;

        case "2":
            Console.WriteLine("=== Усі книги ===");
            printBooks(books);
            break;

        case "3":
            string choiceSearch;
            Console.WriteLine("Оберіть характеристику для пошуку:");
            do
            {
                Console.WriteLine("1 - Назва");
                Console.WriteLine("2 - Автор");
                Console.Write("Ваш вибір: ");
                choiceSearch = Console.ReadLine();
                if (choiceSearch != "1" && choiceSearch != "2")
                {
                    Console.WriteLine("Неправильний вибір.");
                }
            } while (choiceSearch != "1" && choiceSearch != "2");

            Console.Write("Введіть значення для пошуку: ");
            string searchValue = Console.ReadLine();

            List<Book> foundBooks = new List<Book>();

            if (choiceSearch == "1")
            {
                foundBooks = books
                    .Where(b => b.title.Equals(searchValue, StringComparison.OrdinalIgnoreCase))
                    .ToList();
            }
            else if (choiceSearch == "2")
            {
                foundBooks = books
                    .Where(b => b.author.Equals(searchValue, StringComparison.OrdinalIgnoreCase))
                    .ToList();
            }

            Console.WriteLine("\n=== Результати пошуку ===");
            printBooks(foundBooks);
            break;

        case "4":
            Console.Write("Введіть назву книги для зміни: ");
            string modifyTitle = Console.ReadLine();
            var bookToModify = books.FirstOrDefault(b => b.title.Equals(modifyTitle, StringComparison.OrdinalIgnoreCase));
            if (bookToModify != null)
            {
                Console.WriteLine("1 - Змінити ціну");
                Console.WriteLine("2 - Збільшити кількість");
                Console.WriteLine("3 - Зменшити кількість");
                Console.Write("Виберіть опцію: ");
                string action = Console.ReadLine();

                switch (action)
                {
                    case "1":
                        Console.Write("Введіть нову ціну: ");
                        if (decimal.TryParse(Console.ReadLine(), out decimal newPrice))
                            bookToModify.ChangePrice(newPrice);
                        else
                            Console.WriteLine("Неправильна ціна.");
                        break;
                    case "2":
                        bookToModify.IncreaseQuantity();
                        break;
                    case "3":
                        bookToModify.DecreaseQuantity();
                        break;
                    default:
                        Console.WriteLine("Неправильна опція.");
                        break;
                }
            }
            else
                Console.WriteLine("Книгу не знайдено.");
            break;

        case "5":
            Console.Write("Введіть назву для видалення: ");
            string deleteTitle = Console.ReadLine();
            var bookToDelete = books.FirstOrDefault(b => b.title.Equals(deleteTitle, StringComparison.OrdinalIgnoreCase));
            if (bookToDelete != null)
            {
                books.Remove(bookToDelete);
                Console.WriteLine("Книгу видалено.");
            }
            else
                Console.WriteLine("Книгу не знайдено.");
            break;

        case "0":
            Console.WriteLine("Вихід з програми...");
            return;

        default:
            Console.WriteLine("Неправильний вибір.");
            break;
    }
}


Book CreateBookFromConsole()
{
    string title;
    do
    {
        Console.Write("Введіть назву книги: ");
        title = Console.ReadLine();
    } while (string.IsNullOrWhiteSpace(title));

    string author;
    do
    {
        Console.Write("Введіть автора: ");
        author = Console.ReadLine();
    } while (string.IsNullOrWhiteSpace(author));

    Console.Write("Введіть дату створення (yyyy-mm-dd): ");
    DateOnly creationDate;
    while (!DateOnly.TryParse(Console.ReadLine(), out creationDate) || creationDate > DateOnly.FromDateTime(DateTime.Today))
    {
        Console.Write("Неправильна дата. Введіть ще раз (yyyy-mm-dd, не пізніше сьогодні): ");
    }

    Console.Write("Введіть кількість сторінок: ");
    int pages;
    while (!int.TryParse(Console.ReadLine(), out pages) || pages <= 0)
    {
        Console.Write("Неправильне число. Введіть ще раз: ");
    }

    Console.Write("Введіть ціну: ");
    decimal price;
    while (!decimal.TryParse(Console.ReadLine(), out price) || price < 0)
    {
        Console.Write("Неправильна ціна. Введіть ще раз: ");
    }

    Console.Write("Введіть кількість: ");
    int quantity;
    while (!int.TryParse(Console.ReadLine(), out quantity) || quantity < 0)
    {
        Console.Write("Неправильне число. Введіть ще раз: ");
    }

    Console.WriteLine("Оберіть жанр:");
    foreach (var g in Enum.GetValues(typeof(Genre)))
    {
        Console.WriteLine($"{(int)g} - {g}");
    }
    int genreNumber;
    while (!int.TryParse(Console.ReadLine(), out genreNumber) || !Enum.IsDefined(typeof(Genre), genreNumber))
    {
        Console.Write("Неправильний вибір. Спробуйте ще раз: ");
    }
    Genre genre = (Genre)genreNumber;

    return new Book(title, author, creationDate, pages, price, quantity, genre);
}

void printBooks(List<Book> books)
{
    if (books == null || books.Count == 0)
    {
        Console.WriteLine("<cписок порожній>");
        return;
    }

    Console.WriteLine("№ | Назва                | Автор               | Дата       | Стор. |    Ціна   | К-сть | Жанр       | Інші видання");
    Console.WriteLine("--------------------------------------------------------------------------------------------------------------------");

    int index = 1;
    foreach (var book in books)
    {
        Console.WriteLine($"{index,2} | {book}");
        index++;
    }

    Console.WriteLine("--------------------------------------------------------------------------------------------------------------------");
}