USE [CHEVEUX]
GO
/****** Object:  StoredProcedure [dbo].[SP_GetEmpNames]    Script Date: 5/24/2018 7:18:09 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		S.Maqabangqa
-- Create date: 24.05.2018
-- Description:	Gets names of active employees.
-- =============================================
alter PROCEDURE [dbo].[SP_GetEmpNames]
AS
BEGIN 
	SET NOCOUNT ON;
	SELECT e.EmployeeID, u.FirstName + ' ' + u.LastName AS [Name]
	FROM [CHEVEUX].[dbo].[User] u, EMPLOYEE e
	WHERE u.UserID=e.EmployeeID AND u.Active = 'T' AND e.Type ='S'
	ORDER BY  [Name] asc
END
