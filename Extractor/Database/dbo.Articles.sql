﻿IF EXISTS (
		SELECT *
		FROM sys.tables
		WHERE NAME = 'Articles'
		)
	DROP TABLE dbo.Articles;

CREATE TABLE dbo.Articles (
	Id UNIQUEIDENTIFIER NOT NULL DEFAULT NEWID()
	,Link NVARCHAR(MAX) NULL
	,Picture IMAGE NULL
	,Price MONEY NULL
	,Title NVARCHAR(MAX) NOT NULL
	,Units TINYINT NULL
	,PRIMARY KEY CLUSTERED (Id ASC)
	);
