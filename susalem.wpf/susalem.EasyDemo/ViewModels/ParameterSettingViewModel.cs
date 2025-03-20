using HandyControl.Collections;
using susalem.EasyDemo.Entities;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Xml.Linq;
using susalem.EasyDemo.Services;

namespace susalem.EasyDemo.ViewModels
{
    internal class ParameterSettingViewModel : BindableBase
    {
        private readonly IChamParaService _chamParaService;
        private readonly ICabinetInfoService _cabinetInfoService;
        private readonly IDialogService _dialogService;

        public ParameterSettingViewModel(IChamParaService chamParaService, ICabinetInfoService cabinetInfoService, IDialogService dialogService)
        {
            _chamParaService = chamParaService;
            _dialogService = dialogService;
            _cabinetInfoService = cabinetInfoService;
        }

        #region Property

        private string? name;

        public string? Name
        {
            get { return name; }
            set { name = value; RaisePropertyChanged(); }
        }

        private string? pNCode;

        public string? PNCode
        {
            get { return pNCode; }
            set { pNCode = value; RaisePropertyChanged(); }
        }

        private string? serialNum;

        public string? SerialNum
        {
            get { return serialNum; }
            set { serialNum = value; RaisePropertyChanged(); }
        }

        /// <summary>
        /// 机台码
        /// </summary>
        private string? machineId;

        public string? MachineId
        {
            get { return machineId; }
            set { machineId = value; RaisePropertyChanged(); }
        }

        /// <summary>
        /// 机柜号
        /// </summary>
        private string? cabinetId;
        public string? CabinetId
        {
            get { return cabinetId; }
            set { cabinetId = value; RaisePropertyChanged(); }
        }

        private ObservableCollection<string> cabinets = new ObservableCollection<string>();
        public ObservableCollection<string> Cabinets
        {
            get { return cabinets; }
            set { cabinets = value; RaisePropertyChanged(); }
        }


        private string? reheatingTime;

        public string? ReheatingTime
        {
            get { return reheatingTime; }
            set { reheatingTime = value; RaisePropertyChanged(); }
        }

        private string? expirationDate;

        public string? ExpirationDate
        {
            get { return expirationDate; }
            set { expirationDate = value; RaisePropertyChanged(); }
        }

        #endregion

        public ICommand AddCommand
        {
            get => new DelegateCommand(async () =>
            {
                ChemicalParaModel chemicalParaModel = new ChemicalParaModel();
                chemicalParaModel.IsUse = false;
                chemicalParaModel.CabinetId = CabinetId;
                chemicalParaModel.Name = Name;
                chemicalParaModel.PNCode = PNCode;
                chemicalParaModel.SerialNum = SerialNum;
                chemicalParaModel.MachineId = MachineId;
                chemicalParaModel.ReheatingTime = double.Parse(ReheatingTime);
                chemicalParaModel.ExpirationDate = double.Parse(ExpirationDate);
                int ret = _chamParaService.AddPara(chemicalParaModel);

                await RefreshCabinets();

                if (ret >= 0)
                {
                    _dialogService.ShowDialog("MessageView", new DialogParameters() { { "Content", "操作成功!" } }, null);
                }
            });
        }

        public ICommand PageLoaded
        {
            get => new DelegateCommand(async () =>
            {
                //for (int i = 1; i < 21; i++)
                //{
                //    CabinetInfoModel cabinetInfoModel = new CabinetInfoModel();
                //    cabinetInfoModel.CabinetId = i;
                //    cabinetInfoModel.ChamName = string.Empty;
                //    cabinetInfoModel.PNCode = string.Empty;
                //    cabinetInfoModel.MachineId = string.Empty;
                //    cabinetInfoModel.LockAddress = string.Empty;
                //    cabinetInfoModel.GreenLightAddress = string.Empty;
                //    cabinetInfoModel.RedLightAddress = string.Empty;
                //    cabinetInfoModel.DoorAddress = string.Empty;
                //    cabinetInfoModel.IsNull = true;
                //    cabinetInfoModel.TemperatureEndTime = DateTime.MinValue;
                //    cabinetInfoModel.TemperatureStartTime = DateTime.MinValue;
                //    cabinetInfoModel.ExpirationDate = DateTime.MinValue;

                //    _cabinetInfoService.AddCabinetInfo(cabinetInfoModel);
                //}
                await RefreshCabinets();

            });
        }

        public ICommand PageUnLoaded
        {
            get => new DelegateCommand(() =>
            {
                //Login();
            });
        }

        /// <summary>
        /// 异步获取
        /// </summary>
        /// <returns></returns>
        private async Task RefreshCabinets()
        {
            await Task.Run(() =>
            {
                List<CabinetInfoModel> cabinetInfoModels = _cabinetInfoService.FindAllCabinetInfos();
                Application.Current.Dispatcher.Invoke(() =>
                {
                    if (cabinetInfoModels.Count > 0)
                    {
                        Cabinets.Clear();
                        for (int i = 0; i < cabinetInfoModels.Count; i++)
                        {
                            if (cabinetInfoModels[i].IsNull)
                                Cabinets.Add(cabinetInfoModels[i].CabinetId.ToString());
                        }
                    }
                });

            });
        }
    }
}
