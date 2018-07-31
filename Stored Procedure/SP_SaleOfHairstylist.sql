USE [CHEVEUX]
GO
/****** Object:  StoredProcedure [dbo].[SP_SaleOfHairstylist]    Script Date: 2018/07/30 12:24:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Description:	Sales for a hairstylist
-- =============================================
create PROCEDURE [dbo].[SP_SaleOfHairstylist] 
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
