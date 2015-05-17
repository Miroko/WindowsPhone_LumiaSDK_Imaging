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

            PopulateFiltersList();

        }

        private async void PopulateFiltersList()
        {
            foreach (Filter filter in ImageController.INSTANCE.GetAllFilters())
            {
                ListBoxItem item = new ListBoxItem();

                TextBlock label = new TextBlock();
                label.FontSize = 32;
                label.Text = filter.Name;
                label.Margin = new Thickness(12);

                Image img = new Image();
                EditableImage editableImage1 = new EditableImage(img, ImageManipulator.INSTANCE.sourceStream);
                editableImage1.SetFilter(filter);
                await editableImage1.Render();

                StackPanel stack = new StackPanel();
                stack.Children.Add(label);
                stack.Children.Add(img);

                item.Content = stack;

                AllFilters.Items.Add(item);
            }
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            AllFilters.SelectedIndex = -1;
        }

        private void AllFilters_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Object selection = AllFilters.SelectedItem;
            if (selection != null)
            {
                // Set selected filter
                ImageController.INSTANCE.SetCurrentFilter(ImageController.INSTANCE.GetAllFilters()[AllFilters.SelectedIndex]);

                // Navigate to adjustments
                NavigationService.Navigate(new Uri("/EffectAdjustment.xaml", UriKind.Relative));
            }
        }
    }
}