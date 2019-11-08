CREATE TABLE [m_extensions] (
[extension] VARCHAR(10) NOT NULL DEFAULT '' UNIQUE,
[note] VARCHAR(100) NOT NULL DEFAULT '',
PRIMARY KEY([extension])
);