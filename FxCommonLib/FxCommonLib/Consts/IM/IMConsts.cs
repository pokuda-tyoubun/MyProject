using System.Text.RegularExpressions;

namespace FxCommonLib.Consts.IM {
    /// <summary>
    /// 共通定数クラス
    /// </summary>
    public static class IMConsts {

        /// <summary>データディクショナリ</summary>
        #region DataDictionary
        public const string user_id = "user_id"; //ユーザID
        public const string user_name = "user_name"; //ユーザ名
        public const string user_kana = "user_kana"; //カナ
        public const string authority_code = "authority_code"; //権限コード
        public const string password = "password"; //パスワード
        public const string pwd_up_id = "pwd_up_id"; //パスワード変更ユーザID
        public const string pwd_up_date = "pwd_up_date"; //パスワード変更日時
        public const string last_login_date = "last_login_date"; //最終ログイン日時
        public const string admin_menu_lock = "admin_menu_lock"; //管理者メニューロック
        public const string color = "color"; //色
        #endregion

        /// <summary>メッセージID</summary>
        #region MessageId
        public const string AUTH_TYPE_ADMIN = "AUTH_TYPE_ADMIN"; //権限：システム管理者
        public const string AUTH_TYPE_PLANNER = "AUTH_TYPE_PLANNER"; //権限：計画者
        public const string AUTH_TYPE_WORKER = "AUTH_TYPE_WORKER"; //権限：作業者
        public const string MSG_LOGIN_FAILED = "MSG_LOGIN_FAILED"; //ログインに失敗しました。ログインID、またはパスワードが異なります
        public const string MSG_DUPLICATE = "MSG_DUPLICATE"; //{0}が重複しています。
        public const string MSG_NOT_SELECTED = "MSG_NOT_SELECTED"; //未選択
        public const string MSG_RESERVED = "MSG_RESERVED"; //「{0}」は、システムで利用するため設定できません。
        public const string MSG_NOT_ENTRY = "MSG_NOT_ENTRY"; //未入力
        #endregion

        /// <summary>共通設定</summary>
        #region CommonConf
        public const string SessionTimeOutMinutes = "SessionTimeOutMinutes"; //セッションタイムアウト時間（分）
        public const string MaxAppendRowCount = "MaxAppendRowCount"; //新規追加行ダイアログ
        #endregion

        /// <summary>システム権限</summary>
        #region SystemAuthority
        public const string SystemAuthorityAdmin   = "1";
        public const string SystemAuthorityPlanner  = "2";
        public const string SystemAuthorityWorker  = "3";
        #endregion
    }
}
