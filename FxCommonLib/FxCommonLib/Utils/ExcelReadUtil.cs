using ExcelDataReader;
using System;
using System.Data;
using System.IO;

namespace FxCommonLib.Utils {
    /// <summary>
    /// ExcelDataReaderユーティリティ
    /// 参考
    /// GitHub - ExcelDataReader/ExcelDataReader: Lightweight and fast library written in C# for reading Microsoft Excel files
    /// https://github.com/ExcelDataReader/ExcelDataReader
    ///
    /// </summary>
    public class ExcelReadUtil {

        #region Constants
        #endregion Constants
        #region Properties
        #endregion Properties
        #region MemberVariables
        #endregion MemberVariables
        #region Constractors
        #endregion Constractors
        #region PublicMethods

        /// <summary>
        /// Excelファイル読み込み（シート名指定）
        /// シート名が存在しない場合はnullを返却
        /// </summary>
        /// <param name="filePath">ファイルパス</param>
        /// <param name="sheetName">シート名</param>
        /// <returns>シートを読み込んだDataTable</returns>
        public DataTable ReadExcelDataOneSheet(string filePath, string sheetName) {

            DataSet ds = null;
            DataTable dt = null;

            ds = ReadExcelDataAsDataSet(filePath);
            if (ds.Tables.Contains(sheetName)) {
                DeepCopyUtil dcu = new DeepCopyUtil();
                dt = dcu.DeepCopy(ds.Tables[sheetName]);
            }

            return dt;

        }

        /// <summary>
        /// Excelファイル読み込み（シートインデックス指定）
        /// 存在しない場合はnullを返却
        /// </summary>
        /// <param name="filePath">ファイルパス</param>
        /// <param name="sheetIndex">シートインデックス</param>
        /// <returns>シートを読み込んだDataTable</returns>
        public DataTable ReadExcelDataBySheetIndex(string filePath, int sheetIndex) {

            DataSet ds = null;
            DataTable dt = null;

            ds = ReadExcelDataAsDataSet(filePath);
            if (0 <= sheetIndex && sheetIndex < ds.Tables.Count) {
                DeepCopyUtil dcu = new DeepCopyUtil();
                dt = ds.Tables[sheetIndex];
            }

            return dt;
        }

        /// <summary>
        /// Excelファイル読み込み（DataSet）
        /// Excelバージョン2.0-2003(*.xls)から2007 (*.xlsx)に対応
        /// </summary>
        /// <param name="filePath">ファイルパス</param>
        /// <returns>シートごとのDataTableを持つDataSet</returns>
        public DataSet ReadExcelDataAsDataSet(string filePath) {
            DataSet ds = null;
            var ext = Path.GetExtension(filePath);

            try {
                using (var stream = File.Open(filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite)) {

                    // Auto-detect format, supports:
                    //  - Binary Excel files (2.0-2003 format; *.xls)
                    //  - OpenXml Excel files (2007 format; *.xlsx)
                    using (var reader = ExcelReaderFactory.CreateReader(stream)) {

                        var result = reader.AsDataSet();
                        // The result of each spreadsheet is in result.Tables
                        ds = (DataSet)result;
                    }
                }
            } catch (Exception ex) {
                throw ex;
            }

            return ds;
        }

        #endregion PublicMethods
        #region ProtectedMethods
        #endregion ProtectedMethods
        #region PrivateMethods
        #endregion PrivateMethods


    }
}