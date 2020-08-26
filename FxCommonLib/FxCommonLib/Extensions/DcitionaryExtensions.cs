using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FxCommonLib.Extensions {
    public static class DictionaryExtensions {

        /// <summary>データ比較オプション(全角/半角、大文字/小文字無視</summary>
        private static readonly CompareOptions _compOptIgnoreWC = CompareOptions.IgnoreWidth |    // 全角・半角無視
                                                            CompareOptions.IgnoreCase;      // 大文字・小文字無視

        /// <summary>CultureInfo</summary>
        private static readonly CultureInfo _cultureInfo = CultureInfo.CurrentCulture;

        /// <summary>
        /// 引数にnullを渡してもerrorとならないContainsKey拡張メソッド
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="self"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static bool ContainsKeyNullable<TKey, TValue>(
            this Dictionary<TKey, TValue> self, TKey key) {
            if (key == null) {
                return false;
            }
            return self.ContainsKey(key);
        }

        /// <summary>
        /// 全角/半角、大文字/小文字を同一の文字列とみなしキーに含まれているか判定する拡張メソッド
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="self"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static bool ContainsKeyIgnoreCaseWC<TKey, TValue>(
            this Dictionary<TKey, TValue> self, string key) {

            if (key == null) {
                return false;
            }

            //foreach (KeyValuePair<TKey,TValue> chk in self) {
            //    if (chk.Key.GetType() != typeof(string)) { return false; }
            //}

            foreach (KeyValuePair<TKey, TValue> kvp in self) {
                string s = (string)(object)kvp.Key;
                if (String.Compare(s, key, _cultureInfo,_compOptIgnoreWC) == 0) {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// 全角/半角、大文字/小文字を同一の文字列とみなし、Dictionaryの最初のキーを返す拡張メソッド
        /// (keyはstringの前提)
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="self"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string GetFirstKeyIgnoreCaseWC<TKey, TValue>(
            this Dictionary<TKey, TValue> self, string key) {

            if (key == null) {
                return null;
            }

            //foreach (KeyValuePair<TKey,TValue> chk in self) {
            //    if (chk.Key.GetType() != typeof(string)) { return false; }
            //}

            foreach (KeyValuePair<TKey, TValue> kvp in self) {
                string s = (string)(object)kvp.Key;
                if (String.Compare(s, key, _cultureInfo, _compOptIgnoreWC) == 0) {
                    return s;
                }
            }
            return null;
        }

    }
}
