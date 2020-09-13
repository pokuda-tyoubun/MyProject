using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

namespace FxCommonLib.Win32API {
    public class Shell32 {

		[DllImport("shell32")]
        public static extern bool SHObjectProperties(IntPtr hwnd, uint shopObjectType,
                                [MarshalAs(UnmanagedType.LPWStr)] string pszObjectName,
                                [MarshalAs(UnmanagedType.LPWStr)] string pszPropertyPage);

        public enum SHOP : uint {
            SHOP_PRINTERNAME = 0x1,
            SHOP_FILEPATH = 0x2,
            SHOP_VOLUMEGUID = 0x4
        }

        [DllImport("Shell32.dll", CharSet=CharSet.Unicode)]
        public static extern int SHGetKnownFolderPath(
            [MarshalAs(UnmanagedType.LPStruct)] Guid rfid, uint dwFlags, IntPtr hToken, out string pszPath);

    }
}