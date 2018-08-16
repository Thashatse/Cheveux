USE [CHEVEUX]
GO
/****** Object:  StoredProcedure [dbo].[SP_GetCustomerPastBooking]    Script Date: 2018/07/22 14:46:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
alter PROCEDURE [dbo].[SP_GetCustomerPastBooking]
	@CustID nchar(30)
AS
BEGIN
	Select P.[Name], P.ProductDescription, P.Price,  U.FirstName, B.[Date], TS.StartTime, BookingID, B.Arrived, P.ProductID , B.StylistID       
	From BOOKING B, TIMESLOT TS, [User] U, PRODUCT P
	Where CustomerID = @CustID 
	AND B.SlotNo = TS.SlotNo 
	AND B.StylistID = U.UserID 
	AND B.ServiceID = P.ProductID 
	AND (B.Arrived = 'Y' Or B.Arrived = 'N')
	Order by Date desc
END
