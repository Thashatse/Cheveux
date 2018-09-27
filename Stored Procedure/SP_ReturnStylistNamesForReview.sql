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
-- Description:	
-- =============================================
alter PROCEDURE SP_ReturnStylistNamesForReview
	@customerID nchar(30)
AS
BEGIN
	SET NOCOUNT ON;
	
	SELECT distinct B.StylistID, 
				(U.FirstName + ' ' + U.LastName)as[StylistName]
  		
	FROM [CHEVEUX].[dbo].[BOOKING] B,EMPLOYEE E, [USER] U
	WHERE B.BookingID = B.[primaryBookingID]
	AND    B.StylistID = E.EmployeeID
	AND    B.StylistID=U.UserID
	AND	   B.Arrived = 'Y'
	AND	   B.StylistID= B.StylistID
	AND	   B.[Date] !> CAST(GETDATE() AS DATE)
	AND	   B.CustomerID=@customerID

END
GO
