CREATE TABLE [dbo].[ANAGRAFICA] (
    [IdAnagrafica] INT            IDENTITY (1, 1) NOT NULL,
    [Cognome]      NVARCHAR (50)  NOT NULL,
    [Nome]         NVARCHAR (50)  NOT NULL,
    [Indirizzo]    NVARCHAR (100) NOT NULL,
    [Città]        NVARCHAR (50)  NOT NULL,
    [CAP]          NCHAR (10)     NOT NULL,
    [Cod_Fisc]     NCHAR (16)     NOT NULL,
    PRIMARY KEY CLUSTERED ([IdAnagrafica] ASC)
);

