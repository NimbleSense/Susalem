using susalem.EasyDemo.Entities;
using susalem.EasyDemo.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace susalem.EasyDemo.Services.ServicesImpl
{
    public class HistoryService : IHistoryService
    {
        public int AddHistory(HistoryModel model)
        {
            int nRet = 0;
            using (JccRepository hc = new JccRepository())
            {
                try
                {
                    hc.Historys?.Add(model);
                    hc.SaveChanges();
                }
                catch (Exception ex)
                {
                }
            };
            return nRet;
        }

        public List<HistoryModel> FindAllHistorys()
        {
            List<HistoryModel> result = new List<HistoryModel>();
            using (JccRepository hc = new JccRepository())
            {
                try
                {
                    result = hc.Historys?.Select(r => r).ToList();
                }
                catch (Exception ex)
                {
                }
            };
            return result;
        }

        public HistoryModel FindHistoryById(string machinecode, string cabinetId)
        {
            HistoryModel result = new HistoryModel();
            using (JccRepository hc = new JccRepository())
            {
                try
                {
                    result = hc.Historys?.Where(r => r.CabinetId == cabinetId && r.MachineId == machinecode).FirstOrDefault();
                }
                catch (Exception ex)
                {
                }
            };
            return result;
        }

        public HistoryModel FindHistoryByTime(DateTime startTime, DateTime endTime)
        {
            HistoryModel result = new HistoryModel();
            using (JccRepository hc = new JccRepository())
            {
                try
                {
                    result = hc.Historys?.Where(r => r.OpenCabinetTime >= startTime && r.OpenCabinetTime <= endTime).FirstOrDefault();
                }
                catch (Exception ex)
                {
                }
            };
            return result;
        }
    }
}
