using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PriceInspector.Models {
    public class Item {
        /// <summary>識別コード</summary>
        public string Code;
        /// <summary>品名</summary>
        public string ItemName;
        /// <summary>メルカリ価格</summary>
        public Decimal MercariPrice;
        /// <summary>最安値.com価格</summary>
        public Decimal SaiyasunePrice;
    }
}
