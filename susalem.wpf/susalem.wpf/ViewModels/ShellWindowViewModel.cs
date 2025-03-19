using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using Prism.Regions;
using susalem.wpf.Common;
using susalem.wpf.Constants;
using susalem.wpf.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Navigation;

namespace susalem.wpf.ViewModels
{
    public partial class ShellWindowViewModel : ObservableRecipient
    {
        [ObservableProperty]
        [NotifyPropertyChangedRecipients]
        bool systemMode;

        IRegionNavigationService _navigationService;

        MetroWindow shell;

        [ObservableProperty]
        MenuItem selectedItem;

        [ObservableProperty]
        ObservableCollection<MenuItem> menu = new()
        {
            new(){Page=ListPages.Machine,Icon="CubesSolid"},
            new(){Page=ListPages.Message,Icon="CommentDotsRegular"},
            new(){Page=ListPages.Community,Icon="CommentsRegular"},
            new(){Page=ListPages.Enterprise,Icon="BuildingRegular"},
            new(){Page=ListPages.DigitalBoard,Icon="ChartAreaSolid"},
        };
        
        [RelayCommand]
        void Loaded(MetroWindow metroWindow)
        {
            shell = metroWindow;
            _navigationService.RequestNavigate(nameof(DiagramView));
        }

        [RelayCommand]
        void Navigate()
        {
            _navigationService.RequestNavigate(SelectedItem.Page);
        }

        public ShellWindowViewModel(IRegionManager regionManager)
        {
            _navigationService = regionManager.Regions[Regions.List].NavigationService;
            regionManager.Regions[Regions.Pane].NavigationService.RequestNavigate(ListPages.Alarm);

        }
    }
}
