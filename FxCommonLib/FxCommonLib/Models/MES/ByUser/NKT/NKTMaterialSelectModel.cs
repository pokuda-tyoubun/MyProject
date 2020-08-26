using FxCommonLib.Consts.MES;
using FxCommonLib.Extensions;
using FxCommonLib.Utils;
using System;
using System.Data;

namespace FxCommonLib.Models.MES.ByUser.NKT {
    public class NKTMaterialSelectModel : BaseMaterialSelectModel{

        #region Properties

        /// <summary>基準日時</summary>
        private DateTime _baseDate = DateTime.Now;
        /// <summary>基準日時</summary>
        public DateTime BaseDate {
            set { _baseDate = value; }
            get { return _baseDate; }
        }

        /// <summary>材質コード</summary>
        private string _materialCd = string.Empty;
        /// <summary>材質コード</summary>
        public string MaterialCd {
            set { _materialCd = value; }
            get { return _materialCd; }
        }
        /// <summary>材質名</summary>
        private string _materialName = string.Empty;
        /// <summary>材質名</summary>
        public string MaterialName {
            set { _materialName = value; }
            get { return _materialName; }
        }
        /// <summary>厚さ</summary>
        private double _itemThickness = 0;
        /// <summary>厚さ</summary>
        public double ItemThickness {
            set { _itemThickness = value; }
            get { return _itemThickness; }
        }
        /// <summary>厚さ(絶対値)</summary>
        public double ItemThicknessAbs {
            //set { _itemThickness = value; }
            get { return Math.Abs(_itemThickness); }
        }
        /// <summary>巾</summary>
        private double _itemWidth = 0;
        /// <summary>巾</summary>
        public double ItemWidth {
            set { _itemWidth = value; }
            get { return _itemWidth; }
        }
        /// <summary>巾(絶対値)</summary>
        public double ItemWidthAbs {
            //set { _itemWidth = value; }
            get { return Math.Abs(_itemWidth); }
        }
        /// <summary>長さ</summary>
        private double _itemLength = 0;
        /// <summary>長さ</summary>
        public double ItemLength {
            set { _itemLength = value; }
            get { return _itemLength; }
        }
        /// <summary>長さ(絶対値)</summary>
        public double ItemLengthAbs {
            //set { _itemLength = value; }
            get { return Math.Abs(_itemLength); }
        }
        /// <summary>面取り加工コード（材寸記号）</summary>
        private MESConsts.ChamferType _chamferTypeCd = MESConsts.ChamferType.None;
        /// <summary>面取り加工コード（材寸記号）</summary>
        public int ChamferTypeCd {
            set { _chamferTypeCd = (MESConsts.ChamferType)Enum.ToObject(typeof(MESConsts.ChamferType), value); }
            get { return (int)_chamferTypeCd; }
        }
        /// <summary>材寸</summary>
        private string _materialSize = string.Empty;
        /// <summary>材寸</summary>
        public string MaterialSize {
            set { _materialSize = value; }
            get { return _materialSize; }
        }

        /// <summary>仕入先コード</summary>
        private string _supplierCd = string.Empty;
        /// <summary>仕入先コード</summary>
        public string SupplierCd {
            set { _supplierCd = value; }
            get { return _supplierCd; }
        }

        /// <summary>仕入先名称</summary>
        private string _supplierName = string.Empty;
        /// <summary>仕入先名称</summary>
        public string SupplierName {
            set { _supplierName = value; }
            get { return _supplierName; }
        }

        /// <summary>1個あたりの重量</summary>
        private double _unitWeight = 0;
        /// <summary>1個あたりの重量</summary>
        public double UnitWeight {
            set { _unitWeight = value; }
            get { return _unitWeight; }
        }

        /// <summary>Kg単価</summary>
        private double _kiloCost = 0;
        /// <summary>Kg単価</summary>
        public double KiloCost {
            set { _kiloCost = value; }
            get { return _kiloCost; }
        }

        /// <summary>Kg単価(指値)</summary>
        private double _editedKiloCost = 0;
        /// <summary>Kg単価</summary>
        public double EditedKiloCost {
            set { _editedKiloCost = value; }
            get { return _editedKiloCost; }
        }

        /// <summary>材料費（切断のみ）</summary>
        private double _materialCostCutting = 0;
        /// <summary>材料費（切断のみ）</summary>
        public double MaterialCostCutting {
            set { _materialCostCutting = value; }
            get { return _materialCostCutting; }
        }

