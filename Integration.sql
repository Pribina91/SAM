/****** Script for SelectTopNRows command from SSMS  ******/
SELECT COUNT(distinct ZipCode)
  FROM [SAM].[dbo].[ConsuptionPlaces]
  ;

  select distinct [type] FROM [SAM].[dbo].[Consuptions] c
SELECT IDConsuptionPlace,IDDate,c.MeasurementSequence,count(1)
  FROM [SAM].[dbo].[Consuptions] c
  where c.[Type] = 'O'
  group by IDConsuptionPlace,IDDate,c.MeasurementSequence having count(1)>1;

  truncate table [dbo].[Consuptions]
;
update cp
set cp.[DistrictName] = 'Košice'
from 
[ConsuptionPlaces] cp 
--inner join [pscUni] pc on pc.[Column 0] = cp.ZipCode
where cp.[DistrictName] in( 'K3','K4')

SELECT *
  FROM [SAM].[dbo].[Consuptions] c
  join [dbo].[ConsuptionPlaces] p on p.[IDConsuptionPlace]= c.[IDConsuptionPlace]



insert into [dbo].[Consuptions]
([Amount]
      ,[DayOffset]
      ,[MeasurementSequence]
      ,[MeasurementTime]
      ,[IDDate]
      ,[IDConsuptionPlace]
      ,[source]
      ,[Type])

SELECT  /* [KOD]
      ,[OOM_ID]
      ,[DRUH_MERANIA]
      ,[DATUM]*/
	  [MNOZSTVO] as [Amount]
	  ,0 as [DayOffset]
	  ,[CAS] as [MeasurementSequence]
	  ,0 as [MeasurementTime]
	  ,cast(convert(varchar,convert(date,[DATUM],104),112)as bigint)as [IDDate]
	  ,[OOM_ID] as [IDConsuptionPlace]
	  ,1 as [Source]
	  ,[DRUH_MERANIA] as [Type]
	  
--select count(1)
  FROM [SAM].[dbo].[20141215_RAJA_ASFEU_OOM_CASOVERADY_6] c 
  inner join [SAM].[dbo].[20141215_RAJA_ASFEU_OOM_DIAGRAMY_0] d  on c.[DIAGRAM_ID]= d.[KOD]

  where [DRUH_MERANIA] = 'O'
  --d 10897574
  --D (9811598 row(s) affected)
   --   22056220
  --'O' 6577965
  --'D'  700601
  --0
  --1
  --2 (91433225 row(s) affected)
  --3 (91436297 row(s) affected)
  --4 (87485934 row(s) affected)
  --5 (91672976 row(s) affected)
  --6 (90167504 row(s) affected)
  join [SAM].[dbo].[20141215_RAJA_ASFEU_OOM_CASOVERADY_0] c 

  --
  CREATE UNIQUE INDEX pk_diagram
ON [dbo].[20141215_RAJA_ASFEU_OOM_DIAGRAMY_0] ([KOD])

USE [SAM]
begin tran t1;

  CREATE NONCLUSTERED INDEX pk_diagram_cas_rady0
ON [dbo].[20141215_RAJA_ASFEU_OOM_CASOVERADY_0] ([DIAGRAM_ID])INCLUDE ([CAS],[MNOZSTVO])

--  CREATE NONCLUSTERED INDEX pk_diagram_cas_rady1
--ON [dbo].[20141215_RAJA_ASFEU_OOM_CASOVERADY_1] ([DIAGRAM_ID])INCLUDE ([CAS],[MNOZSTVO])

CREATE NONCLUSTERED INDEX pk_diagram_cas_rady2
ON [dbo].[20141215_RAJA_ASFEU_OOM_CASOVERADY_2] ([DIAGRAM_ID])INCLUDE ([CAS],[MNOZSTVO])

CREATE NONCLUSTERED INDEX pk_diagram_cas_rady3
ON [dbo].[20141215_RAJA_ASFEU_OOM_CASOVERADY_3] ([DIAGRAM_ID])INCLUDE ([CAS],[MNOZSTVO])

CREATE NONCLUSTERED INDEX pk_diagram_cas_rady4
ON [dbo].[20141215_RAJA_ASFEU_OOM_CASOVERADY_4] ([DIAGRAM_ID])INCLUDE ([CAS],[MNOZSTVO])

CREATE NONCLUSTERED INDEX pk_diagram_cas_rady5
ON [dbo].[20141215_RAJA_ASFEU_OOM_CASOVERADY_5] ([DIAGRAM_ID])INCLUDE ([CAS],[MNOZSTVO])

CREATE NONCLUSTERED INDEX pk_diagram_cas_rady6
ON [dbo].[20141215_RAJA_ASFEU_OOM_CASOVERADY_6] ([DIAGRAM_ID])INCLUDE ([CAS],[MNOZSTVO])

