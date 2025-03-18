using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using HeBianGu.Diagram.DrawingBox;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace susalem.wpf.ViewModels
{
    public partial class DiagramViewModel : ObservableObject, INavigationAware
    {
        public bool IsNavigationTarget(NavigationContext navigationContext) => true;

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
           
        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            
        }
        [RelayCommand]
        void ItemsChanged(RoutedEventArgs args)
        {
            if (args.Source is Diagram dia)
            {
                var newNode = dia.Nodes.LastOrDefault();                                                                     
                if (newNode!=null)
                {
                    var location = dia.Nodes.Last().Location;
                    var type =newNode.Content.GetType().Name;

                }

            }
        }
    }
}
