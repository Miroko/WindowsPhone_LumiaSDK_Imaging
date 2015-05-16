using Lumia.Imaging;
using LumiaSDKApp.Filters;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace LumiaSDKApp
{
    class ImageController
    {
        public static ImageController INSTANCE = new ImageController();

        private ObservableCollection<Filter> allFilters = new ObservableCollection<Filter>() {
            new Lomo()
        };

        public bool rendering = false;

        private Image currentImage = null;
        private Filter currentFilter = null;

        public void SetCurrentImage(Image image)
        {
            currentImage = image;
        }

        public void SetCurrentFilter(Filter filter)
        {            
            currentFilter = filter;
        }

        public ObservableCollection<Filter> GetAllFilters()
        {
            return allFilters;
        }

        public void SetControls(ListBox filterControls)
        {
            currentFilter.SetControls(filterControls);
        }

        public async void SaveImage()
        {
            if (!rendering)
            {
                rendering = true;
                await ImageManipulator.INSTANCE.SaveStreamToImage(currentFilter);
                rendering = false;
            }            
        }

        public async void UpdateImage()
        {
            if (!rendering)
            {
                rendering = true;
                await ImageManipulator.INSTANCE.SetStreamToImage(currentImage, currentFilter);
                rendering = false;                
            }
        }
    }
}
