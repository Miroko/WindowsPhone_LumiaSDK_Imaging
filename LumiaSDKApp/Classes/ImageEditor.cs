using LumiaSDKApp.Filters;
using Microsoft.Phone.Tasks;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace LumiaSDKApp.Classes
{
    class ImageEditor
    {
        public static ImageEditor INSTANCE = new ImageEditor();

        private bool rendering;

        public FilterController filterController;
        public FilterList filterList;

        private Image viewImage;

        private EditableImage imageInEdit = null;
  
        public ImageEditor()
        {
            filterController = new FilterController(this);
            filterList = new FilterList(this);
        }
        
        public bool IsRendering()
        {
            return rendering;
        }

        public void SetViewImage(Image image)
        {
            viewImage = image;
        }

        public EditableImage GetImageInEdit()
        {
            return imageInEdit;
        }

        public void SetImageInEdit(EditableImage editableImage)
        {
            imageInEdit = editableImage;
        }

        public void PickImage(EventHandler<PhotoResult> onPictureChosen)
        {
            PhotoChooserTask chooser = new PhotoChooserTask();           
            chooser.Completed += onPictureChosen;
            chooser.Show();
        }

        public async Task RenderImage()
        {
            rendering = true;
            await imageInEdit.Render(filterController.GetCurrentFilter());
            rendering = false;
        }

        public void UpdateImage()
        {
            viewImage.Source = imageInEdit.GetBitmap();
        }

        public async void SaveImage()
        {
            await imageInEdit.Save(filterController.GetCurrentFilter());
        }
    }
}
