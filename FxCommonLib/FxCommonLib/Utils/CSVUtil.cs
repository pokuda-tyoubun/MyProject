using LumenWorks.Framework.IO.Csv;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace FxCommonLib.Utils {
    /// <summary>
    /// CSVユーティリティ
    /// <seealso cref="LumenWorks.Framework.IO.Csv"/>
    /// </summary>
    public class CSVUtil {

        #region PublicMethods
        /// <summary>
        /// CSV文字列読み込み
        /// ※要素は全トリムされます。
        /// </summary>
        /// <param name="val">CSV文字列</param>
        /// <param name="tableName">戻り値にセットするテーブル名</param>
        /// <param name="delimiter">区切り文字</param>
        /// <returns>CSVデータをDataTableとして返す。</returns>
        public DataTable ReadStringCsv(string val, string tableName, char delimiter) {
            StringReader sr = new StringReader(val);
            using (CachedCsvReader csvr = new CachedCsvReader(sr, true, delimiter)) {
               return CsvToDataTable(csvr, tableName, false);
            }
        }
        /// <summary>
        /// CSVファイル読み込み
        /// ※要素は全トリムされます。
        /// </summary>
        /// <param name="csvPath">CSVファイルへのパス</param>
        /// <param name="tableName">戻り値にセットするテーブル名</param>
        /// <returns>CSVデータをDataTableとして返す。</returns>
        public DataTable ReadCsv(string csvPath, string tableName) {
            return ReadCsv(csvPath, tableName, ',');
        }
        /// <summary>
        /// CSVファイル読み込み
        /// ※要素は全トリムされます。
        /// </summary>
        /// <param name="csvPath">CSVファイルへのパス</param>
        /// <param name="tableName">戻り値にセットするテーブル名</param>
        /// <param name="delimiter">区切り文字</param>
        /// <returns>CSVデータをDataTableとして返す。</returns>
        public DataTable ReadCsv(string csvPath, string tableName, char delimiter) {
            return ReadCsv(csvPath, tableName, delimiter, Encoding.GetEncoding(932));
        }
        /// <summary>
        /// CSVファイル読み込み
        /// ・要素は全トリムされます。
        /// ・ファイルオープン時でも読み込みます。
        /// </summary>
        /// <param name="csvPath">CSVファイルへのパス</param>
        /// <param name="tableName">戻り値にセットするテーブル名</param>
        /// <param name="delimiter">区切り文字</param>
        /// <param name="enc">文字エンコード</param>
        /// <returns>CSVデータをDataTableとして返す。</returns>
        public DataTable ReadCsv(string csvPath, string tableName, char delimiter, Encoding enc) {
            return ReadCsv(csvPath, tableName, delimiter, enc, true);
        }
        /// <summary>
        /// CSVファイル読み込み
        /// ・要素は全トリムされます。
        /// ・ファイルオープン時でも読み込みます。
        /// </summary>
        /// <param name="csvPath">CSVファイルへのパス</param>
        /// <param name="tableName">戻り値にセットするテーブル名</param>
        /// <param name="delimiter">区切り文字</param>
        /// <param name="enc">文字エンコード</param>
        /// <param name="existsHeader">ヘッダ行が存在するか</param>
        /// <returns>CSVデータをDataTableとして返す。</returns>
        public DataTable ReadCsv(string csvPath, string tableName, char delimiter, Encoding enc, bool existsHeader) {
            using (FileStream fs = new FileStream(csvPath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite)) {
                using (TextReader sr = new StreamReader(fs, enc)) {
                    using (CachedCsvReader csvr = new CachedCsvReader(sr, existsHeader, delimiter)) {
                        return CsvToDataTable(csvr, tableName, existsHeader);
                    }
                }
            }
        }

        /// <summary>
        /// CSVファイル読み込み
        /// ※要素はトリムされない。
        /// ※型を指定するにはSchema.iniを定義する必要がある。
        /// HACK Schema.iniが効いていない？
        /// </summary>
        /// <param name="csvPath"></param>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public DataTable ReadCsvByJet(string csvPath, string tableName) {
            //接続文字列
            string conStr = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + Path.GetDirectoryName(csvPath) + 
                ";Extended Properties=\"text;HDR=Yes;FMT=Delimited\"";
            OleDbConnection con = new OleDbConnection(conStr);

            string sql = "SELECT * FROM [" + Path.GetFileName(csvPath) + "]";
            OleDbDataAdapter da = new OleDbDataAdapter(sql, con);

            //DataTableに格納する
            DataTable dt = new DataTable();
            dt.TableName = tableName;
            da.Fill(dt);

            return dt;
        }

        /// <summary>
        /// CSVファイル読み込み
        /// </summary>
        /// <param name="csvPath"></param>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public DataTable ReadCsvWithoutTrim(string csvPath, string tableName) {

            StreamReader sr = null;
            List<List<string>> list2D = new List<List<string>>();

            try {
                //Shift-JISコードとして開く
                sr = new StreamReader(csvPath, Encoding.GetEncoding("shift_jis"));
                //内容をすべて読み込む
                string csvText = sr.ReadToEnd();
                list2D = CsvTo2DList(csvText);

                //CSV列数チェック
                int colCount = 0;
                bool isFirst = true;
                foreach (List<string> list in list2D) {
                    if (isFirst) {
                        colCount = list.Count;
                        isFirst = false;
                    } else {
                        if (colCount != list.Count) {
                            throw new InvalidDataException("The number of columns is different.");
                        }
                    }
                }

            } finally {
                if (sr != null) {
                    sr.Close();
                }
                
            }

            DataSetUtil dsu = new DataSetUtil();
            return dsu.Convert2DListToDataTable(list2D, tableName);
        }

        /// <summary>
        /// CSVを2次元Listに変換
        /// </summary>
        /// <param name="csvText">CSVの内容が入ったString</param>
        /// <returns>変換結果の2DList</returns>
        private List<List<string>> CsvTo2DListOld(string csvText) {
            List<List<string>> csvRecords = new List<List<string>>();

            //前後の改行を削除しておく
            csvText = csvText.Trim(new char[] {'\r', '\n'});

            //一行取り出すための正規表現
            Regex regLine = new Regex("^.*(?:\\n|$)", RegexOptions.Multiline);

            //1行のCSVから各フィールドを取得するための正規表現
            Regex regCsv = new Regex("\\s*(\"(?:[^\"]|\"\")*\"|[^,]*)\\s*,", RegexOptions.None);

            Match mLine = regLine.Match(csvText);
            while (mLine.Success) {
                //一行取り出す
                string line = mLine.Value;
                //改行記号が"で囲まれているか調べる
                while ((StringUtil.CountString(line, "\"") % 2) == 1) {
                    mLine = mLine.NextMatch();
                    if (!mLine.Success) {
                        throw new ApplicationException("不正なCSV");
                    }
                    line += mLine.Value;
                }
                //行の最後の改行記号を削除
                line = line.TrimEnd(new char[] {'\r', '\n'});
                //最後に「,」をつける
                line += ",";

                //1つの行からフィールドを取り出す
                List<string> csvFields = new List<string>();
                Match m = regCsv.Match(line);
                while (m.Success) {
                    string field = m.Groups[1].Value;
                    //前後の空白を削除
                    //field = field.Trim();
                    Debug.Print("[" + field + "]");
                    //"で囲まれている時
                    if (field.StartsWith("\"") && field.EndsWith("\"")) {
                        //前後の"を取る
                        field = field.Substring(1, field.Length - 2);
                        //「""」を「"」にする
                        field = field.Replace("\"\"", "\"");
                    }
                    csvFields.Add(field);
                    m = m.NextMatch();
                }

                csvRecords.Add(csvFields);

                mLine = mLine.NextMatch();
            }

            return csvRecords;
        }


        /// <summary>
        /// CSVをArrayListに変換
        /// </summary>
        /// <param name="csvText">CSVの内容が入ったString</param>
        /// <returns>変換結果のArrayList</returns>
        public List<List<string>> CsvTo2DList(string csvText) {
            //前後の改行を削除しておく
            csvText = csvText.Trim(new char[] {'\r', '\n'});

            List<List<string>> csvRecords = new List<List<string>>();
            List<string> csvFields = new List<string>();

            int csvTextLength = csvText.Length;
            int startPos = 0, endPos = 0;
            string field = "";

            while (true) {
                //空白を飛ばす
                while (startPos < csvTextLength &&
                    (csvText[startPos] == ' ' || csvText[startPos] == '\t')) {
                    startPos++;
                }

                //データの最後の位置を取得
                if (startPos < csvTextLength && csvText[startPos] == '"') {
                    //"で囲まれているとき
                    //最後の"を探す
                    endPos = startPos;
                    while (true) {
                        endPos = csvText.IndexOf('"', endPos + 1);
                        if (endPos < 0) {
                            throw new ApplicationException("\"が不正");
                        }
                        //"が2つ続かない時は終了
                        if (endPos + 1 == csvTextLength || csvText[endPos + 1] != '"') {
                            break;
                        }
                        //"が2つ続く
                        endPos++;
                    }

                    //一つのフィールドを取り出す
                    field = csvText.Substring(startPos, endPos - startPos + 1);
                    //""を"にする
                    field = field.Substring(1, field.Length - 2).Replace("\"\"", "\"");

                    endPos++;
                    //空白を飛ばす
                    while (endPos < csvTextLength &&
                        csvText[endPos] != ',' && csvText[endPos] != '\n') {
                        endPos++;
                    }
                } else {
                    //"で囲まれていない
                    //カンマか改行の位置
                    endPos = startPos;
                    while (endPos < csvTextLength &&
                        csvText[endPos] != ',' && csvText[endPos] != '\n') {
                        endPos++;
                    }

                    //一つのフィールドを取り出す
                    field = csvText.Substring(startPos, endPos - startPos);
                    //後の空白を削除
                    field = field.TrimEnd();
                }

                //フィールドの追加
                csvFields.Add(field);

                //行の終了か調べる
                if (endPos >= csvTextLength || csvText[endPos] == '\n') {
                    //行の終了
                    //レコードの追加
                    csvRecords.Add(csvFields);
                    csvFields = new List<string>();

                    if (endPos >= csvTextLength) {
                        //終了
                        break;
                    }
                }

                //次のデータの開始位置
                startPos = endPos + 1;
            }

            return csvRecords;
        }


        /// <summary>
        /// CSVファイルに出力
        /// </summary>
        /// <param name="dt">CSVファイルに出力するデータ</param>
        /// <param name="csvPath">出力先のパス</param>
        /// <param name="writeHeader">ヘッダを出力するかどうか</param>
        /// <param name="enc">文字エンコーディング</param>
        public void WriteCsv(DataTable dt, string csvPath, bool writeHeader, Encoding enc) {
            //書き込むファイルを開く
            StreamWriter sr = new StreamWriter(csvPath, false, enc);

            int colCount = dt.Columns.Count;
            int lastColIndex = colCount - 1;

            //ヘッダを書き込む
            if (writeHeader) {
                for (int i = 0; i < colCount; i++) {
                    //ヘッダの取得
                    string field = dt.Columns[i].Caption;
                    //"で囲む
                    field = EncloseDoubleQuotesIfNeed(field);
                    //フィールドを書き込む
                    sr.Write(field);
                    //カンマを書き込む
                    if (lastColIndex > i) {
                        sr.Write(',');
                    }
                }
                //改行する
                sr.Write("\r\n");
            }

            //レコードを書き込む
            foreach (DataRow row in dt.Rows) {
                for (int i = 0; i < colCount; i++) {
                    //フィールドの取得
                    string field = row[i].ToString();
                    //"で囲む
                    field = EncloseDoubleQuotesIfNeed(field);
                    //フィールドを書き込む
                    sr.Write(field);
                    //カンマを書き込む
                    if (lastColIndex > i) {
                        sr.Write(',');
                    }
                }
                //改行する
                sr.Write("\r\n");
            }

            //閉じる
            sr.Close();
        }
        /// <summary>
        /// CSVファイルに出力(CP932)
        /// </summary>
        /// <param name="dt">CSVファイルに出力するデータ</param>
        /// <param name="csvPath">出力先のパス</param>
        /// <param name="writeHeader">ヘッダを出力するかどうか</param>
        public void WriteCsv(DataTable dt, string csvPath, bool writeHeader) {
            //CP932で書き込み
            Encoding enc = Encoding.GetEncoding(932);
            WriteCsv(dt, csvPath, writeHeader, enc);
        }
        #endregion PublicMethods

        #region PrivateMethods
        /// <summary>
        /// CSVファイルをDataTableに変換
        /// LumenWorksのCSVパーサを使って処理
        /// </summary>
        /// <param name="csvr">CSVファイル読み込み結果</param>
        /// <param name="tableName">戻り値にセットするテーブル名</param>
        /// <param name="existsHeader">ヘッダ行が存在するか</param>
        /// <returns>CachedCsvReader</returns>
        private DataTable CsvToDataTable(CachedCsvReader csvr, string tableName, bool existsHeader) {
            DataTable dt = new DataTable(tableName);
            string[] headers = null;
            if (existsHeader) {
                headers = csvr.GetFieldHeaders();

                //ヘッダーの設定
                foreach (string colName in headers) {
                    dt.Columns.Add(colName, typeof(string));
                }
            } else {
                List<string> tmp = new List<string>(); 
                for (int i = 0; i < csvr.FieldCount; i++ ) {
                    dt.Columns.Add(i.ToString(), typeof(string));
                    tmp.Add(i.ToString());
                }
                headers = tmp.ToArray();

                string[] firstRow = csvr.GetFieldHeaders();
                DataRow dr = dt.NewRow();
                for (int i = 0; i < firstRow.Length; i++) {
                    dr[i] = firstRow[i];
                }
                dt.Rows.Add(dr);
            }

            while (csvr.ReadNextRecord()) {
                DataRow dr = dt.NewRow();
                for (int i = 0; i < headers.Length; i++) {
                    dr[i] = csvr[i];
                }
                dt.Rows.Add(dr);
            }
            dt.AcceptChanges();

            return dt;
        }
        /// <summary>
        /// 必要ならば文字列をダブルクォートで囲む
        /// </summary>
        /// <param name="field">値</param>
        /// <returns>処理後の文字列</returns>
        private string EncloseDoubleQuotesIfNeed(string field) {
            if (NeedEncloseDoubleQuotes(field)) {
                return EncloseDoubleQuotes(field);
            }
            return field;
        }

        /// <summary>
        /// 文字列をダブルクォートで囲む
        /// </summary>
        /// <param name="field">値</param>
        /// <returns>処理後の文字列</returns>
        private string EncloseDoubleQuotes(string field) {
            if (field.IndexOf('"') > -1) {
                //"を""とする
                field = field.Replace("\"", "\"\"");
            }
            return "\"" + field + "\"";
        }

        /// <summary>
        /// 文字列をダブルクォートで囲む必要があるか調べる
        /// </summary>
        /// <param name="field">値</param>
        /// <returns>ダブルクォートで囲む必要があるかどうか</returns>
        private bool NeedEncloseDoubleQuotes(string field) {
            return field.IndexOf('"') > -1 ||
                field.IndexOf(',') > -1 ||
                field.IndexOf('\r') > -1 ||
                field.IndexOf('\n') > -1 ||
                field.StartsWith(" ") ||
                field.StartsWith("\t") ||
                field.EndsWith(" ") ||
                field.EndsWith("\t");
        }
        #endregion PrivateMethods
    }
}
