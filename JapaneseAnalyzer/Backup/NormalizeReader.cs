/**
 * Copyright 2004 The Apache Software Foundation
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *		 http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

using System.IO;
using System.Text;

namespace Lucene.Net.Analysis.Ja
{

	/**
	 * NormalizeReader.java
	 *
	 * @author Kazuhiro Kazama
	 */
	public class NormalizeReader :/*extends*/ TextReader
	{
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

		TextReader rdr;
		/**
		 * Create a new normalization reader .
		 *
		 * @param in
		 *					The reader from which characters will be read.
		 */
		public NormalizeReader(TextReader _in)
		{
			//super(in);
			//this.in = _in;
			rdr = _in;
		}

		/**
		 * Read a single character.
		 *
		 * @exception IOException
		 *							If an I/O error occurs
		 */
		public override int Read()
		{
			return (int)convert((char)rdr.Read());
		}

		/**
		 * Read characters into a portion of an array.
		 *
		 * @exception IOException
		 *							If an I/O error occurs
		 */
		public override int Read(char[] cbuf, int off, int len)
		{
			int l = rdr.Read(cbuf, off, len);
			for(int i = off; i < off + len; i++)
				cbuf[i] = convert(cbuf[i]);
			return l;
		}

		public override string ReadToEnd()
		{
			string str = rdr.ReadToEnd();
			StringBuilder result = new StringBuilder(str.Length);

			for(int i = 0; i < str.Length; i++) {
				result.Append(convert(str[i]));
			}
			return result.ToString();
		}
		/**
		 * Convert HALFWIDTH_AND_FULLWIDTH_FORM characters.
		 */
		char convert(char c)
		{
			if((c >= 0xFF01 && c <= 0xFF5E) || (c >= 0xFF61 && c <= 0xFF9F)) {
				c = (char)CONVERSION_TABLE[c - 0xFF00];
			}
			return c;
		}

		/**
		 * Tell whether this stream supports the mark() operation.
		 *
		 */
		public bool markSupported()
		{
			return false;
		}

		/**
		 * Mark the present position in the stream.
		 *
		 * @exception IOException
		 *							If an I/O error occurs
		 */
		public void mark(int readAheadLimit)
		{
			throw new IOException("mark/reset not supported");
		}

		/**
		 * Reset the stream.
		 *
		 * @exception IOException
		 *							If an I/O error occurs
		 */
		public void reset()
		{
			throw new IOException("mark/reset not supported");
		}
	}
}
