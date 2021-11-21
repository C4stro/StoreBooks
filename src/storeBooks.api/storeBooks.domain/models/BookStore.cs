using System;
using System.Collections.Generic;
using System.Text;

namespace storeBooks.domain.models
{
    public class BookStore
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public decimal Price { get; set; }
        public string Currency { get; set; }
        public BookStore() { }

        public BookStore(string title, string author, decimal price, int id)
        {
            this.Author = author;
            this.Price = price;
            this.Title = title;
            this.Id = id;
        }
    }
}
