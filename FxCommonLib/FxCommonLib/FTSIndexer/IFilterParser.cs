using System;
using System.Runtime.InteropServices;
using System.Text;

namespace FxCommonLib.FTSIndexer {
    /// <summary>
    /// iFilterによるテキスト抽出
    /// </summary>
    public class IFilterParser {
        public IFilterParser() {
        }

        //DLL読み込み
        [DllImport("query.dll", CharSet = CharSet.Unicode)]
        private extern static int LoadIFilter(string pwcsPath, ref IUnknown pUnkOuter, ref IFilter ppIUnk);

        [ComImport, Guid("00000000-0000-0000-C000-000000000046")]
        [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
        private interface IUnknown {
            [PreserveSig]
            IntPtr QueryInterface(ref Guid riid, out IntPtr pVoid);

            [PreserveSig]
            IntPtr AddRef();

            [PreserveSig]
            IntPtr Release();
        }


        /// <summary>
        /// iFilterローディング
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        private static IFilter LoadIFilter(string filename) {
            IUnknown iunk = null;
            IFilter filter = null;

            int resultLoad = LoadIFilter(filename, ref iunk, ref filter);
            if (resultLoad != (int)IFilterReturnCodes.S_OK) {
                return null;
            }
            return filter;
        }

        /*
                private static IFilter loadIFilterOffice(string filename)
                {
                    IFilter filter = (IFilter)(new CFilter());
                    System.Runtime.InteropServices.UCOMIPersistFile ipf = (System.Runtime.InteropServices.UCOMIPersistFile)(filter);
                    ipf.Load(filename, 0);

                    return filter;
                }
        */

        /// <summary>
        /// iFilterを利用可能かどうか？
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        public static bool IsParseable(string filename) {
            return LoadIFilter(filename) != null;
        }

        /// <summary>
        /// CHUNK_TEXT状態ファイル(iFilterでテキスト変換されたもの)の検索用文字列作成
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static string Parse(string fileName) {
            IFilter filter = null;

            try {
                StringBuilder plainTextResult = new StringBuilder();
                filter = LoadIFilter(fileName);

                STAT_CHUNK ps = new STAT_CHUNK();
                IFILTER_INIT mFlags = 0;

                uint i = 0;
                filter.Init(mFlags, 0, null, ref i);

                int resultChunk = 0;

                resultChunk = filter.GetChunk(out ps);
                while (resultChunk == 0) {
                    if (ps.flags == CHUNKSTATE.CHUNK_TEXT) {
                        uint sizeBuffer = 60000;
                        int resultText = 0;
                        while (resultText == Constants.FILTER_S_LAST_TEXT || resultText == 0) {
                            sizeBuffer = 60000;
                            StringBuilder sbBuffer = new StringBuilder((int)sizeBuffer);
                            resultText = filter.GetText(ref sizeBuffer, sbBuffer);

                            if (sizeBuffer > 0 && sbBuffer.Length > 0) {
                                string chunk = sbBuffer.ToString(0, (int)sizeBuffer);
                                plainTextResult.Append(chunk);
                            }
                        }
                    }
                    resultChunk = filter.GetChunk(out ps);
                }
                return plainTextResult.ToString();
            } finally {
                if (filter != null) {
                    Marshal.ReleaseComObject(filter);
                    //Marshal.FinalReleaseComObject(filter);
                }
            }
        }
    }
}