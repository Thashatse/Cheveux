USE [master]
GO
/****** Object:  Database [CHEVEUX]    Script Date: 2018/06/09 10:41:34 ******/
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
/****** Object:  Table [dbo].[ACCESSORY]    Script Date: 2018/06/09 10:41:34 ******/
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
/****** Object:  Table [dbo].[BOOKING]    Script Date: 2018/06/09 10:41:34 ******/
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
/****** Object:  Table [dbo].[BRAID_SERVICE]    Script Date: 2018/06/09 10:41:34 ******/
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
/****** Object:  Table [dbo].[BRAND]    Script Date: 2018/06/09 10:41:34 ******/
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
/****** Object:  Table [dbo].[BUSINESS]    Script Date: 2018/06/09 10:41:34 ******/
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
/****** Object:  Table [dbo].[CUST_VISIT]    Script Date: 2018/06/09 10:41:34 ******/
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
/****** Object:  Table [dbo].[EMPLOYEE]    Script Date: 2018/06/09 10:41:34 ******/
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
/****** Object:  Table [dbo].[LENGTH]    Script Date: 2018/06/09 10:41:34 ******/
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
/****** Object:  Table [dbo].[PRODUCT]    Script Date: 2018/06/09 10:41:34 ******/
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
/****** Object:  Table [dbo].[REVIEW]    Script Date: 2018/06/09 10:41:34 ******/
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
/****** Object:  Table [dbo].[SALE]    Script Date: 2018/06/09 10:41:34 ******/
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
/****** Object:  Table [dbo].[SALES_DTL]    Script Date: 2018/06/09 10:41:34 ******/
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
/****** Object:  Table [dbo].[SERVICE]    Script Date: 2018/06/09 10:41:34 ******/
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
/****** Object:  Table [dbo].[STYLE]    Script Date: 2018/06/09 10:41:34 ******/
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
/****** Object:  Table [dbo].[STYLIST_SERVICE]    Script Date: 2018/06/09 10:41:34 ******/
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
/****** Object:  Table [dbo].[TIMESLOT]    Script Date: 2018/06/09 10:41:34 ******/
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
/****** Object:  Table [dbo].[TREATMENT]    Script Date: 2018/06/09 10:41:34 ******/
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
/****** Object:  Table [dbo].[USER]    Script Date: 2018/06/09 10:41:34 ******/
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
/****** Object:  Table [dbo].[WIDTH]    Script Date: 2018/06/09 10:41:34 ******/
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
/****** Object:  StoredProcedure [dbo].[SP_AddBooking]    Script Date: 2018/06/09 10:41:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[SP_AddBooking]
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
/****** Object:  StoredProcedure [dbo].[SP_AddUserGoogleAuth]    Script Date: 2018/06/09 10:41:34 ******/
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
	@UI nvarchar(300)
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
/****** Object:  StoredProcedure [dbo].[SP_CheckForUser]    Script Date: 2018/06/09 10:41:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SP_CheckForUser] 
	@ID nchar(30)
AS
BEGIN
	SELECT UserType
	From [CHEVEUX].[dbo].[User]
	Where UserID = @ID
END
GO
/****** Object:  StoredProcedure [dbo].[SP_CheckForUserType]    Script Date: 2018/06/09 10:41:34 ******/
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
/****** Object:  StoredProcedure [dbo].[SP_CheckIn]    Script Date: 2018/06/09 10:41:34 ******/
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
/****** Object:  StoredProcedure [dbo].[SP_CreateCustVisit]    Script Date: 2018/06/09 10:41:34 ******/
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
	@BookingID nchar = null,
	@Description varchar(50) = null
