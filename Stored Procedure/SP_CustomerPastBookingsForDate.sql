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
-- Author:		S.Maqabangqa
-- =============================================
alter PROCEDURE SP_CustomerPastBookingsForDate
	@customerID nchar(30),
	@date datetime
AS
BEGIN
	SET NOCOUNT ON;
	SELECT BookingID,b.primaryBookingID AS [PrimaryID],B.StylistID,B.CustomerID,
			
		   (SELECT (u.FirstName + ' ' + u.LastName)as[StylistName]
		   FROM [USER] u
		   WHERE u.UserID = b.StylistID)AS[StylistName],

		   (SELECT (u.FirstName+' '+u.LastName)as[CustomerName]
		   FROM [USER] u
		   WHERE u.UserID=@customerID)AS[CustomerFullName],

		   B.[Date],TS.StartTime,B.Arrived

	From   [CHEVEUX].[dbo].[BOOKING] B, TIMESLOT TS, [User] U, EMPLOYEE e
	Where  b.StylistID = u.UserID
	AND		b.StylistID = e.EmployeeID
	AND    B.SlotNo = TS.SlotNo 
	AND    B.CustomerID = @customerID
	AND	   B.Arrived = 'Y'
	AND	   B.[Date] !> CAST(GETDATE() AS DATE)
	AND B.[Date]=@date
	and B.BookingID=B.primaryBookingID

END
GO
