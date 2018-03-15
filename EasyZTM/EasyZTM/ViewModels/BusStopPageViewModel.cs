using EasyZTM.Models;
using EasyZTM.Services;
using System.Collections.Generic;
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
            IsLoading = true;

            _sqlBusStop = (SqlBusStop)parameters["busStop"];

            Title = $"{_sqlBusStop.Description} ({_sqlBusStop.StopId.ToString()})";
            SetFavouriteImage();

            try
            {
                BusList = await _jsonBusStopService.GetAllBusesAsync(_sqlBusStop.StopId);
            }
            catch (System.Net.Http.HttpRequestException)
            {
                Title = "Błąd serwera";
            }

            IsLoading = false;
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

        private List<Delay> _busList;
        public List<Delay> BusList
        {
            get { return _busList; }
            set { SetProperty(ref _busList, value); }
        }

        private bool _isLoading;
        public bool IsLoading
        {
            get { return _isLoading; }
            set { SetProperty(ref _isLoading, value); }
        }

        private bool _isListVisible;
        public bool IsListVisible
        {
            get { return _isListVisible; }
            set { SetProperty(ref _isListVisible, value); }
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
    }

    public class AddToFavouriteEvent : PubSubEvent
    { };
}
