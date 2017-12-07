CREATE TABLE [dbo].[Recipient] (
    [Id]          INT             IDENTITY (1, 1) NOT NULL,
    [Name]        NVARCHAR (1000) NULL,
    [Address]     NVARCHAR (1000) NULL,
    [City]        NVARCHAR (1000) NULL,
    [State]       NVARCHAR (100)  NULL,
    [ZipCode]     NVARCHAR (100)  NULL,
    [Email]       NVARCHAR (100)  NULL,
    [PhoneNumber] NVARCHAR (100)  NULL,
	[PurchaseId] INT NOT NULL,
    CONSTRAINT [PK_Recipient] PRIMARY KEY CLUSTERED ([Id] ASC), 
    CONSTRAINT [FK_Recipient_Purchase] FOREIGN KEY ([PurchaseId]) REFERENCES [Purchase]([Id])
);



