using LibraryV4.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryV4.Domain.Interfaces
{
    public interface IUserService
    {
        string AddUser(User user);
        IEnumerable<User> GetAllUser();
        User GetUserById(int id);
        string UpdateUser(User user);
        string DeleteUser(int id);
    }
}