        /// <summary>材料数量</summary>
        private double _materialQty = 1; // デフォルト設定
        /// <summary>材料数量</summary>
        public double MaterialQty {
            set { _materialQty = value; }
            get { return _materialQty; }
        }

        /// <summary>面取り費用</summary>
        private double _chamferCost = 0;
        /// <summary>面取り費用</summary>
        public double ChamferCost {
            set { _chamferCost = value; }
            get { return _chamferCost; }
        }

        /// <summary>その他加工費</summary>
        private double _otherProcessingCost = 0;
        /// <summary>その他加工費</summary>
        public double OtherProcessingCost {
            set { _otherProcessingCost = value; }
            get { return _otherProcessingCost; }
        }

        /// <summary>材料費計</summary>
        private double _materialCostTotal = 0;
        /// <summary>材料費計</summary>
        public double MaterialCostTotal {
            set { _materialCostTotal = value; }
            get { return _materialCostTotal; }
        }

        #endregion Properties

        #region Constracotrs
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public NKTMaterialSelectModel()
            :base() {
            // 継承元でデータテーブル初期化
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public NKTMaterialSelectModel(DataTable dt)
            : base() {
            // 継承元でデータテーブル初期化
            // データテーブルから上書き
            SetFromDataTable(dt);
        }

        #endregion Constracotrs

        #region PublicMethods

        /// <summary>
        /// データテーブルから上書き
        /// </summary>
        /// <param name="dt"></param>
        public void SetFromDataTable(DataTable dt) {

            if (dt == null || dt.Rows.Count == 0) { return; }

            //DeepCopyUtil dcu = new DeepCopyUtil();
            //_detail = dcu.DeepCopy(dt);
            DataRow dr = dt.Rows[0];

            // NOTE:カラムが存在すれば移送
            if (!string.IsNullOrEmpty(GetFromDr(dr, MESConsts.base_date))) {
                this._baseDate = DateTime.Parse(GetFromDr(dr, MESConsts.base_date));
            }
            this._materialCd = GetFromDr(dr,MESConsts.material_code);
            this._materialName = GetFromDr(dr,MESConsts.material_name);
            this._itemThickness = double.Parse(StringUtil.NullBlankToZero(GetFromDr(dr,MESConsts.item_thickness)));
            this._itemWidth = double.Parse(StringUtil.NullBlankToZero(GetFromDr(dr,MESConsts.item_width)));
            this._itemLength = double.Parse(StringUtil.NullBlankToZero(GetFromDr(dr,MESConsts.item_length)));
            this._chamferTypeCd = (MESConsts.ChamferType)int.Parse(StringUtil.NullBlankToZero(GetFromDr(dr,MESConsts.chamfer_type_code)));
            this._materialSize = GetFromDr(dr,MESConsts.material_size);
            this._supplierCd = GetFromDr(dr,MESConsts.supplier_cd);
            this._supplierName = GetFromDr(dr,MESConsts.supplier_name);
            this._unitWeight = double.Parse(StringUtil.NullBlankToZero(GetFromDr(dr,MESConsts.unit_weight)));
            this._kiloCost = double.Parse(StringUtil.NullBlankToZero(GetFromDr(dr,MESConsts.kilo_cost)));
            this._editedKiloCost = double.Parse(StringUtil.NullBlankToZero(GetFromDr(dr, MESConsts.edited_kilo_cost)));
            this._materialCostCutting = double.Parse(StringUtil.NullBlankToZero(GetFromDr(dr,MESConsts.material_cost_cutting)));

            if (dr.Table.Columns.Contains(MESConsts.material_qty)) {
                // NOTE:材料数のみ、カラムが存在しなければ値キープ
                this._materialQty = double.Parse(StringUtil.NullBlankToZero(GetFromDr(dr, MESConsts.material_qty)));
            }

            this._chamferCost = double.Parse(StringUtil.NullBlankToZero(GetFromDr(dr,MESConsts.chamfer_cost)));
            this._otherProcessingCost = double.Parse(StringUtil.NullBlankToZero(GetFromDr(dr,MESConsts.other_processing_cost)));
            this._materialCostTotal = double.Parse(StringUtil.NullBlankToZero(GetFromDr(dr,MESConsts.material_cost_total)));
            
        }

        /// <summary>
        /// Modelからデータテーブルを取得
        /// </summary>
        public override DataTable ToDataTable() {

            _detail.Clear();
            DataRow dr = _detail.NewRow();
            dr[MESConsts.base_date] = _baseDate;
            dr[MESConsts.material_code] = _materialCd;
            dr[MESConsts.material_name] = _materialName;
            dr[MESConsts.item_thickness] = _itemThickness;
            dr[MESConsts.item_width] = _itemWidth;
            dr[MESConsts.item_length] = _itemLength;
            dr[MESConsts.chamfer_type_code] = (int)_chamferTypeCd;
            dr[MESConsts.material_size] = _materialSize;
            dr[MESConsts.supplier_cd] = _supplierCd;
            dr[MESConsts.supplier_name] = _supplierName;
            dr[MESConsts.unit_weight] = _unitWeight;
            dr[MESConsts.kilo_cost] = _kiloCost;
            dr[MESConsts.edited_kilo_cost] = _editedKiloCost;
            dr[MESConsts.material_cost_cutting] = _materialCostCutting;
            dr[MESConsts.material_qty] = _materialQty;
            dr[MESConsts.chamfer_cost] = _chamferCost;
            dr[MESConsts.other_processing_cost] = _otherProcessingCost;
            dr[MESConsts.material_cost_total] = _materialCostTotal;

            // NOTE:MES工程編集画面連携用
            dr[MESConsts.item_ext3] = _supplierCd;
            dr[MESConsts.item_ext4] = _otherProcessingCost;
            dr[MESConsts.item_ext5] = _materialCostTotal;
            dr[MESConsts.item_ext6] = _materialQty;
            dr[MESConsts.item_ext7] = _editedKiloCost;

            _detail.Rows.Add(dr);

            _detail.AcceptChanges();

            return _detail;
        }

        #endregion PublicMethods

        #region ProtectedMethods
        /// <summary>
        /// 内訳データテーブルの初期化
        /// </summary>
        protected override void InitDetail() {

            DataTable dt = new DataTable(MESConsts.MaterialCostTbl);

            dt.Columns.Add(MESConsts.base_date, typeof(DateTime));
            dt.Columns.Add(MESConsts.material_code,typeof(string));
            dt.Columns.Add(MESConsts.material_name, typeof(string));
            dt.Columns.Add(MESConsts.item_thickness, typeof(double));
            dt.Columns.Add(MESConsts.item_width, typeof(double));
            dt.Columns.Add(MESConsts.item_length, typeof(double));
            dt.Columns.Add(MESConsts.chamfer_type_code, typeof(string));
            dt.Columns.Add(MESConsts.material_size, typeof(string));
            dt.Columns.Add(MESConsts.supplier_cd, typeof(string));
            dt.Columns.Add(MESConsts.supplier_name, typeof(string));
            dt.Columns.Add(MESConsts.unit_weight, typeof(double));
            dt.Columns.Add(MESConsts.kilo_cost, typeof(double));
            dt.Columns.Add(MESConsts.edited_kilo_cost, typeof(double));
            dt.Columns.Add(MESConsts.material_cost_cutting, typeof(double));
            dt.Columns.Add(MESConsts.material_qty, typeof(double));
            dt.Columns.Add(MESConsts.chamfer_cost, typeof(double));
            dt.Columns.Add(MESConsts.other_processing_cost, typeof(double));
            dt.Columns.Add(MESConsts.material_cost_total, typeof(double));

            // NOTE:MES工程編集画面連携用(only output)
            dt.Columns.Add(MESConsts.item_ext3, typeof(string)); // = supplier_cd
            dt.Columns.Add(MESConsts.item_ext4, typeof(double)); // = other_processing_cost
            dt.Columns.Add(MESConsts.item_ext5, typeof(double)); // = material_cost_total
            dt.Columns.Add(MESConsts.item_ext6, typeof(double)); // = material_qty
            dt.Columns.Add(MESConsts.item_ext7, typeof(double)); // = edited_kilo_cost

            dt.AcceptChanges();
            DeepCopyUtil dcu = new DeepCopyUtil();
            _detail = dcu.DeepCopy(dt);
        }
        #endregion ProtectedMethods

        #region PrivateMethods

        /// <summary>
        /// DataRowからカラムが存在しない場合カラ文字を返却
        /// </summary>
        /// <param name="dr"></param>
        /// <param name="dbName"></param>
        /// <returns></returns>
        private string GetFromDr(DataRow dr, string dbName) {

            return StringUtil.NullToBlank(dr.GetValueIfColNameContains(dbName));

        }

        #endregion PrivateMethods

    }
}
