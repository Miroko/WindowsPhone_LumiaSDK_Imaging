using Lumia.Imaging;
using Lumia.Imaging.Artistic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace LumiaSDKApp.Filters
{
    class Lomo : Filter
    {
        private LomoFilter filter = new LomoFilter();

        public override string Name
        {
            get { return "Lomo"; }
        }       

        public override IFilter[] GetFilters()
        {
            return new[] { filter };
        }

        public override void SetControls(ListBox filterControls)
        {
            TextBlock brightnessLabel = new TextBlock();
            brightnessLabel.Text = "Brightness";
            brightnessLabel.Margin = new Thickness(12, 0, 0, 0);
            filterControls.Items.Add(brightnessLabel);
            Slider brightnessSlider = new Slider();
            brightnessSlider.Width = 456;
            brightnessSlider.Minimum = 0;
            brightnessSlider.Maximum = 1;
            brightnessSlider.ValueChanged += brightnessSlider_ValueChanged;
            filterControls.Items.Add(brightnessSlider);

            TextBlock styleLabel = new TextBlock();
            styleLabel.Text = "Style";
            styleLabel.Margin = new Thickness(12, 0, 0, 0);
            filterControls.Items.Add(styleLabel);
            Slider styleSlider = new Slider();
            styleSlider.Width = 456;
            styleSlider.Minimum = 0;
            styleSlider.Maximum = 4;
            styleSlider.ValueChanged += styleSlider_ValueChanged;
            filterControls.Items.Add(styleSlider);

            TextBlock vignettiLabel = new TextBlock();
            vignettiLabel.Text = "Vignetting";
            vignettiLabel.Margin = new Thickness(12, 0, 0, 0);
            filterControls.Items.Add(vignettiLabel);
            Slider vignettiSlider = new Slider();
            vignettiSlider.Width = 456;
            vignettiSlider.Minimum = 0;
            vignettiSlider.Maximum = 2;
            vignettiSlider.ValueChanged += vignettiSlider_ValueChanged;
            filterControls.Items.Add(vignettiSlider); 
            
        }

        void vignettiSlider_ValueChanged(object sender, System.Windows.RoutedPropertyChangedEventArgs<double> e)
        {
            if (ImageController.INSTANCE.rendering) return;

            int newValue = (int)e.NewValue;
            switch (newValue)
            {
                case 0: filter.LomoVignetting = LomoVignetting.Low;
                    break;
                case 1: filter.LomoVignetting = LomoVignetting.Medium;
                    break;
                case 2: filter.LomoVignetting = LomoVignetting.High;
                    break;
            }

            ImageController.INSTANCE.UpdateImage();
        }

        private void brightnessSlider_ValueChanged(object sender, System.Windows.RoutedPropertyChangedEventArgs<double> e)
        {
            if (ImageController.INSTANCE.rendering) return;

            filter.Brightness = e.NewValue;

            ImageController.INSTANCE.UpdateImage();
        }

        private void styleSlider_ValueChanged(object sender, System.Windows.RoutedPropertyChangedEventArgs<double> e)
        {
            if (ImageController.INSTANCE.rendering) return;

            int newValue = (int)e.NewValue;
            switch (newValue)
            {
                case 0: filter.LomoStyle = LomoStyle.Blue;
                    break;
                case 1: filter.LomoStyle = LomoStyle.Green;
                    break;
                case 2: filter.LomoStyle = LomoStyle.Neutral;
                    break;
                case 3: filter.LomoStyle = LomoStyle.Red;
                    break;
                case 4: filter.LomoStyle = LomoStyle.Yellow;
                    break;
            }

            ImageController.INSTANCE.UpdateImage();
        }
    }
}
