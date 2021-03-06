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
alter PROCEDURE SP_CustomersReviewForStylist
	@customerID nchar(30),
	@stylistID nchar(30)
AS
BEGIN
	SET NOCOUNT ON;
	SELECT ReviewID,CustomerID,EmployeeID,primaryBookingID,[Date],[Time],Rating,Comment
	FROM REVIEW
	WHERE CustomerID=@customerID
	and EmployeeID=@stylistID
	and primaryBookingID is null
END
GO
