using EasyZTM.Models;
using EasyZTM.Services;
using System.Collections.Generic;
using Prism.Commands;
using Prism.Events;
using Prism.Navigation;

namespace EasyZTM.ViewModels
{
    public class FavouritePageViewModel : ViewModelBase
    {
        private ISqlBusStopService _sqlBusStopService;
        private INavigationService _navigationService;
        private IEventAggregator _eventAggregator;

        public FavouritePageViewModel(INavigationService navigationService, ISqlBusStopService sqlBusStopService,
                                      IEventAggregator eventAggregator)
            : base(navigationService)
        {
            Title = "Ulubione";

            _navigationService = navigationService;
            _sqlBusStopService = sqlBusStopService;
            _eventAggregator = eventAggregator;

            _eventAggregator.GetEvent<AddToFavouriteEvent>().Subscribe(UpdateFavouriteList);

            UpdateFavouriteList();
        }

        private void UpdateFavouriteList()
        {
            FavouriteList = _sqlBusStopService.GetAllFavouriteStops();
        }

        private List<SqlBusStop> _favouriteList;
        public List<SqlBusStop> FavouriteList
        {
            get { return _favouriteList; }
            set { SetProperty(ref _favouriteList, value); }
        }

        private DelegateCommand<SqlBusStop> _itemTappedCommand;
        public DelegateCommand<SqlBusStop> ItemTappedCommand =>
            _itemTappedCommand ?? (_itemTappedCommand = new DelegateCommand<SqlBusStop>(ExecuteItemTappedCommand));

        private void ExecuteItemTappedCommand(SqlBusStop sqlBusStop)
        {
            var navigationParams = new NavigationParameters();
            navigationParams.Add("busStop", sqlBusStop);
            _navigationService.NavigateAsync("BusStopPage", navigationParams);
        }
    }
}
