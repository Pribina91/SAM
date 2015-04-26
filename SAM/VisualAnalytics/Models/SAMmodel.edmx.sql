
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 04/13/2015 17:21:09
-- Generated from EDMX file: C:\Workspace\SAM\SAM\VisualAnalytics\Models\SAMmodel.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [SAM];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_IDConsuptionPlace]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Consuptions] DROP CONSTRAINT [FK_IDConsuptionPlace];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[ConsuptionPlaces]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ConsuptionPlaces];
GO
IF OBJECT_ID(N'[dbo].[Consuptions]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Consuptions];
GO
IF OBJECT_ID(N'[dbo].[Events]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Events];
GO
IF OBJECT_ID(N'[dbo].[sysdiagrams]', 'U') IS NOT NULL
    DROP TABLE [dbo].[sysdiagrams];
GO
IF OBJECT_ID(N'[SAMModelStoreContainer].[Weathers]', 'U') IS NOT NULL
    DROP TABLE [SAMModelStoreContainer].[Weathers];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'ConsuptionPlaces'
CREATE TABLE [dbo].[ConsuptionPlaces] (
    [IDConsuptionPlace] bigint IDENTITY(1,1) NOT NULL,
    [ZipCode] nvarchar(max)  NOT NULL,
    [CityName] varchar(50)  NULL,
    [DistrictName] varchar(50)  NULL
);
GO

-- Creating table 'Consuptions'
CREATE TABLE [dbo].[Consuptions] (
    [IDConsuption] bigint IDENTITY(1,1) NOT NULL,
    [Amount] real  NOT NULL,
    [DayOffset] int  NOT NULL,
    [MeasurementSequence] int  NOT NULL,
    [MeasurementTime] int  NOT NULL,
    [IDDate] bigint  NOT NULL,
    [IDConsuptionPlace] bigint  NOT NULL,
    [source] int  NULL,
    [Type] varchar(2)  NULL
);
GO

-- Creating table 'sysdiagrams'
CREATE TABLE [dbo].[sysdiagrams] (
    [name] nvarchar(128)  NOT NULL,
    [principal_id] int  NOT NULL,
    [diagram_id] int IDENTITY(1,1) NOT NULL,
    [version] int  NULL,
    [definition] varbinary(max)  NULL
);
GO

-- Creating table 'Events'
CREATE TABLE [dbo].[Events] (
    [IDEvents] int  NOT NULL,
    [IDDate] bigint IDENTITY(1,1) NOT NULL,
    [eventName] nchar(200)  NULL,
    [weekday] nchar(20)  NULL,
    [eventType] nchar(30)  NULL,
    [eventDate] datetime  NOT NULL
);
GO

-- Creating table 'Weathers'
CREATE TABLE [dbo].[Weathers] (
    [IDDate] bigint  NOT NULL,
    [atmosphericPressure] int  NULL,
    [rainfall] int  NULL,
    [windSpeed] int  NULL,
    [surfaceTemperature] int  NULL,
    [solarShine] int  NULL,
    [relativeHumidity] int  NULL,
    [IDLocation] int  NOT NULL,
    [locationName] nvarchar(40)  NULL,
    [MeasurementTime] int  NOT NULL,
    [dt] datetime  NULL,
    [IDweather] int IDENTITY(1,1) NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [IDConsuptionPlace] in table 'ConsuptionPlaces'
ALTER TABLE [dbo].[ConsuptionPlaces]
ADD CONSTRAINT [PK_ConsuptionPlaces]
    PRIMARY KEY CLUSTERED ([IDConsuptionPlace] ASC);
GO

-- Creating primary key on [IDConsuption] in table 'Consuptions'
ALTER TABLE [dbo].[Consuptions]
ADD CONSTRAINT [PK_Consuptions]
    PRIMARY KEY CLUSTERED ([IDConsuption] ASC);
GO

-- Creating primary key on [diagram_id] in table 'sysdiagrams'
ALTER TABLE [dbo].[sysdiagrams]
ADD CONSTRAINT [PK_sysdiagrams]
    PRIMARY KEY CLUSTERED ([diagram_id] ASC);
GO

-- Creating primary key on [IDDate] in table 'Events'
ALTER TABLE [dbo].[Events]
ADD CONSTRAINT [PK_Events]
    PRIMARY KEY CLUSTERED ([IDDate] ASC);
GO

-- Creating primary key on [IDDate], [IDLocation], [MeasurementTime], [IDweather] in table 'Weathers'
ALTER TABLE [dbo].[Weathers]
ADD CONSTRAINT [PK_Weathers]
    PRIMARY KEY CLUSTERED ([IDDate], [IDLocation], [MeasurementTime], [IDweather] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [IDConsuptionPlace] in table 'Consuptions'
ALTER TABLE [dbo].[Consuptions]
ADD CONSTRAINT [FK_IDConsuptionPlace]
    FOREIGN KEY ([IDConsuptionPlace])
    REFERENCES [dbo].[ConsuptionPlaces]
        ([IDConsuptionPlace])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_IDConsuptionPlace'
CREATE INDEX [IX_FK_IDConsuptionPlace]
ON [dbo].[Consuptions]
    ([IDConsuptionPlace]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------