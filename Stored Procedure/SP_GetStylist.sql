USE [CHEVEUX]
GO
/****** Object:  StoredProcedure [dbo].[SP_GetStylist]    Script Date: 2018/08/13 13:38:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

alter PROCEDURE [dbo].[SP_GetStylist] 
AS
BEGIN
	SELECT [UserID], FirstName, ServiceID, p.Name
	FROM [USER] u, STYLIST_SERVICE ss, EMPLOYEE e, PRODUCT p 
	WHERE [UserID] = e.EmployeeID AND ss.EmployeeID = [UserID] AND e.Type = 'S' AND UserType = 'E'
	AND ss.ServiceID = p.ProductID 
	And p.Active = 'Y'
	Order By [FirstName]
END
