CREATE TABLE [dbo].[ScheduledService] (
    [Id]              INT             IDENTITY (1, 1) NOT NULL,
    [Name of Service] NVARCHAR (100)  NULL,
    [Description]     NTEXT           NULL,
    [Image]           NVARCHAR (1000) NULL,
    [DateOrdered]     DATETIME        DEFAULT (getdate()) NULL,
    [DateScheduled]   DATETIME        NULL,
	[PurchaseId] INT,
    CONSTRAINT [PK_Product] PRIMARY KEY CLUSTERED ([Id] ASC), 
    CONSTRAINT [FK_Scheduled Service_Purchase] FOREIGN KEY ([PurchaseId]) REFERENCES [Purchase]([Id])
);

