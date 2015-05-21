using Lumia.Imaging;
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
    class Contrast : Filter
    {
        private ContrastFilter filter = new ContrastFilter();

        public override string Name
        {
            get { return "Contrast"; }
        }

        public override IFilter[] GetFilters()
        {
            return new[] { filter };
        }

        public override void RandomValues()
        {
            Random r = new Random();
            double value = (r.NextDouble() * (double)r.Next(-1, 1));
            filter.Level = value;
        }

        public override void PopulateControls(ListBox listToPopulate)
        {
            TextBlock level = new TextBlock();
            level.Text = "Level";
            level.Margin = new Thickness(12, 0, 0, 0);
            listToPopulate.Items.Add(level);
            Slider levelSlider = new Slider();
            levelSlider.Width = 456;
            levelSlider.Minimum = -1;
            levelSlider.Maximum = 1;
            levelSlider.ValueChanged += levelSlider_ValueChanged;
            listToPopulate.Items.Add(levelSlider);
        }

        private async void levelSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (!ImageEditor.INSTANCE.IsRendering())
            {
                filter.Level = e.NewValue;

                await ImageEditor.INSTANCE.RenderImage();
                ImageEditor.INSTANCE.UpdateImage();
            }
        }

    }
}
