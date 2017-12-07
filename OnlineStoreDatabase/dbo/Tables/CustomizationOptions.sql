CREATE TABLE [dbo].[CustomizationOptions] (
    [Id]          INT            IDENTITY (1, 1) NOT NULL,
	[CustomizationId] INT NOT NULL,
    [Name]        NVARCHAR (100) NULL,
    [Price]       MONEY          NULL,
    [Description] NTEXT          NULL,
	CONSTRAINT PK_CustomizationOption PRIMARY KEY(Id),
	CONSTRAINT FK_ServiceCustomization FOREIGN KEY (CustomizationId) REFERENCES Customization(Id)
	);

