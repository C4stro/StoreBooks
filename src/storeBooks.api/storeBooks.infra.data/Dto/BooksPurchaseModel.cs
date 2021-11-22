using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace storeBooks.infra.data.Dto
{
    public class BooksPurchaseModel
    {
        [Key]
        public int? Id { get; set; }
        public int? IdSaleBook { get; set; }
        public int? IdBook { get; set; }
        public decimal ValueBookEUR { get; set; }
        public decimal ValueBookBRL { get; set; }
        public decimal ValueBookUSD { get; set; }
        public decimal ValueBookGBP { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
    }
}
