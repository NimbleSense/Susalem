using susalem.EasyDemo.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace susalem.EasyDemo.Services
{
    public interface ICabinetInfoService
    {
        List<CabinetInfoModel> FindAllCabinetInfos();
        CabinetInfoModel? FindCabinetInfoByCabinetCode(int id);
        int AddCabinetInfo(CabinetInfoModel info);
        int EditCabinetInfo(CabinetInfoModel para);
        Task<int> EditCabinetInfoList(IEnumerable<CabinetInfoModel> infoList);
        //int DeleteCabinetInfo(int id);
    }
}
