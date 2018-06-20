SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Description:	returns the business table
-- =============================================
CREATE PROCEDURE SP_getBusinessTable
AS
BEGIN
	Select *
	From BUSINESS
END
GO
