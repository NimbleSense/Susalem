using Prism.Ioc;
using Prism.Regions;
using Prism.Unity;
using susalem.EasyDemo.Services;
using susalem.EasyDemo.ViewModels;
using susalem.EasyDemo.ViewModels.Dialogs;
using susalem.EasyDemo.Views;
using susalem.EasyDemo.Views.Dialogs;
using System.Configuration;
using System.Data;
using System.Windows;

namespace susalem.EasyDemo
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : PrismApplication
    {
        protected override Window CreateShell()
        {
            return Container.Resolve<MainWindow>();
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.Register<IUserService, UserService>();
            containerRegistry.Register<IRoleService, RoleService>();
            containerRegistry.Register<IHistoryService, HistoryService>();
            containerRegistry.Register<IChamParaService, ChamParaService>();
            containerRegistry.Register<ICabinetInfoService, CabinetInfoService>();

            containerRegistry.RegisterForNavigation<MainWindow, MainWindowViewModel>();

            containerRegistry.RegisterForNavigation<AlarmRecordView, AlarmRecordViewModel>();
            containerRegistry.RegisterForNavigation<ChambrierenView, ChambrierenViewModel>();
            containerRegistry.RegisterForNavigation<HistoryRecordView, HistoryRecordViewModel>();
            containerRegistry.RegisterForNavigation<LoginRecordView, LoginRecordViewModel>();
            containerRegistry.RegisterForNavigation<OperateMachineView, OperateMachineViewModel>();
            containerRegistry.RegisterForNavigation<ParameterSettingView, ParameterSettingViewModel>();
            containerRegistry.RegisterForNavigation<CurrentCabinetView, CurrentCabinetViewModel>();


            containerRegistry.RegisterDialog<ErrorView, ErrorViewModel>();
            containerRegistry.RegisterDialog<WarningView, WarningViewModel>();
            containerRegistry.RegisterDialog<AddUserView, AddUserViewModel>();
            containerRegistry.RegisterDialog<MessageView, MessageViewModel>();

        }
    }

}
