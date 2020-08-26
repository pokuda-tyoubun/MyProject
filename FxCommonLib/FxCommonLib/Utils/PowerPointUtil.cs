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
    /// PowerPointユーティリティ
    /// https://docs.microsoft.com/en-us/office/vba/api/overview/powerpoint
    /// </summary>
    public class PowerPointUtil {

        #region Constants
        /// <summary>PowerPoint保存フォーマット</summary>
        public enum PpFileFormat : int {
            PpSaveAsDefault = 11, //.ppt, .pptx
            PpSaveAsJPG = 17, //.jpg
            PpSaveAsPDF = 32, //.pdf
            PpSaveAsXPS = 33 //.xps
        }

        public enum MsoTriState : int {
            msoTrue = -1,
            msoFalse = 0
        }
        #endregion Constants

        #region MemberVariables
        /// <summary>COM操作ユーティリティ</summary>
        private COMUtil _comUtil = new COMUtil();
        #endregion MemberVariables

        #region PublicMethods
        /// <summary>
        /// PowerPointファイルを開く
        /// </summary>
        /// <param name="presentations"></param>
        /// <param name="pptFilePath"></param>
        /// <returns></returns>
        public object Open(object presentations, string pptFilePath) {
            return Open(presentations, pptFilePath, readOnly:false, withWindow:Type.Missing);
        }
        public object Open(object presentations, string pptFilePath, MsoTriState withWindow) {
            return Open(presentations, pptFilePath, readOnly:MsoTriState.msoFalse, withWindow:withWindow);
        }
        /// <summary>
        /// PowerPointファイルを開く
        /// </summary>
        /// <param name="presentations"></param>
        /// <param name="pptFilePath"></param>
        /// <param name="readOnly"></param>
        /// <returns></returns>
        private object Open(object presentations, string pptFilePath, object readOnly, object withWindow) {
            object[] parameters = new object[4];
            parameters[0] = pptFilePath; //FileName
            parameters[1] = readOnly; //ReadOnly
            parameters[2] = Type.Missing; //Untitled
            parameters[3] = withWindow; //WithWindow
            return _comUtil.InvokeMember(presentations, "Open", BindingFlags.InvokeMethod, parameters);
        }
        public object Open2007(object presentations, string pptFilePath) {
            return Open2007(presentations, pptFilePath, readOnly:false, withWindow:Type.Missing);
        }
        public object Open2007(object presentations, string pptFilePath, MsoTriState withWindow) {
            return Open2007(presentations, pptFilePath, readOnly:MsoTriState.msoFalse, withWindow:withWindow);
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
        public void SetVisible(object app, MsoTriState value) {
            _comUtil.InvokeMember(app, "Visible", BindingFlags.SetProperty, new object[1]{value});
        }
        public void SetDisplayAlerts(object app, MsoTriState value) {
            _comUtil.InvokeMember(app, "DisplayAlerts", BindingFlags.SetProperty, new object[1]{value});
        }
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
        public void SaveAs(object presentation, string path, PpFileFormat format) {
            object[] parameters = new object[3];
            parameters[0] = path;
            parameters[1] = format;
            parameters[2] = Type.Missing;
            _comUtil.InvokeMember(presentation, "SaveAs", BindingFlags.InvokeMethod, parameters);
        }

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