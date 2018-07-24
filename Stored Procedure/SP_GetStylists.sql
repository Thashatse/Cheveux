USE [CHEVEUX]
GO
/****** Object:  StoredProcedure [dbo].[SP_GetStylist]    Script Date: 2018/07/24 12:58:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [dbo].[SP_GetStylist] 
AS
BEGIN
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT [UserID], FirstName, ServiceID
	FROM [USER], STYLIST_SERVICE 
	WHERE [UserID] = EmployeeID AND UserType = 'E'
END