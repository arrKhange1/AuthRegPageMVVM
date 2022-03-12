using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace DockPanel.Models
{
    class MainWindowModel
    {
        private Page _firstPage;
        private Page _secondPage;

        public Page FirstPage { get { return _firstPage;  } }
        public Page SecondPage { get { return _secondPage; } }
        public MainWindowModel()
        {
            _firstPage = new Views.Pages.RegPageView();
            _secondPage = new Views.Pages.AuthPageView();
           
        }

        


    }
}
