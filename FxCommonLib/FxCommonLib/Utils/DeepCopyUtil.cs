using System.Data;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace FxCommonLib.Utils {
    public class DeepCopyUtil {
        #region PublicMethod
        /// <summary>
        /// オブジェクトをディープコピーする
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        public object DeepCopy(object target) {
            object result;
            BinaryFormatter b = new BinaryFormatter();
            MemoryStream mem = new MemoryStream();
            try {
                b.Serialize(mem, target);
                mem.Position = 0;
                result = b.Deserialize(mem);
            } finally {
                mem.Close();
            }

            return result;
        }

        /// <summary>
        /// DataTableをディープコピーする
        /// ※MemoryStreamを用いるとメモリ効率が悪い。
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        public DataTable DeepCopy(DataTable target) {
            //列定義を含めた完全なDeepCopyを行う。
            return target.Copy();
        }
        #endregion PublicMethod
    }
}
