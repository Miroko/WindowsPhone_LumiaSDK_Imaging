using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System.Threading.Tasks;
using LumiaSDKApp.Classes;

namespace LumiaSDKApp
{
    public partial class EffectAdjustment : PhoneApplicationPage
    {
        public EffectAdjustment()
        {
            InitializeComponent();
        }

        private void ClickFinishEditing(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/MainPage.xaml", UriKind.Relative));

            while (NavigationService.RemoveBackEntry() != null);
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            FilterName.Text = ImageEditor.INSTANCE.filterController.GetCurrentFilter().Name;
            ImageEditor.INSTANCE.filterController.SetControls(FilterControls); 

            ImageEditor.INSTANCE.SetViewImage(ImageInEdit);
            ImageEditor.INSTANCE.UpdateImage();
            
        }
    }
}