AS
BEGIN
	BEGIN TRY
		BEGIN TRANSACTION
			
			INSERT INTO CUST_VISIT (CustomerID,[Date],BookingID,[Description])
			VALUES (@CustomerID,
					(SELECT b.[Date] from BOOKING b where b.CustomerID=@CustomerID and b.BookingID=@BookingID),
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
/****** Object:  StoredProcedure [dbo].[SP_DeleteBooking]    Script Date: 2018/06/09 10:41:34 ******/
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
/****** Object:  StoredProcedure [dbo].[SP_EditUser]    Script Date: 2018/06/09 10:41:34 ******/
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
/****** Object:  StoredProcedure [dbo].[SP_GetAllofBookingDTL]    Script Date: 2018/06/09 10:41:34 ******/
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
/****** Object:  StoredProcedure [dbo].[SP_GetBookingServiceDTL]    Script Date: 2018/06/09 10:41:34 ******/
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
/****** Object:  StoredProcedure [dbo].[SP_GetCurrentVATRate2]    Script Date: 2018/06/09 10:41:34 ******/
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
/****** Object:  StoredProcedure [dbo].[SP_GetCustomerPastBooking]    Script Date: 2018/06/09 10:41:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SP_GetCustomerPastBooking] 
	@CustID nchar(30)
AS
BEGIN
	Select P.[Name], P.ProductDescription, P.Price,  U.FirstName, B.[Date], TS.StartTime, BookingID, B.Arrived       
	From BOOKING B, TIMESLOT TS, [User] U, PRODUCT P
	Where CustomerID = @CustID 
	AND B.SlotNo = TS.SlotNo 
	AND B.StylistID = U.UserID 
	AND B.ServiceID = P.ProductID 
	AND B.Arrived = 'Y' Or B.Arrived = 'N'
END
GO
/****** Object:  StoredProcedure [dbo].[SP_GetCustomerPastBookingDetail]    Script Date: 2018/06/09 10:41:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create PROCEDURE [dbo].[SP_GetCustomerPastBookingDetail] 
	@BookingID nchar(10)
AS
BEGIN
	Select P.[Name], P.ProductDescription, P.Price,  U.FirstName, B.[Date], TS.StartTime, BookingID, B.Arrived       
	From BOOKING B, TIMESLOT TS, [User] U, PRODUCT P
	Where BookingID = @BookingID 
	AND B.SlotNo = TS.SlotNo 
	AND B.StylistID = U.UserID 
	AND B.ServiceID = P.ProductID 
	AND B.Arrived = 'Y' Or B.Arrived = 'N'
END
GO
/****** Object:  StoredProcedure [dbo].[SP_GetCustomerUpcomingBookingDetails]    Script Date: 2018/06/09 10:41:34 ******/
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
/****** Object:  StoredProcedure [dbo].[SP_GetCustomerUpcomingBookings]    Script Date: 2018/06/09 10:41:34 ******/
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
/****** Object:  StoredProcedure [dbo].[SP_GetEmpAgenda]    Script Date: 2018/06/09 10:41:34 ******/
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
	@EmployeeID nchar(30)
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

			p.[Name]AS[ServiceName], b.Arrived

	FROM BOOKING b, TIMESLOT ts, [User] u,[SERVICE] s, PRODUCT p

	WHERE b.StylistID=@EmployeeID
	AND ts.SlotNo=b.SlotNo
	AND b.CustomerID=u.UserID
	AND b.ServiceID=s.ServiceID
	AND s.ServiceID=p.ProductID
	/*AND b.Arrived='N'*/
	
	ORDER BY ts.StartTime,ts.EndTime

END
GO
/****** Object:  StoredProcedure [dbo].[SP_GetEmployeeType]    Script Date: 2018/06/09 10:41:34 ******/
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
/****** Object:  StoredProcedure [dbo].[SP_GetEmpNames]    Script Date: 2018/06/09 10:41:34 ******/
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
/****** Object:  StoredProcedure [dbo].[SP_getInvoiceDL]    Script Date: 2018/06/09 10:41:34 ******/
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
/****** Object:  StoredProcedure [dbo].[SP_GetMyNextCustomer]    Script Date: 2018/06/09 10:41:34 ******/
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
/****** Object:  StoredProcedure [dbo].[SP_GetServices]    Script Date: 2018/06/09 10:41:34 ******/
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
	WHERE ProductType = 'S';
END
GO
/****** Object:  StoredProcedure [dbo].[SP_GetStylist]    Script Date: 2018/06/09 10:41:34 ******/
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
/****** Object:  StoredProcedure [dbo].[SP_GetUserDetails]    Script Date: 2018/06/09 10:41:34 ******/
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
/****** Object:  StoredProcedure [dbo].[SP_ProductSearchByTerm]    Script Date: 2018/06/09 10:41:34 ******/
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
/****** Object:  StoredProcedure [dbo].[SP_SearchStylistsBySearchTerm]    Script Date: 2018/06/09 10:41:34 ******/
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
/****** Object:  StoredProcedure [dbo].[SP_UpdateBooking]    Script Date: 2018/06/09 10:41:34 ******/
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
/****** Object:  StoredProcedure [dbo].[SP_UpdateCustVisit]    Script Date: 2018/06/09 10:41:34 ******/
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
/****** Object:  StoredProcedure [dbo].[SP_ViewCustVisit]    Script Date: 2018/06/09 10:41:34 ******/
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
	SELECT * FROM CUST_VISIT c WHERE c.CustomerID=@CustomerID AND c.BookingID=@BookingID
END
GO
USE [master]
GO
ALTER DATABASE [CHEVEUX] SET  READ_WRITE 
GO
