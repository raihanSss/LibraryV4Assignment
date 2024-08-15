using LibraryV4.Domain;
using LibraryV4.Domain.Interfaces;
using LibraryV4.Domain.Models;
using LibraryV4.Infrastructure;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryV4.Application.Services
{
    public class BookService : IBookService
    {
        private readonly IBookRepository _bookRepository;
        private readonly BookManager _bookManager;

        public BookService(IBookRepository bookRepository, IPeminjamanRepository peminjamanRepository, IOptions<BookLoanSettings> options)
        {
            _bookRepository = bookRepository;
            _bookManager = BookManager.GetInstance(bookRepository, peminjamanRepository, options);
        }

        public string AddBook(Book book)
        {
            _bookRepository.AddBook(book);
            return "Buku berhasil ditambahkan";
        }

        public IEnumerable<Book> GetAllBook()
        {
            return _bookRepository.GetAllBooks();
        }

        public Book GetBookById(int id)
        {
            return _bookRepository.GetBookById(id);
        }

        public string UpdateBook(Book book, int id)
        {
            var existingBook = _bookRepository.GetBookById(id);
            if (existingBook != null)
            {
                existingBook.Title = book.Title;
                existingBook.Author = book.Author;
                existingBook.PublicationYear = book.PublicationYear;
                existingBook.ISBN = book.ISBN;
                _bookRepository.UpdateBook(existingBook);
                return "Data buku berhasil diubah";
            }
            return "Buku tidak ditemukan";
        }

        public string DeleteBook(int id)
        {
            var book = _bookRepository.GetBookById(id);
            if (book != null)
            {
                _bookRepository.DeleteBook(id);
                return "Data buku berhasil dihapus";
            }
            return "Buku tidak ditemukan";
        }

        public string BorrowBook(int bookId, int userId)
        {
            return _bookManager.BorrowBook(bookId, userId);
        }

        public string ReturnBook(int bookId, int userId)
        {
            return _bookManager.ReturnBook(bookId, userId);
        }
    }
}