commit tran t1;


alter table [dbo].[20141215_RAJA_ASFEU_OOM_CASOVERADY_0] 
alter column [DIAGRAM_ID] int null

alter table [dbo].[Consuptions]
add  [Type] varchar(2) null

truncate table [dbo].[ConsuptionsHourly]



insert into [dbo].[ConsuptionsHourly]
([Amount]
      ,[DayOffset]
      ,[MeasurementSequence]
      ,[MeasurementTime]
      ,[IDDate]
      ,[IDConsuptionPlace]
      ,[source]
      ,[Type])

SELECT
sum([Amount]),0,([MeasurementSequence]+45)/60,([MeasurementSequence]+45)/60,[IDDate],[IDConsuptionPlace],2,[Type]

/*[IDConsuption]
      ,[Amount]
      ,[DayOffset]
      ,[MeasurementSequence]
      ,[MeasurementTime]
      ,[IDDate]
      ,[IDConsuptionPlace]
      ,[source]
      ,[Type]*/
  FROM [SAM].[dbo].[Consuptions]
  group by [IDConsuptionPlace],[IDDate],[Type],([MeasurementSequence]+45)/60;
 
  truncate table[dbo].[ConsuptionsDaily];

  insert into [dbo].[ConsuptionsDaily]
([Amount]
      ,[DayOffset]
      ,[MeasurementSequence]
      ,[MeasurementTime]
      ,[IDDate]
      ,[IDConsuptionPlace]
      ,[source]
      ,[Type])
  SELECT
sum([Amount]),0,0,0,[IDDate],[IDConsuptionPlace],1,[Type]

/*[IDConsuption]
      ,[Amount]
      ,[DayOffset]
      ,[MeasurementSequence]
      ,[MeasurementTime]
      ,[IDDate]
      ,[IDConsuptionPlace]
      ,[source]
      ,[Type]*/
  FROM [SAM].[dbo].[Consuptions]
  group by [IDConsuptionPlace],[IDDate],[Type]
  ;
  alter table  [dbo].[Weathers]
add  IDweather int not null IDENTITY(1, 1);

  insert into [dbo].[Weathers]
  ([IDDate]
  ,[MeasurementTime]
  ,[IDLocation]
  ,[locationName]
  ,[dt]
  ,[atmosphericPressure]
  ,[rainfall]
  ,[windSpeed]
  ,[surfaceTemperature]
  ,[solarShine]
  ,[relativeHumidity]
  )
  select
  [IDDate]
  , [Sequence]
  ,[Lokalita_ID]
  ,[Lokalita]
  ,[datum]
  ,[tlak_pr]
  ,[zra_uhrn]
  ,[vie_vp_rych]
  ,[t_pr]
  ,[sln_trv]
  ,[vlh_pr]
  from [dbo].[Pocasie]

CREATE UNIQUE INDEX ix_weather
ON [dbo].[Weathersdaily] ([IDDate],[MeasurementTime],[IDLocation])




update cp
set cp.CityName = po.[Názov pošty]
set cp.DistrictName = po.[Okres]
[ConsuptionPlaces] cp 
--inner join [pscUni] pc on pc.[Column 0] = cp.ZipCode
inner join [POBoxyUni] po on po.[PSČ pre P O BOXy] = cp.ZipCode

CREATE NONCLUSTERED INDEX IX_consumption
ON [dbo].[Consuptions]([IDDate],[MeasurementTime],[IDConsuptionPlace],[Type])


CREATE NONCLUSTERED INDEX IX_consumption_hourly
ON [dbo].[ConsuptionsHourly]([IDDate],[MeasurementTime],[IDConsuptionPlace])

CREATE UNIQUE INDEX pk_placeWeather
ON [dbo].[PlaceWeather]([IDDistrict])

select 
	c.Amount
	,c.IDDate
	,c.MeasurementTime
	,cp.DistrictName
	,cp.ZipCode
	,cp.CityName
	,w.atmosphericPressure
	,w.rainfall
	,w.windSpeed
	,w.surfaceTemperature
	,w.solarShine
	,w.relativeHumidity
	,pw.IDLocation
	 
from [dbo].[ConsuptionsHourly] c
inner join [dbo].[ConsuptionPlaces] cp on cp.IDConsuptionPlace = c.IDConsuptionPlace
inner join [dbo].[PlaceWeather] pw on pw.DistrictName=cp.DistrictName
inner join [dbo].[Weathers] w on w.IDDate = c.IDDate and c.MeasurementTime=w.MeasurementTime and pw.IDLocation=w.IDLocation

where c.IDConsuptionPlace = 16387
order by c.IDDate,c.MeasurementTime

ALTER TABLE dbo.Weathers ADD CONSTRAINT
	PK_Weathers PRIMARY KEY CLUSTERED 
	(
	IDweather
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]


