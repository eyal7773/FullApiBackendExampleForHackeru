using System.ComponentModel.DataAnnotations;

namespace BooksApi.DTO
{
    public class BookModel
    {
        public int Id { get; set; }

        
        public string Title { get; set; }
        public string Description { get; set; }
    }
}
