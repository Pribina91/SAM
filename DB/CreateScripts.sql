USE [master]
GO
/****** Object:  Database [ASFEU]    Script Date: 24.9.2014 16:40:55 ******/
CREATE DATABASE [ASFEU]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'ASFEU', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL11.SQLEXPRESS\MSSQL\DATA\ASFEU.mdf' , SIZE = 314432KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'ASFEU_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL11.SQLEXPRESS\MSSQL\DATA\ASFEU_log.ldf' , SIZE = 10368KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [ASFEU] SET COMPATIBILITY_LEVEL = 110
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [ASFEU].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [ASFEU] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [ASFEU] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [ASFEU] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [ASFEU] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [ASFEU] SET ARITHABORT OFF 
GO
ALTER DATABASE [ASFEU] SET AUTO_CLOSE ON 
GO
ALTER DATABASE [ASFEU] SET AUTO_CREATE_STATISTICS ON 
GO
ALTER DATABASE [ASFEU] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [ASFEU] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [ASFEU] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [ASFEU] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [ASFEU] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [ASFEU] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [ASFEU] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [ASFEU] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [ASFEU] SET  ENABLE_BROKER 
GO
ALTER DATABASE [ASFEU] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [ASFEU] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [ASFEU] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [ASFEU] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [ASFEU] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [ASFEU] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [ASFEU] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [ASFEU] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [ASFEU] SET  MULTI_USER 
GO
ALTER DATABASE [ASFEU] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [ASFEU] SET DB_CHAINING OFF 
GO
ALTER DATABASE [ASFEU] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [ASFEU] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
USE [ASFEU]
GO
/****** Object:  Table [dbo].[BD__CISELNIKY]    Script Date: 24.9.2014 16:40:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[BD__CISELNIKY](
	[KOD] [int] NULL,
	[NADRAD] [int] NULL,
	[NAZOV] [varchar](70) NULL,
	[UZIV_IDENT] [varchar](100) NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[BD_MER_CASOVE_RADY]    Script Date: 24.9.2014 16:40:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BD_MER_CASOVE_RADY](
	[DIAGRAM_ID] [int] NULL,
	[CAS] [int] NULL,
	[MNOZSTVO] [int] NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[BD_MER_DIAGRAMY]    Script Date: 24.9.2014 16:40:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BD_MER_DIAGRAMY](
	[KOD] [int] NULL,
	[OOM_ID] [int] NULL,
	[DATUM] [datetime] NULL,
	[JEDNOTKA] [int] NULL,
	[DRUH_MERANIA] [int] NULL,
	[SUMARNE_MNOZSTVO] [int] NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[BD_OOM]    Script Date: 24.9.2014 16:40:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[BD_OOM](
	[KOD] [int] NULL,
	[DRUH] [int] NULL,
	[SUSTAVA_ID] [int] NULL,
	[PSC] [varchar](10) NULL,
	[TYP_MERANIA] [int] NULL,
	[TRIEDA_TDO] [int] NULL,
	[NAPATOVA_UROVEN] [int] NULL,
	[ROCNY_PREDPOKLAD] [real] NULL,
	[VYROBNA_ID] [int] NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[BD_SUSTAVY]    Script Date: 24.9.2014 16:40:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BD_SUSTAVY](
	[KOD] [int] NULL,
	[TYP_SUSTAVY] [int] NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[BD_TDO_NORM]    Script Date: 24.9.2014 16:40:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BD_TDO_NORM](
	[KOD] [int] NULL,
	[TRIEDA_TDO] [int] NULL,
	[TYP_TDO] [int] NULL,
	[ROK] [int] NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[BD_TDO_NORM_CASOVE_RADY]    Script Date: 24.9.2014 16:40:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BD_TDO_NORM_CASOVE_RADY](
	[DIAGRAM_ID] [int] NULL,
	[CAS] [int] NULL,
	[MNOZSTVO] [int] NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[BD_TDO_NORM_DIAGRAMY]    Script Date: 24.9.2014 16:40:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BD_TDO_NORM_DIAGRAMY](
	[KOD] [int] NULL,
	[NORM_TDO_ID] [int] NULL,
	[DATUM] [datetime] NULL
) ON [PRIMARY]

GO
/****** Object:  Index [BD__CISELNIKY_PK]    Script Date: 24.9.2014 16:40:55 ******/
CREATE UNIQUE NONCLUSTERED INDEX [BD__CISELNIKY_PK] ON [dbo].[BD__CISELNIKY]
(
	[KOD] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [BD_MER_CASOVE_RADY_PK]    Script Date: 24.9.2014 16:40:55 ******/
CREATE UNIQUE NONCLUSTERED INDEX [BD_MER_CASOVE_RADY_PK] ON [dbo].[BD_MER_CASOVE_RADY]
(
	[DIAGRAM_ID] ASC,
	[CAS] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [BD_MER_DIAGRAMY_PK]    Script Date: 24.9.2014 16:40:55 ******/
CREATE UNIQUE NONCLUSTERED INDEX [BD_MER_DIAGRAMY_PK] ON [dbo].[BD_MER_DIAGRAMY]
(
	[KOD] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [BD_OOM_PK]    Script Date: 24.9.2014 16:40:55 ******/
CREATE UNIQUE NONCLUSTERED INDEX [BD_OOM_PK] ON [dbo].[BD_OOM]
(
	[KOD] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [BD_SUSTAVY_PK]    Script Date: 24.9.2014 16:40:55 ******/
CREATE UNIQUE NONCLUSTERED INDEX [BD_SUSTAVY_PK] ON [dbo].[BD_SUSTAVY]
(
	[KOD] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [BD_TDO_NORM_PK]    Script Date: 24.9.2014 16:40:55 ******/
CREATE UNIQUE NONCLUSTERED INDEX [BD_TDO_NORM_PK] ON [dbo].[BD_TDO_NORM]
(
	[KOD] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [BD_TDO_NORM_CASOVE_RADY_PK]    Script Date: 24.9.2014 16:40:55 ******/
CREATE UNIQUE NONCLUSTERED INDEX [BD_TDO_NORM_CASOVE_RADY_PK] ON [dbo].[BD_TDO_NORM_CASOVE_RADY]
(
	[DIAGRAM_ID] ASC,
	[CAS] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [BD_TDO_NORM_DIAGRAMY_PK]    Script Date: 24.9.2014 16:40:55 ******/
CREATE UNIQUE NONCLUSTERED INDEX [BD_TDO_NORM_DIAGRAMY_PK] ON [dbo].[BD_TDO_NORM_DIAGRAMY]
(
	[KOD] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
USE [master]
GO
ALTER DATABASE [ASFEU] SET  READ_WRITE 
GO
