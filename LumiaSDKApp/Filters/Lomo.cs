﻿using Lumia.Imaging;
using Lumia.Imaging.Artistic;
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

        private async void vignettiSlider_ValueChanged(object sender, System.Windows.RoutedPropertyChangedEventArgs<double> e)
        {
            if (!ImageEditor.INSTANCE.IsRendering())
            {
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

                await ImageEditor.INSTANCE.RenderImage();
                ImageEditor.INSTANCE.UpdateImage();
            }
        }

        private async void brightnessSlider_ValueChanged(object sender, System.Windows.RoutedPropertyChangedEventArgs<double> e)
        {
            if (!ImageEditor.INSTANCE.IsRendering())
            {
                filter.Brightness = e.NewValue;

                await ImageEditor.INSTANCE.RenderImage();
                ImageEditor.INSTANCE.UpdateImage();
            }
        }

        private async void styleSlider_ValueChanged(object sender, System.Windows.RoutedPropertyChangedEventArgs<double> e)
        {
            if (!ImageEditor.INSTANCE.IsRendering())
            {
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

                await ImageEditor.INSTANCE.RenderImage();
                ImageEditor.INSTANCE.UpdateImage();
            }
        }
        public override void RandomValues()
        {
            Random r = new Random();
         
            filter.LomoVignetting = LomoVignetting.High;
            filter.LomoStyle = LomoStyle.Blue;

            filter.Saturation = r.NextDouble();
            filter.Brightness = r.NextDouble();
        }

        public override void PopulateControls(ListBox listToPopulate)
        {
            TextBlock brightnessLabel = new TextBlock();
            brightnessLabel.Text = "Brightness";
            brightnessLabel.Margin = new Thickness(12, 0, 0, 0);
            listToPopulate.Items.Add(brightnessLabel);
            Slider brightnessSlider = new Slider();
            brightnessSlider.Width = 456;
            brightnessSlider.Minimum = 0;
            brightnessSlider.Maximum = 1;
            brightnessSlider.ValueChanged += brightnessSlider_ValueChanged;
            listToPopulate.Items.Add(brightnessSlider);

            TextBlock styleLabel = new TextBlock();
            styleLabel.Text = "Style";
            styleLabel.Margin = new Thickness(12, 0, 0, 0);
            listToPopulate.Items.Add(styleLabel);
            Slider styleSlider = new Slider();
            styleSlider.Width = 456;
            styleSlider.Minimum = 0;
            styleSlider.Maximum = 4;
            styleSlider.ValueChanged += styleSlider_ValueChanged;
            listToPopulate.Items.Add(styleSlider);

            TextBlock vignettiLabel = new TextBlock();
            vignettiLabel.Text = "Vignetting";
            vignettiLabel.Margin = new Thickness(12, 0, 0, 0);
            listToPopulate.Items.Add(vignettiLabel);
            Slider vignettiSlider = new Slider();
            vignettiSlider.Width = 456;
            vignettiSlider.Minimum = 0;
            vignettiSlider.Maximum = 2;
            vignettiSlider.ValueChanged += vignettiSlider_ValueChanged;
            listToPopulate.Items.Add(vignettiSlider); 
        }
    }
}
