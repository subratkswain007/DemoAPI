USE [master]
GO
/****** Object:  Database [DemoDB]    Script Date: 12/6/2024 6:21:12 PM ******/
CREATE DATABASE [DemoDB]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'DemoDB', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\DemoDB.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'DemoDB_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\DemoDB_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO
ALTER DATABASE [DemoDB] SET COMPATIBILITY_LEVEL = 160
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [DemoDB].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [DemoDB] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [DemoDB] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [DemoDB] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [DemoDB] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [DemoDB] SET ARITHABORT OFF 
GO
ALTER DATABASE [DemoDB] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [DemoDB] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [DemoDB] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [DemoDB] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [DemoDB] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [DemoDB] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [DemoDB] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [DemoDB] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [DemoDB] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [DemoDB] SET  DISABLE_BROKER 
GO
ALTER DATABASE [DemoDB] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [DemoDB] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [DemoDB] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [DemoDB] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [DemoDB] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [DemoDB] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [DemoDB] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [DemoDB] SET RECOVERY FULL 
GO
ALTER DATABASE [DemoDB] SET  MULTI_USER 
GO
ALTER DATABASE [DemoDB] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [DemoDB] SET DB_CHAINING OFF 
GO
ALTER DATABASE [DemoDB] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [DemoDB] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [DemoDB] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [DemoDB] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'DemoDB', N'ON'
GO
ALTER DATABASE [DemoDB] SET QUERY_STORE = ON
GO
ALTER DATABASE [DemoDB] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [DemoDB]
GO
/****** Object:  Table [dbo].[Department]    Script Date: 12/6/2024 6:21:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Department](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[DepartmentName] [varchar](100) NULL,
	[Category] [varchar](50) NOT NULL,
 CONSTRAINT [PK_Department] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Employee]    Script Date: 12/6/2024 6:21:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Employee](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](100) NULL,
	[DepartmentId] [int] NOT NULL,
	[Address] [nvarchar](100) NULL,
	[Salary] [int] NULL,
 CONSTRAINT [PK_Employee] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Department] ON 

INSERT [dbo].[Department] ([Id], [DepartmentName], [Category]) VALUES (1, N'Sports', N'Tier2')
INSERT [dbo].[Department] ([Id], [DepartmentName], [Category]) VALUES (2, N'Entertainment', N'Tier3')
INSERT [dbo].[Department] ([Id], [DepartmentName], [Category]) VALUES (3, N'Development', N'Tier1')
INSERT [dbo].[Department] ([Id], [DepartmentName], [Category]) VALUES (4, N'Drama', N'Tier3')
INSERT [dbo].[Department] ([Id], [DepartmentName], [Category]) VALUES (5, N'Others', N'Tier4')
SET IDENTITY_INSERT [dbo].[Department] OFF
GO
SET IDENTITY_INSERT [dbo].[Employee] ON 

INSERT [dbo].[Employee] ([Id], [Name], [DepartmentId], [Address], [Salary]) VALUES (1, N'Bibhuti', 3, N'Bhubaneswar', 100000)
INSERT [dbo].[Employee] ([Id], [Name], [DepartmentId], [Address], [Salary]) VALUES (2, N'Subrat', 3, N'Bangalore', 80000)
INSERT [dbo].[Employee] ([Id], [Name], [DepartmentId], [Address], [Salary]) VALUES (3, N'Akshay', 2, N'Mumbai', 5000000)
INSERT [dbo].[Employee] ([Id], [Name], [DepartmentId], [Address], [Salary]) VALUES (4, N'Anupam', 4, N'Delhi', 200000)
INSERT [dbo].[Employee] ([Id], [Name], [DepartmentId], [Address], [Salary]) VALUES (5, N'Virat', 1, N'Pune', 8000000)
INSERT [dbo].[Employee] ([Id], [Name], [DepartmentId], [Address], [Salary]) VALUES (6, N'Rohit', 1, N'Nashik', 7000000)
INSERT [dbo].[Employee] ([Id], [Name], [DepartmentId], [Address], [Salary]) VALUES (7, N'Genda', 5, N'Cuttack', 300000)
INSERT [dbo].[Employee] ([Id], [Name], [DepartmentId], [Address], [Salary]) VALUES (8, N'Ajay', 2, N'Delhi', 6000000)
INSERT [dbo].[Employee] ([Id], [Name], [DepartmentId], [Address], [Salary]) VALUES (10, N'Salman', 2, N'Bandra', 120000000)
INSERT [dbo].[Employee] ([Id], [Name], [DepartmentId], [Address], [Salary]) VALUES (14, N'Pankaj', 4, N'Bihar', 500000)
INSERT [dbo].[Employee] ([Id], [Name], [DepartmentId], [Address], [Salary]) VALUES (22, N'Kartik', 2, N'Bhopal', 2000000)
SET IDENTITY_INSERT [dbo].[Employee] OFF
GO
ALTER TABLE [dbo].[Employee]  WITH CHECK ADD  CONSTRAINT [FK_Employee_Department] FOREIGN KEY([DepartmentId])
REFERENCES [dbo].[Department] ([Id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Employee] CHECK CONSTRAINT [FK_Employee_Department]
GO
USE [master]
GO
ALTER DATABASE [DemoDB] SET  READ_WRITE 
GO
