USE [master]
GO
/****** Object:  Database [CHEVEUX]    Script Date: 2018/10/16 18:01:46 ******/
CREATE DATABASE [CHEVEUX]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'CHEVEUX', FILENAME = N'F:\Google Drive\Visual Studio\Cheveux\CHEVEUX.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'CHEVEUX_log', FILENAME = N'F:\Google Drive\Visual Studio\Cheveux\CHEVEUX_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [CHEVEUX].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [CHEVEUX] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [CHEVEUX] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [CHEVEUX] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [CHEVEUX] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [CHEVEUX] SET ARITHABORT OFF 
GO
ALTER DATABASE [CHEVEUX] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [CHEVEUX] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [CHEVEUX] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [CHEVEUX] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [CHEVEUX] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [CHEVEUX] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [CHEVEUX] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [CHEVEUX] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [CHEVEUX] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [CHEVEUX] SET  DISABLE_BROKER 
GO
ALTER DATABASE [CHEVEUX] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [CHEVEUX] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [CHEVEUX] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [CHEVEUX] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [CHEVEUX] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [CHEVEUX] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [CHEVEUX] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [CHEVEUX] SET RECOVERY FULL 
GO
ALTER DATABASE [CHEVEUX] SET  MULTI_USER 
GO
ALTER DATABASE [CHEVEUX] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [CHEVEUX] SET DB_CHAINING OFF 
GO
ALTER DATABASE [CHEVEUX] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [CHEVEUX] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
EXEC sys.sp_db_vardecimal_storage_format N'CHEVEUX', N'ON'
GO
USE [CHEVEUX]
GO
/****** Object:  Table [dbo].[ACCESSORY]    Script Date: 2018/10/16 18:01:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ACCESSORY](
	[AccessoryID] [nchar](10) NOT NULL,
	[Colour] [varchar](50) NULL,
	[Qty] [int] NULL,
	[BrandID] [nchar](10) NULL,
	[SupplierID] [nchar](10) NULL,
 CONSTRAINT [PK_ACCESSORY] PRIMARY KEY CLUSTERED 
(
	[AccessoryID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Auto_Purchase_Products]    Script Date: 2018/10/16 18:01:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Auto_Purchase_Products](
	[ProductID] [nchar](10) NOT NULL,
	[Qty] [int] NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[BOOKING]    Script Date: 2018/10/16 18:01:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BOOKING](
	[BookingID] [nchar](10) NOT NULL,
	[SlotNo] [nchar](10) NULL,
	[CustomerID] [nchar](30) NULL,
	[StylistID] [nchar](30) NULL,
	[Date] [datetime] NULL,
	[Available] [char](1) NULL,
	[Arrived] [char](1) NULL,
	[Comment] [varchar](max) NULL,
	[NotificationReminder] [bit] NULL,
	[primaryBookingID] [nchar](10) NULL,
 CONSTRAINT [PK_BOOKING] PRIMARY KEY CLUSTERED 
(
	[BookingID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[BookingService]    Script Date: 2018/10/16 18:01:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BookingService](
	[BookingID] [nchar](10) NULL,
	[ServiceID] [nchar](10) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[BRAID_SERVICE]    Script Date: 2018/10/16 18:01:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BRAID_SERVICE](
	[ServiceID] [nchar](10) NOT NULL,
	[WidthID] [nchar](10) NULL,
	[LengthID] [nchar](10) NULL,
	[StyleID] [nchar](10) NULL,
 CONSTRAINT [PK_BRAID_SERVICE] PRIMARY KEY CLUSTERED 
(
	[ServiceID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[BRAND]    Script Date: 2018/10/16 18:01:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BRAND](
	[BrandID] [nchar](10) NOT NULL,
	[Name] [varchar](50) NULL,
	[Type(T/A)] [char](1) NULL,
 CONSTRAINT [PK_Brand] PRIMARY KEY CLUSTERED 
(
	[BrandID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[BUSINESS]    Script Date: 2018/10/16 18:01:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BUSINESS](
	[BusinessID] [nchar](10) NOT NULL,
	[Vat%] [int] NOT NULL,
	[VatRegNo] [nchar](10) NOT NULL,
	[AddressLine1] [varchar](max) NOT NULL,
	[AddressLine2] [varchar](max) NULL,
	[Phone] [nchar](10) NOT NULL,
	[WeekdayStart] [time](7) NOT NULL,
	[WeekdayEnd] [time](7) NOT NULL,
	[WeekendStart] [time](7) NOT NULL,
	[WeekendEnd] [time](7) NOT NULL,
	[PublicHolStart] [time](7) NOT NULL,
	[PublicHolEnd] [time](7) NOT NULL,
	[Logo] [varbinary](max) NULL,
 CONSTRAINT [PK_BUSINESS] PRIMARY KEY CLUSTERED 
(
	[BusinessID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CUST_VISIT]    Script Date: 2018/10/16 18:01:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CUST_VISIT](
	[CustomerID] [nchar](30) NOT NULL,
	[Date] [datetime] NOT NULL,
	[BookingID] [nchar](10) NULL,
	[Description] [varchar](50) NULL,
 CONSTRAINT [PK_CUST_VISIT] PRIMARY KEY CLUSTERED 
(
	[CustomerID] ASC,
	[Date] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[EMPLOYEE]    Script Date: 2018/10/16 18:01:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EMPLOYEE](
	[EmployeeID] [nchar](30) NOT NULL,
	[Type] [nchar](10) NOT NULL,
	[Bio] [varchar](max) NULL,
	[AddressLine1] [varchar](max) NULL,
	[AddressLine2] [varchar](max) NULL,
	[Suburb] [nchar](100) NULL,
	[City] [nchar](100) NULL,
 CONSTRAINT [PK_EMPLOYEE] PRIMARY KEY CLUSTERED 
(
	[EmployeeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Home_Page]    Script Date: 2018/10/16 18:01:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Home_Page](
	[FeatureID] [nchar](10) NOT NULL,
	[ItemID] [nchar](30) NOT NULL,
	[ImageURL] [varchar](max) NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[LENGTH]    Script Date: 2018/10/16 18:01:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LENGTH](
	[LengthID] [nchar](10) NOT NULL,
	[Description] [varchar](50) NULL,
 CONSTRAINT [PK_LENGTH] PRIMARY KEY CLUSTERED 
(
	[LengthID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Order]    Script Date: 2018/10/16 18:01:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Order](
	[SupplierID] [nchar](10) NOT NULL,
	[OrderID] [nchar](10) NOT NULL,
	[OrderDate] [datetime] NOT NULL,
	[Received] [bit] NOT NULL,
	[DateReceived] [datetime] NULL,
 CONSTRAINT [PK_Order] PRIMARY KEY CLUSTERED 
(
	[OrderID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Order_DTL]    Script Date: 2018/10/16 18:01:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Order_DTL](
	[OrderID] [nchar](10) NOT NULL,
	[ProductID] [nchar](10) NOT NULL,
	[Qty] [int] NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PRODUCT]    Script Date: 2018/10/16 18:01:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PRODUCT](
	[ProductID] [nchar](10) NOT NULL,
	[Name] [varchar](max) NULL,
	[ProductDescription] [varchar](max) NULL,
	[Price] [money] NULL,
	[ProductType(T/A/S)] [char](1) NULL,
	[Active] [char](1) NULL,
	[ProductImage] [nvarchar](50) NULL,
 CONSTRAINT [PK_PRODUCT] PRIMARY KEY CLUSTERED 
(
	[ProductID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ProductType]    Script Date: 2018/10/16 18:01:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProductType](
	[TypeID] [nchar](10) NOT NULL,
	[Name] [nchar](100) NOT NULL,
	[Product/Service] [char](1) NOT NULL,
	[PrimaryService] [bit] NULL,
 CONSTRAINT [PK_ProductType] PRIMARY KEY CLUSTERED 
(
	[TypeID] ASC,
	[Product/Service] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[REVIEW]    Script Date: 2018/10/16 18:01:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[REVIEW](
	[ReviewID] [nchar](10) NOT NULL,
	[CustomerID] [nchar](30) NULL,
	[EmployeeID] [nchar](30) NULL,
	[Date] [datetime] NULL,
	[Time] [time](7) NULL,
	[Rating] [float] NULL,
	[Comment] [varchar](50) NULL,
	[primaryBookingID] [nchar](10) NULL,
 CONSTRAINT [PK_REVIEW] PRIMARY KEY CLUSTERED 
(
	[ReviewID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SALE]    Script Date: 2018/10/16 18:01:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SALE](
	[SaleID] [nchar](10) NOT NULL,
	[Date] [datetime] NULL,
	[CustomerID] [nchar](30) NULL,
	[PaymentType] [nchar](10) NULL,
	[BookingID] [nchar](10) NULL,
 CONSTRAINT [PK_Sale] PRIMARY KEY CLUSTERED 
(
	[SaleID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SALES_DTL]    Script Date: 2018/10/16 18:01:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SALES_DTL](
	[SaleID] [nchar](10) NOT NULL,
	[ProductID] [nchar](10) NOT NULL,
	[Qty] [int] NULL,
	[Price] [money] NULL,
 CONSTRAINT [PK_SALES_DTL] PRIMARY KEY CLUSTERED 
(
	[SaleID] ASC,
	[ProductID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SERVICE]    Script Date: 2018/10/16 18:01:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SERVICE](
	[ServiceID] [nchar](10) NOT NULL,
	[NoOfSlots] [int] NULL,
	[Type(A/N/B)] [char](1) NULL,
 CONSTRAINT [PK_SERVICE] PRIMARY KEY CLUSTERED 
(
	[ServiceID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Stock_Management]    Script Date: 2018/10/16 18:01:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Stock_Management](
	[LowStock] [int] NOT NULL,
	[PurchaseQty] [int] NOT NULL,
	[AutoPurchase] [bit] NOT NULL,
	[AutoPurchaseFrequency] [nchar](3) NOT NULL,
	[AutoPurchaseProducts] [bit] NOT NULL,
	[BusinessID] [nchar](10) NOT NULL,
	[NxtOrderDate] [datetime] NOT NULL,
 CONSTRAINT [PK_Stock_Management] PRIMARY KEY CLUSTERED 
(
	[BusinessID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[STYLE]    Script Date: 2018/10/16 18:01:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[STYLE](
	[StyleID] [nchar](10) NOT NULL,
	[Description] [varchar](50) NULL,
 CONSTRAINT [PK_STYLE] PRIMARY KEY CLUSTERED 
(
	[StyleID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[STYLIST_SERVICE]    Script Date: 2018/10/16 18:01:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[STYLIST_SERVICE](
	[EmployeeID] [nchar](30) NOT NULL,
	[ServiceID] [nchar](10) NOT NULL,
 CONSTRAINT [PK_STYLIST_SERVICE] PRIMARY KEY CLUSTERED 
(
	[EmployeeID] ASC,
	[ServiceID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Supplier]    Script Date: 2018/10/16 18:01:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Supplier](
	[SupplierID] [nchar](10) NOT NULL,
	[SupplierName] [nchar](50) NOT NULL,
	[ContactName] [nchar](50) NOT NULL,
	[ContactNo] [nchar](10) NOT NULL,
	[AddressLine1] [nchar](100) NOT NULL,
	[AddressLine2] [nchar](100) NULL,
	[Suburb] [nchar](100) NOT NULL,
	[City] [nchar](100) NOT NULL,
	[ContactEmail] [varchar](50) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TIMESLOT]    Script Date: 2018/10/16 18:01:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TIMESLOT](
	[SlotNo] [nchar](10) NOT NULL,
	[StartTime] [time](7) NULL,
	[EndTime] [time](7) NULL,
 CONSTRAINT [PK_TIMESLOT] PRIMARY KEY CLUSTERED 
(
	[SlotNo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TREATMENT]    Script Date: 2018/10/16 18:01:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TREATMENT](
	[TreatmentID] [nchar](10) NOT NULL,
	[Qty] [int] NULL,
	[TreatmentType] [varchar](50) NULL,
	[BrandID] [nchar](10) NULL,
	[SupplierID] [nchar](10) NULL,
 CONSTRAINT [PK_TREATMENT] PRIMARY KEY CLUSTERED 
(
	[TreatmentID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[USER]    Script Date: 2018/10/16 18:01:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[USER](
	[UserID] [nchar](30) NOT NULL,
	[FirstName] [varchar](50) NULL,
	[LastName] [varchar](50) NULL,
	[UserName] [nvarchar](50) NULL,
	[Email] [varchar](50) NULL,
	[ContactNo] [nchar](10) NULL,
	[Password] [varchar](max) NULL,
	[UserType] [char](1) NULL,
	[Active] [char](1) NULL,
	[UserImage] [varchar](max) NULL,
	[AccountType] [nchar](10) NULL,
	[PreferredCommunication] [char](1) NULL,
	[PassRestCode] [nchar](30) NULL,
 CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
(
	[UserID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[WIDTH]    Script Date: 2018/10/16 18:01:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[WIDTH](
	[WidthID] [nchar](10) NOT NULL,
	[Description] [varchar](50) NULL,
 CONSTRAINT [PK_WIDTH] PRIMARY KEY CLUSTERED 
(
	[WidthID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[ACCESSORY] ([AccessoryID], [Colour], [Qty], [BrandID], [SupplierID]) VALUES (N'Pr1135    ', N'Silver/Gold', 6, N'B19       ', N'001       ')
INSERT [dbo].[ACCESSORY] ([AccessoryID], [Colour], [Qty], [BrandID], [SupplierID]) VALUES (N'Pr1136    ', N'#1', 6, N'B02       ', N'001       ')
INSERT [dbo].[ACCESSORY] ([AccessoryID], [Colour], [Qty], [BrandID], [SupplierID]) VALUES (N'Pr1137    ', N'#1', 6, N'B05       ', N'001       ')
INSERT [dbo].[ACCESSORY] ([AccessoryID], [Colour], [Qty], [BrandID], [SupplierID]) VALUES (N'Pr1138    ', N'Black', 6, N'B21       ', N'001       ')
INSERT [dbo].[ACCESSORY] ([AccessoryID], [Colour], [Qty], [BrandID], [SupplierID]) VALUES (N'Pr1140    ', N'#350', 6, N'B02       ', N'001       ')
INSERT [dbo].[ACCESSORY] ([AccessoryID], [Colour], [Qty], [BrandID], [SupplierID]) VALUES (N'Pr1144    ', N'#2', 6, N'B15       ', N'001       ')
INSERT [dbo].[ACCESSORY] ([AccessoryID], [Colour], [Qty], [BrandID], [SupplierID]) VALUES (N'Pr1145    ', N'#2', 6, N'B02       ', N'001       ')
INSERT [dbo].[ACCESSORY] ([AccessoryID], [Colour], [Qty], [BrandID], [SupplierID]) VALUES (N'Pr1151    ', N'1B', 8, N'B07       ', N'001       ')
INSERT [dbo].[ACCESSORY] ([AccessoryID], [Colour], [Qty], [BrandID], [SupplierID]) VALUES (N'Pr1152    ', N'1B', 6, N'B10       ', N'001       ')
INSERT [dbo].[ACCESSORY] ([AccessoryID], [Colour], [Qty], [BrandID], [SupplierID]) VALUES (N'Pr1153    ', N'1B', 6, N'B07       ', N'001       ')
INSERT [dbo].[ACCESSORY] ([AccessoryID], [Colour], [Qty], [BrandID], [SupplierID]) VALUES (N'Pr1156    ', N'1B/350', 6, N'B10       ', N'001       ')
INSERT [dbo].[ACCESSORY] ([AccessoryID], [Colour], [Qty], [BrandID], [SupplierID]) VALUES (N'Pr1161    ', N'1B', 6, N'B10       ', N'001       ')
INSERT [dbo].[ACCESSORY] ([AccessoryID], [Colour], [Qty], [BrandID], [SupplierID]) VALUES (N'Pr1162    ', N'D33', 6, N'B10       ', N'001       ')
INSERT [dbo].[ACCESSORY] ([AccessoryID], [Colour], [Qty], [BrandID], [SupplierID]) VALUES (N'Pr1164    ', N'#1', 9, N'B07       ', N'001       ')
INSERT [dbo].[ACCESSORY] ([AccessoryID], [Colour], [Qty], [BrandID], [SupplierID]) VALUES (N'Pr1165    ', N'#1', 6, N'B15       ', N'001       ')
INSERT [dbo].[ACCESSORY] ([AccessoryID], [Colour], [Qty], [BrandID], [SupplierID]) VALUES (N'Pr1166    ', N'#350', 6, N'B05       ', N'001       ')
INSERT [dbo].[ACCESSORY] ([AccessoryID], [Colour], [Qty], [BrandID], [SupplierID]) VALUES (N'Pr1167    ', N'Aqua', 6, N'B07       ', N'001       ')
INSERT [dbo].[ACCESSORY] ([AccessoryID], [Colour], [Qty], [BrandID], [SupplierID]) VALUES (N'Pr1168    ', N'#1', 10, N'B22       ', N'001       ')
INSERT [dbo].[ACCESSORY] ([AccessoryID], [Colour], [Qty], [BrandID], [SupplierID]) VALUES (N'Pr1172    ', N'#1', 6, N'B22       ', N'001       ')
INSERT [dbo].[ACCESSORY] ([AccessoryID], [Colour], [Qty], [BrandID], [SupplierID]) VALUES (N'Pr1174    ', N'#2', 8, N'B23       ', N'001       ')
INSERT [dbo].[Auto_Purchase_Products] ([ProductID], [Qty]) VALUES (N'Pr1149    ', 5)
INSERT [dbo].[Auto_Purchase_Products] ([ProductID], [Qty]) VALUES (N'Pr1145    ', 3)
INSERT [dbo].[Auto_Purchase_Products] ([ProductID], [Qty]) VALUES (N'Pr1132    ', 3)
INSERT [dbo].[Auto_Purchase_Products] ([ProductID], [Qty]) VALUES (N'Pr1138    ', 2)
INSERT [dbo].[Auto_Purchase_Products] ([ProductID], [Qty]) VALUES (N'Pr1141    ', 3)
INSERT [dbo].[BOOKING] ([BookingID], [SlotNo], [CustomerID], [StylistID], [Date], [Available], [Arrived], [Comment], [NotificationReminder], [primaryBookingID]) VALUES (N'001621148 ', N'Slo11     ', N'662881846588586465447         ', N'270146203840160705048         ', CAST(N'2018-09-20T00:00:00.000' AS DateTime), N'N', N'N', N'hfgjk', 1, N'001621148 ')
INSERT [dbo].[BOOKING] ([BookingID], [SlotNo], [CustomerID], [StylistID], [Date], [Available], [Arrived], [Comment], [NotificationReminder], [primaryBookingID]) VALUES (N'002778800 ', N'Slo11     ', N'777706452281511868742         ', N'118233419479102946333         ', CAST(N'2018-10-24T00:00:00.000' AS DateTime), N'N', N'N', N'', 0, N'043414818 ')
INSERT [dbo].[BOOKING] ([BookingID], [SlotNo], [CustomerID], [StylistID], [Date], [Available], [Arrived], [Comment], [NotificationReminder], [primaryBookingID]) VALUES (N'004400232 ', N'Slo11     ', N'635506662246576754303         ', N'270146203840160705048         ', CAST(N'2018-10-08T00:00:00.000' AS DateTime), N'N', N'Y', N'', 0, N'004400232 ')
INSERT [dbo].[BOOKING] ([BookingID], [SlotNo], [CustomerID], [StylistID], [Date], [Available], [Arrived], [Comment], [NotificationReminder], [primaryBookingID]) VALUES (N'007017310 ', N'Slo8      ', N'215703134623237784686         ', N'580312424824681240311         ', CAST(N'2018-09-30T00:00:00.000' AS DateTime), N'N', N'N', N'', 0, N'634365345 ')
INSERT [dbo].[BOOKING] ([BookingID], [SlotNo], [CustomerID], [StylistID], [Date], [Available], [Arrived], [Comment], [NotificationReminder], [primaryBookingID]) VALUES (N'007838876 ', N'Slo19     ', N'612648662678214805485         ', N'580312424824681240311         ', CAST(N'2018-09-21T00:00:00.000' AS DateTime), N'N', N'Y', N'', 0, N'568466551 ')
INSERT [dbo].[BOOKING] ([BookingID], [SlotNo], [CustomerID], [StylistID], [Date], [Available], [Arrived], [Comment], [NotificationReminder], [primaryBookingID]) VALUES (N'011003733 ', N'Slo17     ', N'535314300265460205603         ', N'580312424824681240311         ', CAST(N'2018-09-19T00:00:00.000' AS DateTime), N'N', N'Y', N'', 0, N'844052626 ')
INSERT [dbo].[BOOKING] ([BookingID], [SlotNo], [CustomerID], [StylistID], [Date], [Available], [Arrived], [Comment], [NotificationReminder], [primaryBookingID]) VALUES (N'011111111 ', N'Slo1      ', N'635506662246576754303         ', N'270146203840160705048         ', CAST(N'2018-09-01T00:00:00.000' AS DateTime), N'N', N'Y', NULL, 1, N'011111111 ')
INSERT [dbo].[BOOKING] ([BookingID], [SlotNo], [CustomerID], [StylistID], [Date], [Available], [Arrived], [Comment], [NotificationReminder], [primaryBookingID]) VALUES (N'021848722 ', N'Slo4      ', N'535314300265460205603         ', N'105450503348172771121         ', CAST(N'2018-09-24T00:00:00.000' AS DateTime), N'N', N'N', N'', 1, N'550428622 ')
INSERT [dbo].[BOOKING] ([BookingID], [SlotNo], [CustomerID], [StylistID], [Date], [Available], [Arrived], [Comment], [NotificationReminder], [primaryBookingID]) VALUES (N'025542557 ', N'Slo13     ', N'662881846588586465447         ', N'118233419479102946333         ', CAST(N'2018-11-06T00:00:00.000' AS DateTime), N'N', N'N', NULL, 0, N'718042600 ')
INSERT [dbo].[BOOKING] ([BookingID], [SlotNo], [CustomerID], [StylistID], [Date], [Available], [Arrived], [Comment], [NotificationReminder], [primaryBookingID]) VALUES (N'030364610 ', N'Slo2      ', N'535314300265460205603         ', N'580312424824681240311         ', CAST(N'2018-10-20T00:00:00.000' AS DateTime), N'N', N'N', N'', 0, N'030364610 ')
INSERT [dbo].[BOOKING] ([BookingID], [SlotNo], [CustomerID], [StylistID], [Date], [Available], [Arrived], [Comment], [NotificationReminder], [primaryBookingID]) VALUES (N'031055622 ', N'Slo12     ', N'100155997350724101850         ', N'105450503348172771121         ', CAST(N'2018-09-30T00:00:00.000' AS DateTime), N'N', N'N', N'', 0, N'658535522 ')
INSERT [dbo].[BOOKING] ([BookingID], [SlotNo], [CustomerID], [StylistID], [Date], [Available], [Arrived], [Comment], [NotificationReminder], [primaryBookingID]) VALUES (N'034668004 ', N'Slo18     ', N'635506662246576754303         ', N'580312424824681240311         ', CAST(N'2018-09-17T00:00:00.000' AS DateTime), N'N', N'N', N'', 1, N'403034175 ')
INSERT [dbo].[BOOKING] ([BookingID], [SlotNo], [CustomerID], [StylistID], [Date], [Available], [Arrived], [Comment], [NotificationReminder], [primaryBookingID]) VALUES (N'041332566 ', N'Slo18     ', N'384715112175204214480         ', N'118233419479102946333         ', CAST(N'2018-09-10T00:00:00.000' AS DateTime), N'N', N'N', N'', 0, N'041332566 ')
INSERT [dbo].[BOOKING] ([BookingID], [SlotNo], [CustomerID], [StylistID], [Date], [Available], [Arrived], [Comment], [NotificationReminder], [primaryBookingID]) VALUES (N'042860142 ', N'Slo6      ', N'215703134623237784686         ', N'580312424824681240311         ', CAST(N'2018-09-22T00:00:00.000' AS DateTime), N'N', N'Y', N'', 1, N'267247235 ')
INSERT [dbo].[BOOKING] ([BookingID], [SlotNo], [CustomerID], [StylistID], [Date], [Available], [Arrived], [Comment], [NotificationReminder], [primaryBookingID]) VALUES (N'043414818 ', N'Slo10     ', N'777706452281511868742         ', N'118233419479102946333         ', CAST(N'2018-10-24T00:00:00.000' AS DateTime), N'N', N'N', N'', 0, N'043414818 ')
INSERT [dbo].[BOOKING] ([BookingID], [SlotNo], [CustomerID], [StylistID], [Date], [Available], [Arrived], [Comment], [NotificationReminder], [primaryBookingID]) VALUES (N'046007658 ', N'Slo11     ', N'181477264752348604465         ', N'270146203840160705048         ', CAST(N'2018-09-29T00:00:00.000' AS DateTime), N'N', N'N', N'', 0, N'046007658 ')
INSERT [dbo].[BOOKING] ([BookingID], [SlotNo], [CustomerID], [StylistID], [Date], [Available], [Arrived], [Comment], [NotificationReminder], [primaryBookingID]) VALUES (N'052367723 ', N'Slo13     ', N'612648662678214805485         ', N'580312424824681240311         ', CAST(N'2018-08-20T00:00:00.000' AS DateTime), N'N', N'Y', NULL, 1, N'052367723 ')
INSERT [dbo].[BOOKING] ([BookingID], [SlotNo], [CustomerID], [StylistID], [Date], [Available], [Arrived], [Comment], [NotificationReminder], [primaryBookingID]) VALUES (N'053817368 ', N'Slo14     ', N'307776754475328666200         ', N'118233419479102946333         ', CAST(N'2018-10-22T00:00:00.000' AS DateTime), N'N', N'N', N'', 0, N'053817368 ')
INSERT [dbo].[BOOKING] ([BookingID], [SlotNo], [CustomerID], [StylistID], [Date], [Available], [Arrived], [Comment], [NotificationReminder], [primaryBookingID]) VALUES (N'058162045 ', N'Slo17     ', N'154052322058082242258         ', N'118233419479102946333         ', CAST(N'2018-10-24T00:00:00.000' AS DateTime), N'N', N'N', N'', 0, N'058162045 ')
INSERT [dbo].[BOOKING] ([BookingID], [SlotNo], [CustomerID], [StylistID], [Date], [Available], [Arrived], [Comment], [NotificationReminder], [primaryBookingID]) VALUES (N'060026124 ', N'Slo8      ', N'662881846588586465447         ', N'270146203840160705048         ', CAST(N'2018-08-22T00:00:00.000' AS DateTime), N'N', N'Y', NULL, 1, N'060026124 ')
INSERT [dbo].[BOOKING] ([BookingID], [SlotNo], [CustomerID], [StylistID], [Date], [Available], [Arrived], [Comment], [NotificationReminder], [primaryBookingID]) VALUES (N'063323570 ', N'Slo10     ', N'777706452281511868742         ', N'118233419479102946333         ', CAST(N'2018-10-01T00:00:00.000' AS DateTime), N'N', N'Y', N'', 0, N'063323570 ')
INSERT [dbo].[BOOKING] ([BookingID], [SlotNo], [CustomerID], [StylistID], [Date], [Available], [Arrived], [Comment], [NotificationReminder], [primaryBookingID]) VALUES (N'070741623 ', N'Slo11     ', N'662881846588586465447         ', N'270146203840160705048         ', CAST(N'2018-08-29T00:00:00.000' AS DateTime), N'N', N'Y', NULL, 1, N'070741623 ')
INSERT [dbo].[BOOKING] ([BookingID], [SlotNo], [CustomerID], [StylistID], [Date], [Available], [Arrived], [Comment], [NotificationReminder], [primaryBookingID]) VALUES (N'071046156 ', N'Slo6      ', N'612648662678214805485         ', N'118233419479102946333         ', CAST(N'2018-09-20T00:00:00.000' AS DateTime), N'N', N'N', N'', 1, N'071046156 ')
INSERT [dbo].[BOOKING] ([BookingID], [SlotNo], [CustomerID], [StylistID], [Date], [Available], [Arrived], [Comment], [NotificationReminder], [primaryBookingID]) VALUES (N'073082247 ', N'Slo11     ', N'635506662246576754303         ', N'580312424824681240311         ', CAST(N'2018-09-17T00:00:00.000' AS DateTime), N'N', N'N', N'', 1, N'403034175 ')
INSERT [dbo].[BOOKING] ([BookingID], [SlotNo], [CustomerID], [StylistID], [Date], [Available], [Arrived], [Comment], [NotificationReminder], [primaryBookingID]) VALUES (N'080647674 ', N'Slo17     ', N'384715112175204214480         ', N'118233419479102946333         ', CAST(N'2018-09-25T00:00:00.000' AS DateTime), N'N', N'Y', N'', 0, N'155466825 ')
INSERT [dbo].[BOOKING] ([BookingID], [SlotNo], [CustomerID], [StylistID], [Date], [Available], [Arrived], [Comment], [NotificationReminder], [primaryBookingID]) VALUES (N'082066658 ', N'Slo18     ', N'635506662246576754303         ', N'580312424824681240311         ', CAST(N'2018-09-28T00:00:00.000' AS DateTime), N'N', N'N', N'', 1, N'413046600 ')
INSERT [dbo].[BOOKING] ([BookingID], [SlotNo], [CustomerID], [StylistID], [Date], [Available], [Arrived], [Comment], [NotificationReminder], [primaryBookingID]) VALUES (N'083483610 ', N'Slo10     ', N'215703134623237784686         ', N'270146203840160705048         ', CAST(N'2018-09-18T00:00:00.000' AS DateTime), N'N', N'Y', N'', 0, N'564022384 ')
INSERT [dbo].[BOOKING] ([BookingID], [SlotNo], [CustomerID], [StylistID], [Date], [Available], [Arrived], [Comment], [NotificationReminder], [primaryBookingID]) VALUES (N'087310681 ', N'Slo13     ', N'181477264752348604465         ', N'580312424824681240311         ', CAST(N'2018-09-21T00:00:00.000' AS DateTime), N'N', N'Y', N'', 0, N'418380632 ')
INSERT [dbo].[BOOKING] ([BookingID], [SlotNo], [CustomerID], [StylistID], [Date], [Available], [Arrived], [Comment], [NotificationReminder], [primaryBookingID]) VALUES (N'110308484 ', N'Slo7      ', N'105242998585655922697         ', N'118233419479102946333         ', CAST(N'2018-09-29T00:00:00.000' AS DateTime), N'N', N'N', N'', 0, N'747078374 ')
INSERT [dbo].[BOOKING] ([BookingID], [SlotNo], [CustomerID], [StylistID], [Date], [Available], [Arrived], [Comment], [NotificationReminder], [primaryBookingID]) VALUES (N'111111111 ', N'Slo5      ', N'105242998585655922697         ', N'580312424824681240311         ', CAST(N'2018-08-01T00:00:00.000' AS DateTime), N'N', N'Y', NULL, 1, N'111111111 ')
INSERT [dbo].[BOOKING] ([BookingID], [SlotNo], [CustomerID], [StylistID], [Date], [Available], [Arrived], [Comment], [NotificationReminder], [primaryBookingID]) VALUES (N'114357316 ', N'Slo6      ', N'535314300265460205603         ', N'580312424824681240311         ', CAST(N'2018-08-28T00:00:00.000' AS DateTime), N'N', N'Y', NULL, 0, N'114357316 ')
INSERT [dbo].[BOOKING] ([BookingID], [SlotNo], [CustomerID], [StylistID], [Date], [Available], [Arrived], [Comment], [NotificationReminder], [primaryBookingID]) VALUES (N'117688483 ', N'Slo5      ', N'215703134623237784686         ', N'580312424824681240311         ', CAST(N'2018-09-22T00:00:00.000' AS DateTime), N'N', N'Y', N'', 1, N'267247235 ')
INSERT [dbo].[BOOKING] ([BookingID], [SlotNo], [CustomerID], [StylistID], [Date], [Available], [Arrived], [Comment], [NotificationReminder], [primaryBookingID]) VALUES (N'122222222 ', N'Slo7      ', N'100155997350724101850         ', N'118233419479102946333         ', CAST(N'2018-08-07T00:00:00.000' AS DateTime), N'N', N'Y', NULL, 0, N'151711823 ')
INSERT [dbo].[BOOKING] ([BookingID], [SlotNo], [CustomerID], [StylistID], [Date], [Available], [Arrived], [Comment], [NotificationReminder], [primaryBookingID]) VALUES (N'127874202 ', N'Slo11     ', N'105242998585655922697         ', N'105450503348172771121         ', CAST(N'2018-09-10T00:00:00.000' AS DateTime), N'N', N'Y', N'Likes Aunt Jackie''s Shampoo', 0, N'127874202 ')
INSERT [dbo].[BOOKING] ([BookingID], [SlotNo], [CustomerID], [StylistID], [Date], [Available], [Arrived], [Comment], [NotificationReminder], [primaryBookingID]) VALUES (N'128767857 ', N'Slo3      ', N'173735732333607133022         ', N'118233419479102946333         ', CAST(N'2018-09-08T00:00:00.000' AS DateTime), N'N', N'N', NULL, 0, N'128767857 ')
INSERT [dbo].[BOOKING] ([BookingID], [SlotNo], [CustomerID], [StylistID], [Date], [Available], [Arrived], [Comment], [NotificationReminder], [primaryBookingID]) VALUES (N'137540671 ', N'Slo6      ', N'535314300265460205603         ', N'105450503348172771121         ', CAST(N'2018-09-24T00:00:00.000' AS DateTime), N'N', N'N', N'', 1, N'550428622 ')
INSERT [dbo].[BOOKING] ([BookingID], [SlotNo], [CustomerID], [StylistID], [Date], [Available], [Arrived], [Comment], [NotificationReminder], [primaryBookingID]) VALUES (N'141260852 ', N'Slo20     ', N'635506662246576754303         ', N'580312424824681240311         ', CAST(N'2018-09-17T00:00:00.000' AS DateTime), N'N', N'N', N'', 1, N'403034175 ')
INSERT [dbo].[BOOKING] ([BookingID], [SlotNo], [CustomerID], [StylistID], [Date], [Available], [Arrived], [Comment], [NotificationReminder], [primaryBookingID]) VALUES (N'145818714 ', N'Slo2      ', N'612648662678214805485         ', N'270146203840160705048         ', CAST(N'2018-09-30T00:00:00.000' AS DateTime), N'N', N'N', N'', 0, N'145818714 ')
INSERT [dbo].[BOOKING] ([BookingID], [SlotNo], [CustomerID], [StylistID], [Date], [Available], [Arrived], [Comment], [NotificationReminder], [primaryBookingID]) VALUES (N'151553575 ', N'Slo13     ', N'535314300265460205603         ', N'580312424824681240311         ', CAST(N'2018-09-19T00:00:00.000' AS DateTime), N'N', N'Y', N'', 0, N'844052626 ')
INSERT [dbo].[BOOKING] ([BookingID], [SlotNo], [CustomerID], [StylistID], [Date], [Available], [Arrived], [Comment], [NotificationReminder], [primaryBookingID]) VALUES (N'152138032 ', N'Slo12     ', N'181477264752348604465         ', N'580312424824681240311         ', CAST(N'2018-09-21T00:00:00.000' AS DateTime), N'N', N'Y', N'', 0, N'418380632 ')
INSERT [dbo].[BOOKING] ([BookingID], [SlotNo], [CustomerID], [StylistID], [Date], [Available], [Arrived], [Comment], [NotificationReminder], [primaryBookingID]) VALUES (N'155466825 ', N'Slo16     ', N'384715112175204214480         ', N'118233419479102946333         ', CAST(N'2018-09-25T00:00:00.000' AS DateTime), N'N', N'Y', N'', 0, N'155466825 ')
INSERT [dbo].[BOOKING] ([BookingID], [SlotNo], [CustomerID], [StylistID], [Date], [Available], [Arrived], [Comment], [NotificationReminder], [primaryBookingID]) VALUES (N'156415656 ', N'Slo1      ', N'686374338720721300668         ', N'118233419479102946333         ', CAST(N'2018-08-19T00:00:00.000' AS DateTime), N'N', N'N', NULL, 1, N'156415656 ')
INSERT [dbo].[BOOKING] ([BookingID], [SlotNo], [CustomerID], [StylistID], [Date], [Available], [Arrived], [Comment], [NotificationReminder], [primaryBookingID]) VALUES (N'158061872 ', N'Slo5      ', N'612648662678214805485         ', N'580312424824681240311         ', CAST(N'2018-09-19T00:00:00.000' AS DateTime), N'N', N'N', N'', 0, N'216307740 ')
INSERT [dbo].[BOOKING] ([BookingID], [SlotNo], [CustomerID], [StylistID], [Date], [Available], [Arrived], [Comment], [NotificationReminder], [primaryBookingID]) VALUES (N'165151551 ', N'Slo15     ', N'181477264752348604465         ', N'270146203840160705048         ', CAST(N'2018-08-06T00:00:00.000' AS DateTime), N'N', N'Y', NULL, 1, N'165151551 ')
INSERT [dbo].[BOOKING] ([BookingID], [SlotNo], [CustomerID], [StylistID], [Date], [Available], [Arrived], [Comment], [NotificationReminder], [primaryBookingID]) VALUES (N'173457147 ', N'Slo19     ', N'612648662678214805485         ', N'270146203840160705048         ', CAST(N'2018-08-23T00:00:00.000' AS DateTime), N'N', N'Y', NULL, 1, N'173457147 ')
INSERT [dbo].[BOOKING] ([BookingID], [SlotNo], [CustomerID], [StylistID], [Date], [Available], [Arrived], [Comment], [NotificationReminder], [primaryBookingID]) VALUES (N'173566683 ', N'Slo19     ', N'100155997350724101850         ', N'270146203840160705048         ', CAST(N'2018-10-08T00:00:00.000' AS DateTime), N'N', N'Y', N'', 0, N'173566683 ')
INSERT [dbo].[BOOKING] ([BookingID], [SlotNo], [CustomerID], [StylistID], [Date], [Available], [Arrived], [Comment], [NotificationReminder], [primaryBookingID]) VALUES (N'176024758 ', N'Slo13     ', N'181477264752348604465         ', N'580312424824681240311         ', CAST(N'2018-07-24T00:00:00.000' AS DateTime), N'N', N'Y', NULL, 1, N'176024758 ')
INSERT [dbo].[BOOKING] ([BookingID], [SlotNo], [CustomerID], [StylistID], [Date], [Available], [Arrived], [Comment], [NotificationReminder], [primaryBookingID]) VALUES (N'180724634 ', N'Slo5      ', N'181477264752348604465         ', N'118233419479102946333         ', CAST(N'2018-10-21T00:00:00.000' AS DateTime), N'N', N'N', N'', 0, N'180724634 ')
INSERT [dbo].[BOOKING] ([BookingID], [SlotNo], [CustomerID], [StylistID], [Date], [Available], [Arrived], [Comment], [NotificationReminder], [primaryBookingID]) VALUES (N'200174568 ', N'Slo12     ', N'215703134623237784686         ', N'270146203840160705048         ', CAST(N'2018-09-18T00:00:00.000' AS DateTime), N'N', N'Y', N'', 0, N'564022384 ')
INSERT [dbo].[BOOKING] ([BookingID], [SlotNo], [CustomerID], [StylistID], [Date], [Available], [Arrived], [Comment], [NotificationReminder], [primaryBookingID]) VALUES (N'201403174 ', N'Slo20     ', N'535314300265460205603         ', N'118233419479102946333         ', CAST(N'2018-08-26T00:00:00.000' AS DateTime), N'N', N'Y', NULL, 0, N'622473016 ')
INSERT [dbo].[BOOKING] ([BookingID], [SlotNo], [CustomerID], [StylistID], [Date], [Available], [Arrived], [Comment], [NotificationReminder], [primaryBookingID]) VALUES (N'201782088 ', N'Slo15     ', N'100155997350724101850         ', N'118233419479102946333         ', CAST(N'2018-09-03T00:00:00.000' AS DateTime), N'N', N'Y', NULL, 0, N'201782088 ')
INSERT [dbo].[BOOKING] ([BookingID], [SlotNo], [CustomerID], [StylistID], [Date], [Available], [Arrived], [Comment], [NotificationReminder], [primaryBookingID]) VALUES (N'204017675 ', N'Slo10     ', N'535314300265460205603         ', N'580312424824681240311         ', CAST(N'2018-10-20T00:00:00.000' AS DateTime), N'N', N'N', N'', 0, N'030364610 ')
INSERT [dbo].[BOOKING] ([BookingID], [SlotNo], [CustomerID], [StylistID], [Date], [Available], [Arrived], [Comment], [NotificationReminder], [primaryBookingID]) VALUES (N'210504187 ', N'Slo13     ', N'324465860528272078866         ', N'105450503348172771121         ', CAST(N'2018-10-15T00:00:00.000' AS DateTime), N'N', N'Y', N'', 0, N'632574028 ')
INSERT [dbo].[BOOKING] ([BookingID], [SlotNo], [CustomerID], [StylistID], [Date], [Available], [Arrived], [Comment], [NotificationReminder], [primaryBookingID]) VALUES (N'214181455 ', N'Slo11     ', N'384715112175204214480         ', N'270146203840160705048         ', CAST(N'2018-10-03T00:00:00.000' AS DateTime), N'N', N'Y', N'', 1, N'214181455 ')
INSERT [dbo].[BOOKING] ([BookingID], [SlotNo], [CustomerID], [StylistID], [Date], [Available], [Arrived], [Comment], [NotificationReminder], [primaryBookingID]) VALUES (N'214518268 ', N'Slo9      ', N'215703134623237784686         ', N'580312424824681240311         ', CAST(N'2018-09-30T00:00:00.000' AS DateTime), N'N', N'N', N'', 0, N'634365345 ')
INSERT [dbo].[BOOKING] ([BookingID], [SlotNo], [CustomerID], [StylistID], [Date], [Available], [Arrived], [Comment], [NotificationReminder], [primaryBookingID]) VALUES (N'216307740 ', N'          ', N'612648662678214805485         ', N'580312424824681240311         ', CAST(N'2018-09-19T00:00:00.000' AS DateTime), N'N', N'N', N'', 0, N'216307740 ')
INSERT [dbo].[BOOKING] ([BookingID], [SlotNo], [CustomerID], [StylistID], [Date], [Available], [Arrived], [Comment], [NotificationReminder], [primaryBookingID]) VALUES (N'217264372 ', N'Slo15     ', N'100155997350724101850         ', N'580312424824681240311         ', CAST(N'2018-10-02T00:00:00.000' AS DateTime), N'N', N'Y', N'', 0, N'217264372 ')
INSERT [dbo].[BOOKING] ([BookingID], [SlotNo], [CustomerID], [StylistID], [Date], [Available], [Arrived], [Comment], [NotificationReminder], [primaryBookingID]) VALUES (N'222222222 ', N'Slo15     ', N'181477264752348604465         ', N'270146203840160705048         ', CAST(N'2018-08-01T00:00:00.000' AS DateTime), N'N', N'Y', NULL, 1, N'222222222 ')
INSERT [dbo].[BOOKING] ([BookingID], [SlotNo], [CustomerID], [StylistID], [Date], [Available], [Arrived], [Comment], [NotificationReminder], [primaryBookingID]) VALUES (N'222435233 ', N'Slo19     ', N'662881846588586465447         ', N'270146203840160705048         ', CAST(N'2018-08-24T00:00:00.000' AS DateTime), N'N', N'Y', NULL, 0, N'222435233 ')
INSERT [dbo].[BOOKING] ([BookingID], [SlotNo], [CustomerID], [StylistID], [Date], [Available], [Arrived], [Comment], [NotificationReminder], [primaryBookingID]) VALUES (N'227604681 ', N'Slo19     ', N'535314300265460205603         ', N'580312424824681240311         ', CAST(N'2018-09-19T00:00:00.000' AS DateTime), N'N', N'Y', N'', 0, N'844052626 ')
INSERT [dbo].[BOOKING] ([BookingID], [SlotNo], [CustomerID], [StylistID], [Date], [Available], [Arrived], [Comment], [NotificationReminder], [primaryBookingID]) VALUES (N'228313337 ', N'Slo17     ', N'662881846588586465447         ', N'580312424824681240311         ', CAST(N'2018-08-16T00:00:00.000' AS DateTime), N'N', N'Y', NULL, 0, N'228313337 ')
INSERT [dbo].[BOOKING] ([BookingID], [SlotNo], [CustomerID], [StylistID], [Date], [Available], [Arrived], [Comment], [NotificationReminder], [primaryBookingID]) VALUES (N'235230253 ', N'Slo7      ', N'140623085382221348147         ', N'105450503348172771121         ', CAST(N'2018-10-24T00:00:00.000' AS DateTime), N'N', N'N', N'', 0, N'862487288 ')
INSERT [dbo].[BOOKING] ([BookingID], [SlotNo], [CustomerID], [StylistID], [Date], [Available], [Arrived], [Comment], [NotificationReminder], [primaryBookingID]) VALUES (N'237865384 ', N'Slo1      ', N'535314300265460205603         ', N'270146203840160705048         ', CAST(N'2018-09-27T00:00:00.000' AS DateTime), N'N', N'N', N'', 1, N'237865384 ')
INSERT [dbo].[BOOKING] ([BookingID], [SlotNo], [CustomerID], [StylistID], [Date], [Available], [Arrived], [Comment], [NotificationReminder], [primaryBookingID]) VALUES (N'256625222 ', N'Slo13     ', N'635506662246576754303         ', N'580312424824681240311         ', CAST(N'2018-09-17T00:00:00.000' AS DateTime), N'N', N'N', N'', 1, N'403034175 ')
INSERT [dbo].[BOOKING] ([BookingID], [SlotNo], [CustomerID], [StylistID], [Date], [Available], [Arrived], [Comment], [NotificationReminder], [primaryBookingID]) VALUES (N'258414850 ', N'Slo15     ', N'407242734406745485560         ', N'270146203840160705048         ', CAST(N'2018-10-02T00:00:00.000' AS DateTime), N'N', N'Y', N'', 0, N'670383801 ')
INSERT [dbo].[BOOKING] ([BookingID], [SlotNo], [CustomerID], [StylistID], [Date], [Available], [Arrived], [Comment], [NotificationReminder], [primaryBookingID]) VALUES (N'258561000 ', N'Slo8      ', N'215703134623237784686         ', N'580312424824681240311         ', CAST(N'2018-09-22T00:00:00.000' AS DateTime), N'N', N'Y', N'', 1, N'267247235 ')
INSERT [dbo].[BOOKING] ([BookingID], [SlotNo], [CustomerID], [StylistID], [Date], [Available], [Arrived], [Comment], [NotificationReminder], [primaryBookingID]) VALUES (N'260225447 ', N'Slo19     ', N'100155997350724101850         ', N'118233419479102946333         ', CAST(N'2018-09-15T00:00:00.000' AS DateTime), N'N', N'Y', N'', 0, N'260225447 ')
INSERT [dbo].[BOOKING] ([BookingID], [SlotNo], [CustomerID], [StylistID], [Date], [Available], [Arrived], [Comment], [NotificationReminder], [primaryBookingID]) VALUES (N'265662731 ', N'Slo7      ', N'612648662678214805485         ', N'580312424824681240311         ', CAST(N'2018-09-19T00:00:00.000' AS DateTime), N'N', N'N', N'', 0, N'216307740 ')
INSERT [dbo].[BOOKING] ([BookingID], [SlotNo], [CustomerID], [StylistID], [Date], [Available], [Arrived], [Comment], [NotificationReminder], [primaryBookingID]) VALUES (N'267247235 ', N'Slo1      ', N'215703134623237784686         ', N'580312424824681240311         ', CAST(N'2018-09-22T00:00:00.000' AS DateTime), N'N', N'Y', N'', 1, N'267247235 ')
INSERT [dbo].[BOOKING] ([BookingID], [SlotNo], [CustomerID], [StylistID], [Date], [Available], [Arrived], [Comment], [NotificationReminder], [primaryBookingID]) VALUES (N'276220240 ', N'Slo14     ', N'612648662678214805485         ', N'118233419479102946333         ', CAST(N'2018-10-07T00:00:00.000' AS DateTime), N'N', N'N', N'', 1, N'276220240 ')
INSERT [dbo].[BOOKING] ([BookingID], [SlotNo], [CustomerID], [StylistID], [Date], [Available], [Arrived], [Comment], [NotificationReminder], [primaryBookingID]) VALUES (N'282323476 ', N'Slo14     ', N'407242734406745485560         ', N'118233419479102946333         ', CAST(N'2018-10-17T00:00:00.000' AS DateTime), N'N', N'N', N'', 0, N'282323476 ')
INSERT [dbo].[BOOKING] ([BookingID], [SlotNo], [CustomerID], [StylistID], [Date], [Available], [Arrived], [Comment], [NotificationReminder], [primaryBookingID]) VALUES (N'285352471 ', N'Slo10     ', N'535314300265460205603         ', N'270146203840160705048         ', CAST(N'2018-10-11T00:00:00.000' AS DateTime), N'N', N'Y', N'', 1, N'285352471 ')
INSERT [dbo].[BOOKING] ([BookingID], [SlotNo], [CustomerID], [StylistID], [Date], [Available], [Arrived], [Comment], [NotificationReminder], [primaryBookingID]) VALUES (N'286404016 ', N'Slo11     ', N'384715112175204214480         ', N'118233419479102946333         ', CAST(N'2018-10-11T00:00:00.000' AS DateTime), N'N', N'Y', N'', 1, N'286404016 ')
INSERT [dbo].[BOOKING] ([BookingID], [SlotNo], [CustomerID], [StylistID], [Date], [Available], [Arrived], [Comment], [NotificationReminder], [primaryBookingID]) VALUES (N'287647015 ', N'Slo7      ', N'612648662678214805485         ', N'118233419479102946333         ', CAST(N'2018-09-20T00:00:00.000' AS DateTime), N'N', N'N', N'', 1, N'071046156 ')
INSERT [dbo].[BOOKING] ([BookingID], [SlotNo], [CustomerID], [StylistID], [Date], [Available], [Arrived], [Comment], [NotificationReminder], [primaryBookingID]) VALUES (N'300017033 ', N'Slo5      ', N'215703134623237784686         ', N'580312424824681240311         ', CAST(N'2018-09-03T00:00:00.000' AS DateTime), N'N', N'Y', N'Likes Aunt Jackie''s Shampoo', 0, N'300017033 ')
INSERT [dbo].[BOOKING] ([BookingID], [SlotNo], [CustomerID], [StylistID], [Date], [Available], [Arrived], [Comment], [NotificationReminder], [primaryBookingID]) VALUES (N'306284054 ', N'Slo12     ', N'635506662246576754303         ', N'580312424824681240311         ', CAST(N'2018-09-17T00:00:00.000' AS DateTime), N'N', N'N', N'', 1, N'403034175 ')
INSERT [dbo].[BOOKING] ([BookingID], [SlotNo], [CustomerID], [StylistID], [Date], [Available], [Arrived], [Comment], [NotificationReminder], [primaryBookingID]) VALUES (N'315088203 ', N'Slo19     ', N'635506662246576754303         ', N'580312424824681240311         ', CAST(N'2018-09-17T00:00:00.000' AS DateTime), N'N', N'N', N'', 1, N'403034175 ')
INSERT [dbo].[BOOKING] ([BookingID], [SlotNo], [CustomerID], [StylistID], [Date], [Available], [Arrived], [Comment], [NotificationReminder], [primaryBookingID]) VALUES (N'315667782 ', N'Slo10     ', N'105242998585655922697         ', N'270146203840160705048         ', CAST(N'2018-10-13T00:00:00.000' AS DateTime), N'N', N'Y', N'', 1, N'315667782 ')
INSERT [dbo].[BOOKING] ([BookingID], [SlotNo], [CustomerID], [StylistID], [Date], [Available], [Arrived], [Comment], [NotificationReminder], [primaryBookingID]) VALUES (N'324380341 ', N'Slo7      ', N'215703134623237784686         ', N'580312424824681240311         ', CAST(N'2018-09-22T00:00:00.000' AS DateTime), N'N', N'Y', N'', 1, N'267247235 ')
INSERT [dbo].[BOOKING] ([BookingID], [SlotNo], [CustomerID], [StylistID], [Date], [Available], [Arrived], [Comment], [NotificationReminder], [primaryBookingID]) VALUES (N'326472826 ', N'Slo12     ', N'535314300265460205603         ', N'580312424824681240311         ', CAST(N'2018-09-19T00:00:00.000' AS DateTime), N'N', N'Y', N'', 0, N'844052626 ')
INSERT [dbo].[BOOKING] ([BookingID], [SlotNo], [CustomerID], [StylistID], [Date], [Available], [Arrived], [Comment], [NotificationReminder], [primaryBookingID]) VALUES (N'333333333 ', N'Slo9      ', N'686374338720721300668         ', N'118233419479102946333         ', CAST(N'2018-05-02T00:00:00.000' AS DateTime), N'N', N'N', NULL, 1, N'333333333 ')
INSERT [dbo].[BOOKING] ([BookingID], [SlotNo], [CustomerID], [StylistID], [Date], [Available], [Arrived], [Comment], [NotificationReminder], [primaryBookingID]) VALUES (N'334545735 ', N'Slo12     ', N'384715112175204214480         ', N'105450503348172771121         ', CAST(N'2018-08-21T00:00:00.000' AS DateTime), N'N', N'N', NULL, 1, N'334545735 ')
INSERT [dbo].[BOOKING] ([BookingID], [SlotNo], [CustomerID], [StylistID], [Date], [Available], [Arrived], [Comment], [NotificationReminder], [primaryBookingID]) VALUES (N'350634301 ', N'Slo8      ', N'215703134623237784686         ', N'270146203840160705048         ', CAST(N'2018-09-18T00:00:00.000' AS DateTime), N'N', N'Y', N'', 0, N'564022384 ')
INSERT [dbo].[BOOKING] ([BookingID], [SlotNo], [CustomerID], [StylistID], [Date], [Available], [Arrived], [Comment], [NotificationReminder], [primaryBookingID]) VALUES (N'351153017 ', N'Slo11     ', N'635506662246576754303         ', N'270146203840160705048         ', CAST(N'2018-10-01T00:00:00.000' AS DateTime), N'N', N'Y', N'', 0, N'351153017 ')
INSERT [dbo].[BOOKING] ([BookingID], [SlotNo], [CustomerID], [StylistID], [Date], [Available], [Arrived], [Comment], [NotificationReminder], [primaryBookingID]) VALUES (N'358127358 ', N'Slo16     ', N'635506662246576754303         ', N'580312424824681240311         ', CAST(N'2018-09-28T00:00:00.000' AS DateTime), N'N', N'N', N'', 1, N'413046600 ')
INSERT [dbo].[BOOKING] ([BookingID], [SlotNo], [CustomerID], [StylistID], [Date], [Available], [Arrived], [Comment], [NotificationReminder], [primaryBookingID]) VALUES (N'358180023 ', N'Slo14     ', N'612648662678214805485         ', N'118233419479102946333         ', CAST(N'2018-09-05T00:00:00.000' AS DateTime), N'N', N'Y', NULL, 1, N'358180023 ')
INSERT [dbo].[BOOKING] ([BookingID], [SlotNo], [CustomerID], [StylistID], [Date], [Available], [Arrived], [Comment], [NotificationReminder], [primaryBookingID]) VALUES (N'358730881 ', N'Slo14     ', N'181477264752348604465         ', N'580312424824681240311         ', CAST(N'2018-09-21T00:00:00.000' AS DateTime), N'N', N'Y', N'', 0, N'418380632 ')
INSERT [dbo].[BOOKING] ([BookingID], [SlotNo], [CustomerID], [StylistID], [Date], [Available], [Arrived], [Comment], [NotificationReminder], [primaryBookingID]) VALUES (N'360415502 ', N'Slo12     ', N'662881846588586465447         ', N'580312424824681240311         ', CAST(N'2018-09-01T00:00:00.000' AS DateTime), N'N', N'Y', N'', 0, N'574784585 ')
INSERT [dbo].[BOOKING] ([BookingID], [SlotNo], [CustomerID], [StylistID], [Date], [Available], [Arrived], [Comment], [NotificationReminder], [primaryBookingID]) VALUES (N'363731045 ', N'Slo3      ', N'662881846588586465447         ', N'580312424824681240311         ', CAST(N'2018-08-30T00:00:00.000' AS DateTime), N'N', N'N', NULL, 1, N'363731045 ')
INSERT [dbo].[BOOKING] ([BookingID], [SlotNo], [CustomerID], [StylistID], [Date], [Available], [Arrived], [Comment], [NotificationReminder], [primaryBookingID]) VALUES (N'365450324 ', N'Slo7      ', N'100155997350724101850         ', N'118233419479102946333         ', CAST(N'2018-08-26T00:00:00.000' AS DateTime), N'N', N'Y', NULL, 1, N'365450324 ')
INSERT [dbo].[BOOKING] ([BookingID], [SlotNo], [CustomerID], [StylistID], [Date], [Available], [Arrived], [Comment], [NotificationReminder], [primaryBookingID]) VALUES (N'365636365 ', N'Slo12     ', N'100155997350724101850         ', N'270146203840160705048         ', CAST(N'2018-08-18T00:00:00.000' AS DateTime), N'N', N'Y', NULL, 0, N'365636365 ')
INSERT [dbo].[BOOKING] ([BookingID], [SlotNo], [CustomerID], [StylistID], [Date], [Available], [Arrived], [Comment], [NotificationReminder], [primaryBookingID]) VALUES (N'367253433 ', N'Slo15     ', N'535314300265460205603         ', N'580312424824681240311         ', CAST(N'2018-09-19T00:00:00.000' AS DateTime), N'N', N'Y', N'', 0, N'844052626 ')
INSERT [dbo].[BOOKING] ([BookingID], [SlotNo], [CustomerID], [StylistID], [Date], [Available], [Arrived], [Comment], [NotificationReminder], [primaryBookingID]) VALUES (N'371672637 ', N'Slo10     ', N'215703134623237784686         ', N'105450503348172771121         ', CAST(N'2018-10-20T00:00:00.000' AS DateTime), N'N', N'N', N'', 0, N'371672637 ')
INSERT [dbo].[BOOKING] ([BookingID], [SlotNo], [CustomerID], [StylistID], [Date], [Available], [Arrived], [Comment], [NotificationReminder], [primaryBookingID]) VALUES (N'378464537 ', N'Slo7      ', N'181477264752348604465         ', N'105450503348172771121         ', CAST(N'2018-08-31T00:00:00.000' AS DateTime), N'N', N'Y', NULL, 1, N'378464537 ')
INSERT [dbo].[BOOKING] ([BookingID], [SlotNo], [CustomerID], [StylistID], [Date], [Available], [Arrived], [Comment], [NotificationReminder], [primaryBookingID]) VALUES (N'383234444 ', N'Slo9      ', N'535314300265460205603         ', N'580312424824681240311         ', CAST(N'2018-09-01T00:00:00.000' AS DateTime), N'N', N'Y', N'', 0, N'433783285 ')
INSERT [dbo].[BOOKING] ([BookingID], [SlotNo], [CustomerID], [StylistID], [Date], [Available], [Arrived], [Comment], [NotificationReminder], [primaryBookingID]) VALUES (N'386062657 ', N'Slo14     ', N'662881846588586465447         ', N'118233419479102946333         ', CAST(N'2018-11-06T00:00:00.000' AS DateTime), N'N', N'N', NULL, 0, N'718042600 ')
INSERT [dbo].[BOOKING] ([BookingID], [SlotNo], [CustomerID], [StylistID], [Date], [Available], [Arrived], [Comment], [NotificationReminder], [primaryBookingID]) VALUES (N'403034175 ', N'Slo10     ', N'635506662246576754303         ', N'580312424824681240311         ', CAST(N'2018-09-17T00:00:00.000' AS DateTime), N'N', N'N', N'', 1, N'403034175 ')
INSERT [dbo].[BOOKING] ([BookingID], [SlotNo], [CustomerID], [StylistID], [Date], [Available], [Arrived], [Comment], [NotificationReminder], [primaryBookingID]) VALUES (N'405854081 ', N'Slo10     ', N'384715112175204214480         ', N'118233419479102946333         ', CAST(N'2018-09-02T00:00:00.000' AS DateTime), N'N', N'Y', N'', 1, N'405854081 ')
INSERT [dbo].[BOOKING] ([BookingID], [SlotNo], [CustomerID], [StylistID], [Date], [Available], [Arrived], [Comment], [NotificationReminder], [primaryBookingID]) VALUES (N'413046600 ', N'Slo15     ', N'635506662246576754303         ', N'580312424824681240311         ', CAST(N'2018-09-28T00:00:00.000' AS DateTime), N'N', N'N', N'', 1, N'413046600 ')
INSERT [dbo].[BOOKING] ([BookingID], [SlotNo], [CustomerID], [StylistID], [Date], [Available], [Arrived], [Comment], [NotificationReminder], [primaryBookingID]) VALUES (N'415463542 ', N'Slo7      ', N'215703134623237784686         ', N'270146203840160705048         ', CAST(N'2018-09-18T00:00:00.000' AS DateTime), N'N', N'Y', N'', 0, N'564022384 ')
GO
INSERT [dbo].[BOOKING] ([BookingID], [SlotNo], [CustomerID], [StylistID], [Date], [Available], [Arrived], [Comment], [NotificationReminder], [primaryBookingID]) VALUES (N'418380632 ', N'Slo11     ', N'181477264752348604465         ', N'580312424824681240311         ', CAST(N'2018-09-21T00:00:00.000' AS DateTime), N'N', N'Y', N'', 0, N'418380632 ')
INSERT [dbo].[BOOKING] ([BookingID], [SlotNo], [CustomerID], [StylistID], [Date], [Available], [Arrived], [Comment], [NotificationReminder], [primaryBookingID]) VALUES (N'428817728 ', N'Slo16     ', N'612648662678214805485         ', N'580312424824681240311         ', CAST(N'2018-09-21T00:00:00.000' AS DateTime), N'N', N'Y', N'', 0, N'568466551 ')
INSERT [dbo].[BOOKING] ([BookingID], [SlotNo], [CustomerID], [StylistID], [Date], [Available], [Arrived], [Comment], [NotificationReminder], [primaryBookingID]) VALUES (N'430481072 ', N'Slo6      ', N'612648662678214805485         ', N'580312424824681240311         ', CAST(N'2018-09-19T00:00:00.000' AS DateTime), N'N', N'N', N'', 0, N'216307740 ')
INSERT [dbo].[BOOKING] ([BookingID], [SlotNo], [CustomerID], [StylistID], [Date], [Available], [Arrived], [Comment], [NotificationReminder], [primaryBookingID]) VALUES (N'433072775 ', N'Slo14     ', N'535314300265460205603         ', N'580312424824681240311         ', CAST(N'2018-09-19T00:00:00.000' AS DateTime), N'N', N'Y', N'', 0, N'844052626 ')
INSERT [dbo].[BOOKING] ([BookingID], [SlotNo], [CustomerID], [StylistID], [Date], [Available], [Arrived], [Comment], [NotificationReminder], [primaryBookingID]) VALUES (N'433783285 ', N'Slo8      ', N'535314300265460205603         ', N'580312424824681240311         ', CAST(N'2018-09-01T00:00:00.000' AS DateTime), N'N', N'Y', N'', 0, N'433783285 ')
INSERT [dbo].[BOOKING] ([BookingID], [SlotNo], [CustomerID], [StylistID], [Date], [Available], [Arrived], [Comment], [NotificationReminder], [primaryBookingID]) VALUES (N'435337468 ', N'Slo15     ', N'307776754475328666200         ', N'118233419479102946333         ', CAST(N'2018-10-22T00:00:00.000' AS DateTime), N'N', N'N', N'', 0, N'053817368 ')
INSERT [dbo].[BOOKING] ([BookingID], [SlotNo], [CustomerID], [StylistID], [Date], [Available], [Arrived], [Comment], [NotificationReminder], [primaryBookingID]) VALUES (N'442182057 ', N'Slo11     ', N'612648662678214805485         ', N'270146203840160705048         ', CAST(N'2018-10-18T00:00:00.000' AS DateTime), N'N', N'N', N'', 0, N'442182057 ')
INSERT [dbo].[BOOKING] ([BookingID], [SlotNo], [CustomerID], [StylistID], [Date], [Available], [Arrived], [Comment], [NotificationReminder], [primaryBookingID]) VALUES (N'444444444 ', N'Slo4      ', N'181477264752348604465         ', N'118233419479102946333         ', CAST(N'2018-08-09T00:00:00.000' AS DateTime), N'N', N'Y', NULL, 1, N'444444444 ')
INSERT [dbo].[BOOKING] ([BookingID], [SlotNo], [CustomerID], [StylistID], [Date], [Available], [Arrived], [Comment], [NotificationReminder], [primaryBookingID]) VALUES (N'445245670 ', N'Slo11     ', N'324465860528272078866         ', N'118233419479102946333         ', CAST(N'2018-10-23T00:00:00.000' AS DateTime), N'N', N'N', N'', 0, N'866225622 ')
INSERT [dbo].[BOOKING] ([BookingID], [SlotNo], [CustomerID], [StylistID], [Date], [Available], [Arrived], [Comment], [NotificationReminder], [primaryBookingID]) VALUES (N'454466600 ', N'Slo8      ', N'181477264752348604465         ', N'118233419479102946333         ', CAST(N'2018-10-21T00:00:00.000' AS DateTime), N'N', N'N', N'', 0, N'180724634 ')
INSERT [dbo].[BOOKING] ([BookingID], [SlotNo], [CustomerID], [StylistID], [Date], [Available], [Arrived], [Comment], [NotificationReminder], [primaryBookingID]) VALUES (N'455161654 ', N'Slo6      ', N'105242998585655922697         ', N'270146203840160705048         ', CAST(N'2018-08-17T00:00:00.000' AS DateTime), N'N', N'Y', NULL, 1, N'455161654 ')
INSERT [dbo].[BOOKING] ([BookingID], [SlotNo], [CustomerID], [StylistID], [Date], [Available], [Arrived], [Comment], [NotificationReminder], [primaryBookingID]) VALUES (N'455548045 ', N'Slo15     ', N'635506662246576754303         ', N'580312424824681240311         ', CAST(N'2018-09-17T00:00:00.000' AS DateTime), N'N', N'N', N'', 1, N'403034175 ')
INSERT [dbo].[BOOKING] ([BookingID], [SlotNo], [CustomerID], [StylistID], [Date], [Available], [Arrived], [Comment], [NotificationReminder], [primaryBookingID]) VALUES (N'464848183 ', N'Slo3      ', N'215703134623237784686         ', N'580312424824681240311         ', CAST(N'2018-09-22T00:00:00.000' AS DateTime), N'N', N'Y', N'', 1, N'267247235 ')
INSERT [dbo].[BOOKING] ([BookingID], [SlotNo], [CustomerID], [StylistID], [Date], [Available], [Arrived], [Comment], [NotificationReminder], [primaryBookingID]) VALUES (N'467420776 ', N'Slo12     ', N'215703134623237784686         ', N'580312424824681240311         ', CAST(N'2018-10-04T00:00:00.000' AS DateTime), N'N', N'Y', N'', 1, N'467420776 ')
INSERT [dbo].[BOOKING] ([BookingID], [SlotNo], [CustomerID], [StylistID], [Date], [Available], [Arrived], [Comment], [NotificationReminder], [primaryBookingID]) VALUES (N'471565888 ', N'Slo20     ', N'635506662246576754303         ', N'270146203840160705048         ', CAST(N'2018-08-27T00:00:00.000' AS DateTime), N'N', N'Y', NULL, 0, N'471565888 ')
INSERT [dbo].[BOOKING] ([BookingID], [SlotNo], [CustomerID], [StylistID], [Date], [Available], [Arrived], [Comment], [NotificationReminder], [primaryBookingID]) VALUES (N'472263688 ', N'Slo9      ', N'612648662678214805485         ', N'580312424824681240311         ', CAST(N'2018-09-19T00:00:00.000' AS DateTime), N'N', N'N', N'', 0, N'216307740 ')
INSERT [dbo].[BOOKING] ([BookingID], [SlotNo], [CustomerID], [StylistID], [Date], [Available], [Arrived], [Comment], [NotificationReminder], [primaryBookingID]) VALUES (N'480304302 ', N'Slo12     ', N'105242998585655922697         ', N'105450503348172771121         ', CAST(N'2018-09-10T00:00:00.000' AS DateTime), N'N', N'Y', N'Likes Aunt Jackie''s Shampoo', 0, N'127874202 ')
INSERT [dbo].[BOOKING] ([BookingID], [SlotNo], [CustomerID], [StylistID], [Date], [Available], [Arrived], [Comment], [NotificationReminder], [primaryBookingID]) VALUES (N'481287255 ', N'Slo12     ', N'662881846588586465447         ', N'105450503348172771121         ', CAST(N'2018-09-08T00:00:00.000' AS DateTime), N'N', N'N', N'', 0, N'481287255 ')
INSERT [dbo].[BOOKING] ([BookingID], [SlotNo], [CustomerID], [StylistID], [Date], [Available], [Arrived], [Comment], [NotificationReminder], [primaryBookingID]) VALUES (N'483636551 ', N'Slo1      ', N'181477264752348604465         ', N'118233419479102946333         ', CAST(N'2018-08-28T00:00:00.000' AS DateTime), N'N', N'Y', NULL, 0, N'483636551 ')
INSERT [dbo].[BOOKING] ([BookingID], [SlotNo], [CustomerID], [StylistID], [Date], [Available], [Arrived], [Comment], [NotificationReminder], [primaryBookingID]) VALUES (N'487601070 ', N'Slo15     ', N'777706452281511868742         ', N'118233419479102946333         ', CAST(N'2018-09-23T00:00:00.000' AS DateTime), N'N', N'Y', N'', 0, N'653420332 ')
INSERT [dbo].[BOOKING] ([BookingID], [SlotNo], [CustomerID], [StylistID], [Date], [Available], [Arrived], [Comment], [NotificationReminder], [primaryBookingID]) VALUES (N'500868880 ', N'Slo7      ', N'535314300265460205603         ', N'105450503348172771121         ', CAST(N'2018-09-24T00:00:00.000' AS DateTime), N'N', N'N', N'', 1, N'550428622 ')
INSERT [dbo].[BOOKING] ([BookingID], [SlotNo], [CustomerID], [StylistID], [Date], [Available], [Arrived], [Comment], [NotificationReminder], [primaryBookingID]) VALUES (N'511652873 ', N'Slo10     ', N'105242998585655922697         ', N'105450503348172771121         ', CAST(N'2018-10-05T00:00:00.000' AS DateTime), N'N', N'Y', N'', 1, N'511652873 ')
INSERT [dbo].[BOOKING] ([BookingID], [SlotNo], [CustomerID], [StylistID], [Date], [Available], [Arrived], [Comment], [NotificationReminder], [primaryBookingID]) VALUES (N'513073350 ', N'Slo7      ', N'100155997350724101850         ', N'118233419479102946333         ', CAST(N'2018-09-16T00:00:00.000' AS DateTime), N'N', N'N', N'', 1, N'552486404 ')
INSERT [dbo].[BOOKING] ([BookingID], [SlotNo], [CustomerID], [StylistID], [Date], [Available], [Arrived], [Comment], [NotificationReminder], [primaryBookingID]) VALUES (N'516516516 ', N'Slo5      ', N'635506662246576754303         ', N'105450503348172771121         ', CAST(N'2018-05-02T00:00:00.000' AS DateTime), N'N', N'Y', NULL, 1, N'516516516 ')
INSERT [dbo].[BOOKING] ([BookingID], [SlotNo], [CustomerID], [StylistID], [Date], [Available], [Arrived], [Comment], [NotificationReminder], [primaryBookingID]) VALUES (N'521727418 ', N'Slo3      ', N'662881846588586465447         ', N'270146203840160705048         ', CAST(N'2018-09-01T00:00:00.000' AS DateTime), N'N', N'N', N'', 1, N'521727418 ')
INSERT [dbo].[BOOKING] ([BookingID], [SlotNo], [CustomerID], [StylistID], [Date], [Available], [Arrived], [Comment], [NotificationReminder], [primaryBookingID]) VALUES (N'525623472 ', N'Slo13     ', N'181477264752348604465         ', N'118233419479102946333         ', CAST(N'2018-10-14T00:00:00.000' AS DateTime), N'N', N'Y', N'', 1, N'525623472 ')
INSERT [dbo].[BOOKING] ([BookingID], [SlotNo], [CustomerID], [StylistID], [Date], [Available], [Arrived], [Comment], [NotificationReminder], [primaryBookingID]) VALUES (N'525763663 ', N'Slo3      ', N'105242998585655922697         ', N'580312424824681240311         ', CAST(N'2018-08-14T00:00:00.000' AS DateTime), N'N', N'Y', NULL, 1, N'525763663 ')
INSERT [dbo].[BOOKING] ([BookingID], [SlotNo], [CustomerID], [StylistID], [Date], [Available], [Arrived], [Comment], [NotificationReminder], [primaryBookingID]) VALUES (N'531881200 ', N'Slo9      ', N'215703134623237784686         ', N'580312424824681240311         ', CAST(N'2018-09-22T00:00:00.000' AS DateTime), N'N', N'Y', N'', 1, N'267247235 ')
INSERT [dbo].[BOOKING] ([BookingID], [SlotNo], [CustomerID], [StylistID], [Date], [Available], [Arrived], [Comment], [NotificationReminder], [primaryBookingID]) VALUES (N'538667335 ', N'Slo2      ', N'215703134623237784686         ', N'580312424824681240311         ', CAST(N'2018-09-22T00:00:00.000' AS DateTime), N'N', N'Y', N'', 1, N'267247235 ')
INSERT [dbo].[BOOKING] ([BookingID], [SlotNo], [CustomerID], [StylistID], [Date], [Available], [Arrived], [Comment], [NotificationReminder], [primaryBookingID]) VALUES (N'543446466 ', N'Slo17     ', N'100155997350724101850         ', N'580312424824681240311         ', CAST(N'2018-08-21T00:00:00.000' AS DateTime), N'N', N'Y', NULL, 1, N'543446466 ')
INSERT [dbo].[BOOKING] ([BookingID], [SlotNo], [CustomerID], [StylistID], [Date], [Available], [Arrived], [Comment], [NotificationReminder], [primaryBookingID]) VALUES (N'545646646 ', N'Slo16     ', N'181477264752348604465         ', N'118233419479102946333         ', CAST(N'2018-08-13T00:00:00.000' AS DateTime), N'N', N'N', NULL, 1, N'545646646 ')
INSERT [dbo].[BOOKING] ([BookingID], [SlotNo], [CustomerID], [StylistID], [Date], [Available], [Arrived], [Comment], [NotificationReminder], [primaryBookingID]) VALUES (N'547225656 ', N'Slo14     ', N'215703134623237784686         ', N'270146203840160705048         ', CAST(N'2018-08-27T00:00:00.000' AS DateTime), N'N', N'Y', NULL, 0, N'547225656 ')
INSERT [dbo].[BOOKING] ([BookingID], [SlotNo], [CustomerID], [StylistID], [Date], [Available], [Arrived], [Comment], [NotificationReminder], [primaryBookingID]) VALUES (N'550428622 ', N'Slo3      ', N'535314300265460205603         ', N'105450503348172771121         ', CAST(N'2018-09-24T00:00:00.000' AS DateTime), N'N', N'N', N'', 1, N'550428622 ')
INSERT [dbo].[BOOKING] ([BookingID], [SlotNo], [CustomerID], [StylistID], [Date], [Available], [Arrived], [Comment], [NotificationReminder], [primaryBookingID]) VALUES (N'552486404 ', N'Slo6      ', N'100155997350724101850         ', N'118233419479102946333         ', CAST(N'2018-09-16T00:00:00.000' AS DateTime), N'N', N'N', N'', 1, N'552486404 ')
INSERT [dbo].[BOOKING] ([BookingID], [SlotNo], [CustomerID], [StylistID], [Date], [Available], [Arrived], [Comment], [NotificationReminder], [primaryBookingID]) VALUES (N'554743686 ', N'Slo15     ', N'407242734406745485560         ', N'118233419479102946333         ', CAST(N'2018-10-17T00:00:00.000' AS DateTime), N'N', N'N', N'', 0, N'282323476 ')
INSERT [dbo].[BOOKING] ([BookingID], [SlotNo], [CustomerID], [StylistID], [Date], [Available], [Arrived], [Comment], [NotificationReminder], [primaryBookingID]) VALUES (N'555555555 ', N'Slo17     ', N'105242998585655922697         ', N'270146203840160705048         ', CAST(N'2018-07-31T00:00:00.000' AS DateTime), N'N', N'Y', NULL, 1, N'555555555 ')
INSERT [dbo].[BOOKING] ([BookingID], [SlotNo], [CustomerID], [StylistID], [Date], [Available], [Arrived], [Comment], [NotificationReminder], [primaryBookingID]) VALUES (N'560267375 ', N'Slo5      ', N'535314300265460205603         ', N'580312424824681240311         ', CAST(N'2018-10-20T00:00:00.000' AS DateTime), N'N', N'N', N'', 0, N'030364610 ')
INSERT [dbo].[BOOKING] ([BookingID], [SlotNo], [CustomerID], [StylistID], [Date], [Available], [Arrived], [Comment], [NotificationReminder], [primaryBookingID]) VALUES (N'561520075 ', N'Slo11     ', N'407242734406745485560         ', N'270146203840160705048         ', CAST(N'2018-10-09T00:00:00.000' AS DateTime), N'N', N'Y', N'', 0, N'561520075 ')
INSERT [dbo].[BOOKING] ([BookingID], [SlotNo], [CustomerID], [StylistID], [Date], [Available], [Arrived], [Comment], [NotificationReminder], [primaryBookingID]) VALUES (N'561561561 ', N'Slo8      ', N'635506662246576754303         ', N'270146203840160705048         ', CAST(N'2018-08-21T00:00:00.000' AS DateTime), N'N', N'Y', NULL, 1, N'561561561 ')
INSERT [dbo].[BOOKING] ([BookingID], [SlotNo], [CustomerID], [StylistID], [Date], [Available], [Arrived], [Comment], [NotificationReminder], [primaryBookingID]) VALUES (N'562240804 ', N'Slo17     ', N'635506662246576754303         ', N'580312424824681240311         ', CAST(N'2018-09-17T00:00:00.000' AS DateTime), N'N', N'N', N'', 1, N'403034175 ')
INSERT [dbo].[BOOKING] ([BookingID], [SlotNo], [CustomerID], [StylistID], [Date], [Available], [Arrived], [Comment], [NotificationReminder], [primaryBookingID]) VALUES (N'564022384 ', N'Slo6      ', N'215703134623237784686         ', N'270146203840160705048         ', CAST(N'2018-09-18T00:00:00.000' AS DateTime), N'N', N'Y', N'', 0, N'564022384 ')
INSERT [dbo].[BOOKING] ([BookingID], [SlotNo], [CustomerID], [StylistID], [Date], [Available], [Arrived], [Comment], [NotificationReminder], [primaryBookingID]) VALUES (N'564964895 ', N'Slo20     ', N'105242998585655922697         ', N'580312424824681240311         ', CAST(N'2018-08-09T00:00:00.000' AS DateTime), N'N', N'Y', NULL, 1, N'564964895 ')
INSERT [dbo].[BOOKING] ([BookingID], [SlotNo], [CustomerID], [StylistID], [Date], [Available], [Arrived], [Comment], [NotificationReminder], [primaryBookingID]) VALUES (N'565073505 ', N'Slo1      ', N'215703134623237784686         ', N'105450503348172771121         ', CAST(N'2018-10-12T00:00:00.000' AS DateTime), N'N', N'Y', N'', 1, N'565073505 ')
INSERT [dbo].[BOOKING] ([BookingID], [SlotNo], [CustomerID], [StylistID], [Date], [Available], [Arrived], [Comment], [NotificationReminder], [primaryBookingID]) VALUES (N'568466551 ', N'Slo15     ', N'612648662678214805485         ', N'580312424824681240311         ', CAST(N'2018-09-21T00:00:00.000' AS DateTime), N'N', N'Y', N'', 0, N'568466551 ')
INSERT [dbo].[BOOKING] ([BookingID], [SlotNo], [CustomerID], [StylistID], [Date], [Available], [Arrived], [Comment], [NotificationReminder], [primaryBookingID]) VALUES (N'574784585 ', N'Slo11     ', N'662881846588586465447         ', N'580312424824681240311         ', CAST(N'2018-09-01T00:00:00.000' AS DateTime), N'N', N'Y', N'', 0, N'574784585 ')
INSERT [dbo].[BOOKING] ([BookingID], [SlotNo], [CustomerID], [StylistID], [Date], [Available], [Arrived], [Comment], [NotificationReminder], [primaryBookingID]) VALUES (N'575210227 ', N'Slo2      ', N'181477264752348604465         ', N'118233419479102946333         ', CAST(N'2018-09-04T00:00:00.000' AS DateTime), N'N', N'Y', NULL, 1, N'873657564 ')
INSERT [dbo].[BOOKING] ([BookingID], [SlotNo], [CustomerID], [StylistID], [Date], [Available], [Arrived], [Comment], [NotificationReminder], [primaryBookingID]) VALUES (N'580124881 ', N'Slo20     ', N'535314300265460205603         ', N'580312424824681240311         ', CAST(N'2018-09-19T00:00:00.000' AS DateTime), N'N', N'Y', N'', 0, N'844052626 ')
INSERT [dbo].[BOOKING] ([BookingID], [SlotNo], [CustomerID], [StylistID], [Date], [Available], [Arrived], [Comment], [NotificationReminder], [primaryBookingID]) VALUES (N'616466824 ', N'Slo4      ', N'635506662246576754303         ', N'270146203840160705048         ', CAST(N'2018-09-08T00:00:00.000' AS DateTime), N'N', N'N', NULL, 0, N'616466824 ')
INSERT [dbo].[BOOKING] ([BookingID], [SlotNo], [CustomerID], [StylistID], [Date], [Available], [Arrived], [Comment], [NotificationReminder], [primaryBookingID]) VALUES (N'620546558 ', N'Slo17     ', N'635506662246576754303         ', N'580312424824681240311         ', CAST(N'2018-09-28T00:00:00.000' AS DateTime), N'N', N'N', N'', 1, N'413046600 ')
INSERT [dbo].[BOOKING] ([BookingID], [SlotNo], [CustomerID], [StylistID], [Date], [Available], [Arrived], [Comment], [NotificationReminder], [primaryBookingID]) VALUES (N'622154401 ', N'Slo9      ', N'215703134623237784686         ', N'270146203840160705048         ', CAST(N'2018-09-18T00:00:00.000' AS DateTime), N'N', N'Y', N'', 0, N'564022384 ')
INSERT [dbo].[BOOKING] ([BookingID], [SlotNo], [CustomerID], [StylistID], [Date], [Available], [Arrived], [Comment], [NotificationReminder], [primaryBookingID]) VALUES (N'622473016 ', N'Slo19     ', N'535314300265460205603         ', N'118233419479102946333         ', CAST(N'2018-08-26T00:00:00.000' AS DateTime), N'N', N'Y', NULL, 0, N'622473016 ')
INSERT [dbo].[BOOKING] ([BookingID], [SlotNo], [CustomerID], [StylistID], [Date], [Available], [Arrived], [Comment], [NotificationReminder], [primaryBookingID]) VALUES (N'625086526 ', N'Slo4      ', N'535314300265460205603         ', N'580312424824681240311         ', CAST(N'2018-10-20T00:00:00.000' AS DateTime), N'N', N'N', N'', 0, N'030364610 ')
INSERT [dbo].[BOOKING] ([BookingID], [SlotNo], [CustomerID], [StylistID], [Date], [Available], [Arrived], [Comment], [NotificationReminder], [primaryBookingID]) VALUES (N'630673633 ', N'Slo16     ', N'535314300265460205603         ', N'580312424824681240311         ', CAST(N'2018-09-19T00:00:00.000' AS DateTime), N'N', N'Y', N'', 0, N'844052626 ')
INSERT [dbo].[BOOKING] ([BookingID], [SlotNo], [CustomerID], [StylistID], [Date], [Available], [Arrived], [Comment], [NotificationReminder], [primaryBookingID]) VALUES (N'632574028 ', N'Slo10     ', N'324465860528272078866         ', N'105450503348172771121         ', CAST(N'2018-10-15T00:00:00.000' AS DateTime), N'N', N'Y', N'', 0, N'632574028 ')
INSERT [dbo].[BOOKING] ([BookingID], [SlotNo], [CustomerID], [StylistID], [Date], [Available], [Arrived], [Comment], [NotificationReminder], [primaryBookingID]) VALUES (N'634365345 ', N'Slo7      ', N'215703134623237784686         ', N'580312424824681240311         ', CAST(N'2018-09-30T00:00:00.000' AS DateTime), N'N', N'N', N'', 0, N'634365345 ')
INSERT [dbo].[BOOKING] ([BookingID], [SlotNo], [CustomerID], [StylistID], [Date], [Available], [Arrived], [Comment], [NotificationReminder], [primaryBookingID]) VALUES (N'635418676 ', N'Slo18     ', N'612648662678214805485         ', N'580312424824681240311         ', CAST(N'2018-09-21T00:00:00.000' AS DateTime), N'N', N'Y', N'', 0, N'568466551 ')
INSERT [dbo].[BOOKING] ([BookingID], [SlotNo], [CustomerID], [StylistID], [Date], [Available], [Arrived], [Comment], [NotificationReminder], [primaryBookingID]) VALUES (N'636082830 ', N'Slo8      ', N'612648662678214805485         ', N'580312424824681240311         ', CAST(N'2018-09-19T00:00:00.000' AS DateTime), N'N', N'N', N'', 0, N'216307740 ')
INSERT [dbo].[BOOKING] ([BookingID], [SlotNo], [CustomerID], [StylistID], [Date], [Available], [Arrived], [Comment], [NotificationReminder], [primaryBookingID]) VALUES (N'638250468 ', N'Slo1      ', N'635506662246576754303         ', N'580312424824681240311         ', CAST(N'2018-10-17T00:00:00.000' AS DateTime), N'N', N'N', N'', 0, N'638250468 ')
INSERT [dbo].[BOOKING] ([BookingID], [SlotNo], [CustomerID], [StylistID], [Date], [Available], [Arrived], [Comment], [NotificationReminder], [primaryBookingID]) VALUES (N'642735612 ', N'Slo13     ', N'662881846588586465447         ', N'580312424824681240311         ', CAST(N'2018-09-01T00:00:00.000' AS DateTime), N'N', N'Y', N'', 0, N'574784585 ')
INSERT [dbo].[BOOKING] ([BookingID], [SlotNo], [CustomerID], [StylistID], [Date], [Available], [Arrived], [Comment], [NotificationReminder], [primaryBookingID]) VALUES (N'645546546 ', N'slo3      ', N'100155997350724101850         ', N'270146203840160705048         ', CAST(N'2018-08-09T00:00:00.000' AS DateTime), N'N', N'Y', NULL, 1, N'645546546 ')
INSERT [dbo].[BOOKING] ([BookingID], [SlotNo], [CustomerID], [StylistID], [Date], [Available], [Arrived], [Comment], [NotificationReminder], [primaryBookingID]) VALUES (N'645646343 ', N'Slo17     ', N'181477264752348604465         ', N'580312424824681240311         ', CAST(N'2018-08-07T00:00:00.000' AS DateTime), N'N', N'Y', NULL, 1, N'645646343 ')
INSERT [dbo].[BOOKING] ([BookingID], [SlotNo], [CustomerID], [StylistID], [Date], [Available], [Arrived], [Comment], [NotificationReminder], [primaryBookingID]) VALUES (N'645646565 ', N'Slo6      ', N'105242998585655922697         ', N'580312424824681240311         ', CAST(N'2018-08-25T00:00:00.000' AS DateTime), N'N', N'Y', NULL, 1, N'645646565 ')
INSERT [dbo].[BOOKING] ([BookingID], [SlotNo], [CustomerID], [StylistID], [Date], [Available], [Arrived], [Comment], [NotificationReminder], [primaryBookingID]) VALUES (N'647260713 ', N'Slo15     ', N'535314300265460205603         ', N'580312424824681240311         ', CAST(N'2018-09-06T00:00:00.000' AS DateTime), N'N', N'N', N'', 0, N'647260713 ')
INSERT [dbo].[BOOKING] ([BookingID], [SlotNo], [CustomerID], [StylistID], [Date], [Available], [Arrived], [Comment], [NotificationReminder], [primaryBookingID]) VALUES (N'651651655 ', N'Slo7      ', N'181477264752348604465         ', N'118233419479102946333         ', CAST(N'2018-08-22T00:00:00.000' AS DateTime), N'N', N'Y', NULL, 1, N'651651655 ')
INSERT [dbo].[BOOKING] ([BookingID], [SlotNo], [CustomerID], [StylistID], [Date], [Available], [Arrived], [Comment], [NotificationReminder], [primaryBookingID]) VALUES (N'653420332 ', N'Slo14     ', N'777706452281511868742         ', N'118233419479102946333         ', CAST(N'2018-09-23T00:00:00.000' AS DateTime), N'N', N'Y', N'', 0, N'653420332 ')
INSERT [dbo].[BOOKING] ([BookingID], [SlotNo], [CustomerID], [StylistID], [Date], [Available], [Arrived], [Comment], [NotificationReminder], [primaryBookingID]) VALUES (N'654456645 ', N'Slo6      ', N'686374338720721300668         ', N'118233419479102946333         ', CAST(N'2018-08-11T00:00:00.000' AS DateTime), N'N', N'Y', NULL, 1, N'654456645 ')
INSERT [dbo].[BOOKING] ([BookingID], [SlotNo], [CustomerID], [StylistID], [Date], [Available], [Arrived], [Comment], [NotificationReminder], [primaryBookingID]) VALUES (N'657101360 ', N'Slo19     ', N'384715112175204214480         ', N'118233419479102946333         ', CAST(N'2018-09-20T00:00:00.000' AS DateTime), N'N', N'N', N'', 1, N'657101360 ')
INSERT [dbo].[BOOKING] ([BookingID], [SlotNo], [CustomerID], [StylistID], [Date], [Available], [Arrived], [Comment], [NotificationReminder], [primaryBookingID]) VALUES (N'658535522 ', N'Slo11     ', N'100155997350724101850         ', N'105450503348172771121         ', CAST(N'2018-09-30T00:00:00.000' AS DateTime), N'N', N'N', N'', 0, N'658535522 ')
INSERT [dbo].[BOOKING] ([BookingID], [SlotNo], [CustomerID], [StylistID], [Date], [Available], [Arrived], [Comment], [NotificationReminder], [primaryBookingID]) VALUES (N'661167557 ', N'Slo11     ', N'181477264752348604465         ', N'118233419479102946333         ', CAST(N'2018-10-21T00:00:00.000' AS DateTime), N'N', N'N', N'', 0, N'180724634 ')
INSERT [dbo].[BOOKING] ([BookingID], [SlotNo], [CustomerID], [StylistID], [Date], [Available], [Arrived], [Comment], [NotificationReminder], [primaryBookingID]) VALUES (N'666666666 ', N'Slo15     ', N'100155997350724101850         ', N'118233419479102946333         ', CAST(N'2018-08-02T00:00:00.000' AS DateTime), N'N', N'Y', NULL, 1, N'666666666 ')
INSERT [dbo].[BOOKING] ([BookingID], [SlotNo], [CustomerID], [StylistID], [Date], [Available], [Arrived], [Comment], [NotificationReminder], [primaryBookingID]) VALUES (N'670383801 ', N'Slo11     ', N'407242734406745485560         ', N'270146203840160705048         ', CAST(N'2018-10-02T00:00:00.000' AS DateTime), N'N', N'Y', N'', 0, N'670383801 ')
INSERT [dbo].[BOOKING] ([BookingID], [SlotNo], [CustomerID], [StylistID], [Date], [Available], [Arrived], [Comment], [NotificationReminder], [primaryBookingID]) VALUES (N'677752606 ', N'Slo15     ', N'612648662678214805485         ', N'105450503348172771121         ', CAST(N'2018-08-31T00:00:00.000' AS DateTime), N'N', N'Y', N'Likes Aunt Jackie''s Shampoo', 1, N'677752606 ')
INSERT [dbo].[BOOKING] ([BookingID], [SlotNo], [CustomerID], [StylistID], [Date], [Available], [Arrived], [Comment], [NotificationReminder], [primaryBookingID]) VALUES (N'686541662 ', N'Slo4      ', N'612648662678214805485         ', N'580312424824681240311         ', CAST(N'2018-09-19T00:00:00.000' AS DateTime), N'N', N'N', N'', 0, N'216307740 ')
INSERT [dbo].[BOOKING] ([BookingID], [SlotNo], [CustomerID], [StylistID], [Date], [Available], [Arrived], [Comment], [NotificationReminder], [primaryBookingID]) VALUES (N'703343704 ', N'Slo18     ', N'154052322058082242258         ', N'118233419479102946333         ', CAST(N'2018-10-24T00:00:00.000' AS DateTime), N'N', N'N', N'', 0, N'058162045 ')
INSERT [dbo].[BOOKING] ([BookingID], [SlotNo], [CustomerID], [StylistID], [Date], [Available], [Arrived], [Comment], [NotificationReminder], [primaryBookingID]) VALUES (N'714132047 ', N'Slo15     ', N'181477264752348604465         ', N'105450503348172771121         ', CAST(N'2018-10-06T00:00:00.000' AS DateTime), N'N', N'Y', N'', 1, N'714132047 ')
INSERT [dbo].[BOOKING] ([BookingID], [SlotNo], [CustomerID], [StylistID], [Date], [Available], [Arrived], [Comment], [NotificationReminder], [primaryBookingID]) VALUES (N'716668836 ', N'Slo5      ', N'140623085382221348147         ', N'105450503348172771121         ', CAST(N'2018-10-24T00:00:00.000' AS DateTime), N'N', N'N', N'', 0, N'862487288 ')
INSERT [dbo].[BOOKING] ([BookingID], [SlotNo], [CustomerID], [StylistID], [Date], [Available], [Arrived], [Comment], [NotificationReminder], [primaryBookingID]) VALUES (N'718042600 ', N'Slo12     ', N'662881846588586465447         ', N'118233419479102946333         ', CAST(N'2018-11-06T00:00:00.000' AS DateTime), N'N', N'N', NULL, 0, N'718042600 ')
INSERT [dbo].[BOOKING] ([BookingID], [SlotNo], [CustomerID], [StylistID], [Date], [Available], [Arrived], [Comment], [NotificationReminder], [primaryBookingID]) VALUES (N'727068145 ', N'Slo16     ', N'635506662246576754303         ', N'580312424824681240311         ', CAST(N'2018-09-17T00:00:00.000' AS DateTime), N'N', N'N', N'', 1, N'403034175 ')
INSERT [dbo].[BOOKING] ([BookingID], [SlotNo], [CustomerID], [StylistID], [Date], [Available], [Arrived], [Comment], [NotificationReminder], [primaryBookingID]) VALUES (N'735886808 ', N'Slo9      ', N'181477264752348604465         ', N'118233419479102946333         ', CAST(N'2018-10-21T00:00:00.000' AS DateTime), N'N', N'N', N'', 0, N'180724634 ')
INSERT [dbo].[BOOKING] ([BookingID], [SlotNo], [CustomerID], [StylistID], [Date], [Available], [Arrived], [Comment], [NotificationReminder], [primaryBookingID]) VALUES (N'737407647 ', N'Slo5      ', N'384715112175204214480         ', N'105450503348172771121         ', CAST(N'2018-08-24T00:00:00.000' AS DateTime), N'N', N'Y', NULL, 1, N'737407647 ')
INSERT [dbo].[BOOKING] ([BookingID], [SlotNo], [CustomerID], [StylistID], [Date], [Available], [Arrived], [Comment], [NotificationReminder], [primaryBookingID]) VALUES (N'737581157 ', N'Slo11     ', N'215703134623237784686         ', N'580312424824681240311         ', CAST(N'2018-09-22T00:00:00.000' AS DateTime), N'N', N'Y', N'', 1, N'267247235 ')
INSERT [dbo].[BOOKING] ([BookingID], [SlotNo], [CustomerID], [StylistID], [Date], [Available], [Arrived], [Comment], [NotificationReminder], [primaryBookingID]) VALUES (N'743683788 ', N'Slo10     ', N'612648662678214805485         ', N'580312424824681240311         ', CAST(N'2018-09-19T00:00:00.000' AS DateTime), N'N', N'N', N'', 0, N'216307740 ')
INSERT [dbo].[BOOKING] ([BookingID], [SlotNo], [CustomerID], [StylistID], [Date], [Available], [Arrived], [Comment], [NotificationReminder], [primaryBookingID]) VALUES (N'745268283 ', N'Slo4      ', N'215703134623237784686         ', N'580312424824681240311         ', CAST(N'2018-09-22T00:00:00.000' AS DateTime), N'N', N'Y', N'', 1, N'267247235 ')
INSERT [dbo].[BOOKING] ([BookingID], [SlotNo], [CustomerID], [StylistID], [Date], [Available], [Arrived], [Comment], [NotificationReminder], [primaryBookingID]) VALUES (N'747078374 ', N'Slo6      ', N'105242998585655922697         ', N'118233419479102946333         ', CAST(N'2018-09-29T00:00:00.000' AS DateTime), N'N', N'N', N'', 0, N'747078374 ')
INSERT [dbo].[BOOKING] ([BookingID], [SlotNo], [CustomerID], [StylistID], [Date], [Available], [Arrived], [Comment], [NotificationReminder], [primaryBookingID]) VALUES (N'748175877 ', N'Slo11     ', N'324465860528272078866         ', N'105450503348172771121         ', CAST(N'2018-10-15T00:00:00.000' AS DateTime), N'N', N'Y', N'', 0, N'632574028 ')
INSERT [dbo].[BOOKING] ([BookingID], [SlotNo], [CustomerID], [StylistID], [Date], [Available], [Arrived], [Comment], [NotificationReminder], [primaryBookingID]) VALUES (N'755456550 ', N'Slo14     ', N'662881846588586465447         ', N'580312424824681240311         ', CAST(N'2018-08-21T00:00:00.000' AS DateTime), N'N', N'N', NULL, 1, N'755456550 ')
INSERT [dbo].[BOOKING] ([BookingID], [SlotNo], [CustomerID], [StylistID], [Date], [Available], [Arrived], [Comment], [NotificationReminder], [primaryBookingID]) VALUES (N'755654643 ', N'Slo10     ', N'535314300265460205603         ', N'580312424824681240311         ', CAST(N'2018-09-01T00:00:00.000' AS DateTime), N'N', N'Y', N'', 0, N'433783285 ')
INSERT [dbo].[BOOKING] ([BookingID], [SlotNo], [CustomerID], [StylistID], [Date], [Available], [Arrived], [Comment], [NotificationReminder], [primaryBookingID]) VALUES (N'760348252 ', N'Slo11     ', N'535314300265460205603         ', N'580312424824681240311         ', CAST(N'2018-10-03T00:00:00.000' AS DateTime), N'N', N'Y', N'', 1, N'760348252 ')
INSERT [dbo].[BOOKING] ([BookingID], [SlotNo], [CustomerID], [StylistID], [Date], [Available], [Arrived], [Comment], [NotificationReminder], [primaryBookingID]) VALUES (N'764145014 ', N'Slo11     ', N'384715112175204214480         ', N'270146203840160705048         ', CAST(N'2018-10-19T00:00:00.000' AS DateTime), N'N', N'N', N'', 0, N'764145014 ')
INSERT [dbo].[BOOKING] ([BookingID], [SlotNo], [CustomerID], [StylistID], [Date], [Available], [Arrived], [Comment], [NotificationReminder], [primaryBookingID]) VALUES (N'766120571 ', N'Slo5      ', N'535314300265460205603         ', N'105450503348172771121         ', CAST(N'2018-09-24T00:00:00.000' AS DateTime), N'N', N'N', N'', 1, N'550428622 ')
INSERT [dbo].[BOOKING] ([BookingID], [SlotNo], [CustomerID], [StylistID], [Date], [Available], [Arrived], [Comment], [NotificationReminder], [primaryBookingID]) VALUES (N'776107376 ', N'Slo14     ', N'777706452281511868742         ', N'580312424824681240311         ', CAST(N'2018-10-16T00:00:00.000' AS DateTime), N'N', N'Y', N'', 0, N'776107376 ')
INSERT [dbo].[BOOKING] ([BookingID], [SlotNo], [CustomerID], [StylistID], [Date], [Available], [Arrived], [Comment], [NotificationReminder], [primaryBookingID]) VALUES (N'777777777 ', N'Slo20     ', N'105242998585655922697         ', N'580312424824681240311         ', CAST(N'2018-08-03T00:00:00.000' AS DateTime), N'N', N'N', NULL, 1, N'777777777 ')
INSERT [dbo].[BOOKING] ([BookingID], [SlotNo], [CustomerID], [StylistID], [Date], [Available], [Arrived], [Comment], [NotificationReminder], [primaryBookingID]) VALUES (N'781237027 ', N'Slo17     ', N'612648662678214805485         ', N'580312424824681240311         ', CAST(N'2018-09-21T00:00:00.000' AS DateTime), N'N', N'Y', N'', 0, N'568466551 ')
INSERT [dbo].[BOOKING] ([BookingID], [SlotNo], [CustomerID], [StylistID], [Date], [Available], [Arrived], [Comment], [NotificationReminder], [primaryBookingID]) VALUES (N'786223434 ', N'Slo11     ', N'612648662678214805485         ', N'118233419479102946333         ', CAST(N'2018-10-09T00:00:00.000' AS DateTime), N'N', N'Y', N'', 0, N'786223434 ')
INSERT [dbo].[BOOKING] ([BookingID], [SlotNo], [CustomerID], [StylistID], [Date], [Available], [Arrived], [Comment], [NotificationReminder], [primaryBookingID]) VALUES (N'803310300 ', N'Slo10     ', N'215703134623237784686         ', N'580312424824681240311         ', CAST(N'2018-09-22T00:00:00.000' AS DateTime), N'N', N'Y', N'', 1, N'267247235 ')
INSERT [dbo].[BOOKING] ([BookingID], [SlotNo], [CustomerID], [StylistID], [Date], [Available], [Arrived], [Comment], [NotificationReminder], [primaryBookingID]) VALUES (N'808460332 ', N'Slo11     ', N'100155997350724101850         ', N'118233419479102946333         ', CAST(N'2018-10-18T00:00:00.000' AS DateTime), N'N', N'N', N'', 0, N'808460332 ')
INSERT [dbo].[BOOKING] ([BookingID], [SlotNo], [CustomerID], [StylistID], [Date], [Available], [Arrived], [Comment], [NotificationReminder], [primaryBookingID]) VALUES (N'810265160 ', N'Slo1      ', N'215703134623237784686         ', N'270146203840160705048         ', CAST(N'2018-09-10T00:00:00.000' AS DateTime), N'N', N'N', NULL, 0, N'810265160 ')
INSERT [dbo].[BOOKING] ([BookingID], [SlotNo], [CustomerID], [StylistID], [Date], [Available], [Arrived], [Comment], [NotificationReminder], [primaryBookingID]) VALUES (N'832687475 ', N'Slo7      ', N'535314300265460205603         ', N'580312424824681240311         ', CAST(N'2018-10-20T00:00:00.000' AS DateTime), N'N', N'N', N'', 0, N'030364610 ')
INSERT [dbo].[BOOKING] ([BookingID], [SlotNo], [CustomerID], [StylistID], [Date], [Available], [Arrived], [Comment], [NotificationReminder], [primaryBookingID]) VALUES (N'838654358 ', N'Slo11     ', N'215703134623237784686         ', N'270146203840160705048         ', CAST(N'2018-09-18T00:00:00.000' AS DateTime), N'N', N'Y', N'', 0, N'564022384 ')
INSERT [dbo].[BOOKING] ([BookingID], [SlotNo], [CustomerID], [StylistID], [Date], [Available], [Arrived], [Comment], [NotificationReminder], [primaryBookingID]) VALUES (N'842460024 ', N'Slo3      ', N'612648662678214805485         ', N'580312424824681240311         ', CAST(N'2018-09-19T00:00:00.000' AS DateTime), N'N', N'N', N'', 0, N'216307740 ')
GO
INSERT [dbo].[BOOKING] ([BookingID], [SlotNo], [CustomerID], [StylistID], [Date], [Available], [Arrived], [Comment], [NotificationReminder], [primaryBookingID]) VALUES (N'844052626 ', N'Slo11     ', N'535314300265460205603         ', N'580312424824681240311         ', CAST(N'2018-09-19T00:00:00.000' AS DateTime), N'N', N'Y', N'', 0, N'844052626 ')
INSERT [dbo].[BOOKING] ([BookingID], [SlotNo], [CustomerID], [StylistID], [Date], [Available], [Arrived], [Comment], [NotificationReminder], [primaryBookingID]) VALUES (N'846274582 ', N'Slo18     ', N'535314300265460205603         ', N'580312424824681240311         ', CAST(N'2018-09-19T00:00:00.000' AS DateTime), N'N', N'Y', N'', 0, N'844052626 ')
INSERT [dbo].[BOOKING] ([BookingID], [SlotNo], [CustomerID], [StylistID], [Date], [Available], [Arrived], [Comment], [NotificationReminder], [primaryBookingID]) VALUES (N'860870858 ', N'Slo20     ', N'662881846588586465447         ', N'105450503348172771121         ', CAST(N'2018-09-03T00:00:00.000' AS DateTime), N'N', N'Y', N'Likes Aunt Jackie''s Shampoo', 0, N'860870858 ')
INSERT [dbo].[BOOKING] ([BookingID], [SlotNo], [CustomerID], [StylistID], [Date], [Available], [Arrived], [Comment], [NotificationReminder], [primaryBookingID]) VALUES (N'862487288 ', N'Slo4      ', N'140623085382221348147         ', N'105450503348172771121         ', CAST(N'2018-10-24T00:00:00.000' AS DateTime), N'N', N'N', N'', 0, N'862487288 ')
INSERT [dbo].[BOOKING] ([BookingID], [SlotNo], [CustomerID], [StylistID], [Date], [Available], [Arrived], [Comment], [NotificationReminder], [primaryBookingID]) VALUES (N'865236471 ', N'Slo13     ', N'100155997350724101850         ', N'105450503348172771121         ', CAST(N'2018-09-30T00:00:00.000' AS DateTime), N'N', N'N', N'', 0, N'658535522 ')
INSERT [dbo].[BOOKING] ([BookingID], [SlotNo], [CustomerID], [StylistID], [Date], [Available], [Arrived], [Comment], [NotificationReminder], [primaryBookingID]) VALUES (N'866225622 ', N'Slo10     ', N'324465860528272078866         ', N'118233419479102946333         ', CAST(N'2018-10-23T00:00:00.000' AS DateTime), N'N', N'N', N'', 0, N'866225622 ')
INSERT [dbo].[BOOKING] ([BookingID], [SlotNo], [CustomerID], [StylistID], [Date], [Available], [Arrived], [Comment], [NotificationReminder], [primaryBookingID]) VALUES (N'868830621 ', N'Slo20     ', N'612648662678214805485         ', N'118233419479102946333         ', CAST(N'2018-09-03T00:00:00.000' AS DateTime), N'N', N'Y', N'', 0, N'868830621 ')
INSERT [dbo].[BOOKING] ([BookingID], [SlotNo], [CustomerID], [StylistID], [Date], [Available], [Arrived], [Comment], [NotificationReminder], [primaryBookingID]) VALUES (N'873657564 ', N'Slo1      ', N'181477264752348604465         ', N'118233419479102946333         ', CAST(N'2018-09-04T00:00:00.000' AS DateTime), N'N', N'Y', NULL, 1, N'873657564 ')
INSERT [dbo].[BOOKING] ([BookingID], [SlotNo], [CustomerID], [StylistID], [Date], [Available], [Arrived], [Comment], [NotificationReminder], [primaryBookingID]) VALUES (N'875445641 ', N'Slo6      ', N'181477264752348604465         ', N'118233419479102946333         ', CAST(N'2018-10-21T00:00:00.000' AS DateTime), N'N', N'N', N'', 0, N'180724634 ')
INSERT [dbo].[BOOKING] ([BookingID], [SlotNo], [CustomerID], [StylistID], [Date], [Available], [Arrived], [Comment], [NotificationReminder], [primaryBookingID]) VALUES (N'876527887 ', N'Slo14     ', N'635506662246576754303         ', N'580312424824681240311         ', CAST(N'2018-09-17T00:00:00.000' AS DateTime), N'N', N'N', N'', 1, N'403034175 ')
INSERT [dbo].[BOOKING] ([BookingID], [SlotNo], [CustomerID], [StylistID], [Date], [Available], [Arrived], [Comment], [NotificationReminder], [primaryBookingID]) VALUES (N'877884443 ', N'Slo5      ', N'105242998585655922697         ', N'270146203840160705048         ', CAST(N'2018-09-18T00:00:00.000' AS DateTime), N'N', N'Y', N'', 1, N'877884443 ')
INSERT [dbo].[BOOKING] ([BookingID], [SlotNo], [CustomerID], [StylistID], [Date], [Available], [Arrived], [Comment], [NotificationReminder], [primaryBookingID]) VALUES (N'885570402 ', N'Slo10     ', N'777706452281511868742         ', N'118233419479102946333         ', CAST(N'2018-10-08T00:00:00.000' AS DateTime), N'N', N'Y', N'', 0, N'885570402 ')
INSERT [dbo].[BOOKING] ([BookingID], [SlotNo], [CustomerID], [StylistID], [Date], [Available], [Arrived], [Comment], [NotificationReminder], [primaryBookingID]) VALUES (N'886084750 ', N'Slo12     ', N'407242734406745485560         ', N'270146203840160705048         ', CAST(N'2018-10-02T00:00:00.000' AS DateTime), N'N', N'Y', N'', 0, N'670383801 ')
INSERT [dbo].[BOOKING] ([BookingID], [SlotNo], [CustomerID], [StylistID], [Date], [Available], [Arrived], [Comment], [NotificationReminder], [primaryBookingID]) VALUES (N'888888888 ', N'Slo11     ', N'181477264752348604465         ', N'270146203840160705048         ', CAST(N'2018-08-04T00:00:00.000' AS DateTime), N'N', N'N', NULL, 1, N'888888888 ')
INSERT [dbo].[BOOKING] ([BookingID], [SlotNo], [CustomerID], [StylistID], [Date], [Available], [Arrived], [Comment], [NotificationReminder], [primaryBookingID]) VALUES (N'999999999 ', N'Slo6      ', N'686374338720721300668         ', N'118233419479102946333         ', CAST(N'2018-04-04T00:00:00.000' AS DateTime), N'N', N'N', NULL, 0, N'999999999 ')
INSERT [dbo].[BookingService] ([BookingID], [ServiceID]) VALUES (N'755456550 ', N'Pr1147    ')
INSERT [dbo].[BookingService] ([BookingID], [ServiceID]) VALUES (N'111111111 ', N'Pr1120    ')
INSERT [dbo].[BookingService] ([BookingID], [ServiceID]) VALUES (N'122222222 ', N'Pr1129    ')
INSERT [dbo].[BookingService] ([BookingID], [ServiceID]) VALUES (N'165151551 ', N'Pr1125    ')
INSERT [dbo].[BookingService] ([BookingID], [ServiceID]) VALUES (N'176024758 ', N'Pr1101    ')
INSERT [dbo].[BookingService] ([BookingID], [ServiceID]) VALUES (N'222222222 ', N'Pr1129    ')
INSERT [dbo].[BookingService] ([BookingID], [ServiceID]) VALUES (N'228313337 ', N'Pr1160    ')
INSERT [dbo].[BookingService] ([BookingID], [ServiceID]) VALUES (N'333333333 ', N'Pr1122    ')
INSERT [dbo].[BookingService] ([BookingID], [ServiceID]) VALUES (N'365636365 ', N'Pr1118    ')
INSERT [dbo].[BookingService] ([BookingID], [ServiceID]) VALUES (N'444444444 ', N'Pr1129    ')
INSERT [dbo].[BookingService] ([BookingID], [ServiceID]) VALUES (N'455161654 ', N'Pr1147    ')
INSERT [dbo].[BookingService] ([BookingID], [ServiceID]) VALUES (N'525763663 ', N'Pr1128    ')
INSERT [dbo].[BookingService] ([BookingID], [ServiceID]) VALUES (N'543446466 ', N'Pr1105    ')
INSERT [dbo].[BookingService] ([BookingID], [ServiceID]) VALUES (N'545646646 ', N'Pr1116    ')
INSERT [dbo].[BookingService] ([BookingID], [ServiceID]) VALUES (N'555555555 ', N'Pr1102    ')
INSERT [dbo].[BookingService] ([BookingID], [ServiceID]) VALUES (N'564964895 ', N'Pr1123    ')
INSERT [dbo].[BookingService] ([BookingID], [ServiceID]) VALUES (N'645546546 ', N'Pr1160    ')
INSERT [dbo].[BookingService] ([BookingID], [ServiceID]) VALUES (N'645646343 ', N'Pr1103    ')
INSERT [dbo].[BookingService] ([BookingID], [ServiceID]) VALUES (N'651651655 ', N'Pr1112    ')
INSERT [dbo].[BookingService] ([BookingID], [ServiceID]) VALUES (N'654456645 ', N'Pr1129    ')
INSERT [dbo].[BookingService] ([BookingID], [ServiceID]) VALUES (N'666666666 ', N'Pr1129    ')
INSERT [dbo].[BookingService] ([BookingID], [ServiceID]) VALUES (N'755456550 ', N'Pr1160    ')
INSERT [dbo].[BookingService] ([BookingID], [ServiceID]) VALUES (N'888888888 ', N'Pr1119    ')
INSERT [dbo].[BookingService] ([BookingID], [ServiceID]) VALUES (N'999999999 ', N'Pr1129    ')
INSERT [dbo].[BookingService] ([BookingID], [ServiceID]) VALUES (N'777777777 ', N'Pr1123    ')
INSERT [dbo].[BookingService] ([BookingID], [ServiceID]) VALUES (N'365450324 ', N'Pr1129    ')
INSERT [dbo].[BookingService] ([BookingID], [ServiceID]) VALUES (N'365450324 ', N'Pr1147    ')
INSERT [dbo].[BookingService] ([BookingID], [ServiceID]) VALUES (N'365450324 ', N'Pr1160    ')
INSERT [dbo].[BookingService] ([BookingID], [ServiceID]) VALUES (N'433783285 ', N'Pr1129    ')
INSERT [dbo].[BookingService] ([BookingID], [ServiceID]) VALUES (N'516516516 ', N'Pr1129    ')
INSERT [dbo].[BookingService] ([BookingID], [ServiceID]) VALUES (N'516516516 ', N'Pr1160    ')
INSERT [dbo].[BookingService] ([BookingID], [ServiceID]) VALUES (N'516516516 ', N'Pr1147    ')
INSERT [dbo].[BookingService] ([BookingID], [ServiceID]) VALUES (N'516516516 ', N'Pr1148    ')
INSERT [dbo].[BookingService] ([BookingID], [ServiceID]) VALUES (N'365636365 ', N'Pr1160    ')
INSERT [dbo].[BookingService] ([BookingID], [ServiceID]) VALUES (N'561561561 ', N'Pr1129    ')
INSERT [dbo].[BookingService] ([BookingID], [ServiceID]) VALUES (N'561561561 ', N'Pr1147    ')
INSERT [dbo].[BookingService] ([BookingID], [ServiceID]) VALUES (N'561561561 ', N'Pr1160    ')
INSERT [dbo].[BookingService] ([BookingID], [ServiceID]) VALUES (N'378464537 ', N'Pr1107    ')
INSERT [dbo].[BookingService] ([BookingID], [ServiceID]) VALUES (N'645646565 ', N'Pr1121    ')
INSERT [dbo].[BookingService] ([BookingID], [ServiceID]) VALUES (N'334545735 ', N'Pr1114    ')
INSERT [dbo].[BookingService] ([BookingID], [ServiceID]) VALUES (N'052367723 ', N'Pr1129    ')
INSERT [dbo].[BookingService] ([BookingID], [ServiceID]) VALUES (N'052367723 ', N'Pr1160    ')
INSERT [dbo].[BookingService] ([BookingID], [ServiceID]) VALUES (N'156415656 ', N'Pr1129    ')
INSERT [dbo].[BookingService] ([BookingID], [ServiceID]) VALUES (N'011111111 ', N'Pr1160    ')
INSERT [dbo].[BookingService] ([BookingID], [ServiceID]) VALUES (N'041332566 ', N'Pr1160    ')
INSERT [dbo].[BookingService] ([BookingID], [ServiceID]) VALUES (N'868830621 ', N'Pr1129    ')
INSERT [dbo].[BookingService] ([BookingID], [ServiceID]) VALUES (N'677752606 ', N'Pr1129    ')
INSERT [dbo].[BookingService] ([BookingID], [ServiceID]) VALUES (N'483636551 ', N'Pr1160    ')
INSERT [dbo].[BookingService] ([BookingID], [ServiceID]) VALUES (N'173457147 ', N'Pr1160    ')
INSERT [dbo].[BookingService] ([BookingID], [ServiceID]) VALUES (N'547225656 ', N'Pr1128    ')
INSERT [dbo].[BookingService] ([BookingID], [ServiceID]) VALUES (N'114357316 ', N'Pr1129    ')
INSERT [dbo].[BookingService] ([BookingID], [ServiceID]) VALUES (N'114357316 ', N'Pr1148    ')
INSERT [dbo].[BookingService] ([BookingID], [ServiceID]) VALUES (N'114357316 ', N'Pr1160    ')
INSERT [dbo].[BookingService] ([BookingID], [ServiceID]) VALUES (N'128767857 ', N'Pr1147    ')
INSERT [dbo].[BookingService] ([BookingID], [ServiceID]) VALUES (N'151711823 ', N'Pr1160    ')
INSERT [dbo].[BookingService] ([BookingID], [ServiceID]) VALUES (N'120175814 ', N'Pr1160    ')
INSERT [dbo].[BookingService] ([BookingID], [ServiceID]) VALUES (N'120175814 ', N'Pr1160    ')
INSERT [dbo].[BookingService] ([BookingID], [ServiceID]) VALUES (N'222435233 ', N'Pr1129    ')
INSERT [dbo].[BookingService] ([BookingID], [ServiceID]) VALUES (N'222435233 ', N'Pr1160    ')
INSERT [dbo].[BookingService] ([BookingID], [ServiceID]) VALUES (N'358180023 ', N'Pr1129    ')
INSERT [dbo].[BookingService] ([BookingID], [ServiceID]) VALUES (N'873657564 ', N'Pr1129    ')
INSERT [dbo].[BookingService] ([BookingID], [ServiceID]) VALUES (N'767163130 ', N'Pr1129    ')
INSERT [dbo].[BookingService] ([BookingID], [ServiceID]) VALUES (N'767163130 ', N'Pr1160    ')
INSERT [dbo].[BookingService] ([BookingID], [ServiceID]) VALUES (N'873657564 ', N'Pr1160    ')
INSERT [dbo].[BookingService] ([BookingID], [ServiceID]) VALUES (N'575210227 ', N'Pr1129    ')
INSERT [dbo].[BookingService] ([BookingID], [ServiceID]) VALUES (N'575210227 ', N'Pr1160    ')
INSERT [dbo].[BookingService] ([BookingID], [ServiceID]) VALUES (N'201782088 ', N'Pr1129    ')
INSERT [dbo].[BookingService] ([BookingID], [ServiceID]) VALUES (N'622473016 ', N'Pr1129    ')
INSERT [dbo].[BookingService] ([BookingID], [ServiceID]) VALUES (N'622473016 ', N'Pr1160    ')
INSERT [dbo].[BookingService] ([BookingID], [ServiceID]) VALUES (N'300017033 ', N'Pr1103    ')
INSERT [dbo].[BookingService] ([BookingID], [ServiceID]) VALUES (N'552486404 ', N'Pr1129    ')
INSERT [dbo].[BookingService] ([BookingID], [ServiceID]) VALUES (N'363731045 ', N'Pr1148    ')
INSERT [dbo].[BookingService] ([BookingID], [ServiceID]) VALUES (N'471565888 ', N'Pr1160    ')
INSERT [dbo].[BookingService] ([BookingID], [ServiceID]) VALUES (N'616466824 ', N'Pr1160    ')
INSERT [dbo].[BookingService] ([BookingID], [ServiceID]) VALUES (N'001621148 ', N'Pr1112    ')
INSERT [dbo].[BookingService] ([BookingID], [ServiceID]) VALUES (N'552486404 ', N'Pr1160    ')
INSERT [dbo].[BookingService] ([BookingID], [ServiceID]) VALUES (N'860870858 ', N'Pr1155    ')
INSERT [dbo].[BookingService] ([BookingID], [ServiceID]) VALUES (N'267247235 ', N'Pr1128    ')
INSERT [dbo].[BookingService] ([BookingID], [ServiceID]) VALUES (N'403034175 ', N'Pr1101    ')
INSERT [dbo].[BookingService] ([BookingID], [ServiceID]) VALUES (N'718042600 ', N'Pr1129    ')
INSERT [dbo].[BookingService] ([BookingID], [ServiceID]) VALUES (N'718042600 ', N'Pr1147    ')
INSERT [dbo].[BookingService] ([BookingID], [ServiceID]) VALUES (N'718042600 ', N'Pr1160    ')
INSERT [dbo].[BookingService] ([BookingID], [ServiceID]) VALUES (N'060026124 ', N'Pr1160    ')
INSERT [dbo].[BookingService] ([BookingID], [ServiceID]) VALUES (N'070741623 ', N'Pr1129    ')
INSERT [dbo].[BookingService] ([BookingID], [ServiceID]) VALUES (N'070741623 ', N'Pr1147    ')
INSERT [dbo].[BookingService] ([BookingID], [ServiceID]) VALUES (N'070741623 ', N'Pr1148    ')
INSERT [dbo].[BookingService] ([BookingID], [ServiceID]) VALUES (N'070741623 ', N'Pr1160    ')
INSERT [dbo].[BookingService] ([BookingID], [ServiceID]) VALUES (N'737407647 ', N'Pr1129    ')
INSERT [dbo].[BookingService] ([BookingID], [ServiceID]) VALUES (N'737407647 ', N'Pr1160    ')
INSERT [dbo].[BookingService] ([BookingID], [ServiceID]) VALUES (N'418380632 ', N'Pr1129    ')
INSERT [dbo].[BookingService] ([BookingID], [ServiceID]) VALUES (N'237865384 ', N'Pr1160    ')
INSERT [dbo].[BookingService] ([BookingID], [ServiceID]) VALUES (N'658353401 ', N'Pr1129    ')
INSERT [dbo].[BookingService] ([BookingID], [ServiceID]) VALUES (N'658353401 ', N'Pr1147    ')
INSERT [dbo].[BookingService] ([BookingID], [ServiceID]) VALUES (N'658353401 ', N'Pr1160    ')
INSERT [dbo].[BookingService] ([BookingID], [ServiceID]) VALUES (N'403534140 ', N'Pr1129    ')
INSERT [dbo].[BookingService] ([BookingID], [ServiceID]) VALUES (N'403534140 ', N'Pr1147    ')
INSERT [dbo].[BookingService] ([BookingID], [ServiceID]) VALUES (N'403534140 ', N'Pr1160    ')
INSERT [dbo].[BookingService] ([BookingID], [ServiceID]) VALUES (N'547225656 ', N'Pr1160    ')
INSERT [dbo].[BookingService] ([BookingID], [ServiceID]) VALUES (N'810265160 ', N'Pr1155    ')
INSERT [dbo].[BookingService] ([BookingID], [ServiceID]) VALUES (N'483636551 ', N'Pr1129    ')
GO
INSERT [dbo].[BookingService] ([BookingID], [ServiceID]) VALUES (N'403034175 ', N'Pr1148    ')
INSERT [dbo].[BookingService] ([BookingID], [ServiceID]) VALUES (N'877884443 ', N'Pr1160    ')
INSERT [dbo].[BookingService] ([BookingID], [ServiceID]) VALUES (N'216307740 ', N'Pr1118    ')
INSERT [dbo].[BookingService] ([BookingID], [ServiceID]) VALUES (N'844052626 ', N'Pr1118    ')
INSERT [dbo].[BookingService] ([BookingID], [ServiceID]) VALUES (N'657101360 ', N'Pr1129    ')
INSERT [dbo].[BookingService] ([BookingID], [ServiceID]) VALUES (N'071046156 ', N'Pr1129    ')
INSERT [dbo].[BookingService] ([BookingID], [ServiceID]) VALUES (N'071046156 ', N'Pr1160    ')
INSERT [dbo].[BookingService] ([BookingID], [ServiceID]) VALUES (N'260225447 ', N'Pr1129    ')
INSERT [dbo].[BookingService] ([BookingID], [ServiceID]) VALUES (N'413046600 ', N'Pr1155    ')
INSERT [dbo].[BookingService] ([BookingID], [ServiceID]) VALUES (N'747078374 ', N'Pr1129    ')
INSERT [dbo].[BookingService] ([BookingID], [ServiceID]) VALUES (N'747078374 ', N'Pr1160    ')
INSERT [dbo].[BookingService] ([BookingID], [ServiceID]) VALUES (N'658535522 ', N'Pr1147    ')
INSERT [dbo].[BookingService] ([BookingID], [ServiceID]) VALUES (N'653420332 ', N'Pr1129    ')
INSERT [dbo].[BookingService] ([BookingID], [ServiceID]) VALUES (N'046007658 ', N'Pr1160    ')
INSERT [dbo].[BookingService] ([BookingID], [ServiceID]) VALUES (N'634365345 ', N'Pr1147    ')
INSERT [dbo].[BookingService] ([BookingID], [ServiceID]) VALUES (N'063323570 ', N'Pr1129    ')
INSERT [dbo].[BookingService] ([BookingID], [ServiceID]) VALUES (N'405854081 ', N'Pr1129    ')
INSERT [dbo].[BookingService] ([BookingID], [ServiceID]) VALUES (N'647260713 ', N'Pr1103    ')
INSERT [dbo].[BookingService] ([BookingID], [ServiceID]) VALUES (N'127874202 ', N'Pr1129    ')
INSERT [dbo].[BookingService] ([BookingID], [ServiceID]) VALUES (N'521727418 ', N'Pr1160    ')
INSERT [dbo].[BookingService] ([BookingID], [ServiceID]) VALUES (N'127874202 ', N'Pr1160    ')
INSERT [dbo].[BookingService] ([BookingID], [ServiceID]) VALUES (N'433783285 ', N'Pr1147    ')
INSERT [dbo].[BookingService] ([BookingID], [ServiceID]) VALUES (N'418380632 ', N'Pr1147    ')
INSERT [dbo].[BookingService] ([BookingID], [ServiceID]) VALUES (N'653420332 ', N'Pr1160    ')
INSERT [dbo].[BookingService] ([BookingID], [ServiceID]) VALUES (N'481287255 ', N'Pr1105    ')
INSERT [dbo].[BookingService] ([BookingID], [ServiceID]) VALUES (N'433783285 ', N'Pr1160    ')
INSERT [dbo].[BookingService] ([BookingID], [ServiceID]) VALUES (N'574784585 ', N'Pr1129    ')
INSERT [dbo].[BookingService] ([BookingID], [ServiceID]) VALUES (N'574784585 ', N'Pr1147    ')
INSERT [dbo].[BookingService] ([BookingID], [ServiceID]) VALUES (N'574784585 ', N'Pr1160    ')
INSERT [dbo].[BookingService] ([BookingID], [ServiceID]) VALUES (N'564022384 ', N'Pr1129    ')
INSERT [dbo].[BookingService] ([BookingID], [ServiceID]) VALUES (N'564022384 ', N'Pr1147    ')
INSERT [dbo].[BookingService] ([BookingID], [ServiceID]) VALUES (N'564022384 ', N'Pr1148    ')
INSERT [dbo].[BookingService] ([BookingID], [ServiceID]) VALUES (N'568466551 ', N'Pr1129    ')
INSERT [dbo].[BookingService] ([BookingID], [ServiceID]) VALUES (N'568466551 ', N'Pr1148    ')
INSERT [dbo].[BookingService] ([BookingID], [ServiceID]) VALUES (N'568466551 ', N'Pr1160    ')
INSERT [dbo].[BookingService] ([BookingID], [ServiceID]) VALUES (N'145818714 ', N'Pr1160    ')
INSERT [dbo].[BookingService] ([BookingID], [ServiceID]) VALUES (N'550428622 ', N'Pr1155    ')
INSERT [dbo].[BookingService] ([BookingID], [ServiceID]) VALUES (N'550428622 ', N'Pr1160    ')
INSERT [dbo].[BookingService] ([BookingID], [ServiceID]) VALUES (N'155466825 ', N'Pr1129    ')
INSERT [dbo].[BookingService] ([BookingID], [ServiceID]) VALUES (N'155466825 ', N'Pr1160    ')
INSERT [dbo].[BookingService] ([BookingID], [ServiceID]) VALUES (N'214181455 ', N'Pr1160    ')
INSERT [dbo].[BookingService] ([BookingID], [ServiceID]) VALUES (N'670383801 ', N'Pr1129    ')
INSERT [dbo].[BookingService] ([BookingID], [ServiceID]) VALUES (N'670383801 ', N'Pr1147    ')
INSERT [dbo].[BookingService] ([BookingID], [ServiceID]) VALUES (N'670383801 ', N'Pr1160    ')
INSERT [dbo].[BookingService] ([BookingID], [ServiceID]) VALUES (N'885570402 ', N'Pr1129    ')
INSERT [dbo].[BookingService] ([BookingID], [ServiceID]) VALUES (N'885570402 ', N'Pr1160    ')
INSERT [dbo].[BookingService] ([BookingID], [ServiceID]) VALUES (N'885570402 ', N'          ')
INSERT [dbo].[BookingService] ([BookingID], [ServiceID]) VALUES (N'351153017 ', N'Pr1148    ')
INSERT [dbo].[BookingService] ([BookingID], [ServiceID]) VALUES (N'351153017 ', N'          ')
INSERT [dbo].[BookingService] ([BookingID], [ServiceID]) VALUES (N'217264372 ', N'Pr1129    ')
INSERT [dbo].[BookingService] ([BookingID], [ServiceID]) VALUES (N'217264372 ', N'Pr1160    ')
INSERT [dbo].[BookingService] ([BookingID], [ServiceID]) VALUES (N'217264372 ', N'          ')
INSERT [dbo].[BookingService] ([BookingID], [ServiceID]) VALUES (N'760348252 ', N'Pr1155    ')
INSERT [dbo].[BookingService] ([BookingID], [ServiceID]) VALUES (N'760348252 ', N'          ')
INSERT [dbo].[BookingService] ([BookingID], [ServiceID]) VALUES (N'467420776 ', N'Pr1109    ')
INSERT [dbo].[BookingService] ([BookingID], [ServiceID]) VALUES (N'467420776 ', N'          ')
INSERT [dbo].[BookingService] ([BookingID], [ServiceID]) VALUES (N'511652873 ', N'Pr1129    ')
INSERT [dbo].[BookingService] ([BookingID], [ServiceID]) VALUES (N'511652873 ', N'Pr1147    ')
INSERT [dbo].[BookingService] ([BookingID], [ServiceID]) VALUES (N'511652873 ', N'Pr1160    ')
INSERT [dbo].[BookingService] ([BookingID], [ServiceID]) VALUES (N'511652873 ', N'          ')
INSERT [dbo].[BookingService] ([BookingID], [ServiceID]) VALUES (N'714132047 ', N'Pr1148    ')
INSERT [dbo].[BookingService] ([BookingID], [ServiceID]) VALUES (N'714132047 ', N'Pr1160    ')
INSERT [dbo].[BookingService] ([BookingID], [ServiceID]) VALUES (N'714132047 ', N'          ')
INSERT [dbo].[BookingService] ([BookingID], [ServiceID]) VALUES (N'276220240 ', N'Pr1129    ')
INSERT [dbo].[BookingService] ([BookingID], [ServiceID]) VALUES (N'276220240 ', N'          ')
INSERT [dbo].[BookingService] ([BookingID], [ServiceID]) VALUES (N'004400232 ', N'Pr1160    ')
INSERT [dbo].[BookingService] ([BookingID], [ServiceID]) VALUES (N'004400232 ', N'          ')
INSERT [dbo].[BookingService] ([BookingID], [ServiceID]) VALUES (N'173566683 ', N'Pr1160    ')
INSERT [dbo].[BookingService] ([BookingID], [ServiceID]) VALUES (N'173566683 ', N'          ')
INSERT [dbo].[BookingService] ([BookingID], [ServiceID]) VALUES (N'561520075 ', N'Pr1160    ')
INSERT [dbo].[BookingService] ([BookingID], [ServiceID]) VALUES (N'561520075 ', N'          ')
INSERT [dbo].[BookingService] ([BookingID], [ServiceID]) VALUES (N'286404016 ', N'Pr1129    ')
INSERT [dbo].[BookingService] ([BookingID], [ServiceID]) VALUES (N'286404016 ', N'Pr1160    ')
INSERT [dbo].[BookingService] ([BookingID], [ServiceID]) VALUES (N'286404016 ', N'          ')
INSERT [dbo].[BookingService] ([BookingID], [ServiceID]) VALUES (N'285352471 ', N'Pr1160    ')
INSERT [dbo].[BookingService] ([BookingID], [ServiceID]) VALUES (N'285352471 ', N'          ')
INSERT [dbo].[BookingService] ([BookingID], [ServiceID]) VALUES (N'565073505 ', N'Pr1101    ')
INSERT [dbo].[BookingService] ([BookingID], [ServiceID]) VALUES (N'565073505 ', N'          ')
INSERT [dbo].[BookingService] ([BookingID], [ServiceID]) VALUES (N'315667782 ', N'Pr1147    ')
INSERT [dbo].[BookingService] ([BookingID], [ServiceID]) VALUES (N'315667782 ', N'Pr1160    ')
INSERT [dbo].[BookingService] ([BookingID], [ServiceID]) VALUES (N'315667782 ', N'          ')
INSERT [dbo].[BookingService] ([BookingID], [ServiceID]) VALUES (N'776107376 ', N'Pr1160    ')
INSERT [dbo].[BookingService] ([BookingID], [ServiceID]) VALUES (N'776107376 ', N'          ')
INSERT [dbo].[BookingService] ([BookingID], [ServiceID]) VALUES (N'525623472 ', N'Pr1129    ')
INSERT [dbo].[BookingService] ([BookingID], [ServiceID]) VALUES (N'525623472 ', N'          ')
INSERT [dbo].[BookingService] ([BookingID], [ServiceID]) VALUES (N'638250468 ', N'Pr1155    ')
INSERT [dbo].[BookingService] ([BookingID], [ServiceID]) VALUES (N'638250468 ', N'          ')
INSERT [dbo].[BookingService] ([BookingID], [ServiceID]) VALUES (N'808460332 ', N'Pr1129    ')
INSERT [dbo].[BookingService] ([BookingID], [ServiceID]) VALUES (N'808460332 ', N'          ')
INSERT [dbo].[BookingService] ([BookingID], [ServiceID]) VALUES (N'786223434 ', N'Pr1129    ')
INSERT [dbo].[BookingService] ([BookingID], [ServiceID]) VALUES (N'786223434 ', N'          ')
INSERT [dbo].[BookingService] ([BookingID], [ServiceID]) VALUES (N'764145014 ', N'Pr1160    ')
INSERT [dbo].[BookingService] ([BookingID], [ServiceID]) VALUES (N'282323476 ', N'Pr1129    ')
INSERT [dbo].[BookingService] ([BookingID], [ServiceID]) VALUES (N'282323476 ', N'Pr1160    ')
INSERT [dbo].[BookingService] ([BookingID], [ServiceID]) VALUES (N'442182057 ', N'Pr1160    ')
INSERT [dbo].[BookingService] ([BookingID], [ServiceID]) VALUES (N'030364610 ', N'Pr1101    ')
INSERT [dbo].[BookingService] ([BookingID], [ServiceID]) VALUES (N'030364610 ', N'Pr1147    ')
INSERT [dbo].[BookingService] ([BookingID], [ServiceID]) VALUES (N'371672637 ', N'Pr1160    ')
INSERT [dbo].[BookingService] ([BookingID], [ServiceID]) VALUES (N'180724634 ', N'Pr1129    ')
INSERT [dbo].[BookingService] ([BookingID], [ServiceID]) VALUES (N'180724634 ', N'Pr1147    ')
GO
INSERT [dbo].[BookingService] ([BookingID], [ServiceID]) VALUES (N'053817368 ', N'Pr1129    ')
INSERT [dbo].[BookingService] ([BookingID], [ServiceID]) VALUES (N'866225622 ', N'Pr1129    ')
INSERT [dbo].[BookingService] ([BookingID], [ServiceID]) VALUES (N'866225622 ', N'Pr1160    ')
INSERT [dbo].[BookingService] ([BookingID], [ServiceID]) VALUES (N'058162045 ', N'Pr1129    ')
INSERT [dbo].[BookingService] ([BookingID], [ServiceID]) VALUES (N'058162045 ', N'Pr1160    ')
INSERT [dbo].[BookingService] ([BookingID], [ServiceID]) VALUES (N'632574028 ', N'Pr1129    ')
INSERT [dbo].[BookingService] ([BookingID], [ServiceID]) VALUES (N'632574028 ', N'Pr1160    ')
INSERT [dbo].[BookingService] ([BookingID], [ServiceID]) VALUES (N'043414818 ', N'Pr1129    ')
INSERT [dbo].[BookingService] ([BookingID], [ServiceID]) VALUES (N'043414818 ', N'Pr1160    ')
INSERT [dbo].[BookingService] ([BookingID], [ServiceID]) VALUES (N'862487288 ', N'Pr1155    ')
INSERT [dbo].[BRAID_SERVICE] ([ServiceID], [WidthID], [LengthID], [StyleID]) VALUES (N'Pr1101    ', N'W1000     ', N'L1000     ', N'S1001     ')
INSERT [dbo].[BRAID_SERVICE] ([ServiceID], [WidthID], [LengthID], [StyleID]) VALUES (N'Pr1102    ', N'W1001     ', N'L1000     ', N'S1001     ')
INSERT [dbo].[BRAID_SERVICE] ([ServiceID], [WidthID], [LengthID], [StyleID]) VALUES (N'Pr1103    ', N'W1002     ', N'L1000     ', N'S1001     ')
INSERT [dbo].[BRAID_SERVICE] ([ServiceID], [WidthID], [LengthID], [StyleID]) VALUES (N'Pr1104    ', N'W1000     ', N'L1001     ', N'S1001     ')
INSERT [dbo].[BRAID_SERVICE] ([ServiceID], [WidthID], [LengthID], [StyleID]) VALUES (N'Pr1105    ', N'W1001     ', N'L1001     ', N'S1001     ')
INSERT [dbo].[BRAID_SERVICE] ([ServiceID], [WidthID], [LengthID], [StyleID]) VALUES (N'Pr1106    ', N'W1002     ', N'L1001     ', N'S1001     ')
INSERT [dbo].[BRAID_SERVICE] ([ServiceID], [WidthID], [LengthID], [StyleID]) VALUES (N'Pr1107    ', N'W1000     ', N'L1002     ', N'S1001     ')
INSERT [dbo].[BRAID_SERVICE] ([ServiceID], [WidthID], [LengthID], [StyleID]) VALUES (N'Pr1108    ', N'W1001     ', N'L1002     ', N'S1001     ')
INSERT [dbo].[BRAID_SERVICE] ([ServiceID], [WidthID], [LengthID], [StyleID]) VALUES (N'Pr1109    ', N'W1002     ', N'L1002     ', N'S1001     ')
INSERT [dbo].[BRAID_SERVICE] ([ServiceID], [WidthID], [LengthID], [StyleID]) VALUES (N'Pr1110    ', N'W1000     ', N'L1000     ', N'S1002     ')
INSERT [dbo].[BRAID_SERVICE] ([ServiceID], [WidthID], [LengthID], [StyleID]) VALUES (N'Pr1111    ', N'W1001     ', N'L1000     ', N'S1002     ')
INSERT [dbo].[BRAID_SERVICE] ([ServiceID], [WidthID], [LengthID], [StyleID]) VALUES (N'Pr1112    ', N'W1002     ', N'L1000     ', N'S1002     ')
INSERT [dbo].[BRAID_SERVICE] ([ServiceID], [WidthID], [LengthID], [StyleID]) VALUES (N'Pr1113    ', N'W1000     ', N'L1001     ', N'S1002     ')
INSERT [dbo].[BRAID_SERVICE] ([ServiceID], [WidthID], [LengthID], [StyleID]) VALUES (N'Pr1114    ', N'W1001     ', N'L1001     ', N'S1002     ')
INSERT [dbo].[BRAID_SERVICE] ([ServiceID], [WidthID], [LengthID], [StyleID]) VALUES (N'Pr1115    ', N'W1002     ', N'L1001     ', N'S1002     ')
INSERT [dbo].[BRAID_SERVICE] ([ServiceID], [WidthID], [LengthID], [StyleID]) VALUES (N'Pr1116    ', N'W1000     ', N'L1002     ', N'S1002     ')
INSERT [dbo].[BRAID_SERVICE] ([ServiceID], [WidthID], [LengthID], [StyleID]) VALUES (N'Pr1117    ', N'W1001     ', N'L1002     ', N'S1002     ')
INSERT [dbo].[BRAID_SERVICE] ([ServiceID], [WidthID], [LengthID], [StyleID]) VALUES (N'Pr1118    ', N'W1002     ', N'L1002     ', N'S1002     ')
INSERT [dbo].[BRAID_SERVICE] ([ServiceID], [WidthID], [LengthID], [StyleID]) VALUES (N'Pr1119    ', N'W1001     ', N'L1000     ', N'S1003     ')
INSERT [dbo].[BRAID_SERVICE] ([ServiceID], [WidthID], [LengthID], [StyleID]) VALUES (N'Pr1120    ', N'W1001     ', N'L1001     ', N'S1003     ')
INSERT [dbo].[BRAID_SERVICE] ([ServiceID], [WidthID], [LengthID], [StyleID]) VALUES (N'Pr1121    ', N'W1001     ', N'L1002     ', N'S1003     ')
INSERT [dbo].[BRAID_SERVICE] ([ServiceID], [WidthID], [LengthID], [StyleID]) VALUES (N'Pr1122    ', N'W1002     ', N'L1001     ', N'S1003     ')
INSERT [dbo].[BRAID_SERVICE] ([ServiceID], [WidthID], [LengthID], [StyleID]) VALUES (N'Pr1123    ', N'W1002     ', N'L1002     ', N'S1003     ')
INSERT [dbo].[BRAID_SERVICE] ([ServiceID], [WidthID], [LengthID], [StyleID]) VALUES (N'Pr1124    ', N'W1001     ', N'L1001     ', N'S1004     ')
INSERT [dbo].[BRAID_SERVICE] ([ServiceID], [WidthID], [LengthID], [StyleID]) VALUES (N'Pr1125    ', N'W1001     ', N'L1002     ', N'S1004     ')
INSERT [dbo].[BRAID_SERVICE] ([ServiceID], [WidthID], [LengthID], [StyleID]) VALUES (N'Pr1126    ', N'W1002     ', N'L1001     ', N'S1004     ')
INSERT [dbo].[BRAID_SERVICE] ([ServiceID], [WidthID], [LengthID], [StyleID]) VALUES (N'Pr1127    ', N'W1002     ', N'L1002     ', N'S1004     ')
INSERT [dbo].[BRAID_SERVICE] ([ServiceID], [WidthID], [LengthID], [StyleID]) VALUES (N'Pr405     ', NULL, NULL, NULL)
INSERT [dbo].[BRAID_SERVICE] ([ServiceID], [WidthID], [LengthID], [StyleID]) VALUES (N'Pr571     ', NULL, NULL, NULL)
INSERT [dbo].[BRAID_SERVICE] ([ServiceID], [WidthID], [LengthID], [StyleID]) VALUES (N'Pr674     ', NULL, NULL, NULL)
INSERT [dbo].[BRAID_SERVICE] ([ServiceID], [WidthID], [LengthID], [StyleID]) VALUES (N'Pr743     ', NULL, NULL, NULL)
INSERT [dbo].[BRAND] ([BrandID], [Name], [Type(T/A)]) VALUES (N'B01       ', N'ORS', N'T')
INSERT [dbo].[BRAND] ([BrandID], [Name], [Type(T/A)]) VALUES (N'B02       ', N'X-Pression Ultra', N'A')
INSERT [dbo].[BRAND] ([BrandID], [Name], [Type(T/A)]) VALUES (N'B03       ', N'Tropic Isle Living', N'T')
INSERT [dbo].[BRAND] ([BrandID], [Name], [Type(T/A)]) VALUES (N'B04       ', N'Cantu', N'T')
INSERT [dbo].[BRAND] ([BrandID], [Name], [Type(T/A)]) VALUES (N'B05       ', N'Joedir Nature', N'A')
INSERT [dbo].[BRAND] ([BrandID], [Name], [Type(T/A)]) VALUES (N'B06       ', N'Afro Botanics', N'T')
INSERT [dbo].[BRAND] ([BrandID], [Name], [Type(T/A)]) VALUES (N'B07       ', N'Darling', N'A')
INSERT [dbo].[BRAND] ([BrandID], [Name], [Type(T/A)]) VALUES (N'B08       ', N'Shea Moisture', N'T')
INSERT [dbo].[BRAND] ([BrandID], [Name], [Type(T/A)]) VALUES (N'B09       ', N'Dark and Lovely', N'T')
INSERT [dbo].[BRAND] ([BrandID], [Name], [Type(T/A)]) VALUES (N'B10       ', N'Frika', N'A')
INSERT [dbo].[BRAND] ([BrandID], [Name], [Type(T/A)]) VALUES (N'B11       ', N'Inecto', N'T')
INSERT [dbo].[BRAND] ([BrandID], [Name], [Type(T/A)]) VALUES (N'B12       ', N'Aunt Jackie''s', N'T')
INSERT [dbo].[BRAND] ([BrandID], [Name], [Type(T/A)]) VALUES (N'B13       ', N'Long & Lasting', N'T')
INSERT [dbo].[BRAND] ([BrandID], [Name], [Type(T/A)]) VALUES (N'B14       ', N'Head & Shoulders', N'T')
INSERT [dbo].[BRAND] ([BrandID], [Name], [Type(T/A)]) VALUES (N'B15       ', N'Chocolate Hair', N'A')
INSERT [dbo].[BRAND] ([BrandID], [Name], [Type(T/A)]) VALUES (N'B16       ', N'Vicher human Hair', N'A')
INSERT [dbo].[BRAND] ([BrandID], [Name], [Type(T/A)]) VALUES (N'B17       ', N'Joedir Magic', N'A')
INSERT [dbo].[BRAND] ([BrandID], [Name], [Type(T/A)]) VALUES (N'B18       ', N'New Crown', N'A')
INSERT [dbo].[BRAND] ([BrandID], [Name], [Type(T/A)]) VALUES (N'B19       ', N'Hair Decoration', N'A')
INSERT [dbo].[BRAND] ([BrandID], [Name], [Type(T/A)]) VALUES (N'B20       ', N'Isoplus', N'T')
INSERT [dbo].[BRAND] ([BrandID], [Name], [Type(T/A)]) VALUES (N'B21       ', N'Donna', N'A')
INSERT [dbo].[BRAND] ([BrandID], [Name], [Type(T/A)]) VALUES (N'B22       ', N'Nikki', N'A')
INSERT [dbo].[BRAND] ([BrandID], [Name], [Type(T/A)]) VALUES (N'B23       ', N'Vicher', N'A')
INSERT [dbo].[BUSINESS] ([BusinessID], [Vat%], [VatRegNo], [AddressLine1], [AddressLine2], [Phone], [WeekdayStart], [WeekdayEnd], [WeekendStart], [WeekendEnd], [PublicHolStart], [PublicHolEnd], [Logo]) VALUES (N'001       ', 15, N'1001      ', N'1, University Way, Summerstrand', N'Port Elizabeth, 6019', N'0411234567', CAST(N'08:00:00' AS Time), CAST(N'18:00:00' AS Time), CAST(N'08:30:00' AS Time), CAST(N'17:30:00' AS Time), CAST(N'09:00:00' AS Time), CAST(N'17:00:00' AS Time), NULL)
INSERT [dbo].[CUST_VISIT] ([CustomerID], [Date], [BookingID], [Description]) VALUES (N'100155997350724101850         ', CAST(N'2018-08-02T00:00:00.000' AS DateTime), N'666666666 ', N'likes fade with nothing off the top')
INSERT [dbo].[CUST_VISIT] ([CustomerID], [Date], [BookingID], [Description]) VALUES (N'100155997350724101850         ', CAST(N'2018-08-09T00:00:00.000' AS DateTime), N'645546546 ', N'Prefers apple shampoo')
INSERT [dbo].[CUST_VISIT] ([CustomerID], [Date], [BookingID], [Description]) VALUES (N'105242998585655922697         ', CAST(N'2018-07-31T00:00:00.000' AS DateTime), N'555555555 ', N'Neck length, normal sized plait braids')
INSERT [dbo].[CUST_VISIT] ([CustomerID], [Date], [BookingID], [Description]) VALUES (N'105242998585655922697         ', CAST(N'2018-09-10T00:00:00.000' AS DateTime), N'127874202 ', N'Likes Aunt Jackie''s Shampoo')
INSERT [dbo].[CUST_VISIT] ([CustomerID], [Date], [BookingID], [Description]) VALUES (N'181477264752348604465         ', CAST(N'2018-07-24T00:00:00.000' AS DateTime), N'1         ', N'Thin braid bob')
INSERT [dbo].[CUST_VISIT] ([CustomerID], [Date], [BookingID], [Description]) VALUES (N'181477264752348604465         ', CAST(N'2018-08-07T00:00:00.000' AS DateTime), N'645646343 ', N'Prefer premium extensions')
INSERT [dbo].[CUST_VISIT] ([CustomerID], [Date], [BookingID], [Description]) VALUES (N'181477264752348604465         ', CAST(N'2018-08-09T00:00:00.000' AS DateTime), N'444444444 ', N'Customer was 2 hours late')
INSERT [dbo].[CUST_VISIT] ([CustomerID], [Date], [BookingID], [Description]) VALUES (N'215703134623237784686         ', CAST(N'2018-09-03T00:00:00.000' AS DateTime), N'300017033 ', N'Likes Aunt Jackie''s Shampoo')
INSERT [dbo].[CUST_VISIT] ([CustomerID], [Date], [BookingID], [Description]) VALUES (N'612648662678214805485         ', CAST(N'2018-08-31T00:00:00.000' AS DateTime), N'677752606 ', N'Likes Aunt Jackie''s Shampoo')
INSERT [dbo].[CUST_VISIT] ([CustomerID], [Date], [BookingID], [Description]) VALUES (N'635506662246576754303         ', CAST(N'2018-05-02T00:00:00.000' AS DateTime), N'516516516 ', N'Likes Orage Juice')
INSERT [dbo].[CUST_VISIT] ([CustomerID], [Date], [BookingID], [Description]) VALUES (N'662881846588586465447         ', CAST(N'2018-08-16T00:00:00.000' AS DateTime), N'228313337 ', N'Likes Aunt Jackie''s Shampoo')
INSERT [dbo].[CUST_VISIT] ([CustomerID], [Date], [BookingID], [Description]) VALUES (N'662881846588586465447         ', CAST(N'2018-09-03T00:00:00.000' AS DateTime), N'860870858 ', N'Likes Aunt Jackie''s Shampoo')
INSERT [dbo].[EMPLOYEE] ([EmployeeID], [Type], [Bio], [AddressLine1], [AddressLine2], [Suburb], [City]) VALUES (N'105450503348172771121         ', N'S         ', N'Hello, Im Ed.I have been in this industry and community for over 25 years. I enjoy the opportunity to apply my experience, education, and love of this profession to give you an enjoyable and satisfying salon experience.', N'2', N'Fir Street', N'Bethelsdorp                                                                                         ', N'Port Elizabeth                                                                                      ')
INSERT [dbo].[EMPLOYEE] ([EmployeeID], [Type], [Bio], [AddressLine1], [AddressLine2], [Suburb], [City]) VALUES (N'112413834414360855751         ', N'R         ', NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[EMPLOYEE] ([EmployeeID], [Type], [Bio], [AddressLine1], [AddressLine2], [Suburb], [City]) VALUES (N'112475171777167063459         ', N'M         ', NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[EMPLOYEE] ([EmployeeID], [Type], [Bio], [AddressLine1], [AddressLine2], [Suburb], [City]) VALUES (N'118233419479102946333         ', N'S         ', N'Hey there, Im Jayden. I have been a partner in two very successful salons in Oak Lawn - Abracadabra and Nolan Ryan Hair Salon. I have trained at academies in Dallas, Las Vegas, and New York. I am a certified stylist using Deva Curl technique and products. I attend as many classes and shows as I can. Continuing education is crucial in this profession. I am fortunate to be part of such an exciting and fast paced industry. I rewarded each day with every client I work with using my skills to help them look and feel special.', N'55', N' main road', N'Lovermore Park                                                                                      ', N'Port Elizabeth                                                                                      ')
INSERT [dbo].[EMPLOYEE] ([EmployeeID], [Type], [Bio], [AddressLine1], [AddressLine2], [Suburb], [City]) VALUES (N'270146203840160705048         ', N'S         ', N'Hello.I have been in the hair industry for over 6 years. I am well versed in many forms of coloring and cutting techniques, and thrive in long hair, adding soft layers and dimension with color, adding that special touch of sparkle. On the other side of the spectrum, I have a special talent for wavy and curly hair, as well as strong geometrical bobs.', N'99', N' Main Road ', N'Fairbridg Heights                                                                                   ', N'Uitenhage, Eastern Cape                                                                             ')
INSERT [dbo].[EMPLOYEE] ([EmployeeID], [Type], [Bio], [AddressLine1], [AddressLine2], [Suburb], [City]) VALUES (N'341104773302321231832         ', N'R         ', NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[EMPLOYEE] ([EmployeeID], [Type], [Bio], [AddressLine1], [AddressLine2], [Suburb], [City]) VALUES (N'580312424824681240311         ', N'S         ', N'Hi there, I''m Lolo. For me, hair is a passion. A passion that began as a young child. A passion that is inspired by Hollywood and fashion. However,my focus is always on individual clients'' life and style. I pride myself on making my clients feel renewed and refreshed after every salon experience. I''m focuses on every detail to create a polished , fresh look for, my clients.', N'1 Adam St', N'Zwide 2', N' Ibhayi                                                                                             ', N'Port Elizabeth                                                                                      ')
INSERT [dbo].[Home_Page] ([FeatureID], [ItemID], [ImageURL]) VALUES (N'Ser01     ', N'Pr1109                        ', NULL)
INSERT [dbo].[Home_Page] ([FeatureID], [ItemID], [ImageURL]) VALUES (N'Ser02     ', N'Pr1147                        ', NULL)
INSERT [dbo].[Home_Page] ([FeatureID], [ItemID], [ImageURL]) VALUES (N'Ser03     ', N'Pr1129                        ', NULL)
INSERT [dbo].[Home_Page] ([FeatureID], [ItemID], [ImageURL]) VALUES (N'Ser04     ', N'Pr1155                        ', NULL)
INSERT [dbo].[Home_Page] ([FeatureID], [ItemID], [ImageURL]) VALUES (N'Pro01     ', N'Pr1163                        ', N'http://sict-iis.nmmu.ac.za/beauxdebut/Theam/img/portfolio/thumbnails/1.jpg                                                             ')
INSERT [dbo].[Home_Page] ([FeatureID], [ItemID], [ImageURL]) VALUES (N'Pro02     ', N'Pr1170                        ', N'http://sict-iis.nmmu.ac.za/beauxdebut/Theam/img/portfolio/thumbnails/2.jpg                                                             ')
INSERT [dbo].[Home_Page] ([FeatureID], [ItemID], [ImageURL]) VALUES (N'Pro03     ', N'Pr1169                        ', N'http://sict-iis.nmmu.ac.za/beauxdebut/Theam/img/portfolio/thumbnails/3.jpg                                                             ')
INSERT [dbo].[Home_Page] ([FeatureID], [ItemID], [ImageURL]) VALUES (N'Sty01     ', N'118233419479102946333         ', N'http://sict-iis.nmmu.ac.za/beauxdebut/Theam/img/portfolio/thumbnails/4.jpg')
INSERT [dbo].[Home_Page] ([FeatureID], [ItemID], [ImageURL]) VALUES (N'Sty02     ', N'580312424824681240311         ', N'http://sict-iis.nmmu.ac.za/beauxdebut/Theam/img/portfolio/thumbnails/5.jpeg')
INSERT [dbo].[Home_Page] ([FeatureID], [ItemID], [ImageURL]) VALUES (N'Sty03     ', N'270146203840160705048         ', N'http://sict-iis.nmmu.ac.za/beauxdebut/Theam/img/portfolio/thumbnails/6.jpg')
INSERT [dbo].[Home_Page] ([FeatureID], [ItemID], [ImageURL]) VALUES (N'CwuPno    ', N'341104773302321231832         ', NULL)
INSERT [dbo].[Home_Page] ([FeatureID], [ItemID], [ImageURL]) VALUES (N'CwuEma    ', N'112413834414360855751         ', NULL)
INSERT [dbo].[LENGTH] ([LengthID], [Description]) VALUES (N'L1000     ', N'Short')
INSERT [dbo].[LENGTH] ([LengthID], [Description]) VALUES (N'L1001     ', N'Medium')
INSERT [dbo].[LENGTH] ([LengthID], [Description]) VALUES (N'L1002     ', N'Long')
INSERT [dbo].[Order] ([SupplierID], [OrderID], [OrderDate], [Received], [DateReceived]) VALUES (N'001       ', N'001       ', CAST(N'2018-09-16T06:32:00.000' AS DateTime), 1, CAST(N'2018-09-20T14:35:00.000' AS DateTime))
INSERT [dbo].[Order] ([SupplierID], [OrderID], [OrderDate], [Received], [DateReceived]) VALUES (N'001       ', N'002       ', CAST(N'2018-09-06T22:45:00.000' AS DateTime), 1, CAST(N'2018-09-09T10:53:00.000' AS DateTime))
INSERT [dbo].[Order] ([SupplierID], [OrderID], [OrderDate], [Received], [DateReceived]) VALUES (N'001       ', N'123582357 ', CAST(N'2018-10-04T12:01:30.420' AS DateTime), 1, CAST(N'2018-10-04T12:01:50.213' AS DateTime))
INSERT [dbo].[Order] ([SupplierID], [OrderID], [OrderDate], [Received], [DateReceived]) VALUES (N'001       ', N'175550572 ', CAST(N'2018-10-16T15:56:33.683' AS DateTime), 0, NULL)
INSERT [dbo].[Order] ([SupplierID], [OrderID], [OrderDate], [Received], [DateReceived]) VALUES (N'001       ', N'385862653 ', CAST(N'2018-09-21T11:50:29.393' AS DateTime), 1, CAST(N'2018-10-01T10:10:32.257' AS DateTime))
INSERT [dbo].[Order] ([SupplierID], [OrderID], [OrderDate], [Received], [DateReceived]) VALUES (N'001       ', N'435128101 ', CAST(N'2018-10-01T10:17:25.897' AS DateTime), 1, CAST(N'2018-10-04T10:32:56.117' AS DateTime))
INSERT [dbo].[Order] ([SupplierID], [OrderID], [OrderDate], [Received], [DateReceived]) VALUES (N'001       ', N'527060755 ', CAST(N'2018-10-09T15:29:27.610' AS DateTime), 1, CAST(N'2018-10-12T14:46:37.037' AS DateTime))
INSERT [dbo].[Order_DTL] ([OrderID], [ProductID], [Qty]) VALUES (N'001       ', N'Pr1132    ', 10)
INSERT [dbo].[Order_DTL] ([OrderID], [ProductID], [Qty]) VALUES (N'001       ', N'Pr1170    ', 10)
INSERT [dbo].[Order_DTL] ([OrderID], [ProductID], [Qty]) VALUES (N'002       ', N'Pr1132    ', 10)
INSERT [dbo].[Order_DTL] ([OrderID], [ProductID], [Qty]) VALUES (N'002       ', N'PR1170    ', 10)
INSERT [dbo].[Order_DTL] ([OrderID], [ProductID], [Qty]) VALUES (N'385862653 ', N'Pr1149    ', 10)
INSERT [dbo].[Order_DTL] ([OrderID], [ProductID], [Qty]) VALUES (N'385862653 ', N'Pr1170    ', 10)
INSERT [dbo].[Order_DTL] ([OrderID], [ProductID], [Qty]) VALUES (N'385862653 ', N'Pr1151    ', 10)
INSERT [dbo].[Order_DTL] ([OrderID], [ProductID], [Qty]) VALUES (N'385862653 ', N'Pr1169    ', 10)
INSERT [dbo].[Order_DTL] ([OrderID], [ProductID], [Qty]) VALUES (N'385862653 ', N'Pr1136    ', 10)
INSERT [dbo].[Order_DTL] ([OrderID], [ProductID], [Qty]) VALUES (N'385862653 ', N'Pr1164    ', 15)
INSERT [dbo].[Order_DTL] ([OrderID], [ProductID], [Qty]) VALUES (N'385862653 ', N'Pr1159    ', 20)
INSERT [dbo].[Order_DTL] ([OrderID], [ProductID], [Qty]) VALUES (N'435128101 ', N'Pr1171    ', 5)
INSERT [dbo].[Order_DTL] ([OrderID], [ProductID], [Qty]) VALUES (N'435128101 ', N'Pr1164    ', 5)
INSERT [dbo].[Order_DTL] ([OrderID], [ProductID], [Qty]) VALUES (N'435128101 ', N'Pr1143    ', 5)
INSERT [dbo].[Order_DTL] ([OrderID], [ProductID], [Qty]) VALUES (N'123582357 ', N'Pr1149    ', 1)
INSERT [dbo].[Order_DTL] ([OrderID], [ProductID], [Qty]) VALUES (N'123582357 ', N'Pr1169    ', 1)
INSERT [dbo].[Order_DTL] ([OrderID], [ProductID], [Qty]) VALUES (N'527060755 ', N'Pr1168    ', 5)
INSERT [dbo].[Order_DTL] ([OrderID], [ProductID], [Qty]) VALUES (N'527060755 ', N'Pr1132    ', 5)
INSERT [dbo].[Order_DTL] ([OrderID], [ProductID], [Qty]) VALUES (N'527060755 ', N'Pr1149    ', 5)
INSERT [dbo].[Order_DTL] ([OrderID], [ProductID], [Qty]) VALUES (N'527060755 ', N'Pr1163    ', 5)
INSERT [dbo].[Order_DTL] ([OrderID], [ProductID], [Qty]) VALUES (N'175550572 ', N'Pr1132    ', 5)
INSERT [dbo].[PRODUCT] ([ProductID], [Name], [ProductDescription], [Price], [ProductType(T/A/S)], [Active], [ProductImage]) VALUES (N'Pr1101    ', N'Thin braid bob', N'Thin, bob length plait braids', 350.0000, N'S', N'Y', NULL)
INSERT [dbo].[PRODUCT] ([ProductID], [Name], [ProductDescription], [Price], [ProductType(T/A/S)], [Active], [ProductImage]) VALUES (N'Pr1102    ', N'Medium braid bob', N'Neck length, normal sized plait braids', 300.0000, N'S', N'Y', NULL)
INSERT [dbo].[PRODUCT] ([ProductID], [Name], [ProductDescription], [Price], [ProductType(T/A/S)], [Active], [ProductImage]) VALUES (N'Pr1103    ', N'Box braid bob', N'Neck length, box plait braids', 320.0000, N'S', N'Y', NULL)
INSERT [dbo].[PRODUCT] ([ProductID], [Name], [ProductDescription], [Price], [ProductType(T/A/S)], [Active], [ProductImage]) VALUES (N'Pr1104    ', N'Thin shoulder braids', N'Thin, shoulder length plait braids', 380.0000, N'S', N'Y', NULL)
INSERT [dbo].[PRODUCT] ([ProductID], [Name], [ProductDescription], [Price], [ProductType(T/A/S)], [Active], [ProductImage]) VALUES (N'Pr1105    ', N'Medium shoulder braids', N'Shoulder length, normal sized plait braids', 350.0000, N'S', N'Y', NULL)
INSERT [dbo].[PRODUCT] ([ProductID], [Name], [ProductDescription], [Price], [ProductType(T/A/S)], [Active], [ProductImage]) VALUES (N'Pr1106    ', N'Box shoulder braids', N'Shoulder length, thick plait braids', 370.0000, N'S', N'Y', NULL)
INSERT [dbo].[PRODUCT] ([ProductID], [Name], [ProductDescription], [Price], [ProductType(T/A/S)], [Active], [ProductImage]) VALUES (N'Pr1107    ', N'Thin waist length braids', N'Thin, waist length plait braids', 420.0000, N'S', N'Y', NULL)
INSERT [dbo].[PRODUCT] ([ProductID], [Name], [ProductDescription], [Price], [ProductType(T/A/S)], [Active], [ProductImage]) VALUES (N'Pr1108    ', N'Medium waist length braids', N'Normal width, waist length plait braids', 390.0000, N'S', N'Y', NULL)
INSERT [dbo].[PRODUCT] ([ProductID], [Name], [ProductDescription], [Price], [ProductType(T/A/S)], [Active], [ProductImage]) VALUES (N'Pr1109    ', N'Box braids', N'Normal box braids(waist length, thick plait braids)', 410.0000, N'S', N'Y', NULL)
INSERT [dbo].[PRODUCT] ([ProductID], [Name], [ProductDescription], [Price], [ProductType(T/A/S)], [Active], [ProductImage]) VALUES (N'Pr1110    ', N'Thin twist bob', N'Thin, bob length twist', 340.0000, N'S', N'Y', NULL)
INSERT [dbo].[PRODUCT] ([ProductID], [Name], [ProductDescription], [Price], [ProductType(T/A/S)], [Active], [ProductImage]) VALUES (N'Pr1111    ', N'Medium twist bob', N'Neck length, normal sized twists', 300.0000, N'S', N'Y', NULL)
INSERT [dbo].[PRODUCT] ([ProductID], [Name], [ProductDescription], [Price], [ProductType(T/A/S)], [Active], [ProductImage]) VALUES (N'Pr1112    ', N'Box twist bob', N'Neck length box twists', 310.0000, N'S', N'Y', NULL)
INSERT [dbo].[PRODUCT] ([ProductID], [Name], [ProductDescription], [Price], [ProductType(T/A/S)], [Active], [ProductImage]) VALUES (N'Pr1113    ', N'Thin shoulder twist', N'Thin, shoulder length twists', 370.0000, N'S', N'Y', NULL)
INSERT [dbo].[PRODUCT] ([ProductID], [Name], [ProductDescription], [Price], [ProductType(T/A/S)], [Active], [ProductImage]) VALUES (N'Pr1114    ', N'Medium shoulder twist', N'Normal width, shoulder length twists', 340.0000, N'S', N'Y', NULL)
INSERT [dbo].[PRODUCT] ([ProductID], [Name], [ProductDescription], [Price], [ProductType(T/A/S)], [Active], [ProductImage]) VALUES (N'Pr1115    ', N'Box shoulder twist', N'Shoulder length box twist', 380.0000, N'S', N'Y', NULL)
INSERT [dbo].[PRODUCT] ([ProductID], [Name], [ProductDescription], [Price], [ProductType(T/A/S)], [Active], [ProductImage]) VALUES (N'Pr1116    ', N'Thin waist length twist', N'Thin, waist length twists', 410.0000, N'S', N'Y', NULL)
INSERT [dbo].[PRODUCT] ([ProductID], [Name], [ProductDescription], [Price], [ProductType(T/A/S)], [Active], [ProductImage]) VALUES (N'Pr1117    ', N'Medium waist length twist', N'Waist length twists', 390.0000, N'S', N'Y', NULL)
INSERT [dbo].[PRODUCT] ([ProductID], [Name], [ProductDescription], [Price], [ProductType(T/A/S)], [Active], [ProductImage]) VALUES (N'Pr1118    ', N'Box twist', N'Normal box twists(waist length, thick twists)', 420.0000, N'S', N'Y', NULL)
INSERT [dbo].[PRODUCT] ([ProductID], [Name], [ProductDescription], [Price], [ProductType(T/A/S)], [Active], [ProductImage]) VALUES (N'Pr1119    ', N'Straight-back bob', N'Normal width, straight back bob', 200.0000, N'S', N'Y', NULL)
INSERT [dbo].[PRODUCT] ([ProductID], [Name], [ProductDescription], [Price], [ProductType(T/A/S)], [Active], [ProductImage]) VALUES (N'Pr1120    ', N'Straight-back shoulder', N'Normal width, shoulder length cornrows', 250.0000, N'S', N'Y', NULL)
INSERT [dbo].[PRODUCT] ([ProductID], [Name], [ProductDescription], [Price], [ProductType(T/A/S)], [Active], [ProductImage]) VALUES (N'Pr1121    ', N'Straight-back waist', N'Normal width, waist length cornrows', 300.0000, N'S', N'Y', NULL)
INSERT [dbo].[PRODUCT] ([ProductID], [Name], [ProductDescription], [Price], [ProductType(T/A/S)], [Active], [ProductImage]) VALUES (N'Pr1122    ', N'Box straight-back shoulder', N'Thick, shoulder length cornrows', 150.0000, N'S', N'Y', NULL)
INSERT [dbo].[PRODUCT] ([ProductID], [Name], [ProductDescription], [Price], [ProductType(T/A/S)], [Active], [ProductImage]) VALUES (N'Pr1123    ', N'Box straight-back waist', N'Thick, waist length cornrows', 180.0000, N'S', N'Y', NULL)
INSERT [dbo].[PRODUCT] ([ProductID], [Name], [ProductDescription], [Price], [ProductType(T/A/S)], [Active], [ProductImage]) VALUES (N'Pr1124    ', N'Upstyle shoulder', N'Normal width, shoulder length, upstyle cornrows', 250.0000, N'S', N'Y', NULL)
INSERT [dbo].[PRODUCT] ([ProductID], [Name], [ProductDescription], [Price], [ProductType(T/A/S)], [Active], [ProductImage]) VALUES (N'Pr1125    ', N'Upstyle waist', N'Normal width, waist length, upstyle cornrows', 300.0000, N'S', N'Y', NULL)
INSERT [dbo].[PRODUCT] ([ProductID], [Name], [ProductDescription], [Price], [ProductType(T/A/S)], [Active], [ProductImage]) VALUES (N'Pr1126    ', N'Box upstyle shoulder ', N'Shoulder length, thick, upstyle cornrows', 200.0000, N'S', N'Y', NULL)
INSERT [dbo].[PRODUCT] ([ProductID], [Name], [ProductDescription], [Price], [ProductType(T/A/S)], [Active], [ProductImage]) VALUES (N'Pr1127    ', N'Box upstyle waist', N'Waist length, thick, upstyle cornrows', 250.0000, N'S', N'Y', NULL)
INSERT [dbo].[PRODUCT] ([ProductID], [Name], [ProductDescription], [Price], [ProductType(T/A/S)], [Active], [ProductImage]) VALUES (N'Pr1128    ', N'Faux locs', N'Application of faux locs', 400.0000, N'S', N'Y', NULL)
INSERT [dbo].[PRODUCT] ([ProductID], [Name], [ProductDescription], [Price], [ProductType(T/A/S)], [Active], [ProductImage]) VALUES (N'Pr1129    ', N'Cut', N'Hair cut', 25.0000, N'S', N'Y', NULL)
INSERT [dbo].[PRODUCT] ([ProductID], [Name], [ProductDescription], [Price], [ProductType(T/A/S)], [Active], [ProductImage]) VALUES (N'Pr1130    ', N'Jamaican Black Castor Oil(Original)', N'Castor oil', 180.0000, N'T', N'Y', NULL)
INSERT [dbo].[PRODUCT] ([ProductID], [Name], [ProductDescription], [Price], [ProductType(T/A/S)], [Active], [ProductImage]) VALUES (N'Pr1131    ', N'Cantu Curl Cream', N'Curl defining cream', 70.0000, N'T', N'Y', NULL)
INSERT [dbo].[PRODUCT] ([ProductID], [Name], [ProductDescription], [Price], [ProductType(T/A/S)], [Active], [ProductImage]) VALUES (N'Pr1132    ', N'ORS Curls Unleashed Sulphate-Free Shampoo ', N'Clarifying shampoo', 170.0000, N'T', N'Y', NULL)
INSERT [dbo].[PRODUCT] ([ProductID], [Name], [ProductDescription], [Price], [ProductType(T/A/S)], [Active], [ProductImage]) VALUES (N'Pr1133    ', N'Dark and Lovely Au Naturale Plaiting Pudding Cream', N'Cream', 85.0000, N'T', N'Y', NULL)
INSERT [dbo].[PRODUCT] ([ProductID], [Name], [ProductDescription], [Price], [ProductType(T/A/S)], [Active], [ProductImage]) VALUES (N'Pr1134    ', N'Cantu Extra Hold Edge Stay Gel', N'Edge controlling gel', 75.0000, N'T', N'Y', NULL)
INSERT [dbo].[PRODUCT] ([ProductID], [Name], [ProductDescription], [Price], [ProductType(T/A/S)], [Active], [ProductImage]) VALUES (N'Pr1135    ', N'Hair Decoration Filigree Tube', N'Decorative hair accessory(hair cuffs)', 20.0000, N'A', N'Y', NULL)
INSERT [dbo].[PRODUCT] ([ProductID], [Name], [ProductDescription], [Price], [ProductType(T/A/S)], [Active], [ProductImage]) VALUES (N'Pr1136    ', N'X-Pression ultra braid #1', N'Synthetic hair for braiding', 35.0000, N'A', N'Y', NULL)
INSERT [dbo].[PRODUCT] ([ProductID], [Name], [ProductDescription], [Price], [ProductType(T/A/S)], [Active], [ProductImage]) VALUES (N'Pr1137    ', N'Joedir Nature Straight(100% human hair) 10"', N'Weave packet', 150.0000, N'A', N'Y', NULL)
INSERT [dbo].[PRODUCT] ([ProductID], [Name], [ProductDescription], [Price], [ProductType(T/A/S)], [Active], [ProductImage]) VALUES (N'Pr1138    ', N'Donna Wig Cap', N'2 Pack Black Donna wig cap', 40.0000, N'A', N'Y', NULL)
INSERT [dbo].[PRODUCT] ([ProductID], [Name], [ProductDescription], [Price], [ProductType(T/A/S)], [Active], [ProductImage]) VALUES (N'Pr1139    ', N'ORS No-Lye Hair Relaxer', N'Hair relaxer', 75.0000, N'T', N'Y', NULL)
INSERT [dbo].[PRODUCT] ([ProductID], [Name], [ProductDescription], [Price], [ProductType(T/A/S)], [Active], [ProductImage]) VALUES (N'Pr1140    ', N'X-Pression ultra braid #350', N'Synthetic hair for braiding', 35.0000, N'A', N'Y', NULL)
INSERT [dbo].[PRODUCT] ([ProductID], [Name], [ProductDescription], [Price], [ProductType(T/A/S)], [Active], [ProductImage]) VALUES (N'Pr1141    ', N'Dark and Lovely Au Naturale Hair Butter', N'Anti-breakage cream', 75.0000, N'T', N'Y', NULL)
INSERT [dbo].[PRODUCT] ([ProductID], [Name], [ProductDescription], [Price], [ProductType(T/A/S)], [Active], [ProductImage]) VALUES (N'Pr1142    ', N'Afro Botanics Twist, Curl & Define Cream', N'Cream', 65.0000, N'T', N'Y', NULL)
INSERT [dbo].[PRODUCT] ([ProductID], [Name], [ProductDescription], [Price], [ProductType(T/A/S)], [Active], [ProductImage]) VALUES (N'Pr1143    ', N'ORS Curls Unleashed Intense Hair Conditioner', N'Hair Conditioner', 160.0000, N'T', N'Y', NULL)
INSERT [dbo].[PRODUCT] ([ProductID], [Name], [ProductDescription], [Price], [ProductType(T/A/S)], [Active], [ProductImage]) VALUES (N'Pr1144    ', N'Chocolate Premium Quality(100% human hair) 16"', N'Weave packet', 360.0000, N'A', N'Y', NULL)
INSERT [dbo].[PRODUCT] ([ProductID], [Name], [ProductDescription], [Price], [ProductType(T/A/S)], [Active], [ProductImage]) VALUES (N'Pr1145    ', N'X-Pression ultra braid #2', N'Synthetic hair for braiding', 35.0000, N'A', N'Y', NULL)
INSERT [dbo].[PRODUCT] ([ProductID], [Name], [ProductDescription], [Price], [ProductType(T/A/S)], [Active], [ProductImage]) VALUES (N'Pr1146    ', N'Shea Moisture Coconut&Hibiscus Curl Smoothie', N'Curl defining cream', 270.0000, N'T', N'Y', NULL)
INSERT [dbo].[PRODUCT] ([ProductID], [Name], [ProductDescription], [Price], [ProductType(T/A/S)], [Active], [ProductImage]) VALUES (N'Pr1147    ', N'Colour', N'Hair dye', 40.0000, N'S', N'Y', NULL)
INSERT [dbo].[PRODUCT] ([ProductID], [Name], [ProductDescription], [Price], [ProductType(T/A/S)], [Active], [ProductImage]) VALUES (N'Pr1148    ', N'Relax', N'Process of relaxing hair', 100.0000, N'S', N'Y', NULL)
INSERT [dbo].[PRODUCT] ([ProductID], [Name], [ProductDescription], [Price], [ProductType(T/A/S)], [Active], [ProductImage]) VALUES (N'Pr1149    ', N'Afro Botanics Moisturising Shampoo', N'Clarifying Shampoo', 100.0000, N'T', N'Y', NULL)
INSERT [dbo].[PRODUCT] ([ProductID], [Name], [ProductDescription], [Price], [ProductType(T/A/S)], [Active], [ProductImage]) VALUES (N'Pr1150    ', N'Head&Shoulders Moisturising Care Hairfood', N'Hair food', 75.0000, N'T', N'Y', NULL)
INSERT [dbo].[PRODUCT] ([ProductID], [Name], [ProductDescription], [Price], [ProductType(T/A/S)], [Active], [ProductImage]) VALUES (N'Pr1151    ', N'Darling Hair Extensions Yaki Braid 1B', N'Synthetic hair for braiding', 13.0000, N'A', N'Y', NULL)
INSERT [dbo].[PRODUCT] ([ProductID], [Name], [ProductDescription], [Price], [ProductType(T/A/S)], [Active], [ProductImage]) VALUES (N'Pr1152    ', N'Frika Braid Maxi Dread 1B', N'Synthetic hair for soft dread', 70.0000, N'A', N'Y', NULL)
INSERT [dbo].[PRODUCT] ([ProductID], [Name], [ProductDescription], [Price], [ProductType(T/A/S)], [Active], [ProductImage]) VALUES (N'Pr1153    ', N'Darling Soft Dread ', N'Synthetic hair for soft dread', 60.0000, N'A', N'Y', NULL)
INSERT [dbo].[PRODUCT] ([ProductID], [Name], [ProductDescription], [Price], [ProductType(T/A/S)], [Active], [ProductImage]) VALUES (N'Pr1154    ', N'Weave (no planting)', N'Application of weave', 180.0000, N'S', N'Y', NULL)
INSERT [dbo].[PRODUCT] ([ProductID], [Name], [ProductDescription], [Price], [ProductType(T/A/S)], [Active], [ProductImage]) VALUES (N'Pr1155    ', N'Weave (with planting)', N'Application of weave', 200.0000, N'S', N'Y', NULL)
INSERT [dbo].[PRODUCT] ([ProductID], [Name], [ProductDescription], [Price], [ProductType(T/A/S)], [Active], [ProductImage]) VALUES (N'Pr1156    ', N'Frika Braid Maxi Dread 1B/350', N'Synthetic hair for soft dread', 70.0000, N'A', N'Y', NULL)
INSERT [dbo].[PRODUCT] ([ProductID], [Name], [ProductDescription], [Price], [ProductType(T/A/S)], [Active], [ProductImage]) VALUES (N'Pr1157    ', N'Inecto Ultra Gloss Semi-Permanent Hair Colour Kit(Black Leather)', N'Hair dye', 30.0000, N'T', N'Y', NULL)
INSERT [dbo].[PRODUCT] ([ProductID], [Name], [ProductDescription], [Price], [ProductType(T/A/S)], [Active], [ProductImage]) VALUES (N'Pr1158    ', N'Jamaican Black Castor Oil(Rosemary)', N'Castor Oil', 200.0000, N'T', N'Y', NULL)
INSERT [dbo].[PRODUCT] ([ProductID], [Name], [ProductDescription], [Price], [ProductType(T/A/S)], [Active], [ProductImage]) VALUES (N'Pr1159    ', N'Aunt Jackie''s Flaxseed Elongating Curling Gel', N'Curl & twist defining gel', 80.0000, N'T', N'Y', NULL)
INSERT [dbo].[PRODUCT] ([ProductID], [Name], [ProductDescription], [Price], [ProductType(T/A/S)], [Active], [ProductImage]) VALUES (N'Pr1160    ', N'Wash', N'Washing natural hair', 35.0000, N'S', N'Y', NULL)
INSERT [dbo].[PRODUCT] ([ProductID], [Name], [ProductDescription], [Price], [ProductType(T/A/S)], [Active], [ProductImage]) VALUES (N'Pr1161    ', N'Frika Braid Hot Fibre 1B Rich Black', N'Synthetic hair for braiding', 35.0000, N'A', N'Y', NULL)
INSERT [dbo].[PRODUCT] ([ProductID], [Name], [ProductDescription], [Price], [ProductType(T/A/S)], [Active], [ProductImage]) VALUES (N'Pr1162    ', N'Frika Hotex Weave 18" D33', N'Weave Packet', 90.0000, N'A', N'Y', NULL)
INSERT [dbo].[PRODUCT] ([ProductID], [Name], [ProductDescription], [Price], [ProductType(T/A/S)], [Active], [ProductImage]) VALUES (N'Pr1163    ', N'Aunt Jackie''s Curls And Coils Oh So Clean Shampoo', N'Moisturising Shampoo', 60.0000, N'T', N'Y', NULL)
INSERT [dbo].[PRODUCT] ([ProductID], [Name], [ProductDescription], [Price], [ProductType(T/A/S)], [Active], [ProductImage]) VALUES (N'Pr1164    ', N'Darling Hair Extensions Brazillian Wave 1', N'Weave Packet', 85.0000, N'A', N'Y', NULL)
INSERT [dbo].[PRODUCT] ([ProductID], [Name], [ProductDescription], [Price], [ProductType(T/A/S)], [Active], [ProductImage]) VALUES (N'Pr1165    ', N'Chocolate Premium Quality(100% human hair) 16"', N'Weave Packet', 360.0000, N'A', N'Y', NULL)
INSERT [dbo].[PRODUCT] ([ProductID], [Name], [ProductDescription], [Price], [ProductType(T/A/S)], [Active], [ProductImage]) VALUES (N'Pr1166    ', N'Joedir Nature Straight(100% human hair) 12"', N'Weave Packet', 160.0000, N'A', N'Y', NULL)
INSERT [dbo].[PRODUCT] ([ProductID], [Name], [ProductDescription], [Price], [ProductType(T/A/S)], [Active], [ProductImage]) VALUES (N'Pr1167    ', N'Darling One Million Braids Ombre', N'Synthetic hair for braiding', 35.0000, N'A', N'Y', NULL)
INSERT [dbo].[PRODUCT] ([ProductID], [Name], [ProductDescription], [Price], [ProductType(T/A/S)], [Active], [ProductImage]) VALUES (N'Pr1168    ', N'Nikki R Dreads 22"', N'Faux Locs', 90.0000, N'A', N'Y', NULL)
INSERT [dbo].[PRODUCT] ([ProductID], [Name], [ProductDescription], [Price], [ProductType(T/A/S)], [Active], [ProductImage]) VALUES (N'Pr1169    ', N'Shea Moisture Jamaican Black Castor Oil Shampoo', N'Clarifying Shampoo', 250.0000, N'T', N'Y', NULL)
INSERT [dbo].[PRODUCT] ([ProductID], [Name], [ProductDescription], [Price], [ProductType(T/A/S)], [Active], [ProductImage]) VALUES (N'Pr1170    ', N'Afro Botanics Repairing and Strengthening Treatment', N'Hair conditioner', 65.0000, N'T', N'Y', NULL)
INSERT [dbo].[PRODUCT] ([ProductID], [Name], [ProductDescription], [Price], [ProductType(T/A/S)], [Active], [ProductImage]) VALUES (N'Pr1171    ', N'Cantu Shea Butter For Natural Hair Co-Wash', N'Co-Wash', 200.0000, N'T', N'Y', NULL)
INSERT [dbo].[PRODUCT] ([ProductID], [Name], [ProductDescription], [Price], [ProductType(T/A/S)], [Active], [ProductImage]) VALUES (N'Pr1172    ', N'Nikki Soft Dreads', N'Synthetic hair for soft dread', 30.0000, N'A', N'Y', NULL)
INSERT [dbo].[PRODUCT] ([ProductID], [Name], [ProductDescription], [Price], [ProductType(T/A/S)], [Active], [ProductImage]) VALUES (N'Pr1173    ', N'ORS Curls Unleashed Leave-In Conditioner', N'Conditioner', 165.0000, N'T', N'Y', NULL)
INSERT [dbo].[PRODUCT] ([ProductID], [Name], [ProductDescription], [Price], [ProductType(T/A/S)], [Active], [ProductImage]) VALUES (N'Pr1174    ', N'Vicher Brazillian Remi 12"', N'Weave packet', 500.0000, N'A', N'Y', NULL)
INSERT [dbo].[PRODUCT] ([ProductID], [Name], [ProductDescription], [Price], [ProductType(T/A/S)], [Active], [ProductImage]) VALUES (N'Pr1175    ', N'Sick - Half Day', N'Sick', 0.0000, N'U', N'Y', NULL)
INSERT [dbo].[PRODUCT] ([ProductID], [Name], [ProductDescription], [Price], [ProductType(T/A/S)], [Active], [ProductImage]) VALUES (N'Pr1176    ', N'Family - Half Day', N'Personal time', 0.0000, N'U', N'Y', NULL)
INSERT [dbo].[PRODUCT] ([ProductID], [Name], [ProductDescription], [Price], [ProductType(T/A/S)], [Active], [ProductImage]) VALUES (N'Pr1177    ', N'Sick - Full Day', N'Sick', 0.0000, N'U', N'Y', NULL)
INSERT [dbo].[PRODUCT] ([ProductID], [Name], [ProductDescription], [Price], [ProductType(T/A/S)], [Active], [ProductImage]) VALUES (N'Pr1178    ', N'Family - Full Day', N'Personal time', 0.0000, N'U', N'Y', NULL)
INSERT [dbo].[PRODUCT] ([ProductID], [Name], [ProductDescription], [Price], [ProductType(T/A/S)], [Active], [ProductImage]) VALUES (N'Pr462     ', N'test', N't', 360.0000, N'T', N'Y', NULL)
INSERT [dbo].[ProductType] ([TypeID], [Name], [Product/Service], [PrimaryService]) VALUES (N'A         ', N'Accessory                                                                                           ', N'P', 0)
INSERT [dbo].[ProductType] ([TypeID], [Name], [Product/Service], [PrimaryService]) VALUES (N'A         ', N'Application                                                                                         ', N'S', 1)
INSERT [dbo].[ProductType] ([TypeID], [Name], [Product/Service], [PrimaryService]) VALUES (N'B         ', N'Braid                                                                                               ', N'S', 1)
INSERT [dbo].[ProductType] ([TypeID], [Name], [Product/Service], [PrimaryService]) VALUES (N'N         ', N'Natural                                                                                             ', N'S', 1)
INSERT [dbo].[ProductType] ([TypeID], [Name], [Product/Service], [PrimaryService]) VALUES (N'S         ', N'Service                                                                                             ', N'S', 0)
INSERT [dbo].[ProductType] ([TypeID], [Name], [Product/Service], [PrimaryService]) VALUES (N'T         ', N'Treatment                                                                                           ', N'P', 0)
INSERT [dbo].[ProductType] ([TypeID], [Name], [Product/Service], [PrimaryService]) VALUES (N'U         ', N'Employee Leave                                                                                      ', N'S', 0)
INSERT [dbo].[REVIEW] ([ReviewID], [CustomerID], [EmployeeID], [Date], [Time], [Rating], [Comment], [primaryBookingID]) VALUES (N'023833500 ', N'407242734406745485560         ', N'270146203840160705048         ', CAST(N'2018-10-09T00:00:00.000' AS DateTime), CAST(N'16:35:19' AS Time), 5, N'Uses the perfect temperature water', N'561520075 ')
INSERT [dbo].[REVIEW] ([ReviewID], [CustomerID], [EmployeeID], [Date], [Time], [Rating], [Comment], [primaryBookingID]) VALUES (N'241270580 ', N'105242998585655922697         ', N'105450503348172771121         ', CAST(N'2018-10-05T00:00:00.000' AS DateTime), CAST(N'12:01:41' AS Time), 4, N'Service was a bit slow', N'511652873 ')
INSERT [dbo].[REVIEW] ([ReviewID], [CustomerID], [EmployeeID], [Date], [Time], [Rating], [Comment], [primaryBookingID]) VALUES (N'252447888 ', N'777706452281511868742         ', N'118233419479102946333         ', CAST(N'2018-10-01T00:00:00.000' AS DateTime), CAST(N'10:07:42' AS Time), 5, N'Your review here....', N'653420332 ')
INSERT [dbo].[REVIEW] ([ReviewID], [CustomerID], [EmployeeID], [Date], [Time], [Rating], [Comment], [primaryBookingID]) VALUES (N'548332130 ', N'612648662678214805485         ', N'118233419479102946333         ', CAST(N'2018-10-09T00:00:00.000' AS DateTime), CAST(N'16:36:53' AS Time), 2, N'messed up the hairline', N'786223434 ')
INSERT [dbo].[REVIEW] ([ReviewID], [CustomerID], [EmployeeID], [Date], [Time], [Rating], [Comment], [primaryBookingID]) VALUES (N'578206661 ', N'181477264752348604465         ', N'105450503348172771121         ', CAST(N'2018-10-06T00:00:00.000' AS DateTime), CAST(N'09:24:01' AS Time), 5, N'Ed is very efficient', N'714132047 ')
INSERT [dbo].[SALE] ([SaleID], [Date], [CustomerID], [PaymentType], [BookingID]) VALUES (N'004400232 ', CAST(N'2018-10-08T15:22:05.937' AS DateTime), N'635506662246576754303         ', N'Credit    ', N'004400232 ')
INSERT [dbo].[SALE] ([SaleID], [Date], [CustomerID], [PaymentType], [BookingID]) VALUES (N'010170465 ', CAST(N'2018-10-02T09:57:55.873' AS DateTime), N'662881846588586465447         ', N'Cash      ', NULL)
INSERT [dbo].[SALE] ([SaleID], [Date], [CustomerID], [PaymentType], [BookingID]) VALUES (N'011111111 ', CAST(N'2018-09-01T13:07:49.950' AS DateTime), N'635506662246576754303         ', N'Credit    ', N'011111111 ')
INSERT [dbo].[SALE] ([SaleID], [Date], [CustomerID], [PaymentType], [BookingID]) VALUES (N'040808655 ', CAST(N'2018-10-13T16:36:44.130' AS DateTime), N'324465860528272078866         ', N'Cash      ', NULL)
INSERT [dbo].[SALE] ([SaleID], [Date], [CustomerID], [PaymentType], [BookingID]) VALUES (N'052367723 ', CAST(N'2018-08-20T16:26:58.643' AS DateTime), N'612648662678214805485         ', N'Cash      ', N'052367723 ')
INSERT [dbo].[SALE] ([SaleID], [Date], [CustomerID], [PaymentType], [BookingID]) VALUES (N'055402078 ', CAST(N'2018-10-02T09:52:30.320' AS DateTime), N'662881846588586465447         ', NULL, NULL)
INSERT [dbo].[SALE] ([SaleID], [Date], [CustomerID], [PaymentType], [BookingID]) VALUES (N'060026124 ', CAST(N'2018-08-22T14:35:52.100' AS DateTime), N'662881846588586465447         ', N'Cash      ', N'060026124 ')
INSERT [dbo].[SALE] ([SaleID], [Date], [CustomerID], [PaymentType], [BookingID]) VALUES (N'063323570 ', CAST(N'2018-10-01T10:24:38.680' AS DateTime), N'777706452281511868742         ', N'Cash      ', N'063323570 ')
INSERT [dbo].[SALE] ([SaleID], [Date], [CustomerID], [PaymentType], [BookingID]) VALUES (N'070741623 ', CAST(N'2018-08-29T16:55:52.650' AS DateTime), N'662881846588586465447         ', N'Credit    ', N'070741623 ')
INSERT [dbo].[SALE] ([SaleID], [Date], [CustomerID], [PaymentType], [BookingID]) VALUES (N'074163732 ', CAST(N'2018-09-23T16:31:46.727' AS DateTime), N'612648662678214805485         ', N'Cash      ', NULL)
INSERT [dbo].[SALE] ([SaleID], [Date], [CustomerID], [PaymentType], [BookingID]) VALUES (N'107548258 ', CAST(N'2018-10-08T15:21:15.643' AS DateTime), N'777706452281511868742         ', N'Cash      ', NULL)
INSERT [dbo].[SALE] ([SaleID], [Date], [CustomerID], [PaymentType], [BookingID]) VALUES (N'111111111 ', CAST(N'2018-08-01T16:09:48.530' AS DateTime), N'105242998585655922697         ', N'Credit    ', N'111111111 ')
INSERT [dbo].[SALE] ([SaleID], [Date], [CustomerID], [PaymentType], [BookingID]) VALUES (N'114357316 ', CAST(N'2018-08-28T17:43:46.603' AS DateTime), N'535314300265460205603         ', N'Credit    ', N'114357316 ')
INSERT [dbo].[SALE] ([SaleID], [Date], [CustomerID], [PaymentType], [BookingID]) VALUES (N'122222222 ', CAST(N'2018-08-07T12:11:49.487' AS DateTime), N'100155997350724101850         ', N'Cash      ', N'122222222 ')
INSERT [dbo].[SALE] ([SaleID], [Date], [CustomerID], [PaymentType], [BookingID]) VALUES (N'151711823 ', CAST(N'2018-08-24T16:35:28.843' AS DateTime), N'662881846588586465447         ', N'Cash      ', N'151711823 ')
INSERT [dbo].[SALE] ([SaleID], [Date], [CustomerID], [PaymentType], [BookingID]) VALUES (N'155466825 ', CAST(N'2018-09-25T12:55:36.327' AS DateTime), N'384715112175204214480         ', N'Cash      ', N'155466825 ')
INSERT [dbo].[SALE] ([SaleID], [Date], [CustomerID], [PaymentType], [BookingID]) VALUES (N'165151551 ', CAST(N'2018-08-06T12:05:06.577' AS DateTime), N'181477264752348604465         ', N'Credit    ', N'165151551 ')
INSERT [dbo].[SALE] ([SaleID], [Date], [CustomerID], [PaymentType], [BookingID]) VALUES (N'173457147 ', CAST(N'2018-08-23T15:32:31.130' AS DateTime), N'612648662678214805485         ', N'Cash      ', N'173457147 ')
INSERT [dbo].[SALE] ([SaleID], [Date], [CustomerID], [PaymentType], [BookingID]) VALUES (N'173566683 ', CAST(N'2018-10-08T15:23:49.553' AS DateTime), N'100155997350724101850         ', N'Cash      ', N'173566683 ')
INSERT [dbo].[SALE] ([SaleID], [Date], [CustomerID], [PaymentType], [BookingID]) VALUES (N'176024758 ', CAST(N'2018-07-24T14:12:15.560' AS DateTime), N'181477264752348604465         ', N'Credit    ', N'176024758 ')
INSERT [dbo].[SALE] ([SaleID], [Date], [CustomerID], [PaymentType], [BookingID]) VALUES (N'201782088 ', CAST(N'2018-09-03T17:26:31.450' AS DateTime), N'100155997350724101850         ', N'Cash      ', N'201782088 ')
INSERT [dbo].[SALE] ([SaleID], [Date], [CustomerID], [PaymentType], [BookingID]) VALUES (N'210661534 ', CAST(N'2018-10-16T16:01:50.363' AS DateTime), N'140623085382221348147         ', N'Cash      ', NULL)
INSERT [dbo].[SALE] ([SaleID], [Date], [CustomerID], [PaymentType], [BookingID]) VALUES (N'214181455 ', CAST(N'2018-10-03T11:56:03.870' AS DateTime), N'384715112175204214480         ', N'Credit    ', N'214181455 ')
INSERT [dbo].[SALE] ([SaleID], [Date], [CustomerID], [PaymentType], [BookingID]) VALUES (N'217264372 ', CAST(N'2018-10-02T09:46:16.470' AS DateTime), N'100155997350724101850         ', N'Credit    ', N'217264372 ')
INSERT [dbo].[SALE] ([SaleID], [Date], [CustomerID], [PaymentType], [BookingID]) VALUES (N'222222222 ', CAST(N'2018-08-01T09:36:09.030' AS DateTime), N'181477264752348604465         ', N'Cash      ', N'222222222 ')
INSERT [dbo].[SALE] ([SaleID], [Date], [CustomerID], [PaymentType], [BookingID]) VALUES (N'222435233 ', CAST(N'2018-08-24T17:00:49.683' AS DateTime), N'662881846588586465447         ', N'Credit    ', N'222435233 ')
INSERT [dbo].[SALE] ([SaleID], [Date], [CustomerID], [PaymentType], [BookingID]) VALUES (N'224332528 ', CAST(N'2018-10-10T12:57:49.613' AS DateTime), N'766086547051628703024         ', N'Credit    ', NULL)
INSERT [dbo].[SALE] ([SaleID], [Date], [CustomerID], [PaymentType], [BookingID]) VALUES (N'228313337 ', CAST(N'2018-08-16T16:02:07.373' AS DateTime), N'662881846588586465447         ', N'Cash      ', N'228313337 ')
INSERT [dbo].[SALE] ([SaleID], [Date], [CustomerID], [PaymentType], [BookingID]) VALUES (N'232052115 ', CAST(N'2018-09-23T16:26:16.767' AS DateTime), N'215703134623237784686         ', N'Cash      ', NULL)
INSERT [dbo].[SALE] ([SaleID], [Date], [CustomerID], [PaymentType], [BookingID]) VALUES (N'260225447 ', CAST(N'2018-09-15T16:56:41.173' AS DateTime), N'100155997350724101850         ', N'Credit    ', N'260225447 ')
INSERT [dbo].[SALE] ([SaleID], [Date], [CustomerID], [PaymentType], [BookingID]) VALUES (N'267247235 ', CAST(N'2018-09-22T15:07:48.567' AS DateTime), N'215703134623237784686         ', N'Credit    ', N'267247235 ')
INSERT [dbo].[SALE] ([SaleID], [Date], [CustomerID], [PaymentType], [BookingID]) VALUES (N'285352471 ', CAST(N'2018-10-11T16:42:17.327' AS DateTime), N'535314300265460205603         ', N'Cash      ', N'285352471 ')
INSERT [dbo].[SALE] ([SaleID], [Date], [CustomerID], [PaymentType], [BookingID]) VALUES (N'286404016 ', CAST(N'2018-10-11T16:41:25.013' AS DateTime), N'384715112175204214480         ', N'Cash      ', N'286404016 ')
INSERT [dbo].[SALE] ([SaleID], [Date], [CustomerID], [PaymentType], [BookingID]) VALUES (N'300017033 ', CAST(N'2018-09-03T17:28:57.553' AS DateTime), N'215703134623237784686         ', N'Credit    ', N'300017033 ')
INSERT [dbo].[SALE] ([SaleID], [Date], [CustomerID], [PaymentType], [BookingID]) VALUES (N'303004356 ', CAST(N'2018-09-23T16:21:03.903' AS DateTime), N'100155997350724101850         ', N'Credit    ', NULL)
INSERT [dbo].[SALE] ([SaleID], [Date], [CustomerID], [PaymentType], [BookingID]) VALUES (N'315667782 ', CAST(N'2018-10-13T16:34:54.160' AS DateTime), N'105242998585655922697         ', N'Credit    ', N'315667782 ')
INSERT [dbo].[SALE] ([SaleID], [Date], [CustomerID], [PaymentType], [BookingID]) VALUES (N'351153017 ', CAST(N'2018-10-01T13:01:02.397' AS DateTime), N'635506662246576754303         ', N'Cash      ', N'351153017 ')
INSERT [dbo].[SALE] ([SaleID], [Date], [CustomerID], [PaymentType], [BookingID]) VALUES (N'358180023 ', CAST(N'2018-09-05T15:37:20.260' AS DateTime), N'612648662678214805485         ', N'Cash      ', N'358180023 ')
INSERT [dbo].[SALE] ([SaleID], [Date], [CustomerID], [PaymentType], [BookingID]) VALUES (N'364151404 ', CAST(N'2018-09-23T16:29:31.750' AS DateTime), N'777706452281511868742         ', N'Cash      ', NULL)
INSERT [dbo].[SALE] ([SaleID], [Date], [CustomerID], [PaymentType], [BookingID]) VALUES (N'365450324 ', CAST(N'2018-08-26T13:00:29.360' AS DateTime), N'100155997350724101850         ', N'Cash      ', N'365450324 ')
INSERT [dbo].[SALE] ([SaleID], [Date], [CustomerID], [PaymentType], [BookingID]) VALUES (N'365636365 ', CAST(N'2018-08-18T13:14:58.627' AS DateTime), N'100155997350724101850         ', N'Credit    ', N'365636365 ')
INSERT [dbo].[SALE] ([SaleID], [Date], [CustomerID], [PaymentType], [BookingID]) VALUES (N'378464537 ', CAST(N'2018-08-31T18:12:22.360' AS DateTime), N'181477264752348604465         ', N'Credit    ', N'378464537 ')
INSERT [dbo].[SALE] ([SaleID], [Date], [CustomerID], [PaymentType], [BookingID]) VALUES (N'405854081 ', CAST(N'2018-09-02T15:20:23.480' AS DateTime), N'384715112175204214480         ', N'Cash      ', N'405854081 ')
INSERT [dbo].[SALE] ([SaleID], [Date], [CustomerID], [PaymentType], [BookingID]) VALUES (N'418380632 ', CAST(N'2018-09-21T10:38:30.080' AS DateTime), N'181477264752348604465         ', N'Cash      ', N'418380632 ')
INSERT [dbo].[SALE] ([SaleID], [Date], [CustomerID], [PaymentType], [BookingID]) VALUES (N'433783285 ', CAST(N'2018-09-01T13:09:08.880' AS DateTime), N'535314300265460205603         ', N'Cash      ', N'433783285 ')
INSERT [dbo].[SALE] ([SaleID], [Date], [CustomerID], [PaymentType], [BookingID]) VALUES (N'444444444 ', CAST(N'2018-08-09T11:24:49.680' AS DateTime), N'181477264752348604465         ', N'Credit    ', N'444444444 ')
INSERT [dbo].[SALE] ([SaleID], [Date], [CustomerID], [PaymentType], [BookingID]) VALUES (N'455161654 ', CAST(N'2018-08-17T11:07:54.707' AS DateTime), N'105242998585655922697         ', N'Cash      ', N'455161654 ')
INSERT [dbo].[SALE] ([SaleID], [Date], [CustomerID], [PaymentType], [BookingID]) VALUES (N'464058276 ', CAST(N'2018-10-15T16:05:23.977' AS DateTime), N'154052322058082242258         ', N'Credit    ', NULL)
INSERT [dbo].[SALE] ([SaleID], [Date], [CustomerID], [PaymentType], [BookingID]) VALUES (N'467420776 ', CAST(N'2018-10-04T10:30:52.577' AS DateTime), N'215703134623237784686         ', N'Credit    ', N'467420776 ')
INSERT [dbo].[SALE] ([SaleID], [Date], [CustomerID], [PaymentType], [BookingID]) VALUES (N'471565888 ', CAST(N'2018-08-27T17:48:15.700' AS DateTime), N'635506662246576754303         ', N'Cash      ', N'471565888 ')
INSERT [dbo].[SALE] ([SaleID], [Date], [CustomerID], [PaymentType], [BookingID]) VALUES (N'483636551 ', CAST(N'2018-08-28T17:42:46.093' AS DateTime), N'181477264752348604465         ', N'Cash      ', N'483636551 ')
INSERT [dbo].[SALE] ([SaleID], [Date], [CustomerID], [PaymentType], [BookingID]) VALUES (N'511652873 ', CAST(N'2018-10-05T11:57:47.233' AS DateTime), N'105242998585655922697         ', N'Cash      ', N'511652873 ')
INSERT [dbo].[SALE] ([SaleID], [Date], [CustomerID], [PaymentType], [BookingID]) VALUES (N'516516516 ', CAST(N'2018-05-02T00:00:00.000' AS DateTime), N'635506662246576754303         ', N'Credit    ', N'516516516 ')
INSERT [dbo].[SALE] ([SaleID], [Date], [CustomerID], [PaymentType], [BookingID]) VALUES (N'525623472 ', CAST(N'2018-10-14T14:14:09.407' AS DateTime), N'181477264752348604465         ', N'Cash      ', N'525623472 ')
INSERT [dbo].[SALE] ([SaleID], [Date], [CustomerID], [PaymentType], [BookingID]) VALUES (N'525763663 ', CAST(N'2018-08-14T09:44:45.533' AS DateTime), N'105242998585655922697         ', N'Credit    ', N'525763663 ')
INSERT [dbo].[SALE] ([SaleID], [Date], [CustomerID], [PaymentType], [BookingID]) VALUES (N'543446466 ', CAST(N'2018-08-21T16:28:37.073' AS DateTime), N'100155997350724101850         ', N'Credit    ', N'543446466 ')
INSERT [dbo].[SALE] ([SaleID], [Date], [CustomerID], [PaymentType], [BookingID]) VALUES (N'547225656 ', CAST(N'2018-08-27T16:31:48.313' AS DateTime), N'215703134623237784686         ', N'Credit    ', N'547225656 ')
INSERT [dbo].[SALE] ([SaleID], [Date], [CustomerID], [PaymentType], [BookingID]) VALUES (N'555555555 ', CAST(N'2018-07-31T15:53:04.837' AS DateTime), N'105242998585655922697         ', N'Credit    ', N'555555555 ')
INSERT [dbo].[SALE] ([SaleID], [Date], [CustomerID], [PaymentType], [BookingID]) VALUES (N'561520075 ', CAST(N'2018-10-09T15:25:14.500' AS DateTime), N'407242734406745485560         ', N'Cash      ', N'561520075 ')
INSERT [dbo].[SALE] ([SaleID], [Date], [CustomerID], [PaymentType], [BookingID]) VALUES (N'561561561 ', CAST(N'2018-08-21T16:25:59.337' AS DateTime), N'635506662246576754303         ', N'Cash      ', N'561561561 ')
INSERT [dbo].[SALE] ([SaleID], [Date], [CustomerID], [PaymentType], [BookingID]) VALUES (N'564022384 ', CAST(N'2018-09-18T15:24:26.870' AS DateTime), N'215703134623237784686         ', N'Credit    ', N'564022384 ')
INSERT [dbo].[SALE] ([SaleID], [Date], [CustomerID], [PaymentType], [BookingID]) VALUES (N'564964895 ', CAST(N'2018-08-09T12:43:54.983' AS DateTime), N'105242998585655922697         ', N'Cash      ', N'564964895 ')
INSERT [dbo].[SALE] ([SaleID], [Date], [CustomerID], [PaymentType], [BookingID]) VALUES (N'565073505 ', CAST(N'2018-10-12T14:45:25.640' AS DateTime), N'215703134623237784686         ', N'Credit    ', N'565073505 ')
INSERT [dbo].[SALE] ([SaleID], [Date], [CustomerID], [PaymentType], [BookingID]) VALUES (N'568466551 ', CAST(N'2018-09-21T10:43:02.573' AS DateTime), N'612648662678214805485         ', N'Credit    ', N'568466551 ')
INSERT [dbo].[SALE] ([SaleID], [Date], [CustomerID], [PaymentType], [BookingID]) VALUES (N'574784585 ', CAST(N'2018-09-01T13:09:52.000' AS DateTime), N'662881846588586465447         ', N'Credit    ', N'574784585 ')
INSERT [dbo].[SALE] ([SaleID], [Date], [CustomerID], [PaymentType], [BookingID]) VALUES (N'616645615 ', CAST(N'2018-10-14T14:17:01.770' AS DateTime), N'307776754475328666200         ', N'Cash      ', NULL)
INSERT [dbo].[SALE] ([SaleID], [Date], [CustomerID], [PaymentType], [BookingID]) VALUES (N'622473016 ', CAST(N'2018-08-26T16:35:24.680' AS DateTime), N'535314300265460205603         ', N'Credit    ', N'622473016 ')
INSERT [dbo].[SALE] ([SaleID], [Date], [CustomerID], [PaymentType], [BookingID]) VALUES (N'632574028 ', CAST(N'2018-10-15T16:03:17.730' AS DateTime), N'324465860528272078866         ', N'Cash      ', N'632574028 ')
INSERT [dbo].[SALE] ([SaleID], [Date], [CustomerID], [PaymentType], [BookingID]) VALUES (N'645546546 ', CAST(N'2018-08-09T11:09:27.513' AS DateTime), N'100155997350724101850         ', N'Cash      ', N'645546546 ')
INSERT [dbo].[SALE] ([SaleID], [Date], [CustomerID], [PaymentType], [BookingID]) VALUES (N'645646343 ', CAST(N'2018-08-07T16:06:25.043' AS DateTime), N'181477264752348604465         ', N'Credit    ', N'645646343 ')
INSERT [dbo].[SALE] ([SaleID], [Date], [CustomerID], [PaymentType], [BookingID]) VALUES (N'645646565 ', CAST(N'2018-08-25T11:37:13.810' AS DateTime), N'105242998585655922697         ', N'Credit    ', N'645646565 ')
INSERT [dbo].[SALE] ([SaleID], [Date], [CustomerID], [PaymentType], [BookingID]) VALUES (N'651651655 ', CAST(N'2018-08-22T14:34:34.673' AS DateTime), N'181477264752348604465         ', N'Credit    ', N'651651655 ')
INSERT [dbo].[SALE] ([SaleID], [Date], [CustomerID], [PaymentType], [BookingID]) VALUES (N'653420332 ', CAST(N'2018-09-23T16:32:43.913' AS DateTime), N'777706452281511868742         ', N'Cash      ', N'653420332 ')
INSERT [dbo].[SALE] ([SaleID], [Date], [CustomerID], [PaymentType], [BookingID]) VALUES (N'654456645 ', CAST(N'2018-08-11T11:35:06.437' AS DateTime), N'686374338720721300668         ', N'Cash      ', N'654456645 ')
INSERT [dbo].[SALE] ([SaleID], [Date], [CustomerID], [PaymentType], [BookingID]) VALUES (N'656443183 ', CAST(N'2018-07-24T13:40:10.770' AS DateTime), N'181477264752348604465         ', N'Credit    ', N'656443183 ')
INSERT [dbo].[SALE] ([SaleID], [Date], [CustomerID], [PaymentType], [BookingID]) VALUES (N'666666666 ', CAST(N'2018-08-02T15:38:43.240' AS DateTime), N'100155997350724101850         ', N'Cash      ', N'666666666 ')
INSERT [dbo].[SALE] ([SaleID], [Date], [CustomerID], [PaymentType], [BookingID]) VALUES (N'670383801 ', CAST(N'2018-10-02T10:14:13.137' AS DateTime), N'407242734406745485560         ', N'Cash      ', N'670383801 ')
INSERT [dbo].[SALE] ([SaleID], [Date], [CustomerID], [PaymentType], [BookingID]) VALUES (N'677752606 ', CAST(N'2018-08-31T18:38:07.350' AS DateTime), N'612648662678214805485         ', N'Credit    ', N'677752606 ')
INSERT [dbo].[SALE] ([SaleID], [Date], [CustomerID], [PaymentType], [BookingID]) VALUES (N'714132047 ', CAST(N'2018-10-06T09:20:44.060' AS DateTime), N'181477264752348604465         ', N'Cash      ', N'714132047 ')
INSERT [dbo].[SALE] ([SaleID], [Date], [CustomerID], [PaymentType], [BookingID]) VALUES (N'737407647 ', CAST(N'2018-08-24T16:29:14.320' AS DateTime), N'384715112175204214480         ', N'Credit    ', N'737407647 ')
INSERT [dbo].[SALE] ([SaleID], [Date], [CustomerID], [PaymentType], [BookingID]) VALUES (N'760348252 ', CAST(N'2018-10-03T11:55:33.410' AS DateTime), N'535314300265460205603         ', N'Credit    ', N'760348252 ')
INSERT [dbo].[SALE] ([SaleID], [Date], [CustomerID], [PaymentType], [BookingID]) VALUES (N'776107376 ', CAST(N'2018-10-16T15:58:23.063' AS DateTime), N'777706452281511868742         ', N'Cash      ', N'776107376 ')
INSERT [dbo].[SALE] ([SaleID], [Date], [CustomerID], [PaymentType], [BookingID]) VALUES (N'786223434 ', CAST(N'2018-10-09T15:24:59.230' AS DateTime), N'612648662678214805485         ', N'Cash      ', N'786223434 ')
INSERT [dbo].[SALE] ([SaleID], [Date], [CustomerID], [PaymentType], [BookingID]) VALUES (N'844052626 ', CAST(N'2018-09-19T15:47:12.033' AS DateTime), N'535314300265460205603         ', N'Credit    ', N'844052626 ')
INSERT [dbo].[SALE] ([SaleID], [Date], [CustomerID], [PaymentType], [BookingID]) VALUES (N'860870858 ', CAST(N'2018-09-03T17:14:58.893' AS DateTime), N'662881846588586465447         ', N'Credit    ', N'860870858 ')
INSERT [dbo].[SALE] ([SaleID], [Date], [CustomerID], [PaymentType], [BookingID]) VALUES (N'868830621 ', CAST(N'2018-09-03T17:28:16.323' AS DateTime), N'612648662678214805485         ', N'Cash      ', N'868830621 ')
INSERT [dbo].[SALE] ([SaleID], [Date], [CustomerID], [PaymentType], [BookingID]) VALUES (N'873657564 ', CAST(N'2018-09-04T16:30:15.943' AS DateTime), N'181477264752348604465         ', N'Credit    ', N'873657564 ')
INSERT [dbo].[SALE] ([SaleID], [Date], [CustomerID], [PaymentType], [BookingID]) VALUES (N'877884443 ', CAST(N'2018-09-18T15:23:51.907' AS DateTime), N'105242998585655922697         ', N'Credit    ', N'877884443 ')
INSERT [dbo].[SALE] ([SaleID], [Date], [CustomerID], [PaymentType], [BookingID]) VALUES (N'885570402 ', CAST(N'2018-10-08T15:20:48.643' AS DateTime), N'777706452281511868742         ', N'Cash      ', N'885570402 ')
INSERT [dbo].[SALE] ([SaleID], [Date], [CustomerID], [PaymentType], [BookingID]) VALUES (N'NewSale   ', CAST(N'2018-09-23T14:37:32.450' AS DateTime), NULL, NULL, N'NewSale   ')
INSERT [dbo].[SALES_DTL] ([SaleID], [ProductID], [Qty], [Price]) VALUES (N'004400232 ', N'Pr1132    ', 1, 170.0000)
INSERT [dbo].[SALES_DTL] ([SaleID], [ProductID], [Qty], [Price]) VALUES (N'004400232 ', N'Pr1160    ', 1, 35.0000)
INSERT [dbo].[SALES_DTL] ([SaleID], [ProductID], [Qty], [Price]) VALUES (N'010170465 ', N'Pr1138    ', 1, 40.0000)
INSERT [dbo].[SALES_DTL] ([SaleID], [ProductID], [Qty], [Price]) VALUES (N'011111111 ', N'Pr1132    ', 1, 170.0000)
INSERT [dbo].[SALES_DTL] ([SaleID], [ProductID], [Qty], [Price]) VALUES (N'011111111 ', N'Pr1160    ', 1, 35.0000)
INSERT [dbo].[SALES_DTL] ([SaleID], [ProductID], [Qty], [Price]) VALUES (N'040808655 ', N'Pr1132    ', 1, 170.0000)
INSERT [dbo].[SALES_DTL] ([SaleID], [ProductID], [Qty], [Price]) VALUES (N'040808655 ', N'Pr1173    ', 1, 165.0000)
INSERT [dbo].[SALES_DTL] ([SaleID], [ProductID], [Qty], [Price]) VALUES (N'052367723 ', N'Pr1129    ', 1, 25.0000)
INSERT [dbo].[SALES_DTL] ([SaleID], [ProductID], [Qty], [Price]) VALUES (N'052367723 ', N'Pr1160    ', 1, 35.0000)
INSERT [dbo].[SALES_DTL] ([SaleID], [ProductID], [Qty], [Price]) VALUES (N'052367723 ', N'Pr1170    ', 1, 65.0000)
INSERT [dbo].[SALES_DTL] ([SaleID], [ProductID], [Qty], [Price]) VALUES (N'060026124 ', N'Pr1149    ', 1, 100.0000)
INSERT [dbo].[SALES_DTL] ([SaleID], [ProductID], [Qty], [Price]) VALUES (N'060026124 ', N'Pr1160    ', 1, 35.0000)
INSERT [dbo].[SALES_DTL] ([SaleID], [ProductID], [Qty], [Price]) VALUES (N'063323570 ', N'Pr1129    ', 1, 25.0000)
INSERT [dbo].[SALES_DTL] ([SaleID], [ProductID], [Qty], [Price]) VALUES (N'063323570 ', N'Pr1130    ', 1, 180.0000)
INSERT [dbo].[SALES_DTL] ([SaleID], [ProductID], [Qty], [Price]) VALUES (N'070741623 ', N'Pr1129    ', 1, 25.0000)
INSERT [dbo].[SALES_DTL] ([SaleID], [ProductID], [Qty], [Price]) VALUES (N'070741623 ', N'Pr1147    ', 1, 40.0000)
INSERT [dbo].[SALES_DTL] ([SaleID], [ProductID], [Qty], [Price]) VALUES (N'070741623 ', N'Pr1148    ', 1, 100.0000)
INSERT [dbo].[SALES_DTL] ([SaleID], [ProductID], [Qty], [Price]) VALUES (N'070741623 ', N'Pr1160    ', 1, 35.0000)
INSERT [dbo].[SALES_DTL] ([SaleID], [ProductID], [Qty], [Price]) VALUES (N'074163732 ', N'Pr1134    ', 1, 75.0000)
INSERT [dbo].[SALES_DTL] ([SaleID], [ProductID], [Qty], [Price]) VALUES (N'107548258 ', N'Pr1149    ', 1, 100.0000)
INSERT [dbo].[SALES_DTL] ([SaleID], [ProductID], [Qty], [Price]) VALUES (N'111111111 ', N'Pr1120    ', 1, 250.0000)
INSERT [dbo].[SALES_DTL] ([SaleID], [ProductID], [Qty], [Price]) VALUES (N'111111111 ', N'Pr1136    ', 1, 35.0000)
INSERT [dbo].[SALES_DTL] ([SaleID], [ProductID], [Qty], [Price]) VALUES (N'111111111 ', N'Pr1150    ', 1, 75.0000)
INSERT [dbo].[SALES_DTL] ([SaleID], [ProductID], [Qty], [Price]) VALUES (N'114357316 ', N'Pr1129    ', 1, 25.0000)
INSERT [dbo].[SALES_DTL] ([SaleID], [ProductID], [Qty], [Price]) VALUES (N'114357316 ', N'Pr1148    ', 1, 100.0000)
INSERT [dbo].[SALES_DTL] ([SaleID], [ProductID], [Qty], [Price]) VALUES (N'114357316 ', N'Pr1160    ', 1, 35.0000)
INSERT [dbo].[SALES_DTL] ([SaleID], [ProductID], [Qty], [Price]) VALUES (N'122222222 ', N'Pr1129    ', 1, 25.0000)
INSERT [dbo].[SALES_DTL] ([SaleID], [ProductID], [Qty], [Price]) VALUES (N'122222222 ', N'Pr1149    ', 1, 100.0000)
INSERT [dbo].[SALES_DTL] ([SaleID], [ProductID], [Qty], [Price]) VALUES (N'151711823 ', N'Pr1160    ', 1, 35.0000)
INSERT [dbo].[SALES_DTL] ([SaleID], [ProductID], [Qty], [Price]) VALUES (N'155466825 ', N'Pr1129    ', 1, 25.0000)
INSERT [dbo].[SALES_DTL] ([SaleID], [ProductID], [Qty], [Price]) VALUES (N'155466825 ', N'Pr1160    ', 1, 35.0000)
INSERT [dbo].[SALES_DTL] ([SaleID], [ProductID], [Qty], [Price]) VALUES (N'165151551 ', N'Pr1125    ', 1, 300.0000)
INSERT [dbo].[SALES_DTL] ([SaleID], [ProductID], [Qty], [Price]) VALUES (N'165151551 ', N'Pr1150    ', 1, 75.0000)
INSERT [dbo].[SALES_DTL] ([SaleID], [ProductID], [Qty], [Price]) VALUES (N'173457147 ', N'Pr1149    ', 1, 100.0000)
INSERT [dbo].[SALES_DTL] ([SaleID], [ProductID], [Qty], [Price]) VALUES (N'173457147 ', N'Pr1160    ', 1, 35.0000)
INSERT [dbo].[SALES_DTL] ([SaleID], [ProductID], [Qty], [Price]) VALUES (N'173566683 ', N'Pr1160    ', 1, 35.0000)
INSERT [dbo].[SALES_DTL] ([SaleID], [ProductID], [Qty], [Price]) VALUES (N'176024758 ', N'Pr1101    ', 1, 350.0000)
INSERT [dbo].[SALES_DTL] ([SaleID], [ProductID], [Qty], [Price]) VALUES (N'201782088 ', N'Pr1129    ', 1, 25.0000)
INSERT [dbo].[SALES_DTL] ([SaleID], [ProductID], [Qty], [Price]) VALUES (N'210661534 ', N'Pr1149    ', 1, 100.0000)
INSERT [dbo].[SALES_DTL] ([SaleID], [ProductID], [Qty], [Price]) VALUES (N'210661534 ', N'Pr1170    ', 1, 65.0000)
INSERT [dbo].[SALES_DTL] ([SaleID], [ProductID], [Qty], [Price]) VALUES (N'214181455 ', N'Pr1160    ', 1, 35.0000)
INSERT [dbo].[SALES_DTL] ([SaleID], [ProductID], [Qty], [Price]) VALUES (N'214181455 ', N'Pr1169    ', 1, 250.0000)
INSERT [dbo].[SALES_DTL] ([SaleID], [ProductID], [Qty], [Price]) VALUES (N'217264372 ', N'Pr1129    ', 1, 25.0000)
INSERT [dbo].[SALES_DTL] ([SaleID], [ProductID], [Qty], [Price]) VALUES (N'217264372 ', N'Pr1149    ', 2, 100.0000)
INSERT [dbo].[SALES_DTL] ([SaleID], [ProductID], [Qty], [Price]) VALUES (N'217264372 ', N'Pr1160    ', 1, 35.0000)
INSERT [dbo].[SALES_DTL] ([SaleID], [ProductID], [Qty], [Price]) VALUES (N'222222222 ', N'Pr1129    ', 1, 25.0000)
INSERT [dbo].[SALES_DTL] ([SaleID], [ProductID], [Qty], [Price]) VALUES (N'222222222 ', N'Pr1149    ', 1, 100.0000)
INSERT [dbo].[SALES_DTL] ([SaleID], [ProductID], [Qty], [Price]) VALUES (N'222222222 ', N'Pr1150    ', 1, 75.0000)
INSERT [dbo].[SALES_DTL] ([SaleID], [ProductID], [Qty], [Price]) VALUES (N'222435233 ', N'Pr1129    ', 1, 25.0000)
INSERT [dbo].[SALES_DTL] ([SaleID], [ProductID], [Qty], [Price]) VALUES (N'222435233 ', N'Pr1160    ', 1, 35.0000)
INSERT [dbo].[SALES_DTL] ([SaleID], [ProductID], [Qty], [Price]) VALUES (N'224332528 ', N'Pr1132    ', 1, 170.0000)
INSERT [dbo].[SALES_DTL] ([SaleID], [ProductID], [Qty], [Price]) VALUES (N'224332528 ', N'Pr1139    ', 1, 75.0000)
INSERT [dbo].[SALES_DTL] ([SaleID], [ProductID], [Qty], [Price]) VALUES (N'224332528 ', N'Pr1173    ', 1, 165.0000)
INSERT [dbo].[SALES_DTL] ([SaleID], [ProductID], [Qty], [Price]) VALUES (N'228313337 ', N'Pr1160    ', 1, 35.0000)
INSERT [dbo].[SALES_DTL] ([SaleID], [ProductID], [Qty], [Price]) VALUES (N'228313337 ', N'Pr1163    ', 1, 60.0000)
INSERT [dbo].[SALES_DTL] ([SaleID], [ProductID], [Qty], [Price]) VALUES (N'232052115 ', N'Pr1135    ', 2, 20.0000)
INSERT [dbo].[SALES_DTL] ([SaleID], [ProductID], [Qty], [Price]) VALUES (N'260225447 ', N'Pr1129    ', 1, 25.0000)
INSERT [dbo].[SALES_DTL] ([SaleID], [ProductID], [Qty], [Price]) VALUES (N'260225447 ', N'Pr1136    ', 3, 35.0000)
INSERT [dbo].[SALES_DTL] ([SaleID], [ProductID], [Qty], [Price]) VALUES (N'267247235 ', N'Pr1128    ', 1, 400.0000)
INSERT [dbo].[SALES_DTL] ([SaleID], [ProductID], [Qty], [Price]) VALUES (N'267247235 ', N'Pr1151    ', 1, 13.0000)
INSERT [dbo].[SALES_DTL] ([SaleID], [ProductID], [Qty], [Price]) VALUES (N'285352471 ', N'Pr1160    ', 1, 35.0000)
INSERT [dbo].[SALES_DTL] ([SaleID], [ProductID], [Qty], [Price]) VALUES (N'286404016 ', N'Pr1129    ', 1, 25.0000)
INSERT [dbo].[SALES_DTL] ([SaleID], [ProductID], [Qty], [Price]) VALUES (N'286404016 ', N'Pr1149    ', 1, 100.0000)
INSERT [dbo].[SALES_DTL] ([SaleID], [ProductID], [Qty], [Price]) VALUES (N'286404016 ', N'Pr1160    ', 1, 35.0000)
INSERT [dbo].[SALES_DTL] ([SaleID], [ProductID], [Qty], [Price]) VALUES (N'300017033 ', N'Pr1103    ', 1, 320.0000)
INSERT [dbo].[SALES_DTL] ([SaleID], [ProductID], [Qty], [Price]) VALUES (N'300017033 ', N'Pr1164    ', 2, 85.0000)
INSERT [dbo].[SALES_DTL] ([SaleID], [ProductID], [Qty], [Price]) VALUES (N'303004356 ', N'Pr1130    ', 1, 180.0000)
INSERT [dbo].[SALES_DTL] ([SaleID], [ProductID], [Qty], [Price]) VALUES (N'303004356 ', N'Pr1170    ', 1, 65.0000)
INSERT [dbo].[SALES_DTL] ([SaleID], [ProductID], [Qty], [Price]) VALUES (N'315667782 ', N'Pr1147    ', 1, 40.0000)
INSERT [dbo].[SALES_DTL] ([SaleID], [ProductID], [Qty], [Price]) VALUES (N'315667782 ', N'Pr1157    ', 1, 30.0000)
INSERT [dbo].[SALES_DTL] ([SaleID], [ProductID], [Qty], [Price]) VALUES (N'315667782 ', N'Pr1160    ', 1, 35.0000)
INSERT [dbo].[SALES_DTL] ([SaleID], [ProductID], [Qty], [Price]) VALUES (N'315667782 ', N'Pr1169    ', 1, 250.0000)
INSERT [dbo].[SALES_DTL] ([SaleID], [ProductID], [Qty], [Price]) VALUES (N'351153017 ', N'Pr1148    ', 1, 100.0000)
INSERT [dbo].[SALES_DTL] ([SaleID], [ProductID], [Qty], [Price]) VALUES (N'358180023 ', N'Pr1129    ', 1, 25.0000)
INSERT [dbo].[SALES_DTL] ([SaleID], [ProductID], [Qty], [Price]) VALUES (N'358180023 ', N'Pr1149    ', 1, 100.0000)
INSERT [dbo].[SALES_DTL] ([SaleID], [ProductID], [Qty], [Price]) VALUES (N'364151404 ', N'Pr1163    ', 1, 60.0000)
INSERT [dbo].[SALES_DTL] ([SaleID], [ProductID], [Qty], [Price]) VALUES (N'365450324 ', N'Pr1129    ', 1, 25.0000)
INSERT [dbo].[SALES_DTL] ([SaleID], [ProductID], [Qty], [Price]) VALUES (N'365450324 ', N'Pr1147    ', 1, 40.0000)
INSERT [dbo].[SALES_DTL] ([SaleID], [ProductID], [Qty], [Price]) VALUES (N'365450324 ', N'Pr1160    ', 1, 35.0000)
INSERT [dbo].[SALES_DTL] ([SaleID], [ProductID], [Qty], [Price]) VALUES (N'365450324 ', N'Pr1170    ', 1, 65.0000)
INSERT [dbo].[SALES_DTL] ([SaleID], [ProductID], [Qty], [Price]) VALUES (N'365636365 ', N'Pr1118    ', 1, 420.0000)
INSERT [dbo].[SALES_DTL] ([SaleID], [ProductID], [Qty], [Price]) VALUES (N'365636365 ', N'Pr1160    ', 1, 35.0000)
INSERT [dbo].[SALES_DTL] ([SaleID], [ProductID], [Qty], [Price]) VALUES (N'365636365 ', N'Pr1164    ', 1, 85.0000)
INSERT [dbo].[SALES_DTL] ([SaleID], [ProductID], [Qty], [Price]) VALUES (N'378464537 ', N'Pr1107    ', 1, 420.0000)
INSERT [dbo].[SALES_DTL] ([SaleID], [ProductID], [Qty], [Price]) VALUES (N'378464537 ', N'Pr1134    ', 1, 75.0000)
INSERT [dbo].[SALES_DTL] ([SaleID], [ProductID], [Qty], [Price]) VALUES (N'405854081 ', N'Pr1129    ', 1, 25.0000)
INSERT [dbo].[SALES_DTL] ([SaleID], [ProductID], [Qty], [Price]) VALUES (N'418380632 ', N'Pr1129    ', 1, 25.0000)
INSERT [dbo].[SALES_DTL] ([SaleID], [ProductID], [Qty], [Price]) VALUES (N'418380632 ', N'Pr1147    ', 1, 40.0000)
INSERT [dbo].[SALES_DTL] ([SaleID], [ProductID], [Qty], [Price]) VALUES (N'433783285 ', N'Pr1129    ', 1, 25.0000)
INSERT [dbo].[SALES_DTL] ([SaleID], [ProductID], [Qty], [Price]) VALUES (N'433783285 ', N'Pr1147    ', 1, 40.0000)
INSERT [dbo].[SALES_DTL] ([SaleID], [ProductID], [Qty], [Price]) VALUES (N'433783285 ', N'Pr1160    ', 1, 35.0000)
INSERT [dbo].[SALES_DTL] ([SaleID], [ProductID], [Qty], [Price]) VALUES (N'444444444 ', N'Pr1129    ', 1, 25.0000)
INSERT [dbo].[SALES_DTL] ([SaleID], [ProductID], [Qty], [Price]) VALUES (N'444444444 ', N'Pr1169    ', 2, 250.0000)
INSERT [dbo].[SALES_DTL] ([SaleID], [ProductID], [Qty], [Price]) VALUES (N'444444444 ', N'Pr1170    ', 1, 65.0000)
INSERT [dbo].[SALES_DTL] ([SaleID], [ProductID], [Qty], [Price]) VALUES (N'455161654 ', N'Pr1147    ', 1, 40.0000)
INSERT [dbo].[SALES_DTL] ([SaleID], [ProductID], [Qty], [Price]) VALUES (N'464058276 ', N'Pr1130    ', 1, 180.0000)
INSERT [dbo].[SALES_DTL] ([SaleID], [ProductID], [Qty], [Price]) VALUES (N'464058276 ', N'Pr1149    ', 1, 100.0000)
INSERT [dbo].[SALES_DTL] ([SaleID], [ProductID], [Qty], [Price]) VALUES (N'464058276 ', N'Pr1170    ', 1, 65.0000)
INSERT [dbo].[SALES_DTL] ([SaleID], [ProductID], [Qty], [Price]) VALUES (N'467420776 ', N'Pr1109    ', 1, 410.0000)
INSERT [dbo].[SALES_DTL] ([SaleID], [ProductID], [Qty], [Price]) VALUES (N'467420776 ', N'Pr1151    ', 1, 13.0000)
GO
INSERT [dbo].[SALES_DTL] ([SaleID], [ProductID], [Qty], [Price]) VALUES (N'471565888 ', N'Pr1160    ', 1, 35.0000)
INSERT [dbo].[SALES_DTL] ([SaleID], [ProductID], [Qty], [Price]) VALUES (N'483636551 ', N'Pr1129    ', 1, 25.0000)
INSERT [dbo].[SALES_DTL] ([SaleID], [ProductID], [Qty], [Price]) VALUES (N'483636551 ', N'Pr1149    ', 1, 100.0000)
INSERT [dbo].[SALES_DTL] ([SaleID], [ProductID], [Qty], [Price]) VALUES (N'483636551 ', N'Pr1160    ', 1, 35.0000)
INSERT [dbo].[SALES_DTL] ([SaleID], [ProductID], [Qty], [Price]) VALUES (N'511652873 ', N'Pr1129    ', 1, 25.0000)
INSERT [dbo].[SALES_DTL] ([SaleID], [ProductID], [Qty], [Price]) VALUES (N'511652873 ', N'Pr1147    ', 1, 40.0000)
INSERT [dbo].[SALES_DTL] ([SaleID], [ProductID], [Qty], [Price]) VALUES (N'511652873 ', N'Pr1149    ', 1, 100.0000)
INSERT [dbo].[SALES_DTL] ([SaleID], [ProductID], [Qty], [Price]) VALUES (N'511652873 ', N'Pr1160    ', 1, 35.0000)
INSERT [dbo].[SALES_DTL] ([SaleID], [ProductID], [Qty], [Price]) VALUES (N'516516516 ', N'Pr1129    ', 1, 25.0000)
INSERT [dbo].[SALES_DTL] ([SaleID], [ProductID], [Qty], [Price]) VALUES (N'516516516 ', N'Pr1147    ', 1, 25.0000)
INSERT [dbo].[SALES_DTL] ([SaleID], [ProductID], [Qty], [Price]) VALUES (N'516516516 ', N'Pr1148    ', 1, 25.0000)
INSERT [dbo].[SALES_DTL] ([SaleID], [ProductID], [Qty], [Price]) VALUES (N'516516516 ', N'Pr1160    ', 1, 25.0000)
INSERT [dbo].[SALES_DTL] ([SaleID], [ProductID], [Qty], [Price]) VALUES (N'525623472 ', N'Pr1129    ', 1, 25.0000)
INSERT [dbo].[SALES_DTL] ([SaleID], [ProductID], [Qty], [Price]) VALUES (N'525763663 ', N'Pr1128    ', 1, 400.0000)
INSERT [dbo].[SALES_DTL] ([SaleID], [ProductID], [Qty], [Price]) VALUES (N'543446466 ', N'Pr1105    ', 1, 350.0000)
INSERT [dbo].[SALES_DTL] ([SaleID], [ProductID], [Qty], [Price]) VALUES (N'543446466 ', N'Pr1136    ', 1, 35.0000)
INSERT [dbo].[SALES_DTL] ([SaleID], [ProductID], [Qty], [Price]) VALUES (N'547225656 ', N'Pr1128    ', 1, 400.0000)
INSERT [dbo].[SALES_DTL] ([SaleID], [ProductID], [Qty], [Price]) VALUES (N'547225656 ', N'Pr1160    ', 1, 35.0000)
INSERT [dbo].[SALES_DTL] ([SaleID], [ProductID], [Qty], [Price]) VALUES (N'547225656 ', N'Pr1164    ', 1, 85.0000)
INSERT [dbo].[SALES_DTL] ([SaleID], [ProductID], [Qty], [Price]) VALUES (N'555555555 ', N'Pr1102    ', 1, 300.0000)
INSERT [dbo].[SALES_DTL] ([SaleID], [ProductID], [Qty], [Price]) VALUES (N'561520075 ', N'Pr1160    ', 1, 35.0000)
INSERT [dbo].[SALES_DTL] ([SaleID], [ProductID], [Qty], [Price]) VALUES (N'561520075 ', N'Pr1163    ', 1, 60.0000)
INSERT [dbo].[SALES_DTL] ([SaleID], [ProductID], [Qty], [Price]) VALUES (N'561561561 ', N'Pr1129    ', 1, 25.0000)
INSERT [dbo].[SALES_DTL] ([SaleID], [ProductID], [Qty], [Price]) VALUES (N'561561561 ', N'Pr1147    ', 1, 40.0000)
INSERT [dbo].[SALES_DTL] ([SaleID], [ProductID], [Qty], [Price]) VALUES (N'561561561 ', N'Pr1160    ', 1, 35.0000)
INSERT [dbo].[SALES_DTL] ([SaleID], [ProductID], [Qty], [Price]) VALUES (N'564022384 ', N'Pr1129    ', 1, 25.0000)
INSERT [dbo].[SALES_DTL] ([SaleID], [ProductID], [Qty], [Price]) VALUES (N'564022384 ', N'Pr1147    ', 1, 40.0000)
INSERT [dbo].[SALES_DTL] ([SaleID], [ProductID], [Qty], [Price]) VALUES (N'564022384 ', N'Pr1148    ', 1, 100.0000)
INSERT [dbo].[SALES_DTL] ([SaleID], [ProductID], [Qty], [Price]) VALUES (N'564022384 ', N'Pr1170    ', 1, 65.0000)
INSERT [dbo].[SALES_DTL] ([SaleID], [ProductID], [Qty], [Price]) VALUES (N'564964895 ', N'Pr1123    ', 1, 180.0000)
INSERT [dbo].[SALES_DTL] ([SaleID], [ProductID], [Qty], [Price]) VALUES (N'565073505 ', N'Pr1101    ', 1, 350.0000)
INSERT [dbo].[SALES_DTL] ([SaleID], [ProductID], [Qty], [Price]) VALUES (N'565073505 ', N'Pr1151    ', 1, 13.0000)
INSERT [dbo].[SALES_DTL] ([SaleID], [ProductID], [Qty], [Price]) VALUES (N'568466551 ', N'Pr1129    ', 1, 25.0000)
INSERT [dbo].[SALES_DTL] ([SaleID], [ProductID], [Qty], [Price]) VALUES (N'568466551 ', N'Pr1133    ', 1, 85.0000)
INSERT [dbo].[SALES_DTL] ([SaleID], [ProductID], [Qty], [Price]) VALUES (N'568466551 ', N'Pr1146    ', 1, 270.0000)
INSERT [dbo].[SALES_DTL] ([SaleID], [ProductID], [Qty], [Price]) VALUES (N'568466551 ', N'Pr1148    ', 1, 100.0000)
INSERT [dbo].[SALES_DTL] ([SaleID], [ProductID], [Qty], [Price]) VALUES (N'568466551 ', N'Pr1160    ', 1, 35.0000)
INSERT [dbo].[SALES_DTL] ([SaleID], [ProductID], [Qty], [Price]) VALUES (N'574784585 ', N'Pr1129    ', 1, 25.0000)
INSERT [dbo].[SALES_DTL] ([SaleID], [ProductID], [Qty], [Price]) VALUES (N'574784585 ', N'Pr1147    ', 1, 40.0000)
INSERT [dbo].[SALES_DTL] ([SaleID], [ProductID], [Qty], [Price]) VALUES (N'574784585 ', N'Pr1160    ', 1, 35.0000)
INSERT [dbo].[SALES_DTL] ([SaleID], [ProductID], [Qty], [Price]) VALUES (N'574784585 ', N'Pr1169    ', 1, 250.0000)
INSERT [dbo].[SALES_DTL] ([SaleID], [ProductID], [Qty], [Price]) VALUES (N'574784585 ', N'Pr1173    ', 1, 165.0000)
INSERT [dbo].[SALES_DTL] ([SaleID], [ProductID], [Qty], [Price]) VALUES (N'616645615 ', N'Pr1159    ', 1, 80.0000)
INSERT [dbo].[SALES_DTL] ([SaleID], [ProductID], [Qty], [Price]) VALUES (N'622473016 ', N'Pr1129    ', 1, 25.0000)
INSERT [dbo].[SALES_DTL] ([SaleID], [ProductID], [Qty], [Price]) VALUES (N'622473016 ', N'Pr1149    ', 2, 100.0000)
INSERT [dbo].[SALES_DTL] ([SaleID], [ProductID], [Qty], [Price]) VALUES (N'622473016 ', N'Pr1160    ', 1, 35.0000)
INSERT [dbo].[SALES_DTL] ([SaleID], [ProductID], [Qty], [Price]) VALUES (N'622473016 ', N'Pr1170    ', 1, 65.0000)
INSERT [dbo].[SALES_DTL] ([SaleID], [ProductID], [Qty], [Price]) VALUES (N'632574028 ', N'Pr1129    ', 1, 25.0000)
INSERT [dbo].[SALES_DTL] ([SaleID], [ProductID], [Qty], [Price]) VALUES (N'632574028 ', N'Pr1160    ', 1, 35.0000)
INSERT [dbo].[SALES_DTL] ([SaleID], [ProductID], [Qty], [Price]) VALUES (N'645546546 ', N'Pr1160    ', 1, 35.0000)
INSERT [dbo].[SALES_DTL] ([SaleID], [ProductID], [Qty], [Price]) VALUES (N'645646343 ', N'Pr1103    ', 1, 320.0000)
INSERT [dbo].[SALES_DTL] ([SaleID], [ProductID], [Qty], [Price]) VALUES (N'645646565 ', N'Pr1121    ', 1, 300.0000)
INSERT [dbo].[SALES_DTL] ([SaleID], [ProductID], [Qty], [Price]) VALUES (N'645646565 ', N'Pr1164    ', 1, 85.0000)
INSERT [dbo].[SALES_DTL] ([SaleID], [ProductID], [Qty], [Price]) VALUES (N'651651655 ', N'Pr1112    ', 1, 310.0000)
INSERT [dbo].[SALES_DTL] ([SaleID], [ProductID], [Qty], [Price]) VALUES (N'653420332 ', N'Pr1129    ', 1, 25.0000)
INSERT [dbo].[SALES_DTL] ([SaleID], [ProductID], [Qty], [Price]) VALUES (N'653420332 ', N'Pr1160    ', 1, 35.0000)
INSERT [dbo].[SALES_DTL] ([SaleID], [ProductID], [Qty], [Price]) VALUES (N'654456645 ', N'Pr1129    ', 1, 25.0000)
INSERT [dbo].[SALES_DTL] ([SaleID], [ProductID], [Qty], [Price]) VALUES (N'656443183 ', N'Pr1101    ', 1, 350.0000)
INSERT [dbo].[SALES_DTL] ([SaleID], [ProductID], [Qty], [Price]) VALUES (N'666666666 ', N'Pr1129    ', 1, 25.0000)
INSERT [dbo].[SALES_DTL] ([SaleID], [ProductID], [Qty], [Price]) VALUES (N'666666666 ', N'Pr1149    ', 1, 100.0000)
INSERT [dbo].[SALES_DTL] ([SaleID], [ProductID], [Qty], [Price]) VALUES (N'666666666 ', N'Pr1170    ', 1, 65.0000)
INSERT [dbo].[SALES_DTL] ([SaleID], [ProductID], [Qty], [Price]) VALUES (N'670383801 ', N'Pr1129    ', 1, 25.0000)
INSERT [dbo].[SALES_DTL] ([SaleID], [ProductID], [Qty], [Price]) VALUES (N'670383801 ', N'Pr1147    ', 1, 40.0000)
INSERT [dbo].[SALES_DTL] ([SaleID], [ProductID], [Qty], [Price]) VALUES (N'670383801 ', N'Pr1157    ', 1, 30.0000)
INSERT [dbo].[SALES_DTL] ([SaleID], [ProductID], [Qty], [Price]) VALUES (N'670383801 ', N'Pr1160    ', 1, 35.0000)
INSERT [dbo].[SALES_DTL] ([SaleID], [ProductID], [Qty], [Price]) VALUES (N'677752606 ', N'Pr1129    ', 1, 25.0000)
INSERT [dbo].[SALES_DTL] ([SaleID], [ProductID], [Qty], [Price]) VALUES (N'714132047 ', N'Pr1148    ', 1, 100.0000)
INSERT [dbo].[SALES_DTL] ([SaleID], [ProductID], [Qty], [Price]) VALUES (N'714132047 ', N'Pr1160    ', 1, 35.0000)
INSERT [dbo].[SALES_DTL] ([SaleID], [ProductID], [Qty], [Price]) VALUES (N'737407647 ', N'Pr1129    ', 1, 25.0000)
INSERT [dbo].[SALES_DTL] ([SaleID], [ProductID], [Qty], [Price]) VALUES (N'737407647 ', N'Pr1149    ', 1, 100.0000)
INSERT [dbo].[SALES_DTL] ([SaleID], [ProductID], [Qty], [Price]) VALUES (N'737407647 ', N'Pr1160    ', 1, 35.0000)
INSERT [dbo].[SALES_DTL] ([SaleID], [ProductID], [Qty], [Price]) VALUES (N'760348252 ', N'Pr1155    ', 1, 200.0000)
INSERT [dbo].[SALES_DTL] ([SaleID], [ProductID], [Qty], [Price]) VALUES (N'760348252 ', N'Pr1164    ', 1, 85.0000)
INSERT [dbo].[SALES_DTL] ([SaleID], [ProductID], [Qty], [Price]) VALUES (N'776107376 ', N'Pr1149    ', 1, 100.0000)
INSERT [dbo].[SALES_DTL] ([SaleID], [ProductID], [Qty], [Price]) VALUES (N'776107376 ', N'Pr1160    ', 1, 35.0000)
INSERT [dbo].[SALES_DTL] ([SaleID], [ProductID], [Qty], [Price]) VALUES (N'786223434 ', N'Pr1129    ', 1, 25.0000)
INSERT [dbo].[SALES_DTL] ([SaleID], [ProductID], [Qty], [Price]) VALUES (N'844052626 ', N'Pr1118    ', 1, 420.0000)
INSERT [dbo].[SALES_DTL] ([SaleID], [ProductID], [Qty], [Price]) VALUES (N'844052626 ', N'Pr1164    ', 2, 85.0000)
INSERT [dbo].[SALES_DTL] ([SaleID], [ProductID], [Qty], [Price]) VALUES (N'860870858 ', N'Pr1155    ', 1, 200.0000)
INSERT [dbo].[SALES_DTL] ([SaleID], [ProductID], [Qty], [Price]) VALUES (N'868830621 ', N'Pr1129    ', 1, 25.0000)
INSERT [dbo].[SALES_DTL] ([SaleID], [ProductID], [Qty], [Price]) VALUES (N'873657564 ', N'Pr1129    ', 1, 25.0000)
INSERT [dbo].[SALES_DTL] ([SaleID], [ProductID], [Qty], [Price]) VALUES (N'873657564 ', N'Pr1160    ', 1, 35.0000)
INSERT [dbo].[SALES_DTL] ([SaleID], [ProductID], [Qty], [Price]) VALUES (N'873657564 ', N'Pr1169    ', 1, 250.0000)
INSERT [dbo].[SALES_DTL] ([SaleID], [ProductID], [Qty], [Price]) VALUES (N'877884443 ', N'Pr1132    ', 1, 170.0000)
INSERT [dbo].[SALES_DTL] ([SaleID], [ProductID], [Qty], [Price]) VALUES (N'877884443 ', N'Pr1160    ', 1, 35.0000)
INSERT [dbo].[SALES_DTL] ([SaleID], [ProductID], [Qty], [Price]) VALUES (N'885570402 ', N'Pr1129    ', 1, 25.0000)
INSERT [dbo].[SALES_DTL] ([SaleID], [ProductID], [Qty], [Price]) VALUES (N'885570402 ', N'Pr1160    ', 1, 35.0000)
INSERT [dbo].[SERVICE] ([ServiceID], [NoOfSlots], [Type(A/N/B)]) VALUES (N'Pr1101    ', 8, N'B')
INSERT [dbo].[SERVICE] ([ServiceID], [NoOfSlots], [Type(A/N/B)]) VALUES (N'Pr1102    ', 7, N'B')
INSERT [dbo].[SERVICE] ([ServiceID], [NoOfSlots], [Type(A/N/B)]) VALUES (N'Pr1103    ', 5, N'B')
INSERT [dbo].[SERVICE] ([ServiceID], [NoOfSlots], [Type(A/N/B)]) VALUES (N'Pr1104    ', 10, N'B')
INSERT [dbo].[SERVICE] ([ServiceID], [NoOfSlots], [Type(A/N/B)]) VALUES (N'Pr1105    ', 9, N'B')
INSERT [dbo].[SERVICE] ([ServiceID], [NoOfSlots], [Type(A/N/B)]) VALUES (N'Pr1106    ', 7, N'B')
INSERT [dbo].[SERVICE] ([ServiceID], [NoOfSlots], [Type(A/N/B)]) VALUES (N'Pr1107    ', 14, N'B')
INSERT [dbo].[SERVICE] ([ServiceID], [NoOfSlots], [Type(A/N/B)]) VALUES (N'Pr1108    ', 12, N'B')
INSERT [dbo].[SERVICE] ([ServiceID], [NoOfSlots], [Type(A/N/B)]) VALUES (N'Pr1109    ', 10, N'B')
INSERT [dbo].[SERVICE] ([ServiceID], [NoOfSlots], [Type(A/N/B)]) VALUES (N'Pr1110    ', 7, N'B')
INSERT [dbo].[SERVICE] ([ServiceID], [NoOfSlots], [Type(A/N/B)]) VALUES (N'Pr1111    ', 6, N'B')
INSERT [dbo].[SERVICE] ([ServiceID], [NoOfSlots], [Type(A/N/B)]) VALUES (N'Pr1112    ', 5, N'B')
INSERT [dbo].[SERVICE] ([ServiceID], [NoOfSlots], [Type(A/N/B)]) VALUES (N'Pr1113    ', 9, N'B')
INSERT [dbo].[SERVICE] ([ServiceID], [NoOfSlots], [Type(A/N/B)]) VALUES (N'Pr1114    ', 8, N'B')
INSERT [dbo].[SERVICE] ([ServiceID], [NoOfSlots], [Type(A/N/B)]) VALUES (N'Pr1115    ', 7, N'B')
INSERT [dbo].[SERVICE] ([ServiceID], [NoOfSlots], [Type(A/N/B)]) VALUES (N'Pr1116    ', 13, N'B')
INSERT [dbo].[SERVICE] ([ServiceID], [NoOfSlots], [Type(A/N/B)]) VALUES (N'Pr1117    ', 11, N'B')
INSERT [dbo].[SERVICE] ([ServiceID], [NoOfSlots], [Type(A/N/B)]) VALUES (N'Pr1118    ', 10, N'B')
INSERT [dbo].[SERVICE] ([ServiceID], [NoOfSlots], [Type(A/N/B)]) VALUES (N'Pr1119    ', 3, N'B')
INSERT [dbo].[SERVICE] ([ServiceID], [NoOfSlots], [Type(A/N/B)]) VALUES (N'Pr1120    ', 4, N'B')
INSERT [dbo].[SERVICE] ([ServiceID], [NoOfSlots], [Type(A/N/B)]) VALUES (N'Pr1121    ', 5, N'B')
INSERT [dbo].[SERVICE] ([ServiceID], [NoOfSlots], [Type(A/N/B)]) VALUES (N'Pr1122    ', 3, N'B')
INSERT [dbo].[SERVICE] ([ServiceID], [NoOfSlots], [Type(A/N/B)]) VALUES (N'Pr1123    ', 4, N'B')
INSERT [dbo].[SERVICE] ([ServiceID], [NoOfSlots], [Type(A/N/B)]) VALUES (N'Pr1124    ', 4, N'B')
INSERT [dbo].[SERVICE] ([ServiceID], [NoOfSlots], [Type(A/N/B)]) VALUES (N'Pr1125    ', 5, N'B')
INSERT [dbo].[SERVICE] ([ServiceID], [NoOfSlots], [Type(A/N/B)]) VALUES (N'Pr1126    ', 3, N'B')
INSERT [dbo].[SERVICE] ([ServiceID], [NoOfSlots], [Type(A/N/B)]) VALUES (N'Pr1127    ', 4, N'B')
INSERT [dbo].[SERVICE] ([ServiceID], [NoOfSlots], [Type(A/N/B)]) VALUES (N'Pr1128    ', 11, N'A')
INSERT [dbo].[SERVICE] ([ServiceID], [NoOfSlots], [Type(A/N/B)]) VALUES (N'Pr1129    ', 1, N'N')
INSERT [dbo].[SERVICE] ([ServiceID], [NoOfSlots], [Type(A/N/B)]) VALUES (N'Pr1147    ', 3, N'N')
INSERT [dbo].[SERVICE] ([ServiceID], [NoOfSlots], [Type(A/N/B)]) VALUES (N'Pr1148    ', 3, N'N')
INSERT [dbo].[SERVICE] ([ServiceID], [NoOfSlots], [Type(A/N/B)]) VALUES (N'Pr1154    ', 5, N'A')
INSERT [dbo].[SERVICE] ([ServiceID], [NoOfSlots], [Type(A/N/B)]) VALUES (N'Pr1155    ', 4, N'A')
INSERT [dbo].[SERVICE] ([ServiceID], [NoOfSlots], [Type(A/N/B)]) VALUES (N'Pr1160    ', 1, N'N')
INSERT [dbo].[SERVICE] ([ServiceID], [NoOfSlots], [Type(A/N/B)]) VALUES (N'Pr1175    ', 10, N'U')
INSERT [dbo].[SERVICE] ([ServiceID], [NoOfSlots], [Type(A/N/B)]) VALUES (N'Pr1176    ', 10, N'U')
INSERT [dbo].[SERVICE] ([ServiceID], [NoOfSlots], [Type(A/N/B)]) VALUES (N'Pr1177    ', 20, N'U')
INSERT [dbo].[SERVICE] ([ServiceID], [NoOfSlots], [Type(A/N/B)]) VALUES (N'Pr1178    ', 20, N'U')
INSERT [dbo].[Stock_Management] ([LowStock], [PurchaseQty], [AutoPurchase], [AutoPurchaseFrequency], [AutoPurchaseProducts], [BusinessID], [NxtOrderDate]) VALUES (6, 5, 1, N'Ewe', 0, N'001       ', CAST(N'2018-10-23T15:56:39.083' AS DateTime))
INSERT [dbo].[STYLE] ([StyleID], [Description]) VALUES (N'S1001     ', N'Plait')
INSERT [dbo].[STYLE] ([StyleID], [Description]) VALUES (N'S1002     ', N'Twist')
INSERT [dbo].[STYLE] ([StyleID], [Description]) VALUES (N'S1003     ', N'Straight-back cornrows')
INSERT [dbo].[STYLE] ([StyleID], [Description]) VALUES (N'S1004     ', N'Upstyle cornrows')
INSERT [dbo].[STYLIST_SERVICE] ([EmployeeID], [ServiceID]) VALUES (N'105450503348172771121         ', N'Pr1118    ')
INSERT [dbo].[STYLIST_SERVICE] ([EmployeeID], [ServiceID]) VALUES (N'118233419479102946333         ', N'Pr1129    ')
INSERT [dbo].[STYLIST_SERVICE] ([EmployeeID], [ServiceID]) VALUES (N'270146203840160705048         ', N'Pr1160    ')
INSERT [dbo].[STYLIST_SERVICE] ([EmployeeID], [ServiceID]) VALUES (N'580312424824681240311         ', N'Pr1103    ')
INSERT [dbo].[Supplier] ([SupplierID], [SupplierName], [ContactName], [ContactNo], [AddressLine1], [AddressLine2], [Suburb], [City], [ContactEmail]) VALUES (N'001       ', N'Hair Supplies International                       ', N'Pauly D                                           ', N'0102345678', N'2 Harold Flight Rd                                                                                  ', NULL, N'Jet Park                                                                                            ', N'Boksburg                                                                                            ', N'paulyd@HSI.co.za')
INSERT [dbo].[Supplier] ([SupplierID], [SupplierName], [ContactName], [ContactNo], [AddressLine1], [AddressLine2], [Suburb], [City], [ContactEmail]) VALUES (N'075158833 ', N'Hair Supplies Africa                              ', N'Alina Baraz                                       ', N'0211234569', N'2                                                                                                   ', N'Fir Street                                                                                          ', N'Observatory                                                                                         ', N'Cape Town                                                                                           ', N'a.baraz@hsa.co.za')
INSERT [dbo].[TIMESLOT] ([SlotNo], [StartTime], [EndTime]) VALUES (N'Slo1      ', CAST(N'08:00:00' AS Time), CAST(N'08:30:00' AS Time))
INSERT [dbo].[TIMESLOT] ([SlotNo], [StartTime], [EndTime]) VALUES (N'Slo10     ', CAST(N'12:30:00' AS Time), CAST(N'13:00:00' AS Time))
INSERT [dbo].[TIMESLOT] ([SlotNo], [StartTime], [EndTime]) VALUES (N'Slo11     ', CAST(N'13:00:00' AS Time), CAST(N'13:30:00' AS Time))
INSERT [dbo].[TIMESLOT] ([SlotNo], [StartTime], [EndTime]) VALUES (N'Slo12     ', CAST(N'13:30:00' AS Time), CAST(N'14:00:00' AS Time))
INSERT [dbo].[TIMESLOT] ([SlotNo], [StartTime], [EndTime]) VALUES (N'Slo13     ', CAST(N'14:00:00' AS Time), CAST(N'14:30:00' AS Time))
INSERT [dbo].[TIMESLOT] ([SlotNo], [StartTime], [EndTime]) VALUES (N'Slo14     ', CAST(N'14:30:00' AS Time), CAST(N'15:00:00' AS Time))
INSERT [dbo].[TIMESLOT] ([SlotNo], [StartTime], [EndTime]) VALUES (N'Slo15     ', CAST(N'15:00:00' AS Time), CAST(N'15:30:00' AS Time))
INSERT [dbo].[TIMESLOT] ([SlotNo], [StartTime], [EndTime]) VALUES (N'Slo16     ', CAST(N'15:30:00' AS Time), CAST(N'16:00:00' AS Time))
INSERT [dbo].[TIMESLOT] ([SlotNo], [StartTime], [EndTime]) VALUES (N'Slo17     ', CAST(N'16:00:00' AS Time), CAST(N'16:30:00' AS Time))
INSERT [dbo].[TIMESLOT] ([SlotNo], [StartTime], [EndTime]) VALUES (N'Slo18     ', CAST(N'16:30:00' AS Time), CAST(N'17:00:00' AS Time))
INSERT [dbo].[TIMESLOT] ([SlotNo], [StartTime], [EndTime]) VALUES (N'Slo19     ', CAST(N'17:00:00' AS Time), CAST(N'17:30:00' AS Time))
INSERT [dbo].[TIMESLOT] ([SlotNo], [StartTime], [EndTime]) VALUES (N'Slo2      ', CAST(N'08:30:00' AS Time), CAST(N'09:00:00' AS Time))
INSERT [dbo].[TIMESLOT] ([SlotNo], [StartTime], [EndTime]) VALUES (N'Slo20     ', CAST(N'17:30:00' AS Time), CAST(N'18:00:00' AS Time))
INSERT [dbo].[TIMESLOT] ([SlotNo], [StartTime], [EndTime]) VALUES (N'Slo3      ', CAST(N'09:00:00' AS Time), CAST(N'09:30:00' AS Time))
INSERT [dbo].[TIMESLOT] ([SlotNo], [StartTime], [EndTime]) VALUES (N'Slo4      ', CAST(N'09:30:00' AS Time), CAST(N'10:00:00' AS Time))
INSERT [dbo].[TIMESLOT] ([SlotNo], [StartTime], [EndTime]) VALUES (N'Slo5      ', CAST(N'10:00:00' AS Time), CAST(N'10:30:00' AS Time))
INSERT [dbo].[TIMESLOT] ([SlotNo], [StartTime], [EndTime]) VALUES (N'Slo6      ', CAST(N'10:30:00' AS Time), CAST(N'11:00:00' AS Time))
INSERT [dbo].[TIMESLOT] ([SlotNo], [StartTime], [EndTime]) VALUES (N'Slo7      ', CAST(N'11:00:00' AS Time), CAST(N'11:30:00' AS Time))
INSERT [dbo].[TIMESLOT] ([SlotNo], [StartTime], [EndTime]) VALUES (N'Slo8      ', CAST(N'11:30:00' AS Time), CAST(N'12:00:00' AS Time))
INSERT [dbo].[TIMESLOT] ([SlotNo], [StartTime], [EndTime]) VALUES (N'Slo9      ', CAST(N'12:00:00' AS Time), CAST(N'12:30:00' AS Time))
INSERT [dbo].[TREATMENT] ([TreatmentID], [Qty], [TreatmentType], [BrandID], [SupplierID]) VALUES (N'Pr1130    ', 8, N'Oil', N'B03       ', N'001       ')
INSERT [dbo].[TREATMENT] ([TreatmentID], [Qty], [TreatmentType], [BrandID], [SupplierID]) VALUES (N'Pr1131    ', 8, N'Styling cream', N'B04       ', N'001       ')
INSERT [dbo].[TREATMENT] ([TreatmentID], [Qty], [TreatmentType], [BrandID], [SupplierID]) VALUES (N'Pr1132    ', 5, N'Shampoo', N'B01       ', N'001       ')
INSERT [dbo].[TREATMENT] ([TreatmentID], [Qty], [TreatmentType], [BrandID], [SupplierID]) VALUES (N'Pr1133    ', 8, N'Styling cream', N'B09       ', N'001       ')
INSERT [dbo].[TREATMENT] ([TreatmentID], [Qty], [TreatmentType], [BrandID], [SupplierID]) VALUES (N'Pr1134    ', 8, N'Styling gel', N'B04       ', N'001       ')
INSERT [dbo].[TREATMENT] ([TreatmentID], [Qty], [TreatmentType], [BrandID], [SupplierID]) VALUES (N'Pr1139    ', 7, N'Relaxer', N'B01       ', N'001       ')
INSERT [dbo].[TREATMENT] ([TreatmentID], [Qty], [TreatmentType], [BrandID], [SupplierID]) VALUES (N'Pr1141    ', 6, N'Styling cream', N'B09       ', N'001       ')
INSERT [dbo].[TREATMENT] ([TreatmentID], [Qty], [TreatmentType], [BrandID], [SupplierID]) VALUES (N'Pr1142    ', 8, N'Styling cream', N'B06       ', N'001       ')
INSERT [dbo].[TREATMENT] ([TreatmentID], [Qty], [TreatmentType], [BrandID], [SupplierID]) VALUES (N'Pr1143    ', 9, N'Deep conditioner', N'B01       ', N'001       ')
INSERT [dbo].[TREATMENT] ([TreatmentID], [Qty], [TreatmentType], [BrandID], [SupplierID]) VALUES (N'Pr1146    ', 8, N'Styling cream', N'B08       ', N'001       ')
INSERT [dbo].[TREATMENT] ([TreatmentID], [Qty], [TreatmentType], [BrandID], [SupplierID]) VALUES (N'Pr1149    ', 6, N'Shampoo', N'B06       ', N'001       ')
INSERT [dbo].[TREATMENT] ([TreatmentID], [Qty], [TreatmentType], [BrandID], [SupplierID]) VALUES (N'Pr1150    ', 8, N'Hair food', N'B14       ', N'001       ')
INSERT [dbo].[TREATMENT] ([TreatmentID], [Qty], [TreatmentType], [BrandID], [SupplierID]) VALUES (N'Pr1157    ', 7, N'Hair dye', N'B11       ', N'001       ')
INSERT [dbo].[TREATMENT] ([TreatmentID], [Qty], [TreatmentType], [BrandID], [SupplierID]) VALUES (N'Pr1158    ', 8, N'Oil', N'B03       ', N'001       ')
INSERT [dbo].[TREATMENT] ([TreatmentID], [Qty], [TreatmentType], [BrandID], [SupplierID]) VALUES (N'Pr1159    ', 7, N'Styling gel', N'B12       ', N'001       ')
INSERT [dbo].[TREATMENT] ([TreatmentID], [Qty], [TreatmentType], [BrandID], [SupplierID]) VALUES (N'Pr1163    ', 10, N'Shampoo', N'B12       ', N'001       ')
INSERT [dbo].[TREATMENT] ([TreatmentID], [Qty], [TreatmentType], [BrandID], [SupplierID]) VALUES (N'Pr1169    ', 7, N'Shampoo', N'B08       ', N'001       ')
INSERT [dbo].[TREATMENT] ([TreatmentID], [Qty], [TreatmentType], [BrandID], [SupplierID]) VALUES (N'Pr1170    ', 6, N'Deep conditioner', N'B06       ', N'001       ')
INSERT [dbo].[TREATMENT] ([TreatmentID], [Qty], [TreatmentType], [BrandID], [SupplierID]) VALUES (N'Pr1171    ', 8, N'Co-Wash', N'B04       ', N'001       ')
INSERT [dbo].[TREATMENT] ([TreatmentID], [Qty], [TreatmentType], [BrandID], [SupplierID]) VALUES (N'Pr1173    ', 6, N'Leave-In conditioner', N'B01       ', N'001       ')
INSERT [dbo].[USER] ([UserID], [FirstName], [LastName], [UserName], [Email], [ContactNo], [Password], [UserType], [Active], [UserImage], [AccountType], [PreferredCommunication], [PassRestCode]) VALUES (N'0                             ', N'None', NULL, N'N/A', N'N/A', N'N/A       ', N'N/A', N'C', N'F', N'N/A', N'N/A       ', NULL, NULL)
INSERT [dbo].[USER] ([UserID], [FirstName], [LastName], [UserName], [Email], [ContactNo], [Password], [UserType], [Active], [UserImage], [AccountType], [PreferredCommunication], [PassRestCode]) VALUES (N'100155997350724101850         ', N'Tefo', N'Hashatse', N'thashatse                     ', N'thashatse@gmail.com', N'0784608876', NULL, N'C', N'T', N'https://lh6.googleusercontent.com/-_TWyUJ4u9w0/AAAAAAAAAAI/AAAAAAAAA0I/5QBlLD6338g/s96-c/photo.jpg', N'Google    ', N'E', NULL)
INSERT [dbo].[USER] ([UserID], [FirstName], [LastName], [UserName], [Email], [ContactNo], [Password], [UserType], [Active], [UserImage], [AccountType], [PreferredCommunication], [PassRestCode]) VALUES (N'105242998585655922697         ', N'Harrison', N'Ford', N'Harrison1                     ', N'customercheveux@gmail.com', N'0784608877', NULL, N'C', N'T', N'https://lh4.googleusercontent.com/-yKpO8v1GOkk/AAAAAAAAAAI/AAAAAAAAAAA/AAnnY7pdWKxf6Isu9qjB2c0kJkbkARWDJw/s96-c/photo.jpg', N'Google    ', N'E', NULL)
INSERT [dbo].[USER] ([UserID], [FirstName], [LastName], [UserName], [Email], [ContactNo], [Password], [UserType], [Active], [UserImage], [AccountType], [PreferredCommunication], [PassRestCode]) VALUES (N'105450503348172771121         ', N'Ed', N'Styles', N'EdStyles', N'edstyles@mailwave.com', N'0601234568', N'$2a$06$A3oCnHkWFXl6DHcaCzB2I.EtblTyJRB6FfeEvliNbDTz3hb/DC6yO', N'E', N'T', N'https://upload.wikimedia.org/wikipedia/commons/3/34/PICA.jpg', N'Email     ', N'E', NULL)
INSERT [dbo].[USER] ([UserID], [FirstName], [LastName], [UserName], [Email], [ContactNo], [Password], [UserType], [Active], [UserImage], [AccountType], [PreferredCommunication], [PassRestCode]) VALUES (N'112413834414360855751         ', N'Wendy', N'Williams', N'WendyW', N'receptionistcheveux@gmail.com', NULL, NULL, N'E', N'T', N'https://lh4.googleusercontent.com/-3PrjKQq46zI/AAAAAAAAAAI/AAAAAAAAAAA/AAnnY7oEUk-P-VLZWB0osJsxRpnvJMlLRQ/s96-c/photo.jpg', N'Google    ', N'E', NULL)
INSERT [dbo].[USER] ([UserID], [FirstName], [LastName], [UserName], [Email], [ContactNo], [Password], [UserType], [Active], [UserImage], [AccountType], [PreferredCommunication], [PassRestCode]) VALUES (N'112475171777167063459         ', N'Oliver', N'James', N'JamesO                        ', N'managercheveux@gmail.com', N'0412345678', NULL, N'E', N'T', N'https://lh5.googleusercontent.com/-ypchYBYV0nQ/AAAAAAAAAAI/AAAAAAAAAAA/AAnnY7oSgpuGdget6aps1JaKK9No2OsoZQ/s96-c/photo.jpg', N'Google    ', N'E', NULL)
INSERT [dbo].[USER] ([UserID], [FirstName], [LastName], [UserName], [Email], [ContactNo], [Password], [UserType], [Active], [UserImage], [AccountType], [PreferredCommunication], [PassRestCode]) VALUES (N'118233419479102946333         ', N'Jayden', N'Styles', N'Styles1                       ', N'stylistcheveux2@gmail.com', N'0784605555', NULL, N'E', N'T', N'http://sict-iis.nmmu.ac.za/beauxdebut/Theam/img/118233419479102946333.jpg', N'Google    ', N'E', NULL)
INSERT [dbo].[USER] ([UserID], [FirstName], [LastName], [UserName], [Email], [ContactNo], [Password], [UserType], [Active], [UserImage], [AccountType], [PreferredCommunication], [PassRestCode]) VALUES (N'138417072175782863757         ', N'test', N'emp', N'testemp', N'testemp@gmail.com', N'0784608876', N'$2a$06$MYxTgW9MVhRQYchspFm3COKa3ohuHVYoyhb6d6wtZ6yxLuPZN79ce', N'E', N'F', N'https://upload.wikimedia.org/wikipedia/commons/3/34/PICA.jpg', N'Email     ', N'E', NULL)
INSERT [dbo].[USER] ([UserID], [FirstName], [LastName], [UserName], [Email], [ContactNo], [Password], [UserType], [Active], [UserImage], [AccountType], [PreferredCommunication], [PassRestCode]) VALUES (N'140623085382221348147         ', N'Beyoncé Giselle', N'Knowles-Carter', N'QueenB', N'b@parkwoodent.com', N'          ', N'$2a$06$uWnSJzqxg/cswGvV6rQ0IurFolxLMmZLCt6FWF2ppfERc6bbIqf.O', N'C', N'T', N'https://upload.wikimedia.org/wikipedia/commons/3/34/PICA.jpg', N'Email     ', NULL, NULL)
INSERT [dbo].[USER] ([UserID], [FirstName], [LastName], [UserName], [Email], [ContactNo], [Password], [UserType], [Active], [UserImage], [AccountType], [PreferredCommunication], [PassRestCode]) VALUES (N'154052322058082242258         ', N'Anatii', N'Mnyango', N'Anatii', N'anatii@talent.co.za', N'          ', N'$2a$06$qWxNmDYMuV8Lx/gMjF4n5OGkm8y55NjwiYTNxO6kpTlCb5zwopN6a', N'C', N'T', N'https://upload.wikimedia.org/wikipedia/commons/3/34/PICA.jpg', N'Email     ', NULL, NULL)
INSERT [dbo].[USER] ([UserID], [FirstName], [LastName], [UserName], [Email], [ContactNo], [Password], [UserType], [Active], [UserImage], [AccountType], [PreferredCommunication], [PassRestCode]) VALUES (N'181477264752348604465         ', N'John', N'Paul', N'JPaul99                       ', N'johnpaul@gmail.com', N'0833252333', N'$2a$06$x/dh/eurjj3tY0EfK95OXuxbOjJAKhBgnmRA8oBEnA76cwUZI/Yb6', N'C', N'T', N'https://upload.wikimedia.org/wikipedia/commons/3/34/PICA.jpg', N'Email     ', N'E', NULL)
INSERT [dbo].[USER] ([UserID], [FirstName], [LastName], [UserName], [Email], [ContactNo], [Password], [UserType], [Active], [UserImage], [AccountType], [PreferredCommunication], [PassRestCode]) VALUES (N'215703134623237784686         ', N'Osei', N'Oyinlola', N'OseiOfficial', N'oseiofficial@webmail.com', N'0607894563', N'$2a$06$.hmyD0GqR2X5HfmviJyeEuM5ZtmZANVVP26CB5/u1L6A8oqjv.Z5a', N'C', N'T', N'https://upload.wikimedia.org/wikipedia/commons/3/34/PICA.jpg', N'Email     ', N'E', NULL)
INSERT [dbo].[USER] ([UserID], [FirstName], [LastName], [UserName], [Email], [ContactNo], [Password], [UserType], [Active], [UserImage], [AccountType], [PreferredCommunication], [PassRestCode]) VALUES (N'270146203840160705048         ', N'OBA', N'Okafor', N'ObaOfficial', N'obaofficial@webmail.com', N'0612345678', N'$2a$06$4TVvF54c1kmHVPHlI6cJwuc48fyZymmZarkfabRymvJDaoBTlJady', N'E', N'T', N'http://sict-iis.nmmu.ac.za/beauxdebut/Theam/img/270146203840160705048.jpg', N'Email     ', N'E', NULL)
INSERT [dbo].[USER] ([UserID], [FirstName], [LastName], [UserName], [Email], [ContactNo], [Password], [UserType], [Active], [UserImage], [AccountType], [PreferredCommunication], [PassRestCode]) VALUES (N'307776754475328666200         ', N'Quavious', N'Marshall', N'huncho', N'quavo@qualitycontrolmusic.com', N'          ', N'$2a$06$GhQuszxQn2lj6kmaYU0JxOpWF/h/9pOxovhRQfPMGmxcWYaENqwRS', N'C', N'T', N'https://upload.wikimedia.org/wikipedia/commons/3/34/PICA.jpg', N'Email     ', NULL, NULL)
INSERT [dbo].[USER] ([UserID], [FirstName], [LastName], [UserName], [Email], [ContactNo], [Password], [UserType], [Active], [UserImage], [AccountType], [PreferredCommunication], [PassRestCode]) VALUES (N'324465860528272078866         ', N'Aubrey Drake', N'Graham', N'Aubrey', N'aubrey@ovo.net', N'          ', N'$2a$06$fqoqaP0ikuJ708OdgQT6mOr8bHBQL8aSYejczrmNsaWgxhko5FfXW', N'C', N'T', N'https://upload.wikimedia.org/wikipedia/commons/3/34/PICA.jpg', N'Email     ', NULL, NULL)
INSERT [dbo].[USER] ([UserID], [FirstName], [LastName], [UserName], [Email], [ContactNo], [Password], [UserType], [Active], [UserImage], [AccountType], [PreferredCommunication], [PassRestCode]) VALUES (N'341104773302321231832         ', N'Mary', N'Joe', N'MaryJOfficial', N'maryjofficial@gmail.com', N'0833216598', N'$2a$06$bV2AqzzAcOHR3sGbI2dGLuDNbuwxPjKniMPQvXLLYU7z0.ZNZOcyG', N'E', N'T', N'https://upload.wikimedia.org/wikipedia/commons/3/34/PICA.jpg', N'Email     ', N'E', NULL)
INSERT [dbo].[USER] ([UserID], [FirstName], [LastName], [UserName], [Email], [ContactNo], [Password], [UserType], [Active], [UserImage], [AccountType], [PreferredCommunication], [PassRestCode]) VALUES (N'384715112175204214480         ', N'Sisikelelwe', N'Maqabangqa', N'Sike99', N's216443881@mandela.ac.za', NULL, N'$2a$06$zzJMXbWcHC5toDK57rgA0.DtKZ4dpcNuQ2BBp.CvZwRtCqii7nc5q', N'C', N'T', N'https://upload.wikimedia.org/wikipedia/commons/3/34/PICA.jpg', N'Email     ', N'E', NULL)
INSERT [dbo].[USER] ([UserID], [FirstName], [LastName], [UserName], [Email], [ContactNo], [Password], [UserType], [Active], [UserImage], [AccountType], [PreferredCommunication], [PassRestCode]) VALUES (N'407242734406745485560         ', N'Espacio', N'Dios', N'Espacio', N'espaciod@gmail.com', N'          ', N'$2a$06$GjaoYA7s6iY6OKFVbky0G.SF6YFG.xNpU4jbHw.W/SwVuk67bS8yi', N'C', N'T', N'https://upload.wikimedia.org/wikipedia/commons/3/34/PICA.jpg', N'Email     ', NULL, NULL)
INSERT [dbo].[USER] ([UserID], [FirstName], [LastName], [UserName], [Email], [ContactNo], [Password], [UserType], [Active], [UserImage], [AccountType], [PreferredCommunication], [PassRestCode]) VALUES (N'535314300265460205603         ', N'Kunta', N'Mawngi', N'KuntaMawngi', N'kuntamawngi@webmail.com', N'0603658893', N'$2a$06$0ckizcwqG47yrIb57ahDquxqj1R0xfZlSroGVdviWjSVBaNMQEkRi', N'C', N'T', N'https://upload.wikimedia.org/wikipedia/commons/3/34/PICA.jpg', N'Email     ', N'E', NULL)
INSERT [dbo].[USER] ([UserID], [FirstName], [LastName], [UserName], [Email], [ContactNo], [Password], [UserType], [Active], [UserImage], [AccountType], [PreferredCommunication], [PassRestCode]) VALUES (N'580312424824681240311         ', N'Lolo', N'Zouaï', N'Keepitonthelolo               ', N'lolozouai@gmail.com', N'0234567821', N'$2a$06$sj0XDfUG.PSGWkxsMIJaXubgf2V.2FbYKvKL9Z/WtFEO6k843H2Gi', N'E', N'T', N'http://sict-iis.nmmu.ac.za/beauxdebut/Theam/img/580312424824681240311.jpeg', N'Email     ', N'E', NULL)
INSERT [dbo].[USER] ([UserID], [FirstName], [LastName], [UserName], [Email], [ContactNo], [Password], [UserType], [Active], [UserImage], [AccountType], [PreferredCommunication], [PassRestCode]) VALUES (N'612648662678214805485         ', N'Kelvin Boateng', N'Aidoo', N'KBAidoo                       ', N'kbaidoo@mailbox.com', N'          ', N'$2a$06$zfmAxYuJyTAhk.Te2VLOZ.AhCuSX9AGvMQRMMqFXSoZ.CSSaJUXJ6', N'C', N'T', N'https://upload.wikimedia.org/wikipedia/commons/3/34/PICA.jpg', N'Email     ', N'E', NULL)
INSERT [dbo].[USER] ([UserID], [FirstName], [LastName], [UserName], [Email], [ContactNo], [Password], [UserType], [Active], [UserImage], [AccountType], [PreferredCommunication], [PassRestCode]) VALUES (N'635506662246576754303         ', N'Hlakanipa', N'Zonyane', N'MissHips', N's215121902@mandela.ac.za', NULL, N'$2a$06$bxKazhgVPWST74uLLSJxceRmpSNX0ibi7d7lWOjyuXH7WCmFBzMP6', N'C', N'T', N'https://upload.wikimedia.org/wikipedia/commons/3/34/PICA.jpg', N'Email     ', N'E', NULL)
INSERT [dbo].[USER] ([UserID], [FirstName], [LastName], [UserName], [Email], [ContactNo], [Password], [UserType], [Active], [UserImage], [AccountType], [PreferredCommunication], [PassRestCode]) VALUES (N'662881846588586465447         ', N'Lachea', N'Human', N'Lachea', N's214275574@mandela.ac.za', NULL, N'$2a$06$m6/Lo3JKOdJt5MoqizOL6.cuRJy1mfDdrg.325//tw8CXIKUac5kC', N'C', N'T', N'https://upload.wikimedia.org/wikipedia/commons/3/34/PICA.jpg', N'Email     ', N'E', NULL)
INSERT [dbo].[USER] ([UserID], [FirstName], [LastName], [UserName], [Email], [ContactNo], [Password], [UserType], [Active], [UserImage], [AccountType], [PreferredCommunication], [PassRestCode]) VALUES (N'686374338720721300668         ', N'Alejandro', N'Salomon', N'Alejandro', N'Alejandro', NULL, N'$2a$06$MYxTgW9MVhRQYchspFm3COKa3ohuHVYoyhb6d6wtZ6yxLuPZN79ce', N'C', N'F', NULL, N'Email     ', N'E', NULL)
INSERT [dbo].[USER] ([UserID], [FirstName], [LastName], [UserName], [Email], [ContactNo], [Password], [UserType], [Active], [UserImage], [AccountType], [PreferredCommunication], [PassRestCode]) VALUES (N'766086547051628703024         ', N'Christine', N'Queen', N'ChristineAndTheQueens', N'christineandthequeens@mail.com', N'          ', N'$2a$06$IloaSiv9x8E4FLVfArL37OH5SIhhffKuP5zDwWCOHk00mvymfoLHi', N'C', N'T', N'https://upload.wikimedia.org/wikipedia/commons/3/34/PICA.jpg', N'Email     ', NULL, NULL)
INSERT [dbo].[USER] ([UserID], [FirstName], [LastName], [UserName], [Email], [ContactNo], [Password], [UserType], [Active], [UserImage], [AccountType], [PreferredCommunication], [PassRestCode]) VALUES (N'777706452281511868742         ', N'Mr.', N'Eazi', N'Mr.Eazi                       ', N'mreazi@accratolagos.com', N'0789456352', N'$2a$06$SClyMhqMCO6HkNKY1o2tXeXEpvC3GT8SDnHuMMAdptD2tOO3Xgksy', N'C', N'T', N'https://upload.wikimedia.org/wikipedia/commons/3/34/PICA.jpg', N'Email     ', NULL, NULL)
INSERT [dbo].[WIDTH] ([WidthID], [Description]) VALUES (N'W1000     ', N'Thin')
INSERT [dbo].[WIDTH] ([WidthID], [Description]) VALUES (N'W1001     ', N'Normal')
INSERT [dbo].[WIDTH] ([WidthID], [Description]) VALUES (N'W1002     ', N'Box')
/****** Object:  StoredProcedure [dbo].[getSupplierDetails]    Script Date: 2018/10/16 18:02:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Description:	Returns the details of a supplier given the ID
-- =============================================
CREATE PROCEDURE [dbo].[getSupplierDetails]
	@SuppID nchar(10)
AS
BEGIN
	SELECT [SupplierID]
      ,[SupplierName]
      ,[ContactName]
      ,[ContactNo]
      ,[AddressLine1]
      ,[AddressLine2]
      ,[Suburb]
      ,[City]
      ,[ContactEmail]
  FROM [CHEVEUX].[dbo].[Supplier]
  WHERE [SupplierID] = @SuppID
END
GO
/****** Object:  StoredProcedure [dbo].[SP_AboutStylist]    Script Date: 2018/10/16 18:02:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		S.Maqabangqa
-- Description: Gives customer intro about the stylists that work for the salon. 
-- =============================================
CREATE  PROCEDURE [dbo].[SP_AboutStylist]
AS
BEGIN
	SET NOCOUNT ON;
	SELECT u.UserImage,
		   e.EmployeeID,(u.FirstName + ' ' + u.LastName)AS[StylistName],e.[Type],
		   s.ServiceID, p.[Name]AS[Specialisation], (p.ProductDescription)AS[SpecialisationDescription],
		   e.Bio
		   
	FROM [USER] u, EMPLOYEE e, STYLIST_SERVICE s, PRODUCT p
	
	WHERE e.EmployeeID=u.[UserID]
	AND   e.EmployeeID = s.EmployeeID
	AND   p.ProductID = s.ServiceID
	AND	  u.Active = 'T' 
	AND   u.UserType = 'E'
	AND   e.[Type] = 'S'
	
	ORDER BY StylistName asc

END
GO
/****** Object:  StoredProcedure [dbo].[SP_AccessorySearchByTerm]    Script Date: 2018/10/16 18:02:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE  PROCEDURE [dbo].[SP_AccessorySearchByTerm] 
	@searchTerm varchar(50)
AS
BEGIN
	select P.Name, P.ProductDescription, P.Price, P.[ProductType(T/A/S)], P.ProductID
	From PRODUCT P, ACCESSORY A, BRAND B
	WHERE ProductID = AccessoryID AND B.BrandID = A.BrandID AND 
	(P.Name like '%'+@searchTerm+'%'  
	Or P.ProductDescription like '%'+@searchTerm+'%'
	Or A.Colour like '%'+@searchTerm+'%'
	Or B.Name like '%'+@searchTerm+'%') 
	AND P.Active = 'Y'
	Order By P.[ProductType(T/A/S)]
END
GO
/****** Object:  StoredProcedure [dbo].[SP_AddAccessory]    Script Date: 2018/10/16 18:02:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- Description:	Inserting of a product
CREATE PROCEDURE [dbo].[SP_AddAccessory]
	@accessoryID nchar(10),
	@colour varchar(50),
	@qty int,
	@BrandID nchar(10) ,
	@name varchar(max),
	@productDescription varchar(max),
	@price money,
	@productType char(1),
	@SupplierID nchar(10)
	
AS
BEGIN
	BEGIN TRY
		BEGIN TRANSACTION

     INSERT ACCESSORY(AccessoryID, Colour, Qty, BrandID, SupplierID)
	 VALUES(@accessoryID, @colour, @qty, @BrandID, @SupplierID)

     INSERT PRODUCT(ProductID,[Name], ProductDescription,Price, [ProductType(T/A/S)], Active)
	 VALUES(@accessoryID, @name, @productDescription,@price, @productType, 'Y' )
	
	COMMIT TRANSACTION
	END TRY
	BEGIN CATCH
		IF @@TRANCOUNT > 0
			ROLLBACK TRANSACTION
	END CATCH


END
GO
/****** Object:  StoredProcedure [dbo].[SP_AddBooking]    Script Date: 2018/10/16 18:02:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[SP_AddBooking]
	@BookingID nchar(10),
	@Slot nchar(10),
	@CustomerID nchar(30),
	@StylistID nchar(30),
	@Date date,
	@primaryBookingID nchar(10),
	@Comment varchar(max),
	@Arrived char(1)

AS
BEGIN 
	BEGIN TRY
		BEGIN TRANSACTION
			INSERT INTO BOOKING(BookingID, SlotNo, CustomerID, StylistID, [Date], Arrived, Available, Comment, NotificationReminder, primaryBookingID)
			VALUES(@BookingID, @Slot, @CustomerID, @StylistID, @Date, @Arrived, 'N', @Comment, 0, @primaryBookingID)
		COMMIT TRANSACTION
	END TRY
	BEGIN CATCH
		IF @@TRANCOUNT > 0
			ROLLBACK TRANSACTION
	END CATCH

END
GO
/****** Object:  StoredProcedure [dbo].[SP_AddBraidService]    Script Date: 2018/10/16 18:02:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[SP_AddBraidService]
	@ProductID nchar(10),		
	@LengthID nchar(10),
	@StyleID nchar(10),
	@WidthID nchar(10)
AS
BEGIN
	BEGIN TRY
		BEGIN TRANSACTION
			INSERT INTO BRAID_SERVICE(ServiceID, StyleID, LengthID, WidthID)
			VALUES(@ProductID, @StyleID, @LengthID, @WidthID)
		COMMIT TRANSACTION
	END TRY
	BEGIN CATCH
		IF @@TRANCOUNT > 0
			ROLLBACK TRANSACTION
	END CATCH

END
GO
/****** Object:  StoredProcedure [dbo].[SP_AddEmployee]    Script Date: 2018/10/16 18:02:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		S.Maqabangqa
-- =============================================
CREATE  PROCEDURE [dbo].[SP_AddEmployee]
	@employeeID nchar(30),
	@bio varchar(max) = null,
	@AddressLine1 varchar(max) = null,
	@AddressLine2 varchar(max) = null,
	@suburb nchar(100) = null,
	@city nchar(100) = null,
	@firstname varchar(50),
	@lastname varchar(50),
	@username nvarchar(50),
	@email varchar(50),
	@contactNo nchar(10),
	@password varchar(max),
	@userimage varchar(max) = null,
	@passReset nchar(30) = null
AS
BEGIN
	BEGIN TRY
		BEGIN TRANSACTION 
			INSERT INTO	dbo.[EMPLOYEE]
						(EmployeeID,[Type],Bio,AddressLine1,AddressLine2,Suburb,City)
			VALUES		(@employeeID,'S',@bio,@AddressLine1,@AddressLine2,@suburb,@city)
			COMMIT TRANSACTION
	END TRY
	BEGIN CATCH 
		IF @@TRANCOUNT > 0 
			ROLLBACK TRANSACTION
	END CATCH  

	BEGIN TRY 
		BEGIN TRANSACTION 
			INSERT INTO [USER]
						(UserID,FirstName,LastName,UserName,Email,ContactNo,
						[Password],UserType,Active,UserImage,AccountType,
						PreferredCommunication,PassRestCode)
			VALUES		(@employeeID,@firstname,@lastname,@username,@email,@contactNo,
							@password,'E','T',@userimage,'Email',
							'E',@passReset)
		COMMIT TRANSACTION 
	END TRY
	BEGIN CATCH 
		IF @@TRANCOUNT > 0 
			ROLLBACK TRANSACTION
	END CATCH  
END

GO
/****** Object:  StoredProcedure [dbo].[SP_AddNewUser]    Script Date: 2018/10/16 18:02:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE  PROCEDURE [dbo].[SP_AddNewUser] 
	@ID nchar(30),
	@FN varchar(50), 
	@LN varchar(50),
	@UN nvarchar(50),
	@EM varchar(50),
	@CN nchar(10) = null,
	@UI nvarchar(MAX) = null,
	@AT nchar(10),
	@pass nvarchar(MAX) = null,
	@UT char(1)
AS
BEGIN
	begin try
		begin transaction
			INSERT INTO [CHEVEUX].[dbo].[User](UserID, FirstName, LastName, UserName, Email, ContactNo, UserType, Active, UserImage, AccountType, [Password])
			Values(@ID, @FN, @LN, @UN, @EM, @CN, @UT, 'T', @UI, @AT, @pass)
		commit transaction
	end try
	begin catch
		if @@TRANCOUNT > 0
			rollback transaction
	end catch
END
GO
/****** Object:  StoredProcedure [dbo].[SP_AddPaymentTypeToSalesRecord]    Script Date: 2018/10/16 18:02:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Description:	adds payment type to existing sales record
-- =============================================
CREATE  PROCEDURE [dbo].[SP_AddPaymentTypeToSalesRecord]
	@PaymentType nchar(10),
	@SaleD nchar(10)
AS
BEGIN
	begin try
		begin transaction
			UPDATE [SALE]
			SET PaymentType = @PaymentType
			Where SaleID = @SaleD
		Commit transaction
	end try
	begin catch
		if @@TRANCOUNT > 0
			rollback transaction
	end catch
END
GO
/****** Object:  StoredProcedure [dbo].[SP_AddProdToAutoPurch]    Script Date: 2018/10/16 18:02:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SP_AddProdToAutoPurch]
	@ProductID nchar(10),
	@QTY int
AS
BEGIN 
	BEGIN TRY
		BEGIN TRANSACTION
			INSERT INTO Auto_Purchase_Products([ProductID], [Qty])
			VALUES(@ProductID, @QTY)
		COMMIT TRANSACTION
	END TRY
	BEGIN CATCH
		IF @@TRANCOUNT > 0
			ROLLBACK TRANSACTION
	END CATCH

END
GO
/****** Object:  StoredProcedure [dbo].[SP_AddProduct]    Script Date: 2018/10/16 18:02:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- Description:	Inserting of a product
CREATE  PROCEDURE [dbo].[SP_AddProduct]
	@productID nchar(10),
	@name varchar(max),
	@productDescription varchar(max),
	@Price money,
	@productType char(1),
	@active char(1),
	@productImage nvarchar(50) 


AS
BEGIN
	BEGIN TRY
		BEGIN TRANSACTION

     INSERT PRODUCT(ProductID, Name, ProductDescription, Price, [ProductType(T/A/S)], Active, ProductImage)
	 VALUES (@productID, @name, @productDescription, @Price,@productType ,@active , @productImage)
	
	COMMIT TRANSACTION
	END TRY
	BEGIN CATCH
		IF @@TRANCOUNT > 0
			ROLLBACK TRANSACTION
	END CATCH
END
GO
/****** Object:  StoredProcedure [dbo].[SP_AddService]    Script Date: 2018/10/16 18:02:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[SP_AddService] 
	@ProductID nchar(10),
	@Name varchar(MAX),
	@Description varchar(MAX),
	@Price money,
	@Slots int,
	@Type char(1)
AS
BEGIN
	BEGIN TRY
		BEGIN TRANSACTION
			INSERT INTO PRODUCT(ProductID, [Name], ProductDescription, Price, [ProductType(T/A/S)], Active)
			VALUES(@ProductID, @Name, @Description, @Price, 'S', 'Y')
			INSERT INTO SERVICE(ServiceID, NoOfSlots, [Type(A/N/B)])
			VALUES(@ProductID, @Slots, @Type)
		COMMIT TRANSACTION
	END TRY
	BEGIN CATCH
		IF @@TRANCOUNT > 0
			ROLLBACK TRANSACTION
	END CATCH
END
GO
/****** Object:  StoredProcedure [dbo].[SP_AddSpecialisation]    Script Date: 2018/10/16 18:02:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		S.Maqabangqa
-- =============================================
CREATE PROCEDURE [dbo].[SP_AddSpecialisation] 
	@employeeID nchar(30), 
	@serviceID nchar(10)
AS
BEGIN
BEGIN TRY
		BEGIN TRANSACTION
			
			INSERT INTO STYLIST_SERVICE
						(EmployeeID,ServiceID)
			VALUES		(@employeeID,@serviceID)			

		COMMIT TRANSACTION 
		END TRY 
		BEGIN CATCH 
			IF @@TRANCOUNT > 0 
				ROLLBACK TRANSACTION
		END CATCH 
END
GO
/****** Object:  StoredProcedure [dbo].[SP_AddToBookingService]    Script Date: 2018/10/16 18:02:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE  PROCEDURE [dbo].[SP_AddToBookingService]
	@BookingID nchar(10),
	@ServiceID nchar(10)
AS
BEGIN
	BEGIN TRY
		BEGIN TRANSACTION
			INSERT INTO BookingService(BookingID, ServiceID)
			VALUES(@BookingID, @ServiceID)
		COMMIT TRANSACTION
	END TRY
	BEGIN CATCH
		IF @@TRANCOUNT > 0
			ROLLBACK TRANSACTION
	END CATCH

END
GO
/****** Object:  StoredProcedure [dbo].[SP_AddTreatment]    Script Date: 2018/10/16 18:02:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Description:	Inserting of a product
CREATE PROCEDURE [dbo].[SP_AddTreatment]
	@treatmentID nchar(10),
	@qty int,
	@treatmentType varchar(50),
	@BrandID nchar(10) ,
	@name varchar(max),
	@productDescription varchar(max),
	@price money,
	@productType char(1),
	@SupplierID nchar(10)

AS
BEGIN
	BEGIN TRY
		BEGIN TRANSACTION

     INSERT TREATMENT(TreatmentID,Qty,TreatmentType,BrandID, SupplierID)
	 VALUES(@treatmentID, @qty, @treatmentType, @BrandID, @SupplierID)

     INSERT PRODUCT(ProductID,Name,ProductDescription, Price,[ProductType(T/A/S)], Active)
	 VALUES(@treatmentID, @Name,@ProductDescription, @Price,@productType, 'Y' )
	
	COMMIT TRANSACTION
	END TRY
	BEGIN CATCH
		IF @@TRANCOUNT > 0
			ROLLBACK TRANSACTION
	END CATCH

END 
GO
/****** Object:  StoredProcedure [dbo].[SP_AddUserGoogleAuth]    Script Date: 2018/10/16 18:02:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE  PROCEDURE [dbo].[SP_AddUserGoogleAuth] 
	@ID nchar(30),
	@FN varchar(50),
	@LN varchar(50),
	@UN nvarchar(50),
	@EM varchar(50),
	@CN nchar(10),
	@UI nvarchar(MAX)
AS
BEGIN
	begin try
		begin transaction
			INSERT INTO [CHEVEUX].[dbo].[User](UserID, FirstName, LastName, UserName, Email, ContactNo, UserType, Active, UserImage)
			Values(@ID, @FN, @LN, @UN, @EM, @CN, 'C', 'T', @UI)
		commit transaction
	end try
	begin catch
		if @@TRANCOUNT > 0
			rollback transaction
	end catch
END
GO
/****** Object:  StoredProcedure [dbo].[SP_AllStylistPastBksForDate]    Script Date: 2018/10/16 18:02:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		S.Maqabangqa
-- Description:	Gets all stylists past bookings for a specific date
-- =============================================
CREATE PROCEDURE [dbo].[SP_AllStylistPastBksForDate]
@day datetime,
@sortBy nvarchar(max)=null,
	@sortDir nvarchar(max)=null
AS
BEGIN
	SET NOCOUNT ON;
		SELECT BookingID,B.primaryBookingID AS [PrimaryID],B.StylistID,B.CustomerID,
			
		   (SELECT (u.FirstName + ' ' + u.LastName)as[StylistName]
		   FROM [USER] u
		   WHERE u.UserID = B.StylistID)AS[StylistName],

		   (SELECT (u.FirstName+' '+u.LastName)as[CustomerName]
		   FROM [USER] u
		   WHERE u.UserID=B.CustomerID)AS[FullName],

		   B.[Date],TS.StartTime,TS.EndTime,B.Arrived

	From   [CHEVEUX].[dbo].[BOOKING] B, TIMESLOT TS, [User] U, EMPLOYEE e
	Where  B.SlotNo = TS.SlotNo 
	AND    B.StylistID = e.EmployeeID
	AND	   B.CustomerID = U.UserID
	AND    (B.Arrived ='Y' or B.CustomerID='0')
	AND    B.[Date] = @day
	AND    B.[Date] !> CAST(GETDATE() AS DATE)
	AND		B.BookingID = B.primaryBookingID
	ORDER BY 
		(CASE 
		 WHEN @sortBy='Stylist' AND @sortDir='Descending'
		 THEN (SELECT (u.FirstName + ' ' + u.LastName)as[stylist]
				FROM [USER] u 
				WHERE u.UserID=B.StylistID)
		 END) DESC,
		 (CASE
		  WHEN @sortBy='Date' AND @sortDir='Descending'
		  THEN B.[Date]
		  END) DESC,
		  (CASE
		 WHEN @sortBy='Stylist' AND @sortDir='Ascending'
		 THEN (SELECT (u.FirstName + ' ' + u.LastName)as[stylist]
				FROM [USER] u 
				WHERE u.UserID=B.StylistID)
		 END) ASC,
		 (CASE
		  WHEN @sortBy='Date' AND @sortDir='Ascending'
		  THEN B.[Date]
		  END) ASC
END
GO
/****** Object:  StoredProcedure [dbo].[SP_AllStylistsPastBksDR]    Script Date: 2018/10/16 18:02:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		S.Maqabangqa
-- Description: Gets all stylists past bookings withing a date range
-- =============================================
CREATE PROCEDURE [dbo].[SP_AllStylistsPastBksDR]
 @startDate datetime,
 @endDate datetime,
 @sortBy nvarchar(max)=null,
	@sortDir nvarchar(max)=null
AS
BEGIN
	SET NOCOUNT ON;
		SELECT BookingID,B.primaryBookingID AS [PrimaryID],B.StylistID,B.CustomerID,
			
		   (SELECT (u.FirstName + ' ' + u.LastName)as[StylistName]
		   FROM [USER] u
		   WHERE u.UserID = B.StylistID)AS[StylistName],

		   (SELECT (u.FirstName+' '+u.LastName)as[CustomerName]
		   FROM [USER] u
		   WHERE u.UserID=B.CustomerID)AS[FullName],

		   B.[Date],TS.StartTime,TS.EndTime,B.Arrived

	From   [CHEVEUX].[dbo].[BOOKING] B, TIMESLOT TS, [User] U, EMPLOYEE e
	Where  B.SlotNo = TS.SlotNo 
	AND    B.StylistID = e.EmployeeID
	AND	   B.CustomerID = U.UserID
	AND    (B.Arrived ='Y' or B.CustomerID='0') 
	AND	  (B.[Date] BETWEEN @startDate AND @endDate)
	AND    B.[Date] !> CAST(GETDATE() AS DATE)
	AND		B.BookingID = B.primaryBookingID
	ORDER BY 
		(CASE 
		 WHEN @sortBy='Stylist' AND @sortDir='Descending'
		 THEN (SELECT (u.FirstName + ' ' + u.LastName)as[stylist]
				FROM [USER] u 
				WHERE u.UserID=B.StylistID)
		 END) DESC,
		 (CASE
		  WHEN @sortBy='Date' AND @sortDir='Descending'
		  THEN B.[Date]
		  END) DESC,
		  (CASE
		 WHEN @sortBy='Stylist' AND @sortDir='Ascending'
		 THEN (SELECT (u.FirstName + ' ' + u.LastName)as[stylist]
				FROM [USER] u 
				WHERE u.UserID=B.StylistID)
		 END) ASC,
		 (CASE
		  WHEN @sortBy='Date' AND @sortDir='Ascending'
		  THEN B.[Date]
		  END) ASC
END
GO
/****** Object:  StoredProcedure [dbo].[SP_AllStylistsPastBookings]    Script Date: 2018/10/16 18:02:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- ============================================= 
-- Author:		S.Maqabangqa
-- Description:	Gets all stylists past bookings
-- =============================================
CREATE PROCEDURE [dbo].[SP_AllStylistsPastBookings]
@sortBy nvarchar(max)=null,
	@sortDir nvarchar(max)=null
AS
BEGIN
	SET NOCOUNT ON;
		SELECT BookingID,B.primaryBookingID AS [PrimaryID],B.StylistID,B.CustomerID,
			
		   (SELECT (u.FirstName + ' ' + u.LastName)as[StylistName]
		   FROM [USER] u
		   WHERE u.UserID = B.StylistID)AS[StylistName],

		   (SELECT (u.FirstName+' '+u.LastName)as[CustomerName]
		   FROM [USER] u
		   WHERE u.UserID=B.CustomerID)AS[FullName],

		   B.[Date],TS.StartTime,TS.EndTime,B.Arrived

	From   [CHEVEUX].[dbo].[BOOKING] B, TIMESLOT TS, [User] U, EMPLOYEE e
	Where  B.SlotNo = TS.SlotNo 
	AND    B.StylistID = e.EmployeeID
	AND	   B.CustomerID = U.UserID
	AND	   (B.Arrived = 'Y' or B.CustomerID='0')
	AND	   B.[Date] !> CAST(GETDATE() AS DATE)
	AND		B.BookingID = B.primaryBookingID
	ORDER BY 
		(CASE 
		 WHEN @sortBy='Stylist' AND @sortDir='Descending'
		 THEN (SELECT (u.FirstName + ' ' + u.LastName)as[stylist]
				FROM [USER] u 
				WHERE u.UserID=B.StylistID)
		 END) DESC,
		 (CASE
		  WHEN @sortBy='Date' AND @sortDir='Descending'
		  THEN B.[Date]
		  END) DESC,
		  (CASE
		 WHEN @sortBy='Stylist' AND @sortDir='Ascending'
		 THEN (SELECT (u.FirstName + ' ' + u.LastName)as[stylist]
				FROM [USER] u 
				WHERE u.UserID=B.StylistID)
		 END) ASC,
		 (CASE
		  WHEN @sortBy='Date' AND @sortDir='Ascending'
		  THEN B.[Date]
		  END) ASC
END
GO
/****** Object:  StoredProcedure [dbo].[SP_AllStylistsUpcomingBksDR]    Script Date: 2018/10/16 18:02:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		S.Maqabangqa
-- Description:	Gets all stylists upcoming bookings according to a date range.
-- =============================================
CREATE PROCEDURE [dbo].[SP_AllStylistsUpcomingBksDR]
@startDate datetime,
@endDate datetime,
@sortBy nvarchar(max)=null,
	@sortDir nvarchar(max)=null
AS
BEGIN
	SET NOCOUNT ON;
		SELECT BookingID,B.primaryBookingID AS [PrimaryID],B.StylistID,B.CustomerID,
			
		   (SELECT (u.FirstName + ' ' + u.LastName)as[StylistName]
		   FROM [USER] u
		   WHERE u.UserID = B.StylistID)AS[StylistName],

		   (SELECT (u.FirstName+' '+u.LastName)as[CustomerName]
		   FROM [USER] u
		   WHERE u.UserID=B.CustomerID)AS[FullName],

		   B.[Date],TS.StartTime,TS.EndTime,B.Arrived

	From   [CHEVEUX].[dbo].[BOOKING] B, TIMESLOT TS, [User] U, EMPLOYEE e
	Where  B.SlotNo = TS.SlotNo 
	AND    B.StylistID = e.EmployeeID
	AND	   B.CustomerID = U.UserID
	AND    (B.Arrived = 'N' or B.CustomerID='0') 
	AND	  (B.[Date] BETWEEN @startDate AND @endDate)
	AND   B.[Date] !< CAST(GETDATE() AS DATE)
	AND		B.BookingID = B.primaryBookingID
	ORDER BY 
		(CASE 
		 WHEN @sortBy='Stylist' AND @sortDir='Descending'
		 THEN (SELECT (u.FirstName + ' ' + u.LastName)as[stylist]
				FROM [USER] u 
				WHERE u.UserID=B.StylistID)
		 END) DESC,
		 (CASE
		  WHEN @sortBy='Date' AND @sortDir='Descending'
		  THEN B.[Date]
		  END) DESC,
		  (CASE
		 WHEN @sortBy='Stylist' AND @sortDir='Ascending'
		 THEN (SELECT (u.FirstName + ' ' + u.LastName)as[stylist]
				FROM [USER] u 
				WHERE u.UserID=B.StylistID)
		 END) ASC,
		 (CASE
		  WHEN @sortBy='Date' AND @sortDir='Ascending'
		  THEN B.[Date]
		  END) ASC
END
GO
/****** Object:  StoredProcedure [dbo].[SP_AllStylistsUpcomingBksForDate]    Script Date: 2018/10/16 18:02:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		S.Maqabangqa
-- Description:	Gets bookings for all stylists for a specific day
-- =============================================
CREATE PROCEDURE [dbo].[SP_AllStylistsUpcomingBksForDate]
@bookingDate datetime,
@sortBy nvarchar(max)=null,
	@sortDir nvarchar(max)=null
AS
BEGIN
	SET NOCOUNT ON;
		SELECT BookingID,B.primaryBookingID AS [PrimaryID],B.StylistID,B.CustomerID,
			
		   (SELECT (u.FirstName + ' ' + u.LastName)as[StylistName]
		   FROM [USER] u
		   WHERE u.UserID = B.StylistID)AS[StylistName],

		   (SELECT (u.FirstName+' '+u.LastName)as[CustomerName]
		   FROM [USER] u
		   WHERE u.UserID=B.CustomerID)AS[FullName],

		   B.[Date],TS.StartTime,TS.EndTime,B.Arrived

	From   [CHEVEUX].[dbo].[BOOKING] B, TIMESLOT TS, [User] U, EMPLOYEE e
	Where  B.SlotNo = TS.SlotNo 
	AND    B.StylistID = e.EmployeeID
	AND	   B.CustomerID = U.UserID
	AND    (B.Arrived = 'N' or B.CustomerID='0')
	AND    B.[Date] = @bookingDate
	AND    B.[Date] !< CAST(GETDATE() AS DATE)
	AND		B.BookingID = B.primaryBookingID
	ORDER BY 
		(CASE 
		 WHEN @sortBy='Stylist' AND @sortDir='Descending'
		 THEN (SELECT (u.FirstName + ' ' + u.LastName)as[stylist]
				FROM [USER] u 
				WHERE u.UserID=B.StylistID)
		 END) DESC,
		 (CASE
		  WHEN @sortBy='Date' AND @sortDir='Descending'
		  THEN B.[Date]
		  END) DESC,
		  (CASE
		 WHEN @sortBy='Stylist' AND @sortDir='Ascending'
		 THEN (SELECT (u.FirstName + ' ' + u.LastName)as[stylist]
				FROM [USER] u 
				WHERE u.UserID=B.StylistID)
		 END) ASC,
		 (CASE
		  WHEN @sortBy='Date' AND @sortDir='Ascending'
		  THEN B.[Date]
		  END) ASC
END
GO
/****** Object:  StoredProcedure [dbo].[SP_AllStylistsUpcomingBookings]    Script Date: 2018/10/16 18:02:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		S.Maqabangqa
-- Description:	Gets all upcoming bookings for all stylists
-- =============================================
CREATE PROCEDURE [dbo].[SP_AllStylistsUpcomingBookings]
@sortBy nvarchar(max)=null,
	@sortDir nvarchar(max)=null
AS
BEGIN
	SET NOCOUNT ON;
		SELECT BookingID,B.primaryBookingID AS [PrimaryID],B.StylistID,B.CustomerID,
			
		   (SELECT (u.FirstName + ' ' + u.LastName)as[StylistName]
		   FROM [USER] u
		   WHERE u.UserID = B.StylistID)AS[StylistName],

		   (SELECT (u.FirstName+' '+u.LastName)as[CustomerName]
		   FROM [USER] u
		   WHERE u.UserID=B.CustomerID)AS[FullName],

		   B.[Date],TS.StartTime,TS.EndTime,B.Arrived

	From   [CHEVEUX].[dbo].[BOOKING] B, TIMESLOT TS, [User] U, EMPLOYEE e
	Where  B.SlotNo = TS.SlotNo 
	AND    B.StylistID = e.EmployeeID
	AND	   B.CustomerID = U.UserID
	AND    (B.Arrived = 'N' or B.CustomerID='0')
	AND    B.[Date] !< CAST(GETDATE() AS DATE)
	AND		B.BookingID = B.primaryBookingID
	ORDER BY 
		(CASE 
		 WHEN @sortBy='Stylist' AND @sortDir='Descending'
		 THEN (SELECT (u.FirstName + ' ' + u.LastName)as[stylist]
				FROM [USER] u 
				WHERE u.UserID=B.StylistID)
		 END) DESC,
		 (CASE
		  WHEN @sortBy='Date' AND @sortDir='Descending'
		  THEN B.[Date]
		  END) DESC,
		  (CASE
		 WHEN @sortBy='Stylist' AND @sortDir='Ascending'
		 THEN (SELECT (u.FirstName + ' ' + u.LastName)as[stylist]
				FROM [USER] u 
				WHERE u.UserID=B.StylistID)
		 END) ASC,
		 (CASE
		  WHEN @sortBy='Date' AND @sortDir='Ascending'
		  THEN B.[Date]
		  END) ASC
END
GO
/****** Object:  StoredProcedure [dbo].[SP_BraidServiceSearchByTerm]    Script Date: 2018/10/16 18:02:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE  PROCEDURE [dbo].[SP_BraidServiceSearchByTerm]
	@searchTerm varchar(50)
AS
BEGIN
	select P.Name, P.ProductDescription, P.Price, P.[ProductType(T/A/S)], P.ProductID
	From PRODUCT P, SERVICE S, BRAID_SERVICE B, STYLE Y, LENGTH L, WIDTH W
	Where P.ProductID = s.ServiceID AND s.ServiceID = B.ServiceID AND B.LengthID = L.LengthID
	AND B.ServiceID = Y.StyleID AND B.WidthID = W.WidthID
	AND (P.Name like '%'+@searchTerm+'%'  
	Or P.ProductDescription like '%'+@searchTerm+'%' 
	OR W.Description like '%'+@searchTerm+'%'
	OR Y.Description like '%'+@searchTerm+'%'
	OR L.Description like '%'+@searchTerm+'%') AND P.Active = 'Y'
	AND P.[ProductType(T/A/S)] = 'S'
	Order By P.[ProductType(T/A/S)]
END
GO
/****** Object:  StoredProcedure [dbo].[SP_CheckCustomerStylistBooking]    Script Date: 2018/10/16 18:02:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		S.MAQABANGQA
-- Description:	Checks if customer has past bookings with a specific stylist
-- =============================================
CREATE PROCEDURE [dbo].[SP_CheckCustomerStylistBooking]
	@customerID nchar(30),
	@stylistID nchar(30)
AS
BEGIN
	SET NOCOUNT ON;
	SELECT B.BookingID,B.primaryBookingID AS [PrimaryID],B.StylistID,B.CustomerID,
		   (SELECT (u.FirstName + ' ' + u.LastName)as[StylistName]
		   FROM [USER] u
		   WHERE u.UserID = @stylistID)AS[StylistName],

		   (SELECT (u.FirstName+' '+u.LastName)as[CustomerName]
		   FROM [USER] u
		   WHERE u.UserID=@customerID)AS[CustomerName],

		   B.Arrived,

		   (SELECT u.Active
		   FROM [USER] u
		   WHERE u.UserID = @stylistID)AS[StylistAcitveStatus]

	FROM BOOKING B, EMPLOYEE E, [USER] U
	WHERE B.BookingID = B.primaryBookingID
	AND    B.StylistID = E.EmployeeID
	AND	   B.CustomerID = U.UserID
	AND	   B.Arrived = 'Y'
	AND	   B.StylistID=@stylistID
	AND	   B.CustomerID=@customerID
END
GO
/****** Object:  StoredProcedure [dbo].[SP_CheckForAccountTypeEmail]    Script Date: 2018/10/16 18:02:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Description:	Given a username or email address returns the aaccountype
-- =============================================
CREATE PROCEDURE [dbo].[SP_CheckForAccountTypeEmail]
	@identifier nvarchar(50)
AS
BEGIN
	Select AccountType
	From [USER]
	Where (Email = @identifier
		or UserName = @identifier)
		AND [USER].Active = 'T'
END
GO
/****** Object:  StoredProcedure [dbo].[SP_CheckForBrand]    Script Date: 2018/10/16 18:02:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Description:	check for a matching exisiting Brand with given ID
-- =============================================
CREATE PROCEDURE [dbo].[SP_CheckForBrand] 
	@BrandID nchar(10)
AS
BEGIN
	SELECT BrandID 
	FROM [BRAND]
	WHERE BrandID = @BrandID
END
GO
/****** Object:  StoredProcedure [dbo].[SP_CheckForOrder]    Script Date: 2018/10/16 18:02:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Description:	check for a matching exisiting Order with given ID
-- =============================================
create  PROCEDURE [dbo].[SP_CheckForOrder]
	@OrderID nchar(10)
AS
BEGIN
	SELECT OrderID 
	FROM [Order]
	WHERE OrderID = @OrderID
END
GO
/****** Object:  StoredProcedure [dbo].[SP_CheckForProduct]    Script Date: 2018/10/16 18:02:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Description:	check for a matching exisiting Product with given ID
-- =============================================
CREATE  PROCEDURE [dbo].[SP_CheckForProduct] 
	@ProductID nchar(10)
AS
BEGIN
	SELECT ProductID 
	FROM PRODUCT
	WHERE ProductID = @ProductID
END
GO
/****** Object:  StoredProcedure [dbo].[SP_CheckForReview]    Script Date: 2018/10/16 18:02:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		S.MAQABANGQA
-- Description:	check for a matching exisiting Review with given ID
-- =============================================
CREATE PROCEDURE [dbo].[SP_CheckForReview]
	@reviewID nchar(10)
AS
BEGIN
	SELECT ReviewID
	FROM REVIEW
	WHERE ReviewID = @reviewID

END
GO
/****** Object:  StoredProcedure [dbo].[SP_CheckForSupplier]    Script Date: 2018/10/16 18:02:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Description:	check for a matching exisiting Supplier with given ID
-- =============================================
CREATE PROCEDURE [dbo].[SP_CheckForSupplier] 
	@SuppID nchar(10)
AS
BEGIN
	SELECT SupplierID 
	FROM [Supplier]
	WHERE SupplierID = @SuppID
END
GO
/****** Object:  StoredProcedure [dbo].[SP_CheckForUserType]    Script Date: 2018/10/16 18:02:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SP_CheckForUserType] 
	@ID nchar(30)
AS
BEGIN
	SELECT UserType
	From [CHEVEUX].[dbo].[User]
	Where UserID = @ID
	AND [USER].Active = 'T'
END
GO
/****** Object:  StoredProcedure [dbo].[SP_CheckIn]    Script Date: 2018/10/16 18:02:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:	S.Maqabangqa
-- Create date: 03.06.2018
-- Description:	Updates booking arrived to "Y (YES)".
-- =============================================
CREATE PROCEDURE [dbo].[SP_CheckIn]
	@BookingID nchar(10)
AS
BEGIN
	BEGIN TRY 
		BEGIN TRANSACTION
			
			UPDATE BOOKING
			SET Arrived='Y'
			WHERE BOOKING.BookingID=@BookingID
				or BOOKING.primaryBookingID = @BookingID
			AND BOOKING.Arrived='N'
		COMMIT TRANSACTION
	END TRY 
	BEGIN CATCH 
		IF(@@TRANCOUNT > 0)
			ROLLBACK TRANSACTION
	END CATCH
END
GO
/****** Object:  StoredProcedure [dbo].[SP_CreateCustVisit]    Script Date: 2018/10/16 18:02:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		S.Maqabangqa
-- Create date: 04.06.2018
-- Description:	Add Customer Visit
-- =============================================

CREATE PROCEDURE [dbo].[SP_CreateCustVisit]
	@CustomerID nchar(30),
	@Date datetime,
	@PrimaryBookingID nchar(10),
	@Description varchar(50)=null
AS
BEGIN
	BEGIN TRY
		BEGIN TRANSACTION
			
			INSERT INTO CUST_VISIT (CustomerID,[Date],BookingID,[Description])
			VALUES (@CustomerID,
					@Date,
					@PrimaryBookingID,
					@Description)		
		COMMIT TRANSACTION
	END TRY
	BEGIN CATCH
		IF @@TRANCOUNT > 0
			ROLLBACK TRANSACTION
	END CATCH
END
GO
/****** Object:  StoredProcedure [dbo].[SP_CreateProductSalesDTLRecord]    Script Date: 2018/10/16 18:02:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE  PROCEDURE [dbo].[SP_CreateProductSalesDTLRecord]
	@SaleID nchar(10), 
	@ProductID nchar(10),
	@Qty int
AS
BEGIN
	begin try
		begin transaction
			INSERt INTO SALES_DTL(SaleID, ProductID, Qty, Price) 
				values(@SaleID, @ProductID, @Qty, (select Price from PRODUCT where ProductID = @ProductID))

				UPDATE TREATMENT
				SET Qty = (Select Qty From TREATMENT Where TreatmentID = @ProductID)-@Qty
				WHERE TreatmentID = @ProductID

				UPDATE ACCESSORY
				SET Qty = (Select Qty From ACCESSORY Where AccessoryID = @ProductID)-@Qty
				WHERE AccessoryID = @ProductID
		commit transaction
	end try
	begin catch
		if @@TRANCOUNT > 0
			rollback transaction
	end catch
END
GO
/****** Object:  StoredProcedure [dbo].[SP_CreateRestCode]    Script Date: 2018/10/16 18:02:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Description:	Add reset code to user account withh matching email address or username
-- =============================================
CREATE PROCEDURE [dbo].[SP_CreateRestCode]
	-- Add the parameters for the stored procedure here
	@restCode nchar(30),
	@Email varchar(50)
AS
BEGIN
begin try
		begin transaction
			UPDATE [USER]
			SET PassRestCode = @restCode
			Where Email = @Email
		Commit transaction
	end try
	begin catch
		if @@TRANCOUNT > 0
			rollback transaction
	end catch
END
GO
/****** Object:  StoredProcedure [dbo].[SP_CreateSalesDTLRecord]    Script Date: 2018/10/16 18:02:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Description:	Given a SaleID, ProductID & Qty is Creates a salesDTL record
-- =============================================
CREATE  PROCEDURE [dbo].[SP_CreateSalesDTLRecord]
	@SaleID nchar(10), 
	@ProductID nchar(10),
	@Qty int
AS
BEGIN
	begin try
		begin transaction
			INSERt INTO SALES_DTL(SaleID, ProductID, Qty, Price) 
				values(@SaleID, @ProductID, @Qty, (select Price
													from PRODUCT
													where ProductID = @ProductID))
								commit transaction
	end try
	begin catch
		if @@TRANCOUNT > 0
			rollback transaction
	end catch
END
GO
/****** Object:  StoredProcedure [dbo].[SP_CreateSalesRecord]    Script Date: 2018/10/16 18:02:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Description:	Given a the paramters Creates a sales record
-- =============================================
CREATE PROCEDURE [dbo].[SP_CreateSalesRecord] 
	@SaleID nchar(10),
	@CustID nchar(30)
AS
BEGIN
	begin try
		begin transaction
			INSERt INTO SALE(SaleID, [Date], CustomerID, BookingID) 
				values(@SaleID, GETDATE(), @CustID, null)
			commit transaction
	end try
	begin catch
		if @@TRANCOUNT > 0
			rollback transaction
	end catch
END
GO
/****** Object:  StoredProcedure [dbo].[SP_CreateSalesRecordForBooking]    Script Date: 2018/10/16 18:02:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Description:	Given a bookingID Creates a sales record
-- =============================================
CREATE PROCEDURE [dbo].[SP_CreateSalesRecordForBooking]
	@BookingID nchar(10)
AS
BEGIN
	begin try
		begin transaction
			INSERt INTO SALE(SaleID, [Date], CustomerID, BookingID) 
				values(@BookingID, GETDATE(), (select CustomerID
								from BOOKING
								where BookingID = @BookingID), @BookingID)
								commit transaction
	end try
	begin catch
		if @@TRANCOUNT > 0
			rollback transaction
	end catch
END
GO
/****** Object:  StoredProcedure [dbo].[SP_CustomerPastBookingsForDate]    Script Date: 2018/10/16 18:02:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		S.Maqabangqa
-- =============================================
CREATE PROCEDURE [dbo].[SP_CustomerPastBookingsForDate]
	@customerID nchar(30),
	@date datetime
AS
BEGIN
	SET NOCOUNT ON;
	SELECT BookingID,b.primaryBookingID AS [PrimaryID],B.StylistID,B.CustomerID,
			
		   (SELECT (u.FirstName + ' ' + u.LastName)as[StylistName]
		   FROM [USER] u
		   WHERE u.UserID = b.StylistID)AS[StylistName],

		   (SELECT (u.FirstName+' '+u.LastName)as[CustomerName]
		   FROM [USER] u
		   WHERE u.UserID=@customerID)AS[CustomerFullName],

		   B.[Date],TS.StartTime,B.Arrived

	From   [CHEVEUX].[dbo].[BOOKING] B, TIMESLOT TS, [User] U, EMPLOYEE e
	Where  b.StylistID = u.UserID
	AND		b.StylistID = e.EmployeeID
	AND    B.SlotNo = TS.SlotNo 
	AND    B.CustomerID = @customerID
	AND	   B.Arrived = 'Y'
	AND	   B.[Date] !> CAST(GETDATE() AS DATE)
	AND B.[Date]=@date
	and B.BookingID=B.primaryBookingID

END
GO
/****** Object:  StoredProcedure [dbo].[SP_CustomerProductSalesReportByValue]    Script Date: 2018/10/16 18:02:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SP_CustomerProductSalesReportByValue] 
	@StartDate DateTime,
	@EndDate DateTime
AS
BEGIN
	SELECT sum(sd.Price) as [Value], sum(sd.Qty) as Volume, max(U.FirstName) as [Customer]
  FROM [CHEVEUX].[dbo].[SALES_DTL] sd, [CHEVEUX].[dbo].SALE s, [CHEVEUX].[dbo].[USER] u, Product p
  Where sd.SaleID = s.SaleID
	AND S.CustomerID = u.UserID
	AND s.[Date]  between @StartDate and @EndDate
	AND p.[ProductID] = sd.[ProductID]
	AND p.[ProductType(T/A/S)] != 'S'
  Group by U.[UserName]
  order by [Value] desc
END
GO
/****** Object:  StoredProcedure [dbo].[SP_CustomerProductSalesReportByValueCash]    Script Date: 2018/10/16 18:02:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SP_CustomerProductSalesReportByValueCash] 
	@StartDate DateTime,
	@EndDate DateTime
AS
BEGIN
	SELECT sum(sd.Price) as [Value], sum(sd.Qty) as Volume, max(U.FirstName) as [Customer]
  FROM [CHEVEUX].[dbo].[SALES_DTL] sd, [CHEVEUX].[dbo].SALE s, [CHEVEUX].[dbo].[USER] u, Product p
  Where sd.SaleID = s.SaleID
	AND s.PaymentType = 'Cash'
	AND S.CustomerID = u.UserID
	AND s.[Date]  between @StartDate and @EndDate
	AND p.[ProductID] = sd.[ProductID]
	AND p.[ProductType(T/A/S)] != 'S'
  Group by U.[UserName]
  order by [Value] desc
END
GO
/****** Object:  StoredProcedure [dbo].[SP_CustomerProductSalesReportByValueCredit]    Script Date: 2018/10/16 18:02:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SP_CustomerProductSalesReportByValueCredit] 
	@StartDate DateTime,
	@EndDate DateTime
AS
BEGIN
	SELECT sum(sd.Price) as [Value], sum(sd.Qty) as Volume, max(U.FirstName) as [Customer]
  FROM [CHEVEUX].[dbo].[SALES_DTL] sd, [CHEVEUX].[dbo].SALE s, [CHEVEUX].[dbo].[USER] u, Product p
  Where sd.SaleID = s.SaleID
	AND s.PaymentType = 'Credit'
	AND S.CustomerID = u.UserID
	AND s.[Date]  between @StartDate and @EndDate
	AND p.[ProductID] = sd.[ProductID]
	AND p.[ProductType(T/A/S)] != 'S'
  Group by U.[UserName]
  order by [Value] desc
END
GO
/****** Object:  StoredProcedure [dbo].[SP_CustomerProductSalesReportByVolume]    Script Date: 2018/10/16 18:02:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SP_CustomerProductSalesReportByVolume] 
	@StartDate DateTime,
	@EndDate DateTime
AS
BEGIN
	SELECT sum(sd.Price) as [Value], sum(sd.Qty) as Volume, max(U.FirstName) as [Customer]
  FROM [CHEVEUX].[dbo].[SALES_DTL] sd, [CHEVEUX].[dbo].SALE s, [CHEVEUX].[dbo].[USER] u, Product p
  Where sd.SaleID = s.SaleID
	AND S.CustomerID = u.UserID
	AND s.[Date]  between @StartDate and @EndDate
	AND p.[ProductID] = sd.[ProductID]
	AND p.[ProductType(T/A/S)] != 'S'
  Group by U.[UserName]
  order by [Volume] desc
END
GO
/****** Object:  StoredProcedure [dbo].[SP_CustomerProductSalesReportByVolumeCash]    Script Date: 2018/10/16 18:02:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SP_CustomerProductSalesReportByVolumeCash] 
	@StartDate DateTime,
	@EndDate DateTime
AS
BEGIN
	SELECT sum(sd.Price) as [Value], sum(sd.Qty) as Volume, max(U.FirstName) as [Customer]
  FROM [CHEVEUX].[dbo].[SALES_DTL] sd, [CHEVEUX].[dbo].SALE s, [CHEVEUX].[dbo].[USER] u, Product p
  Where sd.SaleID = s.SaleID
	AND s.PaymentType = 'Cash'
	AND S.CustomerID = u.UserID
	AND s.[Date]  between @StartDate and @EndDate
	AND p.[ProductID] = sd.[ProductID]
	AND p.[ProductType(T/A/S)] != 'S'
  Group by U.[UserName]
  order by [Volume] desc
END
GO
/****** Object:  StoredProcedure [dbo].[SP_CustomerProductSalesReportByVolumeCredit]    Script Date: 2018/10/16 18:02:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SP_CustomerProductSalesReportByVolumeCredit]
	@StartDate DateTime,
	@EndDate DateTime
AS
BEGIN
	SELECT sum(sd.Price) as [Value], sum(sd.Qty) as Volume, max(U.FirstName) as [Customer]
  FROM [CHEVEUX].[dbo].[SALES_DTL] sd, [CHEVEUX].[dbo].SALE s, [CHEVEUX].[dbo].[USER] u, Product p
  Where sd.SaleID = s.SaleID
	AND s.PaymentType = 'Credit'
	AND S.CustomerID = u.UserID
	AND s.[Date]  between @StartDate and @EndDate
	AND p.[ProductID] = sd.[ProductID]
	AND p.[ProductType(T/A/S)] != 'S'
  Group by U.[UserName]
  order by [Volume] desc
END
GO
/****** Object:  StoredProcedure [dbo].[SP_CustomerSalesReportByValue]    Script Date: 2018/10/16 18:02:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Description:	Get the product sales order by value
-- =============================================
CREATE PROCEDURE [dbo].[SP_CustomerSalesReportByValue] 
	@StartDate DateTime,
	@EndDate DateTime
AS
BEGIN
	SELECT sum(sd.Price) as [Value], sum(sd.Qty) as Volume, max(U.FirstName) as [Customer]
  FROM [CHEVEUX].[dbo].[SALES_DTL] sd, [CHEVEUX].[dbo].SALE s, [CHEVEUX].[dbo].[USER] u
  Where sd.SaleID = s.SaleID
	AND S.CustomerID = u.UserID
	AND s.[Date]  between @StartDate and @EndDate
  Group by U.[UserName]
  order by [Value] desc
END
GO
/****** Object:  StoredProcedure [dbo].[SP_CustomerSalesReportByValueCash]    Script Date: 2018/10/16 18:02:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Description:	Get the product sales order by value fore cash sales
-- =============================================
CREATE PROCEDURE [dbo].[SP_CustomerSalesReportByValueCash] 
	@StartDate DateTime,
	@EndDate DateTime
AS
BEGIN
	SELECT sum(sd.Price) as [Value], sum(sd.Qty) as Volume, max(U.FirstName) as [Customer]
  FROM [CHEVEUX].[dbo].[SALES_DTL] sd, [CHEVEUX].[dbo].SALE s, [CHEVEUX].[dbo].[USER] u
  Where sd.SaleID = s.SaleID
	AND s.PaymentType = 'Cash'
	AND S.CustomerID = u.UserID
	AND s.[Date]  between @StartDate and @EndDate
  Group by U.[UserName]
  order by [Value] desc
END
GO
/****** Object:  StoredProcedure [dbo].[SP_CustomerSalesReportByValueCredit]    Script Date: 2018/10/16 18:02:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Description:	Get the product sales order by value fore credit sales
-- =============================================
CREATE PROCEDURE [dbo].[SP_CustomerSalesReportByValueCredit] 
	@StartDate DateTime,
	@EndDate DateTime
AS
BEGIN
	SELECT sum(sd.Price) as [Value], sum(sd.Qty) as Volume, max(U.FirstName) as [Customer]
  FROM [CHEVEUX].[dbo].[SALES_DTL] sd, [CHEVEUX].[dbo].SALE s, [CHEVEUX].[dbo].[USER] u
  Where sd.SaleID = s.SaleID
	AND s.PaymentType = 'Credit'
	AND S.CustomerID = u.UserID
	AND s.[Date]  between @StartDate and @EndDate
  Group by U.[UserName]
  order by [Value] desc
END
GO
/****** Object:  StoredProcedure [dbo].[SP_CustomerSalesReportByVolume]    Script Date: 2018/10/16 18:02:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Description:	Get the product sales order by Volume
-- =============================================
create PROCEDURE [dbo].[SP_CustomerSalesReportByVolume] 
	@StartDate DateTime,
	@EndDate DateTime
AS
BEGIN
	SELECT sum(sd.Price) as [Value], sum(sd.Qty) as Volume, max(U.FirstName) as [Customer]
  FROM [CHEVEUX].[dbo].[SALES_DTL] sd, [CHEVEUX].[dbo].SALE s, [CHEVEUX].[dbo].[USER] u
  Where sd.SaleID = s.SaleID
	AND S.CustomerID = u.UserID
	AND s.[Date]  between @StartDate and @EndDate
  Group by U.[UserName]
  order by [Volume] desc
END
GO
/****** Object:  StoredProcedure [dbo].[SP_CustomerSalesReportByVolumeCash]    Script Date: 2018/10/16 18:02:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Description:	Get the product sales order by Volume for cash sales
-- =============================================
CREATE PROCEDURE [dbo].[SP_CustomerSalesReportByVolumeCash] 
	@StartDate DateTime,
	@EndDate DateTime
AS
BEGIN
	SELECT sum(sd.Price) as [Value], sum(sd.Qty) as Volume, max(U.FirstName) as [Customer]
  FROM [CHEVEUX].[dbo].[SALES_DTL] sd, [CHEVEUX].[dbo].SALE s, [CHEVEUX].[dbo].[USER] u
  Where sd.SaleID = s.SaleID
	AND s.PaymentType = 'Cash'
	AND S.CustomerID = u.UserID
	AND s.[Date]  between @StartDate and @EndDate
  Group by U.[UserName]
  order by [Volume] desc
END
GO
/****** Object:  StoredProcedure [dbo].[SP_CustomerSalesReportByVolumeCredit]    Script Date: 2018/10/16 18:02:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Description:	Get the product sales order by Volume for Credit sales
-- =============================================
CREATE PROCEDURE [dbo].[SP_CustomerSalesReportByVolumeCredit]
	@StartDate DateTime,
	@EndDate DateTime
AS
BEGIN
	SELECT sum(sd.Price) as [Value], sum(sd.Qty) as Volume, max(U.FirstName) as [Customer]
  FROM [CHEVEUX].[dbo].[SALES_DTL] sd, [CHEVEUX].[dbo].SALE s, [CHEVEUX].[dbo].[USER] u
  Where sd.SaleID = s.SaleID
	AND s.PaymentType = 'Credit'
	AND S.CustomerID = u.UserID
	AND s.[Date]  between @StartDate and @EndDate
  Group by U.[UserName]
  order by [Volume] desc
END
GO
/****** Object:  StoredProcedure [dbo].[SP_CustomerSatisfaction]    Script Date: 2018/10/16 18:02:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		S.MAQABANGQA
-- =============================================
CREATE PROCEDURE [dbo].[SP_CustomerSatisfaction] 
	@startDate datetime = null,
	@endDate datetime =null
AS
BEGIN
	SET NOCOUNT ON;
	SELECT Distinct(r.CustomerID),(u.FirstName + ' ' + u.LastName)as[CustomerName],
		   AVG(Rating)as Rating, COUNT(r.CustomerID) as NoOfReviews
	FROM REVIEW r, [USER] u
	where r.CustomerID=u.UserID
	and   r.[Date] between @startDate and @endDate
	GROUP BY r.CustomerID,u.FirstName,u.LastName
	ORDER BY Rating desc
END
GO
/****** Object:  StoredProcedure [dbo].[SP_CustomerServiceSalesReportByValue]    Script Date: 2018/10/16 18:02:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SP_CustomerServiceSalesReportByValue] 
	@StartDate DateTime,
	@EndDate DateTime
AS
BEGIN
	SELECT sum(sd.Price) as [Value], sum(sd.Qty) as Volume, max(U.FirstName) as [Customer]
  FROM [CHEVEUX].[dbo].[SALES_DTL] sd, [CHEVEUX].[dbo].SALE s, [CHEVEUX].[dbo].[USER] u, Product p
  Where sd.SaleID = s.SaleID
	AND S.CustomerID = u.UserID
	AND s.[Date]  between @StartDate and @EndDate
	AND p.[ProductID] = sd.[ProductID]
	AND p.[ProductType(T/A/S)] = 'S'
  Group by U.[UserName]
  order by [Value] desc
END
GO
/****** Object:  StoredProcedure [dbo].[SP_CustomerServiceSalesReportByValueCash]    Script Date: 2018/10/16 18:02:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SP_CustomerServiceSalesReportByValueCash] 
	@StartDate DateTime,
	@EndDate DateTime
AS
BEGIN
	SELECT sum(sd.Price) as [Value], sum(sd.Qty) as Volume, max(U.FirstName) as [Customer]
  FROM [CHEVEUX].[dbo].[SALES_DTL] sd, [CHEVEUX].[dbo].SALE s, [CHEVEUX].[dbo].[USER] u, Product p
  Where sd.SaleID = s.SaleID
	AND s.PaymentType = 'Cash'
	AND S.CustomerID = u.UserID
	AND s.[Date]  between @StartDate and @EndDate
	AND p.[ProductID] = sd.[ProductID]
	AND p.[ProductType(T/A/S)] = 'S'
  Group by U.[UserName]
  order by [Value] desc
END
GO
/****** Object:  StoredProcedure [dbo].[SP_CustomerServiceSalesReportByValueCredit]    Script Date: 2018/10/16 18:02:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SP_CustomerServiceSalesReportByValueCredit] 
	@StartDate DateTime,
	@EndDate DateTime
AS
BEGIN
	SELECT sum(sd.Price) as [Value], sum(sd.Qty) as Volume, max(U.FirstName) as [Customer]
  FROM [CHEVEUX].[dbo].[SALES_DTL] sd, [CHEVEUX].[dbo].SALE s, [CHEVEUX].[dbo].[USER] u, Product p
  Where sd.SaleID = s.SaleID
	AND s.PaymentType = 'Credit'
	AND S.CustomerID = u.UserID
	AND s.[Date]  between @StartDate and @EndDate
	AND p.[ProductID] = sd.[ProductID]
	AND p.[ProductType(T/A/S)] = 'S'
  Group by U.[UserName]
  order by [Value] desc
END
GO
/****** Object:  StoredProcedure [dbo].[SP_CustomerServiceSalesReportByVolume]    Script Date: 2018/10/16 18:02:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SP_CustomerServiceSalesReportByVolume] 
	@StartDate DateTime,
	@EndDate DateTime
AS
BEGIN
	SELECT sum(sd.Price) as [Value], sum(sd.Qty) as Volume, max(U.FirstName) as [Customer]
  FROM [CHEVEUX].[dbo].[SALES_DTL] sd, [CHEVEUX].[dbo].SALE s, [CHEVEUX].[dbo].[USER] u, Product p
  Where sd.SaleID = s.SaleID
	AND S.CustomerID = u.UserID
	AND s.[Date]  between @StartDate and @EndDate
	AND p.[ProductID] = sd.[ProductID]
	AND p.[ProductType(T/A/S)] = 'S'
  Group by U.[UserName]
  order by [Volume] desc
END
GO
/****** Object:  StoredProcedure [dbo].[SP_CustomerServiceSalesReportByVolumeCash]    Script Date: 2018/10/16 18:02:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SP_CustomerServiceSalesReportByVolumeCash] 
	@StartDate DateTime,
	@EndDate DateTime
AS
BEGIN
	SELECT sum(sd.Price) as [Value], sum(sd.Qty) as Volume, max(U.FirstName) as [Customer]
  FROM [CHEVEUX].[dbo].[SALES_DTL] sd, [CHEVEUX].[dbo].SALE s, [CHEVEUX].[dbo].[USER] u, Product p
  Where sd.SaleID = s.SaleID
	AND s.PaymentType = 'Cash'
	AND S.CustomerID = u.UserID
	AND s.[Date]  between @StartDate and @EndDate
	AND p.[ProductID] = sd.[ProductID]
	AND p.[ProductType(T/A/S)] = 'S'
  Group by U.[UserName]
  order by [Volume] desc
END
GO
/****** Object:  StoredProcedure [dbo].[SP_CustomerServiceSalesReportByVolumeCredit]    Script Date: 2018/10/16 18:02:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SP_CustomerServiceSalesReportByVolumeCredit]
	@StartDate DateTime,
	@EndDate DateTime
AS
BEGIN
	SELECT sum(sd.Price) as [Value], sum(sd.Qty) as Volume, max(U.FirstName) as [Customer]
  FROM [CHEVEUX].[dbo].[SALES_DTL] sd, [CHEVEUX].[dbo].SALE s, [CHEVEUX].[dbo].[USER] u, Product p
  Where sd.SaleID = s.SaleID
	AND s.PaymentType = 'Credit'
	AND S.CustomerID = u.UserID
	AND s.[Date]  between @StartDate and @EndDate
	AND p.[ProductID] = sd.[ProductID]
	AND p.[ProductType(T/A/S)] = 'S'
  Group by U.[UserName]
  order by [Volume] desc
END
GO
/****** Object:  StoredProcedure [dbo].[SP_CustomersReviewForBooking]    Script Date: 2018/10/16 18:02:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		S.MAQABANGQA
-- =============================================
CREATE PROCEDURE [dbo].[SP_CustomersReviewForBooking]
	@customerID nchar(30),
	@bookingID nchar(10)
AS
BEGIN
	SET NOCOUNT ON;
	SELECT ReviewID,CustomerID,EmployeeID,primaryBookingID,[Date],[Time],Rating,Comment
	FROM [CHEVEUX].[dbo].[REVIEW]
	WHERE CustomerID=@customerID
	and primaryBookingID=@bookingID
END
GO
/****** Object:  StoredProcedure [dbo].[SP_CustomersReviewForStylist]    Script Date: 2018/10/16 18:02:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		S.MAQABANGQA
-- =============================================
CREATE PROCEDURE [dbo].[SP_CustomersReviewForStylist]
	@customerID nchar(30),
	@stylistID nchar(30)
AS
BEGIN
	SET NOCOUNT ON;
	SELECT ReviewID,CustomerID,EmployeeID,primaryBookingID,[Date],[Time],Rating,Comment
	FROM REVIEW
	WHERE CustomerID=@customerID
	and EmployeeID=@stylistID
	and primaryBookingID is null
END
GO
/****** Object:  StoredProcedure [dbo].[SP_CustRecentBookings]    Script Date: 2018/10/16 18:02:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:	S.MAQABANGQA
-- =============================================
CREATE PROCEDURE [dbo].[SP_CustRecentBookings]
	@CustID nchar(30)
AS
BEGIN
	Select U.FirstName, B.[Date], TS.StartTime, BookingID, B.Arrived, B.StylistID       
	From BOOKING B, TIMESLOT TS, [User] U
	Where CustomerID = @CustID 
	AND B.SlotNo = TS.SlotNo 
	AND B.StylistID = U.UserID
	AND B.Arrived = 'Y'
	And B.BookingID = B.primaryBookingID
	AND	B.[Date] !< DATEADD(mm, -2, GETDATE())
	Order by B.[Date] desc
END
GO
/****** Object:  StoredProcedure [dbo].[SP_DeactivateUser]    Script Date: 2018/10/16 18:02:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Description:	Set the active colum of a user acount to false
-- =============================================
CREATE  PROCEDURE [dbo].[SP_DeactivateUser] 
	@UserID nchar(30)
AS
BEGIN
	begin try
		begin transaction
			UPDATE [USER]
			SET Active = 'F'
			Where [UserID] = @UserID
		Commit transaction
	end try
	begin catch
		if @@TRANCOUNT > 0
			rollback transaction
	end catch
END
GO
/****** Object:  StoredProcedure [dbo].[SP_DeleteBooking]    Script Date: 2018/10/16 18:02:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SP_DeleteBooking] 
	@BookingID nchar(10)
AS
BEGIN
		Begin Transaction;
			Begin Try
				DELETE FROM BOOKING
				Where (BookingID = @BookingID
					or  BOOKING.primaryBookingID = @BookingID)

				DELETE FROM BookingService
				Where BookingID = @BookingID
			End try
		Begin Catch
			if @@TRANCOUNT > 0
				Begin
					ROLLBACK TRANSACTION
				End
		End Catch
commit Transaction
END
GO
/****** Object:  StoredProcedure [dbo].[SP_DeleteBookingService]    Script Date: 2018/10/16 18:02:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Description:	Removes Booking Records and Booking Service matchin the BookingID or primaryBookingID
-- =============================================
CREATE PROCEDURE [dbo].[SP_DeleteBookingService] 
	@BookingID nchar(10),
	@ServiceID nchar(10)
AS
BEGIN
		Begin Transaction;
			Begin Try
				DELETE FROM BookingService
				Where BookingID = @BookingID
						AND ServiceID = @ServiceID
			End try
		Begin Catch
			if @@TRANCOUNT > 0
				Begin
					ROLLBACK TRANSACTION
				End
		End Catch
commit Transaction
END
GO
/****** Object:  StoredProcedure [dbo].[SP_DeleteProdFromAutoPurch]    Script Date: 2018/10/16 18:02:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SP_DeleteProdFromAutoPurch]
	@ProductID nchar(10)
AS
BEGIN
		Begin Transaction;
			Begin Try
				DELETE FROM Auto_Purchase_Products
				Where [ProductID] = @ProductID
			End try
		Begin Catch
			if @@TRANCOUNT > 0
				Begin
					ROLLBACK TRANSACTION
				End
		End Catch
commit Transaction
END
GO
/****** Object:  StoredProcedure [dbo].[SP_DeleteSecondaryBookings]    Script Date: 2018/10/16 18:02:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Description:	Removes all secondary bookings matching primary bookingIDs
-- =============================================
CREATE PROCEDURE [dbo].[SP_DeleteSecondaryBookings]
	@PrimaryBookingID nchar(10)
AS
BEGIN
	Begin Transaction;
		Begin Try
			DELETE FROM BOOKING
			Where BookingID != @PrimaryBookingID
					AND primaryBookingID = @PrimaryBookingID
		End try
	Begin Catch
		if @@TRANCOUNT > 0
			Begin
				ROLLBACK TRANSACTION
			End
		End Catch
commit Transaction
END
GO
/****** Object:  StoredProcedure [dbo].[SP_EditBrand]    Script Date: 2018/10/16 18:02:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Description:	updates a brand give the paramaters
-- =============================================
CREATE PROCEDURE [dbo].[SP_EditBrand] 
	@BrandID nchar(10),
	@BrandName varchar(50), 
	@Type char(1)
AS
BEGIN
	begin try
		begin transaction
			UPDATE [CHEVEUX].[dbo].[BRAND]
			SET [Name] = @BrandName,
				[Type(T/A)] = @Type
			WHERE BrandID = @BrandID
		commit transaction
	end try
	begin catch
		if @@TRANCOUNT > 0
			rollback transaction
	end catch
END
GO
/****** Object:  StoredProcedure [dbo].[SP_EditProductType]    Script Date: 2018/10/16 18:02:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Description:	updates a Product Type given the paramaters
-- =============================================
CREATE PROCEDURE [dbo].[SP_EditProductType] 
	@typeID nchar(10),
	@typeName varchar(50), 
	@ProdOrSer char(1)
AS
BEGIN
	begin try
		begin transaction
			UPDATE [CHEVEUX].[dbo].[ProductType]
			SET [Name] = @typeName,
				[Product/Service] = @ProdOrSer
			WHERE TypeID = @typeID
		commit transaction
	end try
	begin catch
		if @@TRANCOUNT > 0
			rollback transaction
	end catch
END
GO
/****** Object:  StoredProcedure [dbo].[SP_EditSupplier]    Script Date: 2018/10/16 18:02:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Description:	updates a supplier give the paramaters
-- =============================================
CREATE PROCEDURE [dbo].[SP_EditSupplier] 
	@SuppID nchar(10),
	@SuppName nchar(50),
	@ContactName nchar(50), 
	@ContactNo nchar(10), 
	@AddressL1 nchar(100), 
	@AddressL2 nchar(100), 
	@Suburb nchar(100), 
	@City nchar(100), 
	@ContactEmail varchar(50)
AS
BEGIN
	begin try
		begin transaction
			UPDATE [CHEVEUX].[dbo].[Supplier]
			SET SupplierName = @SuppName,
				ContactName = @ContactName,
				ContactNo = @ContactNo,
				AddressLine1 = @AddressL1, 
				AddressLine2 = @AddressL2, 
				Suburb = @Suburb, 
				City = @City,
				ContactEmail = @ContactEmail
			WHERE SupplierID = @SuppID
		commit transaction
	end try
	begin catch
		if @@TRANCOUNT > 0
			rollback transaction
	end catch
END
GO
/****** Object:  StoredProcedure [dbo].[SP_EditUser]    Script Date: 2018/10/16 18:02:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Description:	Edits the Username and Contact No. of a user
-- =============================================
CREATE PROCEDURE [dbo].[SP_EditUser] 
	@UserID nchar(30),
	@UserName nchar(30),
	@ContactNo nchar(10),
	@Name varchar(50),
	@LName varchar(50),
	@Email varchar(50)
AS
BEGIN
	begin try
		begin transaction
			UPDATE [User]
			SET UserName = @UserName,
				ContactNo = @ContactNo,
				FirstName = @Name,
				LastName = @LName,
				Email = @Email
			Where UserID = @UserID
			AND [USER].Active = 'T'
		Commit transaction
	end try
	begin catch
		if @@TRANCOUNT > 0
			rollback transaction
	end catch
END
GO
/****** Object:  StoredProcedure [dbo].[SP_FeaturedProductsAndContact]    Script Date: 2018/10/16 18:02:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Description:	Get the features stylists and contact us info
-- =============================================
CREATE  PROCEDURE [dbo].[SP_FeaturedProductsAndContact] 
AS
BEGIN
	SELECT [FeatureID], [ItemID], [ImageURL], [USER].FirstName, [USER].ContactNo, [USER].Email
	FROM [CHEVEUX].[dbo].[Home_Page], [USER]
	Where ItemID = [USER].UserID
END
GO
/****** Object:  StoredProcedure [dbo].[SP_FeaturedProductsAndHairStyles]    Script Date: 2018/10/16 18:02:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Description:	Gets the Featured Products and hairstyles
-- =============================================
CREATE PROCEDURE [dbo].[SP_FeaturedProductsAndHairStyles] 
AS
BEGIN
	SELECT [FeatureID], [ItemID], [ImageURL], PRODUCT.Name, PRODUCT.ProductDescription, PRODUCT.Price
	FROM [CHEVEUX].[dbo].[Home_Page], PRODUCT
	Where ItemID = PRODUCT.ProductID
	Order By [FeatureID]
END
GO
/****** Object:  StoredProcedure [dbo].[SP_Get_Supplier]    Script Date: 2018/10/16 18:02:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

create PROCEDURE [dbo].[SP_Get_Supplier]
AS
BEGIN
	
    -- Insert statements for procedure here
	SELECT *
	FROM Supplier
	

END
GO
/****** Object:  StoredProcedure [dbo].[SP_GetAccountForRestCode]    Script Date: 2018/10/16 18:02:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Description:	Returns acount details for the account matchingf the reset code
-- =============================================
CREATE  PROCEDURE [dbo].[SP_GetAccountForRestCode] 
	@Code nchar(30)
AS
BEGIN
	SELECT UserName, UserID
	from [USER]
	where PassRestCode = @Code
END
GO
/****** Object:  StoredProcedure [dbo].[SP_GetAllAccessories]    Script Date: 2018/10/16 18:02:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Description:	gets all accessories in the database with details from the product Table
-- =============================================
CREATE  PROCEDURE [dbo].[SP_GetAllAccessories]
AS
BEGIN
	SELECT [ProductID]
      ,[PRODUCT].[Name]
      ,[ProductDescription]
      ,[Price]
      ,[ProductType(T/A/S)]
      ,[Active]
      ,[ProductImage]
	  ,[Colour]
      ,[Qty]
      ,BRAND.[BrandID]
	  ,BRAND.Name
	  , BRAND.[Type(T/A)],
	  s.SupplierName,
	  s.ContactName,
	  s.ContactEmail,
	  s.ContactNo,
	  s.SupplierID
	FROM [CHEVEUX].[dbo].[PRODUCT], ACCESSORY, BRAND, Supplier s
	WHERE ProductID = AccessoryID AND BRAND.BrandID = ACCESSORY.BrandID AND s.SupplierID = ACCESSORY.SupplierID 
	order by [ProductType(T/A/S)]
END
GO
/****** Object:  StoredProcedure [dbo].[SP_GetAllBookingReviews]    Script Date: 2018/10/16 18:02:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		S.MAQABANGQA
-- =============================================
CREATE PROCEDURE [dbo].[SP_GetAllBookingReviews]
AS
BEGIN
	SET NOCOUNT ON;
	SELECT r.ReviewID,r.CustomerID,r.EmployeeID,r.primaryBookingID,r.[Date],r.[Time],r.Rating,r.Comment,

		   (select UserImage
		   from [USER] stylist
		   where stylist.UserID=r.EmployeeID)as[StylistImage],

		   (select (stylist.FirstName+' '+stylist.LastName) as sName
		   from [USER] stylist
		   where stylist.UserID=r.EmployeeID)as[StylistName],

		   (select UserImage
		   from [USER] customer
		   where customer.UserID=r.CustomerID and customer.UserID=r.CustomerID)as[CustomerImage],

		   (select (customer.FirstName+' '+customer.LastName) as cName
		   from [USER] customer
		   where customer.UserID=r.CustomerID and customer.UserID=r.CustomerID)as[CustomerName]


	FROM REVIEW r
	WHERE primaryBookingID is not null
	and r.[Date] !< DATEADD(mm,-2,GETDATE())
	order by r.[Date] desc
	
END
GO
/****** Object:  StoredProcedure [dbo].[SP_GetAllBrands]    Script Date: 2018/10/16 18:02:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Description:	returns all brands in the database
-- =============================================
CREATE PROCEDURE [dbo].[SP_GetAllBrands]
AS
BEGIN
	SELECT [BrandID]
      ,[Name]
      ,[Type(T/A)]
  FROM [CHEVEUX].[dbo].[BRAND]
END
GO
/****** Object:  StoredProcedure [dbo].[SP_GetAllofBookingDTL]    Script Date: 2018/10/16 18:02:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		S.Maqabangqa
-- Create date: 04.06.2018
-- Description:	Gets all details of booking
-- =============================================
CREATE PROCEDURE [dbo].[SP_GetAllofBookingDTL]
	@BookingID nchar(10),
	@CustomerID nchar(30) null
AS
BEGIN
	Select  	cv.BookingID,
			cv.CustomerID,CONCAT(U.FirstName,' ',U.LastName)AS[CustomerName],
			B.[Date], TS.StartTime, TS.EndTime, b.Comment 
			     
	From 		BOOKING B, TIMESLOT TS, [User] U, CUST_VISIT cv

	Where 		cv.BookingID = @BookingID 
	AND 		cv.CustomerID=@CustomerID
	AND			B.BookingID=cv.BookingID
	AND			B.CustomerID=cv.CustomerID
	AND 		B.SlotNo = TS.SlotNo 
	AND 		B.StylistID = U.UserID 
END
GO
/****** Object:  StoredProcedure [dbo].[SP_GetAllProducts]    Script Date: 2018/10/16 18:02:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Description:	Gets All Products in the Product Table
-- =============================================
CREATE PROCEDURE [dbo].[SP_GetAllProducts] 
AS
BEGIN
	SELECT *
	FROM [CHEVEUX].[dbo].[PRODUCT]
	order by [ProductType(T/A/S)]
END
GO
/****** Object:  StoredProcedure [dbo].[SP_GetAllReviews]    Script Date: 2018/10/16 18:02:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		S.MAQABANGQA
-- =============================================
CREATE PROCEDURE [dbo].[SP_GetAllReviews]
AS
BEGIN
	SET NOCOUNT ON;
	SELECT ReviewID,CustomerID,EmployeeID,primaryBookingID,[Date],[Time],Rating,Comment
	FROM REVIEW
END
GO
/****** Object:  StoredProcedure [dbo].[SP_GetAllStylistReviews]    Script Date: 2018/10/16 18:02:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		S.MAQABANGQA
-- =============================================
CREATE PROCEDURE [dbo].[SP_GetAllStylistReviews]
AS
BEGIN
	SET NOCOUNT ON;
	SELECT r.ReviewID,r.CustomerID,r.EmployeeID,r.primaryBookingID,r.[Date],r.[Time],r.Rating,r.Comment,

		   (select UserImage
		   from [USER] stylist
		   where stylist.UserID=r.EmployeeID)as[StylistImage],

		   (select (stylist.FirstName+' '+stylist.LastName) as sName
		   from [USER] stylist
		   where stylist.UserID=r.EmployeeID)as[StylistName],

		   (select UserImage
		   from [USER] customer
		   where customer.UserID=r.CustomerID and customer.UserID=r.CustomerID)as[CustomerImage],

		   (select (customer.FirstName+' '+customer.LastName) as cName
		   from [USER] customer
		   where customer.UserID=r.CustomerID and customer.UserID=r.CustomerID)as[CustomerName]


	FROM [CHEVEUX].[dbo].REVIEW r
	WHERE r.primaryBookingID is null
	order by r.[Date] desc
	
END
GO
/****** Object:  StoredProcedure [dbo].[SP_GetAllTreatments]    Script Date: 2018/10/16 18:02:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Description:	gets all treatments and thier details from the product, treatment and brand tables
-- =============================================
CREATE  PROCEDURE [dbo].[SP_GetAllTreatments]
AS
BEGIN
	SELECT [ProductID]
      ,Product.Name
      ,[ProductDescription]
      ,[Price]
      ,[ProductType(T/A/S)]
      ,[Active]
      ,[ProductImage]
	  ,[Qty]
      ,[TreatmentType]
      ,BRAND.[BrandID]
	  ,BRAND.Name
	  ,Brand.[Type(T/A)],
	  s.SupplierName,
	  s.ContactName,
	  s.ContactEmail,
	  s.ContactNo,
	  s.SupplierID
	FROM [CHEVEUX].[dbo].[PRODUCT], TREATMENT, BRAND, Supplier s
	WHERE ProductID = TreatmentID AND BRAND.BrandID = TREATMENT.BrandID AND s.SupplierID = TREATMENT.SupplierID 
	order by [ProductType(T/A/S)]
END
GO
/****** Object:  StoredProcedure [dbo].[SP_GetAuto_Purchase_Products]    Script Date: 2018/10/16 18:02:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Description:	Returns all products and thier names from Auto_Purchase_Products table
-- =============================================
CREATE PROCEDURE [dbo].[SP_GetAuto_Purchase_Products]
AS
BEGIN
	SELECT P.[Name]
	   ,AP.[ProductID]
      ,AP.[Qty]
	  ,p.[ProductType(T/A/S)]
	FROM [CHEVEUX].[dbo].[Auto_Purchase_Products] AP, PRODUCT P
	Where AP.[ProductID] = P.ProductID
	order by P.[Name]
END
GO
/****** Object:  StoredProcedure [dbo].[SP_GetBio]    Script Date: 2018/10/16 18:02:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- =============================================
create PROCEDURE [dbo].[SP_GetBio]
	@EmployeeID nchar(30)
AS
BEGIN
	SET NOCOUNT ON;
	SELECT Bio
	FROM [CHEVEUX].[dbo].[EMPLOYEE]
	WHERE EmployeeID=@EmployeeID
END
GO
/****** Object:  StoredProcedure [dbo].[SP_GetBookedTimes]    Script Date: 2018/10/16 18:02:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[SP_GetBookedTimes] 
	@UserID nchar(30),
	@Date datetime
AS
BEGIN
	SELECT SlotNo
	FROM BOOKING
	WHERE StylistID = @UserID AND [Date] = @Date AND Available = 'N'
END
GO
/****** Object:  StoredProcedure [dbo].[SP_GetBookingDetailsForCustVistRecord]    Script Date: 2018/10/16 18:02:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Description:	Gets booking deatials for a booking in progress
-- =============================================
CREATE PROCEDURE [dbo].[SP_GetBookingDetailsForCustVistRecord]
	@bookingID nchar(10)
AS
BEGIN
	Select U.FirstName, B.[Date], TS.StartTime, BookingID, B.CustomerID, 
		(Select FirstName +' '+LastName
		From [USER]
		Where UserID = B.CustomerID) As custFullName
	From BOOKING B, TIMESLOT TS, [User] U
	Where BookingID = @bookingID
	AND B.SlotNo = TS.SlotNo 
	AND B.StylistID = U.UserID 
	AND B.Arrived = 'Y'
END
GO
/****** Object:  StoredProcedure [dbo].[SP_GetBookingServices]    Script Date: 2018/10/16 18:02:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Description:	Returns services for a specific booking
-- =============================================
CREATE  PROCEDURE [dbo].[SP_GetBookingServices]
	@BookingID nchar(10)
AS
BEGIN
	SELECT bs.[BookingID], bs.[ServiceID], p.[Name], p.Price, p.ProductDescription, p.[ProductType(T/A/S)]
	FROM BookingService bs, PRODUCT p
	where BookingID = @BookingID
		AND bs.[ServiceID] = p.ProductID
END
GO
/****** Object:  StoredProcedure [dbo].[SP_GetBraidService]    Script Date: 2018/10/16 18:02:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[SP_GetBraidService]
	@ServiceID nchar(10)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT s.Description AS styleDesc, w.Description AS widthDesc, l.Description AS lengthDesc
	FROM SERVICE ser, PRODUCT p, STYLE s, WIDTH w, LENGTH l, BRAID_SERVICE bs
	WHERE @ServiceID = p.ProductID AND p.ProductID = ser.ServiceID AND [ProductType(T/A/S)] = 'S'
		AND ser.ServiceID = bs.ServiceID AND bs.StyleID = s.StyleID AND bs.WidthID = w.WidthID
		AND bs.LengthID = l.LengthID
END
GO
/****** Object:  StoredProcedure [dbo].[SP_GetBrand]    Script Date: 2018/10/16 18:02:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Description:	returns Brand details given ID
-- =============================================
CREATE PROCEDURE [dbo].[SP_GetBrand]
	@BrandID nchar(10)
AS
BEGIN
	Select *
	From BRAND
	Where BrandID = @BrandID
END
GO
/****** Object:  StoredProcedure [dbo].[SP_GetBrandsForProductType]    Script Date: 2018/10/16 18:02:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		L.Human
-- =============================================
CREATE PROCEDURE [dbo].[SP_GetBrandsForProductType]
	@productType char(1)
AS
BEGIN
	SET NOCOUNT ON;
	SELECT BrandID, [Name], [Type(T/A)]
    FROM BRAND
	WHERE BRAND.[Type(T/A)]=@productType
END
GO
/****** Object:  StoredProcedure [dbo].[SP_getBusinessTable]    Script Date: 2018/10/16 18:02:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Description:	returns the business table
-- =============================================
CREATE PROCEDURE [dbo].[SP_getBusinessTable]
AS
BEGIN
	Select *
	From BUSINESS
END
GO
/****** Object:  StoredProcedure [dbo].[SP_GetCurrentVATRate2]    Script Date: 2018/10/16 18:02:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SP_GetCurrentVATRate2]
AS
BEGIN
	Select [Vat%]
	From BUSINESS
END
GO
/****** Object:  StoredProcedure [dbo].[SP_GetCustomerPastBooking]    Script Date: 2018/10/16 18:02:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SP_GetCustomerPastBooking]
	@CustID nchar(30)
AS
BEGIN
	Select U.FirstName, B.[Date], TS.StartTime, BookingID, B.Arrived, B.StylistID       
	From BOOKING B, TIMESLOT TS, [User] U
	Where CustomerID = @CustID 
	AND B.SlotNo = TS.SlotNo 
	AND B.StylistID = U.UserID
	AND (B.Arrived = 'Y' Or B.Arrived = 'N')
	And B.BookingID = B.primaryBookingID
	Order by Date desc
END
GO
/****** Object:  StoredProcedure [dbo].[SP_GetCustomerPastBookingDetail]    Script Date: 2018/10/16 18:02:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Description:	Given a booking ID it reaturns the deatails of that booking
-- =============================================
CREATE  PROCEDURE [dbo].[SP_GetCustomerPastBookingDetail] 
	@bookingID nchar(10)
AS
BEGIN
	Select U.FirstName, B.[Date], TS.StartTime, BookingID, B.Arrived , B.StylistID, 
	b.CustomerID, (select FirstName + ' '+ LastName from [USER] where [USER].UserID = b.CustomerID) as CustFullName        
	From BOOKING B, TIMESLOT TS, [User] U
	Where BookingID = @bookingID 
	AND B.SlotNo = TS.SlotNo 
	AND B.StylistID = U.UserID
	AND (B.Arrived = 'Y' Or B.Arrived = 'N')
END
GO
/****** Object:  StoredProcedure [dbo].[SP_GetCustomersReviews]    Script Date: 2018/10/16 18:02:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		S.MAQABANGQA
-- =============================================
CREATE PROCEDURE [dbo].[SP_GetCustomersReviews]
	@customerID nchar(30)
AS
BEGIN
	SET NOCOUNT ON;
	SELECT r.ReviewID,r.CustomerID,r.EmployeeID,r.primaryBookingID,r.[Date],r.[Time],r.Rating,r.Comment,


		   (select UserImage
		   from [USER] stylist
		   where stylist.UserID=r.EmployeeID)as[StylistImage],

		   (select (stylist.FirstName+' '+stylist.LastName) as sName
		   from [USER] stylist
		   where stylist.UserID=r.EmployeeID)as[StylistName],

		   (select UserImage
		   from [USER] customer
		   where customer.UserID=@customerID and customer.UserID=r.CustomerID)as[CustomerImage],

		   (select (customer.FirstName+' '+customer.LastName) as cName
		   from [USER] customer
		   where customer.UserID=@customerID and customer.UserID=r.CustomerID)as[CustomerName]


	FROM [CHEVEUX].[dbo].[REVIEW] r,[USER] u
	WHERE r.CustomerID=@customerID
	and r.CustomerID=u.UserID
	and r.[Date] !< DATEADD(mm, -2, GETDATE())
	order by r.[Date] desc
END
GO
/****** Object:  StoredProcedure [dbo].[SP_GetCustomerUpcomingBookingDetails]    Script Date: 2018/10/16 18:02:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SP_GetCustomerUpcomingBookingDetails] 
	@BookingID nchar(10)
AS
BEGIN
	Select U.UserID,  U.FirstName, B.[Date], ts.SlotNo, TS.StartTime, BookingID, 
	b.CustomerID, (select FirstName + ' '+ LastName from [USER] where [USER].UserID = b.CustomerID) as CustFullName, B.Comment           
	From BOOKING B, TIMESLOT TS, [User] U
	Where BookingID = @BookingID 
	AND B.SlotNo = TS.SlotNo 
	AND B.StylistID = U.UserID
	AND (B.Arrived is null or B.Arrived = 'N') 
END
GO
/****** Object:  StoredProcedure [dbo].[SP_GetCustomerUpcomingBookings]    Script Date: 2018/10/16 18:02:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SP_GetCustomerUpcomingBookings]
	@CustID nchar(30)
AS
BEGIN
Select U.FirstName, B.[Date], TS.StartTime, BookingID, B.Arrived , B.StylistID       
	From BOOKING B, TIMESLOT TS, [User] U
	Where CustomerID = @CustID 
	AND B.SlotNo = TS.SlotNo 
	AND B.StylistID = U.UserID
	AND B.Arrived = 'N'
	And B.BookingID = B.primaryBookingID
	Order by Date
END
GO
/****** Object:  StoredProcedure [dbo].[SP_GetEmpAgenda]    Script Date: 2018/10/16 18:02:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		S.Maqabangqa
-- Create date: 27.05.2018
-- Description:	Gets a specific employees bookings for the day.
-- =============================================
CREATE PROCEDURE [dbo].[SP_GetEmpAgenda]
	@EmployeeID nchar(30),
	@Date datetime = null,
	@sortBy nvarchar(max)=null,
	@sortDir nvarchar(max)=null
AS
BEGIN
	SET NOCOUNT ON;
	SELECT  	b.BookingID,b.primaryBookingID AS [PrimaryID],
				u.[UserID],

				ts.StartTime, ts.EndTime

				,u.[FirstName]AS[CustomerFName],
			
				(SELECT u.UserID
				 FROM [User] u
				 WHERE u.UserID=@EmployeeID)  as [EmpID],

				 (SELECT u.FirstName
				 FROM [User] u
				 WHERE u.UserID=@EmployeeID) AS [EmpFName],

				 b.Arrived, b.[Date]
				, b.Comment
	FROM BOOKING b, TIMESLOT ts, [User] u

	WHERE b.StylistID=@EmployeeID
	AND ts.SlotNo=b.SlotNo
	AND b.CustomerID=u.UserID
	AND b.[Date] = @Date
	AND (Select PaymentType
		From Sale
		Where b.BookingID = SaleID) is null
	AND (b.Arrived='N' OR b.Arrived='Y')
	AND b.BookingID=b.primaryBookingID
	
	/*ORDER BY ts.StartTime,ts.EndTime*/
	ORDER BY
	(CASE 
		 WHEN @sortBy='Stylist' AND @sortDir='Descending'
		 THEN (SELECT (u.FirstName + ' ' + u.LastName)as[stylist]
				FROM [USER] u 
				WHERE u.UserID=B.StylistID)
		 END) DESC,

		 (CASE
		  WHEN @sortBy='Date' AND @sortDir='Descending'
		  THEN B.[Date]
		  END) DESC,

		  (CASE
		  WHEN @sortBy='Customer' AND @sortDir='Descending'
		  THEN (SELECT (u.FirstName+' '+u.LastName)as[CustomerName]
		   FROM [USER] u
		   WHERE u.UserID=B.CustomerID)
		  END) DESC,

		  (CASE
		 WHEN @sortBy='Stylist' AND @sortDir='Ascending'
		 THEN (SELECT (u.FirstName + ' ' + u.LastName)as[stylist]
				FROM [USER] u 
				WHERE u.UserID=B.StylistID)
		 END) ASC,

		 (CASE
		  WHEN @sortBy='Date' AND @sortDir='Ascending'
		  THEN B.[Date]
		  END) ASC,

		  (CASE
		  WHEN @sortBy='Customer' AND @sortDir='Ascending'
		  THEN (SELECT (u.FirstName+' '+u.LastName)as[CustomerName]
		   FROM [USER] u
		   WHERE u.UserID=B.CustomerID)
		  END) ASC,
		  
		  (CASE 
		  WHEN @sortBy is null and @sortDir IS NULL 
		  THEN CONVERT(time(7),ts.StartTime)
		  END) ASC
END
GO
/****** Object:  StoredProcedure [dbo].[SP_GetEmployee]    Script Date: 2018/10/16 18:02:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		S.Maqabangqa
-- =============================================
Create PROCEDURE [dbo].[SP_GetEmployee]
@EmployeeID nchar(30)
AS
BEGIN
	Select u.UserImage,u.UserID, u.FirstName, u.LastName, u.UserName, u.Email, u.ContactNo, e.[Type], u.UserImage, 
		   u.Active, e.AddressLine1, e.AddressLine2, e.Suburb, e.City,e.Bio, ss.ServiceID,
		   p.[Name]AS[Specialisation], (p.ProductDescription)AS[SpecialisationDescription]
	From [USER] u, [CHEVEUX].[dbo].[EMPLOYEE] e, STYLIST_SERVICE ss,PRODUCT p
	Where u.UserID = @EmployeeID
		AND u.UserID = e.EmployeeID
		AND e.EmployeeID=@EmployeeID
		AND   p.ProductID = ss.ServiceID
		AND   u.UserType = 'E'
		AND   ss.EmployeeID=@EmployeeID
END
GO
/****** Object:  StoredProcedure [dbo].[SP_GetEmployeeType]    Script Date: 2018/10/16 18:02:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SP_GetEmployeeType] 
	@EmpID nchar(30)
AS
BEGIN
	Select [Type]
	from EMPLOYEE
	Where EmployeeID = @EmpID
END
GO
/****** Object:  StoredProcedure [dbo].[SP_GetEmployeeTypes]    Script Date: 2018/10/16 18:02:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Description:	Returns all emplyee types in database
-- =============================================
CREATE PROCEDURE [dbo].[SP_GetEmployeeTypes]
AS
BEGIN
	Select DISTINCT [Type]
	From EMPLOYEE
END
GO
/****** Object:  StoredProcedure [dbo].[SP_GetEmpNames]    Script Date: 2018/10/16 18:02:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		S.Maqabangqa
-- Create date: 24.05.2018
-- Description:	Gets names of active employees.
-- =============================================
CREATE PROCEDURE [dbo].[SP_GetEmpNames]
AS
BEGIN 
	SET NOCOUNT ON;
	SELECT e.EmployeeID, u.FirstName + ' ' + u.LastName AS [Name]
	FROM [CHEVEUX].[dbo].[User] u, EMPLOYEE e
	WHERE u.UserID=e.EmployeeID AND u.Active = 'T' AND e.Type ='S'
	ORDER BY  [Name] asc
END
GO
/****** Object:  StoredProcedure [dbo].[SP_getInvoiceDL]    Script Date: 2018/10/16 18:02:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SP_getInvoiceDL] 
	@BookingID nchar(10)
AS
BEGIN
	Select p.[Name], d.Qty, d.Price, p.ProductID, p.[ProductType(T/A/S)]
	From SALES_DTL d, PRODUCT p
	Where d.SaleID = @BookingID
	AND p.ProductID = d.ProductID
END
GO
/****** Object:  StoredProcedure [dbo].[SP_GetLeaveService]    Script Date: 2018/10/16 18:02:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[SP_GetLeaveService]

AS
BEGIN

	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT ProductID, [Name], NoOfSlots
	FROM PRODUCT, SERVICE
	WHERE ProductID = ServiceID AND [ProductType(T/A/S)] = 'U'
END
GO
/****** Object:  StoredProcedure [dbo].[SP_GetLengths]    Script Date: 2018/10/16 18:02:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[SP_GetLengths]

AS
BEGIN
	SELECT * 
	FROM LENGTH
END
GO
/****** Object:  StoredProcedure [dbo].[SP_GetManagerContact]    Script Date: 2018/10/16 18:02:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Description:	Returns the contact details of the manager
-- =============================================
CREATE PROCEDURE [dbo].[SP_GetManagerContact]
AS
BEGIN
	Select Email, ContactNo
	From [USER], EMPLOYEE
	where userType = 'E'
		AND EMPLOYEE.Type = 'M'
		AND [USER].UserID = EmployeeID
END
GO
/****** Object:  StoredProcedure [dbo].[SP_GetMultipleServicesTime]    Script Date: 2018/10/16 18:02:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		S.Maqabangqa
-- Description: Gets the start time and the end time for the bookings 
--              which have multiple services (service > 1 ) 
-- =============================================
CREATE PROCEDURE [dbo].[SP_GetMultipleServicesTime]
	@primaryBookingID nchar(10)
AS
BEGIN
	SET NOCOUNT ON;
	SELECT	
		(SELECT  t.StartTime 
		FROM BOOKING b, TIMESLOT t
		WHERE b.BookingID=@primaryBookingID
				AND b.primaryBookingID=@primaryBookingID
				AND b.SlotNo=t.SlotNo)AS[StartTime],


		(SELECT MAX(t.EndTime) 
		FROM BOOKING b, TIMESLOT t
		WHERE b.primaryBookingID=@primaryBookingID
			AND b.BookingID <> @primaryBookingID
			AND b.SlotNo = t.SlotNo)AS[EndTime]

END
GO
/****** Object:  StoredProcedure [dbo].[SP_GetOGBkngNoti]    Script Date: 2018/10/16 18:02:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Description: Get Out Going Booking Notifications: gets the details nessasry to send booking notifications for bookint taking place tommorow
-- =============================================
CREATE  PROCEDURE [dbo].[SP_GetOGBkngNoti]
AS
BEGIN
	SELECT BookingID
	  ,[SlotNo], (select StartTime from TIMESLOT ts where ts.SlotNo = b.[SlotNo]) as StartTime
      ,[CustomerID], u.FirstName, u.LastName, u.UserName, u.PreferredCommunication, u.Email, u.ContactNo
      ,[StylistID], (Select FirstName from [user] where UserID = [StylistID]) as StylistName
      ,[Date]
      ,[NotificationReminder]
  FROM [BOOKING] b, [USER] u
  WHERE u.UserID = b.CustomerID
		AND [NotificationReminder] = 0
		AND [Date] between getdate() and DATEADD(DAY, 1, GETDATE())
END
GO
/****** Object:  StoredProcedure [dbo].[SP_GetOutstandingProductOrders]    Script Date: 2018/10/16 18:02:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Description:	gets all outstanding product stock orders
-- =============================================
CREATE PROCEDURE [dbo].[SP_GetOutstandingProductOrders]
AS
BEGIN
	Select * 
	From [Order], Supplier
	Where Received = 'false'
		AND [Order].SupplierID = Supplier.SupplierID
END
GO
/****** Object:  StoredProcedure [dbo].[SP_GetPasHash]    Script Date: 2018/10/16 18:02:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Description:	Returnst the password for the user account matching the username or emailaddress
-- =============================================
CREATE  PROCEDURE [dbo].[SP_GetPasHash]
	@identifier nvarchar(50)
AS
BEGIN
	Select [Password]
	From [USER]
	Where (Email = @identifier
		or UserName = @identifier)
		AND [USER].Active = 'T'
END
GO
/****** Object:  StoredProcedure [dbo].[SP_GetPastProductOrders]    Script Date: 2018/10/16 18:02:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Description:	gets all past product stock orders
-- =============================================
CREATE PROCEDURE [dbo].[SP_GetPastProductOrders]
AS 
BEGIN
	Select [OrderID]
      ,[OrderDate]
      ,[Received]
      ,[DateReceived] 
	  ,[Order].[SupplierID]
      ,[SupplierName]
      ,[ContactName]
      ,[ContactNo]
      ,[AddressLine1]
      ,[AddressLine2]
      ,[Suburb]
      ,[City]
      ,[ContactEmail]
	From [Order], Supplier
	Where Received = 'true'
		AND [Order].SupplierID = Supplier.SupplierID
	order by [DateReceived] 
END
GO
/****** Object:  StoredProcedure [dbo].[SP_GetProductOrder]    Script Date: 2018/10/16 18:02:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Description:	Get the ditails of a specific order given the orderID
-- =============================================
CREATE PROCEDURE [dbo].[SP_GetProductOrder]
	@OrderID nchar(10)
AS
BEGIN
	Select [OrderID]
      ,[OrderDate]
      ,[Received]
      ,[DateReceived] 
	  ,[Order].[SupplierID]
      ,[SupplierName]
      ,[ContactName]
      ,[ContactNo]
      ,[AddressLine1]
      ,[AddressLine2]
      ,[Suburb]
      ,[City]
      ,[ContactEmail] 
	From [Order], Supplier
	Where OrderID = @OrderID
		AND [Order].SupplierID = Supplier.SupplierID
	order by [OrderDate] 
END
GO
/****** Object:  StoredProcedure [dbo].[SP_GetProductOrderDetailLines]    Script Date: 2018/10/16 18:02:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Description:	Get the detail lines of a particular order
-- =============================================
CREATE PROCEDURE [dbo].[SP_GetProductOrderDetailLines]
	-- Add the parameters for the stored procedure here
	@OrderID nchar(10)
AS
BEGIN
	Select [Order].[OrderID]
      ,[OrderDate]
      ,[Received]
      ,[DateReceived]
      ,Order_DTL.[ProductID]
	  ,[Name]
      ,[ProductDescription]
      ,[Price]
      ,[ProductType(T/A/S)] as ProductType
      ,[Active]
      ,[ProductImage]
      ,[Qty]
	  ,[Order].[SupplierID]
      ,[SupplierName]
      ,[ContactName]
      ,[ContactNo]
      ,[AddressLine1]
      ,[AddressLine2]
      ,[Suburb]
      ,[City]
      ,[ContactEmail] 
	From [Order], Order_DTL, Supplier, PRODUCT
	Where [Order].OrderID = Order_DTL.OrderID
		AND [Order].OrderID = @OrderID
		AND [Order].SupplierID = Supplier.SupplierID
		AND Order_DTL.ProductID = PRODUCT.ProductID
END
GO
/****** Object:  StoredProcedure [dbo].[SP_GetProductTypes]    Script Date: 2018/10/16 18:02:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Description:	returns the product type table
-- =============================================
CREATE PROCEDURE [dbo].[SP_GetProductTypes]
AS
BEGIN
	SELECT [TypeID]
      ,[Name]
      ,[Product/Service]
	  ,[PrimaryService]
	FROM [CHEVEUX].[dbo].[ProductType]
	order by [Name]
END
GO
/****** Object:  StoredProcedure [dbo].[SP_GetReviewsOfStylist]    Script Date: 2018/10/16 18:02:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		S.MAQABANGQA
-- =============================================
CREATE PROCEDURE [dbo].[SP_GetReviewsOfStylist]
	@stylistID nchar(30)
AS
BEGIN
	SET NOCOUNT ON;
	SELECT  r.ReviewID,r.CustomerID,r.EmployeeID,r.primaryBookingID,r.[Date],r.[Time],r.Rating,r.Comment,
			
			(select UserImage
			from [USER] customer
			where customer.UserID=r.CustomerID and customer.UserID=r.CustomerID)as[CustomerImage],

		   (select (customer.FirstName+' '+customer.LastName) as cName
		   from [USER] customer
		   where customer.UserID=r.CustomerID and customer.UserID=r.CustomerID)as[CustomerName]

	FROM REVIEW r, [USER] u
	WHERE r.EmployeeID=@stylistID
	and r.EmployeeID=u.UserID
	and r.primaryBookingID is null
	and r.[Date] !< DATEADD(mm,-2,GETDATE())
	order by r.[Date] desc
END
GO
/****** Object:  StoredProcedure [dbo].[SP_GetSale]    Script Date: 2018/10/16 18:02:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Description:	get Sale Details
-- =============================================
CREATE PROCEDURE [dbo].[SP_GetSale]
	@saleID nchar(10)
AS
BEGIN
	SELECT *
	From SALE
	Where SaleID = @saleID
END
GO
/****** Object:  StoredProcedure [dbo].[SP_GetSalePaymentType]    Script Date: 2018/10/16 18:02:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Description:	given sale id returns the payment type
-- =============================================
CREATE PROCEDURE [dbo].[SP_GetSalePaymentType] 
	@SaleID nchar(10)
AS
BEGIN
	Select PaymentType
	From SALE
	where SaleID = @SaleID
END
GO
/****** Object:  StoredProcedure [dbo].[SP_GetService]    Script Date: 2018/10/16 18:02:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[SP_GetService]
	@ServiceID nchar(10)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT [Name], ProductDescription, Price, NoOfSlots, [Type(A/N/B)] AS ServiceType
	FROM SERVICE, PRODUCT
	WHERE @ServiceID = ProductID AND ProductID = ServiceID AND [ProductType(T/A/S)] = 'S'
END
GO
/****** Object:  StoredProcedure [dbo].[SP_GetServices]    Script Date: 2018/10/16 18:02:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[SP_GetServices]
AS
BEGIN
	SELECT Name, ProductID, [Type(A/N/B)] AS ServiceType, Price, ProductDescription, Active
	FROM PRODUCT, SERVICE
	WHERE ProductID = ServiceID AND [ProductType(T/A/S)] = 'S';
END
GO
/****** Object:  StoredProcedure [dbo].[SP_GetSlotLength]    Script Date: 2018/10/16 18:02:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SP_GetSlotLength] 
	@ServiceID nchar(10)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT NoOfSlots
	FROM SERVICE
	where ServiceID = @ServiceID
END
GO
/****** Object:  StoredProcedure [dbo].[SP_GetStockManagementSettings]    Script Date: 2018/10/16 18:02:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Description:	Returns the stock management Table
-- =============================================
CREATE PROCEDURE [dbo].[SP_GetStockManagementSettings]
AS
BEGIN
	SELECT [LowStock]
      ,[PurchaseQty]
      ,[AutoPurchase]
      ,[AutoPurchaseFrequency]
      ,[AutoPurchaseProducts]
      ,[BusinessID]
	  ,[NxtOrderDate]
	FROM [CHEVEUX].[dbo].[Stock_Management]
END
GO
/****** Object:  StoredProcedure [dbo].[SP_GetStyles]    Script Date: 2018/10/16 18:02:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE  PROCEDURE [dbo].[SP_GetStyles]

AS
BEGIN
	SELECT *
	FROM STYLE
END
GO
/****** Object:  StoredProcedure [dbo].[SP_GetStylist]    Script Date: 2018/10/16 18:02:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[SP_GetStylist] 
AS
BEGIN
	SELECT [UserID], FirstName, ServiceID, p.Name
	FROM [USER] u, STYLIST_SERVICE ss, EMPLOYEE e, PRODUCT p 
	WHERE [UserID] = e.EmployeeID AND ss.EmployeeID = [UserID] AND e.Type = 'S' AND UserType = 'E'
	AND ss.ServiceID = p.ProductID 
	And p.Active = 'Y'
	Order By [FirstName]
END
GO
/****** Object:  StoredProcedure [dbo].[SP_GetStylistRating]    Script Date: 2018/10/16 18:02:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		S.MAQABANGQA
-- =============================================
CREATE PROCEDURE [dbo].[SP_GetStylistRating]
	@stylistID nchar(30)
AS
BEGIN
	SET NOCOUNT ON;
	SELECT ROUND(AVG(Rating),0) as AverageRating 
	FROM [CHEVEUX].[dbo].[REVIEW]
	WHERE EmployeeID=@stylistID
	AND primaryBookingID IS NULL
	AND [CHEVEUX].[dbo].[REVIEW].[Date] !< DATEADD(mm, -2, GETDATE())
END
GO
/****** Object:  StoredProcedure [dbo].[SP_GetTimeSlots]    Script Date: 2018/10/16 18:02:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[SP_GetTimeSlots] 

AS
BEGIN
	SELECT SlotNo, StartTime
	FROM TIMESLOT
	Order by StartTime
END
GO
/****** Object:  StoredProcedure [dbo].[SP_GetTodaysBookings]    Script Date: 2018/10/16 18:02:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Description:	gets all of todays bookings and their details
-- =============================================
CREATE PROCEDURE [dbo].[SP_GetTodaysBookings]
AS
BEGIN
SELECT [BookingID]
      ,[BOOKING].SlotNo, [StartTime], [EndTime]
      ,[CustomerID],[FirstName], [LastName]
      ,[StylistID]
      ,[Date]
      ,[Available]
      ,[Arrived]
      ,[Comment]
  FROM [CHEVEUX].[dbo].[BOOKING], [CHEVEUX].[dbo].[TIMESLOT], [CHEVEUX].[dbo].[USER]
  Where BOOKING.SlotNo = TIMESLOT.SlotNo
		AND BOOKING.CustomerID = [USER].UserID
		AND [Date] = (SELECT CAST (GETDATE() AS DATE))
		AND BookingID = primaryBookingID
	Order by TIMESLOT.StartTime
END
GO
/****** Object:  StoredProcedure [dbo].[SP_GetTodaysTotalSales]    Script Date: 2018/10/16 18:02:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Description:	returns total of todays sales
-- =============================================
CREATE PROCEDURE [dbo].[SP_GetTodaysTotalSales]
AS
BEGIN
  SELECT sum(Qty * Price )
  FROM [CHEVEUX].[dbo].[SALE], SALES_DTL
  where SALE.SaleID = SALES_DTL.SaleID
		And [Date] > DATEADD(day, DATEDIFF(day, 0, GETDATE()), 0)
		And [Date] < DATEADD(day, DATEDIFF(day, -1, GETDATE()), 0)
END
GO
/****** Object:  StoredProcedure [dbo].[SP_GetTopCustomers]    Script Date: 2018/10/16 18:02:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SP_GetTopCustomers]
	@startDate datetime,
	@endDate datetime
AS
BEGIN
select COUNT(CustomerID) as BookCount, CustomerID,
		(Select [USER].FirstName + ' '+ [USER].LastName 
		from [USER] 
		where [USER].UserID =  BOOKING.CustomerID) as custFullName
from BOOKING
where BOOKING.BookingID = BOOKING.primaryBookingID
AND   BOOKING.Date BETWEEN @startDate and @endDate
group by CustomerID
order by BookCount desc
END
GO
/****** Object:  StoredProcedure [dbo].[SP_GetTotalCusts]    Script Date: 2018/10/16 18:02:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Description:	Gets the total number of registerd customers
-- =============================================
CREATE PROCEDURE [dbo].[SP_GetTotalCusts]
AS
BEGIN
	SELECT COUNT(*)
	FROM [USER]
	where UserType = 'C'
			AND Active = 'T'
END
GO
/****** Object:  StoredProcedure [dbo].[SP_GetUserDetails]    Script Date: 2018/10/16 18:02:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SP_GetUserDetails]
	@ID nchar(30)
AS
BEGIN
	Select FirstName, LastName, UserName, Email, ContactNo, UserType, UserImage, AccountType
	From [User]
	Where [User].[UserID] = @ID And Active = 'T'
END
GO
/****** Object:  StoredProcedure [dbo].[SP_GetWidths]    Script Date: 2018/10/16 18:02:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE  PROCEDURE [dbo].[SP_GetWidths]

AS
BEGIN
	SELECT *
	FROM WIDTH
END
GO
/****** Object:  StoredProcedure [dbo].[SP_LogInEmail]    Script Date: 2018/10/16 18:02:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Description:	using the email of password returns  UserID, UserType, [FirstName]
-- =============================================
CREATE  PROCEDURE [dbo].[SP_LogInEmail] 
	-- Add the parameters for the stored procedure here
	@identifier nvarchar(50),
	@password nvarchar(max)
AS
BEGIN
	Select UserID, UserType, [FirstName]
	From [USER]
	Where Password = @password
	AND (Email = @identifier
		or UserName = @identifier)
		AND [USER].Active = 'T'
END
GO
/****** Object:  StoredProcedure [dbo].[SP_MostPopularStylist]    Script Date: 2018/10/16 18:02:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		S.MAQABANGQA
-- =============================================
CREATE PROCEDURE [dbo].[SP_MostPopularStylist]
	@startDate datetime = null,
	@endDate datetime =null
AS
BEGIN
	SET NOCOUNT ON;
	SELECT Distinct(r.EmployeeID),(u.FirstName + ' ' + u.LastName)as[EmployeeName],
		   AVG(Rating)as Rating
	FROM [CHEVEUX].[dbo].REVIEW r, [USER] u
	where r.EmployeeID=u.UserID
	and   r.[Date] between @startDate and @endDate
	and   r.primaryBookingID is null
	GROUP BY r.EmployeeID,u.FirstName,u.LastName
	ORDER BY Rating desc
END
GO
/****** Object:  StoredProcedure [dbo].[SP_NewBrand]    Script Date: 2018/10/16 18:02:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Description:	creates a brand give the paramaters
-- =============================================
CREATE PROCEDURE [dbo].[SP_NewBrand] 
	@BrandID nchar(10),
	@BrandName varchar(50), 
	@Type char(1)
AS
BEGIN
	begin try
		begin transaction
			INSERT INTO [CHEVEUX].[dbo].[BRAND](BrandID, [Name], [Type(T/A)])
			Values(@BrandID, @BrandName, @Type)
		commit transaction
	end try
	begin catch
		if @@TRANCOUNT > 0
			rollback transaction
	end catch
END
GO
/****** Object:  StoredProcedure [dbo].[SP_NewOrder]    Script Date: 2018/10/16 18:02:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Description:	craetes a new order record given the corect paramaters
-- =============================================
CREATE PROCEDURE [dbo].[SP_NewOrder] 
	@OrderID nchar(10),
	@SuppID nchar(10)
AS
BEGIN
	begin try
		begin transaction
			INSERT INTO [CHEVEUX].[dbo].[Order](OrderID, OrderDate, SupplierID, Received)
			Values(@OrderID, GETDATE(), @SuppID, 0)
		commit transaction
	end try
	begin catch
		if @@TRANCOUNT > 0
			rollback transaction
	end catch
END
GO
/****** Object:  StoredProcedure [dbo].[SP_NewOrderDL]    Script Date: 2018/10/16 18:02:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Description:	creates a new order detail line give the paramaters
-- =============================================
CREATE PROCEDURE [dbo].[SP_NewOrderDL] 
	@OrderID nchar(10),
	@ProdID nchar(10), 
	@Qty int
AS
BEGIN
	begin try
		begin transaction
			INSERT INTO [CHEVEUX].[dbo].[Order_DTL](OrderID, ProductID, Qty)
			Values(@OrderID, @ProdID, @Qty)
		commit transaction
	end try
	begin catch
		if @@TRANCOUNT > 0
			rollback transaction
	end catch
END
GO
/****** Object:  StoredProcedure [dbo].[SP_NewProductType]    Script Date: 2018/10/16 18:02:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Description:	creates a Product Type give the paramaters
-- =============================================
CREATE PROCEDURE [dbo].[SP_NewProductType]
	@typeID nchar(10),
	@typeName varchar(50), 
	@ProdOrSer char(1)
AS
BEGIN
	begin try
		begin transaction
			INSERT INTO [CHEVEUX].[dbo].[ProductType](TypeID, [Name], [Product/Service])
			Values(@typeID, @typeName, @ProdOrSer)
		commit transaction
	end try
	begin catch
		if @@TRANCOUNT > 0
			rollback transaction
	end catch
END
GO
/****** Object:  StoredProcedure [dbo].[SP_NewSupplier]    Script Date: 2018/10/16 18:02:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Description:	creates a supplier give the paramaters
-- =============================================
CREATE PROCEDURE [dbo].[SP_NewSupplier] 
	@SuppID nchar(10),
	@SuppName nchar(50),
	@ContactName nchar(50), 
	@ContactNo nchar(10), 
	@AddressL1 nchar(100), 
	@AddressL2 nchar(100), 
	@Suburb nchar(100), 
	@City nchar(100), 
	@ContactEmail varchar(50)
AS
BEGIN
	begin try
		begin transaction
			INSERT INTO [CHEVEUX].[dbo].[Supplier](SupplierID, SupplierName, ContactName, ContactNo, AddressLine1, AddressLine2, Suburb, City, ContactEmail)
			Values(@SuppID, @SuppName, @ContactName, @ContactNo, @AddressL1, @AddressL2, @Suburb, @City, @ContactEmail)
		commit transaction
	end try
	begin catch
		if @@TRANCOUNT > 0
			rollback transaction
	end catch
END
GO
/****** Object:  StoredProcedure [dbo].[SP_Products]    Script Date: 2018/10/16 18:02:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[SP_Products]

@productID nchar(10)



AS
BEGIN
	
SELECT ProductID, Product.Name, ProductDescription, Price, [ProductType(T/A/S)], Active, ProductImage, colour, qty,BRAND.BrandID, BRAND.Name, BRAND.[Type(T/A)]


 FROM PRODUCT, ACCESSORY, BRAND
 Where ProductID = AccessoryID AND BRAND.BrandID = ACCESSORY.BrandID
 ORDER BY [ProductType(T/A/S)]

END
GO
/****** Object:  StoredProcedure [dbo].[SP_ProductSalesReportByValue]    Script Date: 2018/10/16 18:02:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Description:	Get the product sales order by value
-- =============================================
CREATE PROCEDURE [dbo].[SP_ProductSalesReportByValue] 
	@StartDate DateTime,
	@EndDate DateTime
AS
BEGIN
	SELECT sum(sd.Price) as [Value], sum(sd.Qty) as Volume, sd.ProductID, max(p.[Name]) as [Product]
  FROM [CHEVEUX].[dbo].[SALES_DTL] sd, [CHEVEUX].[dbo].PRODUCT p, [CHEVEUX].[dbo].SALE s
  Where p.ProductID = sd.ProductID
	AND p.[ProductType(T/A/S)] != 'S'
	AND sd.SaleID = s.SaleID
	AND s.[Date]  between @StartDate and @EndDate
  Group by sd.ProductID
  order by [Value] desc
END
GO
/****** Object:  StoredProcedure [dbo].[SP_ProductSalesReportByValueCash]    Script Date: 2018/10/16 18:02:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Description:	Get the product sales order by value fore cash sales
-- =============================================
CREATE PROCEDURE [dbo].[SP_ProductSalesReportByValueCash] 
	@StartDate DateTime,
	@EndDate DateTime
AS
BEGIN
	SELECT sum(sd.Price) as [Value], sum(sd.Qty) as Volume, sd.ProductID, max(p.[Name]) as [Product]
  FROM [CHEVEUX].[dbo].[SALES_DTL] sd, [CHEVEUX].[dbo].PRODUCT p, [CHEVEUX].[dbo].SALE s
  Where p.ProductID = sd.ProductID
	AND p.[ProductType(T/A/S)] != 'S'
	AND sd.SaleID = s.SaleID
	AND s.[Date]  between @StartDate and @EndDate
	AND s.PaymentType = 'Cash'
  Group by sd.ProductID
  order by [Value] desc
END
GO
/****** Object:  StoredProcedure [dbo].[SP_ProductSalesReportByValueCredit]    Script Date: 2018/10/16 18:02:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Description:	Get the product sales order by value fore credit sales
-- =============================================
CREATE PROCEDURE [dbo].[SP_ProductSalesReportByValueCredit] 
	@StartDate DateTime,
	@EndDate DateTime
AS
BEGIN
	SELECT sum(sd.Price) as [Value], sum(sd.Qty) as Volume, sd.ProductID, max(p.[Name]) as [Product]
  FROM [CHEVEUX].[dbo].[SALES_DTL] sd, [CHEVEUX].[dbo].PRODUCT p, [CHEVEUX].[dbo].SALE s
  Where p.ProductID = sd.ProductID
	AND p.[ProductType(T/A/S)] != 'S'
	AND sd.SaleID = s.SaleID
	AND s.[Date]  between @StartDate and @EndDate
	AND s.PaymentType = 'Credit'
  Group by sd.ProductID
  order by [Value] desc
END
GO
/****** Object:  StoredProcedure [dbo].[SP_ProductSalesReportByVolume]    Script Date: 2018/10/16 18:02:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Description:	Get the product sales order by Volume
-- =============================================
CREATE PROCEDURE [dbo].[SP_ProductSalesReportByVolume] 
	@StartDate DateTime,
	@EndDate DateTime
AS
BEGIN
	SELECT sum(sd.Price) as [Value], sum(sd.Qty) as Volume, sd.ProductID, max(p.[Name]) as [Product]
  FROM [CHEVEUX].[dbo].[SALES_DTL] sd, [CHEVEUX].[dbo].PRODUCT p, [CHEVEUX].[dbo].SALE s
  Where p.ProductID = sd.ProductID
	AND sd.SaleID = s.SaleID
	AND p.[ProductType(T/A/S)] != 'S'
	AND s.[Date]  between @StartDate and @EndDate
  Group by sd.ProductID
  order by [Volume] desc
END
GO
/****** Object:  StoredProcedure [dbo].[SP_ProductSalesReportByVolumeCash]    Script Date: 2018/10/16 18:02:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Description:	Get the product sales order by Volume for cash sales
-- =============================================
CREATE PROCEDURE [dbo].[SP_ProductSalesReportByVolumeCash] 
	@StartDate DateTime,
	@EndDate DateTime
AS
BEGIN
	SELECT sum(sd.Price) as [Value], sum(sd.Qty) as Volume, sd.ProductID, max(p.[Name]) as [Product]
  FROM [CHEVEUX].[dbo].[SALES_DTL] sd, [CHEVEUX].[dbo].PRODUCT p, [CHEVEUX].[dbo].SALE s
  Where p.ProductID = sd.ProductID
	AND sd.SaleID = s.SaleID
	AND p.[ProductType(T/A/S)] != 'S'
	AND s.[Date]  between @StartDate and @EndDate
	AND s.PaymentType = 'Cash'
  Group by sd.ProductID
  order by [Volume] desc
END
GO
/****** Object:  StoredProcedure [dbo].[SP_ProductSalesReportByVolumeCredit]    Script Date: 2018/10/16 18:02:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Description:	Get the product sales order by Volume for Credit sales
-- =============================================
CREATE PROCEDURE [dbo].[SP_ProductSalesReportByVolumeCredit]
	@StartDate DateTime,
	@EndDate DateTime
AS
BEGIN
	SELECT sum(sd.Price) as [Value], sum(sd.Qty) as Volume, sd.ProductID, max(p.[Name]) as [Product]
  FROM [CHEVEUX].[dbo].[SALES_DTL] sd, [CHEVEUX].[dbo].PRODUCT p, [CHEVEUX].[dbo].SALE s
  Where p.ProductID = sd.ProductID
	AND sd.SaleID = s.SaleID
	AND p.[ProductType(T/A/S)] != 'S'
	AND s.[Date]  between @StartDate and @EndDate
	AND s.PaymentType = 'Credit'
  Group by sd.ProductID
  order by [Volume] desc
END
GO
/****** Object:  StoredProcedure [dbo].[SP_ProductSearchByTerm]    Script Date: 2018/10/16 18:02:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SP_ProductSearchByTerm]
	@searchTerm varchar(50)
AS
BEGIN
	select P.Name, P.ProductDescription, P.Price, P.[ProductType(T/A/S)], P.ProductID
	From PRODUCT P
	Where (P.Name like '%'+@searchTerm+'%'  Or P.ProductDescription like '%'+@searchTerm+'%') AND P.Active = 'Y'
	Order By P.[ProductType(T/A/S)]
END
GO
/****** Object:  StoredProcedure [dbo].[SP_RemoveProductSalesDTLRecord]    Script Date: 2018/10/16 18:02:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE  PROCEDURE [dbo].[SP_RemoveProductSalesDTLRecord]
	@SaleID nchar(10), 
	@ProductID nchar(10),
	@Qty int
AS
BEGIN
	begin try
		begin transaction
				Delete From SALES_DTL
				Where ProductID = @ProductID
					AND SaleID = @SaleID

				UPDATE TREATMENT
				SET Qty = (Select Qty+@Qty From TREATMENT Where TreatmentID = @ProductID)
				WHERE TreatmentID = @ProductID

				UPDATE ACCESSORY
				SET Qty = (Select Qty+@Qty From ACCESSORY Where AccessoryID = @ProductID)
				WHERE AccessoryID = @ProductID
		commit transaction
	end try
	begin catch
		if @@TRANCOUNT > 0
			rollback transaction
	end catch
END
GO
/****** Object:  StoredProcedure [dbo].[SP_ReturnAvailServices]    Script Date: 2018/10/16 18:02:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		S.MAQABANGQA
-- =============================================
CREATE PROCEDURE [dbo].[SP_ReturnAvailServices]
	@slots int
AS
BEGIN

	SET NOCOUNT ON;

	SELECT  s.ServiceID, p.[Name],p.ProductDescription,p.Price,
			s.NoOfSlots,s.[Type(A/N/B)]
	FROM [SERVICE] s, PRODUCT p
	WHERE s.ServiceID=p.ProductID
	and p.Active ='Y'
	and s.NoOfSlots <= @slots
	and s.[Type(A/N/B)]<>'U'

END
GO
/****** Object:  StoredProcedure [dbo].[SP_ReturnBooking]    Script Date: 2018/10/16 18:02:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		S.MAQABANGQA
-- =============================================
CREATE PROCEDURE [dbo].[SP_ReturnBooking]
	@bookingID nchar(10),
	@customerID nchar(30),
	@stylistID nchar(30),
	@date datetime
AS
BEGIN
	SET NOCOUNT ON;
	SELECT b.BookingID,b.CustomerID,b.StylistID,b.SlotNo,ts.StartTime,b.[Date]
	FROM BOOKING b, TIMESLOT ts
	WHERE b.BookingID=@bookingID
	and   b.CustomerID=@customerID
	and   b.StylistID=@stylistID
	and   b.[Date]=@date
	and   b.SlotNo=ts.SlotNo
END
GO
/****** Object:  StoredProcedure [dbo].[SP_ReturnNextBooking]    Script Date: 2018/10/16 18:02:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		S.MAQABANGQA
-- =============================================
CREATE PROCEDURE [dbo].[SP_ReturnNextBooking]
	@startTime nvarchar(20),
	@bookingID nchar(10),
	@stylistID nchar(30),
	@date datetime
AS
BEGIN
	SET NOCOUNT ON;

	SELECT top(1) b.BookingID,b.SlotNo,ts.StartTime
	FROM BOOKING b, TIMESLOT ts
	WHERE b.BookingID=b.primaryBookingID
	and b.BookingID <> @bookingID
	and b.StylistID=@stylistID
	and b.[Date]=@date
	and b.SlotNo=ts.SlotNo
	and ts.StartTime !< @startTime
	
	ORDER BY ts.StartTime
END
GO
/****** Object:  StoredProcedure [dbo].[SP_ReturnStylistNamesForReview]    Script Date: 2018/10/16 18:02:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		S.MAQABANGQA
-- Description:	
-- =============================================
create PROCEDURE [dbo].[SP_ReturnStylistNamesForReview]
	@customerID nchar(30)
AS
BEGIN
	SET NOCOUNT ON;
	
	SELECT distinct B.StylistID, 
				(U.FirstName + ' ' + U.LastName)as[StylistName]
  		
	FROM [CHEVEUX].[dbo].[BOOKING] B,EMPLOYEE E, [USER] U
	WHERE B.BookingID = B.[primaryBookingID]
	AND    B.StylistID = E.EmployeeID
	AND    B.StylistID=U.UserID
	AND	   B.Arrived = 'Y'
	AND	   B.StylistID= B.StylistID
	AND	   B.[Date] !> CAST(GETDATE() AS DATE)
	AND	   B.CustomerID=@customerID

END
GO
/****** Object:  StoredProcedure [dbo].[SP_ReviewBooking]    Script Date: 2018/10/16 18:02:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		S.Maqabangqa
-- =============================================
CREATE PROCEDURE [dbo].[SP_ReviewBooking]
	@reviewID nchar(10),
	@customerID nchar(30),
	@employeeID nchar(30),
	@primaryBookingID nchar(10),
	@date datetime,
	@time time(7),
	@rating float =  null,
	@comment varchar(50)= null
AS
BEGIN

	BEGIN TRY
		BEGIN TRANSACTION 
			INSERT INTO REVIEW
					   (ReviewID,CustomerID,
					   EmployeeID,primaryBookingID,
					   [Date],[Time],
					   Rating,Comment)
			VALUES	   (@reviewID,@customerID,
						@employeeID,@primaryBookingID,
						@date,@time,@rating,
						@comment)
		COMMIT TRANSACTION
		END TRY
	BEGIN CATCH
		if @@TRANCOUNT > 0
			ROLLBACK TRANSACTION
	END CATCH

END
GO
/****** Object:  StoredProcedure [dbo].[SP_ReviewStylist]    Script Date: 2018/10/16 18:02:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		S.MAQABANGQA
-- =============================================
CREATE PROCEDURE [dbo].[SP_ReviewStylist]
	@reviewID nchar(10),
	@customerID nchar(30),
	@employeeID nchar(30),
	@date datetime,
	@time time(7),
	@rating float = null,
	@comment varchar(50) = null
AS
BEGIN
	BEGIN TRY
		BEGIN TRANSACTION 
			INSERT INTO REVIEW
					   (ReviewID,CustomerID,
					   EmployeeID,
					   [Date],[Time],
					   Rating,Comment)
			VALUES	   (@reviewID,@customerID,
						@employeeID,
						@date,@time,@rating,
						@comment)
		COMMIT TRANSACTION
		END TRY
	BEGIN CATCH
		if @@TRANCOUNT > 0
			ROLLBACK TRANSACTION
	END CATCH
END
GO
/****** Object:  StoredProcedure [dbo].[SP_SaleOfHairstylist]    Script Date: 2018/10/16 18:02:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Description:	Sales for a hairstylist
CREATE PROCEDURE [dbo].[SP_SaleOfHairstylist] 
	@StylistID nchar(30),
	@startDate datetime,
	@endDate datetime
AS

BEGIN

Select s.SaleID, s.[Date],(u.FirstName + ' ' + u.LastName) AS[FullName], s.CustomerID, s.BookingID, s.PaymentType
From SALE s, [USER] u, BOOKING b
where b.BookingID = s.BookingID
	AND b.CustomerID = s.CustomerID
	AND b.CustomerID = u.UserID
	AND b.StylistID = @StylistID
	and s.[Date] BETWEEN @startDate   AND @endDate
END
GO
/****** Object:  StoredProcedure [dbo].[SP_SalesGaugeFroProduct]    Script Date: 2018/10/16 18:02:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Description:	Get the data for the sales gauge for a spacific Product
-- =============================================
CREATE PROCEDURE [dbo].[SP_SalesGaugeFroProduct] 
	@prodID nchar(10)
AS
BEGIN
	SELECT sum(sd.Price) as [Value], sum(sd.Qty) as Volume, sd.ProductID, max(p.[Name]) as [Product]
  FROM [CHEVEUX].[dbo].[SALES_DTL] sd, [CHEVEUX].[dbo].PRODUCT p, [CHEVEUX].[dbo].SALE s
  Where p.ProductID = sd.ProductID
	AND p.ProductID = @prodID
	AND sd.SaleID = s.SaleID
	AND s.[Date]  between dateadd(day, -30, getdate()) and GETDATE( ) 
  Group by sd.ProductID
END
GO
/****** Object:  StoredProcedure [dbo].[SP_SalesGaugeTotals]    Script Date: 2018/10/16 18:02:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Description:	Get the data for the sales gauge
-- =============================================
CREATE PROCEDURE [dbo].[SP_SalesGaugeTotals]
AS
BEGIN
	SELECT sum(sd.Price) as [Value], sum(sd.Qty) as Volume, sd.ProductID, max(p.[Name]) as [Product]
  FROM [CHEVEUX].[dbo].[SALES_DTL] sd, [CHEVEUX].[dbo].PRODUCT p, [CHEVEUX].[dbo].SALE s
  Where p.ProductID = sd.ProductID
	AND p.[ProductType(T/A/S)] != 'A'
	AND p.[ProductType(T/A/S)] != 'T'
	AND sd.SaleID = s.SaleID
	AND s.[Date]  between dateadd(day, -30, getdate()) and GETDATE( )
  Group by sd.ProductID 
  order by [Value] asc
END
GO
/****** Object:  StoredProcedure [dbo].[SP_SearchBookings]    Script Date: 2018/10/16 18:02:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SP_SearchBookings]
	@startDate datetime,
	@endDate datetime
AS
BEGIN
	Select U.FirstName as StylistFirstName, B.[Date], TS.StartTime, BookingID, B.Arrived, B.StylistID, B.Comment, B.CustomerID, 
			(Select U.FirstName+ ' '+U.LastName
			 From [User] U
			 Where b.CustomerID = u.UserID) as CustFirstName          
	From BOOKING B, TIMESLOT TS, [User] U
	Where B.SlotNo = TS.SlotNo 
	AND B.StylistID = U.UserID
	AND (B.Arrived = 'Y' Or B.Arrived = 'N')
	And B.BookingID = B.primaryBookingID

	AND B.[Date] BETWEEN @startDate   AND @endDate

	Order by Date desc
END
GO
/****** Object:  StoredProcedure [dbo].[SP_SearchByTermAndType]    Script Date: 2018/10/16 18:02:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SP_SearchByTermAndType]
	@SearchTerm nchar(50),
	@ProdType char(1)
AS
BEGIN
	select P.Name, P.ProductDescription, P.Price, P.ProductID
	From PRODUCT P
	Where P.[ProductType(T/A/S)] = @ProdType AND P.Name like '%'+@SearchTerm+'%' Or P.ProductDescription like '%'+@SearchTerm+'%'
END
GO
/****** Object:  StoredProcedure [dbo].[SP_SearchForUser]    Script Date: 2018/10/16 18:02:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		S.Maqabangqa
-- =============================================
CREATE PROCEDURE [dbo].[SP_SearchForUser]
	@searchTerm varchar(50)
AS
BEGIN

	SET NOCOUNT ON;

	SELECT u.UserImage,u.UserID, (u.FirstName + ' ' +u.LastName)AS[FullName] , u.UserName,u.Email,u.ContactNo
	FROM [USER] u
	WHERE (u.UserImage like '%'+@searchTerm+'%'
		   Or u.UserID like '%'+@searchTerm+'%'  
		   Or u.FirstName like '%'+@searchTerm+'%'
		   Or u.LastName like '%'+@searchTerm+'%'
		   Or u.FirstName + ' ' + u.LastName like '%'+@searchTerm+'%'
		   Or u.UserName like '%'+@searchTerm+'%'
		   Or u.Email like '%'+@searchTerm+'%'
		   Or u.ContactNo like '%'+@searchTerm+'%')
		   AND u.Active = 'T'
		ORDER BY (u.FirstName + ' ' +u.LastName) ASC
END
GO
/****** Object:  StoredProcedure [dbo].[SP_SearchStylistsBySearchTerm]    Script Date: 2018/10/16 18:02:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SP_SearchStylistsBySearchTerm]
	@searchTerm varchar(50)
AS
BEGIN
	Select U.UserID, U.FirstName, U.LastName, U.UserImage
	From [User] U, EMPLOYEE E
	Where (U.FirstName like '%'+@searchTerm+'%'  Or U.LastName like '%'+@searchTerm+'%')
	And U.UserID = E.EmployeeID 
	And E.Type= 'S' 
	AND U.Active = 'T'
END
GO
/****** Object:  StoredProcedure [dbo].[SP_SelectAccessory]    Script Date: 2018/10/16 18:02:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SP_SelectAccessory]
	@productID nchar(10)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	SELECT [CHEVEUX].[dbo].[PRODUCT].[ProductID]
      ,[CHEVEUX].[dbo].[PRODUCT].[Name]
      ,[CHEVEUX].[dbo].[PRODUCT].[ProductDescription]
      ,[CHEVEUX].[dbo].[PRODUCT].[Price]
      ,[CHEVEUX].[dbo].[PRODUCT].[ProductType(T/A/S)]
	  ,[CHEVEUX].[dbo].ACCESSORY.Colour
	  ,BRAND.[BrandID]
	  ,BRAND.[Name] AS [BrandName]
	  ,[ACCESSORY].SupplierID 
	  ,Supplier.SupplierName
	  ,ACCESSORY.Qty
	
	 FROM [CHEVEUX].[dbo].[PRODUCT], [CHEVEUX].[dbo].[ACCESSORY], BRAND, [CHEVEUX].[dbo].[Supplier]
	WHERE [CHEVEUX].[dbo].[PRODUCT].ProductID = AccessoryID AND BRAND.BrandID = ACCESSORY.BrandID AND [CHEVEUX].[dbo].[Supplier].[SupplierID] = [CHEVEUX].[dbo].[ACCESSORY].SupplierID And [CHEVEUX].[dbo].[PRODUCT].ProductID = @productID
END


GO
/****** Object:  StoredProcedure [dbo].[SP_SelectTreatment]    Script Date: 2018/10/16 18:02:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SP_SelectTreatment]
	@productID nchar(10)
	AS
BEGIN
	  SELECT [ProductID]
      ,PRODUCT.[Name]
      ,[ProductDescription]
      ,[Price]
      ,[ProductType(T/A/S)]
      ,[Active]
	  ,[TreatmentType]
      ,BRAND.[BrandID]
	  ,BRAND.[Name]	AS [BrandName]
	  ,[TREATMENT].[SupplierID]
	  ,Supplier.SupplierName
	  ,TREATMENT.Qty
	  ,TreatmentType
	  
	FROM [CHEVEUX].[dbo].[PRODUCT], [CHEVEUX].[dbo].[TREATMENT], BRAND, [CHEVEUX].[dbo].Supplier
	WHERE ProductID = TreatmentID AND BRAND.BrandID = TREATMENT.BrandID AND [CHEVEUX].[dbo].[Supplier].[SupplierID] = [CHEVEUX].[dbo].[TREATMENT].[SupplierID] AND ProductID = @productID
	AND TREATMENT.SupplierID = Supplier.SupplierID
	order by [ProductType(T/A/S)]
END


GO
/****** Object:  StoredProcedure [dbo].[SP_ServiceSalesReportByValue]    Script Date: 2018/10/16 18:02:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Description:	Get the product sales order by value
-- =============================================
CREATE PROCEDURE [dbo].[SP_ServiceSalesReportByValue] 
	@StartDate DateTime,
	@EndDate DateTime
AS
BEGIN
	SELECT sum(sd.Price) as [Value], sum(sd.Qty) as Volume, sd.ProductID, max(p.[Name]) as [Product]
  FROM [CHEVEUX].[dbo].[SALES_DTL] sd, [CHEVEUX].[dbo].PRODUCT p, [CHEVEUX].[dbo].SALE s
  Where p.ProductID = sd.ProductID
	AND p.[ProductType(T/A/S)] != 'A'
	AND p.[ProductType(T/A/S)] != 'T'
	AND sd.SaleID = s.SaleID
	AND s.[Date]  between @StartDate and @EndDate
  Group by sd.ProductID
  order by [Value] desc
END
GO
/****** Object:  StoredProcedure [dbo].[SP_ServiceSalesReportByValueCash]    Script Date: 2018/10/16 18:02:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Description:	Get the product sales order by value fore cash sales
-- =============================================
CREATE PROCEDURE [dbo].[SP_ServiceSalesReportByValueCash] 
	@StartDate DateTime,
	@EndDate DateTime
AS
BEGIN
	SELECT sum(sd.Price) as [Value], sum(sd.Qty) as Volume, sd.ProductID, max(p.[Name]) as [Product]
  FROM [CHEVEUX].[dbo].[SALES_DTL] sd, [CHEVEUX].[dbo].PRODUCT p, [CHEVEUX].[dbo].SALE s
  Where p.ProductID = sd.ProductID
	AND p.[ProductType(T/A/S)] != 'A'
	AND p.[ProductType(T/A/S)] != 'T'
	AND sd.SaleID = s.SaleID
	AND s.[Date]  between @StartDate and @EndDate
	AND s.PaymentType = 'Cash'
  Group by sd.ProductID
  order by [Value] desc
END
GO
/****** Object:  StoredProcedure [dbo].[SP_ServiceSalesReportByValueCredit]    Script Date: 2018/10/16 18:02:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Description:	Get the product sales order by value fore credit sales
-- =============================================
CREATE PROCEDURE [dbo].[SP_ServiceSalesReportByValueCredit] 
	@StartDate DateTime,
	@EndDate DateTime
AS 
BEGIN
	SELECT sum(sd.Price) as [Value], sum(sd.Qty) as Volume, sd.ProductID, max(p.[Name]) as [Product]
  FROM [CHEVEUX].[dbo].[SALES_DTL] sd, [CHEVEUX].[dbo].PRODUCT p, [CHEVEUX].[dbo].SALE s
  Where p.ProductID = sd.ProductID
	AND p.[ProductType(T/A/S)] != 'A'
	AND p.[ProductType(T/A/S)] != 'T'
	AND sd.SaleID = s.SaleID
	AND s.[Date]  between @StartDate and @EndDate
	AND s.PaymentType = 'Credit'
  Group by sd.ProductID
  order by [Value] desc
END
GO
/****** Object:  StoredProcedure [dbo].[SP_ServiceSalesReportByVolume]    Script Date: 2018/10/16 18:02:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Description:	Get the product sales order by Volume
-- =============================================
CREATE PROCEDURE [dbo].[SP_ServiceSalesReportByVolume] 
	@StartDate DateTime,
	@EndDate DateTime
AS
BEGIN
	SELECT sum(sd.Price) as [Value], sum(sd.Qty) as Volume, sd.ProductID, max(p.[Name]) as [Product]
  FROM [CHEVEUX].[dbo].[SALES_DTL] sd, [CHEVEUX].[dbo].PRODUCT p, [CHEVEUX].[dbo].SALE s
  Where p.ProductID = sd.ProductID
	AND sd.SaleID = s.SaleID
	AND p.[ProductType(T/A/S)] != 'A'
	AND p.[ProductType(T/A/S)] != 'T'
	AND s.[Date]  between @StartDate and @EndDate
  Group by sd.ProductID
  order by [Volume] desc
END
GO
/****** Object:  StoredProcedure [dbo].[SP_ServiceSalesReportByVolumeCash]    Script Date: 2018/10/16 18:02:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Description:	Get the product sales order by Volume for cash sales
-- =============================================
CREATE PROCEDURE [dbo].[SP_ServiceSalesReportByVolumeCash] 
	@StartDate DateTime,
	@EndDate DateTime
AS
BEGIN
	SELECT sum(sd.Price) as [Value], sum(sd.Qty) as Volume, sd.ProductID, max(p.[Name]) as [Product]
  FROM [CHEVEUX].[dbo].[SALES_DTL] sd, [CHEVEUX].[dbo].PRODUCT p, [CHEVEUX].[dbo].SALE s
  Where p.ProductID = sd.ProductID
	AND sd.SaleID = s.SaleID
	AND p.[ProductType(T/A/S)] != 'A'
	AND p.[ProductType(T/A/S)] != 'T'
	AND s.[Date]  between @StartDate and @EndDate
	AND s.PaymentType = 'Cash'
  Group by sd.ProductID
  order by [Volume] desc
END
GO
/****** Object:  StoredProcedure [dbo].[SP_ServiceSalesReportByVolumeCredit]    Script Date: 2018/10/16 18:02:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Description:	Get the product sales order by Volume for Credit sales
-- =============================================
CREATE PROCEDURE [dbo].[SP_ServiceSalesReportByVolumeCredit]
	@StartDate DateTime,
	@EndDate DateTime
AS
BEGIN
	SELECT sum(sd.Price) as [Value], sum(sd.Qty) as Volume, sd.ProductID, max(p.[Name]) as [Product]
  FROM [CHEVEUX].[dbo].[SALES_DTL] sd, [CHEVEUX].[dbo].PRODUCT p, [CHEVEUX].[dbo].SALE s
  Where p.ProductID = sd.ProductID
	AND sd.SaleID = s.SaleID
	AND p.[ProductType(T/A/S)] != 'A'
	AND p.[ProductType(T/A/S)] != 'T'
	AND s.[Date]  between @StartDate and @EndDate
	AND s.PaymentType = 'Credit'
  Group by sd.ProductID
  order by [Volume] desc
END
GO
/****** Object:  StoredProcedure [dbo].[SP_ServiceSearchByTerm]    Script Date: 2018/10/16 18:02:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SP_ServiceSearchByTerm]
	@searchTerm varchar(50)
AS
BEGIN
	select P.Name, P.ProductDescription, P.Price, P.[ProductType(T/A/S)], P.ProductID
	From PRODUCT P
	Where P.ProductID not in
			(SELECT [ServiceID] FROM [CHEVEUX].[dbo].[BRAID_SERVICE])
	AND (P.Name like '%'+@searchTerm+'%'  Or P.ProductDescription like '%'+@searchTerm+'%') AND P.Active = 'Y'
	AND P.[ProductType(T/A/S)] = 'S'
	Order By P.[ProductType(T/A/S)]
END
GO
/****** Object:  StoredProcedure [dbo].[SP_StylistPastBksForDate]    Script Date: 2018/10/16 18:02:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		S.Maqabangqa
-- Description:	Gets stylists past bookings for a specific date
-- =============================================
CREATE PROCEDURE [dbo].[SP_StylistPastBksForDate]
	@stylistID nchar(30),
	@day datetime,
	@sortBy nvarchar(max)=null,
	@sortDir nvarchar(max)=null
AS
BEGIN
	SET NOCOUNT ON;
	SELECT BookingID,b.primaryBookingID AS [PrimaryID],B.StylistID,B.CustomerID,
			
		   (SELECT (u.FirstName + ' ' + u.LastName)as[StylistName]
		   FROM [USER] u
		   WHERE u.UserID = @stylistID)AS[StylistName],

		   (SELECT (u.FirstName+' '+u.LastName)as[CustomerName]
		   FROM [USER] u
		   WHERE u.UserID=B.CustomerID)AS[FullName],

		   B.[Date],TS.StartTime,TS.EndTime,B.Arrived

	From   [CHEVEUX].[dbo].[BOOKING] B, TIMESLOT TS, [User] U, EMPLOYEE e
	Where  b.StylistID = @stylistID
	AND		b.StylistID = e. EmployeeID
	AND    B.SlotNo = TS.SlotNo 
	AND    B.CustomerID = U.UserID 
	AND	   (B.Arrived = 'Y' or B.CustomerID='0')
	AND    B.[Date] = @day
	AND	   B.[Date] !> CAST(GETDATE() AS DATE)
	and b.BookingID=b.primaryBookingID
	ORDER BY
	(CASE 
		 WHEN @sortBy='Stylist' AND @sortDir='Descending'
		 THEN (SELECT (u.FirstName + ' ' + u.LastName)as[stylist]
				FROM [USER] u 
				WHERE u.UserID=B.StylistID)
		 END) DESC,

		 (CASE
		  WHEN @sortBy='Date' AND @sortDir='Descending'
		  THEN B.[Date]
		  END) DESC,

		  (CASE
		  WHEN @sortBy='Customer' AND @sortDir='Descending'
		  THEN (SELECT (u.FirstName+' '+u.LastName)as[CustomerName]
		   FROM [USER] u
		   WHERE u.UserID=B.CustomerID)
		  END) DESC,

		  (CASE
		 WHEN @sortBy='Stylist' AND @sortDir='Ascending'
		 THEN (SELECT (u.FirstName + ' ' + u.LastName)as[stylist]
				FROM [USER] u 
				WHERE u.UserID=B.StylistID)
		 END) ASC,

		 (CASE
		  WHEN @sortBy='Date' AND @sortDir='Ascending'
		  THEN B.[Date]
		  END) ASC,

		  (CASE
		  WHEN @sortBy='Customer' AND @sortDir='Ascending'
		  THEN (SELECT (u.FirstName+' '+u.LastName)as[CustomerName]
		   FROM [USER] u
		   WHERE u.UserID=B.CustomerID)
		  END) ASC

END
GO
/****** Object:  StoredProcedure [dbo].[SP_StylistPastBookings]    Script Date: 2018/10/16 18:02:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		S.Maqabangqa
-- =============================================
CREATE PROCEDURE [dbo].[SP_StylistPastBookings] 
	@stylistID nchar(30),
	@sortBy nvarchar(max)=null,
	@sortDir nvarchar(max)=null
AS
BEGIN
	SET NOCOUNT ON;
	SELECT BookingID,b.primaryBookingID AS [PrimaryID],B.StylistID,B.CustomerID,
			
		   (SELECT (u.FirstName + ' ' + u.LastName)as[StylistName]
		   FROM [USER] u
		   WHERE u.UserID = @stylistID)AS[StylistName],

		   (SELECT (u.FirstName+' '+u.LastName)as[CustomerName]
		   FROM [USER] u
		   WHERE u.UserID=B.CustomerID)AS[FullName],

		   B.[Date],TS.StartTime,TS.EndTime,B.Arrived

	From   [CHEVEUX].[dbo].[BOOKING] B, TIMESLOT TS, [User] U, EMPLOYEE e
	Where  b.StylistID = @stylistID
	AND		b.StylistID = e. EmployeeID
	AND    B.SlotNo = TS.SlotNo 
	AND    B.CustomerID = U.UserID 
	AND	   (B.Arrived = 'Y' or B.CustomerID='0')
	AND	   B.[Date] !> CAST(GETDATE() AS DATE)
	and b.BookingID=b.primaryBookingID
	ORDER BY
	(CASE 
		 WHEN @sortBy='Stylist' AND @sortDir='Descending'
		 THEN (SELECT (u.FirstName + ' ' + u.LastName)as[stylist]
				FROM [USER] u 
				WHERE u.UserID=B.StylistID)
		 END) DESC,

		 (CASE
		  WHEN @sortBy='Date' AND @sortDir='Descending'
		  THEN B.[Date]
		  END) DESC,

		  (CASE
		  WHEN @sortBy='Customer' AND @sortDir='Descending'
		  THEN (SELECT (u.FirstName+' '+u.LastName)as[CustomerName]
		   FROM [USER] u
		   WHERE u.UserID=B.CustomerID)
		  END) DESC,

		  (CASE
		 WHEN @sortBy='Stylist' AND @sortDir='Ascending'
		 THEN (SELECT (u.FirstName + ' ' + u.LastName)as[stylist]
				FROM [USER] u 
				WHERE u.UserID=B.StylistID)
		 END) ASC,

		 (CASE
		  WHEN @sortBy='Date' AND @sortDir='Ascending'
		  THEN B.[Date]
		  END) ASC,

		  (CASE
		  WHEN @sortBy='Customer' AND @sortDir='Ascending'
		  THEN (SELECT (u.FirstName+' '+u.LastName)as[CustomerName]
		   FROM [USER] u
		   WHERE u.UserID=B.CustomerID)
		  END) ASC
END
GO
/****** Object:  StoredProcedure [dbo].[SP_StylistPastBookingsDateRange]    Script Date: 2018/10/16 18:02:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		S.Maqabangqa
-- Description: Gets stylists past bookings depending on the date range given.
-- =============================================
CREATE PROCEDURE [dbo].[SP_StylistPastBookingsDateRange]
	@stylistID nchar(30),
	@startDate datetime,
	@endDate datetime,
	@sortBy nvarchar(max)=null,
	@sortDir nvarchar(max)=null
AS
BEGIN
	SET NOCOUNT ON;
	SELECT BookingID,b.primaryBookingID AS [PrimaryID],B.StylistID,B.CustomerID,
			
		   (SELECT (u.FirstName + ' ' + u.LastName)as[StylistName]
		   FROM [USER] u
		   WHERE u.UserID = @stylistID)AS[StylistName],

		   (SELECT (u.FirstName+' '+u.LastName)as[CustomerName]
		   FROM [USER] u
		   WHERE u.UserID=B.CustomerID)AS[FullName],

		   B.[Date],TS.StartTime,TS.EndTime,B.Arrived

	From   [CHEVEUX].[dbo].[BOOKING] B, TIMESLOT TS, [User] U, EMPLOYEE e
	Where  b.StylistID = @stylistID
	AND		b.StylistID = e. EmployeeID
	AND    B.SlotNo = TS.SlotNo 
	AND    B.CustomerID = U.UserID 
		AND	   (B.Arrived = 'Y' or B.CustomerID='0')
	AND	  (B.[Date] BETWEEN @startDate AND @endDate)
	and b.BookingID=b.primaryBookingID
	ORDER BY
	(CASE 
		 WHEN @sortBy='Stylist' AND @sortDir='Descending'
		 THEN (SELECT (u.FirstName + ' ' + u.LastName)as[stylist]
				FROM [USER] u 
				WHERE u.UserID=B.StylistID)
		 END) DESC,

		 (CASE
		  WHEN @sortBy='Date' AND @sortDir='Descending'
		  THEN B.[Date]
		  END) DESC,

		  (CASE
		  WHEN @sortBy='Customer' AND @sortDir='Descending'
		  THEN (SELECT (u.FirstName+' '+u.LastName)as[CustomerName]
		   FROM [USER] u
		   WHERE u.UserID=B.CustomerID)
		  END) DESC,

		  (CASE
		 WHEN @sortBy='Stylist' AND @sortDir='Ascending'
		 THEN (SELECT (u.FirstName + ' ' + u.LastName)as[stylist]
				FROM [USER] u 
				WHERE u.UserID=B.StylistID)
		 END) ASC,

		 (CASE
		  WHEN @sortBy='Date' AND @sortDir='Ascending'
		  THEN B.[Date]
		  END) ASC,

		  (CASE
		  WHEN @sortBy='Customer' AND @sortDir='Ascending'
		  THEN (SELECT (u.FirstName+' '+u.LastName)as[CustomerName]
		   FROM [USER] u
		   WHERE u.UserID=B.CustomerID)
		  END) ASC
END
GO
/****** Object:  StoredProcedure [dbo].[SP_StylistUpcomingBkForDate]    Script Date: 2018/10/16 18:02:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		S.Maqabangqa
-- =============================================
CREATE PROCEDURE [dbo].[SP_StylistUpcomingBkForDate]
	@stylistID nchar(30),
	@day datetime,
	@sortBy nvarchar(max)=null,
	@sortDir nvarchar(max)=null
AS
BEGIN
	SET NOCOUNT ON;
	SELECT BookingID,b.primaryBookingID AS [PrimaryID],B.StylistID,B.CustomerID,
			
		   (SELECT (u.FirstName + ' ' + u.LastName)as[StylistName]
		   FROM [USER] u
		   WHERE u.UserID = @stylistID)AS[StylistName],

		   (SELECT (u.FirstName+' '+u.LastName)as[CustomerName]
		   FROM [USER] u
		   WHERE u.UserID=B.CustomerID)AS[FullName],

		   B.[Date],TS.StartTime,TS.EndTime,B.Arrived

	From   [CHEVEUX].[dbo].[BOOKING] B, TIMESLOT TS, [User] U, EMPLOYEE e
	Where  b.StylistID = @stylistID
	AND		b.StylistID = e. EmployeeID
	AND    B.SlotNo = TS.SlotNo 
	AND    B.CustomerID = U.UserID 
	AND    (B.Arrived = 'N' or B.CustomerID='0')
	AND    B.[Date] = @day
	AND    B.[Date] !< CAST(GETDATE() AS DATE)
	and b.BookingID=b.primaryBookingID
	ORDER BY
	(CASE 
		 WHEN @sortBy='Stylist' AND @sortDir='Descending'
		 THEN (SELECT (u.FirstName + ' ' + u.LastName)as[stylist]
				FROM [USER] u 
				WHERE u.UserID=B.StylistID)
		 END) DESC,

		 (CASE
		  WHEN @sortBy='Date' AND @sortDir='Descending'
		  THEN B.[Date]
		  END) DESC,

		  (CASE
		  WHEN @sortBy='Customer' AND @sortDir='Descending'
		  THEN (SELECT (u.FirstName+' '+u.LastName)as[CustomerName]
		   FROM [USER] u
		   WHERE u.UserID=B.CustomerID)
		  END) DESC,

		  (CASE
		 WHEN @sortBy='Stylist' AND @sortDir='Ascending'
		 THEN (SELECT (u.FirstName + ' ' + u.LastName)as[stylist]
				FROM [USER] u 
				WHERE u.UserID=B.StylistID)
		 END) ASC,

		 (CASE
		  WHEN @sortBy='Date' AND @sortDir='Ascending'
		  THEN B.[Date]
		  END) ASC,

		  (CASE
		  WHEN @sortBy='Customer' AND @sortDir='Ascending'
		  THEN (SELECT (u.FirstName+' '+u.LastName)as[CustomerName]
		   FROM [USER] u
		   WHERE u.UserID=B.CustomerID)
		  END) ASC
END
GO
/****** Object:  StoredProcedure [dbo].[SP_StylistUpcomingBookings]    Script Date: 2018/10/16 18:02:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		S.Maqabangqa
-- =============================================
CREATE PROCEDURE [dbo].[SP_StylistUpcomingBookings]
@stylistID nchar(30),
@sortBy nvarchar(max)=null,
	@sortDir nvarchar(max)=null
AS
BEGIN
	SET NOCOUNT ON;
	SELECT BookingID,b.primaryBookingID AS [PrimaryID],B.StylistID,B.CustomerID,
			
		   (SELECT (u.FirstName + ' ' + u.LastName)as[StylistName]
		   FROM [USER] u
		   WHERE u.UserID = @stylistID)AS[StylistName],

		   (SELECT (u.FirstName+' '+u.LastName)as[CustomerName]
		   FROM [USER] u
		   WHERE u.UserID=B.CustomerID)AS[FullName],

		   B.[Date],TS.StartTime,TS.EndTime,B.Arrived

	From   [CHEVEUX].[dbo].[BOOKING] B, TIMESLOT TS, [User] U, EMPLOYEE e
	Where  b.StylistID = @stylistID
	AND		b.StylistID = e. EmployeeID
	AND    B.SlotNo = TS.SlotNo 
	AND    B.CustomerID = U.UserID 
	AND    (B.Arrived = 'N' or B.CustomerID='0')
	AND    B.[Date] !< CAST(GETDATE() AS DATE)
	and b.BookingID=b.primaryBookingID
	ORDER BY
	(CASE 
		 WHEN @sortBy='Stylist' AND @sortDir='Descending'
		 THEN (SELECT (u.FirstName + ' ' + u.LastName)as[stylist]
				FROM [USER] u 
				WHERE u.UserID=B.StylistID)
		 END) DESC,

		 (CASE
		  WHEN @sortBy='Date' AND @sortDir='Descending'
		  THEN B.[Date]
		  END) DESC,

		  (CASE
		  WHEN @sortBy='Customer' AND @sortDir='Descending'
		  THEN (SELECT (u.FirstName+' '+u.LastName)as[CustomerName]
		   FROM [USER] u
		   WHERE u.UserID=B.CustomerID)
		  END) DESC,

		  (CASE
		 WHEN @sortBy='Stylist' AND @sortDir='Ascending'
		 THEN (SELECT (u.FirstName + ' ' + u.LastName)as[stylist]
				FROM [USER] u 
				WHERE u.UserID=B.StylistID)
		 END) ASC,

		 (CASE
		  WHEN @sortBy='Date' AND @sortDir='Ascending'
		  THEN B.[Date]
		  END) ASC,

		  (CASE
		  WHEN @sortBy='Customer' AND @sortDir='Ascending'
		  THEN (SELECT (u.FirstName+' '+u.LastName)as[CustomerName]
		   FROM [USER] u
		   WHERE u.UserID=B.CustomerID)
		  END) ASC

END
GO
/****** Object:  StoredProcedure [dbo].[SP_StylistUpcomingBookingsDR]    Script Date: 2018/10/16 18:02:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		S.Maqabangqa
-- Description: Get upcoming bookings for a specific stylist according to date range.
-- =============================================
CREATE PROCEDURE [dbo].[SP_StylistUpcomingBookingsDR]
	@stylistID nchar(30),
	@startDate datetime,
	@endDate datetime,
	@sortBy nvarchar(max)=null,
	@sortDir nvarchar(max)=null
AS
BEGIN
	SET NOCOUNT ON;
	SELECT BookingID,b.primaryBookingID AS [PrimaryID],B.StylistID,B.CustomerID,
			
		   (SELECT (u.FirstName + ' ' + u.LastName)as[StylistName]
		   FROM [USER] u
		   WHERE u.UserID = @stylistID)AS[StylistName],

		   (SELECT (u.FirstName+' '+u.LastName)as[CustomerName]
		   FROM [USER] u
		   WHERE u.UserID=B.CustomerID)AS[FullName],

		   B.[Date],TS.StartTime,TS.EndTime,B.Arrived

	From   BOOKING B, TIMESLOT TS, [User] U, EMPLOYEE e
	Where  b.StylistID = @stylistID
	AND		b.StylistID = e. EmployeeID
	AND    B.SlotNo = TS.SlotNo 
	AND    B.CustomerID = U.UserID 
	AND    (B.Arrived = 'N' or B.CustomerID='0')
	AND	  (B.[Date] BETWEEN @startDate AND @endDate)
	and b.BookingID=b.primaryBookingID
	ORDER BY
	(CASE 
		 WHEN @sortBy='Stylist' AND @sortDir='Descending'
		 THEN (SELECT (u.FirstName + ' ' + u.LastName)as[stylist]
				FROM [USER] u 
				WHERE u.UserID=B.StylistID)
		 END) DESC,

		 (CASE
		  WHEN @sortBy='Date' AND @sortDir='Descending'
		  THEN B.[Date]
		  END) DESC,

		  (CASE
		  WHEN @sortBy='Customer' AND @sortDir='Descending'
		  THEN (SELECT (u.FirstName+' '+u.LastName)as[CustomerName]
		   FROM [USER] u
		   WHERE u.UserID=B.CustomerID)
		  END) DESC,

		  (CASE
		 WHEN @sortBy='Stylist' AND @sortDir='Ascending'
		 THEN (SELECT (u.FirstName + ' ' + u.LastName)as[stylist]
				FROM [USER] u 
				WHERE u.UserID=B.StylistID)
		 END) ASC,

		 (CASE
		  WHEN @sortBy='Date' AND @sortDir='Ascending'
		  THEN B.[Date]
		  END) ASC,

		  (CASE
		  WHEN @sortBy='Customer' AND @sortDir='Ascending'
		  THEN (SELECT (u.FirstName+' '+u.LastName)as[CustomerName]
		   FROM [USER] u
		   WHERE u.UserID=B.CustomerID)
		  END) ASC
END
GO
/****** Object:  StoredProcedure [dbo].[SP_TotalBksMissedByCustomers]    Script Date: 2018/10/16 18:02:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:	 S.MAQABANGQA
-- Description:	Returns to total number of bookings missed by each customer
-- =============================================
CREATE PROCEDURE [dbo].[SP_TotalBksMissedByCustomers]
	@startDate datetime = null,
	@endDate datetime = null
AS
BEGIN
	SET NOCOUNT ON;

	SELECT Distinct(b.CustomerID),(u.FirstName + ' ' + u.LastName)as[CustomerName],
		COUNT(b.BookingID)as[BookingsMissed]
	FROM BOOKING b, [USER] u
	WHERE b.BookingID= primaryBookingID
	and   b.CustomerID=u.UserID
	and   u.UserType='C'
	and   b.[Date]  between @startDate and @endDate
	and   b.Arrived = 'N'
	GROUP BY b.CustomerID,u.FirstName,u.LastName
	ORDER BY BookingsMissed desc
END
GO
/****** Object:  StoredProcedure [dbo].[SP_TotalBookings]    Script Date: 2018/10/16 18:02:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Description:	gets the total number of all time bookings
-- =============================================
CREATE PROCEDURE [dbo].[SP_TotalBookings]
AS
BEGIN
	SELECT count(*)
	FROM BOOKING
	where BookingID = primaryBookingID
END
GO
/****** Object:  StoredProcedure [dbo].[SP_TotalUpcomingBookings]    Script Date: 2018/10/16 18:02:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Description:	gets the total number of upcoming bookings
-- =============================================
CREATE PROCEDURE [dbo].[SP_TotalUpcomingBookings]
AS
BEGIN
	SELECT count(*)
	FROM BOOKING
	where [Date] > DATEADD(day, DATEDIFF(day, 0, GETDATE()), 0)
		And Arrived = 'N'
		And BookingID = primaryBookingID
END
GO
/****** Object:  StoredProcedure [dbo].[SP_TREATMENT]    Script Date: 2018/10/16 18:02:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Description:	Inserting of a product
CREATE PROCEDURE [dbo].[SP_TREATMENT]
	@treatmentID nchar(10),
	@qty int,
	@treatmentType varchar(50),
	@BrandID nchar(10) 

AS
BEGIN
	BEGIN TRY
		BEGIN TRANSACTION

     INSERT TREATMENT(TreatmentID,Qty,TreatmentType,BrandID)
	 VALUES(@treatmentID, @qty, @treatmentType, @BrandID)
	
	COMMIT TRANSACTION
	END TRY
	BEGIN CATCH
		IF @@TRANCOUNT > 0
			ROLLBACK TRANSACTION
	END CATCH
END
GO
/****** Object:  StoredProcedure [dbo].[SP_TreatmentSearchByTerm]    Script Date: 2018/10/16 18:02:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SP_TreatmentSearchByTerm] 
	@searchTerm varchar(50)
AS
BEGIN
	select P.Name, P.ProductDescription, P.Price, P.[ProductType(T/A/S)], P.ProductID
	From PRODUCT P, TREATMENT T, BRAND B
	WHERE ProductID = TreatmentID AND B.BrandID = T.BrandID AND 
	(P.Name like '%'+@searchTerm+'%'  
	Or P.ProductDescription like '%'+@searchTerm+'%'
	Or T.TreatmentType like '%'+@searchTerm+'%'
	Or B.Name like '%'+@searchTerm+'%')  
	AND P.Active = 'Y'
	Order By P.[ProductType(T/A/S)]
END
GO
/****** Object:  StoredProcedure [dbo].[SP_UpdateAccessory]    Script Date: 2018/10/16 18:02:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SP_UpdateAccessory]
    @accessoryID nchar(10),
	@name varchar(max),
	@productDescription varchar(max),
	@price money,
	@SupplierID nchar(10)
	
AS
BEGIN
	BEGIN TRY
		BEGIN TRANSACTION

			UPDATE ACCESSORY
			SET    SupplierID = @SupplierID
			where AccessoryID = @accessoryID
				   
			UPDATE PRODUCT
			SET [Name] = @name,
				ProductDescription = @productDescription,
				Price = @price
			Where ProductID = @accessoryID

		COMMIT TRANSACTION 
	END TRY 
	BEGIN CATCH 
		IF @@TRANCOUNT > 0 
			ROLLBACK TRANSACTION
	END CATCH 
END

	


GO
/****** Object:  StoredProcedure [dbo].[SP_UpdateAddress]    Script Date: 2018/10/16 18:02:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Description:	updates the addres in the Bussiness Table
-- =============================================
CREATE PROCEDURE [dbo].[SP_UpdateAddress]
	@addline1 varchar(max),
	@addline2 varchar(max),
	@BusinessID nchar(10)
AS
BEGIN
	begin try
		begin transaction
			UPDATE BUSINESS
			SET AddressLine1 = @addline1,
				AddressLine2 = @addline2
			Where BusinessID = @BusinessID
		Commit transaction
	end try
	begin catch
		if @@TRANCOUNT > 0
			rollback transaction
	end catch
END
GO
/****** Object:  StoredProcedure [dbo].[SP_UpdateBooking]    Script Date: 2018/10/16 18:02:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Description:	Updates An Existing Booking
-- =============================================
CREATE PROCEDURE [dbo].[SP_UpdateBooking] 
	@BookingID nchar(10),
	@SlotNo nchar(10),
	@StylistID nchar(30),
	@Date datetime,
	@Comment varchar(max)
AS
BEGIN
	begin try
		begin transaction
			UPDATE [CHEVEUX].[dbo].[BOOKING]
			SET SlotNo = @SlotNo, 
				StylistID = @StylistID,
				[Date] = @Date,
				Arrived = 'N',
				NotificationReminder = 0,
				[Comment] = @Comment
			Where (BookingID = @BookingID
					or  BOOKING.primaryBookingID = @BookingID)
		commit transaction
	end try
	begin catch
		if @@TRANCOUNT > 0
			rollback transaction
	end catch
END
GO
/****** Object:  StoredProcedure [dbo].[SP_UpdateBookingReview]    Script Date: 2018/10/16 18:02:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		S.MAQABANGQA
-- =============================================
CREATE PROCEDURE [dbo].[SP_UpdateBookingReview]
	@reviewID nchar(10),
	@bookingID nchar(10),
	@date datetime,
	@time time(7),
	@rating float = null,
	@comment varchar(50)
AS
BEGIN
	BEGIN TRY
		BEGIN TRANSACTION 
			UPDATE [CHEVEUX].[dbo].[REVIEW]
			SET [Date] = @date,
				[Time]= @time,
				Rating = @rating,
				Comment = @comment
			WHERE ReviewID=@reviewID
			AND primaryBookingID=@bookingID
		COMMIT TRANSACTION
		END TRY
	BEGIN CATCH
		if @@TRANCOUNT > 0
			ROLLBACK TRANSACTION
	END CATCH
END
GO
/****** Object:  StoredProcedure [dbo].[SP_UpdateCustVisit]    Script Date: 2018/10/16 18:02:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		S.Maqabangqa
-- Create date: 04.06.2018
-- Description:	Updates Existing Customer Visit.
-- =============================================
CREATE PROCEDURE [dbo].[SP_UpdateCustVisit]
	@CustomerID nchar(30),
	@BookingID nchar(10),
	@Description varchar(max)
AS
BEGIN
	BEGIN TRY 
		BEGIN TRANSACTION
			UPDATE CUST_VISIT
			SET	   [Description] = Convert(varchar(50),@Description)
			WHERE CustomerID=@CustomerID
			AND	  BookingID=@BookingID

			UPDATE BOOKING
			SET	   Comment = @Description
			WHERE  primaryBookingID = @BookingID
			AND    CustomerID = @CustomerID

		COMMIT TRANSACTION
	END TRY 
	BEGIN CATCH 
		IF @@TRANCOUNT > 0
			ROLLBACK TRANSACTION
	END CATCH 
END
GO
/****** Object:  StoredProcedure [dbo].[SP_UpdateEmployee]    Script Date: 2018/10/16 18:02:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		S.Maqabangqa
-- =============================================
CREATE PROCEDURE [dbo].[SP_UpdateEmployee]
	@empID nchar(30),
	@type nchar(10),
	@bio varchar(max) = null,
	@Specialisation nchar(10),
	@addLine1 varchar(max),
	@addLine2 varchar(max) null,
	@suburb varchar(max),
	@city varchar(max)
AS
BEGIN
	BEGIN TRY
		BEGIN TRANSACTION
			
			UPDATE [CHEVEUX].[dbo].[EMPLOYEE]
			SET [CHEVEUX].[dbo].[EMPLOYEE].[Type]=@type,
				[CHEVEUX].[dbo].[EMPLOYEE].Bio=@bio,
				[CHEVEUX].[dbo].[EMPLOYEE].AddressLine1=@addLine1,
				[CHEVEUX].[dbo].[EMPLOYEE].AddressLine2=@addLine2,
				[CHEVEUX].[dbo].[EMPLOYEE].Suburb=@suburb,
				[CHEVEUX].[dbo].[EMPLOYEE].City=@city
			WHERE EmployeeID=@empID

			

		COMMIT TRANSACTION 
	END TRY 
	BEGIN CATCH 
		IF @@TRANCOUNT > 0 
			ROLLBACK TRANSACTION
	END CATCH

		BEGIN TRY
		BEGIN TRANSACTION
			
			UPDATE STYLIST_SERVICE
			SET ServiceID = @Specialisation
			WHERE EmployeeID=@empID
					COMMIT TRANSACTION 
		END TRY 
		BEGIN CATCH 
			IF @@TRANCOUNT > 0 
				ROLLBACK TRANSACTION
		END CATCH  
END
GO
/****** Object:  StoredProcedure [dbo].[SP_UpdateFeaturedContact]    Script Date: 2018/10/16 18:02:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[SP_UpdateFeaturedContact]
	@FeatureID nchar(10),
	@ItemID varchar(50)
AS
BEGIN
	BEGIN TRY
		BEGIN TRANSACTION
			UPDATE [Home_Page]
			SET ItemID = @ItemID
			WHERE FeatureID = @FeatureID
		COMMIT TRANSACTION
	END TRY
	BEGIN CATCH
		IF @@TRANCOUNT > 0
			ROLLBACK TRANSACTION
	END CATCH
END
GO
/****** Object:  StoredProcedure [dbo].[SP_UpdateFeaturedProduct]    Script Date: 2018/10/16 18:02:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[SP_UpdateFeaturedProduct]
	@FeatureID nchar(10),
	@ItemID nchar(10)
AS
BEGIN
	BEGIN TRY
		BEGIN TRANSACTION
			UPDATE [Home_Page]
			SET ItemID = @ItemID
			WHERE FeatureID = @FeatureID
		COMMIT TRANSACTION
	END TRY
	BEGIN CATCH
		IF @@TRANCOUNT > 0
			ROLLBACK TRANSACTION
	END CATCH
END
GO
/****** Object:  StoredProcedure [dbo].[SP_UpdateFeaturedService]    Script Date: 2018/10/16 18:02:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[SP_UpdateFeaturedService]
	@FeatureID nchar(10),
	@ItemID nchar(10)
AS
BEGIN
	BEGIN TRY
		BEGIN TRANSACTION
			UPDATE [Home_Page]
			SET ItemID = @ItemID
			WHERE FeatureID = @FeatureID
		COMMIT TRANSACTION
	END TRY
	BEGIN CATCH
		IF @@TRANCOUNT > 0
			ROLLBACK TRANSACTION
	END CATCH
END
GO
/****** Object:  StoredProcedure [dbo].[SP_UpdateFeaturedStylist]    Script Date: 2018/10/16 18:02:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[SP_UpdateFeaturedStylist]
	@FeatureID nchar(10),
	@ItemID varchar(50)
AS
BEGIN
	BEGIN TRY
		BEGIN TRANSACTION
			UPDATE [Home_Page]
			SET ItemID = @ItemID,
				ImageURL = (Select UserImage
							From [USER]
							Where UserID = @ItemID)
			WHERE FeatureID = @FeatureID
		COMMIT TRANSACTION
	END TRY
	BEGIN CATCH
		IF @@TRANCOUNT > 0
			ROLLBACK TRANSACTION
	END CATCH
END
GO
/****** Object:  StoredProcedure [dbo].[SP_UpdateNotiStatus]    Script Date: 2018/10/16 18:02:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Description:	Sets the Notification Reminder colum of the booking table for a spacific booking
-- =============================================
CREATE PROCEDURE [dbo].[SP_UpdateNotiStatus]
	@BookingID nchar(10),
	@NotiStatus bit
AS
BEGIN
	BEGIN TRY 
		BEGIN TRANSACTION
			UPDATE BOOKING
			SET	   NotificationReminder = @NotiStatus
			WHERE BookingID = @BookingID
		COMMIT TRANSACTION
	END TRY 
	BEGIN CATCH 
		IF @@TRANCOUNT > 0
			ROLLBACK TRANSACTION
	END CATCH 
END
GO
/****** Object:  StoredProcedure [dbo].[SP_UpdateOrder]    Script Date: 2018/10/16 18:02:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[SP_UpdateOrder]
	@OrderID nchar(10),
	@DateReceived datetime,
	@Received bit
AS
BEGIN
	BEGIN TRY
		BEGIN TRANSACTION
			UPDATE [Order]
			SET DateReceived = @DateReceived,
				Received = @Received
			WHERE OrderID = @OrderID
		COMMIT TRANSACTION
	END TRY
	BEGIN CATCH
		IF @@TRANCOUNT > 0
			ROLLBACK TRANSACTION
	END CATCH
END
GO
/****** Object:  StoredProcedure [dbo].[SP_UpdatePhoneNumber]    Script Date: 2018/10/16 18:02:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Description:	updates the phone number in the Bussiness Table
-- =============================================
CREATE PROCEDURE [dbo].[SP_UpdatePhoneNumber]
	@PhoneNumber nchar(10),
	@BusinessID nchar(10)
AS
BEGIN
	begin try
		begin transaction
			UPDATE BUSINESS
			SET Phone = @PhoneNumber
			Where BusinessID = @BusinessID
		Commit transaction
	end try
	begin catch
		if @@TRANCOUNT > 0
			rollback transaction
	end catch
END
GO
/****** Object:  StoredProcedure [dbo].[SP_UpdateProducts]    Script Date: 2018/10/16 18:02:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Description:	Update/editing of products
-- =============================================
CREATE  PROCEDURE [dbo].[SP_UpdateProducts]
	@productID nchar(10),
	@name varchar(max),
	@productDescription varchar(max),
	@price money,
	@productType char(1),
	@active char(1),
	@productImage nvarchar(50)


AS
BEGIN
	BEGIN TRY
		BEGIN TRANSACTION

	UPDATE PRODUCT
	SET Name = @name,
	    ProductDescription = @productDescription,
		Price = @price,
		[ProductType(T/A/S)] =@productType ,
		Active = @active,
		ProductImage = @productImage

	WHERE ProductID = @productID
	



		COMMIT TRANSACTION 
	END TRY 
	BEGIN CATCH 
		IF @@TRANCOUNT > 0 
			ROLLBACK TRANSACTION
	END CATCH 

END

GO
/****** Object:  StoredProcedure [dbo].[SP_UpdateProductSalesDTLRecordQty]    Script Date: 2018/10/16 18:02:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE 
 PROCEDURE [dbo].[SP_UpdateProductSalesDTLRecordQty]
	@SaleID nchar(10), 
	@ProductID nchar(10),
	@Qty int
AS
BEGIN
	begin try
		begin transaction
				UPDATE SALES_DTL
				SET Qty = (Select Qty-@Qty From SALES_DTL Where ProductID = @ProductID AND SaleID = @SaleID)
				Where ProductID = @ProductID
					AND SaleID = @SaleID

				UPDATE TREATMENT
				SET Qty = (Select Qty+@Qty From TREATMENT Where TreatmentID = @ProductID)
				WHERE TreatmentID = @ProductID

				UPDATE ACCESSORY
				SET Qty = (Select Qty+@Qty From ACCESSORY Where AccessoryID = @ProductID)
				WHERE AccessoryID = @ProductID
		commit transaction
	end try
	begin catch
		if @@TRANCOUNT > 0
			rollback transaction
	end catch
END
GO
/****** Object:  StoredProcedure [dbo].[SP_UpdatePublicHolidayHours]    Script Date: 2018/10/16 18:02:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Description:	updates the public holiday hours in the Bussiness Table
-- =============================================
CREATE PROCEDURE [dbo].[SP_UpdatePublicHolidayHours]
	@start time(7),
	@end time(7),
	@BusinessID nchar(10)
AS
BEGIN
	begin try
		begin transaction
			UPDATE BUSINESS
			SET PublicHolStart = @start,
				PublicHolEnd = @end
			Where BusinessID = @BusinessID
		Commit transaction
	end try
	begin catch
		if @@TRANCOUNT > 0
			rollback transaction
	end catch
END
GO
/****** Object:  StoredProcedure [dbo].[SP_UpdateQtyOnHand]    Script Date: 2018/10/16 18:02:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[SP_UpdateQtyOnHand]
	@ProductID nchar(10),
	@Qty int
AS
BEGIN
	BEGIN TRY
		BEGIN TRANSACTION
			UPDATE ACCESSORY
			SET Qty = Qty + @Qty
			WHERE AccessoryID = @ProductID

			UPDATE TREATMENT
			SET Qty = Qty + @Qty
			WHERE TreatmentID = @ProductID
		COMMIT TRANSACTION
	END TRY
	BEGIN CATCH
		IF @@TRANCOUNT > 0
			ROLLBACK TRANSACTION
	END CATCH
END
GO
/****** Object:  StoredProcedure [dbo].[SP_UpdateService]    Script Date: 2018/10/16 18:02:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE  PROCEDURE [dbo].[SP_UpdateService] 
	@ServiceID nchar(10),
	@Price money,
	@Slots int
AS
BEGIN
	BEGIN TRY
		BEGIN TRANSACTION 
			UPDATE SERVICE
			SET NoOfSlots = @Slots
			WHERE ServiceID = @ServiceID

			UPDATE PRODUCT
			SET Price = @Price
			WHERE ProductID = @ServiceID
		COMMIT TRANSACTION
	END TRY
	BEGIN CATCH
		IF @@TRANCOUNT > 0
			ROLLBACK TRANSACTION
	END CATCH
END
GO
/****** Object:  StoredProcedure [dbo].[SP_UpdateStockManagementSettings]    Script Date: 2018/10/16 18:02:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
 
CREATE PROCEDURE [dbo].[SP_UpdateStockManagementSettings]
	@BusinessID nchar(10),
	@LowStock int,
	@PurchaseQty int,
	@AutoPurchase bit,
	@AutoPurchaseFrequency nchar(3),
	@AutoPurchaseProducts bit,
	@nextDate DateTime
AS
BEGIN
	BEGIN TRY
		BEGIN TRANSACTION
			UPDATE [Stock_Management]
			SET LowStock = @LowStock,
				PurchaseQty = @PurchaseQty,
				AutoPurchase = @AutoPurchase,
				AutoPurchaseFrequency = @AutoPurchaseFrequency,
				AutoPurchaseProducts = @AutoPurchaseProducts,
				[NxtOrderDate] = @nextDate
			WHERE BusinessID = @BusinessID
		COMMIT TRANSACTION
	END TRY
	BEGIN CATCH
		IF @@TRANCOUNT > 0
			ROLLBACK TRANSACTION
	END CATCH
END
GO
/****** Object:  StoredProcedure [dbo].[SP_UpdateStylistBio]    Script Date: 2018/10/16 18:02:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Description:	updates the Weekend hours in the Bussiness Table
-- =============================================
CREATE  PROCEDURE [dbo].[SP_UpdateStylistBio]
	@bioUpdate varchar(MAX),
	@empID nchar(30)
AS
BEGIN
	begin try
		begin transaction
			UPDATE EMPLOYEE
			SET Bio = @bioUpdate
			Where EmployeeID = @empID
		Commit transaction
	end try
	begin catch
		if @@TRANCOUNT > 0
			rollback transaction
	end catch
END
GO
/****** Object:  StoredProcedure [dbo].[SP_UpdateStylistReview]    Script Date: 2018/10/16 18:02:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		S.MAQABANGQA
-- =============================================
CREATE PROCEDURE [dbo].[SP_UpdateStylistReview]
	@reviewID nchar(10),
	@employeeID nchar(30),
	@date datetime,
	@time time(7),
	@rating float = null,
	@comment varchar(50)
AS
BEGIN
	BEGIN TRY
		BEGIN TRANSACTION 
			UPDATE REVIEW
			SET [Date] = @date,
				[Time]= @time,
				Rating = @rating,
				Comment = @comment
			WHERE ReviewID=@reviewID
				  AND EmployeeID = @employeeID
		COMMIT TRANSACTION
		END TRY
	BEGIN CATCH
		if @@TRANCOUNT > 0
			ROLLBACK TRANSACTION
	END CATCH
END
GO
/****** Object:  StoredProcedure [dbo].[SP_UpdateTreatment]    Script Date: 2018/10/16 18:02:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SP_UpdateTreatment]
	@treatmentID nchar(10), 
	@name varchar(max),
	@productDescription varchar(max),
	@price money,
	@SupplierID nchar(10)

AS
BEGIN
	BEGIN TRY
		BEGIN TRANSACTION

			UPDATE TREATMENT
			SET SupplierID = @SupplierID
			Where TreatmentID = @treatmentID 
				
			UPDATE PRODUCT
			SET [Name] = @name,
				ProductDescription = @productDescription,
				Price = @price
			Where ProductID = @treatmentID
	   
		COMMIT TRANSACTION 
	END TRY 
	BEGIN CATCH 
		IF @@TRANCOUNT > 0 
			ROLLBACK TRANSACTION
	END CATCH 
END





GO
/****** Object:  StoredProcedure [dbo].[SP_UpdateUserAccountPassword]    Script Date: 2018/10/16 18:02:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Description:	Changes the password for the account matching the userID
-- =============================================
CREATE  PROCEDURE [dbo].[SP_UpdateUserAccountPassword]  
	@Password nvarchar(max),
	@UserID nchar(30)
AS
BEGIN
	begin try
		begin transaction
			UPDATE [USER]
			SET PassRestCode = null,
				[Password] = @Password
			Where UserID = @UserID
		Commit transaction
	end try
	begin catch
		if @@TRANCOUNT > 0
			rollback transaction
	end catch
END
GO
/****** Object:  StoredProcedure [dbo].[SP_UpdateVateRate]    Script Date: 2018/10/16 18:02:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Description:	updates the Vate Rate in the Bussiness Table
-- =============================================
CREATE PROCEDURE [dbo].[SP_UpdateVateRate] 
	@VatRat int,
	@BusinessID nchar(10)
AS
BEGIN
	begin try
		begin transaction
			UPDATE BUSINESS
			SET [Vat%] = @VatRat
			Where BusinessID = @BusinessID
		Commit transaction
	end try
	begin catch
		if @@TRANCOUNT > 0
			rollback transaction
	end catch
END
GO
/****** Object:  StoredProcedure [dbo].[SP_UpdateVateRegNo]    Script Date: 2018/10/16 18:02:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Description:	updates the Vate reg no in the Bussiness Table
-- =============================================
CREATE PROCEDURE [dbo].[SP_UpdateVateRegNo] 
	@VatRegno nchar(10),
	@BusinessID nchar(10)
AS
BEGIN
	begin try
		begin transaction
			UPDATE BUSINESS
			SET VatRegNo = @VatRegno
			Where BusinessID = @BusinessID
		Commit transaction
	end try
	begin catch
		if @@TRANCOUNT > 0
			rollback transaction
	end catch
END
GO
/****** Object:  StoredProcedure [dbo].[SP_UpdateWeekdayHours]    Script Date: 2018/10/16 18:02:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Description:	updates the Weekday hours in the Bussiness Table
-- =============================================
CREATE PROCEDURE [dbo].[SP_UpdateWeekdayHours]
	@start time(7),
	@end time(7),
	@BusinessID nchar(10)
AS
BEGIN
	begin try
		begin transaction
			UPDATE BUSINESS
			SET WeekdayStart = @start,
				WeekdayEnd = @end
			Where BusinessID = @BusinessID
		Commit transaction
	end try
	begin catch
		if @@TRANCOUNT > 0
			rollback transaction
	end catch
END
GO
/****** Object:  StoredProcedure [dbo].[SP_UpdateWeekendHours]    Script Date: 2018/10/16 18:02:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Description:	updates the Weekend hours in the Bussiness Table
-- =============================================
CREATE PROCEDURE [dbo].[SP_UpdateWeekendHours]
	@start time(7),
	@end time(7),
	@BusinessID nchar(10)
AS
BEGIN
	begin try
		begin transaction
			UPDATE BUSINESS
			SET WeekendStart = @start,
				WeekendEnd = @end
			Where BusinessID = @BusinessID
		Commit transaction
	end try
	begin catch
		if @@TRANCOUNT > 0
			rollback transaction
	end catch
END
GO
/****** Object:  StoredProcedure [dbo].[SP_UserList]    Script Date: 2018/10/16 18:02:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		S.Maqabangqa
-- =============================================
CREATE PROCEDURE [dbo].[SP_UserList]
AS
BEGIN
	SET NOCOUNT ON;
	SELECT  u.UserImage,u.UserID, (u.FirstName + ' ' +u.LastName)AS [FullName] , u.UserName,u.Email,u.ContactNo, u.UserType
	FROM [USER] u
	WHERE (u.UserType = 'C' OR u.UserType=null)
		AND u.Active = 'T'
	ORDER BY FullName ASC
END
GO
/****** Object:  StoredProcedure [dbo].[SP_ViewAllEmployee]    Script Date: 2018/10/16 18:02:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Description:	returns a list of all employee details in the form of TypeLibary SP_ViewEmployee
-- =============================================
CREATE PROCEDURE [dbo].[SP_ViewAllEmployee]
AS
BEGIN
	Select UserID, FirstName, LastName, UserName, Email, ContactNo, [Type], UserImage, Active
	From [USER], EMPLOYEE
	Where UserID = EMPLOYEE.EmployeeID 
	AND [USER].Active = 'T'
	order by [Type] desc
END
GO
/****** Object:  StoredProcedure [dbo].[SP_ViewCustVisit]    Script Date: 2018/10/16 18:02:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		S.Maqabangqa
-- Create date: 04.06.2018
-- Description:	View Customer Visit Record
-- =============================================
CREATE PROCEDURE [dbo].[SP_ViewCustVisit]
	@CustomerID nchar(30),
	@BookingID nchar(10)
AS
BEGIN

	SET NOCOUNT ON;
	SELECT * 
	FROM CUST_VISIT c 
	WHERE c.CustomerID=@CustomerID AND c.BookingID=@BookingID
END
GO
/****** Object:  StoredProcedure [dbo].[SP_ViewEmployee]    Script Date: 2018/10/16 18:02:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SP_ViewEmployee] 
	@EmployeeID nchar(30)
AS
BEGIN
	Select UserID, FirstName, LastName, UserName, Email, ContactNo, [Type], UserImage, Active, e.AddressLine1, e.AddressLine2, e.Suburb, e.City
	From [USER], EMPLOYEE e
	Where UserID = e.EmployeeID
		AND UserID = @EmployeeID
END
GO
/****** Object:  StoredProcedure [dbo].[SP_ViewStylistSpecialisation]    Script Date: 2018/10/16 18:02:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Description:	given an emplyeeID (Stylist) it returns that employee specialization
-- =============================================
CREATE PROCEDURE [dbo].[SP_ViewStylistSpecialisation] 
	@EmployeeID nchar(30)
AS
BEGIN
	Select EmployeeID, ServiceID, [Name], ProductDescription, Price, [ProductType(T/A/S)], ProductImage, 
	(Select Bio from EMPLOYEE where EmployeeID = @EmployeeID) as StylistBio
	From STYLIST_SERVICE, PRODUCT
	Where ProductID = ServiceID
		And Active = 'Y'
		AND EmployeeID = @EmployeeID
END
GO
USE [master]
GO
ALTER DATABASE [CHEVEUX] SET  READ_WRITE 
GO
