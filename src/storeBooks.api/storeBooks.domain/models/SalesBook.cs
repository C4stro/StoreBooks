using System;
using System.Collections.Generic;

namespace storeBooks.domain.models
{
    public class SalesBook
    {
        public int Id { get; set; }
        public string Customer { get; set; }
        public decimal ValueTotalPurchase { get; set; }
        public DateTime DatePurchase { get; set; }
        public string Currency { get; set; }
        public List<BookStore> Books { get; set; }
    }
}
