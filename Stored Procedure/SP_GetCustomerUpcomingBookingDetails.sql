USE [CHEVEUX]
GO
/****** Given a booking ID it Returns the booking detatils ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create PROCEDURE [dbo].[SP_GetCustomerUpcomingBookingDetails] 
	@BookingID nchar(10)
AS
BEGIN
	Select P.[Name], P.ProductDescription, P.Price, U.UserID,  U.FirstName, B.[Date], TS.StartTime, BookingID           
	From BOOKING B, TIMESLOT TS, [User] U, PRODUCT P
	Where BookingID = @BookingID 
	AND B.SlotNo = TS.SlotNo 
	AND B.StylistID = U.UserID 
	AND B.ServiceID = P.ProductID 
	AND (B.Arrived is null or B.Arrived = 'N') 
END