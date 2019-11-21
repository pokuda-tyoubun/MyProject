using Gecko;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokudaPriceInspector.Utils {
    public class GeckoBrowserUtil {
        public List<GeckoElement> GetElementByClassName(GeckoElement root, string className) {
            var ret = new List<GeckoElement>();
            foreach (var node in root.ChildNodes) {
                if (node.GetType() == typeof(GeckoHtmlElement)) {
                    GeckoElement element = (GeckoElement)node;   
                    string tmp = element.GetAttribute("class");
                    if (tmp == className) {
                        //探索完了
                        ret.Add(element);
                    }
                }
            }

            return ret;
        }
        public List<GeckoElement> GetElementByClassNameRecv(GeckoElement root, string className) {
            var ret = new List<GeckoElement>();
            GetElementByClassNameRecv(root, className, ret);
            return ret;
        }
        private void GetElementByClassNameRecv(GeckoElement root, string className, List<GeckoElement> ret) {
            foreach (var node in root.ChildNodes) {
                if (node.GetType() != typeof(GeckoNode)) {
                    GeckoElement element = (GeckoElement)node;

                    string tmp = element.GetAttribute("class");
                    //Trace.WriteLine(tmp);
                    if (tmp == className) {
                        //合致したelementを追加
                        ret.Add(element);
                    }
                    GetElementByClassNameRecv(element, className, ret);
                }
                //Trace.WriteLine(node.LocalName);
            }
        }
    }
}
