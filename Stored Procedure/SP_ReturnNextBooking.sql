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
-- Author:		S.MAQABANGQA
-- =============================================
CREATE PROCEDURE SP_ReturnNextBooking
	@startTime nvarchar(20),
	@bookingID nchar(10),
	@stylistID nchar(30),
	@date datetime
AS
BEGIN
	SET NOCOUNT ON;

	SELECT top(1) b.BookingID,b.SlotNo,ts.StartTime
	FROM BOOKING b, TIMESLOT ts
	WHERE b.BookingID=b.primaryBookingID
	and b.BookingID <> @bookingID
	and b.StylistID=@stylistID
	and b.[Date]=@date
	and b.SlotNo=ts.SlotNo
	and ts.StartTime !< @startTime
	
	ORDER BY ts.StartTime
END
GO
