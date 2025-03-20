using susalem.EasyDemo.Entities;
using susalem.EasyDemo.Services;
using HslCommunication;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace susalem.EasyDemo.ViewModels
{
    public class OperateMachineViewModel : BindableBase
    {
        private readonly IDialogService _dialogService;
        private readonly ICabinetInfoService _cabinetInfoService;
        private readonly IChamParaService _chamParaService;
        private readonly IHistoryService _historyService;


        public OperateMachineViewModel(IDialogService dialogService, ICabinetInfoService cabinetInfoService, IHistoryService historyService, IChamParaService chamParaService)
        {
            _dialogService = dialogService;
            _cabinetInfoService = cabinetInfoService;
            _historyService = historyService;
            _chamParaService = chamParaService;
        }

        private string? machineId;
        public string? MachineId
        {
            get { return machineId; }
            set { machineId = value; RaisePropertyChanged(); }
        }

        private string? count;
        public string? Count
        {
            get { return count; }
            set { count = value; RaisePropertyChanged(); }
        }



        public ICommand OpenCabinetCommand
        {
            get => new DelegateCommand(async () =>
            {
                try
                {
                    List<ChemicalParaModel> chemicalParaModels = _chamParaService.FindAllParas();
                    if (chemicalParaModels == null || chemicalParaModels.Count == 0)
                    {
                        _dialogService.ShowDialog("MessageView", new DialogParameters() { { "Content", "请检查该工匠品有无设置参数!" } }, null);
                    }

                    List<CabinetInfoModel> cabinetInfoModels = _cabinetInfoService.FindAllCabinetInfos();
                    if (string.IsNullOrEmpty(MachineId) || string.IsNullOrEmpty(Count))
                    {
                        _dialogService.ShowDialog("MessageView", new DialogParameters() { { "Content", "请填写完整信息!" } }, null);
                        return;
                    }

                    List<CabinetInfoModel> InfoModels = cabinetInfoModels.Where(x => x.MachineId == MachineId).ToList();
                    if (InfoModels.Count == 0)
                    {
                        _dialogService.ShowDialog("MessageView", new DialogParameters() { { "Content", "未查询到该批次工匠品!" } }, null);
                        return;
                    }

                    // 仅开已经回温完成的柜子
                    InfoModels = InfoModels.Where(P => !P.IsTemperaturing).Take(int.Parse(Count)).ToList();
                    if (InfoModels.Count < int.Parse(Count))
                    {
                        _dialogService.ShowDialog("MessageView", new DialogParameters() { { "Content", "柜子中没有这么多回温完成数量的该工匠品!" } }, null);
                        return;
                    }
                    else
                    {
                        _dialogService.ShowDialog("MessageView", new DialogParameters() { { "Content", "请取出工匠品!" } }, null);
                        foreach (var model in InfoModels)
                        {
                            OverAllContext.ModbusTcpLock.WriteAsync(model.LockAddress, true);
                            Thread.Sleep(200);
                            OverAllContext.ModbusTcpLock.WriteAsync(model.LockAddress, false);


                            // 更新柜号信息表
                            model.ChamName = string.Empty;
                            model.PNCode = string.Empty;
                            model.MachineId = string.Empty;
                            model.IsTemperaturing = false;
                            model.IsNull = true;
                            model.TemperatureStartTime = DateTime.MinValue;
                            model.TemperatureEndTime = DateTime.MinValue;
                            model.ExpirationDate = DateTime.MinValue;

                            _cabinetInfoService.EditCabinetInfo(model);

                            HistoryModel historyModel = new HistoryModel();
                            historyModel.CabinetId = model.CabinetId.ToString();
                            historyModel.PNCode = model.PNCode;
                            historyModel.MachineId = model.MachineId;
                            historyModel.Message= "回温结束取出";
                            historyModel.Name = model.ChamName;
                            historyModel.OpenCabinetTime = DateTime.Now;
                            _historyService.AddHistory(historyModel);

                        }


                        //for (int i = 0; i < InfoModels.Count; i++)
                        //{
                        //    //OverAllContext.modbusTcpServer.WriteDiscrete(InfoModels[i].CabinetId.ToString(), true);
                        //    // 门锁有三秒保持信号，在开完锁之后必须立马关锁
                        //    OverAllContext.ModbusTcpLock.WriteAsync(InfoModels[i].LockAddress, true);
                        //    Thread.Sleep(200);
                        //    OverAllContext.ModbusTcpLock.WriteAsync(InfoModels[i].LockAddress, false);


                        //    // 更新柜号信息表

                        //    InfoModels[i].ChamName = string.Empty;
                        //    InfoModels[i].PNCode = string.Empty;
                        //    InfoModels[i].MachineId = string.Empty;
                        //    InfoModels[i].IsTemperaturing = false;
                        //    InfoModels[i].IsNull = true;
                        //    InfoModels[i].TemperatureStartTime = DateTime.MinValue;
                        //    InfoModels[i].TemperatureEndTime = DateTime.MinValue;
                        //    InfoModels[i].ExpirationDate = DateTime.MinValue;

                        //    _cabinetInfoService.EditCabinetInfo(InfoModels[i]);

                        //    HistoryModel historyModel = new HistoryModel();
                        //    historyModel.CabinetId = InfoModels[i].CabinetId.ToString();
                        //    historyModel.PNCode = InfoModels[i].PNCode;
                        //    historyModel.MachineId = InfoModels[i].MachineId;
                        //    historyModel.Name = InfoModels[i].ChamName;
                        //    historyModel.OpenCabinetTime = DateTime.Now;
                        //    _historyService.AddHistory(historyModel);
                        //}

                        // 开柜门后延迟一分钟检查柜门有无关，如未关，报警
                        // 这里需要检查多个柜门是否关闭，注意细节处理
                        await Task.Run(async () =>
                        {
                            await Task.Delay(1000 * 60);

                            bool isExistOpen = false;

                            foreach (var model in InfoModels)
                            {

                                OperateResult<bool> operateResult = OverAllContext.ModbusTcpDoor.ReadBool(model.CabinetId.ToString());
                                var isOpen = operateResult.Content;
                                if (isOpen)
                                {
                                    isExistOpen = true;
                                }
                            }

                            if (isExistOpen)
                            {
                                // 门未关，报警 不确定是不是这个地址
                                await OverAllContext.ModbusTcpLock.WriteAsync("27", true);
                            }


                        });

                    }
                }
                catch (Exception ex)
                {
                    _dialogService.ShowDialog("MessageView", new DialogParameters() { { "Content", $"出现异常，请联系开发人员。详情:{ex.ToString()}!" } }, null);
                }



            });
        }

    }
}
