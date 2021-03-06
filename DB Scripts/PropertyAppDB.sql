USE [master]
GO
/****** Object:  Database [PropertiesApp]    Script Date: 10-04-2021 01:23:12 ******/
CREATE DATABASE [PropertiesApp]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'PropertiesApp', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.SQLEXPRESS\MSSQL\DATA\PropertiesApp.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'PropertiesApp_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.SQLEXPRESS\MSSQL\DATA\PropertiesApp_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO
ALTER DATABASE [PropertiesApp] SET COMPATIBILITY_LEVEL = 140
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [PropertiesApp].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [PropertiesApp] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [PropertiesApp] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [PropertiesApp] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [PropertiesApp] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [PropertiesApp] SET ARITHABORT OFF 
GO
ALTER DATABASE [PropertiesApp] SET AUTO_CLOSE ON 
GO
ALTER DATABASE [PropertiesApp] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [PropertiesApp] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [PropertiesApp] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [PropertiesApp] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [PropertiesApp] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [PropertiesApp] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [PropertiesApp] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [PropertiesApp] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [PropertiesApp] SET  ENABLE_BROKER 
GO
ALTER DATABASE [PropertiesApp] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [PropertiesApp] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [PropertiesApp] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [PropertiesApp] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [PropertiesApp] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [PropertiesApp] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [PropertiesApp] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [PropertiesApp] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [PropertiesApp] SET  MULTI_USER 
GO
ALTER DATABASE [PropertiesApp] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [PropertiesApp] SET DB_CHAINING OFF 
GO
ALTER DATABASE [PropertiesApp] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [PropertiesApp] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [PropertiesApp] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [PropertiesApp] SET QUERY_STORE = OFF
GO
USE [PropertiesApp]
GO
/****** Object:  Table [dbo].[Address]    Script Date: 10-04-2021 01:23:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Address](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[PropertyId] [int] NOT NULL,
	[address1] [varchar](200) NULL,
	[address2] [varchar](200) NULL,
	[city] [varchar](100) NULL,
	[country] [varchar](100) NULL,
	[district] [varchar](100) NULL,
	[state] [varchar](100) NULL,
	[zip] [varchar](100) NULL,
	[zip_plus4] [varchar](100) NULL,
 CONSTRAINT [PK_Address] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Properties]    Script Date: 10-04-2021 01:23:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Properties](
	[id] [int] NOT NULL,
	[year_built] [int] NULL,
	[list_price] [float] NULL,
	[monthly_rent] [float] NULL,
	[gross_yield] [float] NULL,
 CONSTRAINT [PK_Properties] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  StoredProcedure [dbo].[usp_GetPropertyIds]    Script Date: 10-04-2021 01:23:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



CREATE   PROCEDURE [dbo].[usp_GetPropertyIds] 
AS
BEGIN
	SELECT P.[Id] FROM [Properties] P JOIN [Address] A ON P.id = A.PropertyId
END
GO
/****** Object:  StoredProcedure [dbo].[usp_saveProperty]    Script Date: 10-04-2021 01:23:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE   PROCEDURE [dbo].[usp_saveProperty] 
	@Id INT,
	@year_built INT,
	@list_price float,
	@monthly_rent float,
	@gross_yield float,
	@address1 VARCHAR(200) = null,
	@address2 VARCHAR(200) = null,
	@city VARCHAR(100) = null,
	@country VARCHAR(100) = null,
	@district VARCHAR(100) = null,
	@state VARCHAR(100) = null,
	@zip VARCHAR(100) = null,
	@zip_plus4 VARCHAR(100) = null
AS
BEGIN
	 DECLARE @PropertyDataExist INT, @AddressExist INT

	 SELECT @PropertyDataExist = COUNT(1) FROM [Properties] WHERE [id] = @Id
	 SELECT @AddressExist = COUNT(1) FROM [Address] WHERE [PropertyId] = @Id

	 IF @PropertyDataExist = 0
	 BEGIN
		INSERT INTO Properties([id],[year_built],[list_price],[monthly_rent],[gross_yield]) 
		VALUES(@Id,@year_built,@list_price,@monthly_rent,@gross_yield)
	 END

	 IF @AddressExist = 0
	 BEGIN
		INSERT INTO [Address]([PropertyId],[address1],[address2],[city],[country],[district],[state],[zip],[zip_plus4])
		VALUES(@Id ,@address1,@address2,@city,@country,@district,@state,@zip,@zip_plus4)
	 END

END
GO
USE [master]
GO
ALTER DATABASE [PropertiesApp] SET  READ_WRITE 
GO
