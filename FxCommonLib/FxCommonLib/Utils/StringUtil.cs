using log4net;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

namespace FxCommonLib.Utils {
    /// <summary>文字列操作ユーティリティ</summary>
    /// <remarks></remarks>
    public class StringUtil {

        /// <summary>
        /// 0埋めを削除する
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public static string RemoveZeroPadding(string val) {
            double result = 0;
            if (double.TryParse(val, out result)) {
                if (result == 0d) {
                    int pos = val.IndexOf('.');
                    if (pos < 0) {
                        return "0";
                    } else {
                        return "0" + val.Substring(val.IndexOf('.'));
                    }
                }
            }
            return val.TrimStart('0');
        }

        /// <summary>""であればDBNullに変換する。</summary>
        /// <param name="value">変換前オブジェクト</param>
        /// <returns>変換後文字列</returns>
        public static object BlankToDBNull(object value) {
            if (value.ToString() == "") {
                return DBNull.Value;
            }
            return value;
        }
        /// <summary>""であればNullに変換する。</summary>
        /// <param name="value">変換前オブジェクト</param>
        /// <returns>変換後文字列</returns>
        public static object BlankToNull(object value) {
            if (value.ToString() == "") {
                return null;
            }
            return value;
        }

        /// <summary>Nullであれば""に変換する。</summary>
        /// <param name="value">変換前オブジェクト</param>
        /// <returns>変換後文字列</returns>
        /// <remarks><see cref="StringUtil.NullReplace"/></remarks>
        public static string NullToBlank(object value, bool doTrimEnd = true) {
            if (doTrimEnd) {
                return NullReplace(value, "").TrimEnd();
            } else {
                return NullReplace(value, "", doTrimEnd: false);
            }
        }

