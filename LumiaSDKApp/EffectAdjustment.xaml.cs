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
            ImageController.INSTANCE.SetCurrentImage(ImageInEdit);
            ImageController.INSTANCE.SetControls(FilterControls);
            ImageController.INSTANCE.UpdateImage();
        }
    }
}