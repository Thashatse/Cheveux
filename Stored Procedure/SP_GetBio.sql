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
-- =============================================
alter PROCEDURE SP_GetBio
	@EmployeeID nchar(30)
AS
BEGIN
	SET NOCOUNT ON;
	SELECT Bio
	FROM [CHEVEUX].[dbo].[EMPLOYEE]
	WHERE EmployeeID=@EmployeeID
END
GO
