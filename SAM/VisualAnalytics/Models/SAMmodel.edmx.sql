
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 05/13/2015 02:00:08
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
IF OBJECT_ID(N'[dbo].[FK_IDConsuptionPlace2]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ConsuptionsHourly] DROP CONSTRAINT [FK_IDConsuptionPlace2];
GO
IF OBJECT_ID(N'[dbo].[FK_IDConsuptionPlace3]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ConsuptionsDaily] DROP CONSTRAINT [FK_IDConsuptionPlace3];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[ConsuptionModelDaily]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ConsuptionModelDaily];
GO
IF OBJECT_ID(N'[dbo].[ConsuptionModelHourly]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ConsuptionModelHourly];
GO
IF OBJECT_ID(N'[dbo].[ConsuptionPlaces]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ConsuptionPlaces];
GO
IF OBJECT_ID(N'[dbo].[Consuptions]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Consuptions];
GO
IF OBJECT_ID(N'[dbo].[ConsuptionsDaily]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ConsuptionsDaily];
GO
IF OBJECT_ID(N'[dbo].[ConsuptionsHourly]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ConsuptionsHourly];
GO
IF OBJECT_ID(N'[dbo].[Events]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Events];
GO
IF OBJECT_ID(N'[dbo].[PlaceWeather]', 'U') IS NOT NULL
    DROP TABLE [dbo].[PlaceWeather];
GO
IF OBJECT_ID(N'[dbo].[sysdiagrams]', 'U') IS NOT NULL
    DROP TABLE [dbo].[sysdiagrams];
GO
IF OBJECT_ID(N'[dbo].[Weathers]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Weathers];
GO
IF OBJECT_ID(N'[dbo].[WeathersDaily]', 'U') IS NOT NULL
    DROP TABLE [dbo].[WeathersDaily];
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

-- Creating table 'PlaceWeathers'
CREATE TABLE [dbo].[PlaceWeathers] (
    [DistrictName] varchar(50)  NULL,
    [IDDistrict] int  NOT NULL,
    [IDLocation] int  NOT NULL
);
GO

-- Creating table 'ConsuptionsDailies'
CREATE TABLE [dbo].[ConsuptionsDailies] (
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

-- Creating table 'ConsuptionsHourlies'
CREATE TABLE [dbo].[ConsuptionsHourlies] (
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

-- Creating table 'WeathersDailies'
CREATE TABLE [dbo].[WeathersDailies] (
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

-- Creating table 'ConsuptionModelDailies'
CREATE TABLE [dbo].[ConsuptionModelDailies] (
    [Amount] float  NULL,
    [MeasurementTime] int  NOT NULL,
    [IDDate] bigint  NOT NULL,
    [IDDistrict] int  NOT NULL,
    [source] int  NOT NULL,
    [Type] varchar(1)  NOT NULL,
    [IDConsuption] int IDENTITY(1,1) NOT NULL
);
GO

-- Creating table 'ConsuptionModelHourlies'
CREATE TABLE [dbo].[ConsuptionModelHourlies] (
    [Amount] float  NULL,
    [MeasurementTime] int  NOT NULL,
    [IDDate] bigint  NOT NULL,
    [IDDistrict] int  NOT NULL,
    [source] int  NOT NULL,
    [Type] varchar(1)  NOT NULL,
    [IDConsuption] int IDENTITY(1,1) NOT NULL
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

-- Creating primary key on [IDweather] in table 'Weathers'
ALTER TABLE [dbo].[Weathers]
ADD CONSTRAINT [PK_Weathers]
    PRIMARY KEY CLUSTERED ([IDweather] ASC);
GO

-- Creating primary key on [IDDistrict] in table 'PlaceWeathers'
ALTER TABLE [dbo].[PlaceWeathers]
ADD CONSTRAINT [PK_PlaceWeathers]
    PRIMARY KEY CLUSTERED ([IDDistrict] ASC);
GO

-- Creating primary key on [IDConsuption] in table 'ConsuptionsDailies'
ALTER TABLE [dbo].[ConsuptionsDailies]
ADD CONSTRAINT [PK_ConsuptionsDailies]
    PRIMARY KEY CLUSTERED ([IDConsuption] ASC);
GO

-- Creating primary key on [IDConsuption] in table 'ConsuptionsHourlies'
ALTER TABLE [dbo].[ConsuptionsHourlies]
ADD CONSTRAINT [PK_ConsuptionsHourlies]
    PRIMARY KEY CLUSTERED ([IDConsuption] ASC);
GO

-- Creating primary key on [IDweather] in table 'WeathersDailies'
ALTER TABLE [dbo].[WeathersDailies]
ADD CONSTRAINT [PK_WeathersDailies]
    PRIMARY KEY CLUSTERED ([IDweather] ASC);
GO

-- Creating primary key on [IDConsuption] in table 'ConsuptionModelDailies'
ALTER TABLE [dbo].[ConsuptionModelDailies]
ADD CONSTRAINT [PK_ConsuptionModelDailies]
    PRIMARY KEY CLUSTERED ([IDConsuption] ASC);
GO

-- Creating primary key on [IDConsuption] in table 'ConsuptionModelHourlies'
ALTER TABLE [dbo].[ConsuptionModelHourlies]
ADD CONSTRAINT [PK_ConsuptionModelHourlies]
    PRIMARY KEY CLUSTERED ([IDConsuption] ASC);
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

-- Creating foreign key on [IDConsuptionPlace] in table 'ConsuptionsHourlies'
ALTER TABLE [dbo].[ConsuptionsHourlies]
ADD CONSTRAINT [FK_IDConsuptionPlace2]
    FOREIGN KEY ([IDConsuptionPlace])
    REFERENCES [dbo].[ConsuptionPlaces]
        ([IDConsuptionPlace])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_IDConsuptionPlace2'
CREATE INDEX [IX_FK_IDConsuptionPlace2]
ON [dbo].[ConsuptionsHourlies]
    ([IDConsuptionPlace]);
GO

-- Creating foreign key on [IDConsuptionPlace] in table 'ConsuptionsDailies'
ALTER TABLE [dbo].[ConsuptionsDailies]
ADD CONSTRAINT [FK_IDConsuptionPlace3]
    FOREIGN KEY ([IDConsuptionPlace])
    REFERENCES [dbo].[ConsuptionPlaces]
        ([IDConsuptionPlace])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_IDConsuptionPlace3'
CREATE INDEX [IX_FK_IDConsuptionPlace3]
ON [dbo].[ConsuptionsDailies]
    ([IDConsuptionPlace]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------