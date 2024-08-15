using LibraryV4.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryV4.Domain.Interfaces
{
    public interface IBookService
    {
        string AddBook(Book book);
        IEnumerable<Book> GetAllBook();
        Book GetBookById(int id);
        string UpdateBook(Book book, int id);
        string DeleteBook(int id);

        string BorrowBook(int bookId, int userId);
        string ReturnBook(int bookId, int userId);
    }
}
