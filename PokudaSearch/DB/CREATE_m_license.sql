DROP TABLE [m_license];

CREATE TABLE [m_license] (
[利用開始日] DATETIME NOT NULL,
[認証日] DATETIME,
[認証キー] VARCHAR(100) NOT NULL DEFAULT '',
PRIMARY KEY([利用開始日])
);

