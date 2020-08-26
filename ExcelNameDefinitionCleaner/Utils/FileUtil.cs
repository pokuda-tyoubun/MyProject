using System;
using System.Collections.Generic;
using System.IO;
using System.Security.AccessControl;
using System.Security.Principal;
using System.Windows.Forms;
using System.Linq;
using System.Text;

namespace ExcelNameDefinitionCleaner.Utils {
    /// <summary>
    /// File操作ユーティリティ
    /// </summary>
    public class FileUtil {

        #region PublicMethods
        /// <summary>
        /// 引数で指定したファイル数分を残して古いファイルから順に削除
        /// </summary>
        /// <param name="dir">処理対象ディレクトリ</param>
        /// <param name="remainingCount">残すファイル数</param>
        public static void DeleteOldestFile(DirectoryInfo dir, int remainingCount) {
            List<FileInfo> fileList = new List<FileInfo>(dir.GetFiles());
            fileList.Sort(delegate(FileInfo x, FileInfo y) { return y.LastWriteTime.CompareTo(x.LastWriteTime); });

            for (int i = 0; i < fileList.Count; i++ ) {
                if (i > remainingCount - 1) {
                    fileList[i].Delete();
                }
            }
        }

        /// <summary>
        /// 引数で指定した日付より古いファイルを削除
        /// </summary>
        /// <param name="dir">処理対象ディレクトリ</param>
        /// <param name="borderDate">閾となる日付</param>
        public static void DeleteOldFile(DirectoryInfo dir, DateTime borderDate) {
            //ディレクトリの削除
            List<DirectoryInfo> dirList = new List<DirectoryInfo>(dir.GetDirectories());
            for (int i = 0; i < dirList.Count; i++ ) {
                if (dirList[i].LastWriteTime < borderDate) {
                    DeleteDirectory(dirList[i]);
                }
            }
            //ファイルの削除
            List<FileInfo> fileList = new List<FileInfo>(dir.GetFiles());
            for (int i = 0; i < fileList.Count; i++ ) {
                if (fileList[i].LastWriteTime < borderDate) {
                    fileList[i].Delete();
                }
            }
        }
        /// <summary>
        /// 再帰的にディレクトリを削除
        /// </summary>
        /// <param name="path">処理対象パス</param>
        public static void DeleteDirectory(string path) {
            if (Directory.Exists(path)) {
                DeleteDirectory(new DirectoryInfo(path));
            }
        }
        /// <summary>
        /// 再帰的にディレクトリを削除
        /// </summary>
        /// <param name="dirInfo">処理対象ディレクトリ</param>
        public static void DeleteDirectory(DirectoryInfo dirInfo) {
            // すべてのファイルの読み取り専用属性を解除する
            foreach (FileInfo fileInfo in dirInfo.GetFiles()) {
                if ((fileInfo.Attributes & FileAttributes.ReadOnly) == FileAttributes.ReadOnly) {
                    fileInfo.Attributes = FileAttributes.Normal;
                }
            }

            // サブディレクトリ内の読み取り専用属性を解除する (再帰)
            foreach (DirectoryInfo tmpDirInfo in dirInfo.GetDirectories()) {
                DeleteDirectory(tmpDirInfo);
            }

            // このディレクトリの読み取り専用属性を解除する
            if ((dirInfo.Attributes & FileAttributes.ReadOnly) == FileAttributes.ReadOnly) {
                dirInfo.Attributes = FileAttributes.Directory;
            }
            // このディレクトリを削除する
            dirInfo.Delete(true);
        }
        /// <summary>
        /// 再帰的にディレクトリを削除(指定したディレクトリは残す)
        /// </summary>
        /// <param name="path">処理対象パス</param>
        public static void DeleteDirectoryExceptOwn(string path) {
            if (Directory.Exists(path)) {
                foreach (DirectoryInfo tmpDirInfo in new DirectoryInfo(path).GetDirectories()) {
                    DeleteDirectory(tmpDirInfo);
                }
            }
        }

