USE [CHEVEUX]
GO
/****** Object:  StoredProcedure [dbo].[SP_GetStylist]    Script Date: 23 Jul 2018 5:48:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

create PROCEDURE [dbo].[SP_GetStylist] 
AS
BEGIN
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT [UserID], FirstName, ServiceID
	FROM [USER], STYLIST_SERVICE 
	WHERE [UserID] = EmployeeID AND UserType = 'S'
END
