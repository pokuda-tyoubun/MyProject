using System;
using System.Collections.Generic;

using System.IO;

using ChasenForCS;

namespace Lucene.Net.Analysis.Ja
{
	public class PosInfo
	{
		public string token;
		public string tokenKana;
		public string token2;
		public string pos;
	}

	class ChasenTokenizer:Tokenizer
	{
		int position;

		/** word offset, used to imply which character(in ) is parsed */
		//private int offset = 0;

		/** the index used only for ioBuffer */
		//private int bufferIndex = 0;

		/** data length */
		//private int dataLen = 0;

		/// <summary>
		/// Construct a token stream processing the given input.
		/// </summary>
		/// <param name="_in">I/O reader</param>
		public ChasenTokenizer(TextReader _in)
		{
			input = _in;
			init();
		}

		private List <PosInfo> tokenList; // = new List<PosInfo>();
		private int tokenIndex = 0;

		//~ Methods ----------------------------------------------------------------
		private void init()
		{
			string sResult = "";
			string sPart = input.ReadToEnd();
			sResult += ChasenCS.parseStr(sPart);
			tokenList = DeCompose(sResult); // str);
			tokenIndex = 0;
		}

		/// <summary>
		///  Returns the next token in the stream, or null at EOS.
		/// </summary>
		/// <returns>Token</returns>
		public override Token Next()
		{
			if(tokenIndex == tokenList.Count) {
				return null;
			}
			string term = tokenList[tokenIndex].token;
			string reading = tokenList[tokenIndex].tokenKana;
			string original = tokenList[tokenIndex].token2;
			string type = tokenList[tokenIndex].pos;
			int start = position;
			position += term.Length;
			tokenIndex++;
			return new Token(term, start, position, type);

		}

		///-------------------------------------------------------------------------------------
		///
		private List<PosInfo> DeCompose(string chasenResult)
		{
			string[] ary = chasenResult.Split(new Char[] { '\n' });
			tokenList = new List<PosInfo>();
			foreach(string s in ary) {
				if((s.Length > 0) && (s != @"EOS")) {
					string[] pos = s.Split(new char[] { '\t' });
					PosInfo pi = new PosInfo();
					pi.token = pos[0];
					pi.tokenKana = pos[1];
					pi.token2 = pos[2];
					pi.pos = pos[3];
					tokenList.Add(pi);
				}
			}
			return tokenList;
		}

	}
}
