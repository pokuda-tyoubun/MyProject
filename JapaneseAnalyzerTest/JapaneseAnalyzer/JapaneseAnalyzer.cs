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
    public class JapaneseAnalyzer : Analyzer {

        /// <summary>除外文字リスト</summary>
		private List<string> stopTable;
		private Hashtable posTable;
		private string tokenizerClass;

		private string[] stopWords; //  = new string[stopTable.Count];

        public JapaneseAnalyzer(string configName) {
            init(configName);
        }


        /// <summary>
        /// analyzer-mecab.xmlを読み込んで初期化処理を実施
        /// </summary>
        /// <param name="configName"></param>
		private void init(string configName) {
			stopTable = new List<string>();
			posTable = new Hashtable();

			XmlDocument xmlDoc = new XmlDataDocument();

			int count = 0;

			XmlNodeList nodeList;

			xmlDoc.Load(configName);

            //mecab.xmlに指定されているTokenizerを取得
			nodeList = xmlDoc.SelectNodes("/analyzer/tokenizerClass");
			foreach(XmlNode nd in nodeList) {
				tokenizerClass = nd.InnerText;
			}

			stopTable.Clear();
            //除外文字を取得
			nodeList = xmlDoc.SelectNodes("/analyzer/stop/word");
			foreach(XmlNode nd in nodeList) {
				stopTable.Add(nd.InnerText);
			}
            //除外記号を取得
			nodeList = xmlDoc.SelectNodes("/analyzer/stop/letters");
			foreach(XmlNode nd in nodeList) {
				for(int i = 0; i < nd.InnerText.Length; i++) {
					stopTable.Add(nd.InnerText[i].ToString()); // count++);
				}
			}

            //リストから配列へ
			stopWords = new string[stopTable.Count];
			int x = 0;
			foreach(string s in stopTable) {
				stopWords[x++] = s;
			}

            //品詞の種類のテーブルを作成
            //HACK : NMeCab利用では必要ないかも
			posTable.Clear();
			nodeList = xmlDoc.SelectNodes(@"/analyzer/accept/pos");
			foreach(XmlNode nd in nodeList) {
				posTable.Add(nd.InnerText, count++);
			}
		}

        /// <summary>
        /// 文字列で指定したクラスをインスタンス化
        /// </summary>
        /// <param name="name"></param>
        /// <param name="reader"></param>
        /// <returns></returns>
		private object createInstance(string name, TextReader reader) {
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
		/// 入力値からTokenStreamを取得
		/// </summary>
		/// <param name="fieldName">luceneのfieldName</param>
		/// <param name="reader">input reader</param>
		/// <returns>Token Stream</returns>
		public override sealed TokenStream TokenStream(String fieldName, TextReader reader) {
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
