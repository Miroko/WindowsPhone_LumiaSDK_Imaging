using Lumia.Imaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace LumiaSDKApp
{
    abstract class Filter
    {
        public abstract String Name { get; }

        public abstract void SetControls(ListBox filterControls);
        public abstract IFilter[] GetFilters();
        public abstract void RandomValues();
    }
}
