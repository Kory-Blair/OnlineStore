CREATE TABLE [dbo].[Service] (
    [Id]          INT  IDENTITY(1,1)          NOT NULL,
    [Name]        NVARCHAR (100) NULL,
    [Price]       MONEY          NULL,
    [Description] NTEXT          NULL,
	[Image]			NVARCHAR(512) NULL,
	[ShortDescription] NVARCHAR(1000) NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

