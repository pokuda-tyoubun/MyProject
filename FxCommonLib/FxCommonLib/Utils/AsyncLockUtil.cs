using System;
using System.Threading.Tasks;

namespace FxCommonLib.Utils {
    /// <summary>
    /// 非同期処理時のロックユーティリティ
    /// </summary>
    public sealed class AsyncLockUtil : IDisposable {

        #region MemberVariables
        private readonly System.Threading.SemaphoreSlim _semaphore = new System.Threading.SemaphoreSlim(1, 1);
        private readonly Task<IDisposable> _releaser;
        #endregion MemberVariables

        #region Constractors
        public AsyncLockUtil() {
            _releaser = Task.FromResult((IDisposable)new Releaser(this));
        }
        #endregion Constractors

        #region PublicMethods
        /// <summary>
        /// Dispose処理
        /// </summary>
        public void Dispose() {
            _semaphore.Release();
            _semaphore.Dispose();
            GC.SuppressFinalize(this);
        }
        /// <summary>
        /// 非同期処理時のロック
        /// </summary>
        /// <returns></returns>
        public Task<IDisposable> LockAsync() {
            var wait = _semaphore.WaitAsync();
            return wait.IsCompleted ?
                _releaser :
                wait.ContinueWith(
                        (_, state) => (IDisposable)state,
                        _releaser.Result, 
                        System.Threading.CancellationToken.None,
                        TaskContinuationOptions.ExecuteSynchronously, 
                        System.Threading.Tasks.TaskScheduler.Default
                        );
        }
        #endregion PublicMethods

        #region PrivateMethods
        /// <summary>
        /// ロック解放
        /// </summary>
        private sealed class Releaser : IDisposable {
            private readonly AsyncLockUtil m_toRelease;
            internal Releaser(AsyncLockUtil toRelease) { m_toRelease = toRelease; }
            public void Dispose() { 
                m_toRelease._semaphore.Release(); 
                m_toRelease._semaphore.Dispose(); 
            }
        }
        #endregion PrivateMethods
    }
}