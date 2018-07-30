USE [CHEVEUX]
GO
/****** Object:  StoredProcedure [dbo].[SP_SaleOfHairstylist]    Script Date: 2018/07/27 12:03:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Description:	Sales for a hairstylist
-- =============================================
ALTER PROCEDURE [dbo].[SP_SaleOfHairstylist] 
	@StylistID nchar(30),
	@startDate nchar(30)
AS

BEGIN
Select s.SaleID,s.[Date],(u.FirstName + ' ' + u.LastName) AS[FullName], s.CustomerID, s.BookingID
From SALE s, [USER] u, SALES_DTL, BOOKING b
Where s.BookingID = b.BookingID
And b.StylistID = @StylistID
And s.[Date] = @startDate
And u.UserID = b.CustomerID
And s.CustomerID = b.CustomerID
END
