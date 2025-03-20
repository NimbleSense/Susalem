using susalem.EasyDemo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace susalem.EasyDemo.Services
{
    public interface IUserService
    {
        public int AddUser(UserModel user);

        public int DeleteUser(int userId);

        public int EditUser(UserModel user);

        public List<UserModel> FindAllUser();

        public UserModel Login(string userName, string password, bool IsChecked);

    }
}
