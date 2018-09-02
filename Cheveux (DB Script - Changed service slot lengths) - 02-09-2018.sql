USE [master]
GO
/****** Object:  Database [CHEVEUX]    Script Date: 02 Sep 2018 10:14:42 AM ******/
CREATE DATABASE [CHEVEUX]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'CHEVEUX', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.MSSQLSERVER\MSSQL\DATA\CHEVEUX.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'CHEVEUX_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.MSSQLSERVER\MSSQL\DATA\CHEVEUX_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO
ALTER DATABASE [CHEVEUX] SET COMPATIBILITY_LEVEL = 140
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
ALTER DATABASE [CHEVEUX] SET QUERY_STORE = OFF
GO
USE [CHEVEUX]
GO
ALTER DATABASE SCOPED CONFIGURATION SET IDENTITY_CACHE = ON;
GO
ALTER DATABASE SCOPED CONFIGURATION SET LEGACY_CARDINALITY_ESTIMATION = OFF;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET LEGACY_CARDINALITY_ESTIMATION = PRIMARY;
GO
ALTER DATABASE SCOPED CONFIGURATION SET MAXDOP = 0;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET MAXDOP = PRIMARY;
GO
ALTER DATABASE SCOPED CONFIGURATION SET PARAMETER_SNIFFING = ON;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET PARAMETER_SNIFFING = PRIMARY;
GO
ALTER DATABASE SCOPED CONFIGURATION SET QUERY_OPTIMIZER_HOTFIXES = OFF;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET QUERY_OPTIMIZER_HOTFIXES = PRIMARY;
GO
USE [CHEVEUX]
GO
/****** Object:  Table [dbo].[ACCESSORY]    Script Date: 02 Sep 2018 10:14:43 AM ******/
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
/****** Object:  Table [dbo].[BOOKING]    Script Date: 02 Sep 2018 10:14:44 AM ******/
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
/****** Object:  Table [dbo].[BookingService]    Script Date: 02 Sep 2018 10:14:44 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BookingService](
	[BookingID] [nchar](10) NULL,
	[ServiceID] [nchar](10) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[BRAID_SERVICE]    Script Date: 02 Sep 2018 10:14:45 AM ******/
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
/****** Object:  Table [dbo].[BRAND]    Script Date: 02 Sep 2018 10:14:45 AM ******/
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
/****** Object:  Table [dbo].[BUSINESS]    Script Date: 02 Sep 2018 10:14:45 AM ******/
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
/****** Object:  Table [dbo].[CUST_VISIT]    Script Date: 02 Sep 2018 10:14:45 AM ******/
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
/****** Object:  Table [dbo].[EMPLOYEE]    Script Date: 02 Sep 2018 10:14:45 AM ******/
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
/****** Object:  Table [dbo].[Home_Page]    Script Date: 02 Sep 2018 10:14:45 AM ******/
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
/****** Object:  Table [dbo].[LENGTH]    Script Date: 02 Sep 2018 10:14:45 AM ******/
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
/****** Object:  Table [dbo].[Order]    Script Date: 02 Sep 2018 10:14:46 AM ******/
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
/****** Object:  Table [dbo].[Order_DTL]    Script Date: 02 Sep 2018 10:14:46 AM ******/
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
/****** Object:  Table [dbo].[PRODUCT]    Script Date: 02 Sep 2018 10:14:46 AM ******/
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
/****** Object:  Table [dbo].[REVIEW]    Script Date: 02 Sep 2018 10:14:46 AM ******/
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
/****** Object:  Table [dbo].[SALE]    Script Date: 02 Sep 2018 10:14:46 AM ******/
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
/****** Object:  Table [dbo].[SALES_DTL]    Script Date: 02 Sep 2018 10:14:46 AM ******/
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
/****** Object:  Table [dbo].[SERVICE]    Script Date: 02 Sep 2018 10:14:46 AM ******/
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
/****** Object:  Table [dbo].[STYLE]    Script Date: 02 Sep 2018 10:14:47 AM ******/
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
/****** Object:  Table [dbo].[STYLIST_SERVICE]    Script Date: 02 Sep 2018 10:14:47 AM ******/
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
/****** Object:  Table [dbo].[Supplier]    Script Date: 02 Sep 2018 10:14:47 AM ******/
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
/****** Object:  Table [dbo].[TIMESLOT]    Script Date: 02 Sep 2018 10:14:47 AM ******/
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
/****** Object:  Table [dbo].[TREATMENT]    Script Date: 02 Sep 2018 10:14:47 AM ******/
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
/****** Object:  Table [dbo].[USER]    Script Date: 02 Sep 2018 10:14:47 AM ******/
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
/****** Object:  Table [dbo].[WIDTH]    Script Date: 02 Sep 2018 10:14:47 AM ******/
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
/****** Object:  StoredProcedure [dbo].[SP_AboutStylist]    Script Date: 02 Sep 2018 10:14:48 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		S.Maqabangqa
-- Description: Gives customer intro about the stylists that work for the salon. 
-- =============================================
CREATE PROCEDURE [dbo].[SP_AboutStylist]
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
/****** Object:  StoredProcedure [dbo].[SP_AccessorySearchByTerm]    Script Date: 02 Sep 2018 10:14:48 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create PROCEDURE [dbo].[SP_AccessorySearchByTerm] 
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
/****** Object:  StoredProcedure [dbo].[SP_AddAccessory]    Script Date: 02 Sep 2018 10:14:48 AM ******/
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
	@productType char(1)
	
