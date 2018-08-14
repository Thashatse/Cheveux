USE [CHEVEUX]
GO
/****** Object:  StoredProcedure [dbo].[SP_GetStylist]    Script Date: 2018/08/13 13:38:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

Create PROCEDURE [dbo].[SP_GetStylist] 
AS
BEGIN
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT [UserID], FirstName, ServiceID
	FROM [USER] u, STYLIST_SERVICE ss, EMPLOYEE e 
	WHERE [UserID] = e.EmployeeID AND ss.EmployeeID = [UserID] AND e.Type = 'S' AND UserType = 'E'
END
