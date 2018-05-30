USE [CHEVEUX]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create PROCEDURE [dbo].[SP_GetCustomerPastBookingDetail] 
	@BookingID nchar(10)
AS
BEGIN
	Select P.[Name], P.ProductDescription, P.Price,  U.FirstName, B.[Date], TS.StartTime, BookingID, B.Arrived       
	From BOOKING B, TIMESLOT TS, [User] U, PRODUCT P
	Where BookingID = @BookingID 
	AND B.SlotNo = TS.SlotNo 
	AND B.StylistID = U.UserID 
	AND B.ServiceID = P.ProductID 
	AND B.Arrived = 'Y' Or B.Arrived = 'N'
END