AS
BEGIN
	BEGIN TRY
		BEGIN TRANSACTION

     INSERT ACCESSORY(AccessoryID, Colour, Qty, BrandID)
	 VALUES(@accessoryID, @colour, @qty, @BrandID)
	
	

	COMMIT TRANSACTION
	END TRY
	BEGIN CATCH
		IF @@TRANCOUNT > 0
			ROLLBACK TRANSACTION
	END CATCH

	BEGIN TRY
		BEGIN TRANSACTION

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
/****** Object:  StoredProcedure [dbo].[SP_AddBooking]    Script Date: 02 Sep 2018 10:14:48 AM ******/
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
	@Comment varchar(max)

AS
BEGIN 
	BEGIN TRY
		BEGIN TRANSACTION
			INSERT INTO BOOKING(BookingID, SlotNo, CustomerID, StylistID, [Date], Arrived, Available, Comment, NotificationReminder, primaryBookingID)
			VALUES(@BookingID, @Slot, @CustomerID, @StylistID, @Date, 'N', 'N', @Comment, 0, @primaryBookingID)
		COMMIT TRANSACTION
	END TRY
	BEGIN CATCH
		IF @@TRANCOUNT > 0
			ROLLBACK TRANSACTION
	END CATCH

END
GO
/****** Object:  StoredProcedure [dbo].[SP_AddEmployee]    Script Date: 02 Sep 2018 10:14:48 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		S.Maqabangqa
-- =============================================
CREATE PROCEDURE [dbo].[SP_AddEmployee]
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
/****** Object:  StoredProcedure [dbo].[SP_AddNewUser]    Script Date: 02 Sep 2018 10:14:48 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SP_AddNewUser] 
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
/****** Object:  StoredProcedure [dbo].[SP_AddPaymentTypeToSalesRecord]    Script Date: 02 Sep 2018 10:14:48 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Description:	adds payment type to existing sales record
-- =============================================
CREATE PROCEDURE [dbo].[SP_AddPaymentTypeToSalesRecord]
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
/****** Object:  StoredProcedure [dbo].[SP_AddProduct]    Script Date: 02 Sep 2018 10:14:48 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- Description:	Inserting of a product
CREATE PROCEDURE [dbo].[SP_AddProduct]
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
/****** Object:  StoredProcedure [dbo].[SP_AddService]    Script Date: 02 Sep 2018 10:14:48 AM ******/
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
/****** Object:  StoredProcedure [dbo].[SP_AddToBookingService]    Script Date: 02 Sep 2018 10:14:48 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[SP_AddToBookingService]
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
/****** Object:  StoredProcedure [dbo].[SP_AddTreatment]    Script Date: 02 Sep 2018 10:14:48 AM ******/
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
	@active char(1)

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

	BEGIN
	BEGIN TRY
		BEGIN TRANSACTION

     INSERT PRODUCT(ProductID,Name,ProductDescription, Price,[ProductType(T/A/S)], Active)
	 VALUES(@treatmentID, @Name,@ProductDescription, @Price,@productType, 'Y' )
	
	COMMIT TRANSACTION
	END TRY
	BEGIN CATCH
		IF @@TRANCOUNT > 0
			ROLLBACK TRANSACTION
	END CATCH

