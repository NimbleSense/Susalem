using susalem.EasyDemo.Entities;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using susalem.EasyDemo.Services;
using System.Diagnostics;

namespace susalem.EasyDemo.ViewModels
{
    public class CurrentCabinetViewModel : BindableBase
    {
        private readonly ICabinetInfoService _cabinetInfoService;
        private readonly IDialogService _dialogService;
        private CancellationTokenSource _cts = new CancellationTokenSource();
        private CancellationTokenSource _ctsRefresh = new CancellationTokenSource();

        public CurrentCabinetViewModel(ICabinetInfoService cabinetInfoService, IDialogService dialogService)
        {
            _cabinetInfoService = cabinetInfoService;
            _dialogService = dialogService;
            RefreshList();
            RefreshDateTime();

        }

        private ObservableCollection<Cabinfo> _dataList = new ObservableCollection<Cabinfo>();
        public ObservableCollection<Cabinfo> DataList
        {
            get { return _dataList; }
            set { _dataList = value; RaisePropertyChanged(); }
        }

        private string _clock = string.Empty;

        public string Clock
        {
            get { return _clock; }
            set { _clock = value; RaisePropertyChanged(); }
        }
        

        //启动一个线程刷新右上角当前时间
        private void RefreshDateTime()
        {
            Task.Run(async () =>
            {
                while (!_cts.IsCancellationRequested)
                {
                    //Thread.Sleep(1000);
                    await Task.Delay(1000);
                    if (Application.Current != null)
                    {
                        //委托刷新ui
                        Application.Current.Dispatcher.Invoke(() =>
                        {
                            Clock = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
                        });
                    }

                }
            }, _cts.Token);
        }

        //定时刷新回温工匠品列表
        private void RefreshList()
        {

            Task.Factory.StartNew(async () =>
            {
                while (!_ctsRefresh.IsCancellationRequested)
                {
                    try
                    {
                        List<CabinetInfoModel> lstInfoModels = _cabinetInfoService.FindAllCabinetInfos();
                        var group = lstInfoModels.GroupBy(x => x.PNCode);
                        if (group == null || group.Count() == 0)
                            continue;
                        //TODO 线程调用报错的地方 
                        Application.Current.Dispatcher.Invoke(() => { DataList.Clear(); });

                        List<Cabinfo> cabinfoList = new List<Cabinfo>();
                        foreach (var item in group)
                        {
                            Cabinfo cabinfo = new Cabinfo();
                            cabinfo.PNCode = item.Key;
                            foreach (var cabinetInfo in item)
                            {
                                if (cabinetInfo.IsTemperaturing)
                                    cabinfo.IsChambrieren++;//回温中
                                else
                                    cabinfo.Finished++;//回温完成

                                cabinfo.ChemName = cabinetInfo.ChamName;
                            }
                            cabinfo.TotalCount = cabinfo.IsChambrieren + cabinfo.Finished;
                            cabinfoList.Add(cabinfo);

                        }

                        Application.Current.Dispatcher.Invoke(() =>
                        {
                            DataList = new ObservableCollection<Cabinfo>(cabinfoList);
                        });

                        foreach (var item in lstInfoModels)
                        {
                            if (item.IsNull)
                            {
                                continue;
                            }
                            
                            //&& OverAllContext.IsWarn 结束回温
                            if (item.TemperatureEndTime <= DateTime.Now )
                            {
                                Application.Current.Dispatcher.Invoke(() => 
                                {
                                    _dialogService.ShowDialog("WarningView", new DialogParameters() { { "Text", $"机台码:{item.MachineId}结束回温!" } }, null);
                                });
                                
                                // 检测是否回温完成
                               
                            }

                            // 检测是否过期
                            if (item.ExpirationDate <= DateTime.Now.AddDays(2) && OverAllContext.IsError)
                            {
                                DialogParameters keyValuePairs = new DialogParameters();
                                keyValuePairs.Add("ChemicalNum", item.ChamName);
                                keyValuePairs.Add("ExpirationTime", item.ExpirationDate);
                                keyValuePairs.Add("CabinetNum", item.CabinetId);
                                _dialogService.ShowDialog("ErrorView", keyValuePairs, null);
                            }
                        }

                        await Task.Delay(30 * 1000);
                    }
                    catch (Exception ex)
                    {

                    }
                }
            }, _ctsRefresh.Token, TaskCreationOptions.LongRunning, TaskScheduler.Default);
        }

    }

    public class Cabinfo : BindableBase
    {
        private string chemName;
        public string ChemName
        {
            get { return chemName; }
            set { chemName = value; RaisePropertyChanged(); }
        }

        private string pNCode;
        public string PNCode
        {
            get { return pNCode; }
            set { pNCode = value; RaisePropertyChanged(); }
        }

        private int finished;
        public int Finished
        {
            get { return finished; }
            set { finished = value; RaisePropertyChanged(); }
        }

        private int isChambrieren;
        public int IsChambrieren
        {
            get { return isChambrieren; }
            set { isChambrieren = value; RaisePropertyChanged(); }
        }

        private int totalCount;
        public int TotalCount
        {
            get { return totalCount; }
            set { totalCount = value; RaisePropertyChanged(); }
        }
    }
}
