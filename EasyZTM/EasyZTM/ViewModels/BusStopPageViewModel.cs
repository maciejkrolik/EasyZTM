using EasyZTM.Models;
using EasyZTM.Services;
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
        private string _busStopDescription;
        private int _busStopId;
        private IJsonBusStopService _jsonBusStopService;
        private List<Delay> _busList;

        public BusStopPageViewModel(INavigationService navigationService, IJsonBusStopService jsonBusStopService)
            : base(navigationService)
        {
            _jsonBusStopService = jsonBusStopService;
        }

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

        public async override void OnNavigatedTo(NavigationParameters parameters)
        {
            if (parameters.ContainsKey("description"))
            {
                IsListVisible = false;
                IsLoading = true;

                _busStopDescription = (string)parameters["description"];
                _busStopId = (int)parameters["stopId"];

                Title = $"{_busStopDescription} ({_busStopId.ToString()})";

                BusList = await _jsonBusStopService.GetAllBusesAsync(_busStopId);

                IsLoading = false;
                IsListVisible = true;
            }
        }
    }
}
