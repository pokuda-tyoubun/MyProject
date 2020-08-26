using FxCommonLib.Models;
using FxCommonLib.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FxCommonLib.BaseReportWriter {

    /// <summary>
    /// Excel帳票基底クラス
    /// </summary>
    public abstract class BaseReportWriter {

        #region Constants
        #endregion Constants
        #region Properties
        /// <summary>Excelオートシェイプの図形の大きさ調整用モデル</summary>
        protected AdjustExcelAutoShapeModel _adjustExcelAutoShapeModel = new AdjustExcelAutoShapeModel();
        /// <summary>Excelオートシェイプの図形の大きさ調整用モデル</summary>
        public AdjustExcelAutoShapeModel AdjustExcelAutoShapeModel {
            set { _adjustExcelAutoShapeModel = value; }
            get { return _adjustExcelAutoShapeModel; }
        }

        #endregion Properties
        #region MemberVariables
        #endregion MemberVariables
        #region Constractors
        #endregion Constractors
        #region EventHandlers
        #endregion EventHandlers
        #region PublicMethods
        #endregion PublicMethods
        #region ProtectedMethods
        #endregion ProtectedMethods
        #region PrivateMethods
        #endregion PrivateMethods

    }
}
