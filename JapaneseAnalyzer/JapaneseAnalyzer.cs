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

using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Xml;

namespace Lucene.Net.Analysis.Ja
{
	/**
 * Filters a Japanese tokenizer with a {@link POSfilter},{@link
 * LowerCaseFilter} and {@link StopFilter}.
 *
 * @author Kazuhiro Kazama
 * @author Takashi Okamoto
 */


	public class JapaneseAnalyzer : Analyzer
	{
		/**
	* An array containing some common English words that are not usually
	* useful for searching. and some double-byte interpunctions.....
	*/

		//~ Instance fields --------------------------------------------------------

		/** stop word list */
		private List<string> stopTable;
		private Hashtable posTable;
		private string tokenizerClass;

		/// <summary>
		/// Builds an analyzer which removes words in STOP_WORDS.
		/// </summary>

		public JapaneseAnalyzer(string configName)
		{
			init(configName);
		}

		/// <summary>
		/// Builds an analyzer which removes words in the provided array.
		/// </summary>
		/// <param name="stopWords">stop word array</param>
		//		public JapaneseAnalyzer(String[] stopWords)
		//		{
		//			init();
		//		}
		private string[] stopWords; //  = new string[stopTable.Count];

		///-------------------------------------------------------------------------------------
		private void init(string configName)
		{
			stopTable = new List<string>();
			posTable = new Hashtable();

			XmlDocument xmlDoc = new XmlDataDocument();

			int count = 0;

			XmlNodeList nodeList;

			xmlDoc.Load(configName);

			nodeList = xmlDoc.SelectNodes("/analyzer/tokenizerClass");
			foreach(XmlNode nd in nodeList) {
				tokenizerClass = nd.InnerText;
			}

			stopTable.Clear();
			nodeList = xmlDoc.SelectNodes("/analyzer/stop/word");
			foreach(XmlNode nd in nodeList) {
				stopTable.Add(nd.InnerText);
			}
			nodeList = xmlDoc.SelectNodes("/analyzer/stop/letters");
			foreach(XmlNode nd in nodeList) {
				for(int i = 0; i < nd.InnerText.Length; i++) {
					stopTable.Add(nd.InnerText[i].ToString()); // count++);
				}
			}

			stopWords = new string[stopTable.Count];
			int x = 0;
			foreach(string s in stopTable) {
				stopWords[x++] = s;
			}

			posTable.Clear();
			nodeList = xmlDoc.SelectNodes(@"/analyzer/accept/pos");
			foreach(XmlNode nd in nodeList) {
				posTable.Add(nd.InnerText, count++);
			}

		}

		private object createInstance(string name, TextReader reader)
		{
			System.Reflection.Assembly assembly =
			 System.Reflection.Assembly.GetExecutingAssembly();

			object inst = assembly.CreateInstance(
				name
				, false
				, System.Reflection.BindingFlags.CreateInstance
				, null
				, new object[] { reader }
				, null
				, null);

			return inst;

		}
		/// <summary>
		/// get token stream from input
		/// </summary>
		/// <param name="fieldName">lucene field name</param>
		/// <param name="reader">input reader</param>
		/// <returns>Token Stream</returns>
		public override sealed TokenStream TokenStream(String fieldName, TextReader reader)
		{
			TokenStream result;

			reader = (TextReader)new NormalizeReader(reader);


			//result = new ChasenTokenizer(reader);
			result = (TokenStream)createInstance(tokenizerClass, reader);

			result = new POSFilter(result, posTable);
			// result = new DigitFilter(result);
			result = new LowerCaseFilter(result);
			result = new KatakanaStemFilter(result);

			result = new StopFilter(result, stopWords); // Table);
			return result;
		}
	}
}
