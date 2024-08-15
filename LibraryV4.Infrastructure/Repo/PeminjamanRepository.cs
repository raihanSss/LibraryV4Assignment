using LibraryV4.Domain.Interfaces;
using LibraryV4.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryV4.Infrastructure.Repo
{
    public class PeminjamanRepository : IPeminjamanRepository
    {
        private readonly libraryContext _context;

        public PeminjamanRepository(libraryContext context)
        {
            _context = context;
        }

        public void AddPeminjaman(Peminjaman peminjaman)
        {
            _context.Peminjamans.Add(peminjaman);
            _context.SaveChanges();
        }

        public Peminjaman GetPeminjamanById(int id)
        {
            return _context.Peminjamans.Find(id);
        }

        public IEnumerable<Peminjaman> GetPeminjamanByUserId(int userId)
        {
            return _context.Peminjamans.Where(p => p.id_user == userId && p.tanggalKembali == null).ToList();
        }
    }
}
