USE [master]
GO
/****** Object:  Database [VehicleBookingsTest]    Script Date: 2021-12-10 23:01:22 ******/
CREATE DATABASE [VehicleBookingsTest]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'CarRentals', FILENAME = N'C:\Users\souci\Desktop\CarRentals.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'CarRentals_log', FILENAME = N'C:\Users\souci\Desktop\CarRentals_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO
ALTER DATABASE [VehicleBookingsTest] SET COMPATIBILITY_LEVEL = 140
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [VehicleBookingsTest].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [VehicleBookingsTest] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [VehicleBookingsTest] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [VehicleBookingsTest] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [VehicleBookingsTest] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [VehicleBookingsTest] SET ARITHABORT OFF 
GO
ALTER DATABASE [VehicleBookingsTest] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [VehicleBookingsTest] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [VehicleBookingsTest] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [VehicleBookingsTest] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [VehicleBookingsTest] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [VehicleBookingsTest] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [VehicleBookingsTest] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [VehicleBookingsTest] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [VehicleBookingsTest] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [VehicleBookingsTest] SET  DISABLE_BROKER 
GO
ALTER DATABASE [VehicleBookingsTest] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [VehicleBookingsTest] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [VehicleBookingsTest] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [VehicleBookingsTest] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [VehicleBookingsTest] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [VehicleBookingsTest] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [VehicleBookingsTest] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [VehicleBookingsTest] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [VehicleBookingsTest] SET  MULTI_USER 
GO
ALTER DATABASE [VehicleBookingsTest] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [VehicleBookingsTest] SET DB_CHAINING OFF 
GO
ALTER DATABASE [VehicleBookingsTest] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [VehicleBookingsTest] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [VehicleBookingsTest] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [VehicleBookingsTest] SET QUERY_STORE = OFF
GO
USE [VehicleBookingsTest]
GO
/****** Object:  Table [dbo].[Bookings]    Script Date: 2021-12-10 23:01:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Bookings](
	[ReservationId] [int] IDENTITY(1,1) NOT NULL,
	[CustomerId] [int] NOT NULL,
	[VehicleId] [int] NOT NULL,
	[StartMileage] [int] NOT NULL,
	[EndMileage] [int] NULL,
	[StartDate] [varchar](50) NOT NULL,
	[EndDate] [varchar](50) NULL,
	[TotalPrice] [money] NULL,
	[BookingNumber] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Rentals] PRIMARY KEY CLUSTERED 
(
	[ReservationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Categories]    Script Date: 2021-12-10 23:01:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Categories](
	[CategoryId] [int] IDENTITY(1,1) NOT NULL,
	[CategoryType] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Categories] PRIMARY KEY CLUSTERED 
(
	[CategoryId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Customers]    Script Date: 2021-12-10 23:01:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Customers](
	[CustomerId] [int] IDENTITY(1,1) NOT NULL,
	[PersonNumber] [nvarchar](50) NOT NULL,
	[CustomerCreationDate] [nvarchar](50) NULL,
 CONSTRAINT [PK_Customers] PRIMARY KEY CLUSTERED 
(
	[CustomerId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Vehicles]    Script Date: 2021-12-10 23:01:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Vehicles](
	[VehicleId] [int] IDENTITY(1,1) NOT NULL,
	[Mileage] [int] NOT NULL,
	[RegistrationNumber] [nvarchar](50) NOT NULL,
	[CategoryId] [int] NOT NULL,
 CONSTRAINT [PK_Vehicles] PRIMARY KEY CLUSTERED 
(
	[VehicleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Bookings] ON 

INSERT [dbo].[Bookings] ([ReservationId], [CustomerId], [VehicleId], [StartMileage], [EndMileage], [StartDate], [EndDate], [TotalPrice], [BookingNumber]) VALUES (1, 2, 1, 1234, NULL, N'2021-12-04', NULL, NULL, N'123')
INSERT [dbo].[Bookings] ([ReservationId], [CustomerId], [VehicleId], [StartMileage], [EndMileage], [StartDate], [EndDate], [TotalPrice], [BookingNumber]) VALUES (2, 3, 1, 1500, NULL, N'2021-12-04 15:30:14', NULL, NULL, N'18954111fccdd7')
INSERT [dbo].[Bookings] ([ReservationId], [CustomerId], [VehicleId], [StartMileage], [EndMileage], [StartDate], [EndDate], [TotalPrice], [BookingNumber]) VALUES (3, 4, 1, 1500, NULL, N'2021-12-04 19:07:29', NULL, NULL, N'18955f6b1b0266')
INSERT [dbo].[Bookings] ([ReservationId], [CustomerId], [VehicleId], [StartMileage], [EndMileage], [StartDate], [EndDate], [TotalPrice], [BookingNumber]) VALUES (4, 5, 1, 1500, NULL, N'2021-12-04 23:08:37', NULL, NULL, N'1895811ad48350')
INSERT [dbo].[Bookings] ([ReservationId], [CustomerId], [VehicleId], [StartMileage], [EndMileage], [StartDate], [EndDate], [TotalPrice], [BookingNumber]) VALUES (5, 6, 1, 1500, NULL, N'2021-12-04 23:10:43', NULL, NULL, N'18958166213ee0')
INSERT [dbo].[Bookings] ([ReservationId], [CustomerId], [VehicleId], [StartMileage], [EndMileage], [StartDate], [EndDate], [TotalPrice], [BookingNumber]) VALUES (6, 1, 1, 1500, NULL, N'2021-12-04 23:11:31', NULL, NULL, N'18958182a02b5f')
INSERT [dbo].[Bookings] ([ReservationId], [CustomerId], [VehicleId], [StartMileage], [EndMileage], [StartDate], [EndDate], [TotalPrice], [BookingNumber]) VALUES (7, 1, 2, 2000, NULL, N'2021-12-04 23:19:28', NULL, NULL, N'1895829bf41331')
INSERT [dbo].[Bookings] ([ReservationId], [CustomerId], [VehicleId], [StartMileage], [EndMileage], [StartDate], [EndDate], [TotalPrice], [BookingNumber]) VALUES (8, 1, 2, 2000, NULL, N'2021-12-04 23:20:31', NULL, NULL, N'189582c242b480')
INSERT [dbo].[Bookings] ([ReservationId], [CustomerId], [VehicleId], [StartMileage], [EndMileage], [StartDate], [EndDate], [TotalPrice], [BookingNumber]) VALUES (9, 1, 2, 2000, NULL, N'2021-12-04 23:23:09', NULL, NULL, N'1895831db9e1d2')
INSERT [dbo].[Bookings] ([ReservationId], [CustomerId], [VehicleId], [StartMileage], [EndMileage], [StartDate], [EndDate], [TotalPrice], [BookingNumber]) VALUES (1004, 1005, 1, 1500, 2000, N'Dec  3 2021 12:17PM', N'2021-12-07 13:07:36', 2020.0000, N'189781b80f7751')
INSERT [dbo].[Bookings] ([ReservationId], [CustomerId], [VehicleId], [StartMileage], [EndMileage], [StartDate], [EndDate], [TotalPrice], [BookingNumber]) VALUES (1005, 1006, 15, 325, 5000, N'Dec  3 2021 10:02PM', N'2021-12-07 22:18:20', 2005.0000, N'1897d362663e4c')
INSERT [dbo].[Bookings] ([ReservationId], [CustomerId], [VehicleId], [StartMileage], [EndMileage], [StartDate], [EndDate], [TotalPrice], [BookingNumber]) VALUES (1006, 1007, 13, 0, 2000, N'Dec  1 2021 10:23PM', N'2021-12-07 22:28:48', 10800.0000, N'1897d651251e87')
INSERT [dbo].[Bookings] ([ReservationId], [CustomerId], [VehicleId], [StartMileage], [EndMileage], [StartDate], [EndDate], [TotalPrice], [BookingNumber]) VALUES (1007, 2006, 7, 6540, NULL, N'Dec  8 2021  3:10PM', NULL, NULL, N'1899ece5b484d9')
INSERT [dbo].[Bookings] ([ReservationId], [CustomerId], [VehicleId], [StartMileage], [EndMileage], [StartDate], [EndDate], [TotalPrice], [BookingNumber]) VALUES (1008, 2006, 7, 6540, NULL, N'Dec  8 2021 10:40PM', NULL, NULL, N'189a2bd512295a')
INSERT [dbo].[Bookings] ([ReservationId], [CustomerId], [VehicleId], [StartMileage], [EndMileage], [StartDate], [EndDate], [TotalPrice], [BookingNumber]) VALUES (1009, 2006, 7, 6540, NULL, N'Dec  8 2021 10:42PM', NULL, NULL, N'189a2c0a329ee9')
INSERT [dbo].[Bookings] ([ReservationId], [CustomerId], [VehicleId], [StartMileage], [EndMileage], [StartDate], [EndDate], [TotalPrice], [BookingNumber]) VALUES (1010, 2006, 7, 6540, NULL, N'Dec  8 2021 10:43PM', NULL, NULL, N'189a2c2e4b23ac')
INSERT [dbo].[Bookings] ([ReservationId], [CustomerId], [VehicleId], [StartMileage], [EndMileage], [StartDate], [EndDate], [TotalPrice], [BookingNumber]) VALUES (1011, 2006, 7, 6540, NULL, N'Dec  8 2021 10:44PM', NULL, NULL, N'189a2c5b4eed9a')
INSERT [dbo].[Bookings] ([ReservationId], [CustomerId], [VehicleId], [StartMileage], [EndMileage], [StartDate], [EndDate], [TotalPrice], [BookingNumber]) VALUES (1012, 2006, 7, 6540, 5000, N'Dec  8 2021 10:46PM', N'2021-12-10 21:46:24', 1000.0000, N'189a2c9cf87cf2')
SET IDENTITY_INSERT [dbo].[Bookings] OFF
SET IDENTITY_INSERT [dbo].[Categories] ON 

INSERT [dbo].[Categories] ([CategoryId], [CategoryType]) VALUES (1, N'SMALLCAR')
INSERT [dbo].[Categories] ([CategoryId], [CategoryType]) VALUES (2, N'MEDIUMCAR')
INSERT [dbo].[Categories] ([CategoryId], [CategoryType]) VALUES (3, N'TRUCK')
SET IDENTITY_INSERT [dbo].[Categories] OFF
SET IDENTITY_INSERT [dbo].[Customers] ON 

INSERT [dbo].[Customers] ([CustomerId], [PersonNumber], [CustomerCreationDate]) VALUES (1, N'19811103-0535', NULL)
INSERT [dbo].[Customers] ([CustomerId], [PersonNumber], [CustomerCreationDate]) VALUES (2, N'19811103-0536', NULL)
INSERT [dbo].[Customers] ([CustomerId], [PersonNumber], [CustomerCreationDate]) VALUES (3, N'19811103-0537', NULL)
INSERT [dbo].[Customers] ([CustomerId], [PersonNumber], [CustomerCreationDate]) VALUES (4, N'19811103-0538', NULL)
INSERT [dbo].[Customers] ([CustomerId], [PersonNumber], [CustomerCreationDate]) VALUES (5, N'19811103-0539', N'2021-12-04 23:08:37')
INSERT [dbo].[Customers] ([CustomerId], [PersonNumber], [CustomerCreationDate]) VALUES (6, N'19811103-0540', N'2021-12-04 23:10:43')
INSERT [dbo].[Customers] ([CustomerId], [PersonNumber], [CustomerCreationDate]) VALUES (1005, N'19811103-0521', N'2021-12-07 12:18:03')
INSERT [dbo].[Customers] ([CustomerId], [PersonNumber], [CustomerCreationDate]) VALUES (1006, N'19821103-0535', N'2021-12-07 22:02:38')
INSERT [dbo].[Customers] ([CustomerId], [PersonNumber], [CustomerCreationDate]) VALUES (1007, N'19811203-0535', N'2021-12-07 22:23:38')
INSERT [dbo].[Customers] ([CustomerId], [PersonNumber], [CustomerCreationDate]) VALUES (2006, N'19851103-0521', N'2021-12-10 14:10:18')
SET IDENTITY_INSERT [dbo].[Customers] OFF
SET IDENTITY_INSERT [dbo].[Vehicles] ON 

INSERT [dbo].[Vehicles] ([VehicleId], [Mileage], [RegistrationNumber], [CategoryId]) VALUES (1, 2000, N'MLB 41I', 1)
INSERT [dbo].[Vehicles] ([VehicleId], [Mileage], [RegistrationNumber], [CategoryId]) VALUES (2, 2000, N'MLB 70R', 1)
INSERT [dbo].[Vehicles] ([VehicleId], [Mileage], [RegistrationNumber], [CategoryId]) VALUES (3, 2500, N'MLB 987', 2)
INSERT [dbo].[Vehicles] ([VehicleId], [Mileage], [RegistrationNumber], [CategoryId]) VALUES (4, 3000, N'MLB 689', 3)
INSERT [dbo].[Vehicles] ([VehicleId], [Mileage], [RegistrationNumber], [CategoryId]) VALUES (5, 1700, N'MLB 15J', 2)
INSERT [dbo].[Vehicles] ([VehicleId], [Mileage], [RegistrationNumber], [CategoryId]) VALUES (6, 2205, N'MLB 69S', 2)
INSERT [dbo].[Vehicles] ([VehicleId], [Mileage], [RegistrationNumber], [CategoryId]) VALUES (7, 5000, N'MLB 35H', 1)
INSERT [dbo].[Vehicles] ([VehicleId], [Mileage], [RegistrationNumber], [CategoryId]) VALUES (8, 500, N'MLB 29J', 3)
INSERT [dbo].[Vehicles] ([VehicleId], [Mileage], [RegistrationNumber], [CategoryId]) VALUES (9, 301, N'MLB 10K', 2)
INSERT [dbo].[Vehicles] ([VehicleId], [Mileage], [RegistrationNumber], [CategoryId]) VALUES (10, 366, N'MLB 32D', 1)
INSERT [dbo].[Vehicles] ([VehicleId], [Mileage], [RegistrationNumber], [CategoryId]) VALUES (11, 4458, N'MLB 911', 2)
INSERT [dbo].[Vehicles] ([VehicleId], [Mileage], [RegistrationNumber], [CategoryId]) VALUES (12, 32, N'MLB 92M', 3)
INSERT [dbo].[Vehicles] ([VehicleId], [Mileage], [RegistrationNumber], [CategoryId]) VALUES (13, 2000, N'MLB 20P', 3)
INSERT [dbo].[Vehicles] ([VehicleId], [Mileage], [RegistrationNumber], [CategoryId]) VALUES (14, 145, N'MLB 49W', 2)
INSERT [dbo].[Vehicles] ([VehicleId], [Mileage], [RegistrationNumber], [CategoryId]) VALUES (15, 5000, N'MLB 64R', 1)
INSERT [dbo].[Vehicles] ([VehicleId], [Mileage], [RegistrationNumber], [CategoryId]) VALUES (16, 125, N'MLB 10D', 3)
SET IDENTITY_INSERT [dbo].[Vehicles] OFF
/****** Object:  Index [IX_Customers]    Script Date: 2021-12-10 23:01:22 ******/
CREATE NONCLUSTERED INDEX [IX_Customers] ON [dbo].[Customers]
(
	[CustomerId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_Vehicles]    Script Date: 2021-12-10 23:01:22 ******/
ALTER TABLE [dbo].[Vehicles] ADD  CONSTRAINT [IX_Vehicles] UNIQUE NONCLUSTERED 
(
	[RegistrationNumber] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Bookings]  WITH CHECK ADD  CONSTRAINT [FK_Rentals_Customers] FOREIGN KEY([CustomerId])
REFERENCES [dbo].[Customers] ([CustomerId])
GO
ALTER TABLE [dbo].[Bookings] CHECK CONSTRAINT [FK_Rentals_Customers]
GO
ALTER TABLE [dbo].[Bookings]  WITH CHECK ADD  CONSTRAINT [FK_Rentals_Vehicles] FOREIGN KEY([VehicleId])
REFERENCES [dbo].[Vehicles] ([VehicleId])
GO
ALTER TABLE [dbo].[Bookings] CHECK CONSTRAINT [FK_Rentals_Vehicles]
GO
ALTER TABLE [dbo].[Vehicles]  WITH CHECK ADD  CONSTRAINT [CategoryId] FOREIGN KEY([CategoryId])
REFERENCES [dbo].[Categories] ([CategoryId])
GO
ALTER TABLE [dbo].[Vehicles] CHECK CONSTRAINT [CategoryId]
GO
USE [master]
GO
ALTER DATABASE [VehicleBookingsTest] SET  READ_WRITE 
GO
