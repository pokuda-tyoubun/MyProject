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
using System.Collections;
using Lucene.Net.Analysis;

namespace Lucene.Net.Analysis.Ja
{
	/**
	 * Filter tokens extracted with a Japanese tokenizer by their part of speech.
	 *
	 * @author Kazuhiro Kazama
	 */
	public class POSFilter : TokenFilter
	{
		/* Instance variables */
		Hashtable table;


		/**
		 * Construct a filter which removes unspecified pos from the input
		 * TokenStream.
		 */
		public POSFilter(TokenStream _in, string[] pos): base(_in)
		{
			//base(_in);
			input = _in;
			table = makePOSTable(pos);
		}

		/**
		 * Construct a filter which removes unspecified pos from the input
		 * TokenStream.
		 */
		public POSFilter(TokenStream _in, Hashtable posTable): base(_in)
		{
			//base(_in);
			input = _in;
			table = posTable;
		}

		/**
		 * Builds a hashtable from an array of pos.
		 */
		public static Hashtable makePOSTable(string[] pos)
		{
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
		public override Token Next()
		{
			Token t;
			while(true) {
				t = input.Next();
				if(t == null)
					return null;
				if(table.Contains(t.Type()))
					break;
			}
			return t;
		}
	}
}
