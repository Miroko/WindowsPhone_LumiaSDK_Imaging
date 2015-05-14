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
using System.Runtime.InteropServices.WindowsRuntime;
using Microsoft.Phone.Shell;



namespace LumiaSDKApp
{
    public partial class MainPage : PhoneApplicationPage
    {

        private ApplicationBarIconButton selectImageButton;
        private ApplicationBarIconButton selectEffectButton;
        private ApplicationBarIconButton saveImageButton;

        // FilterEffect instance is used to apply different
        // filters to an image.
        // Here we will apply Cartoon filter to an image.
        private FilterEffect _cartoonEffect = null;

        // The following  WriteableBitmap contains 
        // the filtered and thumbnail images.
        private WriteableBitmap _cartoonImageBitmap = null;
        private WriteableBitmap _thumbnailImageBitmap = null;
              
        public MainPage()
        {
            InitializeComponent();

            // Init icon button, can't use x:Name in xaml for reference
            selectImageButton = (ApplicationBarIconButton)ApplicationBar.Buttons[0];
            selectEffectButton = (ApplicationBarIconButton)ApplicationBar.Buttons[1];
            saveImageButton = (ApplicationBarIconButton)ApplicationBar.Buttons[2];

            // Initialize WriteableBitmaps to render the
            // filtered and original images.
            _cartoonImageBitmap = new WriteableBitmap(1,1);
            _thumbnailImageBitmap = new WriteableBitmap(1,1);
        }

        private void SelectImage_Click(object sender, EventArgs e)
        {
            saveImageButton.IsEnabled = false;

            PhotoChooserTask chooser = new PhotoChooserTask();
            chooser.Completed += PickImageCallback;
            chooser.Show();
        }

        private void SelectEffect_Click(object sender, EventArgs e)
        {

        }

        private async void SaveImage_Click(object sender, EventArgs e)
        {
            saveImageButton.IsEnabled = false;

            if (_cartoonEffect == null)
            {
                return;
            }

            var jpegRenderer = new JpegRenderer(_cartoonEffect);

            // Jpeg renderer gives the raw buffer for the filtered image.
            IBuffer jpegOutput = await jpegRenderer.RenderAsync();

            // Save the image as a jpeg to the saved pictures album.
            MediaLibrary library = new MediaLibrary();
            string fileName = string.Format("CartoonImage_{0:G}", DateTime.Now);
            var picture = library.SavePicture(fileName, jpegOutput.AsStream());

            MessageBox.Show("Image saved!");

            saveImageButton.IsEnabled = true;
        }

        private async void PickImageCallback(object sender, PhotoResult e)
        {
            if (e.TaskResult != TaskResult.OK || e.ChosenPhoto == null)
            {
                return;
            }

            try
            {
                // Show the thumbnail of the original image.
                _thumbnailImageBitmap.SetSource(e.ChosenPhoto);
                _cartoonImageBitmap.SetSource(e.ChosenPhoto);
                OriginalImage.Source = _thumbnailImageBitmap;

                // Rewind the stream to the start.                     
                e.ChosenPhoto.Position = 0;

                // A cartoon effect is initialized with  the selected image stream as the source.
                var imageStream = new StreamImageSource(e.ChosenPhoto);
                _cartoonEffect = new FilterEffect(imageStream);

                // Add the cartoon filter as the only filter for the effect.
                var cartoonFilter = new CartoonFilter();
                _cartoonEffect.Filters = new[] { cartoonFilter };

                // Render the image to a WriteableBitmap.
                var renderer = new WriteableBitmapRenderer(_cartoonEffect, _cartoonImageBitmap);
                _cartoonImageBitmap = await renderer.RenderAsync();

                // Set the rendered image as the source for the cartoon image control.
                CartoonImage.Source = _cartoonImageBitmap;
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
                return;
            }

            saveImageButton.IsEnabled = true;
        }
    }
}