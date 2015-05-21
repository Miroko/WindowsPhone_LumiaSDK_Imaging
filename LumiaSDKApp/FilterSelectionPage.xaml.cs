using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using LumiaSDKApp.Classes;
using LumiaSDKApp.Filters;
using System.Threading.Tasks;

namespace LumiaSDKApp
{
    public partial class FilterSelectionPage : PhoneApplicationPage
    {
        public FilterSelectionPage()
        {
            InitializeComponent();

            ImageEditor.INSTANCE.filterList.PopulateFiltersList(AllFilters);
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            AllFilters.SelectedIndex = -1;
        }

        private async void AllFilters_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Object selection = AllFilters.SelectedItem;
            if (selection != null)
            {
                // Render selected filter
                ImageEditor.INSTANCE.filterController.SetCurrentFilter(ImageEditor.INSTANCE.filterList.filters[AllFilters.SelectedIndex]);
                await ImageEditor.INSTANCE.RenderImage();

                NavigationService.Navigate(new Uri("/EffectAdjustment.xaml", UriKind.Relative));
            }
        }
    }
}