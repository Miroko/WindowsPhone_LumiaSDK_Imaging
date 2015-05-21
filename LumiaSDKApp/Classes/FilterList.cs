using LumiaSDKApp.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace LumiaSDKApp.Classes
{
    class FilterList
    {
        private ImageEditor editor;

        public List<Filter> filters = new List<Filter>() {
            new Lomo(),
            new Contrast(),
            new TemparatureAndTint(),
            new HueSaturation()
        };

        public FilterList(ImageEditor editor)
        {
            this.editor = editor;

            foreach (Filter f in filters)
            {
                f.RandomValues();
            }
        }

        public async void PopulateFiltersList(ListBox listToPopulate)
        {
            foreach (Filter filter in filters)
            {
                ListBoxItem item = new ListBoxItem();

                TextBlock label = new TextBlock();
                label.FontSize = 32;
                label.Text = filter.Name;
                label.Margin = new Thickness(12);

                Image img = new Image();
                await RenderPreviewImage(img, filter);

                StackPanel stack = new StackPanel();
                stack.Children.Add(label);
                stack.Children.Add(img);

                item.Content = stack;

                listToPopulate.Items.Add(item);
            }
        }

        public async Task RenderPreviewImage(Image img, Filter filter)
        {
            EditableImage previewImage = new EditableImage(editor.GetImageInEdit().GetSourceStream());
            await previewImage.RenderToImage(img, filter);
        }

    }
}
