using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EasyZTM.ViewModels
{
    public class BusStopPageViewModel : ViewModelBase
    {
        public BusStopPageViewModel(INavigationService navigationService)
            : base(navigationService)
        {
        }

        public override void OnNavigatedTo(NavigationParameters parameters)
        {
            if (parameters.ContainsKey("description"))
            {
                Title = $"{(string)parameters["description"]} ({parameters["stopId"].ToString()})";
            }
        }
    }
}
