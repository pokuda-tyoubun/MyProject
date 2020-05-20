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
    public class OuterIndexFormDriver {
        public WindowControl Window { get; private set; }

        public Async Async { get; private set; }

        public FormsTextBox OuterStorePathText { get; private set; }
        public FormsTextBox OuterTargetPathText { get; private set; }
        public FormsTextBox LocalPathText { get; private set; }

        public FormsButton ReferenceButton { get; private set; }
        public FormsButton OKButton { get; private set; }
        public FormsButton CancelButton { get; private set; }

        public FwFlexGridEx ActiveIndexGrid { get; private set; }

        public OuterIndexFormDriver(WindowControl window, Async async) {
            Window = window;
            Async = async;

            OuterStorePathText = new FormsTextBox(Window.Dynamic().OuterStorePathText);
            OuterTargetPathText = new FormsTextBox(Window.Dynamic().OuterTargetPathText);
            LocalPathText = new FormsTextBox(Window.Dynamic().LocalPathText);

            ReferenceButton = new FormsButton(Window.Dynamic().ReferenceButton);
            OKButton = new FormsButton(Window.Dynamic().OKButton);
            CancelButton = new FormsButton(Window.Dynamic().CancelButton1);

            ActiveIndexGrid = new FwFlexGridEx(Window.Dynamic().ActiveIndexGrid);
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
