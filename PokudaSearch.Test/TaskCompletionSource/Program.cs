using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PokudaSearch.Test.TaskCompletionSource {
    public class Program {
        public enum Status {
            None,
            Successful,
            Canceled,
            Failed,
        }

        private TaskCompletionSource<int> _completionSource = new TaskCompletionSource<int>();
        public TaskCompletionSource<int> CompletionSource {
            get { 
                return _completionSource;  
            }
        }

        /// <summary>volatileによりシングルスレッドでのアクセスを前提</summary>
        private volatile Status _status = Status.None;
        public Status CurrentStatus {
            get { 
                return _status;  
            }
            set { 
                _status = value;  
            }
        }

        public void RunAsync() {
            ThreadPool.QueueUserWorkItem(_ => {
                while (true) {
                    //ステータスが以下の３つになるまで待機する。
                    switch (_status) {
                        case Status.Successful:
                            _completionSource.SetResult(10);
                            goto End;

                        case Status.Canceled:
                            _completionSource.SetCanceled();
                            goto End;

                        case Status.Failed:
                            _completionSource.SetException(new Exception());
                            goto End;
                    }
                    Thread.Sleep(100);
                }
            End:;
            });
        }
    }
}
