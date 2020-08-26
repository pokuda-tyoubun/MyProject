using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FxCommonLib.Utils {
    public class ExpandObjectUtil {
        public dynamic DicToDynamic(Dictionary<string, object> dic) {
            dynamic tmp = new ExpandoObject();
            IDictionary<string, object> wk = tmp;
            foreach (var item in dic) {
                wk.Add(item.Key, item.Value); 
            }

            return tmp;
        }
    }
}
