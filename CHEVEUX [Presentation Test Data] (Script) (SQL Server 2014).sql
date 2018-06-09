USE [master]
GO
/****** Object:  Database [CHEVEUX]    Script Date: 2018/06/09 13:00:49 ******/
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
ALTER DATABASE [CHEVEUX] SET DELAYED_DURABILITY = DISABLED 
GO
EXEC sys.sp_db_vardecimal_storage_format N'CHEVEUX', N'ON'
GO
USE [CHEVEUX]
GO
/****** Object:  Table [dbo].[ACCESSORY]    Script Date: 2018/06/09 13:00:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ACCESSORY](
	[AccessoryID] [nchar](10) NOT NULL,
	[Colour] [varchar](50) NULL,
	[Qty] [int] NULL,
	[BrandID] [nchar](10) NULL,
 CONSTRAINT [PK_ACCESSORY] PRIMARY KEY CLUSTERED 
(
	[AccessoryID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[BOOKING]    Script Date: 2018/06/09 13:00:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BOOKING](
	[BookingID] [nchar](10) NOT NULL,
	[SlotNo] [nchar](10) NULL,
	[CustomerID] [nchar](30) NULL,
	[StylistID] [nchar](30) NULL,
	[ServiceID] [nchar](10) NULL,
	[Date] [datetime] NULL,
	[Available] [char](1) NULL,
	[Arrived] [char](1) NULL,
	[Comment] [varchar](max) NULL,
 CONSTRAINT [PK_BOOKING] PRIMARY KEY CLUSTERED 
(
	[BookingID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[BRAID_SERVICE]    Script Date: 2018/06/09 13:00:49 ******/
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
/****** Object:  Table [dbo].[BRAND]    Script Date: 2018/06/09 13:00:49 ******/
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
/****** Object:  Table [dbo].[BUSINESS]    Script Date: 2018/06/09 13:00:49 ******/
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
/****** Object:  Table [dbo].[CUST_VISIT]    Script Date: 2018/06/09 13:00:49 ******/
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
/****** Object:  Table [dbo].[EMPLOYEE]    Script Date: 2018/06/09 13:00:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EMPLOYEE](
	[EmployeeID] [nchar](30) NOT NULL,
	[AddressLine1] [varchar](max) NULL,
	[AddressLine2] [varchar](max) NULL,
	[Type] [nchar](10) NULL,
 CONSTRAINT [PK_EMPLOYEE] PRIMARY KEY CLUSTERED 
(
	[EmployeeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[LENGTH]    Script Date: 2018/06/09 13:00:49 ******/
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
/****** Object:  Table [dbo].[PRODUCT]    Script Date: 2018/06/09 13:00:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PRODUCT](
	[ProductID] [nchar](10) NOT NULL,
	[Name] [varchar](max) NULL,
	[ProductDescription] [varchar](max) NULL,
	[Price] [nvarchar](50) NULL,
	[ProductType(T/A/S)] [char](1) NULL,
	[Active] [char](1) NULL,
	[ProductImage] [nvarchar](50) NULL,
 CONSTRAINT [PK_PRODUCT] PRIMARY KEY CLUSTERED 
(
	[ProductID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[REVIEW]    Script Date: 2018/06/09 13:00:49 ******/
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
 CONSTRAINT [PK_REVIEW] PRIMARY KEY CLUSTERED 
