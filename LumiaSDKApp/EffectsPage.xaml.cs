using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Lumia.Imaging;
using System.Collections.ObjectModel;
using LumiaSDKApp.Filters;

namespace LumiaSDKApp
{
    public partial class EffectsPage : PhoneApplicationPage
    {
        public EffectsPage()
        {
            InitializeComponent();

            // Set list data source
            AllFilters.DataContext = ImageController.INSTANCE.GetAllFilters();            
        }

        private void AllFilters_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Set selected filter
            ImageController.INSTANCE.SetCurrentFilter((Filter)AllFilters.SelectedItem);

            // Navigate to adjustments
            NavigationService.Navigate(new Uri("/EffectAdjustment.xaml", UriKind.Relative));
        }

    }
}