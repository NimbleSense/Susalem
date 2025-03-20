using susalem.EasyDemo.Entities;
using susalem.EasyDemo.Services;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace susalem.EasyDemo.ViewModels
{
    public class HistoryRecordViewModel : BindableBase
    {
        private ObservableCollection<HistoryRecordDataList> _dataList = new ObservableCollection<HistoryRecordDataList>();
        public ObservableCollection<HistoryRecordDataList> DataList
        {
            get { return _dataList; }
            set { _dataList = value; RaisePropertyChanged(); }
        }


        private readonly IHistoryService _historyService;

        public HistoryRecordViewModel(IHistoryService historyService)
        {
            _historyService = historyService;

            GetHistoryList();
        }

        public ICommand QueryCommand
        {
            get => new DelegateCommand(() =>
            {
                GetHistoryList();

            });
        }

        private void GetHistoryList()
        {
            var historyModelList = _historyService.FindAllHistorys().Select(p => new HistoryRecordDataList
            {
                CabinetId = p.CabinetId,
                MachineId = p.MachineId,
                Name = p.Name,
                PNCode = p.PNCode,
                SerialNum = p.SerialNum,
                OpenCabinetTime = p.OpenCabinetTime,
                Message = p.Message,

            });

            if (StartDate.HasValue)
            {
                historyModelList = historyModelList.Where(p => p.OpenCabinetTime >= StartDate);
            }
            if (EndDate.HasValue)
            {
                historyModelList = historyModelList.Where(p => p.OpenCabinetTime <= EndDate);
            }

            if (!string.IsNullOrEmpty(CabinetId))
            {
                historyModelList = historyModelList.Where(p => p.CabinetId == CabinetId);
            }

            if (!string.IsNullOrEmpty(MachineId))
            {
                historyModelList = historyModelList.Where(p => p.MachineId == MachineId);
            }


            DataList = new ObservableCollection<HistoryRecordDataList>(historyModelList);
        }

        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime? StartDate { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime? EndDate { get; set; }

        /// <summary>
        /// 柜号
        /// </summary>
        public string CabinetId { get; set; }

        /// <summary>
        /// 机台号
        /// </summary>
        public string MachineId { get; set; }

    }


    public class HistoryRecordDataList : BindableBase
    {

        private string _cabinetId;

        public string CabinetId
        {
            get { return _cabinetId; }
            set { _cabinetId = value; RaisePropertyChanged(); }
        }

        private string _name;
        public string Name
        {
            get { return _name; }
            set { _name = value; RaisePropertyChanged(); }
        }

        private string _pnCode;
        public string PNCode
        {
            get { return _pnCode; }
            set { _pnCode = value; RaisePropertyChanged(); }
        }

        private string _serialNum;
        public string SerialNum
        {
            get { return _serialNum; }
            set { _serialNum = value; RaisePropertyChanged(); }
        }

        private string _machineId;
        public string MachineId
        {
            get { return _machineId; }
            set { _machineId = value; RaisePropertyChanged(); }
        }

        private DateTime? _openCabinetTime;
        public DateTime? OpenCabinetTime
        {
            get { return _openCabinetTime; }
            set { _openCabinetTime = value; RaisePropertyChanged(); }
        }
        private string?_message;
        public string? Message
        {
            get { return _message; }
            set { _message = value; RaisePropertyChanged(); }
        }

    }
}
