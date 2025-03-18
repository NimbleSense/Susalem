using Prism.Ioc;
using Prism.Unity;
using susalem.wpf.Constants;
using susalem.wpf.ViewModels;
using susalem.wpf.ViewModels.ListViewModels;
using susalem.wpf.Views;
using susalem.wpf.Views.ListViews;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace susalem.wpf
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : PrismApplication
    {
        protected override Window CreateShell()
        {
//#if DEBUG
//            return new TravelWindow();
//#else
            //var login = Container.Resolve<LoginWindow>();
            //var result= login.ShowDialog();
            //if (result is null||!result.Value)
            //{
            //    App.Current.Shutdown();
            //    return null;
            //}
            //else
            //{
            //    ShutdownMode = ShutdownMode.OnMainWindowClose;
                return Container.Resolve<ShellWindow>();
            //}
//#endif
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.Register<LoginWindow>();
            containerRegistry.Register<LoginWindowViewModel>();
            containerRegistry.RegisterForNavigation<ShellWindow, ShellWindowViewModel>();
            containerRegistry.RegisterForNavigation<MachineListView, MachineListViewModel>(ListPages.Machine);
            containerRegistry.RegisterForNavigation<AlarmLightsView, AlarmLightsViewModel>(ListPages.Alarm);
            containerRegistry.RegisterForNavigation<MachineDetailView, MachineDetailViewModel>(DetailPages.Machine);
            containerRegistry.RegisterForNavigation<DigitalBoardView, DigitalBoardViewModel>(ListPages.DigitalBoard);  // 看板
            containerRegistry.RegisterForNavigation<DiagramView, DiagramViewModel>();

        }
    }
}
