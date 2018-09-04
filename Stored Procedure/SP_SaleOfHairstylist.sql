USE [CHEVEUX]
GO
/****** Object:  StoredProcedure [dbo].[SP_SaleOfHairstylist]    Script Date: 2018/07/30 12:24:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Description:	Sales for a hairstylist
Alter PROCEDURE [dbo].[SP_SaleOfHairstylist] 
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