(
	[ReviewID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SALE]    Script Date: 2018/06/09 13:00:49 ******/
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
/****** Object:  Table [dbo].[SALES_DTL]    Script Date: 2018/06/09 13:00:49 ******/
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
/****** Object:  Table [dbo].[SERVICE]    Script Date: 2018/06/09 13:00:49 ******/
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
/****** Object:  Table [dbo].[STYLE]    Script Date: 2018/06/09 13:00:49 ******/
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
/****** Object:  Table [dbo].[STYLIST_SERVICE]    Script Date: 2018/06/09 13:00:49 ******/
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
/****** Object:  Table [dbo].[TIMESLOT]    Script Date: 2018/06/09 13:00:49 ******/
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
/****** Object:  Table [dbo].[TREATMENT]    Script Date: 2018/06/09 13:00:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TREATMENT](
	[TreatmentID] [nchar](10) NOT NULL,
	[Qty] [int] NULL,
	[TreatmentType] [varchar](50) NULL,
	[BrandID] [nchar](10) NULL,
 CONSTRAINT [PK_TREATMENT] PRIMARY KEY CLUSTERED 
(
	[TreatmentID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[USER]    Script Date: 2018/06/09 13:00:49 ******/
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
	[Password] [nvarchar](50) NULL,
	[UserType] [char](1) NULL,
	[Active] [char](1) NULL,
	[UserImage] [varchar](max) NULL,
	[AccountType] [nchar](10) NULL,
 CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
(
	[UserID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[WIDTH]    Script Date: 2018/06/09 13:00:49 ******/
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
INSERT [dbo].[ACCESSORY] ([AccessoryID], [Colour], [Qty], [BrandID]) VALUES (N'Pr1135    ', N'Silver/Gold', 200, N'B19       ')
INSERT [dbo].[ACCESSORY] ([AccessoryID], [Colour], [Qty], [BrandID]) VALUES (N'Pr1136    ', N'#1', 200, N'B02       ')
INSERT [dbo].[ACCESSORY] ([AccessoryID], [Colour], [Qty], [BrandID]) VALUES (N'Pr1137    ', N'#1', 200, N'B05       ')
INSERT [dbo].[ACCESSORY] ([AccessoryID], [Colour], [Qty], [BrandID]) VALUES (N'Pr1138    ', N'Black', 150, N'B21       ')
INSERT [dbo].[ACCESSORY] ([AccessoryID], [Colour], [Qty], [BrandID]) VALUES (N'Pr1140    ', N'#350', 200, N'B02       ')
INSERT [dbo].[ACCESSORY] ([AccessoryID], [Colour], [Qty], [BrandID]) VALUES (N'Pr1144    ', N'#2', 200, N'B15       ')
INSERT [dbo].[ACCESSORY] ([AccessoryID], [Colour], [Qty], [BrandID]) VALUES (N'Pr1145    ', N'#2', 200, N'B02       ')
INSERT [dbo].[ACCESSORY] ([AccessoryID], [Colour], [Qty], [BrandID]) VALUES (N'Pr1151    ', N'1B', 200, N'B07       ')
INSERT [dbo].[ACCESSORY] ([AccessoryID], [Colour], [Qty], [BrandID]) VALUES (N'Pr1152    ', N'1B', 200, N'B10       ')
INSERT [dbo].[ACCESSORY] ([AccessoryID], [Colour], [Qty], [BrandID]) VALUES (N'Pr1153    ', N'1B', 200, N'B07       ')
INSERT [dbo].[ACCESSORY] ([AccessoryID], [Colour], [Qty], [BrandID]) VALUES (N'Pr1156    ', N'1B/350', 200, N'B10       ')
INSERT [dbo].[ACCESSORY] ([AccessoryID], [Colour], [Qty], [BrandID]) VALUES (N'Pr1161    ', N'1B', 200, N'B10       ')
INSERT [dbo].[ACCESSORY] ([AccessoryID], [Colour], [Qty], [BrandID]) VALUES (N'Pr1162    ', N'D33', 200, N'B10       ')
INSERT [dbo].[ACCESSORY] ([AccessoryID], [Colour], [Qty], [BrandID]) VALUES (N'Pr1164    ', N'#1', 200, N'B07       ')
INSERT [dbo].[ACCESSORY] ([AccessoryID], [Colour], [Qty], [BrandID]) VALUES (N'Pr1165    ', N'#1', 200, N'B15       ')
INSERT [dbo].[ACCESSORY] ([AccessoryID], [Colour], [Qty], [BrandID]) VALUES (N'Pr1166    ', N'#350', 200, N'B05       ')
INSERT [dbo].[ACCESSORY] ([AccessoryID], [Colour], [Qty], [BrandID]) VALUES (N'Pr1167    ', N'Aqua', 200, N'B07       ')
INSERT [dbo].[ACCESSORY] ([AccessoryID], [Colour], [Qty], [BrandID]) VALUES (N'Pr1168    ', N'#1', 200, N'B22       ')
INSERT [dbo].[ACCESSORY] ([AccessoryID], [Colour], [Qty], [BrandID]) VALUES (N'Pr1172    ', N'#1', 200, N'B22       ')
INSERT [dbo].[ACCESSORY] ([AccessoryID], [Colour], [Qty], [BrandID]) VALUES (N'Pr1174    ', N'#2', 200, N'B23       ')
INSERT [dbo].[BOOKING] ([BookingID], [SlotNo], [CustomerID], [StylistID], [ServiceID], [Date], [Available], [Arrived], [Comment]) VALUES (N'1         ', N'Slo10     ', N'105242998585655922697         ', N'118233419479102946333         ', N'Pr1129    ', CAST(N'2018-06-14T00:00:00.000' AS DateTime), N'N', N'N', NULL)
INSERT [dbo].[BOOKING] ([BookingID], [SlotNo], [CustomerID], [StylistID], [ServiceID], [Date], [Available], [Arrived], [Comment]) VALUES (N'10        ', N'Slo16     ', N'105242998585655922697         ', N'118233419479102946333         ', N'Pr1106    ', CAST(N'2018-04-30T00:00:00.000' AS DateTime), N'N', N'Y', NULL)
INSERT [dbo].[BOOKING] ([BookingID], [SlotNo], [CustomerID], [StylistID], [ServiceID], [Date], [Available], [Arrived], [Comment]) VALUES (N'111       ', N'Slo16     ', N'105242998585655922697         ', N'118233419479102946333         ', N'Pr1160    ', CAST(N'2018-06-30T00:00:00.000' AS DateTime), N'N', NULL, NULL)
INSERT [dbo].[BOOKING] ([BookingID], [SlotNo], [CustomerID], [StylistID], [ServiceID], [Date], [Available], [Arrived], [Comment]) VALUES (N'7         ', N'slo10     ', N'105242998585655922697         ', N'118233419479102946333         ', N'Pr1129    ', CAST(N'2018-06-06T00:00:00.000' AS DateTime), N'N', N'Y', NULL)
INSERT [dbo].[BOOKING] ([BookingID], [SlotNo], [CustomerID], [StylistID], [ServiceID], [Date], [Available], [Arrived], [Comment]) VALUES (N'9         ', N'Slo13     ', N'105242998585655922697         ', N'118233419479102946333         ', N'Pr1129    ', CAST(N'2018-06-12T00:00:00.000' AS DateTime), N'N', NULL, NULL)
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
INSERT [dbo].[BUSINESS] ([BusinessID], [Vat%], [VatRegNo], [AddressLine1], [AddressLine2], [Phone], [WeekdayStart], [WeekdayEnd], [WeekendStart], [WeekendEnd], [PublicHolStart], [PublicHolEnd], [Logo]) VALUES (N'001       ', 15, N'1001      ', N'53 Marine Drive, Summerstrand', N'Port Elizabeth, 6001', N'0412345678', CAST(N'08:00:00' AS Time), CAST(N'18:00:00' AS Time), CAST(N'08:30:00' AS Time), CAST(N'17:30:00' AS Time), CAST(N'09:00:00' AS Time), CAST(N'17:00:00' AS Time), NULL)
INSERT [dbo].[EMPLOYEE] ([EmployeeID], [AddressLine1], [AddressLine2], [Type]) VALUES (N'112413834414360855751         ', NULL, NULL, N'R         ')
INSERT [dbo].[EMPLOYEE] ([EmployeeID], [AddressLine1], [AddressLine2], [Type]) VALUES (N'112475171777167063459         ', NULL, NULL, N'M         ')
INSERT [dbo].[EMPLOYEE] ([EmployeeID], [AddressLine1], [AddressLine2], [Type]) VALUES (N'118233419479102946333         ', NULL, NULL, N'S         ')
INSERT [dbo].[LENGTH] ([LengthID], [Description]) VALUES (N'L1000     ', N'Short')
INSERT [dbo].[LENGTH] ([LengthID], [Description]) VALUES (N'L1001     ', N'Medium')
INSERT [dbo].[LENGTH] ([LengthID], [Description]) VALUES (N'L1002     ', N'Long')
INSERT [dbo].[PRODUCT] ([ProductID], [Name], [ProductDescription], [Price], [ProductType(T/A/S)], [Active], [ProductImage]) VALUES (N'Pr1101    ', N'Thin braid bob', N'Thin, bob length plait braids', N' 350.00', N'S', N'Y', NULL)
INSERT [dbo].[PRODUCT] ([ProductID], [Name], [ProductDescription], [Price], [ProductType(T/A/S)], [Active], [ProductImage]) VALUES (N'Pr1102    ', N'Medium braid bob', N'Neck length, normal sized plait braids', N' 300.00', N'S', N'Y', NULL)
INSERT [dbo].[PRODUCT] ([ProductID], [Name], [ProductDescription], [Price], [ProductType(T/A/S)], [Active], [ProductImage]) VALUES (N'Pr1103    ', N'Box braid bob', N'Neck length, box plait braids', N' 320.00', N'S', N'Y', NULL)
INSERT [dbo].[PRODUCT] ([ProductID], [Name], [ProductDescription], [Price], [ProductType(T/A/S)], [Active], [ProductImage]) VALUES (N'Pr1104    ', N'Thin shoulder braids', N'Thin, shoulder length plait braids', N' 380.00', N'S', N'Y', NULL)
INSERT [dbo].[PRODUCT] ([ProductID], [Name], [ProductDescription], [Price], [ProductType(T/A/S)], [Active], [ProductImage]) VALUES (N'Pr1105    ', N'Medium shoulder braids', N'Shoulder length, normal sized plait braids', N' 350.00', N'S', N'Y', NULL)
INSERT [dbo].[PRODUCT] ([ProductID], [Name], [ProductDescription], [Price], [ProductType(T/A/S)], [Active], [ProductImage]) VALUES (N'Pr1106    ', N'Box shoulder braids', N'Shoulder length, thick plait braids', N' 370.00', N'S', N'Y', NULL)
INSERT [dbo].[PRODUCT] ([ProductID], [Name], [ProductDescription], [Price], [ProductType(T/A/S)], [Active], [ProductImage]) VALUES (N'Pr1107    ', N'Thin waist length braids', N'Thin, waist length plait braids', N' 420.00', N'S', N'Y', NULL)
INSERT [dbo].[PRODUCT] ([ProductID], [Name], [ProductDescription], [Price], [ProductType(T/A/S)], [Active], [ProductImage]) VALUES (N'Pr1108    ', N'Medium waist length braids', N'Normal width, waist length plait braids', N' 390.00', N'S', N'Y', NULL)
INSERT [dbo].[PRODUCT] ([ProductID], [Name], [ProductDescription], [Price], [ProductType(T/A/S)], [Active], [ProductImage]) VALUES (N'Pr1109    ', N'Box braids', N'Normal box braids(waist length, thick plait braids)', N' 410.00', N'S', N'Y', NULL)
INSERT [dbo].[PRODUCT] ([ProductID], [Name], [ProductDescription], [Price], [ProductType(T/A/S)], [Active], [ProductImage]) VALUES (N'Pr1110    ', N'Thin twist bob', N'Thin, bob length twist', N' 340.00', N'S', N'Y', NULL)
INSERT [dbo].[PRODUCT] ([ProductID], [Name], [ProductDescription], [Price], [ProductType(T/A/S)], [Active], [ProductImage]) VALUES (N'Pr1111    ', N'Medium twist bob', N'Neck length, normal sized twists', N' 300.00', N'S', N'Y', NULL)
INSERT [dbo].[PRODUCT] ([ProductID], [Name], [ProductDescription], [Price], [ProductType(T/A/S)], [Active], [ProductImage]) VALUES (N'Pr1112    ', N'Box twist bob', N'Neck length box twists', N' 310.00', N'S', N'Y', NULL)
INSERT [dbo].[PRODUCT] ([ProductID], [Name], [ProductDescription], [Price], [ProductType(T/A/S)], [Active], [ProductImage]) VALUES (N'Pr1113    ', N'Thin shoulder twist', N'Thin, shoulder length twists', N' 370.00', N'S', N'Y', NULL)
INSERT [dbo].[PRODUCT] ([ProductID], [Name], [ProductDescription], [Price], [ProductType(T/A/S)], [Active], [ProductImage]) VALUES (N'Pr1114    ', N'Medium shoulder twist', N'Normal width, shoulder length twists', N' 340.00', N'S', N'Y', NULL)
INSERT [dbo].[PRODUCT] ([ProductID], [Name], [ProductDescription], [Price], [ProductType(T/A/S)], [Active], [ProductImage]) VALUES (N'Pr1115    ', N'Box shoulder twist', N'Shoulder length box twist', N' 380.00', N'S', N'Y', NULL)
INSERT [dbo].[PRODUCT] ([ProductID], [Name], [ProductDescription], [Price], [ProductType(T/A/S)], [Active], [ProductImage]) VALUES (N'Pr1116    ', N'Thin waist length twist', N'Thin, waist length twists', N' 410.00', N'S', N'Y', NULL)
INSERT [dbo].[PRODUCT] ([ProductID], [Name], [ProductDescription], [Price], [ProductType(T/A/S)], [Active], [ProductImage]) VALUES (N'Pr1117    ', N'Medium waist length twist', N'Waist length twists', N' 390.00', N'S', N'Y', NULL)
INSERT [dbo].[PRODUCT] ([ProductID], [Name], [ProductDescription], [Price], [ProductType(T/A/S)], [Active], [ProductImage]) VALUES (N'Pr1118    ', N'Box twist', N'Normal box twists(waist length, thick twists)', N' 420.00', N'S', N'Y', NULL)
INSERT [dbo].[PRODUCT] ([ProductID], [Name], [ProductDescription], [Price], [ProductType(T/A/S)], [Active], [ProductImage]) VALUES (N'Pr1119    ', N'Straight-back bob', N'Normal width, straight back bob', N' 200.00', N'S', N'Y', NULL)
INSERT [dbo].[PRODUCT] ([ProductID], [Name], [ProductDescription], [Price], [ProductType(T/A/S)], [Active], [ProductImage]) VALUES (N'Pr1120    ', N'Straight-back shoulder', N'Normal width, shoulder length cornrows', N' 250.00', N'S', N'Y', NULL)
INSERT [dbo].[PRODUCT] ([ProductID], [Name], [ProductDescription], [Price], [ProductType(T/A/S)], [Active], [ProductImage]) VALUES (N'Pr1121    ', N'Straight-back waist', N'Normal width, waist length cornrows', N' 300.00', N'S', N'Y', NULL)
INSERT [dbo].[PRODUCT] ([ProductID], [Name], [ProductDescription], [Price], [ProductType(T/A/S)], [Active], [ProductImage]) VALUES (N'Pr1122    ', N'Box straight-back shoulder', N'Thick, shoulder length cornrows', N' 150.00', N'S', N'Y', NULL)
INSERT [dbo].[PRODUCT] ([ProductID], [Name], [ProductDescription], [Price], [ProductType(T/A/S)], [Active], [ProductImage]) VALUES (N'Pr1123    ', N'Box straight-back waist', N'Thick, waist length cornrows', N' 180.00', N'S', N'Y', NULL)
INSERT [dbo].[PRODUCT] ([ProductID], [Name], [ProductDescription], [Price], [ProductType(T/A/S)], [Active], [ProductImage]) VALUES (N'Pr1124    ', N'Upstyle shoulder', N'Normal width, shoulder length, upstyle cornrows', N' 250.00', N'S', N'Y', NULL)
INSERT [dbo].[PRODUCT] ([ProductID], [Name], [ProductDescription], [Price], [ProductType(T/A/S)], [Active], [ProductImage]) VALUES (N'Pr1125    ', N'Upstyle waist', N'Normal width, waist length, upstyle cornrows', N' 300.00', N'S', N'Y', NULL)
INSERT [dbo].[PRODUCT] ([ProductID], [Name], [ProductDescription], [Price], [ProductType(T/A/S)], [Active], [ProductImage]) VALUES (N'Pr1126    ', N'Box upstyle shoulder ', N'Shoulder length, thick, upstyle cornrows', N' 200.00', N'S', N'Y', NULL)
INSERT [dbo].[PRODUCT] ([ProductID], [Name], [ProductDescription], [Price], [ProductType(T/A/S)], [Active], [ProductImage]) VALUES (N'Pr1127    ', N'Box upstyle waist', N'Waist length, thick, upstyle cornrows', N' 250.00', N'S', N'Y', NULL)
INSERT [dbo].[PRODUCT] ([ProductID], [Name], [ProductDescription], [Price], [ProductType(T/A/S)], [Active], [ProductImage]) VALUES (N'Pr1128    ', N'Faux locs', N'Application of faux locs', N' 400.00', N'S', N'Y', NULL)
INSERT [dbo].[PRODUCT] ([ProductID], [Name], [ProductDescription], [Price], [ProductType(T/A/S)], [Active], [ProductImage]) VALUES (N'Pr1129    ', N'Cut', N'Hair cut', N' 25.00', N'S', N'Y', NULL)
INSERT [dbo].[PRODUCT] ([ProductID], [Name], [ProductDescription], [Price], [ProductType(T/A/S)], [Active], [ProductImage]) VALUES (N'Pr1130    ', N'Jamaican Black Castor Oil(Original)', N'Castor oil', N' 180.00', N'T', N'Y', NULL)
INSERT [dbo].[PRODUCT] ([ProductID], [Name], [ProductDescription], [Price], [ProductType(T/A/S)], [Active], [ProductImage]) VALUES (N'Pr1131    ', N'Cantu Curl Cream', N'Curl defining cream', N' 70.00', N'T', N'Y', NULL)
INSERT [dbo].[PRODUCT] ([ProductID], [Name], [ProductDescription], [Price], [ProductType(T/A/S)], [Active], [ProductImage]) VALUES (N'Pr1132    ', N'ORS Curls Unleashed Sulphate-Free Shampoo ', N'Clarifying shampoo', N' 170.00', N'T', N'Y', NULL)
INSERT [dbo].[PRODUCT] ([ProductID], [Name], [ProductDescription], [Price], [ProductType(T/A/S)], [Active], [ProductImage]) VALUES (N'Pr1133    ', N'Dark and Lovely Au Naturale Plaiting Pudding Cream', N'Cream', N' 85.00', N'T', N'Y', NULL)
INSERT [dbo].[PRODUCT] ([ProductID], [Name], [ProductDescription], [Price], [ProductType(T/A/S)], [Active], [ProductImage]) VALUES (N'Pr1134    ', N'Cantu Extra Hold Edge Stay Gel', N'Edge controlling gel', N' 75.00', N'T', N'Y', NULL)
INSERT [dbo].[PRODUCT] ([ProductID], [Name], [ProductDescription], [Price], [ProductType(T/A/S)], [Active], [ProductImage]) VALUES (N'Pr1135    ', N'Hair Decoration Filigree Tube', N'Decorative hair accessory(hair cuffs)', N' 20.00', N'A', N'Y', NULL)
INSERT [dbo].[PRODUCT] ([ProductID], [Name], [ProductDescription], [Price], [ProductType(T/A/S)], [Active], [ProductImage]) VALUES (N'Pr1136    ', N'X-Pression ultra braid #1', N'Synthetic hair for braiding', N' 35.00', N'A', N'Y', NULL)
INSERT [dbo].[PRODUCT] ([ProductID], [Name], [ProductDescription], [Price], [ProductType(T/A/S)], [Active], [ProductImage]) VALUES (N'Pr1137    ', N'Joedir Nature Straight(100% human hair) 10"', N'Weave packet', N' 150.00', N'A', N'Y', NULL)
INSERT [dbo].[PRODUCT] ([ProductID], [Name], [ProductDescription], [Price], [ProductType(T/A/S)], [Active], [ProductImage]) VALUES (N'Pr1138    ', N'Donna Wig Cap', N'2 Pack Black Donna wig cap', N' 40.00', N'A', N'Y', NULL)
INSERT [dbo].[PRODUCT] ([ProductID], [Name], [ProductDescription], [Price], [ProductType(T/A/S)], [Active], [ProductImage]) VALUES (N'Pr1139    ', N'ORS No-Lye Hair Relaxer', N'Hair relaxer', N' 75.00', N'T', N'Y', NULL)
INSERT [dbo].[PRODUCT] ([ProductID], [Name], [ProductDescription], [Price], [ProductType(T/A/S)], [Active], [ProductImage]) VALUES (N'Pr1140    ', N'X-Pression ultra braid #350', N'Synthetic hair for braiding', N' 35.00', N'A', N'Y', NULL)
INSERT [dbo].[PRODUCT] ([ProductID], [Name], [ProductDescription], [Price], [ProductType(T/A/S)], [Active], [ProductImage]) VALUES (N'Pr1141    ', N'Dark and Lovely Au Naturale Hair Butter', N'Anti-breakage cream', N' 75.00', N'T', N'Y', NULL)
INSERT [dbo].[PRODUCT] ([ProductID], [Name], [ProductDescription], [Price], [ProductType(T/A/S)], [Active], [ProductImage]) VALUES (N'Pr1142    ', N'Afro Botanics Twist, Curl & Define Cream', N'Cream', N' 65.00', N'T', N'Y', NULL)
INSERT [dbo].[PRODUCT] ([ProductID], [Name], [ProductDescription], [Price], [ProductType(T/A/S)], [Active], [ProductImage]) VALUES (N'Pr1143    ', N'ORS Curls Unleashed Intense Hair Conditioner', N'Hair Conditioner', N' 160.00', N'T', N'Y', NULL)
INSERT [dbo].[PRODUCT] ([ProductID], [Name], [ProductDescription], [Price], [ProductType(T/A/S)], [Active], [ProductImage]) VALUES (N'Pr1144    ', N'Chocolate Premium Quality(100% human hair) 16"', N'Weave packet', N' 360.00', N'A', N'Y', NULL)
INSERT [dbo].[PRODUCT] ([ProductID], [Name], [ProductDescription], [Price], [ProductType(T/A/S)], [Active], [ProductImage]) VALUES (N'Pr1145    ', N'X-Pression ultra braid #2', N'Synthetic hair for braiding', N' 35.00', N'A', N'Y', NULL)
INSERT [dbo].[PRODUCT] ([ProductID], [Name], [ProductDescription], [Price], [ProductType(T/A/S)], [Active], [ProductImage]) VALUES (N'Pr1146    ', N'Shea Moisture Coconut&Hibiscus Curl Smoothie', N'Curl defining cream', N' 270.00', N'T', N'Y', NULL)
INSERT [dbo].[PRODUCT] ([ProductID], [Name], [ProductDescription], [Price], [ProductType(T/A/S)], [Active], [ProductImage]) VALUES (N'Pr1147    ', N'Colour', N'Hair dye', N' 40.00', N'S', N'Y', NULL)
INSERT [dbo].[PRODUCT] ([ProductID], [Name], [ProductDescription], [Price], [ProductType(T/A/S)], [Active], [ProductImage]) VALUES (N'Pr1148    ', N'Relax', N'Process of relaxing hair', N' 100.00', N'S', N'Y', NULL)
INSERT [dbo].[PRODUCT] ([ProductID], [Name], [ProductDescription], [Price], [ProductType(T/A/S)], [Active], [ProductImage]) VALUES (N'Pr1149    ', N'Afro Botanics Moisturising Shampoo', N'Clarifying Shampoo', N' 100.00', N'T', N'Y', NULL)
INSERT [dbo].[PRODUCT] ([ProductID], [Name], [ProductDescription], [Price], [ProductType(T/A/S)], [Active], [ProductImage]) VALUES (N'Pr1150    ', N'Head&Shoulders Moisturising Care Hairfood', N'Hair food', N' 75.00', N'T', N'Y', NULL)
INSERT [dbo].[PRODUCT] ([ProductID], [Name], [ProductDescription], [Price], [ProductType(T/A/S)], [Active], [ProductImage]) VALUES (N'Pr1151    ', N'Darling Hair Extensions Yaki Braid 1B', N'Synthetic hair for braiding', N' 13.00', N'A', N'Y', NULL)
INSERT [dbo].[PRODUCT] ([ProductID], [Name], [ProductDescription], [Price], [ProductType(T/A/S)], [Active], [ProductImage]) VALUES (N'Pr1152    ', N'Frika Braid Maxi Dread 1B', N'Synthetic hair for soft dread', N' 70.00', N'A', N'Y', NULL)
INSERT [dbo].[PRODUCT] ([ProductID], [Name], [ProductDescription], [Price], [ProductType(T/A/S)], [Active], [ProductImage]) VALUES (N'Pr1153    ', N'Darling Soft Dread ', N'Synthetic hair for soft dread', N' 60.00', N'A', N'Y', NULL)
INSERT [dbo].[PRODUCT] ([ProductID], [Name], [ProductDescription], [Price], [ProductType(T/A/S)], [Active], [ProductImage]) VALUES (N'Pr1154    ', N'Weave (no planting)', N'Application of weave', N' 180.00', N'S', N'Y', NULL)
INSERT [dbo].[PRODUCT] ([ProductID], [Name], [ProductDescription], [Price], [ProductType(T/A/S)], [Active], [ProductImage]) VALUES (N'Pr1155    ', N'Weave (with planting)', N'Application of weave', N' 200.00', N'S', N'Y', NULL)
INSERT [dbo].[PRODUCT] ([ProductID], [Name], [ProductDescription], [Price], [ProductType(T/A/S)], [Active], [ProductImage]) VALUES (N'Pr1156    ', N'Frika Braid Maxi Dread 1B/350', N'Synthetic hair for soft dread', N' 70.00', N'A', N'Y', NULL)
INSERT [dbo].[PRODUCT] ([ProductID], [Name], [ProductDescription], [Price], [ProductType(T/A/S)], [Active], [ProductImage]) VALUES (N'Pr1157    ', N'Inecto Ultra Gloss Semi-Permanent Hair Colour Kit(Black Leather)', N'Hair dye', N' 30.00', N'T', N'Y', NULL)
INSERT [dbo].[PRODUCT] ([ProductID], [Name], [ProductDescription], [Price], [ProductType(T/A/S)], [Active], [ProductImage]) VALUES (N'Pr1158    ', N'Jamaican Black Castor Oil(Rosemary)', N'Castor Oil', N' 200.00', N'T', N'Y', NULL)
INSERT [dbo].[PRODUCT] ([ProductID], [Name], [ProductDescription], [Price], [ProductType(T/A/S)], [Active], [ProductImage]) VALUES (N'Pr1159    ', N'Aunt Jackie''s Flaxseed Elongating Curling Gel', N'Curl & twist defining gel', N' 80.00', N'T', N'Y', NULL)
INSERT [dbo].[PRODUCT] ([ProductID], [Name], [ProductDescription], [Price], [ProductType(T/A/S)], [Active], [ProductImage]) VALUES (N'Pr1160    ', N'Wash', N'Washing natural hair', N' 35.00', N'S', N'Y', NULL)
INSERT [dbo].[PRODUCT] ([ProductID], [Name], [ProductDescription], [Price], [ProductType(T/A/S)], [Active], [ProductImage]) VALUES (N'Pr1161    ', N'Frika Braid Hot Fibre 1B Rich Black', N'Synthetic hair for braiding', N' 35.00', N'A', N'Y', NULL)
INSERT [dbo].[PRODUCT] ([ProductID], [Name], [ProductDescription], [Price], [ProductType(T/A/S)], [Active], [ProductImage]) VALUES (N'Pr1162    ', N'Frika Hotex Weave 18" D33', N'Weave Packet', N' 90.00', N'A', N'Y', NULL)
INSERT [dbo].[PRODUCT] ([ProductID], [Name], [ProductDescription], [Price], [ProductType(T/A/S)], [Active], [ProductImage]) VALUES (N'Pr1163    ', N'Aunt Jackie''s Curls And Coils Oh So Clean Shampoo', N'Moisturising Shampoo', N' 60.00', N'T', N'Y', NULL)
INSERT [dbo].[PRODUCT] ([ProductID], [Name], [ProductDescription], [Price], [ProductType(T/A/S)], [Active], [ProductImage]) VALUES (N'Pr1164    ', N'Darling Hair Extensions Brazillian Wave 1', N'Weave Packet', N' 85.00', N'A', N'Y', NULL)
INSERT [dbo].[PRODUCT] ([ProductID], [Name], [ProductDescription], [Price], [ProductType(T/A/S)], [Active], [ProductImage]) VALUES (N'Pr1165    ', N'Chocolate Premium Quality(100% human hair) 16"', N'Weave Packet', N' 360.00', N'A', N'Y', NULL)
INSERT [dbo].[PRODUCT] ([ProductID], [Name], [ProductDescription], [Price], [ProductType(T/A/S)], [Active], [ProductImage]) VALUES (N'Pr1166    ', N'Joedir Nature Straight(100% human hair) 12"', N'Weave Packet', N' 160.00', N'A', N'Y', NULL)
INSERT [dbo].[PRODUCT] ([ProductID], [Name], [ProductDescription], [Price], [ProductType(T/A/S)], [Active], [ProductImage]) VALUES (N'Pr1167    ', N'Darling One Million Braids Ombre', N'Synthetic hair for braiding', N' 35.00', N'A', N'Y', NULL)
INSERT [dbo].[PRODUCT] ([ProductID], [Name], [ProductDescription], [Price], [ProductType(T/A/S)], [Active], [ProductImage]) VALUES (N'Pr1168    ', N'Nikki R Dreads 22"', N'Faux Locs', N' 90.00', N'A', N'Y', NULL)
INSERT [dbo].[PRODUCT] ([ProductID], [Name], [ProductDescription], [Price], [ProductType(T/A/S)], [Active], [ProductImage]) VALUES (N'Pr1169    ', N'Shea Moisture Jamaican Black Castor Oil Shampoo', N'Clarifying Shampoo', N' 250.00', N'T', N'Y', NULL)
INSERT [dbo].[PRODUCT] ([ProductID], [Name], [ProductDescription], [Price], [ProductType(T/A/S)], [Active], [ProductImage]) VALUES (N'Pr1170    ', N'Afro Botanics Repairing and Strengthening Treatment', N'Hair conditioner', N' 65.00', N'T', N'Y', NULL)
INSERT [dbo].[PRODUCT] ([ProductID], [Name], [ProductDescription], [Price], [ProductType(T/A/S)], [Active], [ProductImage]) VALUES (N'Pr1171    ', N'Cantu Shea Butter For Natural Hair Co-Wash', N'Co-Wash', N' 200.00', N'T', N'Y', NULL)
INSERT [dbo].[PRODUCT] ([ProductID], [Name], [ProductDescription], [Price], [ProductType(T/A/S)], [Active], [ProductImage]) VALUES (N'Pr1172    ', N'Nikki Soft Dreads', N'Synthetic hair for soft dread', N' 30.00', N'A', N'Y', NULL)
INSERT [dbo].[PRODUCT] ([ProductID], [Name], [ProductDescription], [Price], [ProductType(T/A/S)], [Active], [ProductImage]) VALUES (N'Pr1173    ', N'ORS Curls Unleashed Leave-In Conditioner', N'Conditioner', N' 165.00', N'T', N'Y', NULL)
INSERT [dbo].[PRODUCT] ([ProductID], [Name], [ProductDescription], [Price], [ProductType(T/A/S)], [Active], [ProductImage]) VALUES (N'Pr1174    ', N'Vicher Brazillian Remi 12"', N'Weave packet', N' 500.00', N'A', N'Y', NULL)
INSERT [dbo].[SALE] ([SaleID], [Date], [CustomerID], [PaymentType], [BookingID]) VALUES (N'10        ', CAST(N'2018-04-30T00:00:00.000' AS DateTime), N'105242998585655922697         ', N'Credit    ', N'10        ')
INSERT [dbo].[SALE] ([SaleID], [Date], [CustomerID], [PaymentType], [BookingID]) VALUES (N'7         ', CAST(N'2018-06-06T00:00:00.000' AS DateTime), N'105242998585655922697         ', N'Credit    ', N'7         ')
INSERT [dbo].[SALES_DTL] ([SaleID], [ProductID], [Qty], [Price]) VALUES (N'10        ', N'Pr1106    ', 1, 370.0000)
INSERT [dbo].[SALES_DTL] ([SaleID], [ProductID], [Qty], [Price]) VALUES (N'7         ', N'Pr1129    ', 1, 25.0000)
INSERT [dbo].[SALES_DTL] ([SaleID], [ProductID], [Qty], [Price]) VALUES (N'7         ', N'Pr1132    ', 1, 170.0000)
INSERT [dbo].[SERVICE] ([ServiceID], [NoOfSlots], [Type(A/N/B)]) VALUES (N'Pr1101    ', 1, N'B')
INSERT [dbo].[SERVICE] ([ServiceID], [NoOfSlots], [Type(A/N/B)]) VALUES (N'Pr1102    ', 1, N'B')
INSERT [dbo].[SERVICE] ([ServiceID], [NoOfSlots], [Type(A/N/B)]) VALUES (N'Pr1103    ', 1, N'B')
INSERT [dbo].[SERVICE] ([ServiceID], [NoOfSlots], [Type(A/N/B)]) VALUES (N'Pr1104    ', 1, N'B')
INSERT [dbo].[SERVICE] ([ServiceID], [NoOfSlots], [Type(A/N/B)]) VALUES (N'Pr1105    ', 1, N'B')
INSERT [dbo].[SERVICE] ([ServiceID], [NoOfSlots], [Type(A/N/B)]) VALUES (N'Pr1106    ', 1, N'B')
INSERT [dbo].[SERVICE] ([ServiceID], [NoOfSlots], [Type(A/N/B)]) VALUES (N'Pr1107    ', 1, N'B')
INSERT [dbo].[SERVICE] ([ServiceID], [NoOfSlots], [Type(A/N/B)]) VALUES (N'Pr1108    ', 1, N'B')
INSERT [dbo].[SERVICE] ([ServiceID], [NoOfSlots], [Type(A/N/B)]) VALUES (N'Pr1109    ', 1, N'B')
INSERT [dbo].[SERVICE] ([ServiceID], [NoOfSlots], [Type(A/N/B)]) VALUES (N'Pr1110    ', 1, N'B')
INSERT [dbo].[SERVICE] ([ServiceID], [NoOfSlots], [Type(A/N/B)]) VALUES (N'Pr1111    ', 1, N'B')
INSERT [dbo].[SERVICE] ([ServiceID], [NoOfSlots], [Type(A/N/B)]) VALUES (N'Pr1112    ', 1, N'B')
INSERT [dbo].[SERVICE] ([ServiceID], [NoOfSlots], [Type(A/N/B)]) VALUES (N'Pr1113    ', 1, N'B')
INSERT [dbo].[SERVICE] ([ServiceID], [NoOfSlots], [Type(A/N/B)]) VALUES (N'Pr1114    ', 1, N'B')
INSERT [dbo].[SERVICE] ([ServiceID], [NoOfSlots], [Type(A/N/B)]) VALUES (N'Pr1115    ', 1, N'B')
INSERT [dbo].[SERVICE] ([ServiceID], [NoOfSlots], [Type(A/N/B)]) VALUES (N'Pr1116    ', 1, N'B')
INSERT [dbo].[SERVICE] ([ServiceID], [NoOfSlots], [Type(A/N/B)]) VALUES (N'Pr1117    ', 1, N'B')
INSERT [dbo].[SERVICE] ([ServiceID], [NoOfSlots], [Type(A/N/B)]) VALUES (N'Pr1118    ', 1, N'B')
INSERT [dbo].[SERVICE] ([ServiceID], [NoOfSlots], [Type(A/N/B)]) VALUES (N'Pr1119    ', 1, N'B')
INSERT [dbo].[SERVICE] ([ServiceID], [NoOfSlots], [Type(A/N/B)]) VALUES (N'Pr1120    ', 1, N'B')
INSERT [dbo].[SERVICE] ([ServiceID], [NoOfSlots], [Type(A/N/B)]) VALUES (N'Pr1121    ', 1, N'B')
INSERT [dbo].[SERVICE] ([ServiceID], [NoOfSlots], [Type(A/N/B)]) VALUES (N'Pr1122    ', 1, N'B')
INSERT [dbo].[SERVICE] ([ServiceID], [NoOfSlots], [Type(A/N/B)]) VALUES (N'Pr1123    ', 1, N'B')
INSERT [dbo].[SERVICE] ([ServiceID], [NoOfSlots], [Type(A/N/B)]) VALUES (N'Pr1124    ', 1, N'B')
INSERT [dbo].[SERVICE] ([ServiceID], [NoOfSlots], [Type(A/N/B)]) VALUES (N'Pr1125    ', 1, N'B')
INSERT [dbo].[SERVICE] ([ServiceID], [NoOfSlots], [Type(A/N/B)]) VALUES (N'Pr1126    ', 1, N'B')
INSERT [dbo].[SERVICE] ([ServiceID], [NoOfSlots], [Type(A/N/B)]) VALUES (N'Pr1127    ', 1, N'B')
INSERT [dbo].[SERVICE] ([ServiceID], [NoOfSlots], [Type(A/N/B)]) VALUES (N'Pr1128    ', 1, N'A')
INSERT [dbo].[SERVICE] ([ServiceID], [NoOfSlots], [Type(A/N/B)]) VALUES (N'Pr1129    ', 1, N'N')
INSERT [dbo].[SERVICE] ([ServiceID], [NoOfSlots], [Type(A/N/B)]) VALUES (N'Pr1147    ', 1, N'N')
INSERT [dbo].[SERVICE] ([ServiceID], [NoOfSlots], [Type(A/N/B)]) VALUES (N'Pr1148    ', 1, N'N')
INSERT [dbo].[SERVICE] ([ServiceID], [NoOfSlots], [Type(A/N/B)]) VALUES (N'Pr1154    ', 1, N'A')
INSERT [dbo].[SERVICE] ([ServiceID], [NoOfSlots], [Type(A/N/B)]) VALUES (N'Pr1155    ', 1, N'A')
INSERT [dbo].[SERVICE] ([ServiceID], [NoOfSlots], [Type(A/N/B)]) VALUES (N'Pr1160    ', 1, N'N')
INSERT [dbo].[STYLE] ([StyleID], [Description]) VALUES (N'S1001     ', N'Plait')
INSERT [dbo].[STYLE] ([StyleID], [Description]) VALUES (N'S1002     ', N'Twist')
INSERT [dbo].[STYLE] ([StyleID], [Description]) VALUES (N'S1003     ', N'Straight-back cornrows')
INSERT [dbo].[STYLE] ([StyleID], [Description]) VALUES (N'S1004     ', N'Upstyle cornrows')
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
INSERT [dbo].[TREATMENT] ([TreatmentID], [Qty], [TreatmentType], [BrandID]) VALUES (N'Pr1130    ', 200, N'Oil', N'B03       ')
INSERT [dbo].[TREATMENT] ([TreatmentID], [Qty], [TreatmentType], [BrandID]) VALUES (N'Pr1131    ', 200, N'Styling cream', N'B04       ')
INSERT [dbo].[TREATMENT] ([TreatmentID], [Qty], [TreatmentType], [BrandID]) VALUES (N'Pr1132    ', 200, N'Shampoo', N'B01       ')
INSERT [dbo].[TREATMENT] ([TreatmentID], [Qty], [TreatmentType], [BrandID]) VALUES (N'Pr1133    ', 200, N'Styling cream', N'B09       ')
INSERT [dbo].[TREATMENT] ([TreatmentID], [Qty], [TreatmentType], [BrandID]) VALUES (N'Pr1134    ', 200, N'Styling gel', N'B04       ')
INSERT [dbo].[TREATMENT] ([TreatmentID], [Qty], [TreatmentType], [BrandID]) VALUES (N'Pr1139    ', 200, N'Relaxer', N'B01       ')
INSERT [dbo].[TREATMENT] ([TreatmentID], [Qty], [TreatmentType], [BrandID]) VALUES (N'Pr1141    ', 200, N'Styling cream', N'B09       ')
INSERT [dbo].[TREATMENT] ([TreatmentID], [Qty], [TreatmentType], [BrandID]) VALUES (N'Pr1142    ', 200, N'Styling cream', N'B06       ')
INSERT [dbo].[TREATMENT] ([TreatmentID], [Qty], [TreatmentType], [BrandID]) VALUES (N'Pr1143    ', 200, N'Deep conditioner', N'B01       ')
INSERT [dbo].[TREATMENT] ([TreatmentID], [Qty], [TreatmentType], [BrandID]) VALUES (N'Pr1146    ', 200, N'Styling cream', N'B08       ')
INSERT [dbo].[TREATMENT] ([TreatmentID], [Qty], [TreatmentType], [BrandID]) VALUES (N'Pr1149    ', 200, N'Shampoo', N'B06       ')
INSERT [dbo].[TREATMENT] ([TreatmentID], [Qty], [TreatmentType], [BrandID]) VALUES (N'Pr1150    ', 200, N'Hair food', N'B14       ')
INSERT [dbo].[TREATMENT] ([TreatmentID], [Qty], [TreatmentType], [BrandID]) VALUES (N'Pr1157    ', 200, N'Hair dye', N'B11       ')
INSERT [dbo].[TREATMENT] ([TreatmentID], [Qty], [TreatmentType], [BrandID]) VALUES (N'Pr1158    ', 200, N'Oil', N'B03       ')
INSERT [dbo].[TREATMENT] ([TreatmentID], [Qty], [TreatmentType], [BrandID]) VALUES (N'Pr1159    ', 200, N'Styling gel', N'B12       ')
INSERT [dbo].[TREATMENT] ([TreatmentID], [Qty], [TreatmentType], [BrandID]) VALUES (N'Pr1163    ', 200, N'Shampoo', N'B12       ')
INSERT [dbo].[TREATMENT] ([TreatmentID], [Qty], [TreatmentType], [BrandID]) VALUES (N'Pr1169    ', 200, N'Shampoo', N'B08       ')
INSERT [dbo].[TREATMENT] ([TreatmentID], [Qty], [TreatmentType], [BrandID]) VALUES (N'Pr1170    ', 200, N'Deep conditioner', N'B06       ')
INSERT [dbo].[TREATMENT] ([TreatmentID], [Qty], [TreatmentType], [BrandID]) VALUES (N'Pr1171    ', 200, N'Co-Wash', N'B04       ')
INSERT [dbo].[TREATMENT] ([TreatmentID], [Qty], [TreatmentType], [BrandID]) VALUES (N'Pr1173    ', 200, N'Leave-In conditioner', N'B01       ')
INSERT [dbo].[USER] ([UserID], [FirstName], [LastName], [UserName], [Email], [ContactNo], [Password], [UserType], [Active], [UserImage], [AccountType]) VALUES (N'105242998585655922697         ', N'Customer', N'Cheveux', N'Customer                      ', N'customercheveux@gmail.com', N'0784605555', NULL, N'C', N'T', N'https://lh4.googleusercontent.com/-yKpO8v1GOkk/AAAAAAAAAAI/AAAAAAAAAAA/AB6qoq22Zi_5jzEtYv_VVi_ceoUlt12v1A/s96-c/photo.jpg', NULL)
INSERT [dbo].[USER] ([UserID], [FirstName], [LastName], [UserName], [Email], [ContactNo], [Password], [UserType], [Active], [UserImage], [AccountType]) VALUES (N'112413834414360855751         ', N'Receptionist', N'Cheveux', N'Rexeptionist                  ', N'receptionistcheveux@gmail.com', N'0789456123', NULL, N'E', N'T', N'https://lh4.googleusercontent.com/-3PrjKQq46zI/AAAAAAAAAAI/AAAAAAAAAAA/AB6qoq1D5rM3a2me8sh__Mhj3do3jjR5bw/s96-c/photo.jpg', NULL)
INSERT [dbo].[USER] ([UserID], [FirstName], [LastName], [UserName], [Email], [ContactNo], [Password], [UserType], [Active], [UserImage], [AccountType]) VALUES (N'112475171777167063459         ', N'Manager', N'Cheveux', N'Manager', N'managercheveux@gmail.com', N'0456789123', NULL, N'E', N'T', N'https://lh5.googleusercontent.com/-ypchYBYV0nQ/AAAAAAAAAAI/AAAAAAAAAAA/AB6qoq3Qp0IbVDPsi5FBg1ol5BCefx_8GA/s96-c/photo.jpg', NULL)
INSERT [dbo].[USER] ([UserID], [FirstName], [LastName], [UserName], [Email], [ContactNo], [Password], [UserType], [Active], [UserImage], [AccountType]) VALUES (N'118233419479102946333         ', N'Stylist', N'Cheveux', N'Stylist', N'stylistcheveux2@gmail.com', N'0987654321', NULL, N'E', N'T', N'https://lh3.googleusercontent.com/-qhraoOZWvNE/AAAAAAAAAAI/AAAAAAAAAAA/AB6qoq3-X3wVvrbx_YV72KsSnIgoNTJ4gg/s96-c/photo.jpg', NULL)
INSERT [dbo].[WIDTH] ([WidthID], [Description]) VALUES (N'W1000     ', N'Thin')
INSERT [dbo].[WIDTH] ([WidthID], [Description]) VALUES (N'W1001     ', N'Normal')
INSERT [dbo].[WIDTH] ([WidthID], [Description]) VALUES (N'W1002     ', N'Box')
/****** Object:  StoredProcedure [dbo].[SP_AddBooking]    Script Date: 2018/06/09 13:00:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

cREATE PROCEDURE [dbo].[SP_AddBooking]
	@Slot nchar(10),
	@CustomerID nchar(30),
	@StylistID nchar(30),
	@ServiceID nchar(10),
	@Date date,
	@Comment varchar(MAX)

AS
BEGIN 
	BEGIN TRY
		BEGIN TRANSACTION
			INSERT INTO BOOKING(SlotNo, CustomerID, StylistID, ServiceID, [Date], Comment)
			VALUES(@Slot, @CustomerID, @StylistID, @ServiceID, @Date, @Comment)
		COMMIT TRANSACTION
	END TRY
	BEGIN CATCH
		IF @@TRANCOUNT > 0
			ROLLBACK TRANSACTION
	END CATCH

END
GO
/****** Object:  StoredProcedure [dbo].[SP_AddUserGoogleAuth]    Script Date: 2018/06/09 13:00:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create PROCEDURE [dbo].[SP_AddUserGoogleAuth] 
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
/****** Object:  StoredProcedure [dbo].[SP_CheckForUserType]    Script Date: 2018/06/09 13:00:51 ******/
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
END
GO
/****** Object:  StoredProcedure [dbo].[SP_CheckIn]    Script Date: 2018/06/09 13:00:51 ******/
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
	@BookingID nchar(10), 
	@StylistID nchar(30) = null
AS
BEGIN
	BEGIN TRY 
		BEGIN TRANSACTION
			
			UPDATE BOOKING
			SET Arrived='Y'
			WHERE BOOKING.BookingID=@BookingID
			AND	  BOOKING.StylistID=@StylistID
			AND BOOKING.Arrived='N'
		COMMIT TRANSACTION
	END TRY 
	BEGIN CATCH 
		IF(@@TRANCOUNT > 0)
			ROLLBACK TRANSACTION
	END CATCH
END
GO
/****** Object:  StoredProcedure [dbo].[SP_CreateCustVisit]    Script Date: 2018/06/09 13:00:51 ******/
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
	@BookingID nchar = null,
	@Description varchar(50) = null
AS
BEGIN
	BEGIN TRY
		BEGIN TRANSACTION
			
			INSERT INTO CUST_VISIT (CustomerID,[Date],BookingID,[Description])
			VALUES (@CustomerID,
					@Date,
					@BookingID,
					@Description)


		COMMIT TRANSACTION
	END TRY
	BEGIN CATCH
		IF @@TRANCOUNT > 0
			ROLLBACK TRANSACTION
	END CATCH
END
GO
/****** Object:  StoredProcedure [dbo].[SP_DeleteBooking]    Script Date: 2018/06/09 13:00:51 ******/
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
/****** Object:  StoredProcedure [dbo].[SP_EditUser]    Script Date: 2018/06/09 13:00:51 ******/
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
	@ContactNo nchar(10)
AS
BEGIN
	begin try
		begin transaction
			UPDATE [User]
			SET UserName = @UserName,
				ContactNo = @ContactNo
			Where UserID = @UserID
		Commit transaction
	end try
	begin catch
		if @@TRANCOUNT > 0
			rollback transaction
	end catch
END
GO
/****** Object:  StoredProcedure [dbo].[SP_GetAllofBookingDTL]    Script Date: 2018/06/09 13:00:51 ******/
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
			cv.CustomerID,CONCAT(U.FirstName,' ',U.LastName)AS[CustomerName] ,
			P.[Name]AS[ServiceName], P.ProductDescription AS [ServiceDescription], P.Price,
			B.[Date], TS.StartTime, TS.EndTime   
			     
	From 		BOOKING B, TIMESLOT TS, [User] U, PRODUCT P, CUST_VISIT cv

	Where 		cv.BookingID = @BookingID 
	AND 		cv.CustomerID=@CustomerID
	AND			B.BookingID=cv.BookingID
	AND			B.CustomerID=cv.CustomerID
	AND 		B.SlotNo = TS.SlotNo 
	AND 		B.StylistID = U.UserID 
	AND 		B.ServiceID = P.ProductID  
END
GO
/****** Object:  StoredProcedure [dbo].[SP_GetBookingServiceDTL]    Script Date: 2018/06/09 13:00:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		S.Maqabangqa
-- Create date: 04.06.2018
-- Description:	Gets service details of customer booking
-- =============================================
CREATE PROCEDURE [dbo].[SP_GetBookingServiceDTL]
	@BookingID nchar(10),
	@CustomerID nchar(30) = null
AS
BEGIN
	SET NOCOUNT ON;
	SELECT b.BookingID, p.[Name]AS[ServiceName] , p.ProductDescription AS [ServiceDescription]
	FROM BOOKING b, [SERVICE] s, PRODUCT p
	WHERE b.BookingID=@BookingID
	AND   b.CustomerID=@CustomerID
	AND   b.ServiceID=s.ServiceID
	AND   s.ServiceID = p.ProductID
END
GO
/****** Object:  StoredProcedure [dbo].[SP_GetCurrentVATRate2]    Script Date: 2018/06/09 13:00:51 ******/
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
/****** Object:  StoredProcedure [dbo].[SP_GetCustomerPastBooking]    Script Date: 2018/06/09 13:00:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create PROCEDURE [dbo].[SP_GetCustomerPastBooking] 
	@CustID nchar(30)
AS
BEGIN
	Select P.[Name], P.ProductDescription, P.Price,  U.FirstName, B.[Date], TS.StartTime, BookingID, B.Arrived       
	From BOOKING B, TIMESLOT TS, [User] U, PRODUCT P
	Where CustomerID = @CustID 
	AND B.SlotNo = TS.SlotNo 
	AND B.StylistID = U.UserID 
	AND B.ServiceID = P.ProductID 
	AND (B.Arrived = 'Y' Or B.Arrived = 'N')
END
GO
/****** Object:  StoredProcedure [dbo].[SP_GetCustomerPastBookingDetail]    Script Date: 2018/06/09 13:00:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Description:	Given a booking ID it reaturns the deatails of that booking
-- =============================================
CREATE PROCEDURE [dbo].[SP_GetCustomerPastBookingDetail] 
	@bookingID nchar(10)
AS
BEGIN
	Select P.[Name], P.ProductDescription, P.Price,  U.FirstName, B.[Date], TS.StartTime, BookingID, B.Arrived       
	From BOOKING B, TIMESLOT TS, [User] U, PRODUCT P
	Where BookingID = @bookingID 
	AND B.SlotNo = TS.SlotNo 
	AND B.StylistID = U.UserID 
	AND B.ServiceID = P.ProductID 
	AND (B.Arrived = 'Y' Or B.Arrived = 'N')
END
GO
/****** Object:  StoredProcedure [dbo].[SP_GetCustomerUpcomingBookingDetails]    Script Date: 2018/06/09 13:00:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create PROCEDURE [dbo].[SP_GetCustomerUpcomingBookingDetails] 
	-- Add the parameters for the stored procedure here
	@BookingID nchar(10)
AS
BEGIN
	Select P.[Name], P.ProductDescription, P.Price, U.UserID,  U.FirstName, B.[Date], TS.StartTime, BookingID           
	From BOOKING B, TIMESLOT TS, [User] U, PRODUCT P
	Where BookingID = @BookingID 
	AND B.SlotNo = TS.SlotNo 
	AND B.StylistID = U.UserID 
	AND B.ServiceID = P.ProductID 
	AND B.Arrived is null 
END
GO
/****** Object:  StoredProcedure [dbo].[SP_GetCustomerUpcomingBookings]    Script Date: 2018/06/09 13:00:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SP_GetCustomerUpcomingBookings]
	@CustID nchar(30)
AS
BEGIN
Select P.[Name], P.ProductDescription, P.Price,  U.FirstName, B.[Date], TS.StartTime, BookingID, B.Arrived       
	From BOOKING B, TIMESLOT TS, [User] U, PRODUCT P
	Where CustomerID = @CustID 
	AND B.SlotNo = TS.SlotNo 
	AND B.StylistID = U.UserID 
	AND B.ServiceID = P.ProductID 
	AND B.Arrived is null
	END
GO
/****** Object:  StoredProcedure [dbo].[SP_GetEmpAgenda]    Script Date: 2018/06/09 13:00:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		S.Maqabangqa
-- Create date: 27.05.2018
-- Description:	Gets a specific employees agenda for the day.
-- =============================================
CREATE PROCEDURE [dbo].[SP_GetEmpAgenda]
	@EmployeeID nchar(30),
	@Date datetime = null
AS
BEGIN
	SET NOCOUNT ON;
	SELECT  	b.BookingID,
			u.[UserID],
			ts.StartTime, ts.EndTime
			, u.[FirstName]AS[CustomerFName],
			
			(SELECT u.FirstName
			 FROM [User] u
			 WHERE u.UserID=@EmployeeID) AS [EmpFName],

			p.[Name]AS[ServiceName], b.Arrived, b.[Date]

	FROM BOOKING b, TIMESLOT ts, [User] u,[SERVICE] s, PRODUCT p

	WHERE b.StylistID=@EmployeeID
	AND ts.SlotNo=b.SlotNo
	AND b.CustomerID=u.UserID
	AND b.ServiceID=s.ServiceID
	AND s.ServiceID=p.ProductID
	AND b.[Date] = @Date
	/*AND (b.Arrived='N' OR b.Arrived IS NULL)*/
	
	ORDER BY ts.StartTime,ts.EndTime
END
GO
/****** Object:  StoredProcedure [dbo].[SP_GetEmployeeType]    Script Date: 2018/06/09 13:00:51 ******/
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
/****** Object:  StoredProcedure [dbo].[SP_GetEmpNames]    Script Date: 2018/06/09 13:00:51 ******/
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
END
GO
/****** Object:  StoredProcedure [dbo].[SP_getInvoiceDL]    Script Date: 2018/06/09 13:00:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SP_getInvoiceDL] 
	@BookingID nchar(10)
AS
BEGIN
	Select p.[Name], d.Qty, d.Price
	From SALE s, SALES_DTL d, PRODUCT p
	Where s.SaleID = d.SaleID
	AND s.BookingID = @BookingID
	AND p.ProductID = d.ProductID
END
GO
/****** Object:  StoredProcedure [dbo].[SP_GetMyNextCustomer]    Script Date: 2018/06/09 13:00:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		S.Maqabangqa
-- Create date: 07.06.2018
-- Description:	Shows the stylist appointments of ONLY customers that have been checked-in (arrived for their appointment)
-- =============================================
Create PROCEDURE [dbo].[SP_GetMyNextCustomer]
    @EmployeeID nchar(30),
	@Date datetime = null
AS
BEGIN
	SET NOCOUNT ON;
	SELECT  	b.BookingID,
				u.[UserID],
				ts.StartTime, ts.EndTime
				, u.[FirstName]AS[CustomerFName],
			
				(SELECT u.FirstName
					FROM [User] u
					WHERE u.UserID=@EmployeeID) AS [EmpFName],

				p.[Name]AS[ServiceName], b.Arrived, b.[Date]

	FROM BOOKING b, TIMESLOT ts, [User] u,[SERVICE] s, PRODUCT p

	WHERE b.StylistID=@EmployeeID
	AND	  ts.SlotNo=b.SlotNo
	AND   b.CustomerID=u.UserID
	AND   b.ServiceID=s.ServiceID
	AND   s.ServiceID=p.ProductID
	AND   b.[Date] = @Date
	AND   b.Arrived='Y'
	
    ORDER BY ts.StartTime,ts.EndTime

END
GO
/****** Object:  StoredProcedure [dbo].[SP_GetServices]    Script Date: 2018/06/09 13:00:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[SP_GetServices]
AS
BEGIN
	SET NOCOUNT ON;

	SELECT Name
	FROM PRODUCT
	WHERE [ProductType(T/A/S)] = 'S';
END
GO
/****** Object:  StoredProcedure [dbo].[SP_GetStylist]    Script Date: 2018/06/09 13:00:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

Create PROCEDURE [dbo].[SP_GetStylist] 
	@ServiceID nchar(10)
AS
BEGIN
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT u.FirstName
	FROM USERS u, STYLIST_SERVICE ss, PRODUCT p
	WHERE @ServiceID=p.ProductID AND UserType='E' AND u.UserCode = ss.EmployeeID AND ss.ServiceID = p.ProductID
END
GO
/****** Object:  StoredProcedure [dbo].[SP_GetUserDetails]    Script Date: 2018/06/09 13:00:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SP_GetUserDetails]
	@ID nchar(30)
AS
BEGIN
	Select FirstName, LastName, UserName, Email, ContactNo, UserType, UserImage
	From [User]
	Where [User].[UserID] = @ID And Active = 'T'
END
GO
/****** Object:  StoredProcedure [dbo].[SP_ProductSearchByTerm]    Script Date: 2018/06/09 13:00:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create PROCEDURE [dbo].[SP_ProductSearchByTerm]
	@searchTerm varchar(50)
AS
BEGIN
	select P.Name, P.ProductDescription, P.Price, P.[ProductType(T/A/S)], P.ProductID
	From PRODUCT P
	Where (P.Name like '%'+@searchTerm+'%'  Or P.ProductDescription like '%'+@searchTerm+'%') AND P.Active = 'Y'
	Order By P.[ProductType(T/A/S)]
END
GO
/****** Object:  StoredProcedure [dbo].[SP_SearchByTermAndType]    Script Date: 2018/06/09 13:00:51 ******/
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
/****** Object:  StoredProcedure [dbo].[SP_SearchStylistsBySearchTerm]    Script Date: 2018/06/09 13:00:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create PROCEDURE [dbo].[SP_SearchStylistsBySearchTerm]
	@searchTerm varchar(50)
AS
BEGIN
	Select U.UserID, U.FirstName, U.LastName, U.UserImage
	From [User] U, EMPLOYEE E
	Where (U.FirstName like '%'+@searchTerm+'%'  Or U.LastName like '%'+@searchTerm+'%')
	And U.UserID = E.EmployeeID 
	And E.Type= 'S' 
END
GO
/****** Object:  StoredProcedure [dbo].[SP_UpdateBooking]    Script Date: 2018/06/09 13:00:51 ******/
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
	@ServiceID nchar(10),
	@Date datetime
AS
BEGIN
	begin try
		begin transaction
			UPDATE BOOKING
			SET SlotNo = @SlotNo, 
				StylistID = @StylistID,
				ServiceID = @ServiceID,
				[Date] = @Date,
				Arrived = 'N'
			Where BookingID = @BookingID 
			AND (select Available
					from BOOKING
					Where Date = @Date
					AND SlotNo = @SlotNo
					AND StylistID = @StylistID) = 'Y'
					commit transaction
	end try
	begin catch
		if @@TRANCOUNT > 0
			rollback transaction
	end catch
END
GO
/****** Object:  StoredProcedure [dbo].[SP_UpdateCustVisit]    Script Date: 2018/06/09 13:00:51 ******/
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
	@BookingID nchar(10) null,
	@Description varchar(50) = null
AS
BEGIN
	BEGIN TRY 
		BEGIN TRANSACTION
			UPDATE CUST_VISIT
			SET	   [Description] = @Description
			WHERE CustomerID=@CustomerID
			AND	  BookingID=@BookingID
		COMMIT TRANSACTION
	END TRY 
	BEGIN CATCH 
		IF @@TRANCOUNT > 0
			ROLLBACK TRANSACTION
	END CATCH 
END
GO
/****** Object:  StoredProcedure [dbo].[SP_ViewCustVisit]    Script Date: 2018/06/09 13:00:51 ******/
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
	@BookingID nchar(10) = null
AS
BEGIN

	SET NOCOUNT ON;
	SELECT * 
	FROM CUST_VISIT c 
	WHERE c.CustomerID=@CustomerID AND c.BookingID=@BookingID
END
GO
USE [master]
GO
ALTER DATABASE [CHEVEUX] SET  READ_WRITE 
GO
