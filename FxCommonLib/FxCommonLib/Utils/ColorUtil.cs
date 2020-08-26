using System;
using System.Drawing;

namespace FxCommonLib.Utils {
    /// <summary>
    /// 色操作のユーティリティクラス
    /// </summary>
    public class ColorUtil {
        /// <summary>
        /// ランダムカラー取得
        /// </summary>
        /// <param name="r">シードとなる疑似乱数ジェネレータ</param>
        /// <returns></returns>
        public Color GetRandomColor(Random r) {
            int red = r.Next(256);
            int green = r.Next(256);
            int blue = r.Next(256);

            return Color.FromArgb(red, green, blue);
        }
        /// <summary>
        /// 淡いランダムカラー取得
        /// </summary>
        /// <param name="r">シードとなる疑似乱数ジェネレータ</param>
        /// <returns></returns>
        public Color GetPaleRandomColor(Random r) {
            int red = r.Next(100, 256);
            int green = r.Next(100, 256);
            int blue = r.Next(100, 256);

            return Color.FromArgb(red, green, blue);
        }

        /// <summary>
        /// 文字列"#AA80CB"からColorオブジェクトを生成
        /// </summary>
        /// <param name="strColor">色を表す16進数文字列</param>
        /// <returns>Colorオブジェクト</returns>
        public Color GetColor(string strColor) {
            return ColorTranslator.FromHtml(strColor);
        }

        /// <summary>
        /// Colorオブジェクトから文字列"#AA80CB"を生成
        /// </summary>
        /// <param name="c">Colorオブジェクト</param>
        /// <returns>色を表す16進数文字列</returns>
        public string GetColorString(Color c) {
            return String.Format("#{0:X2}{1:X2}{2:X2}", c.R, c.G, c.B);
        }
        /// <summary>
        /// 反転色を取得
        /// </summary>
        /// <param name="color">Colorオブジェクト</param>
        /// <returns>反転色のColorオブジェクト</returns>
        public Color GetXorColor(Color color) {
            return Color.FromArgb(color.ToArgb() ^ 0xffffff);
        }
    }
}
