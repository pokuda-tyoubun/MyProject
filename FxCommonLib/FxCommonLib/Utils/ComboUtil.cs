using C1.Win.C1Input;
using FxCommonLib.Consts;
using System.Data;
using System.Windows.Forms;

namespace FxCommonLib.Utils {
    public class ComboUtil {
        /// <summary>
        /// 候補設定
        /// </summary>
        /// <param name="ctl"></param>
        /// <param name="dt"></param>
        public static void SetCandidate(ComboBox ctl, DataTable dt) {
            BindingSource bs = new BindingSource();
            bs.DataSource = dt;
            ctl.DataSource = bs;
            ctl.DisplayMember = CommonConsts.name;
            ctl.ValueMember = CommonConsts.code;
        }
        /// <summary>
        /// 候補設定 C1ComboBox
        /// </summary>
        /// <param name="ccb"></param>
        /// <param name="dt"></param>
        public static void SetCandidate(C1ComboBox ccb, DataTable dt) {
            BindingSource bs = new BindingSource();
            bs.DataSource = dt;
            ccb.ItemsDataSource = bs;

            ccb.TextDetached = true; //選択後、ValueMemberで表示されるのを防ぐ
            ccb.ItemsDisplayMember = CommonConsts.name;
            ccb.ItemsValueMember = CommonConsts.code;
        }
        /// <summary>
        /// SelectedIndex設定 C1ComboBox
        /// </summary>
        /// <param name="ccb"></param>
        /// <param name="candidateTbl"></param>
        /// <param name="selectedcode"></param>
        public static void SetSelectIndexFromCode(C1ComboBox ccb, DataTable candidateTbl, string selectedcode) {
            //※BindingSource利用時は、SelectedItem,SelectedTextが動作しないので、SelectedIndexで初期選択を行う
            DataRow[] dra = candidateTbl.Select("convert(" + CommonConsts.code + ", 'System.String') = '" + selectedcode + "'");
            ccb.SelectedIndex = candidateTbl.Rows.IndexOf(dra[0]);
        }

    }
}
