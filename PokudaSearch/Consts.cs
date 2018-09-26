using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokudaSearch {
    public static class Consts {
        /// <summary>Luceneインデックスディレクトリ</summary>
        public const string StoreDirName = @"\IndexStore";
        /// <summary>検索用Luceneインデックスディレクトリ名</summary>
        public const string IndexDirName = @"\Index";
        /// <summary>Luceneインデックス構築用ディレクトリ名</summary>
        public const string BuildDirName = @"\Build";
    }
}