        /// <summary>
        /// ファイルが開かれてロックされているかどうか
        /// </summary>
        /// <param name="path">ファイルへのパス</param>
        /// <returns>true:ロックされている／false:されていない</returns>
        public static bool IsFileLocked(string path) {
            FileStream stream = null;
            if (!File.Exists(path)) {
                return false;
            }
            try {
                stream = new FileStream(path, FileMode.Open, FileAccess.ReadWrite, FileShare.None);
            } catch {
                return true;
            }
            finally {
                if (stream != null) {
                    stream.Close();
                }
            }
         
            return false;
        }

        /// <summary>
        /// ファイルサイズ用文字列に変換します。
        /// </summary>
        /// <param name="size">バイト単位のサイズ</param>
        /// <returns>KB,MB単位に変換された文字列</returns>
        public static string GetSizeString(string byteSize) {
            string ret = "";
            if (byteSize != "") {
                long size = long.Parse(byteSize);
                ret = GetSizeString(size);
            }

            return ret;
        }
        /// <summary>
        /// ファイルサイズ用文字列に変換します。
        /// </summary>
        /// <param name="byteSize"></param>
        /// <returns></returns>
        public static string GetSizeString(long byteSize) {
            string ret = "";
            if (byteSize < 1024) {
                //KB未満
                ret = byteSize.ToString("#,##0") + "B";
            } else if (byteSize < 1048576) {
                //KB
                ret = Math.Round(((double)byteSize / 1024), 3, MidpointRounding.AwayFromZero).ToString("#,##0.##") + "KB";
            } else if (byteSize < 1073741824) {
                //MB
                ret = Math.Round(((double)byteSize / 1048576), 3, MidpointRounding.AwayFromZero).ToString("#,##0.##") + "MB";
            } else {
                //GB以上
                ret = Math.Round(((double)byteSize / 1073741824), 3, MidpointRounding.AwayFromZero).ToString("#,##0.##") + "GB";
            }

            return ret;
        }

        /// <summary>
        /// Everyoneフルコントロール権限を付与
        /// </summary>
        /// <param name="filePath">処理対象パス</param>
        public static void AddFullControleRule(string filePath) {
            //EveryOneFullControle
            var rule = new FileSystemAccessRule("Everyone", FileSystemRights.FullControl, 
                InheritanceFlags.None, PropagationFlags.None, AccessControlType.Allow);

            //ファイルセキュリティオブジェクトを取得
            FileSecurity security = File.GetAccessControl(filePath);
            //権限付与
            security.AddAccessRule(rule);
            //変更したファイルセキュリティをファイルに設定
            File.SetAccessControl(filePath, security);
        }
        /// <summary>
        /// ファイル強制削除
        /// HACK 引数で強制削除を選択できるようにする。
        /// </summary>
        /// <param name="filePath">処理対象パス</param>
        public static void DeleteFile(string filePath) {
            FileInfo fi = new FileInfo(filePath);

            // ファイルが存在しているか判断する
            if (fi.Exists) {
                // 読み取り専用属性がある場合は、読み取り専用属性を解除する
                if ((fi.Attributes & FileAttributes.ReadOnly) == FileAttributes.ReadOnly) {
                    fi.Attributes = FileAttributes.Normal;
                }

                // ファイルを削除する
                fi.Delete();
            }
        }

