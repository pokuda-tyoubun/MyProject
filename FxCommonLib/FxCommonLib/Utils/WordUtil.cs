using C1.Win.C1FlexGrid;
using FxCommonLib.Consts;
using FxCommonLib.Consts.MES;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Reflection;
using System.Text;
using System.Windows.Forms;

namespace FxCommonLib.Utils {
    /// <summary>
    /// Wordユーティリティ
    /// https://docs.microsoft.com/ja-jp/dotnet/api/microsoft.office.interop.word.wdsaveformat?redirectedfrom=MSDN&view=word-pia
    /// </summary>
    public class WordUtil {
        //HACK MsoTriStateを親クラスへ

        #region Constants
        /// <summary>Word保存フォーマット</summary>
        public enum WdSaveFormat : int {
            WdFormatDocument = 0, //.doc, .docx
            WdFormatWebArchive  = 17, //.mhtml
            WdFormatPDF  = 17, //.pdf
            WdFormatXPS  = 18 //.xps
        }
        #endregion Constants

        #region MemberVariables
        /// <summary>COM操作ユーティリティ</summary>
        private COMUtil _comUtil = new COMUtil();
        #endregion MemberVariables

        #region PublicMethods
        /// <summary>
        /// Wordファイルを開く
        /// </summary>
        /// <param name="presentations"></param>
        /// <param name="pptFilePath"></param>
        /// <returns></returns>
        public object Open(object documents, string filePath) {
            return Open(documents, filePath, readOnly:false, withWindow:Type.Missing);
        }
        //public object Open(object presentations, string pptFilePath, MsoTriState withWindow) {
        //    return Open(presentations, pptFilePath, readOnly:MsoTriState.msoFalse, withWindow:withWindow);
        //}

        /// <summary>
        /// Wordファイルを開く
        /// </summary>
        /// <param name="presentations"></param>
        /// <param name="pptFilePath"></param>
        /// <param name="readOnly"></param>
        /// <returns></returns>
        private object Open(object documents, string filePath, object readOnly, object withWindow) {
            object[] parameters = new object[16];
            parameters[0] = filePath; //FileName
            parameters[1] = readOnly; //ReadOnly
            parameters[2] = Type.Missing; //Untitled
            parameters[3] = withWindow; //WithWindow
            parameters[3] = withWindow; //WithWindow
            parameters[3] = withWindow; //WithWindow
            parameters[3] = withWindow; //WithWindow
            parameters[3] = withWindow; //WithWindow
            parameters[3] = withWindow; //WithWindow
            parameters[3] = withWindow; //WithWindow
            parameters[3] = withWindow; //WithWindow
            parameters[3] = withWindow; //WithWindow
            parameters[3] = withWindow; //WithWindow
            parameters[3] = withWindow; //WithWindow
            return _comUtil.InvokeMember(documents, "Open", BindingFlags.InvokeMethod, parameters);
        }
        public object Open2007(object presentations, string pptFilePath) {
            return Open2007(presentations, pptFilePath, readOnly:false, withWindow:Type.Missing);
        }
        private object Open2007(object presentations, string pptFilePath, object readOnly, object withWindow) {
            object[] parameters = new object[5];
            parameters[0] = pptFilePath; //FileName
            parameters[1] = readOnly; //ReadOnly
            parameters[2] = Type.Missing; //Untitled
            parameters[3] = withWindow; //WithWindow
            parameters[4] = Type.Missing; //OpenAndRepair
            return _comUtil.InvokeMember(presentations, "Open2007", BindingFlags.InvokeMethod, parameters);
        }
        public object GetWindows(object app) {
            return _comUtil.InvokeMember(app, "Windows", BindingFlags.GetProperty);
        }
        /// <summary>
        /// app.Visibleプロパティ
        /// </summary>
        /// <param name="xlApp"></param>
        /// <param name="value"></param>
        /// <summary>
        /// app.Visibleプロパティ
        /// </summary>
        /// <param name="xlApp"></param>
        /// <param name="value"></param>
        public void GetVisible(object app, bool value) {
            _comUtil.InvokeMember(app, "Visible", BindingFlags.GetProperty);
        }
        /// <summary>
        /// app.Presentationsプロパティ
        /// </summary>
        /// <param name="app"></param>
        /// <returns></returns>
        public object GetPresentations(object app) {
            return _comUtil.InvokeMember(app, "Presentations", BindingFlags.GetProperty);
        }
        /// <summary>
        /// Presentation.Slidesプロパティ
        /// </summary>
        /// <param name="presentation"></param>
        /// <returns></returns>
        public object GetSlides(object presentation) {
            return _comUtil.InvokeMember(presentation, "Slides", BindingFlags.GetProperty);
        }

        /// <summary>
        /// Presentaion.SaveAsメソッド
        /// </summary>
        /// <param name="presentation"></param>
        /// <param name="path"></param>
        /// <param name="format"></param>

        /// <summary>
        /// Presentations.Slides.Countプロパティ
        /// </summary>
        /// <param name="slides"></param>
        /// <returns></returns>
        public int GetSlidesCount(object slides) {
            return (int)_comUtil.InvokeMember(slides, "Count", BindingFlags.GetProperty);
        }
        /// <summary>Closeメソッド</summary>
        /// <param name="book">Workbook</param>
        public void Close(object presentation) {
            _comUtil.InvokeMember(presentation, "Close", BindingFlags.InvokeMethod);
        }

        /// <summary>Quitメソッド</summary>
        /// <param name="app">Application</param>
        public void Quit(object app) {
            _comUtil.InvokeMember(app, "Quit", BindingFlags.InvokeMethod);
        }
        #endregion PublicMethods
    }
}