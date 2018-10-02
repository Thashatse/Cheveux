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
CREATE PROCEDURE SP_GetCustomersReviews
	@customerID nchar(30)
AS
BEGIN
	SET NOCOUNT ON;
	SELECT ReviewID,CustomerID,EmployeeID,primaryBookingID,[Date],[Time],Rating,Comment
	FROM REVIEW
	WHERE CustomerID=@customerID
END	@customerID nchar(30)
AS
BEGIN
	SET NOCOUNT ON;
	SELECT r.ReviewID,r.CustomerID,r.EmployeeID,r.primaryBookingID,r.[Date],r.[Time],r.Rating,r.Comment,


		   (select UserImage
		   from [USER] stylist
		   where stylist.UserID=r.EmployeeID)as[StylistImage],

		   (select (stylist.FirstName+' '+stylist.LastName) as sName
		   from [USER] stylist
		   where stylist.UserID=r.EmployeeID)as[StylistName],

		   (select UserImage
		   from [USER] customer
		   where customer.UserID=@customerID and customer.UserID=r.CustomerID)as[CustomerImage],

		   (select (customer.FirstName+' '+customer.LastName) as cName
		   from [USER] customer
		   where customer.UserID=@customerID and customer.UserID=r.CustomerID)as[CustomerName]


	FROM REVIEW r,[USER] u
	WHERE r.CustomerID=@customerID
	and r.CustomerID=u.UserID
END
GO
