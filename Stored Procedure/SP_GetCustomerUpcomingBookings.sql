USE [CHEVEUX]
GO
/****** Given a customer ID gets upcoming bookings for that customer ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create PROCEDURE [dbo].[SP_GetCustomerUpcomingBookings]
	@CustID nchar(30)
AS
BEGIN
Select P.[Name], P.ProductDescription, P.Price,  U.FirstName, B.[Date], TS.StartTime, BookingID, B.Arrived       
	From BOOKING B, TIMESLOT TS, [User] U, PRODUCT P
	Where CustomerID = @CustID 
	AND B.SlotNo = TS.SlotNo 
	AND B.StylistID = U.UserID 
	AND B.ServiceID = P.ProductID 
	AND (B.Arrived is null or B.Arrived = 'N') 
	AND B.[Date] > GETDATE()
	END
