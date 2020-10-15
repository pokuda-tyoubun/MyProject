using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FxCommonLib.Win32API {
    public static class Macros {

        #region WindowMessage
        // Define the Windows messages we will handle
        public const int WM_PARENTNOTIFY = 0x0210;

        public const int WM_MOUSEMOVE = 0x0200;
        public const int WM_LBUTTONDOWN = 0x0201;
        public const int WM_LBUTTONUP = 0x0202;
        public const int WM_LBUTTONDBLCLK = 0x0203;
        public const int WM_RBUTTONDOWN = 0x0204;
        public const int WM_RBUTTONUP = 0x0205;
        public const int WM_RBUTTONDBLCLK = 0x0206;
        public const int WM_MBUTTONDOWN = 0x0207;
        public const int WM_MBUTTONUP = 0x0208;
        public const int WM_MBUTTONDBLCLK = 0x0209;
        public const int WM_MOUSEWHEEL = 0x020A;
        public const int WM_XBUTTONDOWN = 0x020B;
        public const int WM_XBUTTONUP = 0x020C;
        public const int WM_XBUTTONDBLCLK = 0x020D;
        public const int WM_MOUSELEAVE = 0x02A3;
        #endregion WindowMessage

        public static ushort GET_KEYSTATE_WPARAM(uint wParam) {
            return LOWORD(wParam);
        }

        public static ushort GET_NCHITTEST_WPARAM(uint wParam) {
            return LOWORD(wParam);
        }

        public enum XBUTTONS : ushort {
            XBUTTON1 = 1,
            XBUTTON2 = 2
        }

        public static XBUTTONS GET_XBUTTON_WPARAM(uint wParam) {
            return (XBUTTONS)HIWORD(wParam);
        }

        public static short GET_X_LPARAM(uint lp) {
            return (short)LOWORD(lp);
        }

        public static short GET_Y_LPARAM(uint lp) {
            return (short)HIWORD(lp);
        }

        public static uint MAKEWPARAM(ushort l, ushort h) {
            return MAKELONG(l, h);
        }

        public static uint MAKELPARAM(ushort l, ushort h) {
            return MAKELONG(l, h);
        }

        public static uint MAKELRESULT(ushort l, ushort h) {
            return MAKELONG(l, h);
        }

        public static ushort MAKEWORD(byte a, byte b) {
            return (ushort)((ushort)a | (((ushort)b) << 8));
        }

        public static uint MAKELONG(ushort l, ushort h) {
            return ((uint)l) | (((uint)h) << 16);
        }

        public static uint LODWORD(ulong dq) {
            return (uint)dq;
        }

        public static uint HIDWORD(ulong dq) {
            return (uint)(dq >> 32);
        }

        public static ushort LOWORD(uint dd) {
            return (ushort)dd;
        }

        public static ushort HIWORD(uint dd) {
            return (ushort)(dd >> 16);
        }

        public static byte LOBYTE(ushort dw) {
            return (byte)dw;
        }

        public static byte HIBYTE(ushort dw) {
            return (byte)(dw >> 8);
        }
    }
}
