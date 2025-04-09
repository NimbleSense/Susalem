using susalem.EasyDemo.Models;
using susalem.EasyDemo.Repository;
using susalem.EasyDemo.Share;
using susalem.EasyDemo.Views;
using HslCommunication.Core.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using susalem.EasyDemo.Services;

namespace susalem.EasyDemo.Services.ServicesImpl
{
    public class UserService : IUserService
    {
        public int AddUser(UserModel user)
        {
            int nRet = 0;
            using (JccRepository hc = new JccRepository())
            {
                try
                {
                    hc.Users!.Add(user);
                    nRet = hc.SaveChanges();
                }
                catch (Exception ex)
                {

                }
            }
            return nRet;
        }

        public int DeleteUser(int userId)
        {
            int nRet = 0;
            using (JccRepository hc = new JccRepository())
            {
                try
                {
                    hc.Users!.Remove(hc.Users.Where(u => u.UserId == userId).FirstOrDefault()!);
                    hc.SaveChanges();
                }
                catch (Exception ex)
                {

                }
            }
            return nRet;
        }

        public int EditUser(UserModel user)
        {
            int nRet = 0;
            using (JccRepository hc = new JccRepository())
            {
                try
                {
                    hc.Users!.Update(user);
                    nRet = hc.SaveChanges();
                }
                catch (Exception ex)
                {

                }
            }
            return nRet;
        }

        public List<UserModel> FindAllUser()
        {
            List<UserModel> data = new List<UserModel>();
            using (JccRepository hc = new JccRepository())
            {
                try
                {
                    data = hc.Users!.Select(u => u).ToList();

                }
                catch (Exception ex)
                {

                }
            }
            return data;
        }

        public UserModel Login(string userName, string password, bool IsChecked)
        {
            UserModel user = new UserModel();
            using (JccRepository hc = new JccRepository())
            {
                try
                {
                    var list = hc.Users!.Where(u => u.UserName == userName && u.State == 1).ToList();

                    foreach (var item in list)
                    {
                        if (Cryptography.Decrypt(item.Password!) != password)
                            list.Remove(item);
                        break;
                    }

                    if (list.Count > 0)
                    {
                        var _user = list.FirstOrDefault();
                        if (_user != null)
                        {
                            user.UserId = _user.UserId;
                            user.UserName = _user.UserName;
                            user.Password = _user.Password;
                            user.UserIcon = _user.UserIcon;
                            user.State = _user.State;
                        }

                        //int roleId = hc.UserRoles!.Where(u => u.UserId == resultModel.Data!.UserId).FirstOrDefault()!.RoleId;
                        //if (roleId > 0)
                        //{
                        //    resultModel.Data!.RealName = hc.Roles!.Where(r => r.RoleId == roleId).FirstOrDefault()!.RoleName;
                        //    resultModel.Data!.Level = hc.Roles!.Where(r => r.RoleId == roleId).FirstOrDefault()!.Level;
                        //}
                    }
                    else
                    {
                        return null;
                    }
                }
                catch (Exception ex)
                {

                }
            }
            return user;
        }
    }
}
