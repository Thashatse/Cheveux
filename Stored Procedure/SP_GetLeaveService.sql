USE [CHEVEUX]
GO
/****** Object:  StoredProcedure [dbo].[SP_GetLeaveService]    Script Date: 2018/09/26 13:51:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [dbo].[SP_GetLeaveService]

AS
BEGIN

	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT ProductID, [Name], NoOfSlots
	FROM PRODUCT, SERVICE
	WHERE ProductID = ServiceID AND [ProductType(T/A/S)] = 'U'
END
