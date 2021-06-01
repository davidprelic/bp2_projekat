
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 05/31/2021 17:35:05
-- Generated from EDMX file: C:\Users\David\Desktop\PROJEKTI 2020\BP2_Projekat\BP2_Projekat\DB_Model\AutoKompanijaDb.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [AutoKompanijaDb];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_AutoKompanijaZaposljava]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Zaposljavas] DROP CONSTRAINT [FK_AutoKompanijaZaposljava];
GO
IF OBJECT_ID(N'[dbo].[FK_ZaposljavaSluzbenik]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Zaposljavas] DROP CONSTRAINT [FK_ZaposljavaSluzbenik];
GO
IF OBJECT_ID(N'[dbo].[FK_SluzbenikPregovara]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Pregovaras] DROP CONSTRAINT [FK_SluzbenikPregovara];
GO
IF OBJECT_ID(N'[dbo].[FK_KupacPregovara]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Pregovaras] DROP CONSTRAINT [FK_KupacPregovara];
GO
IF OBJECT_ID(N'[dbo].[FK_AutoKompanijaAutoSalon]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[AutoSalons] DROP CONSTRAINT [FK_AutoKompanijaAutoSalon];
GO
IF OBJECT_ID(N'[dbo].[FK_AutoSalonAutomobil]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Automobils] DROP CONSTRAINT [FK_AutoSalonAutomobil];
GO
IF OBJECT_ID(N'[dbo].[FK_KupacAutomobil]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Automobils] DROP CONSTRAINT [FK_KupacAutomobil];
GO
IF OBJECT_ID(N'[dbo].[FK_AutomobilPlacanje]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Placanjes] DROP CONSTRAINT [FK_AutomobilPlacanje];
GO
IF OBJECT_ID(N'[dbo].[FK_Kredit_inherits_Placanje]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Placanjes_Kredit] DROP CONSTRAINT [FK_Kredit_inherits_Placanje];
GO
IF OBJECT_ID(N'[dbo].[FK_Lizing_inherits_Placanje]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Placanjes_Lizing] DROP CONSTRAINT [FK_Lizing_inherits_Placanje];
GO
IF OBJECT_ID(N'[dbo].[FK_Kes_inherits_Placanje]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Placanjes_Kes] DROP CONSTRAINT [FK_Kes_inherits_Placanje];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[AutoKompanijas]', 'U') IS NOT NULL
    DROP TABLE [dbo].[AutoKompanijas];
GO
IF OBJECT_ID(N'[dbo].[AutoSalons]', 'U') IS NOT NULL
    DROP TABLE [dbo].[AutoSalons];
GO
IF OBJECT_ID(N'[dbo].[Sluzbeniks]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Sluzbeniks];
GO
IF OBJECT_ID(N'[dbo].[Kupacs]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Kupacs];
GO
IF OBJECT_ID(N'[dbo].[Automobils]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Automobils];
GO
IF OBJECT_ID(N'[dbo].[Placanjes]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Placanjes];
GO
IF OBJECT_ID(N'[dbo].[Zaposljavas]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Zaposljavas];
GO
IF OBJECT_ID(N'[dbo].[Pregovaras]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Pregovaras];
GO
IF OBJECT_ID(N'[dbo].[Placanjes_Kredit]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Placanjes_Kredit];
GO
IF OBJECT_ID(N'[dbo].[Placanjes_Lizing]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Placanjes_Lizing];
GO
IF OBJECT_ID(N'[dbo].[Placanjes_Kes]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Placanjes_Kes];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'AutoKompanijas'
CREATE TABLE [dbo].[AutoKompanijas] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Naziv] nvarchar(max)  NOT NULL,
    [DatumOsnivanja] datetime  NOT NULL
);
GO

-- Creating table 'AutoSalons'
CREATE TABLE [dbo].[AutoSalons] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [AutoKompanijaId] int  NOT NULL,
    [BrojRaspolozivihAutomobila] int  NOT NULL,
    [Grad] nvarchar(max)  NOT NULL,
    [Ulica] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'Sluzbeniks'
CREATE TABLE [dbo].[Sluzbeniks] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Ime] nvarchar(max)  NOT NULL,
    [Prezime] nvarchar(max)  NOT NULL,
    [DatumZaposlenja] datetime  NOT NULL
);
GO

-- Creating table 'Kupacs'
CREATE TABLE [dbo].[Kupacs] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Ime] nvarchar(max)  NOT NULL,
    [Prezime] nvarchar(max)  NOT NULL,
    [OmiljeniAutomobil] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'Automobils'
CREATE TABLE [dbo].[Automobils] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Marka] nvarchar(max)  NOT NULL,
    [Model] nvarchar(max)  NOT NULL,
    [Cena] int  NOT NULL,
    [AutoSalonId] int  NULL,
    [KupacId] int  NULL,
    [DatumNarucivanja] datetime  NULL
);
GO

-- Creating table 'Placanjes'
CREATE TABLE [dbo].[Placanjes] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [DatumPlacanja] datetime  NOT NULL,
    [AutomobilId] int  NOT NULL
);
GO

-- Creating table 'Zaposljavas'
CREATE TABLE [dbo].[Zaposljavas] (
    [AutoKompanijaId] int  NOT NULL,
    [SluzbenikId] int  NOT NULL
);
GO

-- Creating table 'Pregovaras'
CREATE TABLE [dbo].[Pregovaras] (
    [SluzbenikId] int  NOT NULL,
    [KupacId] int  NOT NULL
);
GO

-- Creating table 'Placanjes_Kredit'
CREATE TABLE [dbo].[Placanjes_Kredit] (
    [KamatnaStopa] int  NOT NULL,
    [PeriodOtplate] int  NOT NULL,
    [Id] int  NOT NULL
);
GO

-- Creating table 'Placanjes_Lizing'
CREATE TABLE [dbo].[Placanjes_Lizing] (
    [RataLizingNaknade] int  NOT NULL,
    [VrstaLizinga] nvarchar(max)  NOT NULL,
    [Id] int  NOT NULL
);
GO

-- Creating table 'Placanjes_Kes'
CREATE TABLE [dbo].[Placanjes_Kes] (
    [Vrednost] int  NOT NULL,
    [Id] int  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'AutoKompanijas'
ALTER TABLE [dbo].[AutoKompanijas]
ADD CONSTRAINT [PK_AutoKompanijas]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'AutoSalons'
ALTER TABLE [dbo].[AutoSalons]
ADD CONSTRAINT [PK_AutoSalons]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Sluzbeniks'
ALTER TABLE [dbo].[Sluzbeniks]
ADD CONSTRAINT [PK_Sluzbeniks]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Kupacs'
ALTER TABLE [dbo].[Kupacs]
ADD CONSTRAINT [PK_Kupacs]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Automobils'
ALTER TABLE [dbo].[Automobils]
ADD CONSTRAINT [PK_Automobils]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Placanjes'
ALTER TABLE [dbo].[Placanjes]
ADD CONSTRAINT [PK_Placanjes]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [AutoKompanijaId], [SluzbenikId] in table 'Zaposljavas'
ALTER TABLE [dbo].[Zaposljavas]
ADD CONSTRAINT [PK_Zaposljavas]
    PRIMARY KEY CLUSTERED ([AutoKompanijaId], [SluzbenikId] ASC);
GO

-- Creating primary key on [SluzbenikId], [KupacId] in table 'Pregovaras'
ALTER TABLE [dbo].[Pregovaras]
ADD CONSTRAINT [PK_Pregovaras]
    PRIMARY KEY CLUSTERED ([SluzbenikId], [KupacId] ASC);
GO

-- Creating primary key on [Id] in table 'Placanjes_Kredit'
ALTER TABLE [dbo].[Placanjes_Kredit]
ADD CONSTRAINT [PK_Placanjes_Kredit]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Placanjes_Lizing'
ALTER TABLE [dbo].[Placanjes_Lizing]
ADD CONSTRAINT [PK_Placanjes_Lizing]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Placanjes_Kes'
ALTER TABLE [dbo].[Placanjes_Kes]
ADD CONSTRAINT [PK_Placanjes_Kes]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [AutoKompanijaId] in table 'Zaposljavas'
ALTER TABLE [dbo].[Zaposljavas]
ADD CONSTRAINT [FK_AutoKompanijaZaposljava]
    FOREIGN KEY ([AutoKompanijaId])
    REFERENCES [dbo].[AutoKompanijas]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [SluzbenikId] in table 'Zaposljavas'
ALTER TABLE [dbo].[Zaposljavas]
ADD CONSTRAINT [FK_ZaposljavaSluzbenik]
    FOREIGN KEY ([SluzbenikId])
    REFERENCES [dbo].[Sluzbeniks]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ZaposljavaSluzbenik'
CREATE INDEX [IX_FK_ZaposljavaSluzbenik]
ON [dbo].[Zaposljavas]
    ([SluzbenikId]);
GO

-- Creating foreign key on [SluzbenikId] in table 'Pregovaras'
ALTER TABLE [dbo].[Pregovaras]
ADD CONSTRAINT [FK_SluzbenikPregovara]
    FOREIGN KEY ([SluzbenikId])
    REFERENCES [dbo].[Sluzbeniks]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [KupacId] in table 'Pregovaras'
ALTER TABLE [dbo].[Pregovaras]
ADD CONSTRAINT [FK_KupacPregovara]
    FOREIGN KEY ([KupacId])
    REFERENCES [dbo].[Kupacs]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_KupacPregovara'
CREATE INDEX [IX_FK_KupacPregovara]
ON [dbo].[Pregovaras]
    ([KupacId]);
GO

-- Creating foreign key on [AutoKompanijaId] in table 'AutoSalons'
ALTER TABLE [dbo].[AutoSalons]
ADD CONSTRAINT [FK_AutoKompanijaAutoSalon]
    FOREIGN KEY ([AutoKompanijaId])
    REFERENCES [dbo].[AutoKompanijas]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_AutoKompanijaAutoSalon'
CREATE INDEX [IX_FK_AutoKompanijaAutoSalon]
ON [dbo].[AutoSalons]
    ([AutoKompanijaId]);
GO

-- Creating foreign key on [AutoSalonId] in table 'Automobils'
ALTER TABLE [dbo].[Automobils]
ADD CONSTRAINT [FK_AutoSalonAutomobil]
    FOREIGN KEY ([AutoSalonId])
    REFERENCES [dbo].[AutoSalons]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_AutoSalonAutomobil'
CREATE INDEX [IX_FK_AutoSalonAutomobil]
ON [dbo].[Automobils]
    ([AutoSalonId]);
GO

-- Creating foreign key on [KupacId] in table 'Automobils'
ALTER TABLE [dbo].[Automobils]
ADD CONSTRAINT [FK_KupacAutomobil]
    FOREIGN KEY ([KupacId])
    REFERENCES [dbo].[Kupacs]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_KupacAutomobil'
CREATE INDEX [IX_FK_KupacAutomobil]
ON [dbo].[Automobils]
    ([KupacId]);
GO

-- Creating foreign key on [AutomobilId] in table 'Placanjes'
ALTER TABLE [dbo].[Placanjes]
ADD CONSTRAINT [FK_AutomobilPlacanje]
    FOREIGN KEY ([AutomobilId])
    REFERENCES [dbo].[Automobils]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_AutomobilPlacanje'
CREATE INDEX [IX_FK_AutomobilPlacanje]
ON [dbo].[Placanjes]
    ([AutomobilId]);
GO

-- Creating foreign key on [Id] in table 'Placanjes_Kredit'
ALTER TABLE [dbo].[Placanjes_Kredit]
ADD CONSTRAINT [FK_Kredit_inherits_Placanje]
    FOREIGN KEY ([Id])
    REFERENCES [dbo].[Placanjes]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating foreign key on [Id] in table 'Placanjes_Lizing'
ALTER TABLE [dbo].[Placanjes_Lizing]
ADD CONSTRAINT [FK_Lizing_inherits_Placanje]
    FOREIGN KEY ([Id])
    REFERENCES [dbo].[Placanjes]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating foreign key on [Id] in table 'Placanjes_Kes'
ALTER TABLE [dbo].[Placanjes_Kes]
ADD CONSTRAINT [FK_Kes_inherits_Placanje]
    FOREIGN KEY ([Id])
    REFERENCES [dbo].[Placanjes]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------