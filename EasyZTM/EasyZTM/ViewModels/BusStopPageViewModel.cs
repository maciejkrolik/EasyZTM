using EasyZTM.Models;
using EasyZTM.Services;
using System.Collections.Generic;
using System.Threading.Tasks;
using Prism.Commands;
using Prism.Events;
using Prism.Navigation;

namespace EasyZTM.ViewModels
{
    public class BusStopPageViewModel : ViewModelBase
    {
        private SqlBusStop _sqlBusStop;
        private IJsonBusStopService _jsonBusStopService;
        private ISqlBusStopService _sqlBusStopService;
        private IEventAggregator _eventAggregator;

        public BusStopPageViewModel(INavigationService navigationService, IJsonBusStopService jsonBusStopService,
                                    ISqlBusStopService sqlBusStopService, IEventAggregator eventAggregator)
            : base(navigationService)
        {
            _jsonBusStopService = jsonBusStopService;
            _sqlBusStopService = sqlBusStopService;
            _eventAggregator = eventAggregator;
        }

        public async override void OnNavigatedTo(NavigationParameters parameters)
        {
            IsListVisible = false;
            IsBusy = true;

            _sqlBusStop = (SqlBusStop)parameters["busStop"];

            SetFavouriteImage();
            await PopulateListView();

            IsBusy = false;
            IsListVisible = true;
        }

        private void SetFavouriteImage()
        {
            if (!_sqlBusStop.isFavourite)
            {
                ImgPath = "ic_favorite_border.png";
            }
            else
            {
                ImgPath = "ic_favorite.png";
            }
        }

        private async Task PopulateListView()
        {
            int execCounter = 0;
            while (execCounter < 3)
            {
                try
                {
                    SetTitle();
                    BusList = await _jsonBusStopService.GetAllBusesAsync(_sqlBusStop.StopId);
                    execCounter++;
                    break;
                }
                catch (System.Net.Http.HttpRequestException)
                {
                    Title = "Błąd serwera";
                    execCounter++;
                }
            }
        }

        private void SetTitle()
        {
            Title = $"{_sqlBusStop.Description} ({_sqlBusStop.StopId.ToString()})";
        }

        private List<Delay> _busList;
        public List<Delay> BusList
        {
            get { return _busList; }
            set { SetProperty(ref _busList, value); }
        }

        private bool _isBusy;
        public bool IsBusy
        {
            get { return _isBusy; }
            set { SetProperty(ref _isBusy, value); }
        }

        private bool _isListVisible;
        public bool IsListVisible
        {
            get { return _isListVisible; }
            set { SetProperty(ref _isListVisible, value); }
        }

        private bool _isPullToRefreshActive;
        public bool IsPullToRefreshActive
        {
            get { return _isPullToRefreshActive; }
            set { SetProperty(ref _isPullToRefreshActive, value); }
        }

        private string _imgPath;
        public string ImgPath
        {
            get { return _imgPath; }
            set { SetProperty(ref _imgPath, value); }
        }

        private DelegateCommand _favouriteButtonClicked;
        public DelegateCommand FavouriteButtonClicked =>
            _favouriteButtonClicked ?? (_favouriteButtonClicked = new DelegateCommand(ExecuteFavouriteButtonClicked));

        private void ExecuteFavouriteButtonClicked()
        {
            if (_sqlBusStop.isFavourite)
            {
                _sqlBusStopService.DeleteBusStopFromFavourite(_sqlBusStop.StopId);
                _sqlBusStop.isFavourite = false;
                SetFavouriteImage();
            }
            else
            {
                _sqlBusStopService.AddBusStopToFavourites(_sqlBusStop.StopId);
                _sqlBusStop.isFavourite = true;
                SetFavouriteImage();
            }

            _eventAggregator.GetEvent<AddToFavouriteEvent>().Publish();
        }

        private DelegateCommand _refreshCommand;
        public DelegateCommand RefreshCommand =>
            _refreshCommand ?? (_refreshCommand = new DelegateCommand(ExecuteRefreshCommand));

        private async void ExecuteRefreshCommand()
        {
            await PopulateListView();
            IsPullToRefreshActive = false;
        }
    }

    public class AddToFavouriteEvent : PubSubEvent
    { };
}
