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
-- Description:	Checks if customer has past bookings with a specific stylist
-- =============================================
CREATE PROCEDURE SP_CheckCustomerStylistBooking
	@customerID nchar(30),
	@stylistID nchar(30)
AS
BEGIN
	SET NOCOUNT ON;
	SELECT B.BookingID,B.primaryBookingID AS [PrimaryID],B.StylistID,B.CustomerID,
		   (SELECT (u.FirstName + ' ' + u.LastName)as[StylistName]
		   FROM [USER] u
		   WHERE u.UserID = @stylistID)AS[StylistName],

		   (SELECT (u.FirstName+' '+u.LastName)as[CustomerName]
		   FROM [USER] u
		   WHERE u.UserID=@customerID)AS[CustomerName],

		   B.Arrived,

		   (SELECT u.Active
		   FROM [USER] u
		   WHERE u.UserID = @stylistID)AS[StylistAcitveStatus]

	FROM BOOKING B, EMPLOYEE E, [USER] U
	WHERE B.BookingID = B.primaryBookingID
	AND    B.StylistID = E.EmployeeID
	AND	   B.CustomerID = U.UserID
	AND	   B.Arrived = 'Y'
	AND	   B.StylistID=@stylistID
	AND	   B.CustomerID=@customerID
END
GO
