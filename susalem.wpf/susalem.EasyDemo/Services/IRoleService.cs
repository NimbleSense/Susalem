using susalem.EasyDemo.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace susalem.EasyDemo.Services
{
    public interface IRoleService
    {
        List<RoleModel> FindAllRole();
        int AddRole(RoleModel role);
        int EditRole(RoleModel role);
        int DeleteRole(int roleId);
        int FindMaxRoleId();
    }
}
