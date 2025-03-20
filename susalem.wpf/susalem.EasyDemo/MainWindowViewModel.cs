using HslCommunication.ModBus;
using Prism.Commands;
using Prism.Ioc;
using Prism.Mvvm;
using Prism.Regions;
using susalem.EasyDemo.Entities;
using susalem.EasyDemo.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace susalem.EasyDemo
{
    public class MainWindowViewModel : BindableBase
    {
        private readonly IRegionManager _regionManager;
        private readonly ICabinetInfoService _cabinetInfoService;
        private CancellationTokenSource cts = new CancellationTokenSource();


        public MainWindowViewModel(IRegionManager regionManager, ICabinetInfoService cabinetInfoService)
        {
            _regionManager = regionManager;
           
            //OverAllContext.modbusTcpServer = new ModbusTcpServer();
            //OverAllContext.modbusTcpServer.ServerStart(502, true);

            OverAllContext.ModbusTcpLock = new ModbusTcpNet("192.168.1.102", 502);
            OverAllContext.ModbusTcpStatusLight = new ModbusTcpNet("192.168.1.101", 502);
            OverAllContext.ModbusTcpDoor = new ModbusTcpNet("192.168.1.100", 502);
            _cabinetInfoService = cabinetInfoService;

            RefreshLight();
            RefreshIsTemperaturing();
            
        }

        /// <summary>
        /// 30s更新灯信号
        /// </summary>
        private void RefreshLight()
        {
            Task.Factory.StartNew(async () =>
            {
                // 柜子空的时候不亮灯，回温中红灯，回温结束绿灯
                // 遍历所有柜子的回温信息，如果回温完成写入灯的状态
                // 如果过保质期需要弹窗，回温结束也弹窗
                while (!cts.IsCancellationRequested)
                {
                    try
                    {
                        //TODO 优化方案： 根据历史柜子操作记录来更新灯信号
                        List<CabinetInfoModel> cabinetInfoModelList = _cabinetInfoService.FindAllCabinetInfos();
                        if (cabinetInfoModelList == null || cabinetInfoModelList.Count == 0)
                        {
                            await Task.Delay(30 * 1000);
                            continue;
                        }

                        foreach (var cabinetInfoModel in cabinetInfoModelList)
                        {
                            //TODO 如何熄灭灯
                            if (cabinetInfoModel.IsNull)
                            {
                                await OverAllContext.ModbusTcpLock.WriteAsync(cabinetInfoModel.GreenLightAddress, false);
                            }
                            else
                            {
                                if (cabinetInfoModel.IsTemperaturing)
                                {
                                    await OverAllContext.ModbusTcpLock.WriteAsync(cabinetInfoModel.RedLightAddress, true);
                                }
                                else
                                {
                                    await OverAllContext.ModbusTcpLock.WriteAsync(cabinetInfoModel.GreenLightAddress, true);
                                }
                            }
                        }


                    }
                    catch (Exception ex)
                    {

                    }
                    await Task.Delay(30 * 1000);
                }


            }, cts.Token, TaskCreationOptions.LongRunning, TaskScheduler.Default);
        }

        /// <summary>
        /// 30s刷新回温状态  
        /// </summary>
        private void RefreshIsTemperaturing()
        {
            Task.Factory.StartNew(async () =>
            {
                while (!cts.IsCancellationRequested)
                {
                    try
                    {
                        List<CabinetInfoModel> cabinetInfoModelList = _cabinetInfoService.FindAllCabinetInfos().Where(p => !p.IsNull).ToList();
                        if (cabinetInfoModelList == null || cabinetInfoModelList.Count == 0)
                        {
                            await Task.Delay(30 * 1000);
                            continue;
                        }

                        foreach (var cabinetInfoModel in cabinetInfoModelList)
                        {

                            if (cabinetInfoModel.IsTemperaturing && DateTime.Now >= cabinetInfoModel.TemperatureEndTime)
                            {
                                cabinetInfoModel.IsTemperaturing = false;
                            }
                        }

                        await _cabinetInfoService.EditCabinetInfoList(cabinetInfoModelList);
                    }
                    catch (Exception ex)
                    {

                    }

                    await Task.Delay(30 * 1000);
                }
            }, cts.Token, TaskCreationOptions.LongRunning, TaskScheduler.Default);
        }

        /// <summary>
        /// 页面加载跳转实时柜子界面
        /// </summary>
        public ICommand PageLoaded
        {
            get => new DelegateCommand(() =>
            {
                var region = _regionManager.Regions["MainRegion"];
                //给region添加事件，当视图跳转完成触发
                region.NavigationService.Navigated += OnNavigated;
                _regionManager.Regions["MainRegion"].RequestNavigate("LoginRecordView");
                
            });
        }

        public ICommand ParaSettingCommand
        {
            get => new DelegateCommand(() =>
            {
                NavigationParameters keyValuePairs = new NavigationParameters();
                //keyValuePairs.Add("Menu", menuItem);
                _regionManager.Regions["MainRegion"].RequestNavigate("ParameterSettingView", keyValuePairs);
            });
        }

        public ICommand LoginCommand
        {
            get => new DelegateCommand(() =>
            {
                NavigationParameters keyValuePairs = new NavigationParameters();
                //keyValuePairs.Add("Menu", menuItem);
                _regionManager.Regions["MainRegion"].RequestNavigate("LoginRecordView", keyValuePairs);

            });
        }

        public ICommand ChambrierenCommand
        {
            get => new DelegateCommand(() =>
            {
                NavigationParameters keyValuePairs = new NavigationParameters();
                //keyValuePairs.Add("Menu", menuItem);
                _regionManager.Regions["MainRegion"].RequestNavigate("ChambrierenView", keyValuePairs);
            });
        }

        public ICommand OperateCommand
        {
            get => new DelegateCommand(() =>
            {
                NavigationParameters keyValuePairs = new NavigationParameters();
                //keyValuePairs.Add("Menu", menuItem);
                _regionManager.Regions["MainRegion"].RequestNavigate("OperateMachineView", keyValuePairs);
            });
        }

        public ICommand HistoryCommand
        {
            get => new DelegateCommand(() =>
            {
                NavigationParameters keyValuePairs = new NavigationParameters();
                //keyValuePairs.Add("Menu", menuItem);
                _regionManager.Regions["MainRegion"].RequestNavigate("HistoryRecordView", keyValuePairs);
            });
        }

        public ICommand AlarmCommand
        {
            get => new DelegateCommand(() =>
            {
                NavigationParameters keyValuePairs = new NavigationParameters();
                //keyValuePairs.Add("Menu", menuItem);
                //_regionManager.Regions["MainRegion"].RequestNavigate("AlarmRecordView", keyValuePairs);
                _regionManager.Regions["MainRegion"].RequestNavigate("CurrentCabinetView", keyValuePairs);

            });
        }




        private void OnNavigated(object sender, RegionNavigationEventArgs e)
        {
            // 检查导航跳转条件
            if (ShouldCancelNavigation(e.Uri))
            {
                NavigationParameters keyValuePairs = new NavigationParameters();
                _regionManager.Regions["MainRegion"].RequestNavigate("LoginRecordView", keyValuePairs);
                MessageBox.Show("请先登录账号");
                //e.Cancel = true; // 取消导航
                // 可以在此处添加提示逻辑
            }
        }

        private bool ShouldCancelNavigation(Uri uri)
        {
            // 账号为空并且uri不是登录界面时，取消导航
            if (OverAllContext.User == null && !uri.OriginalString.Equals("LoginRecordView"))
            {
                return true;
            }
            return false;
        }


    }
}
