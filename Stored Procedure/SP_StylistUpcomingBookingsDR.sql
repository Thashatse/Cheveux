/*    ==Scripting Parameters==

    Source Server Version : SQL Server 2017 (14.0.1000)
    Source Database Engine Edition : Microsoft SQL Server Enterprise Edition
    Source Database Engine Type : Standalone SQL Server

    Target Server Version : SQL Server 2017
    Target Database Engine Edition : Microsoft SQL Server Standard Edition
    Target Database Engine Type : Standalone SQL Server
*/

USE [CHEVEUX]
GO
/****** Object:  StoredProcedure [dbo].[SP_StylistUpcomingBookingsDR]    Script Date: 8/8/2018 8:48:03 PM ******/
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
	@endDate datetime
AS
BEGIN
	SET NOCOUNT ON;
	SELECT BookingID,B.StylistID,B.CustomerID,
			
		   (SELECT (u.FirstName + ' ' + u.LastName)as[StylistName]
		   FROM [USER] u
		   WHERE u.UserID = @stylistID)AS[StylistName],

		   (SELECT (u.FirstName+' '+u.LastName)as[CustomerName]
		   FROM [USER] u
		   WHERE u.UserID=B.CustomerID)AS[FullName],

		   B.[Date],TS.StartTime,P.ProductID,P.[Name],P.ProductDescription,B.Arrived,P.Price

	From   BOOKING B, TIMESLOT TS, [User] U, PRODUCT P
	Where  b.StylistID = @stylistID
	AND    B.SlotNo = TS.SlotNo 
	AND    B.StylistID = U.UserID 
	AND    B.ServiceID = P.ProductID 
	AND    B.Arrived = 'N' 
	AND	  (B.[Date] BETWEEN @startDate AND @endDate)

	ORDER BY B.[Date],TS.StartTime asc 
END
