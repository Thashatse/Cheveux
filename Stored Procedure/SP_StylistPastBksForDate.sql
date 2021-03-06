USE [CHEVEUX]
GO
/****** Object:  StoredProcedure [dbo].[SP_StylistPastBksForDate]    Script Date: 10/16/2018 7:35:29 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		S.Maqabangqa
-- Description:	Gets stylists past bookings for a specific date
-- =============================================
ALTER PROCEDURE [dbo].[SP_StylistPastBksForDate]
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

	From   [CHEVEUX].[dbo].[BOOKING] B, TIMESLOT TS, [User] U, EMPLOYEE e
	Where  b.StylistID = @stylistID
	AND		b.StylistID = e. EmployeeID
	AND    B.SlotNo = TS.SlotNo 
	AND    B.CustomerID = U.UserID 
	AND	   (B.Arrived = 'Y' or B.CustomerID='0')
	AND    B.[Date] = @day
	AND	   B.[Date] !> CAST(GETDATE() AS DATE)
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
