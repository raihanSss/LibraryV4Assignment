using LibraryV4.Domain;
using LibraryV4.Domain.Interfaces;
using LibraryV4.Domain.Models;
using LibraryV4.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryV4.Application.Services
{
    namespace Library.Services
    {
        public class PinjamService : IPeminjamanService
        {
            private readonly libraryContext _context;
            private readonly BookLoanSettings _bookLoanSettings;

            public PinjamService(libraryContext context, IOptions<BookLoanSettings> bookLoanSettings)
            {
                _context = context;
                _bookLoanSettings = bookLoanSettings.Value;
            }

            public async Task<Peminjaman> GetPeminjamanByIdAsync(int id)
            {
                return await _context.Peminjamans
                    .Include(p => p.Book)
                    .Include(p => p.User)
                    .FirstOrDefaultAsync(p => p.Id == id);
            }

            public async Task<IEnumerable<Peminjaman>> GetAllPeminjamanAsync()
            {
                return await _context.Peminjamans
                    .Include(p => p.Book)
                    .Include(p => p.User)
                    .ToListAsync();
            }

            public async Task AddPeminjamanAsync(Peminjaman peminjaman)
            {

                await _context.SaveChangesAsync();
            }

            public async Task DeletePeminjamanAsync(int id)
            {
                var peminjaman = await _context.Peminjamans.FindAsync(id);
                if (peminjaman != null)
                {
                    _context.Peminjamans.Remove(peminjaman);
                    await _context.SaveChangesAsync();
                }
            }

            public bool CanUserBorrowMoreBooks(int currentBorrowedBooks)
            {
                return currentBorrowedBooks < _bookLoanSettings.MaxBooksBorrowed;
            }

            public DateTime CalculateReturnDate(DateTime borrowDate)
            {
                return borrowDate.AddDays(_bookLoanSettings.LoanDurationDays);
            }
        }

    }
}