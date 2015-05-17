using Lumia.Imaging;
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

        public override void SetControls(ListBox filterControls)
        {
            TextBlock levelLabel = new TextBlock();
            levelLabel.Text = "Contrast";
            levelLabel.Margin = new Thickness(12, 0, 0, 0);
            filterControls.Items.Add(levelLabel);
            Slider levelSlider = new Slider();
            levelSlider.Width = 456;
            levelSlider.Minimum = -1;
            levelSlider.Maximum = 1;
            levelSlider.ValueChanged += levelSlider_ValueChanged;
            filterControls.Items.Add(levelSlider);
        }

        void levelSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (ImageController.INSTANCE.rendering) return;

            filter.Level = e.NewValue;

            ImageController.INSTANCE.UpdateImage();   
        }


        public override void RandomValues()
        {
            Random r = new Random();
            double value = (r.NextDouble() * (double)r.Next(-1, 1));
            filter.Level = value;
        }
    }
}