END
END 
GO
/****** Object:  StoredProcedure [dbo].[SP_AddUserGoogleAuth]    Script Date: 02 Sep 2018 10:14:48 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SP_AddUserGoogleAuth] 
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
/****** Object:  StoredProcedure [dbo].[SP_AllStylistPastBksForDate]    Script Date: 02 Sep 2018 10:14:48 AM ******/
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
		SELECT BookingID,b.primaryBookingID AS [PrimaryID],B.StylistID,B.CustomerID,
			
		   (SELECT (u.FirstName + ' ' + u.LastName)as[StylistName]
		   FROM [USER] u
		   WHERE u.UserID = B.StylistID)AS[StylistName],

		   (SELECT (u.FirstName+' '+u.LastName)as[CustomerName]
		   FROM [USER] u
		   WHERE u.UserID=B.CustomerID)AS[FullName],

		   B.[Date],TS.StartTime,TS.EndTime,B.Arrived

	From   BOOKING B, TIMESLOT TS, [User] U
	Where  B.SlotNo = TS.SlotNo 
	AND    B.StylistID = U.UserID 
	AND    B.Arrived = 'Y' 
	AND    B.[Date] = @day
	AND    B.[Date] !> CAST(GETDATE() AS DATE)
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
/****** Object:  StoredProcedure [dbo].[SP_AllStylistsPastBksDR]    Script Date: 02 Sep 2018 10:14:48 AM ******/
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
		SELECT BookingID,b.primaryBookingID AS [PrimaryID],B.StylistID,B.CustomerID,
			
		   (SELECT (u.FirstName + ' ' + u.LastName)as[StylistName]
		   FROM [USER] u
		   WHERE u.UserID = B.StylistID)AS[StylistName],

		   (SELECT (u.FirstName+' '+u.LastName)as[CustomerName]
		   FROM [USER] u
		   WHERE u.UserID=B.CustomerID)AS[FullName],

		   B.[Date],TS.StartTime,TS.EndTime,B.Arrived

	From   BOOKING B, TIMESLOT TS, [User] U
	Where  B.SlotNo = TS.SlotNo 
	AND    B.StylistID = U.UserID 
	AND    B.Arrived = 'Y' 
	AND	  (B.[Date] BETWEEN @startDate AND @endDate)
	AND    B.[Date] !> CAST(GETDATE() AS DATE)
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
/****** Object:  StoredProcedure [dbo].[SP_AllStylistsPastBookings]    Script Date: 02 Sep 2018 10:14:48 AM ******/
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
	SELECT BookingID,B.StylistID,B.CustomerID,
			(SELECT (u.FirstName + ' ' + u.LastName)as[stylist]
		   FROM [USER] u 
		   WHERE u.UserID=B.StylistID)AS[StylistName],

		   (SELECT (u.FirstName+' '+u.LastName)as[CustomerName]
		   FROM [USER] u
		   WHERE u.UserID=B.CustomerID)AS[FullName],

		   B.[Date],TS.StartTime,P.ProductID,P.[Name],P.ProductDescription,B.Arrived,P.Price

	From   BOOKING B, TIMESLOT TS, [User] U, PRODUCT P, EMPLOYEE e
	Where  B.SlotNo = TS.SlotNo 
	AND    B.StylistID = U.UserID
	AND	   B.StylistID = e.EmployeeID 	
	AND	   B.Arrived = 'Y'
	AND	   B.[Date] !> CAST(GETDATE() AS DATE)
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
/****** Object:  StoredProcedure [dbo].[SP_AllStylistsUpcomingBksDR]    Script Date: 02 Sep 2018 10:14:48 AM ******/
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
		SELECT BookingID,b.primaryBookingID AS [PrimaryID],B.StylistID,B.CustomerID,
			
		   (SELECT (u.FirstName + ' ' + u.LastName)as[StylistName]
		   FROM [USER] u
		   WHERE u.UserID = B.StylistID)AS[StylistName],

		   (SELECT (u.FirstName+' '+u.LastName)as[CustomerName]
		   FROM [USER] u
		   WHERE u.UserID=B.CustomerID)AS[FullName],

		   B.[Date],TS.StartTime,TS.EndTime,B.Arrived

	From   BOOKING B, TIMESLOT TS, [User] U
	Where  B.SlotNo = TS.SlotNo 
	AND    B.StylistID = U.UserID 
	AND    B.Arrived = 'N' 
	AND	  (B.[Date] BETWEEN @startDate AND @endDate)
	AND   B.[Date] !< CAST(GETDATE() AS DATE)
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
/****** Object:  StoredProcedure [dbo].[SP_AllStylistsUpcomingBksForDate]    Script Date: 02 Sep 2018 10:14:48 AM ******/
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
		SELECT BookingID,b.primaryBookingID AS [PrimaryID],B.StylistID,B.CustomerID,
			
		   (SELECT (u.FirstName + ' ' + u.LastName)as[StylistName]
		   FROM [USER] u
		   WHERE u.UserID = B.StylistID)AS[StylistName],

		   (SELECT (u.FirstName+' '+u.LastName)as[CustomerName]
		   FROM [USER] u
		   WHERE u.UserID=B.CustomerID)AS[FullName],

		   B.[Date],TS.StartTime,TS.EndTime,B.Arrived

	From   BOOKING B, TIMESLOT TS, [User] U
	Where  B.SlotNo = TS.SlotNo 
	AND    B.StylistID = U.UserID 
	AND    B.Arrived = 'N' 
	AND    B.[Date] = @bookingDate
	AND    B.[Date] !< CAST(GETDATE() AS DATE)
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
/****** Object:  StoredProcedure [dbo].[SP_AllStylistsUpcomingBookings]    Script Date: 02 Sep 2018 10:14:48 AM ******/
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
	SELECT BookingID,B.StylistID,B.CustomerID,
			
		   (SELECT (u.FirstName + ' ' + u.LastName)as[stylist]
		   FROM [USER] u 
		   WHERE u.UserID=B.StylistID)AS[StylistName],

		   (SELECT (u.FirstName+' '+u.LastName)as[CustomerName]
		   FROM [USER] u
		   WHERE u.UserID=B.CustomerID)AS[FullName],

		   B.[Date],TS.StartTime,P.ProductID,P.[Name],P.ProductDescription,B.Arrived,P.Price

	From   BOOKING B, TIMESLOT TS, [User] U, PRODUCT P, EMPLOYEE e
	Where  B.SlotNo = TS.SlotNo 
	AND    B.StylistID = U.UserID
	AND	   B.StylistID = e.EmployeeID 	 
	AND    B.Arrived = 'N' 
	AND    B.[Date] !< CAST(GETDATE() AS DATE)
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
/****** Object:  StoredProcedure [dbo].[SP_BraidServiceSearchByTerm]    Script Date: 02 Sep 2018 10:14:48 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create PROCEDURE [dbo].[SP_BraidServiceSearchByTerm]
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
/****** Object:  StoredProcedure [dbo].[SP_CheckForAccountTypeEmail]    Script Date: 02 Sep 2018 10:14:48 AM ******/
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
/****** Object:  StoredProcedure [dbo].[SP_CheckForProduct]    Script Date: 02 Sep 2018 10:14:48 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Description:	check for a matching exisiting Product with given ID
-- =============================================
CREATE PROCEDURE [dbo].[SP_CheckForProduct] 
	@ProductID nchar(10)
AS
BEGIN
	SELECT ProductID 
	FROM PRODUCT
	WHERE ProductID = @ProductID
END
GO
/****** Object:  StoredProcedure [dbo].[SP_CheckForUserType]    Script Date: 02 Sep 2018 10:14:48 AM ******/
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
/****** Object:  StoredProcedure [dbo].[SP_CheckIn]    Script Date: 02 Sep 2018 10:14:48 AM ******/
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
/****** Object:  StoredProcedure [dbo].[SP_CreateCustVisit]    Script Date: 02 Sep 2018 10:14:48 AM ******/
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
/****** Object:  StoredProcedure [dbo].[SP_CreateProductSalesDTLRecord]    Script Date: 02 Sep 2018 10:14:48 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SP_CreateProductSalesDTLRecord]
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
/****** Object:  StoredProcedure [dbo].[SP_CreateRestCode]    Script Date: 02 Sep 2018 10:14:48 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Description:	Add reset code to user account withh matching email address or username
-- =============================================
CREATE PROCEDURE [dbo].[SP_CreateRestCode]
	-- Add the parameters for the stored procedure here
	@restCode nchar(28),
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
/****** Object:  StoredProcedure [dbo].[SP_CreateSalesDTLRecord]    Script Date: 02 Sep 2018 10:14:48 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Description:	Given a SaleID, ProductID & Qty is Creates a salesDTL record
-- =============================================
Create PROCEDURE [dbo].[SP_CreateSalesDTLRecord]
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
/****** Object:  StoredProcedure [dbo].[SP_CreateSalesRecord]    Script Date: 02 Sep 2018 10:14:48 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Description:	Given a bookingID Creates a sales record
-- =============================================
create PROCEDURE [dbo].[SP_CreateSalesRecord] 
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
/****** Object:  StoredProcedure [dbo].[SP_DeactivateUser]    Script Date: 02 Sep 2018 10:14:48 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Description:	Set the active colum of a user acount to false
-- =============================================
CREATE PROCEDURE [dbo].[SP_DeactivateUser] 
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
/****** Object:  StoredProcedure [dbo].[SP_DeleteBooking]    Script Date: 02 Sep 2018 10:14:48 AM ******/
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
/****** Object:  StoredProcedure [dbo].[SP_DeleteBookingService]    Script Date: 02 Sep 2018 10:14:48 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Description:	Removes Booking Records and Booking Service matchin the BookingID or primaryBookingID
-- =============================================
Create PROCEDURE [dbo].[SP_DeleteBookingService] 
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
/****** Object:  StoredProcedure [dbo].[SP_DeleteSecondaryBookings]    Script Date: 02 Sep 2018 10:14:48 AM ******/
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
/****** Object:  StoredProcedure [dbo].[SP_EditUser]    Script Date: 02 Sep 2018 10:14:48 AM ******/
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
/****** Object:  StoredProcedure [dbo].[SP_FeaturedProductsAndContact]    Script Date: 02 Sep 2018 10:14:48 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Description:	Get the features stylists and contact us info
-- =============================================
CREATE PROCEDURE [dbo].[SP_FeaturedProductsAndContact] 
AS
BEGIN
	SELECT [FeatureID], [ItemID], [ImageURL], [USER].FirstName, [USER].ContactNo, [USER].Email
	FROM [CHEVEUX].[dbo].[Home_Page], [USER]
	Where ItemID = [USER].UserID
END
GO
/****** Object:  StoredProcedure [dbo].[SP_FeaturedProductsAndHairStyles]    Script Date: 02 Sep 2018 10:14:48 AM ******/
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
/****** Object:  StoredProcedure [dbo].[SP_GetAccountForRestCode]    Script Date: 02 Sep 2018 10:14:48 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Description:	Returns acount details for the account matchingf the reset code
-- =============================================
CREATE PROCEDURE [dbo].[SP_GetAccountForRestCode] 
	@Code nchar(30)
AS
BEGIN
	SELECT UserName, UserID
	from [USER]
	where PassRestCode = @Code
END
GO
/****** Object:  StoredProcedure [dbo].[SP_GetAllAccessories]    Script Date: 02 Sep 2018 10:14:48 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Description:	gets all accessories in the database with details from the product Table
-- =============================================
CREATE PROCEDURE [dbo].[SP_GetAllAccessories]
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
/****** Object:  StoredProcedure [dbo].[SP_GetAllofBookingDTL]    Script Date: 02 Sep 2018 10:14:48 AM ******/
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
			B.[Date], TS.StartTime, TS.EndTime   
			     
	From 		BOOKING B, TIMESLOT TS, [User] U, CUST_VISIT cv

	Where 		cv.BookingID = @BookingID 
	AND 		cv.CustomerID=@CustomerID
	AND			B.BookingID=cv.BookingID
	AND			B.CustomerID=cv.CustomerID
	AND 		B.SlotNo = TS.SlotNo 
	AND 		B.StylistID = U.UserID 
