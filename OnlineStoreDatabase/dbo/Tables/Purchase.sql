CREATE TABLE [dbo].[Purchase] (
    [Id]          INT            IDENTITY (1, 1) NOT NULL,
    [PurchaseId]  INT            NULL,
    [ServiceName] NVARCHAR (100) NULL,
    [Price]       MONEY          NULL,
	[RecurrenceId] INT NULL,
	[CustomizationId] INT NULL,
	[ServiceId] INT NULL,
	[DateCreated] DateTime Null,
	[DateLastModified] DateTime NULL,
	[TrackingNumber] nvarchar(100) NULL,
	[ShippingAndHandling] money NULL,
	[Tax] money Null,
	[SubTotal] money NULL,
	[Total] money NULL,

    [Month] NVARCHAR(50) NULL, 
    [Date] NVARCHAR(50) NULL, 
    [Day] NVARCHAR(50) NULL, 
    [Time] NVARCHAR(50) NULL, 
    [AMPM] NVARCHAR(50) NULL, 
    [Minutes] NVARCHAR(50) NULL, 
    CONSTRAINT [PK_PurchaseId] PRIMARY KEY CLUSTERED ([Id] ASC), 
    CONSTRAINT [FK_Purchase_Recurrence] FOREIGN KEY ([RecurrenceId]) REFERENCES [Recurrence]([Id]), 
    CONSTRAINT [FK_Purchase_Customization] FOREIGN KEY (CustomizationId) REFERENCES [Customization]([Id]), 
    CONSTRAINT [FK_Purchase_Service] FOREIGN KEY ([ServiceId]) REFERENCES [Service]([Id])
);


	




