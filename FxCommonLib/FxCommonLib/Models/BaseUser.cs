using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FxCommonLib.Models {
    public abstract class BaseUser {
        /// <summary>権限コード</summary>
        public string AuthorityCd { get; set; }
        /// <summary>パスワード</summary>
        public string Password { get; set; }
        /// <summary>最終ログイン日時</summary>
        public DateTime? LastLoginDate { get; set; }
        /// <summary>最終パスワード更新者</summary>
        public string PasswordUpdateId { get; set; }
        /// <summary>最終パスワード更新日時</summary>
        public DateTime? PasswordUpdateDate { get; set; }
        /// <summary>最終アクセス日時(httpリクエストがあったか)</summary>
        public DateTime? LastAccessDate { get; set; }
        /// <summary>セッションID</summary>
        public string SessionId { get; set; }
        /// <summary>管理者メニューロック</summary>
        public bool AdminMenuLock { get; set; }

        /// <summary>ログインID</summary>
        public abstract string LoginId {
            get;
        }
        /// <summary>ログイン名</summary>
        public abstract string LoginName {
            get;
        }
    }
}
