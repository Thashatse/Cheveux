USE [CHEVEUX]
GO
/****** Object:  StoredProcedure [dbo].[SP_GetStylist]    Script Date: 05 Jun 2018 9:26:06 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

Create PROCEDURE [dbo].[SP_GetStylist] 
	@ServiceID nchar(10)
AS
BEGIN
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT u.FirstName
	FROM USERS u, STYLIST_SERVICE ss, PRODUCT p
	WHERE @ServiceID=p.ProductID AND UserType='E' AND u.UserCode = ss.EmployeeID AND ss.ServiceID = p.ProductID
END
