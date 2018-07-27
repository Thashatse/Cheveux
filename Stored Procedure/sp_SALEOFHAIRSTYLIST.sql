USE [CHEVEUX]
GO
/****** Description:	Sales for a hairstylist ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SP_SaleOfHairstylist]
	@StylistID nchar(30)
AS
BEGIN
Select s.SaleID,s.[Date],(u.FirstName + ' ' + u.LastName) AS[FullName], s.CustomerID, s.BookingID
From SALE s, [USER] u, SALES_DTL, BOOKING b
Where s.BookingID = b.BookingID
And b.StylistID = @StylistID
And u.UserID = b.CustomerID
And s.CustomerID = b.CustomerID
END
