using Codeer.Friendly;
using Codeer.Friendly.Windows.Grasp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokudaSearch.Test.Interfaces {
    public interface IFormDriver {
        /// <summary>WindowControlオブジェクト</summary>
        WindowControl Window { get; }
        /// <summary>Asyncオブジェクト</summary>
        Async Async { get; }
        /// <summary>
        /// 画面を閉じる
        /// </summary>
        void Close();
    }
}
