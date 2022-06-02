using BooksApi.DB;
using BooksApi.DTO;
using BooksApi.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BooksApi.Repos
{
    public class BooksRepository  : IBooksRepository
    {
        private readonly BooksContext _context;

        public BooksRepository(BooksContext context)
        {
            _context = context;
        }

        public async Task<List<BookModel>> GetAllBooksAsync()
        {
            var records =
                await _context
                .Books
                .Select(x => new BookModel
                {
                    Id = x.Id,
                    Title = x.Title,
                    Description = x.Description
                })
                .ToListAsync();

            return records;
        }
        public async Task<BookModel> GetBookByIdAsync(int id)
        {
            var record =
                await
                _context
                .Books
                .Where(x => x.Id == id)
                .Select(x => new BookModel
                {
                    Id = x.Id,
                    Title = x.Title,
                    Description = x.Description
                })
                .FirstOrDefaultAsync();

            return record;
        }

        public async Task<int> AddBookAsync(BookModel bookModel)
        {
            var book = new Book()
            {
                Title = bookModel.Title,
                Description = bookModel.Description
            };
            _context.Books.Add(book);
            await _context.SaveChangesAsync();

            return book.Id;
        }

        public async Task UpdateBookAsync(int bookId, BookModel modifiedBook)
        {
            //var book = await _context.Books.FindAsync(bookId);  

            //if (book != null)
            //{
            //    book.Title = modifiedBook.Title;
            //    book.Description = modifiedBook.Description;
            //}

            //await _context.SaveChangesAsync();

            var book = new Book()
            {
                Id = bookId,
                Title = modifiedBook.Title,
                Description = modifiedBook.Description
            };

            _context.Books.Update(book);
            //_context.Entry(book).State = EntityState.Modified;

            await _context.SaveChangesAsync();

        }

        public async Task UpdateBookAsync(int bookId, JsonPatchDocument bookModel)
        {
            var book = await _context.Books.FindAsync(bookId);  

            if (book != null)
            {
                bookModel.ApplyTo(book);
                await _context.SaveChangesAsync();
            }
        }


        public async Task DeleteBookAsync(int bookId)
        {
            var book = new Book {  Id = bookId };

            _context.Books.Remove(book);

            await _context.SaveChangesAsync();

        }

        /***
         * Install
         * 
         * Statup - נותנים לה גישה לאסמבלי
         * 
         * מגדירים קובץ מפה
         *      Book - > BookModel
         *      BookModel -> Book 
         * 
         * \ומשתמשים
         * 
         * 
         * 
         * 
         * */
    }
}
