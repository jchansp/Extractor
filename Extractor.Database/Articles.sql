CREATE TABLE [dbo].[Articles]
(
	[Id] INT NOT NULL PRIMARY KEY, 
    [Link] NVARCHAR(50) NULL, 
    [Picture] IMAGE NULL, 
    [Price] MONEY NULL, 
    [Title] NVARCHAR(50) NULL, 
    [Units] TINYINT NULL
)
