using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryV4.Domain.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        public string nama { get; set; }
        public string alamat { get; set; }
        public string nohp { get; set; }
        public string jenisKelamin { get; set; }
    }
}
