﻿USE [CHEVEUX]
GO
/****** Object:  StoredProcedure [dbo].[SP_SaleOfHairstylist]    Script Date: 2018/07/26 15:23:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


-- Create date: <Create Date,,>
-- Description:	Sales for a hairstylist
-- =============================================
CREATE PROCEDURE [dbo].[SP_SaleOfHairstylist]
	-- Add the parameters for the stored procedure here
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
