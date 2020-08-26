using C1.Win.C1FlexGrid;
using FxCommonLib.Consts;
using FxCommonLib.Consts.MES;
using FxCommonLib.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Text;
using System.Windows.Forms;

namespace FxCommonLib.Utils {
    /// <summary>
    /// Excelユーティリティ
    ///参考
    ///https://zenmai.wordpress.com/2011/06/24/excel%E3%81%AE%E5%8F%82%E7%85%A7%E3%82%92%E8%BF%BD%E5%8A%A0%E3%81%9B%E3%81%9A%E3%81%ABexcel%E3%82%92%E4%BD%BF%E3%81%86c/
    /// </summary>
    public class ExcelUtil {

        #region Constants
        /// <summary>一括してセルに値をコピーする際のバッファサイズ</summary>
        private const int RowBufSize = 2000;
        /// <summary>MsoTriState(Office2013以降)</summary>
        public enum MsoTriState : int {
            msoCTrue = 1, //サポートされていない
            msoFalse = 0,
            msoTriStateMixed = -2, //サポートされていない
            msoTriStateToggle = -3, //サポートされていない
            msoTrue = -1
        }

        /// <summary>
        /// 図形を拡大縮小する場合、図形の位置を保持する部分を指定します。
        /// </summary>
        enum MsoScaleFrom : int {
            msoScaleFromTopLeft = 0,
            msoScaleFromMiddle = 1,
            msoScaleFromBottomRight = 2,
        }

        /// <summary>移動方向</summary>
        public enum XlDirection : int {
            XlDown = -4121,
            XlToLeft = -4159,
            XlToRight = -4161,
            XlUp = -4162
        }
        /// <summary>罫線の種類</summary>
        public enum XlLineStyle : int {
            XlContinuous = 1,
            XlDash = -4115,
            XlDashDot = 4,
            XlDashDotDot = 5,
            XlDot = -4118,
            XlDouble = -4119,
            XlLineStyleNone = -4142,
            XlSlantDashDot = 13
        }
        /// <summary>Excelファイルフォーマット</summary>
        public enum XlFileFormat : int {
            XlWebArchive = 45, //.mht .mhtml
            XlExcel12 = 50, //xlsb
            XlOpenXMLWorkbook = 51, //xlsx
            XlOpenXMLWorkbookMacroEnabled = 52, //xlsm
            XlExcel8 = 56, //xls
            XlCSV = 6
        }
        #endregion Constants

        #region MemberVariables
        /// <summary>COM操作ユーティリティ</summary>
        private COMUtil _comUtil = new COMUtil();
        #endregion MemberVariables

