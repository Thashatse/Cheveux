USE [CHEVEUX]
GO
/****** Object:  StoredProcedure [dbo].[SP_GetCustomerPastBooking]    Script Date: 2018/07/22 14:46:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create PROCEDURE [dbo].[SP_SearchBookings]
	@searchTerm nchar(30)
AS
BEGIN
	Select U.FirstName as StylistFirstName, B.[Date], TS.StartTime, BookingID, B.Arrived, B.StylistID, B.Comment, 
			(Select U.FirstName
			 From [User] U
			 Where b.CustomerID = u.UserID) as CustFirstName          
	From BOOKING B, TIMESLOT TS, [User] U
	Where B.SlotNo = TS.SlotNo 
	AND B.StylistID = U.UserID
	AND (B.Arrived = 'Y' Or B.Arrived = 'N')
	And B.BookingID = B.primaryBookingID

	AND (U.FirstName like '%'+@SearchTerm+'%' Or
		U.FirstName like '%'+@SearchTerm+'%' Or
		B.[Date] like '%'+@SearchTerm+'%' Or
		TS.StartTime like '%'+@SearchTerm+'%' Or
		B.Comment like '%'+@SearchTerm+'%' Or
		(Select U.FirstName
			 From [User] U
			 Where b.CustomerID = u.UserID) like '%'+@SearchTerm+'%' )

	Order by Date desc
END
