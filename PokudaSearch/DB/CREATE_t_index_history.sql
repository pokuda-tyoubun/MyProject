CREATE TABLE [t_index_history] ( 
[予約No] INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL DEFAULT '0', 
[作成開始] DATETIME, 
[作成完了] DATETIME, 
[モード] VARCHAR(10) NOT NULL DEFAULT '', 
[パス] VARCHAR(255) NOT NULL DEFAULT '', 
[作成時間(分)] INTEGER NOT NULL DEFAULT '0', 
[対象ファイル数] INTEGER NOT NULL DEFAULT '0', 
[インデックス済み] INTEGER NOT NULL DEFAULT '0', 
[インデックス対象外] INTEGER NOT NULL DEFAULT '0', 
[総バイト数] INTEGER NOT NULL DEFAULT '0', 
[テキスト抽出器] VARCHAR(10) NOT NULL DEFAULT ''
);
