using EasyZTM.Models;
using EasyZTM.Services;
using System.Collections.Generic;
using Prism.Navigation;

namespace EasyZTM.ViewModels
{
    public class FavouritePageViewModel : ViewModelBase
    {
        private ISqlBusStopService _sqlBusStopService;
        private List<SqlBusStop> _favouriteList;

        public FavouritePageViewModel(INavigationService navigationService, ISqlBusStopService sqlBusStopService)
            : base(navigationService)
        {
            Title = "Ulubione";

            _sqlBusStopService = sqlBusStopService;

            _favouriteList = _sqlBusStopService.GetAllFavouriteStops();
        }

        public List<SqlBusStop> FavouriteList
        {
            get { return _favouriteList; }
            set { SetProperty(ref _favouriteList, value); }
        }
    }
}
