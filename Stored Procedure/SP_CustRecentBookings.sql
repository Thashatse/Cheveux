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
-- Author:	S.MAQABANGQA
-- =============================================
CREATE PROCEDURE SP_CustRecentBookings
	@CustID nchar(30)
AS
BEGIN
	Select U.FirstName, B.[Date], TS.StartTime, BookingID, B.Arrived, B.StylistID       
	From BOOKING B, TIMESLOT TS, [User] U
	Where CustomerID = @CustID 
	AND B.SlotNo = TS.SlotNo 
	AND B.StylistID = U.UserID
	AND B.Arrived = 'Y'
	And B.BookingID = B.primaryBookingID
	AND	B.[Date] !< DATEADD(mm, -2, GETDATE())
	Order by B.[Date] desc
END
GO
