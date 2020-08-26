using C1.Win.C1FlexGrid;
using FxCommonLib.Consts;
using FxCommonLib.Models;
using FxCommonLib.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FxCommonLib.Tests.Controls.Demo {
    public partial class FlexGridExDemo : Form {
        public FlexGridExDemo() {
            InitializeComponent();
            var ccu = CreateColumnConfigUtil();
            var mlu = CreateMultiLangUtil();
            this.flexGridEx1.Init(mlu, ccu);
            this.flexGridEx1.PulldownDic = new Dictionary<string, Dictionary<string, string>>() {
                { "L", new Dictionary<string, string>() {
                           { "1", "選択肢1" },
                           { "2", "選択肢2" },
                           { "3", "選択肢2" }
                       }
                }
            };
            this.flexGridEx1.SetStyle();
            this.flexGridEx1.CellButtonDic = new Dictionary<string, HashSet<string>>() {
                { "CB", new HashSet<string>() { "#FFFFFF", "#000000" } }
            };
            this.flexGridEx2.Init(mlu, ccu);
            this.flexGridEx2.PulldownDic = new Dictionary<string, Dictionary<string, string>>() {
                { "L", new Dictionary<string, string>() {
                           { "1", "選択肢1" },
                           { "2", "選択肢2" },
                           { "3", "選択肢2" }
                       }
                }
            };
            this.flexGridEx2.SetStyle();
            this.flexGridEx2.CellButtonDic = new Dictionary<string, HashSet<string>>() {
                { "CB", new HashSet<string>() { "#FFFFFF", "#000000" } }
            };
            var dt = new DataTable();
            foreach (Column col in this.flexGridEx1.Cols.Cast<Column>().Skip(1)) {
                dt.Columns.Add(ccu.DictionaryByName[col.Name].DBName, typeof(string));
            }
            dt.Rows.Add(dt.NewRow());
            dt.Rows.Add(dt.NewRow());
            dt.Rows.Add(dt.NewRow());
            this.flexGridEx1.BindData(dt.Copy());
            this.flexGridEx2.BindData(dt.Copy());
            return;
        }

        private MultiLangUtil CreateMultiLangUtil() {
            MultiLangUtil mlu = new MultiLangUtil();
            CSVUtil csvUtil = new CSVUtil();
            DataTable msgTable = csvUtil.ReadCsv(@".\TestData\FlexGridExTestMessage.csv", "", ',', Encoding.GetEncoding("shift_jis"));
            mlu.MessageDictionary = msgTable.AsEnumerable()
                                            .ToDictionary(
                                                r => r[CommonConsts.message_id].ToString(),
                                                r => r[CommonConsts.message].ToString()
                                            );
            return mlu;
        }

        private ColumnConfigUtil CreateColumnConfigUtil() {
            ColumnConfigUtil ccu = new ColumnConfigUtil(this.Name, this.flexGridEx1.Name);
            CSVUtil csvUtil = new CSVUtil();
            DataTable colTable = csvUtil.ReadCsv(@".\TestData\FlexGridExTestColumn.csv", "", ',', Encoding.GetEncoding("shift_jis"));

            //ColumnConfig形式に変換
            int leftFixedCount = 0;
            foreach (DataRow r in colTable.Rows) {
                ColumnInfo ci = new ColumnInfo();

                ci.DBName = r[CommonConsts.db_name].ToString();
                ci.Width = int.Parse(r[CommonConsts.width].ToString());
                ccu.ColConf.Height = int.Parse(r[CommonConsts.height].ToString());
                ci.ColName = r[CommonConsts.col_name].ToString();
                ci.ConfEditable = StringUtil.ParseBool(r[CommonConsts.conf_editable].ToString());
                ci.Visible = StringUtil.ParseBool(r[CommonConsts.visible].ToString());
                ci.Editable = StringUtil.ParseBool(r[CommonConsts.editable].ToString());
                ci.DisplayOrder = int.Parse(r[CommonConsts.disp_order].ToString());
                ci.DataType = r[CommonConsts.data_type].ToString();
                ci.MaxLength = int.Parse(r[CommonConsts.max_length].ToString());
                ci.Required = StringUtil.ParseBool(r[CommonConsts.required].ToString());
                ci.IsPrimaryKey = StringUtil.ParseBool(r[CommonConsts.primary_key].ToString());
                ci.PasswordChar = r[CommonConsts.password_char].ToString();
                ci.Note = r[CommonConsts.note].ToString();
                if (StringUtil.ParseBool(r[CommonConsts.col_fixed].ToString()) == true) {
                    leftFixedCount++;
                }

                ccu.ColConf.ColList.Add(ci);
            }
            ccu.ColConf.LeftFixedCount = leftFixedCount;
            ccu.CreateDictionary();
            return ccu;
        }

        private void CheckBox1_CheckedChanged(object sender, EventArgs e) {
            this.flexGridEx1.Cols[2].Visible = ((CheckBox)sender).Checked;
        }

        private void CheckBox2_CheckedChanged(object sender, EventArgs e) {
            this.flexGridEx2.Cols[2].Visible = ((CheckBox)sender).Checked;
        }
    }
}
