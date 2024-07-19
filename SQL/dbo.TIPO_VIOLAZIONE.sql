CREATE TABLE [dbo].[TIPO_VIOLAZIONE] (
    [IdViolazione] INT            IDENTITY (1, 1) NOT NULL,
    [Descrizione]  NVARCHAR (260) NULL,
    PRIMARY KEY CLUSTERED ([IdViolazione] ASC)
);

