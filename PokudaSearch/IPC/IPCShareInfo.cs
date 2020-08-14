using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokudaSearch.IPC {
    public class IPCShareInfo : MarshalByRefObject {

        private int _processId = 0;
        public int ProcessId {
            get { return _processId; }
            set { _processId = value; }
        }

        public delegate void CallEventHandler(IPCShareInfoEventArg e);
        public event CallEventHandler OnSend;
        public void SendInfo(string path) {
            if (OnSend != null) {
                OnSend(new IPCShareInfoEventArg(path));
            }
        }

        public class IPCShareInfoEventArg : EventArgs {
            private string _path = "";
            public string Path {
                get { return _path; }
                set { _path = value; }
            }

            public IPCShareInfoEventArg(string path) {
                _path = path;
            }
        }
    }
}