ALTER TABLE [dbo].[PlaceWeather] 
alter column [IDDistrict] int not null

ALTER TABLE [dbo].[PlaceWeather] 
ADD CONSTRAINT
	PK_PlaceWeathers PRIMARY KEY CLUSTERED 
	(
	[IDDistrict]
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]


SELECT r.value, f.* FROM sys.partition_functions   f
INNER JOIN sys.partition_range_values   r ON f.function_id = r.function_id 
WHERE f.name = 'date'

SELECT t.object_id, t.name, p.partition_id, p.partition_number, p.rows FROM sys.partitions AS p
INNER JOIN sys.tables AS t  ON  p.object_id = t.object_id

SELECT *
FROM sys.partition_schemes ps
INNER JOIN sys.partition_functions pf ON pf.function_id=ps.function_id

INNER JOIN sys.partition_range_values prf ON pf.function_id=prf.function_id

select * from sys.partition_range_values prf

;

select * 
into WeathersDaily
from Weathers

where 1=2

ALTER TABLE dbo.WeathersDaily ADD CONSTRAINT
	PK_WeathersDaily PRIMARY KEY CLUSTERED 
	(
	IDweather
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

insert into dbo.WeathersDaily
(
	[IDDate]
      ,[atmosphericPressure]
      ,[rainfall]
      ,[windSpeed]
      ,[surfaceTemperature]
      ,[solarShine]
      ,[relativeHumidity]
      ,[IDLocation]
      ,[locationName]
      ,[MeasurementTime]
      ,[dt]
	  )

select 
[IDDate]
   ,avg([atmosphericPressure])
   , sum([rainfall])
   , avg([windSpeed])
   , avg([surfaceTemperature])
   , sum([solarShine])
   , avg([relativeHumidity])
   , [IDLocation]
   ,[locationName]
   ,-1
   ,CONVERT (datetime,convert(char(8),[IDDate]))
  FROM [SAM].[dbo].[Weathers]
  group by [IDDate],[IDLocation]
   ,[locationName]
order by 
[IDLocation],[IDDate]

/*model*/
truncate table [ConsuptionModelHourly]

 insert into [dbo].[ConsuptionModelDaily]
([Amount]
      ,[MeasurementTime]
      ,[IDDate]
      ,[IDDistrict]
      ,[source]
      ,[Type])
	  
  SELECT
round(sum([Amount]),0)as Amount,0 as MeasurementTime,[IDDate]as IDDate,pw.IDDistrict as [IDDistrict],1 as [source],'S' as[Type]

/*[IDConsuption]
      ,[Amount]
      ,[DayOffset]
      ,[MeasurementSequence]
      ,[MeasurementTime]
      ,[IDDate]
      ,[IDConsuptionPlace]
      ,[source]
      ,[Type]*/
--into [dbo].[ConsuptionModelDaily]
  FROM [SAM].[dbo].[ConsuptionsDaily] c
  inner join [dbo].[ConsuptionPlaces] cp on cp.IDConsuptionPlace = c.IDConsuptionPlace
  inner join [dbo].[PlaceWeather] pw on pw.DistrictName=cp.DistrictName
  group by [IDDate],[Type],pw.IDDistrict
  order by pw.IDDistrict,[IDDate]
  ;
  --(25056 row(s) affected)
insert into [dbo].[ConsuptionModelDaily]
    SELECT
round(avg([Amount]),0)as Amount,0 as MeasurementTime,[IDDate]as IDDate,pw.IDDistrict as [IDDistrict],1 as [source],'A' as[Type]

/*[IDConsuption]
      ,[Amount]
      ,[DayOffset]
      ,[MeasurementSequence]
      ,[MeasurementTime]
      ,[IDDate]
      ,[IDConsuptionPlace]
      ,[source]
      ,[Type]*/

  FROM [SAM].[dbo].[ConsuptionsDaily] c
  inner join [dbo].[ConsuptionPlaces] cp on cp.IDConsuptionPlace = c.IDConsuptionPlace
  inner join [dbo].[PlaceWeather] pw on pw.DistrictName=cp.DistrictName
  group by [IDDate],[Type],pw.IDDistrict
  order by pw.IDDistrict,[IDDate]
  ;
  truncate table [ConsuptionModelHourly]
insert into [dbo].[ConsuptionModelHourly] --(704928 row(s) affected)
    SELECT
round(sum([Amount]),0)as Amount,c.MeasurementTime as MeasurementTime,[IDDate]as IDDate,pw.IDDistrict as [IDDistrict],1 as [source],'S' as[Type]

/*[IDConsuption]
      ,[Amount]
      ,[DayOffset]
      ,[MeasurementSequence]
      ,[MeasurementTime]
      ,[IDDate]
      ,[IDConsuptionPlace]
      ,[source]
      ,[Type]*/
	
  FROM [SAM].[dbo].[ConsuptionsHourly] c
  inner join [dbo].[ConsuptionPlaces] cp on cp.IDConsuptionPlace = c.IDConsuptionPlace
  inner join [dbo].[PlaceWeather] pw on pw.DistrictName=cp.DistrictName
  group by [IDDate],[Type],pw.IDDistrict,c.MeasurementTime
  order by pw.IDDistrict,[IDDate],c.MeasurementTime
  ;
  --(29372 row(s) affected)

insert into [dbo].[ConsuptionModelHourly]--(704928 row(s) affected)
    SELECT
round(avg([Amount]),0)as Amount,c.MeasurementTime as MeasurementTime,[IDDate]as IDDate,pw.IDDistrict as [IDDistrict],1 as [source],'A' as[Type]

/*[IDConsuption]
      ,[Amount]
      ,[DayOffset]
      ,[MeasurementSequence]
      ,[MeasurementTime]
      ,[IDDate]
      ,[IDConsuptionPlace]
      ,[source]
      ,[Type]*/

  FROM [SAM].[dbo].[ConsuptionsHourly] c
  inner join [dbo].[ConsuptionPlaces] cp on cp.IDConsuptionPlace = c.IDConsuptionPlace
  inner join [dbo].[PlaceWeather] pw on pw.DistrictName=cp.DistrictName
  group by [IDDate],[Type],pw.IDDistrict,c.MeasurementTime
  order by pw.IDDistrict,[IDDate],c.MeasurementTime
  ;
  --(601344 row(s) affected)
  alter table [dbo].[Weathers]
  alter column IDLocation int not null;
    alter table [dbo].[PlaceWeather]
  alter column IDLocation int not null;


ALTER TABLE [dbo].[ConsuptionModelHourly]
ADD [IDConsuption] INT IDENTITY(1,1)
GO
-- add new primary key constraint on new column   
ALTER TABLE [dbo].[ConsuptionModelHourly]
ADD CONSTRAINT PK_modelhourly
PRIMARY KEY CLUSTERED ([IDConsuption])
GO

SELECT 
*
    FROM   [dbo].ConsuptionPlaces AS [Extent1]
    inner join [SAM].[dbo].[PlaceWeather] pw on pw.DistrictName= [Extent1].DistrictName
	
    WHERE 1=1
	and  pw.IDDistrict = 62
	and [Extent1].IDConsuptionPlace = 24

    ORDER BY  [Extent1].[IDDate] ASC

{SELECT 
    [Extent1].[MeasurementTime] AS [MeasurementTime], 
    [Extent1].[Amount] AS [Amount], 
    [Extent2].[IDDistrict] AS [IDDistrict], 
    [Extent2].[IDLocation] AS [IDLocation], 
    [Extent1].[IDDate] AS [IDDate], 
    [Extent3].[surfaceTemperature] AS [surfaceTemperature], 
    [Extent3].[rainfall] AS [rainfall], 
    [Extent3].[windSpeed] AS [windSpeed], 
    [Extent3].[relativeHumidity] AS [relativeHumidity], 
    [Extent3].[solarShine] AS [solarShine]
    FROM   [dbo].[ConsuptionModelDaily] AS [Extent1]
    INNER JOIN [dbo].[PlaceWeather] AS [Extent2] ON [Extent1].[IDDistrict] = [Extent2].[IDDistrict]
    INNER JOIN [dbo].[WeathersDaily] AS [Extent3] ON ([Extent1].[IDDate] = [Extent3].[IDDate]) AND ([Extent2].[IDLocation] = [Extent3].[IDLocation])
    WHERE ('S' = [Extent1].[Type]) AND (62 = [Extent2].[IDDistrict])
    ORDER BY [Extent2].[IDDistrict] ASC, [Extent1].[IDDate] ASC}


	SELECT 
*
    FROM    [dbo].[ConsuptionsDaily] AS [Extent1]
    INNER JOIN [dbo].[ConsuptionPlaces] AS [Extent2] ON [Extent1].[IDConsuptionPlace] = [Extent2].[IDConsuptionPlace]
    INNER JOIN [dbo].[PlaceWeather] AS [Extent3] ON ([Extent2].[DistrictName] = [Extent3].[DistrictName]) OR (([Extent2].[DistrictName] IS NULL) AND ([Extent3].[DistrictName] IS NULL))
   -- INNER JOIN [dbo].[WeathersDaily] AS [Extent4] ON ([Extent1].[IDDate] = [Extent4].[IDDate]) AND ([Extent1].[MeasurementTime] = [Extent4].[MeasurementTime]) AND ([Extent3].[IDLocation] = [Extent4].[IDLocation])
    WHERE Amount>10000 and IDDistrict =62
	--[Extent2].IDConsuptionPlace = 1622
	order by [Extent1].IDDate