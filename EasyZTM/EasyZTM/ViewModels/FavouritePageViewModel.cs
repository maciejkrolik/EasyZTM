using EasyZTM.Models;
using EasyZTM.Services;
using System.Collections.Generic;
using Prism.Navigation;
using Prism.Commands;

namespace EasyZTM.ViewModels
{
    public class FavouritePageViewModel : ViewModelBase
    {
        private ISqlBusStopService _sqlBusStopService;
        private List<SqlBusStop> _favouriteList;
        private DelegateCommand<SqlBusStop> _itemTappedCommand;
        private INavigationService _navigationService;

        public FavouritePageViewModel(INavigationService navigationService, ISqlBusStopService sqlBusStopService)
            : base(navigationService)
        {
            Title = "Ulubione";

            _navigationService = navigationService;
            _sqlBusStopService = sqlBusStopService;

            _favouriteList = _sqlBusStopService.GetAllFavouriteStops();
        }

        public List<SqlBusStop> FavouriteList
        {
            get { return _favouriteList; }
            set { SetProperty(ref _favouriteList, value); }
        }

        public DelegateCommand<SqlBusStop> ItemTappedCommand =>
            _itemTappedCommand ?? (_itemTappedCommand = new DelegateCommand<SqlBusStop>(ExecuteItemTappedCommand));

        private void ExecuteItemTappedCommand(SqlBusStop sqlBusStop)
        {
            var navigationParams = new NavigationParameters();
            navigationParams.Add("description", sqlBusStop.Description);
            navigationParams.Add("stopId", sqlBusStop.StopId);
            _navigationService.NavigateAsync("BusStopPage", navigationParams);
        }
    }
}
