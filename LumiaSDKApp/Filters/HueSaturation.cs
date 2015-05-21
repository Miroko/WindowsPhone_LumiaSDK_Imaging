using Lumia.Imaging.Adjustments;
using LumiaSDKApp.Classes;
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

        private async void saturationSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (!ImageEditor.INSTANCE.IsRendering())
            {
                filter.Saturation = e.NewValue;

                await ImageEditor.INSTANCE.RenderImage();
                ImageEditor.INSTANCE.UpdateImage();
            }
        }

        private async void hueSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (!ImageEditor.INSTANCE.IsRendering())
            {
                filter.Hue = e.NewValue;

                await ImageEditor.INSTANCE.RenderImage();
                ImageEditor.INSTANCE.UpdateImage();
            }
        }

        public override void RandomValues()
        {
            Random r = new Random();
            double value = (r.NextDouble() * (double)r.Next(-1, 1));
            filter.Hue = value;
            filter.Saturation = value;
        }

        public override void PopulateControls(ListBox listToPopulate)
        {
            TextBlock hue = new TextBlock();
            hue.Text = "Hue";
            hue.Margin = new Thickness(12, 0, 0, 0);
            listToPopulate.Items.Add(hue);
            Slider hueSlider = new Slider();
            hueSlider.Width = 456;
            hueSlider.Minimum = -1;
            hueSlider.Maximum = 1;
            hueSlider.ValueChanged += hueSlider_ValueChanged;
            listToPopulate.Items.Add(hueSlider);

            TextBlock saturation = new TextBlock();
            saturation.Text = "Saturation";
            saturation.Margin = new Thickness(12, 0, 0, 0);
            listToPopulate.Items.Add(saturation);
            Slider saturationSlider = new Slider();
            saturationSlider.Width = 456;
            saturationSlider.Minimum = -1;
            saturationSlider.Maximum = 1;
            saturationSlider.ValueChanged += saturationSlider_ValueChanged;
            listToPopulate.Items.Add(saturationSlider);
        }
    }
}
