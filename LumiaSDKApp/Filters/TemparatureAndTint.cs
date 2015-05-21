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
    class TemparatureAndTint : Filter
    {
        private TemperatureAndTintFilter filter = new TemperatureAndTintFilter();

        public override string Name
        {
            get { return "TemperatureAndTint"; }
        }

        public override Lumia.Imaging.IFilter[] GetFilters()
        {
            return new[] { filter };
        }

        private async void tintSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (!ImageEditor.INSTANCE.IsRendering())
            {                
                filter.Tint = e.NewValue;

                await ImageEditor.INSTANCE.RenderImage();
                ImageEditor.INSTANCE.UpdateImage();
            }
        }

        private async void temparatureSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (!ImageEditor.INSTANCE.IsRendering())
            {
                filter.Temperature = e.NewValue;

                await ImageEditor.INSTANCE.RenderImage();
                ImageEditor.INSTANCE.UpdateImage();
            }
        }

        public override void RandomValues()
        {
            Random r = new Random();
            double value = (r.NextDouble() * (double)r.Next(-1, 1));
            filter.Temperature = value;
            filter.Tint = value;
        }

        public override void PopulateControls(ListBox listToPopulate)
        {
            TextBlock temperatureLabel = new TextBlock();
            temperatureLabel.Text = "Temperature";
            temperatureLabel.Margin = new Thickness(12, 0, 0, 0);
            listToPopulate.Items.Add(temperatureLabel);
            Slider temparatureSlider = new Slider();
            temparatureSlider.Width = 456;
            temparatureSlider.Minimum = -1;
            temparatureSlider.Maximum = 1;
            temparatureSlider.ValueChanged += temparatureSlider_ValueChanged;
            listToPopulate.Items.Add(temparatureSlider);

            TextBlock tintLabel = new TextBlock();
            tintLabel.Text = "Tint";
            tintLabel.Margin = new Thickness(12, 0, 0, 0);
            listToPopulate.Items.Add(tintLabel);
            Slider tintSlider = new Slider();
            tintSlider.Width = 456;
            tintSlider.Minimum = -1;
            tintSlider.Maximum = 1;
            tintSlider.ValueChanged += tintSlider_ValueChanged;
            listToPopulate.Items.Add(tintSlider);
        }
    }
}
