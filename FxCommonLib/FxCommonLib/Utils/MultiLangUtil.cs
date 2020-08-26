using System.Collections.Generic;

namespace FxCommonLib.Utils {

    /// <summary>
    /// 多言語対応ユーティリティ
    /// </summary>
    public class MultiLangUtil {

        private Dictionary<string, string> _msgDic = new Dictionary<string, string>();
        public Dictionary<string, string> MessageDictionary {
            set {_msgDic = value; }
            get { return _msgDic; }
        }

        //public MultiLangUtil(string cultureName, string path) {
        //    string resourceFilePath = "";

        //    if (cultureName == "en") {
        //        //英語
        //        resourceFilePath = "en.resx";
        //    } else {
        //        //日本語
        //        resourceFilePath = "ja.resx";
        //    }
        //    using (ResXResourceReader reader = new ResXResourceReader(path + resourceFilePath)) {
        //        foreach (DictionaryEntry entry in reader) {
        //            _msgDic.Add(entry.Key.ToString(), entry.Value.ToString());
        //        }
        //    }
        //}

        public MultiLangUtil() {
        }

        /// <summary>
        /// メッセージ取得
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public string GetMsg(string key) {
            return _msgDic[key];
        }
        /// <summary>
        /// メッセージ取得
        /// </summary>
        /// <param name="key"></param>
        /// <param name="val1"></param>
        /// <param name="val2"></param>
        /// <returns></returns>
        public string GetMsg(string key, string val1, string val2) {
            return string.Format(_msgDic[key], _msgDic[val1], _msgDic[val2]);
        }
        /// <summary>
        /// メッセージ取得
        /// </summary>
        /// <param name="key"></param>
        /// <param name="val1"></param>
        /// <returns></returns>
        public string GetMsg(string key, string val1) {
            return string.Format(_msgDic[key], _msgDic[val1]);
        }
    } 
}
