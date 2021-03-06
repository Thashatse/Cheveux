USE [CHEVEUX]
GO
/****** getst booking that match the search term and date range ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
alter PROCEDURE [dbo].[SP_SearchBookings]
	@startDate datetime,
	@endDate datetime
AS
BEGIN
	Select U.FirstName as StylistFirstName, B.[Date], TS.StartTime, BookingID, B.Arrived, B.StylistID, B.Comment, B.CustomerID, 
			(Select U.FirstName+ ' '+U.LastName
			 From [User] U
			 Where b.CustomerID = u.UserID) as CustFirstName          
	From BOOKING B, TIMESLOT TS, [User] U
	Where B.SlotNo = TS.SlotNo 
	AND B.StylistID = U.UserID
	AND (B.Arrived = 'Y' Or B.Arrived = 'N')
	And B.BookingID = B.primaryBookingID

	AND B.[Date] BETWEEN @startDate   AND @endDate

	Order by Date desc
END
