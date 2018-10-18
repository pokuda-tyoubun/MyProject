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
using System.Collections.Generic;

using System.IO;

using MecabDotNet;

namespace Lucene.Net.Analysis.Ja
{
	public class MecabPosInfo
	{
		public string token;
		public string tokenKana;
		public string token2;
		public string pos;
	}

	/**
	 * This is a Japanese tokenizer which uses "Sen" morphological
	 * analyzer.
	 *
	 * @author Takashi Okamoto
	 * @author Kazuhiro Kazama
	 */
	class MeCabTokenizer : Tokenizer
	{
		//private StreamTagger tagger = null;
		Mecab mecab;
		private int position = 0;
		private List<MecabPosInfo> list;
		private int tokenIndex = 0;

		public MeCabTokenizer(TextReader _in)
		{
			input = _in;
			mecab = new Mecab("-Ochasen");
			init();
            mecab.mecab_destroy(); // ÉÅÉÇÉäÅ[Çâï˙Ç∑ÇÈ
		}

		private void init()
		{
			string sPart = input.ReadToEnd();
			string result;
			result = mecab.mecab_sparse_tostr(sPart);
			list = DeCompose(result);
			tokenIndex = 0;
		}

		///-------------------------------------------------------------------------------------
		///
		private List<MecabPosInfo> DeCompose(string result)
		{
			string[] ary = result.Split(new Char[] { '\n' });
			list = new List<MecabPosInfo>();
			foreach(string s in ary) {
				if((s.Length > 0) && (s != @"EOS")) {
					string[] pos = s.Split(new char[] { '\t',',' });
					if(pos.GetLength(0) > 3) {
						MecabPosInfo pi = new MecabPosInfo();
						// chasen format
						pi.token = pos[0];
						pi.tokenKana = pos[1];
						pi.token2 = pos[2];
						pi.pos = pos[3];

						//
						/**
						pi.token = pos[0];
						pi.tokenKana = pos[3];
						pi.token2 = pos[3];
						pi.pos = pos[1];
						**/
						list.Add(pi);
					} else {
						Console.WriteLine("??");
					}
				}
			}
			return list;
		}

		public override Token Next()
		{
			if(tokenIndex == list.Count) {
				return null;
			}
			string term = list[tokenIndex].token;
			string reading = list[tokenIndex].tokenKana;
			string original = list[tokenIndex].token2;
			string type = list[tokenIndex].pos;
			int start = position;
			position += term.Length;
			tokenIndex++;
			return new Token(term, start, position, type);
		}

		public void close()
		{
			// TODO Auto-generated method stub
			base.Close();
		}
	}
}
