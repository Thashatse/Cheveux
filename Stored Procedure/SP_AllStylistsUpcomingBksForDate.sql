USE [CHEVEUX]
GO
/****** Object:  StoredProcedure [dbo].[SP_AllStylistsUpcomingBksForDate]    Script Date: 10/16/2018 7:21:23 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		S.Maqabangqa
-- Description:	Gets bookings for all stylists for a specific day
-- =============================================
ALTER PROCEDURE [dbo].[SP_AllStylistsUpcomingBksForDate]
@bookingDate datetime,
@sortBy nvarchar(max)=null,
	@sortDir nvarchar(max)=null
AS
BEGIN
	SET NOCOUNT ON;
		SELECT BookingID,B.primaryBookingID AS [PrimaryID],B.StylistID,B.CustomerID,
			
		   (SELECT (u.FirstName + ' ' + u.LastName)as[StylistName]
		   FROM [USER] u
		   WHERE u.UserID = B.StylistID)AS[StylistName],

		   (SELECT (u.FirstName+' '+u.LastName)as[CustomerName]
		   FROM [USER] u
		   WHERE u.UserID=B.CustomerID)AS[FullName],

		   B.[Date],TS.StartTime,TS.EndTime,B.Arrived

	From   [CHEVEUX].[dbo].[BOOKING] B, TIMESLOT TS, [User] U, EMPLOYEE e
	Where  B.SlotNo = TS.SlotNo 
	AND    B.StylistID = e.EmployeeID
	AND	   B.CustomerID = U.UserID
	AND    (B.Arrived = 'N' or B.CustomerID='0')
	AND    B.[Date] = @bookingDate
	AND    B.[Date] !< CAST(GETDATE() AS DATE)
	AND		B.BookingID = B.primaryBookingID
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
