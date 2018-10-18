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
using System;
// import java.io.IOException;
using System.Text;

namespace Lucene.Net.Analysis.Ja
{
	/**
	 * Merge digits extracted with a Japanese tokenizer.
	 *
	 * @author Kazuhiro Kazama
	 */
	public class DigitFilter : TokenFilter
	{

		/* Instance variables */
		bool preRead;

		Token preReadToken;

		/**
		 * Construct filtering <i>in </i>.
		 */

		public DigitFilter(TokenStream _in)
			: base(_in)
		{
			//super(in);
			preRead = false;
			preReadToken = null;
			input = _in;
		}

		/**
		 * Returns the next token in the stream, or null at EOS.
		 * Merge consecutive digits.
		 */
		public override Token Next()
		{
			if(preRead) {
				preRead = false;
				return preReadToken;
			}
			Token t = input.Next();
			if(t == null)
				return null;
			string term = t.TermText();
			if(term.Length == 1 && Char.IsDigit(term[0])) {
				int start = t.StartOffset();
				int end = t.EndOffset();
				string type = t.Type();
				StringBuilder st = new StringBuilder();
				st.Append(t.TermText());
				while(true) {
					t = input.Next();
					if(t == null
							|| (t.TermText().Length != 1 || !Char.IsDigit(t.TermText()[0]))) {
						preRead = true;
						preReadToken = t;
						return new Token(st.ToString(), start, end, type);
					}
					st.Append(t.TermText());
					end = t.EndOffset();
				}
			}
			return t;
		}
	}
}
