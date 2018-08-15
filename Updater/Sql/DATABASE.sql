
DROP PROCEDURE IF EXISTS [dbo].[AddOrUpdate_Games]
DROP PROCEDURE IF EXISTS [dbo].[AddOrUpdate_Teams]
DROP PROCEDURE IF EXISTS [dbo].[AddOrUpdate_Stages]
DROP PROCEDURE IF EXISTS [dbo].[AddOrUpdate_Seasons]
DROP PROCEDURE IF EXISTS [dbo].[AddOrUpdate_Leagues]
DROP PROCEDURE IF EXISTS [dbo].[AddOrUpdate_Countries]
GO
DROP TYPE IF EXISTS [dbo].[GamesTable]
DROP TYPE IF EXISTS [dbo].[TeamsTable]
DROP TYPE IF EXISTS [dbo].[StagesTable]
DROP TYPE IF EXISTS [dbo].[SeasonsTable]
DROP TYPE IF EXISTS [dbo].[LeaguesTable]
DROP TYPE IF EXISTS [dbo].[CountriesTable]
GO
DROP TABLE IF EXISTS [dbo].[Games]
DROP TABLE IF EXISTS [dbo].[Teams]
DROP TABLE IF EXISTS [dbo].[Stages]
DROP TABLE IF EXISTS [dbo].[Seasons]
DROP TABLE IF EXISTS [dbo].[Leagues]
DROP TABLE IF EXISTS [dbo].[Countries]
GO

