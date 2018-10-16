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
alter PROCEDURE [dbo].[SP_StylistUpcomingBookingsDR]
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
	AND    B.Arrived = 'N' 
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
