using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PriceInspector.Models {
    public class Item {
        /// <summary>識別コード</summary>
        public string Code = "";
        /// <summary>品名</summary>
        public string ItemName = "";
        /// <summary>メルカリ価格</summary>
        public Decimal MercariPrice = -1;
        /// <summary>メルカリURL</summary>
        public string MercariUrl = "";
        /// <summary>最安値.com価格</summary>
        public Decimal SaiyasuneNewPrice = -1;
        /// <summary>最安値.com価格</summary>
        public Decimal SaiyasuneOldPrice = -1;
        /// <summary>最安値URL</summary>
        public string SaiyasuneUrl = "";
        /// <summary>Amazon.co.jp価格</summary>
        public Decimal AmazonJpPrice = -1;
        /// <summary>Amazon.com価格</summary>
        public Decimal AmazonComPrice = -1;

        public void TraceAll() {
            var sb = new StringBuilder();
            sb.AppendLine(this.Code);
            sb.AppendLine(this.ItemName);
            sb.AppendLine(this.MercariPrice.ToString("C"));
            sb.AppendLine(this.MercariUrl);
            sb.AppendLine(this.SaiyasuneNewPrice.ToString("C"));
            sb.AppendLine(this.SaiyasuneOldPrice.ToString("C"));
            sb.AppendLine(this.SaiyasuneUrl);
            sb.AppendLine("Diff:" + (this.SaiyasuneOldPrice - this.MercariPrice).ToString("C"));
            sb.AppendLine(this.AmazonJpPrice.ToString("C"));
            sb.AppendLine(this.AmazonComPrice.ToString("C"));
            Trace.WriteLine(sb.ToString());
        }

        public Decimal Profit() {
            Decimal min = -1;

            if (this.SaiyasuneNewPrice > 0) {
                min = this.SaiyasuneNewPrice;
            }
            if (this.SaiyasuneOldPrice > 0) {
                if (min > this.SaiyasuneOldPrice) {
                    min = this.SaiyasuneOldPrice;
                }
            }

            return min - this.MercariPrice;
        }

        public static DataTable CreateBlankTable() {
            DataTable ret = new DataTable();

            ret.Columns.Add("識別コード", typeof(string));
            ret.Columns.Add("品名", typeof(string));
            ret.Columns.Add("メルカリ価格", typeof(Decimal));
            ret.Columns.Add("メルカリUrl", typeof(string));
            ret.Columns.Add("最安値.com新品価格", typeof(Decimal));
            ret.Columns.Add("最安値.com中古価格", typeof(Decimal));
            ret.Columns.Add("最安値Url", typeof(string));
            ret.Columns.Add("利益", typeof(Decimal));
            ret.AcceptChanges();

            return ret;
        }
    }
}
