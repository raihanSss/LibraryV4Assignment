using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryV4.Domain.Models
{
    public class Peminjaman
    {
        [Key]
        public int Id { get; set; }

        public int id_buku { get; set; }

        [ForeignKey("id_buku")]
        public Book Book { get; set; }

        public int id_user { get; set; }
        [ForeignKey("id_user")]
        public User User { get; set; }

        public DateTime tanggalPinjam { get; set; }

        public DateTime tanggalKembali { get; set; }
    }
}
