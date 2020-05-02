using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FlexLucene.Queryparser.Surround.Parser;
using FlexLucene.Queryparser.Classic;
using FlexLucene.Analysis.Ja;
using System.IO;
using PokudaSearch.IndexUtil;
using FlexLucene.Analysis.Ja.Dict;
using FlexLucene.Search;
using java.nio.file;
using FlexLucene.Store;
using FlexLucene.Index;
using System.Diagnostics;
using FlexLucene.Document;

namespace PokudaSearch.Test.Views {
    //TODO 小堀のテストメソッドの作成の仕方を参考にする
    [TestClass]
    public class SandBoxTest {
        [TestMethod]
        public void 対象ファイル0の場合Test() {
            //FIXME
        }
        [TestMethod]
        public void インデックス作成に失敗した場合Test() {
            //FIXME
        }
    }
}
