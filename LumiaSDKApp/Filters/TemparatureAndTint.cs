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

        public override void SetControls(ListBox filterControls)
        {
            TextBlock temperatureLabel = new TextBlock();
            temperatureLabel.Text = "Temperature";
            temperatureLabel.Margin = new Thickness(12, 0, 0, 0);
            filterControls.Items.Add(temperatureLabel);
            Slider temparatureSlider = new Slider();
            temparatureSlider.Width = 456;
            temparatureSlider.Minimum = -1;
            temparatureSlider.Maximum = 1;
            temparatureSlider.ValueChanged += temparatureSlider_ValueChanged;
            filterControls.Items.Add(temparatureSlider);

            TextBlock tintLabel = new TextBlock();
            tintLabel.Text = "Tint";
            tintLabel.Margin = new Thickness(12, 0, 0, 0);
            filterControls.Items.Add(tintLabel);
            Slider tintSlider = new Slider();
            tintSlider.Width = 456;
            tintSlider.Minimum = -1;
            tintSlider.Maximum = 1;
            tintSlider.ValueChanged += tintSlider_ValueChanged;
            filterControls.Items.Add(tintSlider);
        }

        void tintSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (ImageController.INSTANCE.rendering) return;

            filter.Tint = e.NewValue;

            ImageController.INSTANCE.UpdateImage();
        }

        void temparatureSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (ImageController.INSTANCE.rendering) return;

            filter.Temperature = e.NewValue;

            ImageController.INSTANCE.UpdateImage();
        }
    }
}
