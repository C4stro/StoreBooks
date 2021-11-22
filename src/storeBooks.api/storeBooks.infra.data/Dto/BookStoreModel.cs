using System.ComponentModel.DataAnnotations;

namespace storeBooks.repository.Dto
{
    public class BookStoreModel
    {
        [Key]
        public int? Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public decimal Price { get; set; }
        public bool IsDeleted { get; set; }
    }
}
