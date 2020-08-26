using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using FxCommonLib.Utils;
using System.Security.AccessControl;
using System.Diagnostics;

namespace FxCommonLib.Tests.Utils {
    [TestClass]
    public class FileUtilTest {

        private void InitTestDir() {
            //テストファイル置き場
            FileUtil.DeleteDirectory(@".\test");
            Directory.CreateDirectory(@".\test");
        }

        [TestMethod]
        public void DeleteOldestFileTest() {
            InitTestDir();

            //テストデータ作成
            File.Create(@".\test\file1.txt").Close();
            File.SetLastWriteTime(@".\test\file1.txt", new DateTime(2018, 3, 1));
            File.Create(@".\test\file2.txt").Close();
            File.SetLastWriteTime(@".\test\file2.txt", new DateTime(2018, 3, 2));
            File.Create(@".\test\file3.txt").Close();
            File.SetLastWriteTime(@".\test\file3.txt", new DateTime(2018, 3, 3));

            //テスト
            FileUtil.DeleteOldestFile(new DirectoryInfo(@".\test"), 1);

            //結果検証
            var di = new DirectoryInfo(@".\test");
            Assert.AreEqual(1, di.GetFiles().Length);
            Assert.AreEqual("file3.txt", di.GetFiles()[0].Name);
        }
        [TestMethod]
        public void DeleteOldFileTest() {
            InitTestDir();

            //テストデータ作成
            File.Create(@".\test\file1.txt").Close();
            File.SetLastWriteTime(@".\test\file1.txt", new DateTime(2018, 3, 1));
            File.Create(@".\test\file2.txt").Close();
            File.SetLastWriteTime(@".\test\file2.txt", new DateTime(2018, 3, 2));
            File.Create(@".\test\file3.txt").Close();
            File.SetLastWriteTime(@".\test\file3.txt", new DateTime(2018, 3, 3));

            //テスト
            DateTime border = new DateTime(2018, 3, 3);
            FileUtil.DeleteOldFile(new DirectoryInfo(@".\test"), border);

            //結果検証
            var di = new DirectoryInfo(@".\test");
            Assert.AreEqual(1, di.GetFiles().Length);
            Assert.AreEqual("file3.txt", di.GetFiles()[0].Name);
        }
        [TestMethod]
        public void DeleteDirectoryTest() {
            InitTestDir();

            //テストデータ作成
            Directory.CreateDirectory(@".\test\sub1");
            Directory.CreateDirectory(@".\test\sub2");
            File.Create(@".\test\sub1\file1.txt").Close();
            File.Create(@".\test\sub2\file2.txt").Close();
            File.Create(@".\test\sub2\file3.txt").Close();
            //file3.txtに読取専用属性を付与
            FileAttributes attr = File.GetAttributes(@".\test\sub2\file3.txt");
            File.SetAttributes(@".\test\sub2\file3.txt", attr | System.IO.FileAttributes.ReadOnly);

            //テスト
            FileUtil.DeleteDirectory(new DirectoryInfo(@".\test"));

            //結果検証
            Assert.AreEqual(false, Directory.Exists(@".\test"));
        }
        [TestMethod]
        public void DeleteDirectoryExceptOwnTest() {
            InitTestDir();

            //テストデータ作成
            Directory.CreateDirectory(@".\test\sub1");
            Directory.CreateDirectory(@".\test\sub2");
            File.Create(@".\test\sub1\file1.txt").Close();
            File.Create(@".\test\sub2\file2.txt").Close();
            File.Create(@".\test\sub2\file3.txt").Close();
            //file3.txtに読取専用属性を付与
            FileAttributes attr = File.GetAttributes(@".\test\sub2\file3.txt");
            File.SetAttributes(@".\test\sub2\file3.txt", attr | System.IO.FileAttributes.ReadOnly);

            //テスト
            FileUtil.DeleteDirectoryExceptOwn(@".\test");

            //結果検証
            Assert.AreEqual(true, Directory.Exists(@".\test"));
            var di = new DirectoryInfo(@".\test");
            Assert.AreEqual(0, di.GetFiles().Length);
        }
        [TestMethod]
        public void IsFileLockedTest() {
            InitTestDir();

            //テストデータ作成
            File.Create(@".\test\file1.txt").Close();
            using (FileStream fs = new FileStream( @".\test\file1.txt", FileMode.Open, FileAccess.Read, FileShare.None)) {
                //結果検証
                Assert.AreEqual(true, FileUtil.IsFileLocked(@".\test\file1.txt"));
            }
            //結果検証
            Assert.AreEqual(false, FileUtil.IsFileLocked(@".\test\file1.txt"));

        }
        [TestMethod]
        public void GetSizeStringTest() {
            Assert.AreEqual("1,000B", FileUtil.GetSizeString("1000"));
            Assert.AreEqual("1KB", FileUtil.GetSizeString("1024"));
            Assert.AreEqual("976.56KB", FileUtil.GetSizeString("1000000"));
            Assert.AreEqual("1MB", FileUtil.GetSizeString("1048576"));
            Assert.AreEqual("1GB", FileUtil.GetSizeString("1073741824"));
            Assert.AreEqual("1.52GB", FileUtil.GetSizeString("1640460288"));
        }
        [TestMethod]
        public void AddFullControleRuleTest() {
            InitTestDir();

            //テストデータ作成
            File.Create(@".\test\file1.txt").Close();
            var dic = FileUtil.GetFileSystemAccessRuleDic(@".\test\file1.txt");
            Assert.AreEqual(false, dic.ContainsKey("Everyone"));

            //テスト
            FileUtil.AddFullControleRule(@".\test\file1.txt");

            //結果検証
            dic = FileUtil.GetFileSystemAccessRuleDic(@".\test\file1.txt");
            Assert.AreEqual(true, dic.ContainsKey("Everyone"));
            Assert.AreEqual("FullControl", dic["Everyone"].ToString());
        }
        [TestMethod]
        public void DeleteFileTest() {
            InitTestDir();

            //テストデータ作成
            File.Create(@".\test\file1.txt").Close();
            //file1.txtに読取専用属性を付与
            FileAttributes attr = File.GetAttributes(@".\test\file1.txt");
            File.SetAttributes(@".\test\file1.txt", attr | System.IO.FileAttributes.ReadOnly);

            //テスト
            FileUtil.DeleteFile(@".\test\file1.txt");

            //結果検証
            var di = new DirectoryInfo(@".\test");
            Assert.AreEqual(0, di.GetFiles().Length);
        }
        [TestMethod]
        public void CopyDirectoryTest() {
            InitTestDir();

            //テストデータ作成
            Directory.CreateDirectory(@".\test\sub1");
            Directory.CreateDirectory(@".\test\sub2");
            File.Create(@".\test\sub1\file1.txt").Close();
            File.Create(@".\test\sub2\file2.txt").Close();
            File.Create(@".\test\sub2\file3.txt").Close();

            //テスト
            FileUtil.CopyDirectory(@".\test", @".\test2");
            //結果検証
            Assert.AreEqual(true, FileUtil.CompareDirectory(@".\test", @".\test2"));
        }
        [TestMethod]
        public void CompareDirectoryTest() {
            InitTestDir();

            //テストデータ作成
            Directory.CreateDirectory(@".\test\sub1");
            Directory.CreateDirectory(@".\test\sub2");
            File.Create(@".\test\sub1\file1.txt").Close();
            File.Create(@".\test\sub2\file2.txt").Close();
            File.Create(@".\test\sub2\file3.txt").Close();

            //テスト
            FileUtil.CopyDirectory(@".\test", @".\test2");
            //結果検証
            Assert.AreEqual(true, FileUtil.CompareDirectory(@".\test", @".\test2"));

            //ファイル移動
            File.Move(@".\test\sub2\file3.txt", @".\test\sub1\file3.txt");
            //結果検証
            Assert.AreEqual(false, FileUtil.CompareDirectory(@".\test", @".\test2"));

            //ファイルを戻す
            File.Move(@".\test\sub1\file3.txt", @".\test\sub2\file3.txt");
            //テスト
            FileUtil.CopyDirectory(@".\test", @".\test2");
            //結果検証
            Assert.AreEqual(true, FileUtil.CompareDirectory(@".\test", @".\test2"));

            //単独ディレクトリ作成
            Directory.CreateDirectory(@".\test\sub3");
            //結果検証
            Assert.AreEqual(false, FileUtil.CompareDirectory(@".\test", @".\test2"));
        }
        [TestMethod]
        public void AppendFileNameSuffixTest() {
            InitTestDir();

            //テストデータ作成
            File.Create(@".\test\file1.txt").Close();

            var fi = new FileInfo(@".\test\file1.txt");
            //結果検証
            Assert.AreEqual(@"file1hoge.txt", FileUtil.AppendFileNameSuffix(fi, "hoge"));
        }
        [TestMethod]
        public void IsDirectoryTest() {
            InitTestDir();

            //テストデータ作成
            File.Create(@".\test\file1.txt").Close();

            //結果検証
            Assert.AreEqual(false, FileUtil.IsDirectory(@".\test\file1.txt"));
            Assert.AreEqual(true, FileUtil.IsDirectory(@".\test"));
        }

        [TestCleanup]
        public void TestCleanup() {
            FileUtil.DeleteDirectory(@".\test");
            FileUtil.DeleteDirectory(@".\test2");
        }
        //HACK FileUtil系メソッドのstatic化を検討
    }
}
