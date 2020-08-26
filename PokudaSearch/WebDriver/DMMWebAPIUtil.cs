using opennlp.tools.parser;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace PokudaSearch.WebDriver {
    public class DMMWebAPIUtil {
        public List<TagInfo> GetItemList(string keyword) {
            List<TagInfo> ret = null;
            WebResponse response = null;
            try {
                const string apiId = "zx5gaKL4fwyKDeBzUZ1q";
                const string affiliateId = "PokudaEng-990";
                string url = @"https://api.dmm.com/affiliate/v3/ItemList?api_id=" + apiId + "&affiliate_id=" + affiliateId + "&site=FANZA&service=digital&floor=videoa&hits=10&sort=date&keyword=%e4%b8%8a%e5%8e%9f%e4%ba%9c%e8%a1%a3&output=json";

                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                response = request.GetResponse();
                Stream dataStream = response.GetResponseStream();
                StreamReader sReader = new StreamReader(dataStream);
                string jsonString = sReader.ReadToEnd();
                Debug.Write(jsonString);

                //var jo = JObject.Parse(jsonString);
                //JToken jt = jo.SelectToken("$.rows..elements..distance.value");
                //ret = double.Parse(StringUtil.NullBlankToZero(jt.ToString()));

                return ret;
            } finally {
                response.Close();
            }
        }
    }
}
