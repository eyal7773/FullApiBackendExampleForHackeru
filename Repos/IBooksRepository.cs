using BooksApi.DTO;
using Microsoft.AspNetCore.JsonPatch;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BooksApi.Repos
{
    public interface IBooksRepository
    {
        Task<List<BookModel>> GetAllBooksAsync();
        Task<BookModel> GetBookByIdAsync(int id);
        Task<int> AddBookAsync(BookModel bookModel);
        Task UpdateBookAsync(int bookId, BookModel modifiedBook);
        Task UpdateBookAsync(int bookId, JsonPatchDocument bookModel);

        Task DeleteBookAsync(int bookId);
    }
}