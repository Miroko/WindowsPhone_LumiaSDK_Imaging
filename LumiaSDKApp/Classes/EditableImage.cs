using Lumia.Imaging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace LumiaSDKApp.Classes
{
    class EditableImage
    {
        private Stream sourceStream;

        private WriteableBitmap bitmap;

        private Filter filter;

        private Image image;

        public EditableImage(Image image, Stream source)
        {
            this.image = image;
            bitmap = new WriteableBitmap(1,1);
            SetSource(source);
        }

        internal void SetFilter(Filter filter)
        {
            this.filter = filter;
        }

        private void SetSource(Stream stream)
        {
            this.sourceStream = stream;
            bitmap.SetSource(sourceStream);
            sourceStream.Position = 0;  
        }

        public async Task Render()
        {
            sourceStream.Position = 0;

            using (StreamImageSource source = new StreamImageSource(sourceStream))
            using (FilterEffect filterEffect = new FilterEffect(source))
            {
                // Set filters
                if (filter != null)
                {
                    filterEffect.Filters = filter.GetFilters();
                }
                // Render filters to bitmap
                WriteableBitmapRenderer renderer = new WriteableBitmapRenderer(filterEffect, bitmap);
                await renderer.RenderAsync();
                image.Source = bitmap;
            } 
        }
    }
}
