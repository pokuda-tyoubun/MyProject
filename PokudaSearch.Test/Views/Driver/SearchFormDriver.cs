using Codeer.Friendly.CCC.Util;
using Codeer.Friendly.Dynamic;
using Codeer.Friendly.Windows;
using Codeer.Friendly.Windows.Grasp;
using Ong.Friendly.FormsStandardControls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokudaSearch.Test.Views.Driver {
    public class SearchFormDriver {
        public WindowControl Window { get; private set; }
        public FormsButton SearchButton { get; private set; }
        public FormsButton ClearButton { get; private set; }
        public FormsTextBox KeywordText { get; private set; }
        public FormsTextBox ExtensionText { get; private set; }
        public FwC1DateEdit UpdateDate1 { get; private set; }
        public FwC1DateEdit UpdateDate2 { get; private set; }
        public FormsButton ShowPreviewButton { get; private set; }
        public FwFlexGridEx TargetIndexGrid { get; private set; }
        public FwFlexGridEx ReslutGrid { get; private set; }

        public FormsToolStripItem ItemColConfMenu { get; private set; }


        public SearchFormDriver(WindowControl window) {
            Window = window;
            SearchButton = new FormsButton(Window.Dynamic().SearchButton);
            ClearButton = new FormsButton(Window.Dynamic().ClearButton);
            KeywordText = new FormsTextBox(Window.Dynamic().KeywordText);
            ExtensionText = new FormsTextBox(Window.Dynamic().ExtensionText);
            UpdateDate1 = new FwC1DateEdit(Window.Dynamic().UpdateDate1);
            UpdateDate2 = new FwC1DateEdit(Window.Dynamic().UpdateDate2);
            ShowPreviewButton = new FormsButton(Window.Dynamic().ShowPreviewButton);
            TargetIndexGrid = new FwFlexGridEx(Window.Dynamic().TargetIndexGrid);
            ReslutGrid = new FwFlexGridEx(Window.Dynamic().ReslutGrid);
        }
    }
}
