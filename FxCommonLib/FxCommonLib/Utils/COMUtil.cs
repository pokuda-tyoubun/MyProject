using System;
using System.Reflection;

namespace FxCommonLib.Utils {
    /// <summary>
    /// COMユーティリティ
    /// </summary>
    public class COMUtil {

        #region PublicMethods
        /// <summary>
        /// COMオブジェクトへの参照を作成および取得します
        /// </summary>
        /// <param name="progId">作成するオブジェクトのプログラムID</param>
        /// <param name="serverName">
        /// オブジェクトが作成されるネットワークサーバー名
        /// </param>
        /// <returns>作成されたCOMオブジェクト</returns>
        public object CreateObject(string progId, string serverName) {
            Type t;
            if (serverName == null || serverName.Length == 0) {
                t = Type.GetTypeFromProgID(progId);
            } else {
                t = Type.GetTypeFromProgID(progId, serverName, true);
            }
            return Activator.CreateInstance(t);
        }

        /// <summary>
        /// COMオブジェクトへの参照を作成および取得します
        /// </summary>
        /// <param name="progId">作成するオブジェクトのプログラムID</param>
        /// <returns>作成されたCOMオブジェクト</returns>
        public object CreateObject(string progId) {
            return CreateObject(progId, null);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="objCom"></param>
        /// <param name="force"></param>
        public void MReleaseComObject(object objCom, bool force = true) {
            if (objCom == null) {
                return;
            }
            try {
                if (System.Runtime.InteropServices.Marshal.IsComObject(objCom)) {
                    if (force) {
                        System.Runtime.InteropServices.Marshal.FinalReleaseComObject(objCom);
                    } else {
                        System.Runtime.InteropServices.Marshal.ReleaseComObject(objCom);
                    }
                }
            } finally {
                objCom = null;
            }
        }

        /// <summary>
        /// InvokeMemberメソッドのラッパー
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="member"></param>
        /// <param name="bf"></param>
        /// <param name="b"></param>
        /// <param name="params"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public object InvokeMember(object objMain, string member, BindingFlags bf, Binder b, object obj, params object[] @params) {
            return objMain.GetType().InvokeMember(member, bf, b, obj, @params);
        }
        /// <summary>
        /// InvokeMember
        /// </summary>
        /// <param name="objMain"></param>
        /// <param name="member"></param>
        /// <param name="bf"></param>
        /// <returns></returns>
        public object InvokeMember(object objMain, string member, BindingFlags bf) {
            try {
                return objMain.GetType().InvokeMember(member, bf, null, objMain, null);
            } catch (Exception) {
                throw;
            }
        }
        /// <summary>
        /// InvokeMember
        /// </summary>
        /// <param name="objMain"></param>
        /// <param name="member"></param>
        /// <param name="bf"></param>
        /// <param name="params"></param>
        /// <returns></returns>
        public object InvokeMember(object objMain, string member, BindingFlags bf, params object[] @params) {
            return objMain.GetType().InvokeMember(member, bf, null, objMain, @params);
        }
        #endregion PublicMethods
    }
}