        #region PublicMethods
        /// <summary>
        /// 指定したシートマップに従ってデータをコピー
        /// </summary>
        /// <param name="srcBookPath"></param>
        /// <param name="destBookPath"></param>
        /// <param name="sheetMapping"></param>
        public void CopySheetsData(string srcBookPath, string destBookPath, OrderedDictionary<string, string> sheetMapping) {
            COMUtil comUtil = new COMUtil();
            object app = null;
            object srcBooks = null;
            object srcBook = null;
            object srcSheet = null;

            object destBooks = null;
            object destBook = null;
            object destSheet = null;

            Cursor.Current = Cursors.WaitCursor;
            try {
                //テンプレートをオープン
                app = comUtil.CreateObject("Excel.Application");
                SetDisplayAlerts(app, false);

                srcBooks = GetWorkbooks(app);
                srcBook = Open(srcBooks, srcBookPath);

                destBooks = GetWorkbooks(app);
                destBook = Open(destBooks, destBookPath);

                foreach (var kvp in sheetMapping) {
                    string srcSheetName = kvp.Key;
                    string destSheetName = kvp.Value;

                    srcSheet = GetSheets(srcBook, srcSheetName);
                    CopyCells(srcSheet);
                    destSheet = GetSheets(destBook, destSheetName);
                    PasteCells(destBook, destSheet);
                }
                Close(srcBook);
                Save(destBook);
                Close(destBook);

            } finally {
                SetDisplayAlerts(app, true);
                Quit(app);

                comUtil.MReleaseComObject(srcSheet);
                comUtil.MReleaseComObject(destSheet);
                comUtil.MReleaseComObject(srcBook);
                comUtil.MReleaseComObject(destBook);
                comUtil.MReleaseComObject(app);
                GC.Collect();
                Cursor.Current = Cursors.Default;
            }
        }
        /// <summary>
        /// Excelファイルを開く
        /// </summary>
        /// <param name="books">Workbooks</param>
        /// <param name="xlsFilePath">Excelファイルへのパス</param>
        /// <returns>Workbook</returns>
        public object Open(object books, string xlsFilePath) {
            return Open(books, xlsFilePath, password: Type.Missing);
        }
        /// <summary>
        /// Excelファイルを開く
        /// </summary>
        /// <param name="books"></param>
        /// <param name="xlsFilePath"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public object Open(object books, string xlsFilePath, string password) {
            return Open(books, xlsFilePath, password: (object)password);
        }
        /// <summary>
        /// Excelファイルを開く
        /// </summary>
        /// <param name="books"></param>
        /// <param name="xlsFilePath"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        private object Open(object books, string xlsFilePath, object password) {
            object[] parameters = new object[15];
            parameters[0] = xlsFilePath; //FileName
            parameters[1] = Type.Missing; //UpdateLinks
            parameters[2] = Type.Missing; //ReadOnly
            parameters[3] = Type.Missing; //Format
            parameters[4] = password; //Password
            parameters[5] = Type.Missing; //WriteResPassword
            parameters[6] = Type.Missing; //IgnoreReadOnlyRecommended
            parameters[7] = Type.Missing; //Origin
            parameters[8] = Type.Missing; //Delimiter
            parameters[9] = Type.Missing; //Editable
            parameters[10] = Type.Missing; //Notify
            parameters[11] = Type.Missing; //Converter
            parameters[12] = Type.Missing; //AddToMru
            parameters[13] = Type.Missing; //Local
            parameters[14] = Type.Missing; //CorruptLoad
            return _comUtil.InvokeMember(books, "Open", BindingFlags.InvokeMethod, parameters);
        }
        /// <summary>Closeメソッド</summary>
        /// <param name="book">Workbook</param>
        public void Close(object book) {
            _comUtil.InvokeMember(book, "Close", BindingFlags.InvokeMethod);
        }

        /// <summary>Closeメソッド</summary>
        /// <param name="book">Workbook</param>
        public void Close(object book, bool saveChanges) {
            _comUtil.InvokeMember(book, "Close", BindingFlags.InvokeMethod, new object[1] { saveChanges });
        }

        /// <summary>Quitメソッド</summary>
        /// <param name="app">Application</param>
        public void Quit(object app) {
            _comUtil.InvokeMember(app, "Quit", BindingFlags.InvokeMethod);
        }
        /// <summary>Visibleプロパティ設定</summary>
        /// <param name="xlApp">Application.Visible</param>
        /// <param name="style">true/false</param>
        public void SetVisible(object xlApp, bool value) {
            _comUtil.InvokeMember(xlApp, "Visible", BindingFlags.SetProperty, new object[1]{value});
        }
        /// <summary>Workbooksプロパティ取得</summary>
        /// <param name="xlApp">Application.Workbooks</param>
        /// <returns>Workbooks</returns>
        public object GetWorkbooks(object xlApp) {
            return _comUtil.InvokeMember(xlApp, "Workbooks", BindingFlags.GetProperty);
        }
        /// <summary>
        /// 全シートオブジェクトを取得
        /// </summary>
        /// <param name="book"></param>
        /// <returns></returns>
        public object GetWorksheets(object book) {
            return _comUtil.InvokeMember(book, "Sheets", BindingFlags.GetProperty);
        }
        /// <summary>
        /// 全シートを選択
        /// </summary>
        /// <param name="sheets"></param>
        public void WorksheetsSelect(object sheets) {
            _comUtil.InvokeMember(sheets, "Select", BindingFlags.InvokeMethod);
        }
        /// <summary>Selectionプロパティ取得</summary>
        /// <param name="xlApp">Application</param>
        /// <returns>Range</returns>
        public object GetSelection(object xlApp) {
            return _comUtil.InvokeMember(xlApp, "Selection", BindingFlags.GetProperty);
        }
        /// <summary>PrintOutメソッド</summary>
        /// <param name="sheet">Worksheet</param>
        public void PrintOut(object sheet) {
            object[] parameters = new object[8];
            parameters[0] = Type.Missing;
            parameters[1] = Type.Missing;
            parameters[2] = Type.Missing;
            parameters[3] = Type.Missing;
            parameters[4] = Type.Missing;
            parameters[5] = Type.Missing;
            parameters[6] = Type.Missing;
            parameters[7] = Type.Missing;
            object window = _comUtil.InvokeMember(sheet, "PrintOut", BindingFlags.InvokeMethod, parameters);
        }
        /// <summary>PrintOutメソッド</summary>
        /// <param name="sheet">Worksheet</param>
        /// <param name="activePrinter">プリンタ名指定</param>
        public void PrintOut(object sheet, string activePrinter) {
            object[] parameters = new object[8];
            parameters[0] = Type.Missing;
            parameters[1] = Type.Missing;
            parameters[2] = Type.Missing;
            parameters[3] = Type.Missing;
            parameters[4] = activePrinter;
            parameters[5] = Type.Missing;
            parameters[6] = Type.Missing;
            parameters[7] = Type.Missing;
            object window = _comUtil.InvokeMember(sheet, "PrintOut", BindingFlags.InvokeMethod, parameters);
        }
        /// <summary>
        /// 選択しているシートを引数のプリンタで全て印刷
        /// </summary>
        /// <param name="app"></param>
        /// <param name="activePrinter">プリンタ名</param>
        public void SelectedSheetsPrintOut(object app, string activePrinter) {
            object window = _comUtil.InvokeMember(app, "ActiveWindow", BindingFlags.GetProperty);
            object sheets = _comUtil.InvokeMember(window, "SelectedSheets", BindingFlags.GetProperty);

            object[] parameters = new object[8];
            parameters[0] = Type.Missing;
            parameters[1] = Type.Missing;
            parameters[2] = Type.Missing;
            parameters[3] = Type.Missing;
            parameters[4] = activePrinter;
            parameters[5] = Type.Missing;
            parameters[6] = Type.Missing;
            parameters[7] = Type.Missing;
            _comUtil.InvokeMember(sheets, "PrintOut", BindingFlags.InvokeMethod, parameters);
        }
        /// <summary>
        /// 選択しているシートをデフォルトプリンタで全て印刷
        /// </summary>
        /// <param name="app"></param>
        public void SelectedSheetsPrintOut(object app) {
            object window = _comUtil.InvokeMember(app, "ActiveWindow", BindingFlags.GetProperty);
            object sheets = _comUtil.InvokeMember(window, "SelectedSheets", BindingFlags.GetProperty);

            object[] parameters = new object[8];
            parameters[0] = Type.Missing;
            parameters[1] = Type.Missing;
            parameters[2] = Type.Missing;
            parameters[3] = Type.Missing;
            parameters[4] = Type.Missing;
            parameters[5] = Type.Missing;
            parameters[6] = Type.Missing;
            parameters[7] = Type.Missing;
            _comUtil.InvokeMember(sheets, "PrintOut", BindingFlags.InvokeMethod, parameters);
        }
        /// <summary>Rows.Selectメソッド</summary>
        /// <param name="sheet">Worksheet</param>
        /// <param name="top">最上行</param>
        /// <param name="bottom">最下行</param>
        public void SelectRows(object sheet, int top, int bottom) {
            string param = top.ToString() + ":" + bottom.ToString();
            object rows = _comUtil.InvokeMember(sheet, "Rows", BindingFlags.GetProperty, new object[1]{param});
            _comUtil.InvokeMember(rows, "Select", BindingFlags.InvokeMethod);
        }
        /// <summary>Rows.Groupメソッド</summary>
        /// <param name="sheet">Worksheet</param>
        /// <param name="top">最上行</param>
        /// <param name="bottom">最下行</param>
        public void Group(object sheet, int top, int bottom) {
            string param = top.ToString() + ":" + bottom.ToString();
            object rows = _comUtil.InvokeMember(sheet, "Rows", BindingFlags.GetProperty, new object[1]{param});
            _comUtil.InvokeMember(rows, "Group", BindingFlags.InvokeMethod);
        }
        /// <summary>CutCopyModeプロパティ設定</summary>
        /// <param name="xlApp">Application</param>
        /// <param name="style"></param>
        public void SetCutCopyMode(object xlApp, bool value) {
            _comUtil.InvokeMember(xlApp, "CutCopyMode", BindingFlags.SetProperty, new object[1]{value});
        }
        /// <summary>Selection.Copyメソッド</summary>
        /// <param name="xlApp">Application</param>
        public void CopySelection(object xlApp) {
            object selection = GetSelection(xlApp);
            _comUtil.InvokeMember(selection, "Copy", BindingFlags.InvokeMethod);
        }
        /// <summary>Selection.Insertメソッド</summary>
        /// <param name="xlApp">Application</param>
        public void InsertSelection(object xlApp, XlDirection direction) {
            object selection = GetSelection(xlApp);
            _comUtil.InvokeMember(selection, "Insert", BindingFlags.InvokeMethod, new object[2]{(int)direction, Type.Missing});
        }
        /// <summary>選択シェイプ移動</summary>
        /// <param name="xlApp">Application</param>
        /// <param name="offsetX">X座標の移動距離</param>
        /// <param name="offsetY">Y座標の移動距離</param>
        public void MoveSelectionShape(object xlApp, double offsetX, double offsetY) {
            object shape = GetSelection(xlApp);
            object shapeRange = _comUtil.InvokeMember(shape, "ShapeRange", BindingFlags.GetProperty);
            double left = double.Parse(_comUtil.InvokeMember(shapeRange, "Left", BindingFlags.GetProperty).ToString());
            _comUtil.InvokeMember(shapeRange, "Left", BindingFlags.SetProperty, new object[1]{left + offsetX});
            double top = double.Parse(_comUtil.InvokeMember(shapeRange, "Top", BindingFlags.GetProperty).ToString());
            _comUtil.InvokeMember(shapeRange, "Top", BindingFlags.SetProperty, new object[1]{top + offsetY});
        }
        /// <summary>Sheetsプロパティ取得</summary>
        /// <param name="book">Workbook</param>
        /// <param name="sheetName">シート名</param>
        /// <returns>Worksheet</returns>
        public object GetSheets(object book, string sheetName) {
            return _comUtil.InvokeMember(book, "Sheets", BindingFlags.GetProperty, new object[1]{sheetName});
        }
        /// <summary>Sheet.Nameプロパティ取得 /// </summary>
        /// <param name="sheet">Worksheet</param>
        /// <returns>シート名</returns>
        public string GetSheetName(object sheet) {
            return _comUtil.InvokeMember(sheet, "Name", BindingFlags.GetProperty).ToString();
        }
        /// <summary>Sheet.Nameプロパティ設定</summary>
        /// <param name="sheet">Worksheet</param>
        /// <param name="name">シート名</param>
        public void SetSheetName(object sheet, string name) {
            _comUtil.InvokeMember(sheet, "Name", BindingFlags.SetProperty,  new object[1]{name});
        }
        /// <summary>Sheetsプロパティ取得</summary>
        /// <param name="book">Workbook</param>
        /// <param name="index">インデックス</param>
        /// <returns>Worksheet</returns>
        public object GetSheets(object book, int index) {
            return _comUtil.InvokeMember(book, "Sheets", BindingFlags.GetProperty, new object[1]{index});
        }
        /// <summary>Sheetsプロパティ取得</summary>
        /// <param name="book">Workbook</param>
        /// <returns>Worksheets</returns>
        public object GetSheets(object book) {
            return _comUtil.InvokeMember(book, "Sheets", BindingFlags.GetProperty);
        }
        /// <summary>Workbooks追加</summary>
        /// <param name="books">Workbooks</param>
        /// <returns>Workbook</returns>
        public object AddWorkbooks(object books) {
            return _comUtil.InvokeMember(books, "Add", BindingFlags.InvokeMethod);
        }
        /// <summary>Worksheetコピー</summary>
        /// <param name="orgSheet">コピー元Worksheet</param>
        /// <param name="destSheet">コピー先Worksheet</param>
        /// <param name="isBefore">挿入位置</param>
        public void CopySheet(object orgSheet, object destSheet, bool isBefore) {
            object[] parameters = new object[2];
            if (isBefore) {
                parameters[0] = destSheet;
                parameters[1] = Type.Missing;
            } else {
                parameters[0] = Type.Missing;
                parameters[1] = destSheet;
            }
            _comUtil.InvokeMember(destSheet, "Copy", BindingFlags.InvokeMethod, null, orgSheet, parameters);
        }
        /// <summary>Cell.Value設定</summary>
        /// <param name="sheet">Worksheet</param>
        /// <param name="rowIdx">行インデックス</param>
        /// <param name="colIdx">列インデックス</param>
        /// <param name="style">設定値</param>
        public void SetCellValue(object sheet, int rowIdx, int colIdx, string value) {
            object cell = GetCells(sheet, rowIdx, colIdx);
            _comUtil.InvokeMember(cell, "Value", BindingFlags.SetProperty,  new object[1]{value});
        }
        /// <summary>
        /// シート全体をコピー(sheet.Cells.Copy)
        /// </summary>
        /// <param name="sheet"></param>
        public void CopyCells(object sheet) {
            object cells = _comUtil.InvokeMember(sheet, "Cells", BindingFlags.GetProperty);
            _comUtil.InvokeMember(cells, "Copy", BindingFlags.InvokeMethod);
        }
        /// <summary>
        /// シート全体を選択して貼り付け
        /// </summary>
        /// <param name="book"></param>
        /// <param name="sheet"></param>
        public void PasteCells(object book, object sheet) {
            _comUtil.InvokeMember(sheet, "Activate", BindingFlags.InvokeMethod);
            object activeSheet = _comUtil.InvokeMember(book, "ActiveSheet", BindingFlags.GetProperty);
            object cells = _comUtil.InvokeMember(activeSheet, "Cells", BindingFlags.GetProperty);
            _comUtil.InvokeMember(cells, "Select", BindingFlags.InvokeMethod);
            _comUtil.InvokeMember(activeSheet, "Paste", BindingFlags.InvokeMethod);
        }
        /// <summary>Borders.LinStyle設定</summary>
        /// <param name="range">Range</param>
        /// <param name="style">罫線の種類</param>
        public void SetLineStyle(object range, XlLineStyle style) {
            object borders = _comUtil.InvokeMember(range, "Borders", BindingFlags.GetProperty);
            _comUtil.InvokeMember(borders, "LineStyle", BindingFlags.SetProperty, new object[1]{(int)style});
        }
        /// <summary>Pasteメソッド</summary>
        /// <param name="sheet">Worksheet</param>
        public void Paste(object sheet) {
            try { 
                _comUtil.InvokeMember(sheet, "Paste", BindingFlags.InvokeMethod);
            } catch (Exception) {
                throw;
            }
        }
        /// <summary>シート削除</summary>
        /// <param name="sheet">Worksheet</param>
        public void DeleteSheet(object sheet) {
            _comUtil.InvokeMember(sheet, "Delete", BindingFlags.InvokeMethod);
        }
        /// <summary>Sheets.Countプロパティ取得</summary>
        /// <param name="sheets">Worksheets</param>
        /// <returns>シート数</returns>
        public int GetSheetCount(object sheets) {
            return (int)_comUtil.InvokeMember(sheets, "Count", BindingFlags.GetProperty);
        }

        /// <summary>不要シートを削除(判定アドレスがブランクであれば削除)</summary>
        /// <param name="xlApp">Application</param>
        /// <param name="book">Workbook</param>
        /// <param name="judgeAddress">判定アドレス</param>
        public void DeleteUnnecessarySheet(object xlApp, object book, string judgeAddress) {
            int sheetCount = GetSheetCount(GetSheets(book));

            for (int i = 1; i <= sheetCount; i++) {
                object sheet = GetSheets(book, i);
                string judgeStr = GetCellValue(sheet, judgeAddress);
                if (StringUtil.NullToBlank(judgeStr) == "") {
                    //シート削除
                    SetDisplayAlerts(xlApp, false);
                    DeleteSheet(sheet);
                    sheetCount--;
                    SetDisplayAlerts(xlApp, true);
                }
            }
            //先頭シートを選択
            SheetActivate(GetSheets(book, 1));
        }
        /// <summary>DisplayAlertsプロパティ設定</summary>
        /// <param name="xlApp">Application</param>
        /// <param name="value">true/false</param>
        public void SetDisplayAlerts(object xlApp, bool value) {
            _comUtil.InvokeMember(xlApp, "DisplayAlerts", BindingFlags.SetProperty, new object[1]{value});
        }
        /// <summary>SheetActivateメソッド</summary>
        /// <param name="sheet">Worksheet</param>
        public void SheetActivate(object sheet) {
            _comUtil.InvokeMember(sheet, "Activate", BindingFlags.InvokeMethod);
        }
        /// <summary>Cell.Valueプロパティ取得</summary>
        /// <param name="sheet">Worksheet</param>
        /// <param name="address">アドレス</param>
        /// <returns>値</returns>
        public string GetCellValue(object sheet, string address) {
            string ret = "";
            object range = GetRange(sheet, address);
            ret = StringUtil.NullToBlank(_comUtil.InvokeMember(range, "Value", BindingFlags.GetProperty));

            return ret;
        }
        /// <summary>Cell.Valueプロパティ取得</summary>
        /// <param name="sheet">Worksheet</param>
        /// <param name="rowIdx">行インデックス</param>
        /// <param name="colIdx">列インデックス</param>
        /// <returns>値</returns>
        public string GetCellValue(object sheet, int rowIdx, int colIdx) {
            string ret = "";
            object cell = _comUtil.InvokeMember(sheet, "Cells", BindingFlags.GetProperty, new object[2] {rowIdx, colIdx});
            ret = StringUtil.NullToBlank(_comUtil.InvokeMember(cell, "Value", BindingFlags.GetProperty));

            return ret;
        }
        /// <summary>Cell.Valueプロパティ設定</summary>
        /// <param name="sheet">Worksheet</param>
        /// <param name="address">アドレス</param>
        /// <param name="value">値</param>
        public void SetCellValue(object sheet, string address, string value) {
            string ret = "";
            object range = GetRange(sheet, address);
            ret = (string)_comUtil.InvokeMember(range, "Value", BindingFlags.SetProperty, new object[1]{value});
        }
        /// <summary>Range.Valueプロパティ設定(高速に貼り付けられる)</summary>
        /// <param name="range">Range</param>
        /// <param name="array">設定値の２次元配列</param>
        public void SetRangeValue(object range, string[,] array) {
            _comUtil.InvokeMember(range, "Value", BindingFlags.SetProperty, new object[1]{array});
        }
        /// <summary>
        ///
        /// </summary>
        /// <param name="range"></param>
        /// <returns></returns>
        public string[,] GetRangeValue(object range) {
            return (string[,])_comUtil.InvokeMember(range, "Value", BindingFlags.GetProperty);
        }
        public object[,] GetRangeValue(object sheet, int topRow, int leftCol, int bottomRow, int rightCol) {
            object cell1 = GetCells(sheet, topRow, leftCol);
            object cell2 = GetCells(sheet, bottomRow, rightCol);
            object range = GetRange(sheet, GetAddress(cell1) + ":" + GetAddress(cell2));
            return (object[,])_comUtil.InvokeMember(range, "Value", BindingFlags.GetProperty);
        }
        /// <summary>
        /// Shapesプロパティ取得
        /// </summary>
        /// <param name="sheet"></param>
        /// <returns></returns>
        public object GetShapes(object sheet) {
            return _comUtil.InvokeMember(sheet, "Shapes", BindingFlags.GetProperty);
        }
        /// <summary>
        /// Shapesプロパティ取得
        /// </summary>
        /// <param name="sheet"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public object GetShapes(object sheet, int index) {
            return _comUtil.InvokeMember(sheet, "Shapes", BindingFlags.GetProperty, new object[1]{index});
        }
        /// <summary>
        /// AddPictureメソッド
        /// </summary>
        /// <param name="shapes"></param>
        /// <param name="imgPath"></param>
        /// <param name="range"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        public void AddPicture(object shapes, string imgPath, object range, float width, float height) {
            object[] parameters = new object[7];
            parameters[0] = imgPath;
            parameters[1] = (int)MsoTriState.msoFalse;
            parameters[2] = (int)MsoTriState.msoTrue;
            parameters[3] = GetRangeLeft(range);
            parameters[4] = RangeTop(range);
            parameters[5] = width;
            parameters[6] = height;
            _comUtil.InvokeMember(shapes, "AddPicture", BindingFlags.InvokeMethod, parameters);
        }
        /// <summary>
        /// Shapes.Rotationプロパティ設定
        /// </summary>
        /// <param name="shapes"></param>
        /// <param name="angle"></param>
        public void SetRotation(object shape, int angle) {
            _comUtil.InvokeMember(shape, "Rotation", BindingFlags.SetProperty, new object[1] { angle });
        }

        /// <summary>
        /// Shapesから引数で指定したShapeを取得
        /// </summary>
        /// <param name="shapes"></param>
        /// <param name="idx"></param>
        /// <returns></returns>
        public object GetShape(object shapes, int idx) {
            object[] parameters = new object[1];
            parameters[0] = idx;
            return _comUtil.InvokeMember(shapes, "Item", BindingFlags.InvokeMethod, parameters);
        }

        /// <summary>
        /// Shape.Widthプロパティ取得
        /// </summary>
        /// <param name="shape"></param>
        /// <returns></returns>
        public int GetWidth(object shape) {
            return (int)_comUtil.InvokeMember(shape, "Width", BindingFlags.GetProperty);
        }

        /// <summary>
        /// Shape.Heightプロパティ取得
        /// </summary>
        /// <param name="shape"></param>
        /// <returns></returns>
        public int GetHeight(object shape) {
            return Convert.ToInt32(_comUtil.InvokeMember(shape, "Height", BindingFlags.GetProperty));
        }

        /// <summary>
        /// Shape.IncrementLeftメソッド
        /// </summary>
        /// <param name="shape"></param>
        /// <param name="distance"></param>
        public void IncrementLeft(object shape, int distance) {
            object[] incrementParam = new object[1];
            incrementParam[0] = distance;
            _comUtil.InvokeMember(shape, "IncrementLeft", BindingFlags.InvokeMethod, incrementParam);
        }

        /// <summary>
        /// Shape.IncrementTopメソッド
        /// </summary>
        /// <param name="shape"></param>
        /// <param name="distance"></param>
        public void IncrementTop(object shape, int distance) {
            object[] incrementParam = new object[1];
            incrementParam[0] = distance;
            _comUtil.InvokeMember(shape, "IncrementTop", BindingFlags.InvokeMethod, incrementParam);
        }

        /// <summary>
        /// Shape.Cutメソッド
        /// </summary>
        /// <param name="shape"></param>
        public void Cut(object shape) {
            _comUtil.InvokeMember(shape, "Cut", BindingFlags.InvokeMethod);
        }

        /// <summary>
        /// Shape.ScaleHeightメソッド
        /// </summary>
        /// <param name="shape"></param>
        public void ScaleHeight(object shape) {
            object[] param = new object[3];
            param[0] = 1;
            param[1] = MsoTriState.msoTrue;
            param[2] = MsoScaleFrom.msoScaleFromTopLeft;
            _comUtil.InvokeMember(shape, "ScaleHeight", BindingFlags.InvokeMethod, param);
        }

        /// <summary>
        /// Shape.ScaleWidthメソッド
        /// </summary>
        /// <param name="shape"></param>
        public void ScaleWidth(object shape) {
            object[] param = new object[3];
            param[0] = 1;
            param[1] = MsoTriState.msoTrue;
            param[2] = MsoScaleFrom.msoScaleFromTopLeft;
            _comUtil.InvokeMember(shape, "ScaleWidth", BindingFlags.InvokeMethod, param);
        }

        /// <summary>Shape.Countプロパティ取得</summary>
        /// <param name="shapes">shape</param>
        /// <returns>シート数</returns>
        public int GetShapeCount(object shapes) {
            return (int)_comUtil.InvokeMember(shapes, "Count", BindingFlags.GetProperty);
        }

        /// <summary>Shape.Nameプロパティ取得 /// </summary>
        /// <param name="shape">shape</param>
        /// <returns>シート名</returns>
        public string GetShapeName(object shape) {
            return _comUtil.InvokeMember(shape, "Name", BindingFlags.GetProperty).ToString();
        }

        /// <summary>
        /// Rangeプロパティ取得
        /// </summary>
        /// <param name="sheet"></param>
        /// <param name="address"></param>
        /// <returns></returns>
        public object GetRange(object sheet, string address) {
            return _comUtil.InvokeMember(sheet, "Range", BindingFlags.GetProperty, new object[1] { address });
        }
        /// <summary>
        /// Rangeプロパティ取得
        /// </summary>
        /// <param name="sheet"></param>
        /// <param name="address"></param>
        /// <returns></returns>
        public object GetRange(object sheet, object cell1, object cell2) {
            return _comUtil.InvokeMember(sheet, "Range", BindingFlags.GetProperty, new object[2] { cell1, cell2 });
        }
        /// <summary>
        /// 指定セルからデータが続く範囲で一番下行のインデックスを取得
        /// (Ctrl+Downと同じ操作)
        /// </summary>
        /// <param name="sheet"></param>
        /// <param name="row"></param>
        /// <param name="col"></param>
        /// <returns></returns>
        public int GetEndRowIndex(object sheet, int row, int col) {
            int ret = 0;
            object cell = GetCells(sheet, row, col);
            object endCell =  _comUtil.InvokeMember(cell, "End", BindingFlags.GetProperty, new object[1] {(int)XlDirection.XlDown});
            ret =  (int)_comUtil.InvokeMember(endCell, "Row", BindingFlags.GetProperty);
            return ret;
        }
        /// <summary>
        /// 指定セルからデータが続く範囲で一番右列のインデックスを取得
        /// (Ctrl+Rightと同じ操作)
        /// </summary>
        /// <param name="sheet"></param>
        /// <param name="row"></param>
        /// <param name="col"></param>
        /// <returns></returns>
        public int GetEndColIndex(object sheet, int row, int col) {
            int ret = 0;
            object cell = GetCells(sheet, row, col);
            object endCell =  _comUtil.InvokeMember(cell, "End", BindingFlags.GetProperty, new object[1] {(int)XlDirection.XlToRight});
            ret =  (int)_comUtil.InvokeMember(endCell, "Column", BindingFlags.GetProperty);
            return ret;
        }
        /// <summary>
        /// セル背景色設定（ColorIndex）
        /// </summary>
        /// <param name="sheet"></param>
        /// <param name="cell1"></param>
        /// <param name="cell2"></param>
        /// <param name="c"></param>
        /// <returns></returns>
        public void SetInteriorColorIndex(object sheet, int r1, int c1, int r2, int c2, int colorIndex) {
            object cell1 = GetCells(sheet, r1, c1);
            object cell2 = GetCells(sheet, r2, c2);
            object range = GetRange(sheet, cell1, cell2);
            object interior =  _comUtil.InvokeMember(range, "Interior", BindingFlags.GetProperty);
            _comUtil.InvokeMember(interior, "ColorIndex", BindingFlags.SetProperty, new object[1] { colorIndex });
        }
        /// <summary>
        /// セル背景色設定（ColorIndex）
        /// </summary>
        /// <param name="sheet"></param>
        /// <param name="address"></param>
        /// <param name="colorIndex"></param>
        public void SetInteriorColorIndex(object sheet, string address, int colorIndex) {
            object range = GetRange(sheet, address);
            object interior =  _comUtil.InvokeMember(range, "Interior", BindingFlags.GetProperty);
            _comUtil.InvokeMember(interior, "ColorIndex", BindingFlags.SetProperty, new object[1] { colorIndex });
        }
        /// <summary>
        /// セル背景色設定（Color）
        /// </summary>
        /// <param name="sheet"></param>
        /// <param name="cell1"></param>
        /// <param name="cell2"></param>
        /// <param name="c"></param>
        /// <returns></returns>
        public void SetInteriorColor(object sheet, object range, Color color) {
            object interior = _comUtil.InvokeMember(range, "Interior", BindingFlags.GetProperty);
            _comUtil.InvokeMember(interior, "Color", BindingFlags.SetProperty, new object[1] { color });
        }
        /// <summary>
        /// Range.Leftプロパティ取得
        /// </summary>
        /// <param name="range"></param>
        /// <returns></returns>
        public float GetRangeLeft(object range) {
            return float.Parse(_comUtil.InvokeMember(range, "Left", BindingFlags.GetProperty).ToString());
        }
        /// <summary>
        /// Range.Rightプロパティ取得
        /// </summary>
        /// <param name="range"></param>
        /// <returns></returns>
        public float RangeRight(object range) {
            return float.Parse(_comUtil.InvokeMember(range, "Right", BindingFlags.GetProperty).ToString());
        }
        /// <summary>
        /// Range.Topプロパティ取得
        /// </summary>
        /// <param name="range"></param>
        /// <returns></returns>
        public float RangeTop(object range) {
            return float.Parse(_comUtil.InvokeMember(range, "Top", BindingFlags.GetProperty).ToString());
        }
        /// <summary>
        /// Range.ColumnWidthプロパティ取得
        /// </summary>
        /// <param name="range"></param>
        /// <returns></returns>
        public float RangeColumnWidth(object range) {
            return float.Parse(_comUtil.InvokeMember(range, "ColumnWidth", BindingFlags.GetProperty).ToString());
        }
        /// <summary>
        /// Range.RowHeightプロパティ取得
        /// </summary>
        /// <param name="range"></param>
        /// <returns></returns>
        public float RangeRowHeight(object range) {
            return float.Parse(_comUtil.InvokeMember(range, "RowHeight", BindingFlags.GetProperty).ToString());
        }
        /// <summary>
        /// 指定した範囲のセルを結合する
        /// </summary>
        /// <param name="range"></param>
        public void RangeMerge(object range) {
            _comUtil.InvokeMember(range, "MergeCells", BindingFlags.SetProperty, true);
        }
        /// <summary>
        /// Saveメソッド
        /// </summary>
        /// <param name="book"></param>
        public void Save(object book) {
            _comUtil.InvokeMember(book, "Save", BindingFlags.InvokeMethod);
        }
        /// <summary>
        /// SaveAsメソッド
        /// </summary>
        /// <param name="book"></param>
        /// <param name="path"></param>
        public void SaveAs(object book, string path) {
            object[] parameters = new object[12];
            parameters[0] = path;
            parameters[1] = Type.Missing;
            parameters[2] = Type.Missing;
            parameters[3] = Type.Missing;
            parameters[4] = Type.Missing;
            parameters[5] = Type.Missing;
            parameters[6] = Type.Missing;
            parameters[7] = Type.Missing;
            parameters[8] = Type.Missing;
            parameters[9] = Type.Missing;
            parameters[10] = Type.Missing;
            parameters[11] = Type.Missing;
            _comUtil.InvokeMember(book, "SaveAs", BindingFlags.InvokeMethod, parameters);
        }
        /// <summary>
        /// SaveAsメソッド
        /// </summary>
        /// <param name="book"></param>
        /// <param name="path"></param>
        /// <param name="fileFormat"></param>
        public void SaveAs(object book, string path, XlFileFormat fileFormat) {
            object[] parameters = new object[12];
            parameters[0] = path;
            parameters[1] = (int)fileFormat;
            parameters[2] = Type.Missing;
            parameters[3] = Type.Missing;
            parameters[4] = Type.Missing;
            parameters[5] = Type.Missing;
            parameters[6] = Type.Missing;
            parameters[7] = Type.Missing;
            parameters[8] = Type.Missing;
            parameters[9] = Type.Missing;
            parameters[10] = Type.Missing;
            parameters[11] = Type.Missing;
            _comUtil.InvokeMember(book, "SaveAs", BindingFlags.InvokeMethod, parameters);
        }
        /// <summary>
        /// Range.Widthプロパティ取得
        /// </summary>
        /// <param name="range"></param>
        /// <returns></returns>
        public float GetRangeWidth(object range) {
            return float.Parse(_comUtil.InvokeMember(range, "Width", BindingFlags.GetProperty).ToString());
        }
        /// <summary>
        /// Range.Heightプロパティ取得
        /// </summary>
        /// <param name="range"></param>
        /// <returns></returns>
        public float RangeHeight(object range) {
            return float.Parse(_comUtil.InvokeMember(range, "Height", BindingFlags.GetProperty).ToString());
        }
        /// <summary>
        /// Versionプロパティ取得
        /// </summary>
        /// <param name="app"></param>
        /// <returns></returns>
        public decimal GetVersion(object app) {
            return decimal.Parse(_comUtil.InvokeMember(app, "Version", BindingFlags.GetProperty).ToString());
        }
        /// <summary>
        /// DefaultSaveFormatプロパティ取得
        /// </summary>
        /// <param name="app"></param>
        /// <returns></returns>
        public int GetDefaultSaveFormat(object app) {
            return int.Parse(_comUtil.InvokeMember(app, "DefaultSaveFormat", BindingFlags.GetProperty).ToString());
        }
        /// <summary>
        /// DefaultSaveFormatプロパティ設定
        /// </summary>
        /// <param name="app"></param>
        /// <param name="format"></param>
        public void SetDefaultSaveFormat(object app, int format) {
            _comUtil.InvokeMember(app, "DefaultSaveFormat", BindingFlags.SetProperty, new object[1] { format });
        }
        /// <summary>
        /// ScreenUpdatingプロパティ設定
        /// </summary>
        /// <param name="app"></param>
        /// <param name="value"></param>
        public void SetScreenUpdating(object app, bool value) {
            _comUtil.InvokeMember(app, "ScreenUpdating", BindingFlags.SetProperty, new object[1] { value });
        }
        /// <summary>
        /// Cellsプロパティを取得
        /// </summary>
        /// <param name="sheet"></param>
        /// <param name="rowIdx"></param>
        /// <param name="colIdx"></param>
        /// <returns></returns>
        public object GetCells(object sheet, int rowIdx, int colIdx) {
            return _comUtil.InvokeMember(sheet, "Cells", BindingFlags.GetProperty, new object[2] { rowIdx, colIdx });
        }
        /// <summary>
        /// Addressプロパティを取得
        /// </summary>
        /// <param name="range"></param>
        /// <returns></returns>
        public string GetAddress(object range) {
            return (string)_comUtil.InvokeMember(range, "Address", BindingFlags.GetProperty);
        }
        /// <summary>
        /// AutoFilterメソッド
        /// </summary>
        /// <param name="sheet"></param>
        /// <param name="address"></param>
        public void AutoFilter(object sheet, string address) {
            object range = GetRange(sheet, address);
            object[] parameters = new object[5];
            parameters[0] = (int)1;
            parameters[1] = Type.Missing;
            parameters[2] = Type.Missing;
            parameters[3] = Type.Missing;
            parameters[4] = Type.Missing;
            _comUtil.InvokeMember(range, "AutoFilter", BindingFlags.InvokeMethod, parameters);
        }
        /// <summary>
        /// Cell.Selectメソッド
        /// </summary>
        /// <param name="sheet"></param>
        /// <param name="rowIdx"></param>
        /// <param name="colIdx"></param>
        public void CellSelect(object sheet, int rowIdx, int colIdx) {
            object cell = GetCells(sheet, rowIdx, colIdx);
            _comUtil.InvokeMember(cell, "Select", BindingFlags.InvokeMethod);
        }
        /// <summary>
        /// Cell.Activateメソッド
        /// </summary>
        /// <param name="sheet"></param>
        /// <param name="rowIdx"></param>
        /// <param name="colIdx"></param>
        public void CellActivate(object sheet, int rowIdx, int colIdx) {
            object cell = GetCells(sheet, rowIdx, colIdx);
            _comUtil.InvokeMember(cell, "Activate", BindingFlags.InvokeMethod);
        }
        /// <summary>
        /// Range.Selectメソッド
        /// </summary>
        /// <param name="range"></param>
        public void RangeSelect(object range) {
            _comUtil.InvokeMember(range, "Select", BindingFlags.InvokeMethod);
        }
        /// <summary>
        /// FreezePanesプロパティ設定
        /// </summary>
        /// <param name="app"></param>
        /// <param name="sheet"></param>
        /// <param name="address"></param>
        /// <param name="value"></param>
        public void SetFreezePanes(object app, object sheet, string address, bool value) {
            object range = GetRange(sheet, address);
            RangeSelect(range);
            object window = _comUtil.InvokeMember(app, "ActiveWindow", BindingFlags.GetProperty);
            _comUtil.InvokeMember(window, "FreezePanes", BindingFlags.SetProperty, new object[1] { value });
        }

        /// <summary>
        /// FreezePanesプロパティ設定
        /// </summary>
        /// <param name="app"></param>
        /// <param name="sheet"></param>
        /// <param name="value"></param>
        /// <param name="grid"></param>
        public void SetFreezePanes(object app, object sheet, bool value, int rowIdx, int colIdx) {
            object cell = GetCells(sheet, rowIdx, colIdx);
            object range = GetRange(sheet, GetAddress(cell));
            RangeSelect(range);
            object window = _comUtil.InvokeMember(app, "ActiveWindow", BindingFlags.GetProperty);
            _comUtil.InvokeMember(window, "FreezePanes", BindingFlags.SetProperty, new object[1] { value });
        }
        
        /// <summary>
        /// ColumnAutoFitメソッド
        /// </summary>
        /// <param name="app"></param>
        /// <param name="sheet"></param>
        /// <param name="address"></param>
        public void ColmunAutoFit(object app, object sheet, string address) {
            object range = GetRange(sheet, address);
            object columns = _comUtil.InvokeMember(app,"Columns", BindingFlags.GetProperty);
            _comUtil.InvokeMember(range, "AutoFit", BindingFlags.InvokeMethod, null, columns, null);
        }

        /// <summary>
        /// C1FlexGrid上に表示されているデータをExcelに出力
        /// </summary>
        /// <param name="grid"></param>
        /// <param name="bw"></param>
        /// <param name="mlu"></param>
        public void ExtractToExcel(C1FlexGrid grid, BackgroundWorker bw, MultiLangUtil mlu, bool isAddedColor = false, int rowHeaderIdx = 1) {
            object app = null;
            object books = null;
            object book = null;
            object sheet = null;

            //0件は抽出しない。
            if (grid.Rows.Count == 0) {
                MessageBox.Show(mlu.GetMsg(CommonConsts.MSG_EXTRACT_ZERO), mlu.GetMsg(CommonConsts.TITLE_WARN),
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            //表示列数の取得
            int visibleColCount = 0;
            foreach (Column col in grid.Cols) {
                if (col.Visible) {
                    visibleColCount++;
                }
            }

            string[,] array = new string[RowBufSize, visibleColCount - 1];
            object[,] colorArray = new object[RowBufSize, visibleColCount - 1];
            HashSet<Color> colorHash = new HashSet<Color>();
            int defaultSaveFormat = 0;

            try {
                app = _comUtil.CreateObject("Excel.Application");

                defaultSaveFormat = GetDefaultSaveFormat(app);
                //256列対応
                int colPos = 0;
                decimal ver = GetVersion(app);
                if (ver < 12 && array.GetLength(1) > 256) {
                    MessageBox.Show(mlu.GetMsg(CommonConsts.MSG_EXCEL2003_DATA_TRUNCATE),
                        mlu.GetMsg(CommonConsts.TITLE_WARN), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    colPos = 256;
                } else {
                    SetDefaultSaveFormat(app, 51); //Excel.XlFileFormat.xlOpenXMLWorkbook
                    colPos = array.GetLength(1);
                }

                books = GetWorkbooks(app);
                book = AddWorkbooks(books);
                sheet = GetSheets(book, 1);

                SetScreenUpdating(app, false);

                int rowIdx = 0;
                int colIdx = 0;
                //出力
                int startRowIdx = 1;
                foreach (Row r in grid.Rows) {
                    colIdx = 0;
                    foreach (Column c in grid.Cols) {
                        if (c.Visible == true && c.Index > 0) {
                            string val = "";
                            if (r.Index > 0 && c.DataMap != null) {
                                Dictionary<string, string> dic = (Dictionary<string, string>)c.DataMap;
                                if (dic.ContainsKey(StringUtil.NullToBlank(grid[r.Index, c.Index]))) {
                                    val = dic[StringUtil.NullToBlank(grid[r.Index, c.Index])].ToString();
                                } else {
                                    val = StringUtil.NullToBlank(grid[r.Index, c.Index]);
                                }
                            } else {
                                val = StringUtil.NullToBlank(grid[r.Index, c.Index]);
                            }
                            array[rowIdx % RowBufSize, colIdx] = val;

                            CellRange cr = grid.GetCellRange(r.Index, c.Index);
                            if (cr.Style != null 
                                && cr.Style.BackColor != SystemColors.Control 
                                && cr.Style.BackColor != grid.Styles.Normal.BackColor
                                && cr.Style.BackColor != ColorTranslator.FromHtml("#ffffff")
                                ) {

                                colorArray[rowIdx % RowBufSize, colIdx] = cr.Style.BackColor;
                                colorHash.Add(cr.Style.BackColor);
                            } else if(rowIdx < grid.Rows.Fixed) {
                                //ヘッダー部のカラー設定
                                colorArray[rowIdx % RowBufSize, colIdx] = ColorTranslator.FromHtml("#f0f8ff");
                                colorHash.Add(ColorTranslator.FromHtml("#f0f8ff"));
                            }

                            colIdx++;
                        }
                    }
                    rowIdx++;

                    //キャンセルの割り込み
                    if (bw.CancellationPending) {
                        return;
                    }

                    //バッファに溜まったデータをExcelに書き込み
                    if ((rowIdx % RowBufSize) == 0) {
                        CopyArrayToExcel(array, startRowIdx, rowIdx, colPos, sheet, grid);
                        if (isAddedColor) {
                            CopyBackColorToExcel(colorArray, colorHash, startRowIdx, rowIdx, colPos, sheet);
                        }
                        Array.Clear(array, 0, array.Length);
                        Array.Clear(colorArray, 0, colorArray.Length);
                        colorHash = new HashSet<Color>();
                        startRowIdx += RowBufSize;
                    }

                    //プログレスバー更新
                    int p = (int)(((double)rowIdx / (double)grid.Rows.Count) * 100);
                    bw.ReportProgress(p, rowIdx.ToString() + "/" + (grid.Rows.Count + 1).ToString() + " " + mlu.GetMsg(CommonConsts.ACT_EXTRACT));
                }

                CopyArrayToExcel(array, startRowIdx, rowIdx, colPos, sheet, grid);
                if (isAddedColor) {
                    bw.ReportProgress(99, mlu.GetMsg(CommonConsts.MSG_COLORING_BACKCOLOR));
                    CopyBackColorToExcel(colorArray, colorHash, startRowIdx, rowIdx, colPos, sheet);
                }

                //フィルタ設定
                AutoFilter(sheet, rowHeaderIdx.ToString() + ":" + rowHeaderIdx.ToString());
                //ヘッダー固定
                SetFreezePanes(app, sheet, true, grid.Rows.Fixed + 1, 1);
                // ColumnAutoFit設定
                ColmunAutoFit(app, sheet, rowHeaderIdx.ToString() + ":" + rowHeaderIdx.ToString());

                // 罫線設定（グリッド印字範囲に実線格子）
                SetLineStyle(GetRange(sheet,
                    GetCells(sheet, startRowIdx, 1),
                    GetCells(sheet, rowIdx, colIdx)), ExcelUtil.XlLineStyle.XlContinuous);

            } finally {
                SetDefaultSaveFormat(app, defaultSaveFormat);
                SetVisible(app, true);
                SetScreenUpdating(app, true);

                _comUtil.MReleaseComObject(sheet);
                _comUtil.MReleaseComObject(book);
                _comUtil.MReleaseComObject(books);
                _comUtil.MReleaseComObject(app);
                GC.Collect();
            }
        }
        /// <summary>
        /// シート名禁則文字を変換
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string ReplaceSheetNameNGWord(string value) {
            value = value.Replace(":", "_");
            value = value.Replace("\\", "_");
            value = value.Replace("?", "_");
            value = value.Replace("[", "_");
            value = value.Replace("]", "_");
            value = value.Replace("/", "_");
            value = value.Replace("*", "_");

            return value;
        }
        /// <summary>
        /// 指定したブックの図形スケールを縦横100%に設定
        /// NOTE: EPPlusで図形スケールがずれてしまうため
        /// </summary>
        /// <param name="fileInfo"></param>
        /// <param name="doSave"></param>
        /// <param name="sheetNames"></param>
        /// <param name="shapeNames"></param>
        /// <returns></returns>
        public List<string> AdjustAutoShapes(AdjustExcelAutoShapeModel adjustExcelAutoShapeModel) {

            if (adjustExcelAutoShapeModel == null) { return null; }

            List<AdjustExcelAutoShapeModel> list = new List<AdjustExcelAutoShapeModel>();
            list.Add(adjustExcelAutoShapeModel);
            return AdjustAutoShapes(list);
        }
 
        /// <summary>
        /// 指定したブックの図形スケールを縦横100%に設定
        /// NOTE: EPPlusで図形スケールがずれてしまうため
        /// </summary>
        /// <param name="fileInfo"></param>
        /// <param name="doSave"></param>
        /// <param name="sheetNames"></param>
        /// <param name="shapeNames"></param>
        /// <returns></returns>
        public List<string> AdjustAutoShapes(List<AdjustExcelAutoShapeModel> adjustExcelAutoShapeModelList) {

            if (adjustExcelAutoShapeModelList == null) { return null; }

            List<string> errMsgKeyList = new List<string>();

            COMUtil comUtil = new COMUtil();

            object app = null;
            object books = null;
            object book = null;

            Cursor.Current = Cursors.WaitCursor;

            try {

                app = comUtil.CreateObject("Excel.Application");

                SetVisible(app, false);
                SetDisplayAlerts(app, false);
                SetScreenUpdating(app, false);

                // Books
                books = GetWorkbooks(app);

                foreach (AdjustExcelAutoShapeModel model in adjustExcelAutoShapeModelList) {
                    string bookFullPath = model.FileFullPath;
                    if (string.IsNullOrEmpty(bookFullPath)) { continue; }

                    FileInfo fi = new FileInfo(bookFullPath);
                    if (FileUtil.IsFileLocked(fi.FullName)) {
                        errMsgKeyList.Add(MESConsts.MSG_ALREADY_OPEN + ',' + bookFullPath);
                    } else {
                        book = Open(books, fi.FullName);
                        AdjustAutoShapes(comUtil, book, model.DoSave, model.SheetNameList, model.ShapeNameList);
                    }
                }

            } catch (Exception e) {

                throw e;

            } finally {

                SetDisplayAlerts(app, true);

                Quit(app);


                comUtil.MReleaseComObject(books);
                comUtil.MReleaseComObject(app);
                GC.Collect();

                Cursor.Current = Cursors.Default;
            }

            return errMsgKeyList;
        }

        #endregion PublicMethods

        #region PrivateMethods
        /// <summary>
        /// 指定したブックの図形スケールを縦横100%に設定
        /// NOTE: EPPlusで図形スケールがずれてしまうため
        /// </summary>
        /// <param name="fileInfo"></param>
        /// <param name="doSave"></param>
        /// <param name="sheetNames"></param>
        /// <param name="shapeNames"></param>
        /// <returns></returns>
        private string AdjustAutoShapes(COMUtil comUtil, object book, bool doSave, List<string> sheetNames, List<string> shapeNames) {
            string errMsgKey = string.Empty;


            object sheets = null;
            object sheet = null;

            try {

                // Sheets
                sheets = GetWorksheets(book);
                int sheetNum = GetSheetCount(sheets);
                for (int i = 1; i <= sheetNum; i++) {

                    sheet = GetSheets(book, i);

                    // sheet指定ありの場合
                    if (sheetNames != null && sheetNames.Count > 0) {
                        if (!sheetNames.Contains(GetSheetName(sheet))) {
                            continue;
                        }
                    }

                    object shapes = GetShapes(sheet);
                    int shapeNum = GetShapeCount(shapes);

                    if (shapeNum > 0) {

                        // Shapes
                        for (int j = 1; j <= shapeNum; j++) {
                            object shape = GetShape(shapes, j);

                            if (shapeNames != null && shapeNames.Count > 0) {
                                // 指定された図形を調整

                                if (shapeNames.Contains(GetShapeName(shape))) {
                                    ScaleHeight(shape);
                                    ScaleWidth(shape);

                                    //object shapeRange = _comUtil.InvokeMember(shape, "ShapeRange", BindingFlags.GetProperty);
                                    //_comUtil.InvokeMember(shapeRange, "Width", BindingFlags.SetProperty, new object[1] { width });
                                    //_comUtil.InvokeMember(shapeRange, "Height", BindingFlags.SetProperty, new object[1] { height });
                                }

                            } else {
                                // すべて調整
                                ScaleHeight(shape);
                                ScaleWidth(shape);
                            }

                        }
                    }

                }

                // ブックを閉じる
                Close(book, doSave);

            } catch (Exception e) {

                throw e;

            } finally {

                comUtil.MReleaseComObject(sheet);
                comUtil.MReleaseComObject(sheets);
                comUtil.MReleaseComObject(book);

            }

            return errMsgKey;
        }

        /// <summary>
        /// 文字列配列の値をExcelにコピー
        /// </summary>
        /// <param name="array"></param>
        /// <param name="startRowIdx"></param>
        /// <param name="rowIdx"></param>
        /// <param name="colPos"></param>
        /// <param name="sheet"></param>
        private void CopyArrayToExcel(string[,] array, int startRowIdx, int rowIdx, int colPos, object sheet, C1FlexGrid grid) {
            object cell1 = null;
            object cell2 = null;
            object range = null;

            object cellForMerge = null;
            object cellForMerge2 = null;
            object rangeForMerge = null;
            try {
                cell1 = GetCells(sheet, startRowIdx, 1);
                cell2 = GetCells(sheet, rowIdx, colPos);
                range = GetRange(sheet, GetAddress(cell1) + ":" + GetAddress(cell2));

                List<CellRange> visibleMergedRanges = new List<CellRange>();
                int[] invisibleCounters = new int[grid.Cols.Count + 1];
                int invisibleColCounter = 0;
                int invisibleCounter = 0;

                for (int i = 1; i < grid.Cols.Count; i++) {
                    invisibleCounters[i] = 0;
                }
                for (int i = 1; i < grid.Cols.Count; i++) {

                    if (!grid.Cols[i].IsVisible) {
                        invisibleColCounter++;
                    }
                    foreach (CellRange cr in grid.MergedRanges) {
                        if (grid.Cols[i].IsVisible && grid.Cols[i].Index == cr.c1) {
                            visibleMergedRanges.Add(cr);
                            invisibleCounters[cr.c1] = invisibleCounter + invisibleColCounter;
                        } else if (!grid.Cols[i].IsVisible && grid.Cols[i].Index == cr.c1) {
                            invisibleCounter++;
                        }
                    }
                }

                foreach (CellRange item in visibleMergedRanges) {
                    cellForMerge = GetCells(sheet, item.r1 + 1, item.c1 - invisibleCounters[item.c1]);
                    cellForMerge2 = GetCells(sheet, item.r2 + 1, item.c2 - invisibleCounters[item.c1]);
                    rangeForMerge = GetRange(sheet, GetAddress(cellForMerge) + ":" + GetAddress(cellForMerge2));
                    RangeMerge(rangeForMerge);
                }
                SetRangeValue(range, array);

            } finally {
            }
        }
        /// <summary>
        /// セルの背景色をExcelにコピー
        /// </summary>
        /// <param name="array"></param>
        /// <param name="startRowIdx"></param>
        /// <param name="rowIdx"></param>
        /// <param name="colPos"></param>
        /// <param name="sheet"></param>
        private void CopyBackColorToExcel(object[,] colorArray, HashSet<Color> colorHash, int startRowIdx, int rowIdx, int colPos, object sheet) {
            object range = null;

            try {
                foreach (Color color in colorHash) {
                    StringBuilder sb = new StringBuilder();
                    for (int rIdx = startRowIdx; rIdx <= rowIdx; rIdx++) {
                        for (int cIdx = 1; cIdx <= colPos; cIdx++) {
                            if (colorArray[rIdx - startRowIdx, cIdx - 1] == null
                                || (Color)colorArray[rIdx - startRowIdx, cIdx - 1] != color) {
                                continue;
                            }
                            object cell = GetCells(sheet, rIdx, cIdx);
                            string addr = GetAddress(cell);
                            if (sb.Length + addr.Length <= 255) {
                                sb.AppendFormat("{0},", addr);
                            } else {
                                range = GetRange(sheet, sb.Remove(sb.Length - 1, 1).ToString());
                                SetInteriorColor(sheet, range, color);
                                sb.Clear();
                                sb.AppendFormat("{0},", addr);
                            }
                        }
                    }
                    range = GetRange(sheet, sb.Remove(sb.Length - 1, 1).ToString());
                    SetInteriorColor(sheet, range, color);
                }
            } finally {
            }
        }
        #endregion PrivateMethods
    }
}