END
GO
/****** Object:  StoredProcedure [dbo].[SP_GetAllProducts]    Script Date: 02 Sep 2018 10:14:48 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Description:	Gets All Products in the Product Table
-- =============================================
Create PROCEDURE [dbo].[SP_GetAllProducts] 
AS
BEGIN
	SELECT *
	FROM [CHEVEUX].[dbo].[PRODUCT]
	order by [ProductType(T/A/S)]
END
GO
/****** Object:  StoredProcedure [dbo].[SP_GetAllTreatments]    Script Date: 02 Sep 2018 10:14:48 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Description:	gets all treatments and thier details from the product, treatment and brand tables
-- =============================================
CREATE PROCEDURE [dbo].[SP_GetAllTreatments]
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
/****** Object:  StoredProcedure [dbo].[SP_GetBookedTimes]    Script Date: 02 Sep 2018 10:14:48 AM ******/
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
/****** Object:  StoredProcedure [dbo].[SP_GetBookingDetailsForCustVistRecord]    Script Date: 02 Sep 2018 10:14:48 AM ******/
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
/****** Object:  StoredProcedure [dbo].[SP_GetBookingServices]    Script Date: 02 Sep 2018 10:14:48 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Description:	Returns services for a specific booking
-- =============================================
CREATE PROCEDURE [dbo].[SP_GetBookingServices]
	@BookingID nchar(10)
AS
BEGIN
	SELECT bs.[BookingID], bs.[ServiceID], p.[Name], p.Price, p.ProductDescription
	FROM BookingService bs, PRODUCT p
	where BookingID = @BookingID
		AND bs.[ServiceID] = p.ProductID
END
GO
/****** Object:  StoredProcedure [dbo].[SP_GetBraidService]    Script Date: 02 Sep 2018 10:14:48 AM ******/
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
	SELECT s.Description AS styleDesc, w.Description AS widthDes, l.Description AS lengthDesc
	FROM SERVICE ser, PRODUCT p, STYLE s, WIDTH w, LENGTH l, BRAID_SERVICE bs
	WHERE @ServiceID = p.ProductID AND p.ProductID = ser.ServiceID AND [ProductType(T/A/S)] = 'S'
		AND ser.ServiceID = bs.ServiceID AND bs.StyleID = s.StyleID AND bs.WidthID = w.WidthID
		AND bs.LengthID = l.LengthID
