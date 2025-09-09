using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public class Book
    {
        public string title;
        public string author;
        private DateOnly creationDate;
        private int countOfPages;
        private decimal price;
        private int quantity;
        private List<DateOnly> datesOfPublication;

        public Genre genre;

        public Book(string title, string author, DateOnly creationDate, int countOfPages, decimal price, int quantity, Genre genre)
        {
            this.title = title;
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
                Console.WriteLine($"Ціну книги \"{title}\" змінено на {price} USD.");
            }
            else
            {
                Console.WriteLine("Ціна не може бути від’ємною.");
            }
        }

        public void IncreaseQuantity()
        {
            quantity++;
            Console.WriteLine($"Кількість примірників \"{title}\" збільшено. Тепер: {quantity}.");
        }

        public void DecreaseQuantity()
        {
            if (quantity > 0)
            {
                quantity--;
                Console.WriteLine($"Кількість примірників \"{title}\" зменшено. Тепер: {quantity}.");
            }
            else
            {
                Console.WriteLine($"Не можна зменшити. \"{title}\" відсутня на складі.");
            }
        }

        public override string ToString()
        {
            return $"{title,-20} | {author,-18} | {creationDate:dd.MM.yyyy} | {countOfPages,5} | {price,8} USD | {quantity,5} | {genre,-10} | {(datesOfPublication.Count > 1 ? string.Join(", ", datesOfPublication.Skip(1).Select(d => d.ToString("dd.MM.yyyy"))) : "[]")}";
        }
    }
}
