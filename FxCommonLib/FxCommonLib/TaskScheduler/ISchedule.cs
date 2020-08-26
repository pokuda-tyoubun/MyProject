using System;

namespace FxCommonLib.TaskScheduler {
    public interface ISchedule {
        /// <summary>
        /// タスク開始判定
        /// </summary>
        /// <returns></returns>
        Boolean IsTrigger();
        /// <summary>
        /// 実行する処理
        /// </summary>
        void Execute();

        /// <summary>
        /// 終了判定
        /// </summary>
        /// <returns></returns>
        Boolean IsFinish();
    }
}