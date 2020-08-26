using FxCommonLib.Consts;
using FxCommonLib.Consts.MES;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace FxCommonLib.Utils {
    /// <summary>
    /// DataSetユーティリティ
    /// </summary>
    public class DataSetUtil {
        /// <summary>階層レベル列の名称</summary>
        public const string HierarchyLevelColName = "h-level";

        public DataTable GetSortedDataTable(DataTable dt, string orderBy) {
            // dtのスキーマや制約をコピーしたDataTableを作成します。
            DataTable table = dt.Clone();
            DataRow[] rows = dt.Select(null, orderBy);

            foreach (DataRow row in rows) {
                DataRow addRow = table.NewRow();
                // カラム情報をコピーします。
                addRow.ItemArray = row.ItemArray;
                // DataTableに格納します。
                table.Rows.Add(addRow);
            }

            return table;
        }


        /// <summary>
        /// 列定義Enumから0行のDataTableを作成
        /// </summary>
        /// <param name="tbl"></param>
        /// <param name="colIdxType"></param>
        /// <returns></returns>
        public DataTable CreateBlankTbl(Type colIdxType) {
            DataTable tbl = new DataTable();
            foreach (Enum value in Enum.GetValues(colIdxType)) {
                var colName = EnumUtil.GetLabel(value);
                tbl.Columns.Add(colName, typeof(string));
            }
            tbl.AcceptChanges();

            return tbl;
        }

        /// <summary>
        /// 2次元リストのデータをデータテーブルに変換
        /// </summary>
        /// <param name="list">2次元リスト</param>
        /// <param name="tableName">テーブル名</param>
        /// <returns>変換後のDataTable</returns>
        public DataTable Convert2DListToDataTable(List<List<string>> list, string tableName) {
            DataTable dt = new DataTable(tableName);
            bool isHeader = true;
            foreach (List<string> row in list) {
                if (isHeader) {
                    //ヘッダ作成
                    foreach (string val in row) {
                        dt.Columns.Add(val, typeof(string));
                    }
                } else {
                    //データ部
                    int i = 0;
                    DataRow dr = dt.NewRow();
                    foreach (string val in row) {
                        dr[i] = val;
                        i++;
                    }
                    dt.Rows.Add(dr);
                } 
                isHeader = false;
            }
            dt.AcceptChanges();
            return dt;
        }
        /// <summary>
        /// 引数のDataTableの重複行を削除
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public DataTable DistinctRow(DataTable dt) {
            var dv = new DataView(dt);
            DataTable ret = dv.ToTable(true, GetColumnNameList(dt).ToArray());

            return ret;
        }
        /// <summary>
        /// 列名リスト取得
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public List<string> GetColumnNameList(DataTable dt) {
            var ret = new List<string>();
            foreach (DataColumn dc in dt.Columns) {
                ret.Add(dc.ColumnName);
            }
            return ret;
        }

        /// <summary>
        /// DataTableの型をSqlServerのテーブルの型に合わせて変換する。
        /// </summary>
        /// <param name="csvDt">処理対象のDataTable</param>
        /// <param name="colDic">列の型定義辞書</param>
        /// <returns>変換後のDataTable</returns>
        public DataTable ConvertSqlTable(DataTable csvDt, Dictionary<string, Type> colDic) {
            DataTable sqlDt = new DataTable();

            //DataTableの型をSqlServerと合せる
            //ヘッダーの設定
            foreach (string colName in colDic.Keys) {
                sqlDt.Columns.Add(colName, colDic[colName]);
            }
            //データのコピー
            foreach (DataRow dr in csvDt.Rows) {
                DataRow retRow = sqlDt.NewRow();
                foreach (string colName in colDic.Keys) {
                    string csvColName = colName.Replace("_", "-");
                    //if (colDic[colName] == typeof(SqlString)) {
                    //    retRow[colName] = (object)dr[csvColName] as String;
                    //} else if (colDic[colName] == typeof(SqlDecimal)) {
                    //    retRow[colName] = (object)dr[csvColName] as num;
                    //}

                    if (csvDt.Columns.IndexOf(csvColName) >= 0) {
                        if ((string)dr[csvColName] == "") {
                            retRow[colName] = DBNull.Value;
                        } else if (colDic[colName] == typeof(Boolean)) {
                            retRow[colName] = ((string)dr[csvColName] == "0") ? false : true;
                        } else {
                            retRow[colName] = dr[csvColName];
                        }
                    }
                }
                sqlDt.Rows.Add(retRow);
            }
            sqlDt.AcceptChanges();

            return sqlDt;
        }

        /// <summary>
        /// 木構造に展開したDataTableを再作成します。
        /// </summary>
        /// <param name="sourceTable">変換元のテーブル</param>
        /// <param name="parentColName">親を識別する列名</param>
        /// <param name="idColName">自身を識別する列名</param>
        /// <returns></returns>
        public DataTable CreateTreeTable(
            DataTable sourceTable, string rootId, string parentColName, string idColName, string levelColName = HierarchyLevelColName) {
            Stack<int> levelStack = new Stack<int>();
            DataTable tmp = sourceTable.Copy();
            //階層レベル列を追加
            tmp.Columns.Add(levelColName);
            DataTable treeTable = tmp.Clone();
            AddTreeElementRecv(sourceTable, treeTable, levelStack, rootId, parentColName, idColName, levelColName);
            return treeTable;
        }
        /// <summary>
        /// 深さ優先探索で木構造を再帰読み込みし、treeTableにレコードをコピー
        /// </summary>
        /// <param name="sourceTable"></param>
        /// <param name="treeTable"></param>
        /// <param name="currentId"></param>
        /// <param name="parentColName"></param>
        /// <param name="idColName"></param>
        private void AddTreeElementRecv(
            DataTable sourceTable, DataTable treeTable, Stack<int> levelStack, 
            string currentId, string parentColName, string idColName, string levelColName) {

            levelStack.Push(levelStack.Count);
            //子を取得
            DataRow[] children = (
                from row in sourceTable.AsEnumerable()
                let parentCol = row.Field<string>(parentColName)
                where parentCol == currentId
                select row).ToArray();
            foreach (DataRow dr in children) {
                DataRow newRow = treeTable.NewRow();
                newRow.ItemArray = dr.ItemArray;
                newRow[levelColName] = levelStack.Count;
                treeTable.Rows.Add(newRow);
                AddTreeElementRecv(sourceTable, treeTable, levelStack, dr[idColName].ToString(), parentColName, idColName, levelColName);
            }
            levelStack.Pop();
        }

        /// <summary>
        /// リクエストデータセット作成
        /// </summary>
        /// <param name="dic"></param>
        /// <returns></returns>
        public DataSet CreateRequestDataSet(Dictionary<string, string> dic) {
            DataSet ret = new DataSet(CommonConsts.ParamDataSet);
            DataTable tbl = new DataTable(CommonConsts.ParamDataTbl);
            foreach (var key in dic.Keys) {
                tbl.Columns.Add(key);
            }
            DataRow dr = tbl.NewRow();
            foreach (var key in dic.Keys) {
                dr[key] = dic[key];
            }
            tbl.Rows.Add(dr);
            ret.Tables.Add(tbl);
            ret.AcceptChanges();

            return ret;
        }

        /// <summary>
        /// tmpTableの全行をdestTableに追加
        /// </summary>
        /// <param name="destTable"></param>
        /// <param name="tmpTable"></param>
        public void AppendTable(DataTable destTable, DataTable tmpTable) {
            foreach (DataRow dr in tmpTable.Rows) {
                DataRow newRow = destTable.NewRow();
                newRow.ItemArray = dr.ItemArray;
                destTable.Rows.Add(newRow);
            }
        }


        /// <summary>
        /// DataSet内のDataTableを１つのDataTableとしてマージ
        /// (テーブル構造が同じであること。)
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        public DataTable MergeTable(DataSet ds) {
            DataSetUtil dsu = new DataSetUtil();
            DataTable ret = new DataTable();
            bool isFirst = true;

            foreach(DataTable t in ds.Tables) {
                if (isFirst) {
                    //初回に構造をコピー
                    ret = t.Clone();
                    isFirst = false;
                }
                dsu.AppendTable(ret, t);
            }

            return ret;
        }
        /// <summary>
        /// DataSet内のDataTableを１つのDataTableとしてマージ
        /// (テーブル構造が同じであること。)
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public DataTable MergeTable(List<DataTable> list) {
            DataSetUtil dsu = new DataSetUtil();
            DataTable ret = new DataTable();
            bool isFirst = true;

            foreach(DataTable t in list) {
                if (isFirst) {
                    //初回に構造をコピー
                    ret = t.Clone();
                    isFirst = false;
                }
                dsu.AppendTable(ret, t);
            }

            return ret;
        }

        /// <summary>
        /// 行の順序を反転させる。
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public DataTable ReverseRows(DataTable dt) {
            DataTable ret = dt.Clone();

            for (int i = dt.Rows.Count - 1; i >= 0; i--) {
                DataRow newRow = ret.NewRow();
                newRow.ItemArray = dt.Rows[i].ItemArray;
                ret.Rows.Add(newRow);
            }
            return ret;
        }


        /// <summary>
        /// DataTableの値をInsert文に変換（SQLServer用）
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="doTruncate"></param>
        /// <returns></returns>
        public string ConvInsertStatementForSQLServer(DataTable dt, bool doTruncate = true) {
            var sql = new StringBuilder("");
            if (doTruncate) {
                sql.Append("TRUNCATE TABLE ");
                sql.Append(dt.TableName);
                sql.AppendLine(";");
            }

            //列名作成
            var cols = new StringBuilder("(");
            int i = 1;
            foreach (DataColumn dc in dt.Columns) {
                if (i > 1) {
                    cols.Append(",");
                }
                cols.Append("[");
                cols.Append(dc.ColumnName);
                cols.Append("]");
                i++;
            }
            cols.Append(") ");

            i = 1;
            string preItemCode = "";
            foreach (DataRow dr in dt.Rows) {
                if (!doTruncate) {
                    string currentItemCode = "";
                    if (dt.TableName == "[bom].[t_extcol]") {
                        currentItemCode = StringUtil.NullToBlank(dr["code"]);
                    } else {
                        currentItemCode = StringUtil.NullToBlank(dr["item_code"]);
                    }
                    if (preItemCode != currentItemCode) {
                        sql.Append("DELETE FROM ");
                        sql.Append(dt.TableName);
                        sql.Append(" ");
                        if (dt.TableName == "[bom].[t_extcol]") {
                            sql.Append("WHERE [code] = '" + StringUtil.NullToBlank(dr["code"]) + "';");
                        } else {
                            sql.Append("WHERE [item_code] = '" + StringUtil.NullToBlank(dr["item_code"]) + "'");
                        }
                        sql.AppendLine("");

                        preItemCode = currentItemCode;
                    }
                }

                sql.Append("INSERT INTO ");
                sql.Append(dt.TableName);
                sql.Append(" ");
                sql.Append(cols.ToString());
                sql.Append(" VALUES (");

                int j = 1;
                foreach (DataColumn dc in dt.Columns) {
                    if (j > 1) {
                        sql.Append(",");
                    }
                    Object val = dr[dc.ColumnName];
                    if (val == null) {
                        sql.Append("NULL");
                    } else if (val.ToString() == "GETDATE()") {
                        sql.Append("GETDATE()");
                    } else {
                        sql.Append(StringUtil.ConvSQLServerNewLine(val.ToString()));
                    }
                    j++;
                }
                sql.Append(");");
                sql.AppendLine("");
                i++;
                if (i % 100 == 0) {
                    sql.AppendLine("GO");
                }
            }

            return sql.ToString();
        }

        /// <summary>
        /// 等結合してマッチした行だけ抽出
        /// </summary>
        /// <param name="srcTbl"></param>
        /// <param name="matchTbl"></param>
        /// <param name="srcTblKey"></param>
        /// <param name="matchTblKey"></param>
        /// <returns></returns>
        public static DataTable ExtractMachingData(DataTable srcTbl, DataTable matchTbl, string srcTblKey, string matchTblKey) {
            //Linqで等結合
            DataRow[] extractRows = (
                from srcRow in srcTbl.AsEnumerable()
                join matchRow in matchTbl.AsEnumerable()
                on srcRow.Field<String>(srcTblKey) equals matchRow.Field<String>(matchTblKey)
                select srcRow).ToArray();

            DataTable ret = srcTbl.Clone();
            foreach (DataRow dr in extractRows) {
                DataRow newRow = ret.NewRow();
                newRow.ItemArray = dr.ItemArray;
                ret.Rows.Add(newRow);
            }

            return ret;
        }
        /// <summary>
        /// データテーブルから指定されたkey,valueで辞書を作成
        /// </summary>
        /// <param name="target"></param>
        /// <param name="keyCol"></param>
        /// <param name="valueCol"></param>
        /// <returns></returns>
        public static Dictionary<string, string> CreateDic(DataTable target, string keyCol, string valueCol) {
            var dic = new Dictionary<string, string>();
            foreach (DataRow r in target.Rows) {
                string key = StringUtil.NullToBlank(r[keyCol]);
                string val = StringUtil.NullToBlank(r[valueCol]);
                if (dic.ContainsKey(key) == false) {
                    dic.Add(key, val);
                }
            }
            return dic;
        }

        /// <summary>
        /// code,nameを保持するテーブルから辞書を作成
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static Dictionary<string, string> CreateCodeNameDic(DataTable dt) {
            return CreateDic(dt, CommonConsts.code, CommonConsts.name);
        }
        /// <summary>
        /// 対象行を全て削除
        /// </summary>
        /// <param name="table"></param>
        /// <param name="columnName"></param>
        /// <param name="val"></param>
        public static void RemoveSelectRow(DataTable table, string columnName, object val) {
            DataRow[] rows = table.Select(columnName + "='" + val + "'");
            Array.ForEach<DataRow>(rows, row => table.Rows.Remove(row));
            table.AcceptChanges();
        }
        /// <summary>
        /// DataTableの行入替
        /// (同一Tableであること。)
        /// </summary>
        /// <param name="row1"></param>
        /// <param name="row2"></param>
        /// <returns></returns>
        public static bool SwapRow(DataRow row1, DataRow row2) {
            if (row1.Table != row2.Table) {
                // 同じテーブルに所属している必要があります
                return false;
            }

            DataTable table = row1.Table;

            // 
            // row1のコピーを作成
            int index1 = table.Rows.IndexOf(row1);
            DataRow newRow1 = table.NewRow();
            newRow1.ItemArray = row1.ItemArray;

            // row2のコピーを作成
            int index2 = table.Rows.IndexOf(row2);
            DataRow newRow2 = table.NewRow();
            newRow2.ItemArray = row2.ItemArray;

            // 
            // コピーを入れ替えて挿入しつつ、元の行は削除
            table.Rows.InsertAt(newRow1, index2);
            table.Rows.Remove(row2);
            table.Rows.InsertAt(newRow2, index1);
            table.Rows.Remove(row1);
            return true;
        }

        #region Linq記述Sample

        public static DataTable GetSumGroupByTable(DataTable dt, string groupCol, string summaryCol) {
            var newDt = dt.AsEnumerable()
              .GroupBy(r => r.Field<string>(groupCol))
              .Select(g => {
                  var row = dt.NewRow();
                  row[groupCol] = g.Key;
                  row[summaryCol] = g.Sum(r => r.Field<int>(summaryCol));
                  return row;
              }).CopyToDataTable();

            return newDt;
        }
        public static DataTable GetMinGroupByTable(DataTable dt, string groupCol, string minCol) {
            var newDt = dt.AsEnumerable()
              .GroupBy(r => r.Field<string>(groupCol))
              .Select(g => {
                  var row = dt.NewRow();
                  row[groupCol] = g.Key;
                  row[minCol] = g.Min(r => r.Field<string>(minCol));
                  return row;
              }).CopyToDataTable();

            return newDt;
        }
        public static DataTable GetMinGroupByTable2(DataTable dt, string groupCol, string minCol) {
            var newDt = dt.AsEnumerable()
              .GroupBy(r => r.Field<string>(groupCol).Substring(0, 5))
              .Select(g => {
                  var row = dt.NewRow();
                  row[groupCol] = g.Key;
                  row[minCol] = g.Min(r => r.Field<string>(minCol));
                  return row;
              }).CopyToDataTable();

            newDt = dt.AsEnumerable()
              .GroupBy(r => r.Field<string>(groupCol).Substring(0, r.Field<string>(groupCol).Length - 1))
              .Select(g => {
                  var row = dt.NewRow();
                  row[groupCol] = g.Key;
                  row[minCol] = g.Min(r => r.Field<string>(minCol));
                  return row;
              }).CopyToDataTable();
            return newDt;
        }


        #endregion Linq記述Sample
    }
}
