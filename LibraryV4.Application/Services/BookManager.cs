using LibraryV4.Domain;
using LibraryV4.Domain.Interfaces;
using LibraryV4.Domain.Models;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryV4.Application.Services
{
    public class BookManager
    {
        private static BookManager _instance;
        private readonly IBookRepository _bookRepository;
        private readonly IPeminjamanRepository _peminjamanRepository;
        private readonly int _maxBooksPerUser;
        private readonly int _loanDurationDays;

        // Private constructor to prevent external instantiation
        private BookManager(IBookRepository bookRepository, IPeminjamanRepository peminjamanRepository, IOptions<BookLoanSettings> options)
        {
            _bookRepository = bookRepository;
            _peminjamanRepository = peminjamanRepository;
            _maxBooksPerUser = options.Value.MaxBooksBorrowed;
            _loanDurationDays = options.Value.LoanDurationDays;
        }

        // Public method to provide a global point of access
        public static BookManager GetInstance(IBookRepository bookRepository, IPeminjamanRepository peminjamanRepository, IOptions<BookLoanSettings> options)
        {
            if (_instance == null)
            {
                _instance = new BookManager(bookRepository, peminjamanRepository, options);
            }
            return _instance;
        }

        public string BorrowBook(int bookId, int userId)
        {
            var borrowedBooksCount = _peminjamanRepository.GetPeminjamanByUserId(userId).Count();

            if (borrowedBooksCount >= _maxBooksPerUser)
            {
                return $"Pengguna telah mencapai batas maksimum {_maxBooksPerUser} buku yang dipinjam.";
            }

            var book = _bookRepository.GetBookById(bookId);
            if (book != null && book.IsAvailable)
            {
                // Logic for borrowing a book
                var peminjaman = new Peminjaman
                {
                    id_buku = bookId,
                    id_user = userId,
                    tanggalPinjam = DateTime.Now,
                    tanggalKembali = DateTime.Now.AddDays(_loanDurationDays) 
                };

                _peminjamanRepository.AddPeminjaman(peminjaman);

                book.IsAvailable = false;
                _bookRepository.UpdateBook(book);

                return "Buku berhasil dipinjam";
            }
            return "Buku tidak tersedia";
        }

        public string ReturnBook(int bookId, int userId)
        {
            var peminjaman = _peminjamanRepository.GetPeminjamanByUserId(userId)
                                 .FirstOrDefault(p => p.id_buku == bookId && p.tanggalKembali == null);

            if (peminjaman != null)
            {

                var book = _bookRepository.GetBookById(bookId);
                if (book != null)
                {
                    book.IsAvailable = true;
                    _bookRepository.UpdateBook(book);
                }

                return "Buku berhasil dikembalikan";
            }
            return "Peminjaman tidak ditemukan atau sudah dikembalikan";
        }
    }
}
