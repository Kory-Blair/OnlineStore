CREATE TABLE [dbo].[Recurrence] (
    [Id]                  INT            IDENTITY (1, 1) NOT NULL,
    [Name]                NVARCHAR (100) NULL,
    [Option]              NVARCHAR (1)   NULL,
    [Price Multiplier]    DECIMAL (18, 2)   NULL,
    [Savings]             DECIMAL(18, 2)            NULL,
    [Recurrence_Purchase] INT            NULL,
    CONSTRAINT [PK_Recurrence] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Recurrence_Purchase] FOREIGN KEY ([Recurrence_Purchase]) REFERENCES [dbo].[Purchase] ([Id])
);







