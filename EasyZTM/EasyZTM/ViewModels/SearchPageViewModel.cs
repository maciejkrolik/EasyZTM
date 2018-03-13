using EasyZTM.Models;
using EasyZTM.Services;
using System.Collections.Generic;
using System.Linq;
using Prism.Commands;
using Prism.Navigation;

namespace EasyZTM.ViewModels
{
    public class SearchPageViewModel : ViewModelBase
    {
        private string _keyword;
        private List<SqlBusStop> _suggestions;
        private DelegateCommand _searchCommand;
        private DelegateCommand<SqlBusStop> _itemTappedCommand;
        private INavigationService _navigationService;
        private readonly List<SqlBusStop> _busStopList;

        public SearchPageViewModel(INavigationService navigationService, ISqlBusStopService sqlBusStopService)
            : base(navigationService)
        {
            Title = "Szukaj";
            _navigationService = navigationService;

            _busStopList = sqlBusStopService.GetAllStops();
        }

        public string Keyword
        {
            get { return _keyword; }
            set { SetProperty(ref _keyword, value); }
        }

        public List<SqlBusStop> Suggestions
        {
            get { return _suggestions; }
            set { SetProperty(ref _suggestions, value); }
        }

        public DelegateCommand SearchCommand =>
            _searchCommand ?? (_searchCommand = new DelegateCommand(ExecuteSearchCommand));

        private void ExecuteSearchCommand()
        {
            Suggestions = _busStopList.Where(x => x.Description
                                                   .ToLower()
                                                   .Contains(_keyword.ToLower()))
                                                   .ToList();
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
