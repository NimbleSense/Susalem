using susalem.EasyDemo.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace susalem.EasyDemo.Services
{
    public interface IHistoryService
    {
        List<HistoryModel> FindAllHistorys();

        HistoryModel FindHistoryByTime(DateTime startTime, DateTime endTime);

        HistoryModel FindHistoryById(string machinecode, string cabinetId);

        int AddHistory(HistoryModel model);
        //int EditCabinetInfo(HistoryModel model);
    }
}
