using Lumia.Imaging;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using Windows.Storage.Streams;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Windows;
using System.Windows.Media;

namespace LumiaSDKApp.Classes
{
    class EditableImage
    {
        private Stream sourceStream;

        private WriteableBitmap bitmap;

        public EditableImage(Stream source)
        {
            bitmap = new WriteableBitmap(1,1);
            SetSource(source);
        }

        private void SetSource(Stream stream)
        {
            this.sourceStream = stream;
            bitmap.SetSource(sourceStream);
            sourceStream.Position = 0;  
        }

        public Stream GetSourceStream()
        {
            return sourceStream;
        }

        public async Task RenderToImage(Image image, Filter filter)
        {
            sourceStream.Position = 0;

            using (StreamImageSource source = new StreamImageSource(sourceStream))
            using (FilterEffect filterEffect = new FilterEffect(source))
            using (WriteableBitmapRenderer renderer = new WriteableBitmapRenderer(filterEffect, bitmap))
            {
                // Set filters
                if (filter != null)
                {
                    filterEffect.Filters = filter.GetFilters();
                }
                // Render filters to bitmap
                await renderer.RenderAsync();
                image.Source = bitmap;
            } 
        }

        public async Task Render(Filter filter)
        {
            sourceStream.Position = 0;

            using (StreamImageSource source = new StreamImageSource(sourceStream))
            using (FilterEffect filterEffect = new FilterEffect(source))
            using (WriteableBitmapRenderer renderer = new WriteableBitmapRenderer(filterEffect, bitmap))
            {
                // Set filters
                if (filter != null)
                {
                    filterEffect.Filters = filter.GetFilters();
                }
                // Render filters to bitmap
                await renderer.RenderAsync();
            } 
        }

        public async Task Save(Filter filter)
        {
            sourceStream.Position = 0;

            using (StreamImageSource source = new StreamImageSource(sourceStream))
            using (FilterEffect filterEffect = new FilterEffect(source))
            using (JpegRenderer renderer = new JpegRenderer(filterEffect))
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

        public ImageSource GetBitmap()
        {
            return bitmap;
        }
    }
}