        /// <summary>
        /// ディレクトリ構造を再帰的にコピー
        /// </summary>
        /// <param name="sourceDirName">コピー元ディレクトリ</param>
        /// <param name="destDirName">コピー先ディレクトリ</param>
        public static void CopyDirectory(string sourceDirName, string destDirName) {
            //コピー先のディレクトリがないときは作る
            if (!Directory.Exists(destDirName)) {
                Directory.CreateDirectory(destDirName);
                //属性もコピー
                File.SetAttributes(destDirName, File.GetAttributes(sourceDirName));
            }

            //コピー先のディレクトリ名の末尾に"\"をつける
            if (destDirName[destDirName.Length - 1] != Path.DirectorySeparatorChar) {
                destDirName = destDirName + Path.DirectorySeparatorChar;
            }

            //コピー元のディレクトリにあるファイルをコピー
            string[] files = Directory.GetFiles(sourceDirName);
            foreach (string file in files) {
                File.Copy(file, destDirName + Path.GetFileName(file), true);
            }

            //コピー元のディレクトリにあるディレクトリについて、再帰的に呼び出す
            string[] dirs = Directory.GetDirectories(sourceDirName);
            foreach (string dir in dirs) {
                CopyDirectory(dir, destDirName + Path.GetFileName(dir));
            }
        }

        /// <summary>
        /// ディレクトリ構造が同じか比較
        /// </summary>
        /// <param name="dir1">対象ディレクトリ1</param>
        /// <param name="dir2">対象ディレクトリ2</param>
        /// <returns>true:同じ／false:異なる</returns>
        public static bool CompareDirectory(string dir1, string dir2) {
            var di1 = new DirectoryInfo(dir1);
            var di2 = new DirectoryInfo(dir2);

            //保有ファイルでの比較
            IEnumerable<FileInfo> list1 = di1.GetFiles("*.*", SearchOption.AllDirectories);  
            IEnumerable<FileInfo> list2 = di2.GetFiles("*.*", SearchOption.AllDirectories);
            var fc = new FileCompare(di1.FullName, di2.FullName);
            if (!list1.SequenceEqual(list2, fc)) {
                return false;
            }

            //保有ディレクトリでの比較
            IEnumerable<DirectoryInfo> dirList1 = di1.GetDirectories("*.*", System.IO.SearchOption.AllDirectories);  
            IEnumerable<DirectoryInfo> dirList2 = di2.GetDirectories("*.*", System.IO.SearchOption.AllDirectories);
            var dc = new DirectoryCompare(di1.FullName, di2.FullName);
            if (!dirList1.SequenceEqual(dirList2, dc)) {
                return false;
            }

            return true;
        }

        /// <summary>
        /// ファイル名にSuffixを追加
        /// </summary>
        /// <param name="orgFileInfo">処理対象ファイル</param>
        /// <param name="suffix">付与したいSuffix</param>
        /// <returns>Suffix追加済みファイル名</returns>
        public static string AppendFileNameSuffix(FileInfo orgFileInfo, string suffix) {
            string withoutExtensionName = orgFileInfo.Name.Replace(orgFileInfo.Extension, "");
            return withoutExtensionName + suffix + orgFileInfo.Extension;
        }

