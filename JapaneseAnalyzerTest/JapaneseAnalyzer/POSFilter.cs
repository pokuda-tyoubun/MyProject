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

	public class POSFilter : TokenFilter {

		Hashtable _table;

		/**
		 * Construct a filter which removes unspecified pos from the input
		 * TokenStream.
		 */
		public POSFilter(TokenStream inputStream, string[] pos): base(inputStream) {
			base.input = inputStream;
			_table = makePOSTable(pos);
		}

		/**
		 * Construct a filter which removes unspecified pos from the input
		 * TokenStream.
		 */
		public POSFilter(TokenStream inputStream, Hashtable posTable): base(inputStream) {
			base.input = inputStream;
			_table = posTable;
		}

		/**
		 * 配列からハッシュテーブルへ変換
		 */
		public static Hashtable makePOSTable(string[] pos) {
			Hashtable posTable = new Hashtable(pos.Length);
			for(int i = 0; i < pos.Length; i++)
				posTable.Add(pos[i], pos[i]);
			return posTable;
		}

		/**
		 * Returns the next token in the stream, or null at EOS.
		 * <p>
		 * Removes a specified part of speech.
		 */
		public override Token Next() {
			Token t;
			while(true) {
				t = base.input.n
                if (t == null) {
					return null;
                }
                if (_table.Contains(t.Type)) {
					break;
                }
			}
			return t;
		}
	}
}
