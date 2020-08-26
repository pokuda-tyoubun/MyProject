using FxCommonLib.Consts.MES;
using System.Data;

namespace FxCommonLib.Models {
    public class ResourceNode {
        public DataRow Row { get; set; }

        public string Resource { 
            get { return this.ToString(); }
        }

        public override string ToString() {
            string ret = "";
            if (this.Row != null) {
                ret = this.Row[MESConsts.resource].ToString();
            }
            return ret;
        }
    }
}
