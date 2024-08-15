using LibraryV4.Domain.Interfaces;
using LibraryV4.Domain.Models;
using LibraryV4.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryV4.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public string AddUser(User user)
        {
            _userRepository.AddUser(user);
            return "Data User berhasil ditambahkan";
        }

        public IEnumerable<User> GetAllUser()
        {
            return _userRepository.GetAllUsers();
        }

        public User GetUserById(int id)
        {
            return _userRepository.GetUserById(id);
        }

        public string UpdateUser(User user)
        {
            var existingUser = _userRepository.GetUserById(user.Id);
            if (existingUser != null)
            {
                existingUser.nama = user.nama;
                existingUser.alamat = user.alamat;
                existingUser.nohp = user.nohp;
                existingUser.jenisKelamin = user.jenisKelamin;

                _userRepository.UpdateUser(existingUser);
                return "Data user berhasil diubah";
            }
            return "User tidak ditemukan";
        }

        public string DeleteUser(int id)
        {
            var user = _userRepository.GetUserById(id);
            if (user != null)
            {
                _userRepository.DeleteUser(id);
                return "Data user berhasil dihapus";
            }
            return "User tidak ditemukan";
        }
    }
}