SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Description:	Returns all emplyee types in database
-- =============================================
CREATE PROCEDURE SP_GetEmployeeTypes
AS
BEGIN
	Select DISTINCT [Type]
	From EMPLOYEE
END
GO
