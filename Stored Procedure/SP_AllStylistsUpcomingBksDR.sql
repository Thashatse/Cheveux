-- ================================================
-- Template generated from Template Explorer using:
-- Create Procedure (New Menu).SQL
--
-- Use the Specify Values for Template Parameters 
-- command (Ctrl-Shift-M) to fill in the parameter 
-- values below.
--
-- This block of comments will not be included in
-- the definition of the procedure.
-- ================================================
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		S.Maqabangqa
-- Description:	Gets all stylists upcoming bookings according to a date range.
-- =============================================
CREATE PROCEDURE SP_AllStylistsUpcomingBksDR
@startDate datetime,
@endDate datetime
AS
BEGIN
	SET NOCOUNT ON;
	SELECT BookingID,B.StylistID,B.CustomerID,
			
		   (SELECT (u.FirstName + ' ' + u.LastName)as[stylist]
		   FROM [USER] u 
		   WHERE u.UserID=B.StylistID)AS[StylistName],

		   (SELECT (u.FirstName+' '+u.LastName)as[CustomerName]
		   FROM [USER] u
		   WHERE u.UserID=B.CustomerID)AS[FullName],

		   B.[Date],TS.StartTime,P.ProductID,P.[Name],P.ProductDescription,B.Arrived,P.Price

	From   BOOKING B, TIMESLOT TS, [User] U, PRODUCT P
	Where  B.SlotNo = TS.SlotNo 
	AND    B.StylistID = U.UserID 
	AND    B.ServiceID = P.ProductID 
	AND    B.Arrived = 'N' 
	AND	  (B.[Date] BETWEEN @startDate AND @endDate)
	AND   B.[Date] !< CAST(GETDATE() AS DATE)

	ORDER BY B.[Date],TS.StartTime asc 
END
GO