        /// <summary>
        /// フォルダかどうか判定
        /// </summary>
        /// <param name="path">処理対象パス</param>
        /// <returns>true:ディレクトリ／false:ファイル</returns>
        public static bool IsDirectory(string path) {
            if (File.Exists(path) == true) {
                return false;
            } else if (Directory.Exists(path) == true) {
                return true;
            } else {
                throw new FileNotFoundException();
            }
        }
        /// <summary>
        /// ファイル名に使用できない文字が使われていないか判定
        /// </summary>
        /// <param name="fileName">ファイル名</param>
        /// <returns>true:OK／false:NG</returns>
        public static bool IsVaildFileName(string fileName) {

            //ファイル名に使用できない文字を取得
            char[] invalidChars = System.IO.Path.GetInvalidFileNameChars();

            return (fileName.IndexOfAny(invalidChars) < 0);

        }
        /// <summary>
        /// ファイルアクセスルールを辞書として取得
        /// </summary>
        /// <param name="filePath">処理対象パス</param>
        /// <returns>ファイルアクセスルール辞書</returns>
        public static Dictionary<string, FileSystemRights> GetFileSystemAccessRuleDic(string filePath) {
            var ret = new Dictionary<string, FileSystemRights>();
            FileSecurity security = File.GetAccessControl(filePath);
            AuthorizationRuleCollection arc = security.GetAccessRules(true, true, typeof(System.Security.Principal.NTAccount));
            foreach (FileSystemAccessRule ar in arc) {
                ret.Add(ar.IdentityReference.Value, ar.FileSystemRights);
            }
            return ret;
        }
        /// <summary>
        /// ファイルアップロード選択ダイアログを表示
        /// </summary>
        /// <param name="titleMsg">ダイアログのタイトル</param>
        /// <param name="tb">パスをセットするTextBox</param>
        public void SetUploadFile(string titleMsg, TextBox tb) {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = titleMsg;
            if (ofd.ShowDialog() == DialogResult.OK) {
                tb.Text = ofd.FileName;
            }
        }
        /// <summary>
        /// 初期ディレクトリを指定してファイル選択
        /// </summary>
        /// <param name="titleMsg">ダイアログのタイトル</param>
        /// <param name="selectedPath">パスをセットするTextBox</param>
        public string GetSelectedFileFullPath(string titleMsg, string selectedPath) {

            string filePath = string.Empty;
            using (OpenFileDialog openFileDialog = new OpenFileDialog()) {
                openFileDialog.Title = titleMsg;
                openFileDialog.InitialDirectory = selectedPath;

                if (openFileDialog.ShowDialog() == DialogResult.OK) {
                    //Get the path of specified file
                    filePath = openFileDialog.FileName;

                }
            }
            return filePath;
        }
        /// <summary>
        /// ディレクトリ選択ダイアログから選択したパスを取得
        /// </summary>
        /// <param name="titleMsg">ダイアログのタイトル</param>
        /// <param name="selectedPath">初期選択フォルダ</param>
        /// <returns>選択したディレクトリパス</returns>
        public static string GetSelectedDirectory(string titleMsg, string selectedPath) {
            return GetSelectedDirectory(titleMsg, Environment.SpecialFolder.Desktop, selectedPath, true);
        }
        /// <summary>
        /// ディレクトリ選択ダイアログから選択したパスを取得
        /// </summary>
        /// <param name="titleMsg">ダイアログのタイトル</param>
        /// <param name="rootFolder">ルートフォルダ</param>
        /// <param name="selectedPath">初期選択フォルダ</param>
        /// <param name="showNewFolderButton">ユーザーが新しいフォルダを作成できる</param>
        /// <returns>選択したディレクトリパス</returns>
        public static string GetSelectedDirectory(string titleMsg, System.Environment.SpecialFolder rootFolder, string selectedPath, bool showNewFolderButton) {

            string retSelectedDirectory = "";

            //FolderBrowserDialogクラスのインスタンスを作成
            FolderBrowserDialog fbd = new FolderBrowserDialog();

            try {

                //上部に表示する説明テキスト
                fbd.Description = titleMsg;

                //ルートフォルダ
                fbd.RootFolder = rootFolder;
            
                //最初に選択するフォルダを指定する
                //RootFolder以下にあるフォルダである必要がある
                //fbd.SelectedPath = @"C:\Windows";

                if (!String.IsNullOrEmpty(selectedPath)) {
                    fbd.SelectedPath = selectedPath;
                }

                //ユーザーが新しいフォルダを作成できるようにする
                fbd.ShowNewFolderButton = showNewFolderButton;

                //ダイアログを表示する
                if (fbd.ShowDialog() == DialogResult.OK) {
                    //選択されたフォルダを設定する
                    retSelectedDirectory = fbd.SelectedPath;
                }

            } catch (Exception) {
                throw;
            } finally {
                fbd.Dispose();
            }

            return retSelectedDirectory;
        }

