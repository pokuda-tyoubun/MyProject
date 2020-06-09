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
using Microsoft.WindowsAPICodePack.Shell;
using Microsoft.WindowsAPICodePack.Shell.PropertySystem;
using PokudaSearch.Test.TaskCompletionSource;

namespace PokudaSearch.Test {
    [TestClass]
    public class SandBoxTest {
        [TestMethod]
        public void FilePropertiesTest() {
            var file = ShellFile.FromFilePath(@"C:\Temp\test.jpg");
            //拡張プロパティ取得
            Console.WriteLine(file.Properties.System.Title.Value);
            Console.WriteLine(file.Properties.System.Author.Value);
            Console.WriteLine(file.Properties.System.Comment.Value);

            //拡張プロパティセット
            ShellPropertyWriter propertyWriter =  file.Properties.GetPropertyWriter();
            propertyWriter.WriteProperty(SystemProperties.System.Title, new string[] { "タイトル" });
            propertyWriter.WriteProperty(SystemProperties.System.Author, new string[] { "著者" });
            propertyWriter.WriteProperty(SystemProperties.System.Comment, new string[] { "コメント" });
            propertyWriter.Close();
        }
        [TestMethod]
        public void PathReplaceTest() {
            string path1 = @"S:\bin\DB\test.db";
            string path2 = @"C:\Temp\bin\IndexStore\Index1";

            string tmp1 = path1.Substring(0, path1.IndexOf(@"bin\DB"));
            string tmp2 = path2.Substring(path2.IndexOf(@"bin\IndexStore"));
            Assert.AreEqual(tmp1 + tmp2, @"S:\bin\IndexStore\Index1");
        }


        [TestMethod]
        public void TaskCompletionSourceTest() {

            var p = new Program();
            p.RunAsync();

            //ステータス変更
            //p.CurrentStatus = Program.Status.Successful;

            var task = p.CompletionSource.Task;
            try {
                task.Wait();
            } catch { }
            Console.WriteLine(task.Status);

        }
    }
}
