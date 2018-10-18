using Lucene.Net.Analysis;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace JapaneseAnalyzerTest.JapaneseAnalyzer {

	public class NormalizeReader : TextReader {
        /// <summary>変換テーブル</summary>
		public static int[] CONVERSION_TABLE = {-1, '!', '"', '#', '$', '%',
			'&', '\'', '(', ')', '*', '+', ',', '-', '.', '/', '0', '1', '2', '3',
			'4', '5', '6', '7', '8', '9', ':', ';', '<', '+', '>', '?', '@', 'A',
			'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O',
			'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z', '[', '\\', ']',
			'^', '_', '`', 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k',
			'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y',
			'z', '{', '|', '}', '~', -1, -1, '。', '「', '」', '、', '・', 'ヲ', 'ァ', 'ィ',
			'ゥ', 'ェ', 'ォ', 'ャ', 'ュ', 'ョ', 'ッ', 'ー', 'ア', 'イ', 'ウ', 'エ', 'オ', 'カ',
			'キ', 'ク', 'ケ', 'コ', 'サ', 'シ', 'ス', 'セ', 'ソ', 'タ', 'チ', 'ツ', 'テ', 'ト',
			'ナ', 'ニ', 'ヌ', 'ネ', 'ノ', 'ハ', 'ヒ', 'フ', 'ヘ', 'ホ', 'マ', 'ミ', 'ム', 'メ',
			'モ', 'ヤ', 'ユ', 'ヨ', 'ラ', 'リ', 'ル', 'レ', 'ロ', 'ワ', 'ン', '゛', '゜',};

		private TextReader _textReader;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="textReader"></param>
		public NormalizeReader(TextReader textReader) {
			_textReader = textReader;
		}

        /// <summary>
        /// 以下のとおり変換
        /// ・全角記号英数字→半角
        /// ・半角カナ→全角カナ
        /// </summary>
        /// <returns></returns>
		public override int Read() {
			return (int)ConvertChar((char)_textReader.Read());
		}

		/**
		 * Read characters into a portion of an array.
		 *
		 * @exception IOException
		 *							If an I/O error occurs
		 */
		public override int Read(char[] cbuf, int off, int len) {
			int l = _textReader.Read(cbuf, off, len);
            for (int i = off; i < off + len; i++) {
				cbuf[i] = ConvertChar(cbuf[i]);
            }
			return l;
		}

		public override string ReadToEnd() {
			string str = _textReader.ReadToEnd();
			StringBuilder result = new StringBuilder(str.Length);

			for(int i = 0; i < str.Length; i++) {
				result.Append(ConvertChar(str[i]));
			}
			return result.ToString();
		}
        /// <summary>
        /// 以下のとおり変換
        /// ・全角記号英数字→半角
        /// ・半角カナ→全角カナ
        /// </summary>
        /// <returns></returns>
		char ConvertChar(char c) {
			if((c >= 0xFF01 && c <= 0xFF5E) || (c >= 0xFF61 && c <= 0xFF9F)) {
				c = (char)CONVERSION_TABLE[c - 0xFF00];
			}
			return c;
		}

		/**
		 * Tell whether this stream supports the mark() operation.
		 *
		 */
		public bool markSupported() {
			return false;
		}

		/**
		 * Mark the present position in the stream.
		 *
		 * @exception IOException
		 *							If an I/O error occurs
		 */
		public void mark(int readAheadLimit) {
			throw new IOException("mark/reset not supported");
		}

		/**
		 * Reset the stream.
		 *
		 * @exception IOException
		 *							If an I/O error occurs
		 */
		public void reset() {
			throw new IOException("mark/reset not supported");
		}
	}
}
