using LibraryV4.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryV4.Domain.Interfaces
{
    public interface IPeminjamanRepository
    {
        void AddPeminjaman(Peminjaman peminjaman);
        Peminjaman GetPeminjamanById(int id);
        IEnumerable<Peminjaman> GetPeminjamanByUserId(int userId);
    }
}
