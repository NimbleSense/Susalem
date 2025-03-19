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
    public class CabinetInfoService : ICabinetInfoService
    {
        public int AddCabinetInfo(CabinetInfoModel info)
        {
            int nRet = 0;
            using (JccRepository hc = new JccRepository())
            {
                try
                {
                    hc.CabinetInfos?.Add(info);
                    hc.SaveChanges();
                }
                catch (Exception ex)
                {
                }
            };
            return nRet;
        }

        public int EditCabinetInfo(CabinetInfoModel info)
        {
            int nRet = 0;
            using (JccRepository hc = new JccRepository())
            {
                try
                {
                    hc.CabinetInfos?.Update(info);
                    hc.SaveChanges();
                }
                catch (Exception ex)
                {
                }
            };
            return nRet;
        }

        public async Task<int> EditCabinetInfoList(IEnumerable<CabinetInfoModel> infoList)
        {
            int nRet = 0;
            using (JccRepository hc = new JccRepository())
            {
                try
                {
                    hc.CabinetInfos!.UpdateRange(infoList);
                    nRet = await hc.SaveChangesAsync();
                }
                catch (Exception ex)
                {
                }
            };
            return nRet;
        }

        public List<CabinetInfoModel> FindAllCabinetInfos()
        {
            List<CabinetInfoModel> result = new List<CabinetInfoModel>();
            using (JccRepository hc = new JccRepository())
            {
                try
                {
                    result = hc.CabinetInfos?.Select(r => r).ToList();
                }
                catch (Exception ex)
                {
                }
            };
            return result;
        }

        public CabinetInfoModel? FindCabinetInfoByCabinetCode(int id)
        {
            CabinetInfoModel? result = new CabinetInfoModel();
            using (JccRepository hc = new JccRepository())
            {
                try
                {
                    result = hc.CabinetInfos?.Where(r => r.CabinetId == id).FirstOrDefault();
                }
                catch (Exception ex)
                {
                }
            };
            return result;
        }
    }
}
