
using System.Collections.Generic;
using System.Linq;

namespace FxCommonLib.Models {
    /// <summary>
    /// Excelオートシェイプの大きさ変更用
    /// NOTE:ExcelUtil#AdjustAutoShapesの引数として利用する
    /// </summary>
    public class AdjustExcelAutoShapeModel {

        #region Properties

        /// <summary>Excelファイルフルパス</summary>
        protected string _fileFullPath = string.Empty;
        /// <summary>Excelファイルフルパス</summary>
        public string FileFullPath {
            set { _fileFullPath = value; }
            get { return _fileFullPath; }
        }

        /// <summary>オートシェイプの大きさを調整するExcelシート名のリスト</summary>
        protected List<string> _sheetNameList = new List<string>();
        /// <summary>Excelファイルフルパス</summary>
        public List<string> SheetNameList {
            set { _sheetNameList = value; }
            get { return _sheetNameList; }
        }

        /// <summary>大きさを調整するオートシェイプ名のリスト</summary>
        protected List<string> _shapeNameList = new List<string>();
        /// <summary>大きさを調整するオートシェイプ名のリスト</summary>
        public List<string> ShapeNameList {
            set { _shapeNameList = value; }
            get { return _shapeNameList; }
        }

        /// <summary>ファイルを保存するか</summary>
        protected bool _doSave = true;
        /// <summary>ファイルを保存するか</summary>
        public bool DoSave {
            set { _doSave = value; }
            get { return _doSave; }
        }

        #endregion Properties

        #region PublicMethods

        /// <summary>
        /// シート名を重複なしで追加
        /// </summary>
        /// <param name="sheetName"></param>
        public void AddSheetName(string sheetName) {
            if (!_sheetNameList.Contains(sheetName)) {
                _sheetNameList.Add(sheetName);
            }
            
        }

        /// <summary>
        /// オートシェイプ名を重複なしで追加
        /// </summary>
        /// <param name="shapeName"></param>
        public void AddShapeName(string shapeName) {
            if (!_shapeNameList.Contains(shapeName)) {
                _shapeNameList.Add(shapeName);
            }

        }

        #endregion PublicMethods

    }
}
