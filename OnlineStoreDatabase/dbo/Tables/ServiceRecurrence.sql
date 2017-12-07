CREATE TABLE [dbo].[ServiceRecurrence] (
    [ServiceID]       INT           NOT NULL,
    [RecurrenceID]    INT           NOT NULL,
    [Frequency]       NVARCHAR (50) NULL,
    [PriceMultiplier] INT           NULL,
    CONSTRAINT [PK_ServiceRecurrence] PRIMARY KEY CLUSTERED ([ServiceID] ASC, [RecurrenceID] ASC),
    CONSTRAINT [FK_ServiceRecurrence_Recurrence] FOREIGN KEY ([RecurrenceID]) REFERENCES [dbo].[Recurrence] ([Id]),
    CONSTRAINT [FK_ServiceRecurrence_Service] FOREIGN KEY ([ServiceID]) REFERENCES [dbo].[Service] ([Id])
);




