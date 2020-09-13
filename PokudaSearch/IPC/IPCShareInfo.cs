using org.omg.CORBA;
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
        public void SendInfo(string option, string path) {
            if (OnSend != null) {
                OnSend(new IPCShareInfoEventArg(option, path));
            }
        }

        public override object InitializeLifetimeService() {
            //IPC通信のタイムアウトを無制限にする
            return null;
        }

        public class IPCShareInfoEventArg : EventArgs {
            private string _path = "";
            public string Path {
                get { return _path; }
                set { _path = value; }
            }
            private string _option = "";
            public string Option {
                get { return _option; }
                set { _option = value; }
            }

            public IPCShareInfoEventArg(string option, string path) {
                _option = option;
                _path = path;
            }
        }
    }
}
