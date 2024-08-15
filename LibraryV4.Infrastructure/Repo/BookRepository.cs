using LibraryV4.Domain.Interfaces;
using LibraryV4.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryV4.Infrastructure.Repo
{
    public class BookRepository : IBookRepository
    {
        private readonly libraryContext _context;

        public BookRepository(libraryContext context)
        {
            _context = context;
        }

        public Book GetBookById(int id)
        {
            return _context.Books.Find(id);
        }

        public IEnumerable<Book> GetAllBooks()
        {
            return _context.Books.ToList();
        }

        public void AddBook(Book book)
        {
            _context.Books.Add(book);
            _context.SaveChanges();
        }

        public void UpdateBook(Book book)
        {
            _context.Books.Update(book);
            _context.SaveChanges();
        }

        public void DeleteBook(int id)
        {
            var book = _context.Books.Find(id);
            if (book != null)
            {
                _context.Books.Remove(book);
                _context.SaveChanges();
            }
        }
    }
}
