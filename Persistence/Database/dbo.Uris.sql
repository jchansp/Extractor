CREATE TABLE [dbo].[Uris] (
    [Id]          UNIQUEIDENTIFIER DEFAULT (newid()) NOT NULL,
    [AbsoluteUri] NVARCHAR (255)   NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
	UNIQUE ([AbsoluteUri])
);

