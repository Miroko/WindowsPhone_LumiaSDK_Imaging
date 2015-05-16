using Lumia.Imaging;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Windows.Storage.Streams;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Windows;

namespace LumiaSDKApp
{
    class ImageManipulator
    {
        public static ImageManipulator INSTANCE = new ImageManipulator();

        private WriteableBitmap bitmap;

        public Stream sourceStream;
  
        public ImageManipulator()
        {
            bitmap = new WriteableBitmap(0,0);
        }

        public void SetSourceStream(Stream sourceStream)
        {
            this.sourceStream = sourceStream;
            bitmap.SetSource(sourceStream);
            sourceStream.Position = 0;           
        }

        public async Task SetStreamToImage(Image image, Filter filter)
        {
            sourceStream.Position = 0;

            using(StreamImageSource source = new StreamImageSource(sourceStream))
            using(FilterEffect filterEffect = new FilterEffect(source))
            {
                // Set filters
                if (filter != null)
                {
                    filterEffect.Filters = filter.GetFilters();
                }
                // Render filters to bitmap
                WriteableBitmapRenderer renderer = new WriteableBitmapRenderer(filterEffect, bitmap);
                WriteableBitmap bitmapWithEffects = await renderer.RenderAsync();
                image.Source = bitmapWithEffects;
            } 
        }

        public async Task SaveStreamToImage(Filter filter)
        {
            sourceStream.Position = 0;

            using(StreamImageSource source = new StreamImageSource(sourceStream))
            using(FilterEffect filterEffect = new FilterEffect(source))
            using(JpegRenderer renderer = new JpegRenderer(filterEffect))
            {
                // Set filters
                if (filter != null)
                {
                    filterEffect.Filters = filter.GetFilters();
                }

                // Output buffer
                IBuffer output = await renderer.RenderAsync();

                // Save to library
                MediaLibrary library = new MediaLibrary();
                string fileName = string.Format("Image_{0:G}", DateTime.Now);
                var picture = library.SavePicture(fileName, output.AsStream());

                MessageBox.Show("Image saved");
            }
        }
    }
}