END
GO
/****** Object:  StoredProcedure [dbo].[SP_GetBrandsForProductType]    Script Date: 02 Sep 2018 10:14:48 AM ******/
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
/****** Object:  StoredProcedure [dbo].[SP_getBusinessTable]    Script Date: 02 Sep 2018 10:14:48 AM ******/
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
/****** Object:  StoredProcedure [dbo].[SP_GetCurrentVATRate2]    Script Date: 02 Sep 2018 10:14:48 AM ******/
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
/****** Object:  StoredProcedure [dbo].[SP_GetCustomerPastBooking]    Script Date: 02 Sep 2018 10:14:48 AM ******/
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
/****** Object:  StoredProcedure [dbo].[SP_GetCustomerPastBookingDetail]    Script Date: 02 Sep 2018 10:14:48 AM ******/
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
	Select U.FirstName, B.[Date], TS.StartTime, BookingID, B.Arrived , B.StylistID, 
	b.CustomerID, (select FirstName + ' '+ LastName from [USER] where [USER].UserID = b.CustomerID) as CustFullName        
	From BOOKING B, TIMESLOT TS, [User] U
	Where BookingID = @bookingID 
	AND B.SlotNo = TS.SlotNo 
	AND B.StylistID = U.UserID
	AND (B.Arrived = 'Y' Or B.Arrived = 'N')
END
GO
/****** Object:  StoredProcedure [dbo].[SP_GetCustomerUpcomingBookingDetails]    Script Date: 02 Sep 2018 10:14:48 AM ******/
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
/****** Object:  StoredProcedure [dbo].[SP_GetCustomerUpcomingBookings]    Script Date: 02 Sep 2018 10:14:48 AM ******/
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
/****** Object:  StoredProcedure [dbo].[SP_GetEmpAgenda]    Script Date: 02 Sep 2018 10:14:48 AM ******/
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
/****** Object:  StoredProcedure [dbo].[SP_GetEmployeeType]    Script Date: 02 Sep 2018 10:14:48 AM ******/
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
/****** Object:  StoredProcedure [dbo].[SP_GetEmployeeTypes]    Script Date: 02 Sep 2018 10:14:48 AM ******/
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
/****** Object:  StoredProcedure [dbo].[SP_GetEmpNames]    Script Date: 02 Sep 2018 10:14:48 AM ******/
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
/****** Object:  StoredProcedure [dbo].[SP_getInvoiceDL]    Script Date: 02 Sep 2018 10:14:48 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SP_getInvoiceDL] 
	@BookingID nchar(10)
AS
BEGIN
	Select p.[Name], d.Qty, d.Price, p.ProductID, p.[ProductType(T/A/S)]
	From SALE s, SALES_DTL d, PRODUCT p
	Where s.SaleID = d.SaleID
	AND s.BookingID = @BookingID
	AND p.ProductID = d.ProductID
END
GO
/****** Object:  StoredProcedure [dbo].[SP_GetLengths]    Script Date: 02 Sep 2018 10:14:48 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

Create PROCEDURE [dbo].[SP_GetLengths]

AS
BEGIN
	SELECT * 
	FROM LENGTH
END
GO
/****** Object:  StoredProcedure [dbo].[SP_GetMultipleServicesTime]    Script Date: 02 Sep 2018 10:14:48 AM ******/
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
/****** Object:  StoredProcedure [dbo].[SP_GetOGBkngNoti]    Script Date: 02 Sep 2018 10:14:48 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Description: Get Out Going Booking Notifications: gets the details nessasry to send booking notifications for bookint taking place tommorow
-- =============================================
CREATE PROCEDURE [dbo].[SP_GetOGBkngNoti]
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
/****** Object:  StoredProcedure [dbo].[SP_GetPasHash]    Script Date: 02 Sep 2018 10:14:48 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Description:	Returnst the password for the user account matching the username or emailaddress
-- =============================================
CREATE PROCEDURE [dbo].[SP_GetPasHash]
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
/****** Object:  StoredProcedure [dbo].[SP_GetProductTypes]    Script Date: 02 Sep 2018 10:14:48 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Description:	Returns all Product types in Product table of database
-- =============================================
CREATE PROCEDURE [dbo].[SP_GetProductTypes]
AS
BEGIN
	Select DISTINCT [ProductType(T/A/S)]
	From [CHEVEUX].[dbo].[PRODUCT]
END
GO
/****** Object:  StoredProcedure [dbo].[SP_GetSale]    Script Date: 02 Sep 2018 10:14:48 AM ******/
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
/****** Object:  StoredProcedure [dbo].[SP_GetSalePaymentType]    Script Date: 02 Sep 2018 10:14:48 AM ******/
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
/****** Object:  StoredProcedure [dbo].[SP_GetService]    Script Date: 02 Sep 2018 10:14:48 AM ******/
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
	SELECT [Name], ProductDescription, Price, NoOfSlots
	FROM SERVICE, PRODUCT
	WHERE @ServiceID = ProductID AND ProductID = ServiceID AND [ProductType(T/A/S)] = 'S'
