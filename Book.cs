using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public class Book
    {
        private string title;
        private string description;
        private string author;
        private DateOnly creationDate;
        private int countOfPages;
        private decimal price;
        private int quantity;
        private List<DateOnly> datesOfPublication;

        public Genre genre;

        public Book(string title, string description, string author, DateOnly creationDate, int countOfPages, decimal price, int quantity, Genre genre)
        {
            this.title = title;
            this.description = description;
            this.author = author;
            this.creationDate = creationDate;
            this.countOfPages = countOfPages;
            this.price = price;
            this.quantity = quantity;
            this.genre = genre;
            this.datesOfPublication = new List<DateOnly> { creationDate };
        }

        public void AddPublicationDate(DateOnly newDate)
        {
            datesOfPublication.Add(newDate);
        }

        public void ChangePrice(decimal newPrice)
        {
            if (newPrice >= 0)
            {
                price = newPrice;
                Console.WriteLine($"Price of \"{title}\" updated to {price} USD.");
            }
            else
            {
                Console.WriteLine("Price cannot be negative.");
            }
        }

        public void IncreaseQuantity()
        {
            quantity++;
            Console.WriteLine($"Quantity of \"{title}\" increased. Now: {quantity}.");
        }

        public void DecreaseQuantity()
        {
            if (quantity > 0)
            {
                quantity--;
                Console.WriteLine($"Quantity of \"{title}\" decreased. Now: {quantity}.");
            }
            else
            {
                Console.WriteLine($"Cannot decrease. \"{title}\" is out of stock.");
            }
        }

        public override string ToString()
        {
            return $"=== Book Information ===\n" +
                   $"Title: {title}\n" +
                   $"Author: {author}\n" +
                   $"Description: {description}\n" +
                   $"First publication: {creationDate.ToString("dd.MM.yyyy")}\n" +
                   $"Pages: {countOfPages}\n" +
                   $"Price: {price} USD\n" +
                   $"Quantity: {quantity}\n" +
                   $"Genre: {genre}\n" +
                   $"Other editions: {(datesOfPublication.Count > 1 ? string.Join(", ", datesOfPublication.Skip(1).Select(d => d.ToString("dd.MM.yyyy"))) : "[]")}\n" +
                   $"=========================";
        }
    }
}