/****** Object:  Table [dbo].[Countries]    Script Date: 13.08.2018 1:59:37 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Countries](
	[Id] [nvarchar](32) NOT NULL,
	[XCountryId] [nvarchar](24) NOT NULL,
	[Title] [nvarchar](128) NULL,
	[XBetId] [int] NULL,
 CONSTRAINT [PK_Countries] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[Leagues](
	[Id] [nvarchar](32) NOT NULL,
	[XCountryId] [nvarchar](24) NOT NULL,
	[XLeagueId] [nvarchar](24) NOT NULL,
	[Title] [nvarchar](128) NULL,
	[ParentId] [nvarchar](32) NOT NULL
 CONSTRAINT [PK_Leagues] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Leagues]  WITH CHECK ADD  CONSTRAINT [FK_Leagues_Countries] FOREIGN KEY([ParentId])
REFERENCES [dbo].[Countries] ([Id])
GO

ALTER TABLE [dbo].[Leagues] CHECK CONSTRAINT [FK_Leagues_Countries]
GO

CREATE TABLE [dbo].[Seasons](
	[Id] [nvarchar](32) NOT NULL,
	[XCountryId] [nvarchar](24) NOT NULL,
	[XLeagueId] [nvarchar](24) NOT NULL,
	[XSeasonId] [nvarchar](24) NOT NULL,
	[Title] [nvarchar](128) NULL,
	[ParentId] [nvarchar](32) NOT NULL
	
 CONSTRAINT [PK_Seasons] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Seasons]  WITH CHECK ADD  CONSTRAINT [FK_Seasons_Leagues] FOREIGN KEY([ParentId])
REFERENCES [dbo].[Leagues] ([Id])
GO

ALTER TABLE [dbo].[Seasons] CHECK CONSTRAINT [FK_Seasons_Leagues]
GO

CREATE TABLE [dbo].[Stages](
	[Id] [nvarchar](32) NOT NULL,
	[XCountryId] [nvarchar](24) NOT NULL,
	[XLeagueId] [nvarchar](24) NOT NULL,
	[XSeasonId] [nvarchar](24) NOT NULL,
	[XStageId] [nvarchar](24) NOT NULL,
	[Title] [nvarchar](128) NULL,
	[ParentId] [nvarchar](32) NOT NULL
	
 CONSTRAINT [PK_Stages] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Stages]  WITH CHECK ADD  CONSTRAINT [FK_Stages_Seasons] FOREIGN KEY([ParentId])
REFERENCES [dbo].[Seasons] ([Id])
GO

ALTER TABLE [dbo].[Stages] CHECK CONSTRAINT [FK_Stages_Seasons]
GO

CREATE TABLE [dbo].[Teams](
	[Id] [nvarchar](32) NOT NULL,
	[XTeamId] [nvarchar](24) NOT NULL,	
	[Title] [nvarchar](128) NULL
 CONSTRAINT [PK_Teams] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

-- ==================================================================================================================================

CREATE TYPE [dbo].[CountriesTable] AS TABLE(
	[Id] [nvarchar](32) NOT NULL,
	[XCountryId] [nvarchar](24) NOT NULL,
	[Title] [nvarchar](128) NULL,
	[XBetId] [int] NULL
)
GO

CREATE TYPE [dbo].[LeaguesTable] AS TABLE(
	[Id] [nvarchar](32) NOT NULL,
	[XCountryId] [nvarchar](24) NOT NULL,
	[XLeagueId] [nvarchar] (24) NOT NULL,
	[Title] [nvarchar](128) NULL,	
	[ParentId] [nvarchar](32) NOT NULL
)
GO

CREATE TYPE [dbo].[SeasonsTable] AS TABLE(
	[Id] [nvarchar](32) NOT NULL,
	[XCountryId] [nvarchar](24) NOT NULL,
	[XLeagueId] [nvarchar] (24) NOT NULL,
	[XSeasonId] [nvarchar] (24) NOT NULL,
	[Title] [nvarchar](128) NULL,	
	[ParentId] [nvarchar](32) NOT NULL
)
GO

CREATE TYPE [dbo].[StagesTable] AS TABLE(
	[Id] [nvarchar](32) NOT NULL,
	[XCountryId] [nvarchar](24) NOT NULL,
	[XLeagueId] [nvarchar] (24) NOT NULL,
	[XSeasonId] [nvarchar] (24) NOT NULL,
	[XStageId] [nvarchar] (24) NOT NULL,
	[Title] [nvarchar](128) NULL,	
	[ParentId] [nvarchar](32) NOT NULL
)
GO

CREATE TYPE [dbo].[TeamsTable] AS TABLE(
	[Id] [nvarchar](32) NOT NULL,
	[XTeamId] [nvarchar](24) NOT NULL,
	[Title] [nvarchar](128) NULL
)
GO

--===================================================================================================================================

CREATE PROCEDURE [dbo].[AddOrUpdate_Countries] 
	(@CountriesInput [dbo].[CountriesTable] READONLY,
	 @Inserted int = 0 OUT,
	 @Updated  int = 0 OUT)
AS
BEGIN 
	SET NOCOUNT ON;    

	DECLARE @id [nvarchar](32);
    DECLARE cur CURSOR LOCAL FAST_FORWARD FOR SELECT Id FROM @CountriesInput;

    OPEN cur;
    FETCH NEXT FROM cur INTO @id;
    
	WHILE (@@FETCH_STATUS = 0)
    BEGIN
		IF (NOT EXISTS(SELECT 1 Id FROM [Countries] WHERE Id = @id))    
			BEGIN 
				INSERT INTO [Countries] ([Id],[XCountryId],[Title],[XBetId]) 
				SELECT				    Id, XCountryId,  Title, XBetId
				FROM @CountriesInput	WHERE Id = @id;
				SET @Inserted = @Inserted + 1;
			END;
		ELSE 
			BEGIN 
				UPDATE [dbo].[Countries]
				SET
					[XCountryId]= c.XCountryId,
					[Title] = c.Title
					FROM @CountriesInput c WHERE Countries.Id = @id AND c.Id = @id
				SET @Updated = @Updated + 1;
			END;
       FETCH NEXT FROM cur INTO @id;
    END;

    CLOSE cur;
    DEALLOCATE cur;
END
GO

CREATE PROCEDURE [dbo].[AddOrUpdate_Leagues] 
	(@LeaguesInput [dbo].[LeaguesTable] READONLY,
	 @Inserted int = 0 OUT,
	 @Updated  int = 0 OUT)
AS
BEGIN 
	SET NOCOUNT ON;    

	DECLARE @id [nvarchar](32);
    DECLARE cur CURSOR LOCAL FAST_FORWARD FOR SELECT Id FROM @LeaguesInput;

    OPEN cur;
    FETCH NEXT FROM cur INTO @id;
    
	WHILE (@@FETCH_STATUS = 0)
    BEGIN
		IF (NOT EXISTS(SELECT 1 Id FROM [Leagues] WHERE Id = @id))    
			BEGIN 
				INSERT INTO [dbo].[Leagues] 		   
					([Id]
					,[XCountryId]
					,[XLeagueId]
					,[ParentId]					
					,[Title])
				SELECT
					 Id
					,XCountryId
					,XLeagueId
					,ParentId					
					,Title
				FROM @LeaguesInput	WHERE Id = @id;
				SET @Inserted = @Inserted + 1;
			END;
		ELSE 
			BEGIN 
				UPDATE [dbo].[Leagues]
					SET
						[XCountryId] = l.XCountryId,						
						[XLeagueId]  = l.XLeagueId,
						[Title]		 = l.Title,
						[ParentId]	 = l.ParentId
					FROM @LeaguesInput l WHERE Leagues.Id = @id AND l.Id = @id
				SET @Updated = @Updated + 1;
			END;
       FETCH NEXT FROM cur INTO @id;
    END;

    CLOSE cur;
    DEALLOCATE cur;
	END
GO

CREATE PROCEDURE [dbo].[AddOrUpdate_Seasons] 
	(@SeasonsInput [dbo].[SeasonsTable] READONLY,
	 @Inserted int = 0 OUT,
	 @Updated  int = 0 OUT)
AS
BEGIN 
	SET NOCOUNT ON;    

	DECLARE @id [nvarchar](32);
    DECLARE cur CURSOR LOCAL FAST_FORWARD FOR SELECT Id FROM @SeasonsInput;

    OPEN cur;
    FETCH NEXT FROM cur INTO @id;
    
	WHILE (@@FETCH_STATUS = 0)
    BEGIN
		IF (NOT EXISTS(SELECT 1 Id FROM [Seasons] WHERE Id = @id))    
			BEGIN 
				INSERT INTO [dbo].[Seasons] 		   
					([Id]
					,[XCountryId]
					,[XLeagueId]
					,[XSeasonId]
					,[Title]
					,[ParentId])
				SELECT
					 Id
					,XCountryId
					,XLeagueId
					,XSeasonId					
					,Title
					,ParentId
				FROM @SeasonsInput	WHERE Id = @id;
				SET @Inserted = @Inserted + 1;
			END;
		ELSE 
			BEGIN 
				UPDATE [dbo].[Seasons]
					SET	
						[XCountryId]	= l.XCountryId
					   ,[XLeagueId]		= l.XLeagueId
					   ,[XSeasonId]		= l.XSeasonId
					   ,[Title]			= l.Title
					   ,[ParentId]		= l.ParentId
					FROM @SeasonsInput l WHERE Seasons.Id = @id AND l.Id = @id
				SET @Updated = @Updated + 1;
			END;
       FETCH NEXT FROM cur INTO @id;
    END;

    CLOSE cur;
    DEALLOCATE cur;
	END
GO

CREATE PROCEDURE [dbo].[AddOrUpdate_Stages] 
	(@StagesInput [dbo].[StagesTable] READONLY,
	 @Inserted int = 0 OUT,
	 @Updated  int = 0 OUT)
AS
BEGIN 
	SET NOCOUNT ON;    

	DECLARE @id [nvarchar](32);
    DECLARE cur CURSOR LOCAL FAST_FORWARD FOR SELECT Id FROM @StagesInput;

    OPEN cur;
    FETCH NEXT FROM cur INTO @id;
    
	WHILE (@@FETCH_STATUS = 0)
    BEGIN
		IF (NOT EXISTS(SELECT 1 Id FROM [Stages] WHERE Id = @id))    
			BEGIN 
				INSERT INTO [dbo].[Stages] 		   
					([Id]
					,[XCountryId]
					,[XLeagueId]
					,[XSeasonId]
					,[XStageId]					
					,[Title]
					,[ParentId])
				SELECT
					 Id
					,XCountryId
					,XLeagueId
					,XSeasonId
					,XStageId				
					,Title
					,ParentId
				FROM @StagesInput	WHERE Id = @id;
				SET @Inserted = @Inserted + 1;
			END;
		ELSE 
			BEGIN 
				UPDATE [dbo].[Stages]
					SET
						[XCountryId] = l.XCountryId
					   ,[XLeagueId]	 = l.XLeagueId
					   ,[XSeasonId]	 = l.XSeasonId
					   ,[XStageId]	 = l.XStageId
					   ,[Title]		 = l.Title
					   ,[ParentId]	 = l.ParentId
					FROM @StagesInput l WHERE Stages.Id = @id AND l.Id = @id
				SET @Updated = @Updated + 1;
			END;
       FETCH NEXT FROM cur INTO @id;
    END;

    CLOSE cur;
    DEALLOCATE cur;
	END
GO

CREATE PROCEDURE [dbo].[AddOrUpdate_Teams] 
	(@TeamsInput [dbo].[TeamsTable] READONLY,
	 @Inserted int = 0 OUT,
	 @Updated  int = 0 OUT)
AS
BEGIN 
	SET NOCOUNT ON;    

	DECLARE @id [nvarchar](32);
    DECLARE cur CURSOR LOCAL FAST_FORWARD FOR SELECT Id FROM @TeamsInput;

    OPEN cur;
    FETCH NEXT FROM cur INTO @id;
    
	WHILE (@@FETCH_STATUS = 0)
    BEGIN
		IF (NOT EXISTS(SELECT 1 Id FROM [Teams] WHERE Id = @id))    
			BEGIN 
				INSERT INTO [Teams] ([Id]
									,[XTeamId]
									,[Title])
				SELECT				 Id
									,XTeamId
									,Title 
				FROM @TeamsInput	WHERE Id = @id;
				SET @Inserted = @Inserted + 1;
			END;
		ELSE 
			BEGIN 
				UPDATE [dbo].[Teams]
				SET
					[XTeamId]	= c.XTeamId,
					[Title]		= c.Title
					FROM @TeamsInput c WHERE Teams.Id = @id AND c.Id = @id
				SET @Updated = @Updated + 1;
			END;
       FETCH NEXT FROM cur INTO @id;
    END;

    CLOSE cur;
    DEALLOCATE cur;
END
