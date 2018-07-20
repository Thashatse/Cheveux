USE [CHEVEUX]
GO
/****** Object:  StoredProcedure [dbo].[SP_GetCustomerUpcomingBookings]    Script Date: 2018/07/20 11:31:13 ******/
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
	Where CustomerID = '105242998585655922697'          
	AND B.SlotNo = TS.SlotNo 
	AND B.StylistID = U.UserID 
	AND B.ServiceID = P.ProductID 
	AND B.Arrived = 'N'
	END
