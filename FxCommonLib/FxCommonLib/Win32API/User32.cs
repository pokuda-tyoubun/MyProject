using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

namespace FxCommonLib.Win32API {
    public class User32 {

    	[StructLayoutAttribute(LayoutKind.Sequential)]
    	public struct POINT {
    		public POINT(System.Drawing.Point p) { x = p.X; y = p.Y; }
    		public POINT(Int32 X, Int32 Y) { x = X; y = Y; }
    		public Int32 x;
    		public Int32 y;
    	}

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool SetForegroundWindow(IntPtr hWnd);

		[DllImport("user32.dll", EntryPoint = "WindowFromPoint", SetLastError = true, CallingConvention = CallingConvention.StdCall)]
		public static extern IntPtr WindowFromPoint(POINT Point);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern int GetWindowTextLength(IntPtr hWnd);

		[DllImport("user32.dll")]
		public static extern int GetWindowText(IntPtr hwnd, StringBuilder lpString, int cch);

		[DllImport("user32.dll")]
		public static extern int GetClassName(IntPtr hwnd, StringBuilder lpClassName, int nMaxCount);
    }
}
