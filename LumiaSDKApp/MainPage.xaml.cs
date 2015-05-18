using Lumia.Imaging;
using Lumia.Imaging.Artistic;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Tasks;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Media.Imaging;
using Windows.Storage.Streams;
using Microsoft.Phone.Shell;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Diagnostics;

namespace LumiaSDKApp
{
    public partial class MainPage : PhoneApplicationPage
    {
        private ApplicationBarIconButton selectImageButton;
        private ApplicationBarIconButton selectEffectButton;
        private ApplicationBarIconButton saveImageButton;

        public MainPage()
        {
            InitializeComponent();
           
            // Init icon button, can't use x:Name in xaml for reference
            selectImageButton = (ApplicationBarIconButton)ApplicationBar.Buttons[0];
            selectEffectButton = (ApplicationBarIconButton)ApplicationBar.Buttons[1];
            saveImageButton = (ApplicationBarIconButton)ApplicationBar.Buttons[2];

            // Pick image
            OpenImagePicker();
        }

        private void LoadImage()
        {           
            ImageController.INSTANCE.SetCurrentImage(ImageInEdit);
            ImageController.INSTANCE.UpdateImage();

            selectEffectButton.IsEnabled = true;
            saveImageButton.IsEnabled = true;
        }

        private void OpenImagePicker()
        {
            PhotoChooserTask chooser = new PhotoChooserTask();
            chooser.Completed += OnPictureChosen;
            chooser.Show();
        }

        private void OnPictureChosen(object sender, PhotoResult e)
        {
            if (e.TaskResult == TaskResult.OK && e.ChosenPhoto != null)
            {
                ImageManipulator.INSTANCE.SetSourceStream(e.ChosenPhoto);
                ImageController.INSTANCE.SetCurrentFilter(null);
                LoadImage();
            }
        }

        private void ClickSelectImage(object sender, EventArgs e)
        {
            OpenImagePicker();
        }

        private void ClickSelectEffect(object sender, EventArgs e)
        {
            //NavigationService.Navigate(new Uri("/EffectsPage.xaml", UriKind.Relative));
            NavigationService.Navigate(new Uri("/FilterSelectionPage.xaml", UriKind.Relative));
        }

        private void ClickSaveImage(object sender, EventArgs e)
        {
            ImageController.INSTANCE.SaveImage();
        }

        private bool ImageSelected()
        {
            return ImageManipulator.INSTANCE.sourceStream != null;
        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            if (ImageSelected())
            {
                LoadImage();
            }
        }
    }
}