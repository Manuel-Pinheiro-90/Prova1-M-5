CREATE TABLE [dbo].[VERBALE] (
    [IdVerbale]               INT            IDENTITY (1, 1) NOT NULL,
    [DataViolazione]          DATE           NULL,
    [IndirizzoViolazione]     NVARCHAR (100) NULL,
    [Nominativo_Agente]       NVARCHAR (50)  NULL,
    [DataTrascrizioneVerbale] DATE           NULL,
    [Importo]                 MONEY          NULL,
    [DecurtamentoPunti]       INT            NULL,
    [IdAnagrafica]            INT            NULL,
    [IdViolazione]            INT            NULL,
    PRIMARY KEY CLUSTERED ([IdVerbale] ASC),
    CONSTRAINT [FK_ANAGRAFICA] FOREIGN KEY ([IdAnagrafica]) REFERENCES [dbo].[ANAGRAFICA] ([IdAnagrafica]),
    CONSTRAINT [FK_TIPO_VIOLAZIONE] FOREIGN KEY ([IdViolazione]) REFERENCES [dbo].[TIPO_VIOLAZIONE] ([IdViolazione])
);

