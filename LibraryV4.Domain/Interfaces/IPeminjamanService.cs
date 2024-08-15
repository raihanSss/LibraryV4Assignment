using LibraryV4.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryV4.Domain.Interfaces
{
    public interface IPeminjamanService
    {
        Task<Peminjaman> GetPeminjamanByIdAsync(int id);
        Task<IEnumerable<Peminjaman>> GetAllPeminjamanAsync();
        Task AddPeminjamanAsync(Peminjaman peminjaman);
        Task DeletePeminjamanAsync(int id);
    }
}