END
GO
/****** Object:  StoredProcedure [dbo].[SP_GetServices]    Script Date: 02 Sep 2018 10:14:48 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[SP_GetServices]
AS
BEGIN
	SELECT Name, ProductID, [Type(A/N/B)] AS ServiceType, Price
	FROM PRODUCT, SERVICE
	WHERE ProductID = ServiceID AND [ProductType(T/A/S)] = 'S';
END
GO
/****** Object:  StoredProcedure [dbo].[SP_GetSlotLength]    Script Date: 02 Sep 2018 10:14:48 AM ******/
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
/****** Object:  StoredProcedure [dbo].[SP_GetStyles]    Script Date: 02 Sep 2018 10:14:48 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[SP_GetStyles]

AS
BEGIN
	SELECT *
	FROM STYLE
END
GO
/****** Object:  StoredProcedure [dbo].[SP_GetStylist]    Script Date: 02 Sep 2018 10:14:48 AM ******/
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
/****** Object:  StoredProcedure [dbo].[SP_GetTimeSlots]    Script Date: 02 Sep 2018 10:14:48 AM ******/
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
/****** Object:  StoredProcedure [dbo].[SP_GetTodaysBookings]    Script Date: 02 Sep 2018 10:14:48 AM ******/
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
/****** Object:  StoredProcedure [dbo].[SP_GetTodaysTotalSales]    Script Date: 02 Sep 2018 10:14:48 AM ******/
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
/****** Object:  StoredProcedure [dbo].[SP_GetTotalCusts]    Script Date: 02 Sep 2018 10:14:48 AM ******/
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
END
GO
/****** Object:  StoredProcedure [dbo].[SP_GetUserDetails]    Script Date: 02 Sep 2018 10:14:48 AM ******/
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
	Where [User].[UserID] = @ID
END
GO
/****** Object:  StoredProcedure [dbo].[SP_GetWidths]    Script Date: 02 Sep 2018 10:14:48 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[SP_GetWidths]

AS
BEGIN
	SELECT *
	FROM WIDTH
END
GO
/****** Object:  StoredProcedure [dbo].[SP_LogInEmail]    Script Date: 02 Sep 2018 10:14:48 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Description:	using the email of password returns  UserID, UserType, [FirstName]
-- =============================================
CREATE PROCEDURE [dbo].[SP_LogInEmail] 
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
/****** Object:  StoredProcedure [dbo].[SP_Products]    Script Date: 02 Sep 2018 10:14:48 AM ******/
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
/****** Object:  StoredProcedure [dbo].[SP_ProductSearchByTerm]    Script Date: 02 Sep 2018 10:14:48 AM ******/
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
/****** Object:  StoredProcedure [dbo].[SP_RemoveProductSalesDTLRecord]    Script Date: 02 Sep 2018 10:14:48 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SP_RemoveProductSalesDTLRecord]
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
/****** Object:  StoredProcedure [dbo].[SP_SaleOfHairstylist]    Script Date: 02 Sep 2018 10:14:48 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Description:	Sales for a hairstylist
-- =============================================
CREATE PROCEDURE [dbo].[SP_SaleOfHairstylist] 
	@StylistID nchar(30),
	@startDate datetime,
	@endDate datetime
AS

BEGIN
Select s.SaleID,s.[Date],(u.FirstName + ' ' + u.LastName) AS[FullName], s.CustomerID, s.BookingID
From SALE s, [USER] u, SALES_DTL, BOOKING b
Where s.BookingID = b.BookingID
And b.StylistID = @StylistID
and s.[Date] BETWEEN @startDate   AND @endDate
And u.UserID = b.CustomerID
And s.CustomerID = b.CustomerID
END
GO
/****** Object:  StoredProcedure [dbo].[SP_SearchBookings]    Script Date: 02 Sep 2018 10:14:48 AM ******/
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
/****** Object:  StoredProcedure [dbo].[SP_SearchByTermAndType]    Script Date: 02 Sep 2018 10:14:48 AM ******/
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
/****** Object:  StoredProcedure [dbo].[SP_SearchForUser]    Script Date: 02 Sep 2018 10:14:48 AM ******/
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
/****** Object:  StoredProcedure [dbo].[SP_SearchStylistsBySearchTerm]    Script Date: 02 Sep 2018 10:14:48 AM ******/
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
/****** Object:  StoredProcedure [dbo].[SP_SelectAccessory]    Script Date: 02 Sep 2018 10:14:48 AM ******/
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
	SELECT [ProductID]
      ,[PRODUCT].[Name] AS [ProductName]
      ,[ProductDescription]
      ,[Price]
      ,[ProductType(T/A/S)]
	  ,BRAND.[BrandID]
	  ,BRAND.[Name] AS [BrandName]
	
	 FROM [CHEVEUX].[dbo].[PRODUCT], ACCESSORY, BRAND, Supplier s
	WHERE ProductID = AccessoryID AND BRAND.BrandID = ACCESSORY.BrandID AND s.SupplierID = ACCESSORY.SupplierID And ProductID= @productID 
	order by [ProductType(T/A/S)]
