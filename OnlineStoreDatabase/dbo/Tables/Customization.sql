CREATE TABLE [dbo].[Customization] (
    [Id]   INT            IDENTITY (1, 1) NOT NULL,
	[ServiceID] INT NOT NULL,
    [Name] NVARCHAR (100) NULL, 
    CONSTRAINT [PK_Customization] PRIMARY KEY ([Id]), 
    CONSTRAINT [FK_Customization_Service] FOREIGN KEY (ServiceID) REFERENCES Service([Id])
);

