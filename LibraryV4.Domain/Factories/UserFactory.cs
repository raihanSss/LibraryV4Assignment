using LibraryV4.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryV4.Domain.Factories
{
    public static class UserFactory
    {
        public static User CreateUser(string nama, string alamat, string nohp, string jenisKelamin)
        {
            return new User
            {
                nama = nama,
                alamat = alamat,
                nohp = nohp,
                jenisKelamin = jenisKelamin
            };
        }
    }
}
