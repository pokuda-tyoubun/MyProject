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
using System.Text.RegularExpressions;


namespace Lucene.Net.Analysis.Ja
{

	/**
	 * Convert a katakana word to a normalized form by stemming KATAKANA-HIRAGANA
	 * PROLONGED SOUND MARK (U+30FC) which exists at the last of the string. In
	 * general, most of Japanese full-text search engine uses more complicated
	 * method which needs dictionaries. I think they are better than this filter in
	 * quality, but they needs a well-tuned dictionary. In contract, this filter is
	 * simple and maintenance-free.
	 *
	 * Note: This filter don't supports hankaku katakana characters, so you must
	 * convert them before using this filter. And this filter support only
	 * pre-composed characters.
	 *
	 * @author Kazuhiro Kazama
	 */
	public class KatakanaStemFilter : TokenFilter
	{
		//static char COMBINING_KATAKANA_HIRAGANA_VOICED_SOUND_MARK = '\u3099';
		//static char COMBINING_KATAKANA_HIRAGANA_SEMI_VOICED_SOUND_MARK = '\u309A';
		//static char KATAKANA_HIRAGANA_VOICED_SOUND_MARK = '\u309B';
		//static char KATAKANA_HIRAGANA_SEMI_VOICED_SOUND_MARK = '\u309C';
		static char KATAKANA_HIRAGANA_PROLONGED_SOUND_MARK = '\u30FC';

		public KatakanaStemFilter(TokenStream _in)
			: base(_in)
		{
			//super(in);
			input = _in;
		}

		/**
		 * Returns the next input Token, after being stemmed
		 */
		public override Token Next()
		{
			Token token = input.Next();
			if(token == null)
				return null;
			string s = token.TermText();

			int len = s.Length;
			if(len > 3 && s[len - 1] == KATAKANA_HIRAGANA_PROLONGED_SOUND_MARK && isKatakanaString(s)) {
				token = new Token(s.Substring(0, len - 1), token.StartOffset(), token.EndOffset(), token.Type());
			}

			return token;
		}

		private bool isKatakanaString(string s)
		{
			/// TODO:
			//string rslt = Strings.StrConv(s, VbStrConv.Katakana,0);
			return Regex.IsMatch(s, @"^\p{IsKatakana}*$");
			/**
			for (int i = 0; i < s.Length; i++) {
				char c = s[i];
				if (Character.UnicodeBlock.of(c) != Character.UnicodeBlock.KATAKANA
						&& c != COMBINING_KATAKANA_HIRAGANA_VOICED_SOUND_MARK
						&& c != COMBINING_KATAKANA_HIRAGANA_SEMI_VOICED_SOUND_MARK
						&& c != KATAKANA_HIRAGANA_VOICED_SOUND_MARK
						&& c != KATAKANA_HIRAGANA_SEMI_VOICED_SOUND_MARK)
					return false;
			}
			return true;
			 * */
			//return false;
		}
	}
}
