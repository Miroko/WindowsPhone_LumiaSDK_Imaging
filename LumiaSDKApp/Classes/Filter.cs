using Lumia.Imaging;
using LumiaSDKApp.Classes;
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
        public abstract IFilter[] GetFilters();
        public abstract void RandomValues();

        public abstract void PopulateControls(ListBox listToPopulate);
    }
}
