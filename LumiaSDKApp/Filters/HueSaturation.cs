using Lumia.Imaging.Adjustments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace LumiaSDKApp.Filters
{
    class HueSaturation : Filter
    {
        private HueSaturationFilter filter = new HueSaturationFilter();

        public override string Name
        {
            get { return "HueSaturation"; }
        }

        public override Lumia.Imaging.IFilter[] GetFilters()
        {
            return new[] { filter };
        }

        public override void SetControls(ListBox filterControls)
        {
            TextBlock hue = new TextBlock();
            hue.Text = "Hue";
            hue.Margin = new Thickness(12, 0, 0, 0);
            filterControls.Items.Add(hue);
            Slider hueSlider = new Slider();
            hueSlider.Width = 456;
            hueSlider.Minimum = -1;
            hueSlider.Maximum = 1;
            hueSlider.ValueChanged += hueSlider_ValueChanged;
            filterControls.Items.Add(hueSlider);

            TextBlock saturation = new TextBlock();
            saturation.Text = "Saturation";
            saturation.Margin = new Thickness(12, 0, 0, 0);
            filterControls.Items.Add(saturation);
            Slider saturationSlider = new Slider();
            saturationSlider.Width = 456;
            saturationSlider.Minimum = -1;
            saturationSlider.Maximum = 1;
            saturationSlider.ValueChanged += saturationSlider_ValueChanged;
            filterControls.Items.Add(saturationSlider);
        }

        void saturationSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (ImageController.INSTANCE.rendering) return;

            filter.Saturation = e.NewValue;

            ImageController.INSTANCE.UpdateImage();
        }

        void hueSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (ImageController.INSTANCE.rendering) return;

            filter.Hue = e.NewValue;

            ImageController.INSTANCE.UpdateImage();
        }
    }
}
