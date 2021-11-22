using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace storeBooks.infra.data.Dto
{
    public class SalesBookStoreModel
    {
        [Key]
        public int? Id { get; set; }
        public string Customer { get; set; }
        public decimal ValueTotalPurchaseEUR { get; set; }
        public DateTime DatePurchase { get; set; }
        public string Currency { get; set; }
        [ForeignKey("IdSaleBook")]
        public List<BooksPurchaseModel> BooksPurchase { get; set; }
    }
}
