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
    public class IndexBuildFormDriver {
        public WindowControl Window { get; private set; }
        public FormsTextBox TargetDirText { get; private set; }
        public FormsButton ReferenceButton { get; private set; }

        public FormsButton UpdateIndexButton { get; private set; }
        public FormsButton StopButton { get; private set; }

        public FwFlexGridEx ActiveIndexGrid { get; private set; }
        public FwFlexGridEx IndexHistoryGrid { get; private set; }

        public IndexBuildFormDriver(WindowControl window) {
            Window = window;
            TargetDirText = new FormsTextBox(Window.Dynamic().TargetDirText);
            ReferenceButton = new FormsButton(Window.Dynamic().ReferenceButton);
            UpdateIndexButton = new FormsButton(Window.Dynamic().UpdateIndexButton);
            StopButton = new FormsButton(Window.Dynamic().StopButton);
            ActiveIndexGrid = new FwFlexGridEx(Window.Dynamic().ActiveIndexGrid);
            IndexHistoryGrid = new FwFlexGridEx(Window.Dynamic().IndexHistoryGrid);
        }

    }
}
