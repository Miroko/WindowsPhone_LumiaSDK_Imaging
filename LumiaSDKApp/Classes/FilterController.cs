using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace LumiaSDKApp.Classes
{
    class FilterController
    {       
        private ImageEditor editor;

        private Filter currentFilter;

        public FilterController(ImageEditor editor)
        {
            this.editor = editor;
        }

        public void SetControls(ListBox listToPopulate){
            currentFilter.PopulateControls(listToPopulate);
        }

        public Filter GetCurrentFilter()
        {
            return currentFilter;
        }

        public void SetCurrentFilter(Filter filter)
        {
            currentFilter = filter;
        }
    }
}
