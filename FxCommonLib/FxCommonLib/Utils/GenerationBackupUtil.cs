using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Compression;
using C1.C1Zip.ZLib;

namespace FxCommonLib.Utils {
    public class GenerationBackupUtil {
        public int GenerationCount { get; set; }
        public string BasePath { get; set; }

        public GenerationBackupUtil(int generationCount, string basePath) {
            GenerationCount = generationCount;
            BasePath = basePath;
        }

        public void Backup(List<string> archivePathList, string rootDirName, string zipFileName) {
            //クリア処理
            FileUtil.DeleteDirectory(BasePath + @"\" + rootDirName);
            FileUtil.DeleteFile(BasePath + @"\" + zipFileName);

            //フォルダ作成
            CreateDirectory(rootDirName);

            //対象をコピー
            foreach (string path in archivePathList) {
                string dirName = Path.GetFileName(path);
                string dest = BasePath + @"\" + rootDirName + @"\" + dirName;
                if (FileUtil.IsDirectory(path)) {
                    //ディレクトリの場合
                    Directory.CreateDirectory(dest);
                    FileUtil.CopyDirectory(path, dest); 
                } else {
                    //ファイルの場合
                    File.Copy(path, dest);
                }
            }

            //圧縮
            ZipFile.CreateFromDirectory(BasePath + @"\" + rootDirName, BasePath + @"\" + zipFileName, CompressionLevel.Optimal, false);
            //世代ディレクトリに移動
            MoveZipFile(zipFileName);
            FileUtil.DeleteDirectory(BasePath + @"\" + rootDirName);
        }

        private void CreateDirectory(string rootName) {
            if (!Directory.Exists(BasePath + @"\" + rootName)) {
                Directory.CreateDirectory(BasePath + @"\" + rootName);
            }
            for (int i = 1; i <= GenerationCount; i++) {
                if (!Directory.Exists(BasePath + @"\" + i.ToString())) {
                    Directory.CreateDirectory(BasePath + @"\" + i.ToString());
                }
            }
        }

        private void MoveZipFile(string zipFileName) {
            //最も古いファイルを削除
            File.Delete(BasePath + @"\" + GenerationCount.ToString() + @"\" + zipFileName);
            for (int i = GenerationCount - 1; i >= 1; i--) {
                if (File.Exists(BasePath + @"\" + i.ToString() + @"\" + zipFileName)) {
                    File.Move(BasePath + @"\" + i.ToString() + @"\" + zipFileName, 
                        BasePath + @"\" + (i + 1).ToString() + @"\" + zipFileName);
                }
            }
            //最新ファイルを移動
            File.Move(BasePath + @"\" + zipFileName, BasePath + @"\1\" + zipFileName);
        }
    }
}