END
GO
/****** Object:  StoredProcedure [dbo].[SP_SelectTreatment]    Script Date: 02 Sep 2018 10:14:48 AM ******/
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
      ,PRODUCT.[Name] AS [ProductName]
      ,[ProductDescription]
      ,[Price]
      ,[ProductType(T/A/S)]
      ,[Active]
	  ,[TreatmentType]
      ,BRAND.[BrandID]
	  ,BRAND.[Name]	AS [BrandName]
	  
	  
	FROM [CHEVEUX].[dbo].[PRODUCT], TREATMENT, BRAND, Supplier s
	WHERE ProductID = TreatmentID AND BRAND.BrandID = TREATMENT.BrandID AND s.SupplierID = TREATMENT.SupplierID AND ProductID = @productID
	order by [ProductType(T/A/S)]
END
GO
/****** Object:  StoredProcedure [dbo].[SP_ServiceSearchByTerm]    Script Date: 02 Sep 2018 10:14:48 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create PROCEDURE [dbo].[SP_ServiceSearchByTerm]
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
/****** Object:  StoredProcedure [dbo].[SP_StylistPastBksForDate]    Script Date: 02 Sep 2018 10:14:48 AM ******/
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

	From   BOOKING B, TIMESLOT TS, [User] U
	Where  b.StylistID = @stylistID
	AND    B.SlotNo = TS.SlotNo 
	AND    B.StylistID = U.UserID 
	AND	   B.Arrived = 'Y'
	AND    B.[Date] = @day

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
/****** Object:  StoredProcedure [dbo].[SP_StylistPastBookings]    Script Date: 02 Sep 2018 10:14:48 AM ******/
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

	From   BOOKING B, TIMESLOT TS, [User] U
	Where  b.StylistID = @stylistID
	AND    B.SlotNo = TS.SlotNo 
	AND    B.StylistID = U.UserID 
	AND	   B.Arrived = 'Y'
	AND	   B.[Date] !> CAST(GETDATE() AS DATE)
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
/****** Object:  StoredProcedure [dbo].[SP_StylistPastBookingsDateRange]    Script Date: 02 Sep 2018 10:14:48 AM ******/
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

	From   BOOKING B, TIMESLOT TS, [User] U
	Where  b.StylistID = @stylistID
	AND    B.SlotNo = TS.SlotNo 
	AND    B.StylistID = U.UserID 
	AND	   B.Arrived = 'Y'
	AND	  (B.[Date] BETWEEN @startDate AND @endDate)

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
/****** Object:  StoredProcedure [dbo].[SP_StylistUpcomingBkForDate]    Script Date: 02 Sep 2018 10:14:48 AM ******/
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

	From   BOOKING B, TIMESLOT TS, [User] U
	Where  b.StylistID = @stylistID
	AND    B.SlotNo = TS.SlotNo 
	AND    B.StylistID = U.UserID 
	AND	   B.Arrived = 'N'
	AND    B.[Date] = @day

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
/****** Object:  StoredProcedure [dbo].[SP_StylistUpcomingBookings]    Script Date: 02 Sep 2018 10:14:48 AM ******/
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

	From   BOOKING B, TIMESLOT TS, [User] U
	Where  b.StylistID = @stylistID
	AND    B.SlotNo = TS.SlotNo 
	AND    B.StylistID = U.UserID 
	AND    B.Arrived = 'N'
	AND    B.[Date] !< CAST(GETDATE() AS DATE)
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
/****** Object:  StoredProcedure [dbo].[SP_StylistUpcomingBookingsDR]    Script Date: 02 Sep 2018 10:14:48 AM ******/
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
	AND    B.StylistID = U.UserID 
	AND    B.Arrived = 'N'
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
/****** Object:  StoredProcedure [dbo].[SP_TotalBookings]    Script Date: 02 Sep 2018 10:14:48 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Description:	gets the total number of all time bookings
-- =============================================
create PROCEDURE [dbo].[SP_TotalBookings]
AS
BEGIN
	SELECT count(*)
	FROM BOOKING
	where BookingID = primaryBookingID
END
GO
/****** Object:  StoredProcedure [dbo].[SP_TotalUpcomingBookings]    Script Date: 02 Sep 2018 10:14:48 AM ******/
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
/****** Object:  StoredProcedure [dbo].[SP_TREATMENT]    Script Date: 02 Sep 2018 10:14:48 AM ******/
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
/****** Object:  StoredProcedure [dbo].[SP_TreatmentSearchByTerm]    Script Date: 02 Sep 2018 10:14:48 AM ******/
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
/****** Object:  StoredProcedure [dbo].[SP_UpdateAddress]    Script Date: 02 Sep 2018 10:14:48 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Description:	updates the addres in the Bussiness Table
-- =============================================
create PROCEDURE [dbo].[SP_UpdateAddress]
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
/****** Object:  StoredProcedure [dbo].[SP_UpdateBooking]    Script Date: 02 Sep 2018 10:14:48 AM ******/
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
			UPDATE BOOKING
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
/****** Object:  StoredProcedure [dbo].[SP_UpdateCustVisit]    Script Date: 02 Sep 2018 10:14:48 AM ******/
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
/****** Object:  StoredProcedure [dbo].[SP_UpdateEmployee]    Script Date: 02 Sep 2018 10:14:48 AM ******/
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
END
GO
/****** Object:  StoredProcedure [dbo].[SP_UpdateNotiStatus]    Script Date: 02 Sep 2018 10:14:48 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Description:	Sets the Notification Reminder colum of the booking table for a spacific booking
-- =============================================
create PROCEDURE [dbo].[SP_UpdateNotiStatus]
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
/****** Object:  StoredProcedure [dbo].[SP_UpdatePhoneNumber]    Script Date: 02 Sep 2018 10:14:48 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Description:	updates the phone number in the Bussiness Table
-- =============================================
create PROCEDURE [dbo].[SP_UpdatePhoneNumber]
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
/****** Object:  StoredProcedure [dbo].[SP_UpdateProducts]    Script Date: 02 Sep 2018 10:14:48 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Description:	Update/editing of products
-- =============================================
CREATE PROCEDURE [dbo].[SP_UpdateProducts]
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
/****** Object:  StoredProcedure [dbo].[SP_UpdateProductSalesDTLRecordQty]    Script Date: 02 Sep 2018 10:14:48 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SP_UpdateProductSalesDTLRecordQty]
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
/****** Object:  StoredProcedure [dbo].[SP_UpdatePublicHolidayHours]    Script Date: 02 Sep 2018 10:14:48 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Description:	updates the public holiday hours in the Bussiness Table
-- =============================================
create PROCEDURE [dbo].[SP_UpdatePublicHolidayHours]
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
/****** Object:  StoredProcedure [dbo].[SP_UpdateService]    Script Date: 02 Sep 2018 10:14:48 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[SP_UpdateService] 
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
/****** Object:  StoredProcedure [dbo].[SP_UpdateStylistBio]    Script Date: 02 Sep 2018 10:14:48 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Description:	updates the Weekend hours in the Bussiness Table
-- =============================================
Create PROCEDURE [dbo].[SP_UpdateStylistBio]
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
/****** Object:  StoredProcedure [dbo].[SP_UpdateUserAccountPassword]    Script Date: 02 Sep 2018 10:14:48 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Description:	Changes the password for the account matching the userID
-- =============================================
CREATE PROCEDURE [dbo].[SP_UpdateUserAccountPassword]  
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
/****** Object:  StoredProcedure [dbo].[SP_UpdateVateRate]    Script Date: 02 Sep 2018 10:14:48 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Description:	updates the Vate Rate in the Bussiness Table
-- =============================================
Create PROCEDURE [dbo].[SP_UpdateVateRate] 
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
/****** Object:  StoredProcedure [dbo].[SP_UpdateVateRegNo]    Script Date: 02 Sep 2018 10:14:48 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Description:	updates the Vate reg no in the Bussiness Table
-- =============================================
create PROCEDURE [dbo].[SP_UpdateVateRegNo] 
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
/****** Object:  StoredProcedure [dbo].[SP_UpdateWeekdayHours]    Script Date: 02 Sep 2018 10:14:48 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Description:	updates the Weekday hours in the Bussiness Table
-- =============================================
create PROCEDURE [dbo].[SP_UpdateWeekdayHours]
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
/****** Object:  StoredProcedure [dbo].[SP_UpdateWeekendHours]    Script Date: 02 Sep 2018 10:14:48 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Description:	updates the Weekend hours in the Bussiness Table
-- =============================================
create PROCEDURE [dbo].[SP_UpdateWeekendHours]
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
/****** Object:  StoredProcedure [dbo].[SP_UserList]    Script Date: 02 Sep 2018 10:14:48 AM ******/
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
/****** Object:  StoredProcedure [dbo].[SP_ViewAllEmployee]    Script Date: 02 Sep 2018 10:14:48 AM ******/
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
/****** Object:  StoredProcedure [dbo].[SP_ViewCustVisit]    Script Date: 02 Sep 2018 10:14:48 AM ******/
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
/****** Object:  StoredProcedure [dbo].[SP_ViewEmployee]    Script Date: 02 Sep 2018 10:14:48 AM ******/
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
/****** Object:  StoredProcedure [dbo].[SP_ViewStylistSpecialisation]    Script Date: 02 Sep 2018 10:14:48 AM ******/
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
