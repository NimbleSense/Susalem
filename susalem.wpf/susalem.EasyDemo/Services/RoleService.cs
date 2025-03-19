using susalem.EasyDemo.Entities;
using susalem.EasyDemo.Repository;
using susalem.EasyDemo.Services.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace susalem.EasyDemo.Services
{
    public class RoleService : IRoleService
    {
        public int AddRole(RoleModel role)
        {
            int nRet = 0;
            using (JccRepository hc = new JccRepository())
            {
                try
                {
                    hc.Roles?.Add(role);
                    hc.SaveChanges();
                }
                catch (Exception ex)
                {
                }
            };
            return nRet;
        }

        public int DeleteRole(int roleId)
        {
            int nRet = 0;
            using (JccRepository hc = new JccRepository())
            {
                try
                {
                    hc.Roles?.Remove(hc.Roles.Where(u => u.RoleId == roleId).FirstOrDefault()!);
                    hc.SaveChanges();
                }
                catch (Exception ex)
                {
                }
            };
            return nRet;
        }

        public int EditRole(RoleModel role)
        {
            int nRet = 0;
            using (JccRepository hc = new JccRepository())
            {
                try
                {
                    hc.Roles?.Update(role);
                    hc.SaveChanges();
                }
                catch (Exception ex)
                {
                }
            };
            return nRet;
        }

        public List<RoleModel> FindAllRole()
        {
            List<RoleModel> result  = new List<RoleModel>();
            using (JccRepository hc = new JccRepository())
            {
                try
                {
                    result = hc.Roles?.Select(r => r).ToList();
                }
                catch (Exception ex)
                {
                }
            };
            return result;
        }

        public int FindMaxRoleId()
        {
            int nRet = 0;
            using (JccRepository hc = new JccRepository())
            {
                try
                {
                    nRet = hc.Roles.Select(u => u.RoleId).Max();
                }
                catch (Exception ex)
                {
                }
            };
            return nRet;
        }
    }
}
