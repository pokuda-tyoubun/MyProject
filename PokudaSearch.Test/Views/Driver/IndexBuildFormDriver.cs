using Codeer.Friendly;
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

        public Async Async { get; private set; }

        public FormsTextBox TargetDirText { get; private set; }
        public FormsButton ReferenceButton { get; private set; }

        public FormsButton UpdateIndexButton { get; private set; }
        public FormsButton AddOuterIndexButton { get; private set; }

        public FormsButton StopButton { get; private set; }
        public FormsProgressBar ProgressBar { get; private set; }

        public FwFlexGridEx ActiveIndexGrid { get; private set; }
        public FwFlexGridEx IndexHistoryGrid { get; private set; }

        public IndexBuildFormDriver(WindowControl window, Async async) {
            Window = window;
            Async = async;

            TargetDirText = new FormsTextBox(Window.Dynamic().TargetDirText);
            ReferenceButton = new FormsButton(Window.Dynamic().ReferenceButton);
            UpdateIndexButton = new FormsButton(Window.Dynamic().UpdateIndexButton);
            AddOuterIndexButton = new FormsButton(Window.Dynamic().AddOuterIndexButton);
            StopButton = new FormsButton(Window.Dynamic().StopButton);
            ProgressBar = new FormsProgressBar(Window.Dynamic().ProgressBar);
            ActiveIndexGrid = new FwFlexGridEx(Window.Dynamic().ActiveIndexGrid);
            IndexHistoryGrid = new FwFlexGridEx(Window.Dynamic().IndexHistoryGrid);
        }


        public void Close() {
            Window.Close();
            if(Async == null) {
                Window.WaitForDestroy();
                return;
            }
            Async.WaitForCompletion();
        }
    }
}
