using FxCommonLib;
using FxCommonLib.Consts.MES;
using FxCommonLib.Utils;
using MathNet.Numerics.Statistics;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace FxCommonLib.Models.MES{
    public abstract class BaseMaterialSelectModel {

        #region Properties
        /// <summary>内訳データテーブル</summary>
        protected DataTable _detail = new DataTable(MESConsts.MaterialCostTbl);
        public DataTable Detail {
            set { _detail = value; }
            //get { return _detail; }
        }
        #endregion Properties

        #region MemberVariables
        #endregion MemnerVariables

        #region Constracotrs
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public BaseMaterialSelectModel() {
            // 内訳データテーブルの初期化
            InitDetail();
        }
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public BaseMaterialSelectModel(DataTable dt) {
            // 内訳データテーブルの初期化
            InitDetail();
        }

        #endregion Constracotrs

        #region PublicMethods

        public abstract DataTable ToDataTable();

        #endregion PublicMethods

        #region ProtectedMethods

        protected abstract void InitDetail();



        #endregion ProtectedMethods

        #region PrivateMethods
        #endregion PrivateMethods
    }
}
