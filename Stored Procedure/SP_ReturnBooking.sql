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
CREATE PROCEDURE SP_ReturnBooking
	@bookingID nchar(10),
	@customerID nchar(30),
	@stylistID nchar(30),
	@date datetime
AS
BEGIN
	SET NOCOUNT ON;
	SELECT b.BookingID,b.CustomerID,b.StylistID,b.SlotNo,ts.StartTime,b.[Date]
	FROM BOOKING b, TIMESLOT ts
	WHERE b.BookingID=@bookingID
	and   b.CustomerID=@customerID
	and   b.StylistID=@stylistID
	and   b.[Date]=@date
	and   b.SlotNo=ts.SlotNo
END
GO
