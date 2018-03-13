using Xamarin.Forms;

namespace EasyZTM.Views
{
    public partial class BusStopPage : ContentPage
    {
        public BusStopPage()
        {
            InitializeComponent();
        }

        //Unselecting an item from the list when tapped
        private void ListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            ((ListView)sender).SelectedItem = null;
        }
    }
}
