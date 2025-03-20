using susalem.EasyDemo.Entities;
using susalem.EasyDemo.Services;
using HslCommunication;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using Prism.Regions;

namespace susalem.EasyDemo.ViewModels
{
    /// <summary>
    /// 回温
    /// </summary>
    internal class ChambrierenViewModel : BindableBase
    {
        private readonly IChamParaService _chamParaService;
        private readonly IHistoryService _historyService;
        private readonly ICabinetInfoService _cabinetInfoService;
        private readonly IDialogService _dialogService;

        public ChambrierenViewModel(IChamParaService chamParaService, IDialogService dialogService, ICabinetInfoService cabinetInfoService, IHistoryService historyService)
        {
            //RefreshDateTime();
            _chamParaService = chamParaService;
            _dialogService = dialogService;
            _cabinetInfoService = cabinetInfoService;
            _historyService = historyService;
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



        public ICommand OpenCabinetCommand
        {
            get => new DelegateCommand(async () =>
            {
                try
                {
                    List<ChemicalParaModel> chemicalParaModels = _chamParaService.FindAllParas();
                    if (string.IsNullOrEmpty(PNCode) || string.IsNullOrEmpty(SerialNum))
                    {
                        _dialogService.ShowDialog("MessageView", new DialogParameters() { { "Content", "请输入工匠品料号和工匠品编号!" } }, null);
                        return;
                    }
                    if (chemicalParaModels == null || chemicalParaModels.Count == 0)
                    {
                        _dialogService.ShowDialog("MessageView", new DialogParameters() { { "Content", "请检查该工匠品有无设置参数!" } }, null);
                        return;
                    }

                    ChemicalParaModel? ChemicalParaModel = chemicalParaModels.Find(x => x.PNCode == PNCode && x.SerialNum == SerialNum);
                    if (ChemicalParaModel == null)
                    {
                        _dialogService.ShowDialog("MessageView", new DialogParameters() { { "Content", "请检查该工匠品有无设置参数!" } }, null);
                        return;
                    }

                    CabinetInfoModel? infoModel = _cabinetInfoService.FindCabinetInfoByCabinetCode(int.Parse(ChemicalParaModel.CabinetId));
                    if (infoModel == null)
                    {
                        _dialogService.ShowDialog("MessageView", new DialogParameters() { { "Content", "请检查该工匠品有无设置参数!" } }, null);
                        return;
                    }

                    if (!infoModel.IsNull)
                    {
                        _dialogService.ShowDialog("MessageView", new DialogParameters() { { "Content", "该柜子正在使用中!" } }, null);
                        return;
                    }
                    _dialogService.ShowDialog("MessageView", new DialogParameters() { { "Content", "开始回温" } }, null);

                    // 门锁有三秒保持信号，在开完锁之后必须立马关锁
                    OverAllContext.ModbusTcpLock.WriteAsync(infoModel.LockAddress, true);
                    Thread.Sleep(200);
                    OverAllContext.ModbusTcpLock.WriteAsync(infoModel.LockAddress, false);


                    // 更新柜号信息表
                    infoModel.ChamName = ChemicalParaModel.Name;
                    infoModel.PNCode = ChemicalParaModel.PNCode;
                    infoModel.IsNull = false;
                    infoModel.MachineId = ChemicalParaModel.MachineId;
                    infoModel.IsTemperaturing = true;
                    infoModel.TemperatureStartTime = DateTime.Now;
                    infoModel.TemperatureEndTime = DateTime.Now.AddHours(ChemicalParaModel.ReheatingTime);
                    infoModel.ExpirationDate = DateTime.Now.AddHours(ChemicalParaModel.ReheatingTime).AddDays(ChemicalParaModel.ExpirationDate);
                    

                    _cabinetInfoService.EditCabinetInfo(infoModel);


                    // 更新历史记录表
                    HistoryModel historyModel = new HistoryModel();
                    historyModel.CabinetId = ChemicalParaModel.CabinetId;
                    historyModel.PNCode = ChemicalParaModel.PNCode;
                    historyModel.SerialNum = ChemicalParaModel.SerialNum;
                    historyModel.MachineId = ChemicalParaModel.MachineId;
                    historyModel.Message = "开柜回温"; 
                    historyModel.Name = ChemicalParaModel.Name;
                    historyModel.OpenCabinetTime = DateTime.Now;
                    _historyService.AddHistory(historyModel);

                    // 开柜门后延迟一分钟检查柜门有无关，如未关，报警
                    await Task.Run(async () =>
                    {
                        await Task.Delay(1000 * 60);
                        OperateResult<bool> operateResult = OverAllContext.ModbusTcpDoor.ReadBool(ChemicalParaModel.CabinetId);
                        var isOpen = operateResult.Content;
                        if (isOpen)
                        {
                            // 门未关，报警 不确定是不是这个地址
                            await OverAllContext.ModbusTcpLock.WriteAsync("27", true);
                            //TODU 要不要等待0.2s关闭

                        }
                    });
                }
                catch (Exception ex)
                {
                    _dialogService.ShowDialog("MessageView", new DialogParameters() { { "Content", $"出现异常，请联系开发人员。详情:{ex.ToString()}!" } }, null);
                }
            });
        }


    }
}
