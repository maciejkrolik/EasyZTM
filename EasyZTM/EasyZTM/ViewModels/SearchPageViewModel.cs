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
        private INavigationService _navigationService;
        private ISqlBusStopService _sqlBusStopService;

        public SearchPageViewModel(INavigationService navigationService, ISqlBusStopService sqlBusStopService)
            : base(navigationService)
        {
            Title = "Szukaj";
            _navigationService = navigationService;
            _sqlBusStopService = sqlBusStopService;
        }

        public string Keyword
        {
            get { return _keyword; }
            set { SetProperty(ref _keyword, value); }
        }

        private List<SqlBusStop> _suggestions;
        public List<SqlBusStop> Suggestions
        {
            get { return _suggestions; }
            set { SetProperty(ref _suggestions, value); }
        }

        private DelegateCommand _searchCommand;
        public DelegateCommand SearchCommand =>
            _searchCommand ?? (_searchCommand = new DelegateCommand(ExecuteSearchCommand));

        private void ExecuteSearchCommand()
        {
            Suggestions = _sqlBusStopService.GetAllStops().Where(x => x.Description
                                                          .ToLower()
                                                          .Contains(_keyword.ToLower()))
                                                          .ToList();
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
