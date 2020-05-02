using Codeer.Friendly.CCC.Util;
using Codeer.Friendly.Dynamic;
using Codeer.Friendly.Windows.Grasp;
using PokudaSearch.Test.Views.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokudaSearch.Test.Driver {
    public class MainFrameFormDriver {
        public WindowControl Window { get; private set; }
        public FwC1Ribbon Ribbon { get; private set; }
        private FwC1RibbonButton FileExplorerFormButton { get; set; }
        private FwC1RibbonButton SearchFormButton { get; set; }
        private FwC1RibbonButton IndexBuildFormButton { get; set; }
        private FwC1RibbonButton TagEditFormButton { get; set; }


        public MainFrameFormDriver(WindowControl window) {
            Window = window;
            Ribbon = new FwC1Ribbon(Window.Dynamic().Ribbon);
            FileExplorerFormButton = new FwC1RibbonButton(Window.Dynamic().FileExplorerFormButton);
            SearchFormButton = new FwC1RibbonButton(Window.Dynamic().SearchFormButton);
            IndexBuildFormButton = new FwC1RibbonButton(Window.Dynamic().IndexBuildFormButton);
            TagEditFormButton = new FwC1RibbonButton(Window.Dynamic().TagEditFormButton);
        }


        public IndexBuildFormDriver IndexBuildFormButton_EmulateClick() {
            IndexBuildFormButton.EmulateClick();
            var indexBuildForm = new WindowControl(AppDriver.App.Type<MainFrameForm>().IndexBuildForm);
            return new IndexBuildFormDriver(indexBuildForm);
        }
        public SearchFormDriver SearchFormButton_EmulateClick() {
            SearchFormButton.EmulateClick();
            var searchForm = new WindowControl(AppDriver.App.Type<MainFrameForm>().SearchForm);
            return new SearchFormDriver(searchForm);
        }
    }
}