        /// <summary>NullまたはDBNullであれば""に変換する。</summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string NullDBNullToBlank(object value) {
            if (value == DBNull.Value) {
                return "";
            } 
            return NullReplace(value, "").TrimEnd();
        }
        /// <summary>NullまたはDBNullであれば"0"に変換する。</summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string NullDBNullToZero(object value) {
            if (value == DBNull.Value) {
                return "0";
            } 
            return NullReplace(value, "0").TrimEnd();
        }

        /// <summary>Nullであれば"0"に変換する。</summary>
        /// <param name="value"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static string NullToZero(object value) {
            return NullReplace(value, "0").TrimEnd();
        }

        /// <summary>Nullであれば任意の文字列に変える</summary>
        /// <param name="value"></param>
        /// <param name="replace"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static string NullReplace(object value, string replace, bool doTrimEnd = true) {
            string ret = null;
            ret = replace;
            if (value != null) {
                ret = value.ToString();
            }
            if (doTrimEnd) {
                return ret.TrimEnd();
            } else {
                return ret;
            }
        }

        /// <summary>Nullまたは""であれば、任意の文字列に変換する。</summary>
        /// <param name="value"></param>
        /// <param name="replace"></param>
        /// <returns></returns>
        public static string NullOrBlankReplace(object value, string replace) {
            string ret = null;
            ret = NullReplace(value, replace);
            if (string.IsNullOrEmpty(ret.TrimEnd())) {
                return replace;
            }
            return ret.TrimEnd();
        }

        /// <summary>Nullまたは""であれば、"0"に変換する。</summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string NullBlankToZero(object value) {
            return NullOrBlankReplace(value, "0");
        }

        /// <summary>半角、小文字に変換</summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string ConvNarrowLCase(object value) {
            return Strings.StrConv(Strings.StrConv(NullToBlank(value), VbStrConv.Narrow), VbStrConv.Lowercase);
        }
        /// <summary>半角大文字に変換</summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string ConvNarrowUCase(object value) {
            return Strings.StrConv(Strings.StrConv(NullToBlank(value), VbStrConv.Narrow), VbStrConv.Uppercase);
        }

        /// <summary>全角文字に変換</summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string ConvWide(object value) {
            return Strings.StrConv(NullToBlank(value), VbStrConv.Wide);
        }

        /// <summary>Windowsのファイル禁止文字を全角文字に置換</summary>
        public static string ReplaceWindowsFileNGWord2Wide(string value) {

            if (string.IsNullOrEmpty(value)) { return string.Empty; }

            //全体禁止文字
            value = value.Replace("\\", "￥");
            value = value.Replace("/", "／");
            value = value.Replace(":", "：");
            value = value.Replace("*", "＊");
            value = value.Replace("?", "？");
            value = value.Replace("\"", "”");
            value = value.Replace("<", "＜");
            value = value.Replace(">", "＞");
            value = value.Replace("|", "｜");

            return value;
        }

        /// <summary>文字列配列リストを2次元配列に変換</summary>
        /// <param name="l">文字列配列リスト</param>
        /// <returns>2次元配列</returns>
        public static string[,] ConvListTo2DArray(List<string[]> l) {
            string[,] ret = new string[l.Count, l[0].Length];

            for (int i = 0; i <= l.Count - 1; i++) {
                for (int j = 0; j <= l[i].Length - 1; j++) {
                    ret[i, j] = l[i][j];
                }
            }

            return ret;
        }

        /// <summary>インデント設定</summary>
        /// <param name="depth">インデントの深さ</param>
        /// <param name="indentStr">インデント文字</param>
        /// <returns></returns>
        public static string Indent(int depth, string indentStr) {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < depth; i++) {
                sb.Append(indentStr);
            }

            return sb.ToString();
        }

        /// <summary>複数項目値(FLEXSCHE形式<値>/<キー>;)を辞書形式に変換</summary>
        /// <param name="multiVal">複数項目値の文字列</param>
        /// <returns>辞書</returns>
        public static Dictionary<string, string> MultiValToDic(string multiVal, ILog logger = null) {
            Dictionary<string, string> ret = new Dictionary<string, string>();
            string[] valArray = multiVal.Split(';');
            foreach (string v in valArray) {
                int idx = v.LastIndexOf("/");
                string val = "";
                string key = "";
                if (idx > 0) {
                    val = v.Substring(0, idx);
                    key = v.Substring(idx + 1);
                } else {
                    val = "";
                    key = v;
                }
                if (ret.ContainsKey(key)) {
                    //重複キーがある場合は、無視する。
                    if (logger != null) {
                        logger.Warn("Duplicate Key:" + key);
                    }
                } else {
                    ret.Add(key, val);
                }
            }
            return ret;
        }
        /// <summary>複数項目値(FLEXSCHE形式<値>/<キー>;)をデータテーブル型に変換</summary>
        /// <param name="multiVal">セミコロン区切りで複数値</param>
        /// <returns>DataTable</returns>
        public static DataTable MultiValToTable(string multiVal) {
            DataTable ret = new DataTable();
            ret.Columns.Add("key");
            ret.Columns.Add("value");

            if (multiVal != "") {
                string[] valArray = multiVal.Split(';');
                foreach (string v in valArray) {
                    DataRow dr = ret.NewRow();

                    int idx = v.LastIndexOf("/");
                    string val = "";
                    string key = "";
                    if (idx > 0) {
                        val = v.Substring(0, idx);
                        key = v.Substring(idx + 1);
                    } else {
                        val = v;
                        key = "";
                    }
                    dr["key"] = key;
                    dr["value"] = val;
                    ret.Rows.Add(dr);
                }
            }
            ret.AcceptChanges();
            return ret;
        }
        /// <summary>複数項目値(FLEXSCHE形式<値>/<キー>;)の各値のサマリーを取得します。</summary>
        /// <param name="multiVal"></param>
        /// <returns></returns>
        public static string MultiValToTotal(string multiVal) {
            bool isSet = false;
            double ret = 0;
            string[] valArray = multiVal.Split(';');
            foreach (string v in valArray) {
                int idx = v.LastIndexOf("/");
                string val = "";
                string key = "";
                if (idx > 0) {
                    val = v.Substring(0, idx);
                    key = v.Substring(idx + 1);
                } else {
                    val = v;
                    key = "";
                }
                double d2;
                if (double.TryParse(val, out d2)) {
                    isSet = true;
                    ret = ret + d2;
                }
            }
            if (isSet == false) {
                return "";
            }
            return ret.ToString();
        }

        /// <summary>
        /// FLEXSCHE形式の複数項目文字列を作成
        /// </summary>
        /// <param name="dic"></param>
        /// <param name="extcolVal"></param>
        /// <returns></returns>
        public static string ToMultiItemString(SortedDictionary<int, string> dic, string[] extcolVal) {
            StringBuilder ret = new StringBuilder("");
            foreach (KeyValuePair<int, string> kvp in dic) {
                if (ret.ToString() != "") {
                    ret.Append(";");
                }
                ret.Append(extcolVal[kvp.Key - 1]);
                ret.Append("/");
                ret.Append(kvp.Value);
            }
            return ret.ToString();
        }

        /// <summary>文字列をBoolean値に読み替える。</summary>
        /// <param name="val">処理対象文字</param>
        /// <returns>true/false</returns>
        public static bool ParseBool(string val) {
            if (val == null) {
                return false;
            }
            if (val.ToLower() == "true") {
                return true;
            }
            if (val == "1") {
                return true;
            }
            return false;
        }
        /// <summary>
        /// objectをBoolean値に読み替える。</summary>
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public static bool ParseBool(object val) {
            return ParseBool(StringUtil.NullToZero(val));
        }
        /// <summary>sourceからtarget文字の出現数をカウントする。</summary>
        /// <param name="val">処理対象文字列</param>
        /// <param name="target">カウント文字</param>
        /// <returns>出現数</returns>
        public static int CountChar(string val, char target) {
            return val.Length - val.Replace(target.ToString(), "").Length;
        }

        /// <summary>
        /// 指定された文字列内にある文字列が幾つあるか数える
        /// </summary>
        /// <param name="strInput">strFindが幾つあるか数える文字列</param>
        /// <param name="strFind">数える文字列</param>
        /// <returns>strInput内にstrFindが幾つあったか</returns>
        public static int CountString(string strInput, string strFind) {
            int foundCount = 0;
            int sPos = strInput.IndexOf(strFind);
            while (sPos > -1) {
                foundCount++;
                sPos = strInput.IndexOf(strFind, sPos + 1);
            }

            return foundCount;
        }

        /// <summary>
        /// 特定の文字の間の文字を抽出する。
        /// </summary>
        /// <param name="val"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public static string ExtractBetweenChar(string val, char start, char end) {
            int startPos = val.IndexOf(start.ToString());
            int endPos = val.IndexOf(end.ToString());
            if (startPos < 0 || endPos < 0) {
                return "";
            }
            return val.Substring(startPos + 1, endPos - startPos - 1);
        }
        /// <summary>
        /// 特定の文字の間の文字を削除する。
        /// (特定文字も含む)
        /// </summary>
        /// <param name="val"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public static string RemoveBetweenChar(string val, char start, char end) {
            string tmp = ExtractBetweenChar(val, start, end);
            tmp = val.Replace(start + tmp + end, "");

            return tmp;
        }

        /// <summary>
        /// SQLServerのDML用に改行文字を変換する。
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public static string ConvSQLServerNewLine(string val) {
            var ret = new StringBuilder("");
            string[] lines = val.Split('\n');
            ret.Append("N'");
            int i = 1;
            foreach (string line in lines) {
                if (i > 1) {
                    ret.Append("'");
                    ret.Append("+ NCHAR(13) + NCHAR(10) + ");
                    ret.Append("N'");
                }
                ret.Append(line.TrimEnd('\r'));
                i++;
            }
            ret.Append("'");
            return ret.ToString();
        }

        /// <summary>
        /// 末尾の改行コードを削除
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public static string TrimCrLf(string val) {
            return Regex.Replace(val, @"[\r\n]+$", "");
        }
        /// <summary>
        /// 引数の文字をsから除去
        /// </summary>
        /// <param name="s"></param>
        /// <param name="characters"></param>
        /// <returns></returns>
        public static string RemoveChars(string s, char[] characters) {
            System.Text.StringBuilder buf = new System.Text.StringBuilder(s);
            foreach (char c in characters) {
                buf.Replace(c.ToString(), "");
            }
            return buf.ToString();
        }

        /// <summary>
        /// 指定した文字列内の指定した文字列を別の文字列に置換する。
        /// </summary>
        /// <param name="input">置換する文字列のある文字列。</param>
        /// <param name="oldValue">検索文字列。</param>
        /// <param name="newValue">置換文字列。</param>
        /// <param name="count">置換する回数。負の数が指定されたときは、すべて置換する。</param>
        /// <param name="compInfo">文字列の検索に使用するCompareInfo。</param>
        /// <param name="compOptions">文字列の検索に使用するCompareOptions。</param>
        /// <returns>置換された結果の文字列。</returns>
        public static string ReplaceByForwardCount(string input, string oldValue, string newValue, int count, 
                                                   CompareInfo compInfo, CompareOptions compOptions) {
            if (input == null || input.Length == 0 ||
                oldValue == null || oldValue.Length == 0 ||
                count == 0) {
                return input;
            }

            if (compInfo == null) {
                compInfo = CultureInfo.InvariantCulture.CompareInfo;
                compOptions = CompareOptions.Ordinal;
            }

            int inputLen = input.Length;
            int oldValueLen = oldValue.Length;
            StringBuilder buf = new StringBuilder(inputLen);

            int currentPoint = 0;
            int foundPoint = -1;
            int currentCount = 0;

            do {
                //文字列を検索する
                foundPoint = compInfo.IndexOf(input, oldValue, currentPoint, compOptions);
                if (foundPoint < 0) {
                    buf.Append(input.Substring(currentPoint));
                    break;
                }

                //見つかった文字列を新しい文字列に換える
                buf.Append(input.Substring(currentPoint, foundPoint - currentPoint));
                buf.Append(newValue);

                //次の検索開始位置を取得
                currentPoint = foundPoint + oldValueLen;

                //指定回数置換したか調べる
                currentCount++;
                if (currentCount == count) {
                    buf.Append(input.Substring(currentPoint));
                    break;
                }
            }
            while (currentPoint < inputLen);

            return buf.ToString();
        }


        /// <summary>
        /// 指定した文字列内の指定した文字列を別の文字列に置換する。
        /// </summary>
        /// <param name="input">置換する文字列のある文字列。</param>
        /// <param name="oldValue">検索文字列。</param>
        /// <param name="newValue">置換文字列。</param>
        /// <param name="count">置換する回数。負の数が指定されたときは、すべて置換する。</param>
        /// <param name="ignoreCase">大文字と小文字を区別しない時はTrue。</param>
        /// <returns>置換された結果の文字列。</returns>
        public static string ReplaceByForwardCount(string input, string oldValue, string newValue, int count, bool ignoreCase) {
            if (ignoreCase) {
                return ReplaceByForwardCount(input, oldValue, newValue, count,
                    CultureInfo.InvariantCulture.CompareInfo,
                    CompareOptions.OrdinalIgnoreCase);
            } else {
                return ReplaceByForwardCount(input, oldValue, newValue, count,
                    CultureInfo.InvariantCulture.CompareInfo,
                    CompareOptions.Ordinal);
            }
        }

        /// <summary>
        /// 指定した文字列内の指定した文字列を別の文字列に置換する。
        /// </summary>
        /// <param name="input">置換する文字列のある文字列。</param>
        /// <param name="oldValue">検索文字列。</param>
        /// <param name="newValue">置換文字列。</param>
        /// <param name="count">置換する回数。負の数が指定されたときは、すべて置換する。</param>
        /// <returns>置換された結果の文字列。</returns>
        public static string ReplaceByForwardCount(string input, string oldValue, string newValue, int count) {
            return ReplaceByForwardCount(input, oldValue, newValue, count,
                CultureInfo.InvariantCulture.CompareInfo,
                CompareOptions.Ordinal);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="src"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public static string SplitByRow(string src, int start, int end) {
            StringBuilder ret = new StringBuilder("");
            string[] rows = src.Split('\n');
            int i = 1;
            foreach (string row in rows) {
                if (start <= i && i <= end) {
                    string tmp = row.Replace("\r", "");
                    ret.AppendLine(tmp);
                }
                if (i > end) {
                    break;
                }
                i++;
            }

            return ret.ToString();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="src"></param>
        /// <param name="start"></param>
        /// <returns></returns>
        public static string SplitByRow(string src, int start) {
            return SplitByRow(src, start, int.MaxValue);
        }

        /// <summary>
        /// VBのLeftと同じ
        /// 引数のlengthより長い文字はlengthで区切る。
        /// 引数のlengthより短い文字はそのまま。
        /// </summary>
        /// <param name="val"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public static string Left(string val, int length) {
            val = NullToBlank(val);
            if (val.Length <= length) {
                return val;
            }
            return val.Substring(0, length);
        }
        //HACK Rightも実装

        /// <summary>
        /// C# で指定したエンコードで指定したバイト数"以下"に安全に文字列を切り詰める
        /// 引数のmaxByteCountより短い場合はそのまま。
        /// </summary>
        /// <param name="val"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public static string LeftB(string s, Encoding encoding, int maxByteCount) {
            var bytes = encoding.GetBytes(s);
            if (bytes.Length <= maxByteCount) return s;

            var result = s.Substring(0,
                encoding.GetString(bytes, 0, maxByteCount).Length);

            while (encoding.GetByteCount(result) > maxByteCount) {
                result = result.Substring(0, result.Length - 1);
            }
            return result;
        }
        /// <summary>
        /// ShiftJisで指定したバイト数"以下"に安全に文字列を切り詰める
        /// 引数のmaxByteCountより短い場合はそのまま。
        /// </summary>
        /// <param name="val"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public static string LeftBShiftJis(string s, int maxByteCount) {
            return LeftB(s, Encoding.GetEncoding("Shift_JIS"), maxByteCount);
        }
        //HACK Rightも実装

        public static string RemoveLastChar(string org, char c) {
            if (org.EndsWith(c.ToString())) {
                org = org.Remove(org.Length - 1);
            }

            return org;
        }
    }
}