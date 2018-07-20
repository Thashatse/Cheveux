USE [CHEVEUX]
GO
/****** Object:  StoredProcedure [dbo].[SP_GetCustomerPastBooking]    Script Date: 2018/07/20 11:01:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[SP_GetCustomerPastBooking] 
	@CustID nchar(30)
AS
BEGIN
	Select P.[Name], P.ProductDescription, P.Price,  U.FirstName, B.[Date], TS.StartTime, BookingID, B.Arrived       
	From BOOKING B, TIMESLOT TS, [User] U, PRODUCT P
	Where CustomerID = @CustID         
	AND B.SlotNo = TS.SlotNo 
	AND B.StylistID = U.UserID 
	AND B.ServiceID = P.ProductID
	AND B.[Date] < (select DATEADD(day, -1,  GETDATE()) as Tommorow)
	AND (SELECT CONVERT (time, SYSDATETIME())) > (Select StartTime
		from TIMESLOT
		where SlotNo = B.SlotNo)
END