        /// <summary>
        /// 指定したディレクトリ以下の全てのファイルのFileInfoを取得する
        /// </summary>
        /// <param name="targetDir"></param>
        /// <returns></returns>
        public static List<FileInfo> GetAllFileInfo(string targetDir) {
            var ret = new List<FileInfo>();

            DirectoryInfo di = new DirectoryInfo(targetDir);
            GetFileInfoRecursively(di, ref ret);

            return ret;
        }

        //HACK 異なるディレクトリツリーから共通のファイルを抽出する。
        //HACK 異なるディレクトリツリーから差分ファイルを抽出する。
        //https://docs.microsoft.com/ja-jp/dotnet/csharp/programming-guide/concepts/linq/how-to-compare-the-contents-of-two-folders-linq

        #endregion PublicMethods

        #region PrivateMethods
        private static void GetFileInfoRecursively(DirectoryInfo directory, ref List<FileInfo> allFileInfo) {

			foreach (FileInfo fi in directory.GetFiles()) {
				//Officeのテンポラリファイルは無視。
                if (fi.Name.StartsWith("~")) {
					continue;
                }
                allFileInfo.Add(fi);
			}
			//再帰的にサブフォルダも追加
			foreach (DirectoryInfo di in directory.GetDirectories()) {
                if ((di.Attributes & FileAttributes.Hidden) != FileAttributes.Hidden) {
                    //不可視ディレクトリ以外
    				GetFileInfoRecursively(di, ref allFileInfo);
                }
			}
        }
        #endregion PrivateMethods
    }


    /// <summary>
    /// ファイル内容比較クラス
    /// </summary>
    internal class FileCompare : IEqualityComparer<FileInfo> {
        /// <summary>対象ディレクトリ1への絶対パス</summary>
        private string _basePath1 = "";
        /// <summary>対象ディレクトリ2への絶対パス</summary>
        private string _basePath2 = "";

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public FileCompare(string basePath1, string basePath2) {
            _basePath1 = basePath1;
            _basePath2 = basePath2;
        }  

        /// <summary>
        /// 同一ファイルか判定
        /// </summary>
        /// <param name="f1">対象ファイル1</param>
        /// <param name="f2">対象ファイル2</param>
        /// <returns>true:同一ファイル／false:別ファイル</returns>
        public bool Equals(FileInfo f1, FileInfo f2) {
            //ディレクトリ構造&ファイル名比較
            if (f1.FullName.Replace(_basePath1, "") != f2.FullName.Replace(_basePath2, "")) {
                return false;
            }
            //ファイルサイズ比較
            if (f1.Length != f2.Length) {
                return false;
            }
            return true;
        }

        public int GetHashCode(System.IO.FileInfo fi) {
            string s = String.Format("{0}{1}", fi.Name, fi.Length);  
            return s.GetHashCode();  
        }
    } 
    /// <summary>
    /// ディレクトリ内容比較クラス
    /// </summary>
    internal class DirectoryCompare : IEqualityComparer<DirectoryInfo> {
        /// <summary>対象ディレクトリ1への絶対パス</summary>
        private string _basePath1 = "";
        /// <summary>対象ディレクトリ2への絶対パス</summary>
        private string _basePath2 = "";

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public DirectoryCompare(string basePath1, string basePath2) {
            _basePath1 = basePath1;
            _basePath2 = basePath2;
        }  

        /// <summary>
        /// 同一ディレクトリか判定
        /// </summary>
        /// <param name="d1">対象ディレクトリ1</param>
        /// <param name="d2">対象ディレクトリ2</param>
        /// <returns>true:同一ディレクトリ／false:別ディレクトリ</returns>
        public bool Equals(DirectoryInfo d1, DirectoryInfo d2) {
            //ディレクトリ構造比較
            if (d1.FullName.Replace(_basePath1, "") != d2.FullName.Replace(_basePath2, "")) {
                return false;
            }
            return true;
        }  
        public int GetHashCode(DirectoryInfo di) {
            string s = di.FullName;
            return s.GetHashCode();  
        }
    } 
}
